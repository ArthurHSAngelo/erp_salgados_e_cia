/* =====================================================================
   ERP SALGADOS & CIA - Atualizacao de um banco que JA EXISTE

   Para quem JA TEM o banco "salgados_cia" criado com uma versao anterior do
   01_schema.sql e quer ganhar as novidades sem apagar nada. Rode este arquivo
   UMA vez, inteiro, como root. E seguro rodar de novo (nao repete nada), e
   serve para qualquer versao antiga: so aplica o que ainda falta.
   Quem cria o banco do zero com o 01_schema.sql atual NAO precisa deste arquivo.

   O que ele faz, nesta ordem:
   1. Bloqueio de login por tentativas: cria as colunas tentativas_falhas e
      bloqueado_ate na tabela usuario, troca o trigger trg_usuario_au (para a
      contagem de tentativas nao encher o log) e cria as procedures
      sp_login_falhou e sp_login_ok.
   2. Vale Alimentacao por unidade: cria a coluna aceita_vale_alimentacao na
      tabela unidade (todas comecam como "nao aceita"; marque as que aceitam na
      tela Unidades), acrescenta a forma de pagamento ValeAlimentacao nas vendas
      e nas comandas e troca os triggers que anotam o log da unidade e os que
      conferem o pagamento.
   3. Correcao de caixa fechado: troca o trigger trg_caixa_bu e cria a procedure
      sp_editar_caixa_fechado, que permite corrigir o dinheiro contado e a
      maquininha de um caixa ja fechado, so com motivo e autorizacao de Gerente
      ou Administrador, com registro no log.
   ===================================================================== */
USE salgados_cia;

/* ---- 1. Bloqueio de login por tentativas ---- */

SET @tem := (SELECT COUNT(*) FROM information_schema.columns
              WHERE table_schema = 'salgados_cia' AND table_name = 'usuario' AND column_name = 'tentativas_falhas');
SET @sql := IF(@tem = 0,
               'ALTER TABLE usuario ADD COLUMN tentativas_falhas INT UNSIGNED NOT NULL DEFAULT 0, ADD COLUMN bloqueado_ate DATETIME NULL',
               'SELECT ''colunas ja existem'' AS aviso');
PREPARE p FROM @sql;
EXECUTE p;
DEALLOCATE PREPARE p;

DELIMITER $$

DROP TRIGGER IF EXISTS trg_usuario_au$$
CREATE TRIGGER trg_usuario_au AFTER UPDATE ON usuario FOR EACH ROW
BEGIN
    -- so anota no log quando o cadastro mudou (nao anota a contagem de tentativas de login)
    IF NOT (OLD.login <=> NEW.login AND OLD.perfil <=> NEW.perfil AND OLD.unidade_id <=> NEW.unidade_id
            AND OLD.status <=> NEW.status AND OLD.senha_hash <=> NEW.senha_hash) THEN
        INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
        VALUES (@usuario_id, 'UPDATE', 'usuario', NEW.id, NEW.login,
                LEFT(CONCAT_WS('; ', fn_diff('login', OLD.login, NEW.login), fn_diff('perfil', OLD.perfil, NEW.perfil),
                                     fn_diff('unidade', (SELECT nome FROM unidade WHERE id = OLD.unidade_id), (SELECT nome FROM unidade WHERE id = NEW.unidade_id)),
                                     fn_diff('status', OLD.status, NEW.status),
                                     IF(OLD.senha_hash <=> NEW.senha_hash, NULL, 'senha alterada')), 255), @unidade_id);
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_login_falhou / sp_login_ok  (bloqueio por tentativas de login)
   NO DIA A DIA: protege contra quem fica "chutando" senhas. Depois de 5 senhas
   erradas seguidas para o mesmo login, o acesso fica bloqueado por 15 minutos,
   mesmo que a senha certa seja digitada. Como a contagem fica guardada aqui no
   banco, o bloqueio vale em qualquer computador da rede. Passado o prazo, o
   login volta sozinho; um login certo zera a contagem; e trocar a senha do
   login tambem desbloqueia (o programa faz isso).
   sp_login_falhou(login): conta mais uma tentativa errada e devolve uma linha
   com tentativas, bloqueado (0 ou 1) e minutos que faltam para liberar.
   sp_login_ok(login): zera a contagem depois de um login certo.
   --------------------------------------------------------------------- */
DROP PROCEDURE IF EXISTS sp_login_falhou$$
CREATE PROCEDURE sp_login_falhou (IN p_login VARCHAR(40))
BEGIN
    DECLARE v_id INT UNSIGNED;
    DECLARE v_tent INT UNSIGNED DEFAULT 0;
    DECLARE v_ate DATETIME;
    DECLARE v_limite INT DEFAULT 5;
    DECLARE v_minutos INT DEFAULT 15;
    DECLARE v_bloq TINYINT DEFAULT 0;
    DECLARE v_resta INT DEFAULT 0;

    START TRANSACTION;
    SELECT id, tentativas_falhas, bloqueado_ate INTO v_id, v_tent, v_ate
      FROM usuario WHERE login = p_login LIMIT 1 FOR UPDATE;

    IF v_id IS NULL THEN
        COMMIT;
        SELECT 0 AS tentativas, 0 AS bloqueado, 0 AS minutos;
    ELSE
        IF v_ate IS NOT NULL AND v_ate > NOW() THEN
            SET v_bloq = 1;                                   -- ja esta bloqueado: nao conta de novo
        ELSE
            IF v_ate IS NOT NULL THEN SET v_tent = 0; END IF; -- o prazo venceu: recomeca a contagem
            SET v_tent = v_tent + 1;
            IF v_tent >= v_limite THEN
                SET v_ate = NOW() + INTERVAL v_minutos MINUTE;
                SET v_bloq = 1;
                UPDATE usuario SET tentativas_falhas = v_tent, bloqueado_ate = v_ate WHERE id = v_id;
                INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
                VALUES (NULL, 'UPDATE', 'usuario', v_id, p_login,
                        CONCAT('Acesso bloqueado por ', v_minutos, ' minutos apos ', v_tent, ' tentativas erradas'), NULL);
            ELSE
                UPDATE usuario SET tentativas_falhas = v_tent, bloqueado_ate = NULL WHERE id = v_id;
            END IF;
        END IF;
        IF v_bloq = 1 THEN
            SET v_resta = CEIL(TIMESTAMPDIFF(SECOND, NOW(), v_ate) / 60);
        END IF;
        COMMIT;
        SELECT v_tent AS tentativas, v_bloq AS bloqueado, v_resta AS minutos;
    END IF;
END$$

DROP PROCEDURE IF EXISTS sp_login_ok$$
CREATE PROCEDURE sp_login_ok (IN p_login VARCHAR(40))
BEGIN
    UPDATE usuario SET tentativas_falhas = 0, bloqueado_ate = NULL
     WHERE login = p_login AND (tentativas_falhas <> 0 OR bloqueado_ate IS NOT NULL);
END$$

DELIMITER ;

-- Conferir:  SHOW PROCEDURE STATUS WHERE Db = 'salgados_cia' AND Name LIKE 'sp_login%';

/* ---- 2. Vale Alimentacao por unidade ---- */

SET @tem := (SELECT COUNT(*) FROM information_schema.columns
              WHERE table_schema = 'salgados_cia' AND table_name = 'unidade' AND column_name = 'aceita_vale_alimentacao');
SET @sql := IF(@tem = 0,
               'ALTER TABLE unidade ADD COLUMN aceita_vale_alimentacao TINYINT(1) NOT NULL DEFAULT 0 AFTER telefone',
               'SELECT ''coluna ja existe'' AS aviso');
PREPARE p FROM @sql;
EXECUTE p;
DEALLOCATE PREPARE p;

ALTER TABLE pagamento_comanda MODIFY forma_pagamento ENUM('Dinheiro','Pix','Credito','Debito','ValeAlimentacao','Notinha') NOT NULL;
ALTER TABLE pagamento_venda   MODIFY forma_pagamento ENUM('Dinheiro','Pix','Credito','Debito','ValeAlimentacao','Notinha') NOT NULL;

DELIMITER $$

DROP TRIGGER IF EXISTS trg_unidade_ai$$
CREATE TRIGGER trg_unidade_ai AFTER INSERT ON unidade FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'unidade', NEW.id, NEW.nome,
            LEFT(CONCAT('cnpj=', IFNULL(NEW.cnpj, ''), '; endereco=', NEW.endereco, ', ', IFNULL(NEW.numero, ''), '; vale alimentacao=', IF(NEW.aceita_vale_alimentacao = 1, 'aceita', 'nao aceita')), 255), @unidade_id)$$

DROP TRIGGER IF EXISTS trg_unidade_au$$
CREATE TRIGGER trg_unidade_au AFTER UPDATE ON unidade FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'unidade', NEW.id, NEW.nome,
            LEFT(CONCAT_WS('; ', fn_diff('nome', OLD.nome, NEW.nome), fn_diff('cnpj', OLD.cnpj, NEW.cnpj),
                                 fn_diff('endereco', OLD.endereco, NEW.endereco), fn_diff('numero', OLD.numero, NEW.numero),
                                 fn_diff('vale alimentacao', IF(OLD.aceita_vale_alimentacao = 1, 'aceita', 'nao aceita'), IF(NEW.aceita_vale_alimentacao = 1, 'aceita', 'nao aceita')),
                                 fn_diff('status', OLD.status, NEW.status)), 255), @unidade_id)$$

DROP TRIGGER IF EXISTS trg_pag_comanda_bi$$
CREATE TRIGGER trg_pag_comanda_bi BEFORE INSERT ON pagamento_comanda FOR EACH ROW
BEGIN
    DECLARE v_status VARCHAR(20);
    DECLARE v_cliente INT UNSIGNED;
    DECLARE v_unidade INT UNSIGNED;
    SELECT status, cliente_id, unidade_id INTO v_status, v_cliente, v_unidade FROM comanda WHERE id = NEW.comanda_id;
    IF v_status IS NULL OR v_status <> 'Aberta' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Comanda inexistente ou nao esta aberta.';
    END IF;
    IF NEW.forma_pagamento = 'Notinha' AND v_cliente IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Notinha exige cliente cadastrado na comanda.';
    END IF;
    IF NEW.forma_pagamento = 'ValeAlimentacao'
       AND NOT EXISTS (SELECT 1 FROM unidade WHERE id = v_unidade AND aceita_vale_alimentacao = 1) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Esta unidade nao aceita vale alimentacao.';
    END IF;
END$$

DROP TRIGGER IF EXISTS trg_pagamento_bi$$
CREATE TRIGGER trg_pagamento_bi BEFORE INSERT ON pagamento_venda FOR EACH ROW
BEGIN
    DECLARE v_status VARCHAR(20);
    DECLARE v_cliente INT UNSIGNED;
    DECLARE v_unidade INT UNSIGNED;
    SELECT status, cliente_id, unidade_id INTO v_status, v_cliente, v_unidade FROM venda WHERE id = NEW.venda_id;
    IF v_status IS NULL OR v_status <> 'Concluida' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda inexistente ou cancelada.';
    END IF;
    IF NEW.forma_pagamento = 'Notinha' AND v_cliente IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Notinha exige cliente cadastrado na venda.';
    END IF;
    IF NEW.forma_pagamento = 'ValeAlimentacao'
       AND NOT EXISTS (SELECT 1 FROM unidade WHERE id = v_unidade AND aceita_vale_alimentacao = 1) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Esta unidade nao aceita vale alimentacao.';
    END IF;
END$$

DELIMITER ;

/* ---- 3. Correcao de caixa fechado ---- */

DELIMITER $$

DROP TRIGGER IF EXISTS trg_caixa_bu$$
CREATE TRIGGER trg_caixa_bu BEFORE UPDATE ON caixa FOR EACH ROW
BEGIN
    IF OLD.status = 'Fechado' THEN
        IF IFNULL(@corrigindo_caixa, 0) <> 1 THEN
            SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa fechado nao pode ser alterado.';
        END IF;
        IF NEW.status <> OLD.status
           OR NEW.unidade_id <> OLD.unidade_id
           OR NEW.responsavel_funcionario_id <> OLD.responsavel_funcionario_id
           OR NEW.valor_inicial <> OLD.valor_inicial
           OR NOT (NEW.data_abertura <=> OLD.data_abertura)
           OR NOT (NEW.data_fechamento <=> OLD.data_fechamento) THEN
            SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Na correcao so podem mudar o dinheiro contado e a maquininha.';
        END IF;
    END IF;
END$$

DROP PROCEDURE IF EXISTS sp_editar_caixa_fechado$$
/* ---------------------------------------------------------------------
   sp_editar_caixa_fechado  (procedure: corrigir o caixa ja FECHADO)
   NO DIA A DIA: na conferencia o responsavel digitou o dinheiro ou o valor
   da maquininha errado. So um Gerente ou o Administrador autoriza a
   correcao (o autorizador digita o login dele na tela), e e obrigatorio
   dizer o motivo. Muda so o dinheiro contado e a maquininha contada; a
   diferenca e recalculada com a mesma conta do fechamento. So vale para
   caixa fechado, da loja de trabalho. O log guarda o antes e o depois, o
   motivo e quem autorizou.
   Uso: CALL sp_editar_caixa_fechado(id_caixa, dinheiro_contado, maquininha_contada, 'motivo', id_autorizador);
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_editar_caixa_fechado (IN p_caixa INT UNSIGNED, IN p_contado DECIMAL(10,2), IN p_maquininha DECIMAL(10,2), IN p_motivo VARCHAR(200), IN p_autorizador INT UNSIGNED)
BEGIN
    DECLARE v_status VARCHAR(10);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_inicial DECIMAL(10,2);
    DECLARE v_contado DECIMAL(10,2);
    DECLARE v_maq DECIMAL(10,2);
    DECLARE v_dif DECIMAL(10,2);
    DECLARE v_din DECIMAL(10,2);
    DECLARE v_perfil VARCHAR(20);
    DECLARE v_login VARCHAR(40);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN SET @corrigindo_caixa = 0; ROLLBACK; RESIGNAL; END;

    SELECT status, unidade_id, valor_inicial, valor_contado_dinheiro, valor_contado_maquininha, diferenca
      INTO v_status, v_unidade, v_inicial, v_contado, v_maq, v_dif
      FROM caixa WHERE id = p_caixa;
    IF v_status IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa inexistente.';
    END IF;
    IF v_status <> 'Fechado' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Somente um caixa fechado pode ser corrigido aqui.';
    END IF;
    IF @unidade_id IS NOT NULL AND @unidade_id <> v_unidade THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Este caixa pertence a outra unidade.';
    END IF;
    IF p_contado IS NULL OR p_contado < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Informe o valor contado em dinheiro (zero ou maior).';
    END IF;
    IF p_maquininha IS NOT NULL AND p_maquininha < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'O valor da maquininha nao pode ser negativo.';
    END IF;
    IF p_motivo IS NULL OR TRIM(p_motivo) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Informe o motivo da edicao.';
    END IF;
    SELECT perfil, login INTO v_perfil, v_login FROM usuario WHERE id = p_autorizador AND status = 'Ativo';
    IF v_perfil IS NULL OR v_perfil = 'Atendente' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Apenas Administrador ou Gerente pode autorizar.';
    END IF;
    IF p_contado = v_contado AND p_maquininha <=> v_maq THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nenhuma alteracao foi feita.';
    END IF;

    SELECT COALESCE(SUM(total), 0) INTO v_din FROM vw_caixa_totais
     WHERE caixa_id = p_caixa AND forma_pagamento = 'Dinheiro';

    START TRANSACTION;
    SET @corrigindo_caixa = 1;
    UPDATE caixa
       SET valor_contado_dinheiro = p_contado,
           valor_contado_maquininha = p_maquininha,
           diferenca = p_contado - (v_inicial + v_din)
     WHERE id = p_caixa;
    SET @corrigindo_caixa = 0;
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'caixa', p_caixa, CONCAT('Caixa ', p_caixa),
            LEFT(CONCAT_WS('; ',
                 'correcao de caixa fechado',
                 fn_diff('dinheiro contado', v_contado, p_contado),
                 fn_diff('maquininha', v_maq, p_maquininha),
                 fn_diff('diferenca', v_dif, p_contado - (v_inicial + v_din)),
                 CONCAT('motivo: ', TRIM(p_motivo)),
                 CONCAT('autorizado por: ', v_login)), 255), @unidade_id);
    COMMIT;
END$$

DELIMITER ;

SELECT 'Banco atualizado' AS resultado;

/* =====================================================================
   ERP SALGADOS & CIA - Atualizacao: Vale Alimentacao por unidade

   Para quem JA TEM o banco "salgados_cia" criado com uma versao anterior do
   01_schema.sql e quer ganhar o Vale Alimentacao sem apagar nada. Rode este
   arquivo UMA vez, inteiro, como root. E seguro rodar de novo (nao repete nada).
   Quem cria o banco do zero com o 01_schema.sql atual NAO precisa deste arquivo.

   O que ele faz:
   1. cria a coluna aceita_vale_alimentacao na tabela unidade (todas comecam
      como "nao aceita"; marque as que aceitam na tela Unidades);
   2. acrescenta a forma de pagamento ValeAlimentacao nas vendas e nas comandas;
   3. troca os triggers que anotam o log da unidade e os que conferem o
      pagamento (Vale Alimentacao so e aceito em unidade que marcou que aceita).
   Rode depois do 04 (se o seu banco e anterior ao bloqueio de login).
   ===================================================================== */
USE salgados_cia;

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

SELECT 'Vale Alimentacao instalado' AS resultado;

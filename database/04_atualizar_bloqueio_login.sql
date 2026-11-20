/* =====================================================================
   ERP SALGADOS & CIA - Atualizacao: bloqueio de login por tentativas

   Para quem JA TEM o banco "salgados_cia" criado com uma versao anterior do
   01_schema.sql e quer ganhar o bloqueio de login sem apagar nada. Rode este
   arquivo UMA vez, inteiro, como root. E seguro rodar de novo (nao repete nada).
   Quem cria o banco do zero com o 01_schema.sql atual NAO precisa deste arquivo.

   O que ele faz:
   1. cria as colunas tentativas_falhas e bloqueado_ate na tabela usuario;
   2. troca o trigger trg_usuario_au, para a contagem de tentativas nao encher o log;
   3. cria as procedures sp_login_falhou e sp_login_ok.
   ===================================================================== */
USE salgados_cia;

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

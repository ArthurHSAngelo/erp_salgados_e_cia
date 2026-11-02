/* =====================================================================
   ERP SALGADOS & CIA - Banco de dados (versao enxuta, MySQL 8.0+)
   Ordem: 01_schema.sql -> 02_dados_teste.sql

   COMO LER ESTE ARQUIVO
   Acima de cada tabela, trigger, procedure e view ha um comentario
   "NO DIA A DIA" explicando, em linguagem simples, para que ela serve
   na rotina da salgaderia.

   Vocabulario rapido:
   - TABELA     = onde os dados ficam guardados (como uma planilha).
   - TRIGGER    = regra automatica: o proprio banco executa quando algo
                  e gravado (ex.: vendeu -> baixa o estoque sozinho).
   - PROCEDURE  = "botao" que o sistema C# chama para fazer uma tarefa
                  completa (ex.: fechar o caixa).
   - VIEW       = consulta pronta, usada nos relatorios.

   O C# deve executar  SET @usuario_id = <id do logado>;  ao abrir a conexao,
   para o log saber quem fez cada operacao.
   ===================================================================== */
DROP DATABASE IF EXISTS salgados_cia;
CREATE DATABASE salgados_cia CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE salgados_cia;

/* =====================================================================
   TABELAS
   ===================================================================== */

/* ---------------------------------------------------------------------
   UNIDADE
   NO DIA A DIA: cada loja da rede (ex.: "Centro", "Bairro"). Quase tudo no
   sistema pertence a uma unidade: o caixa, o estoque, as vendas e os
   funcionarios. Se a rede abrir uma loja nova, basta cadastrar aqui.
   Loja fechada e marcada como Inativa, nunca apagada.
   --------------------------------------------------------------------- */
CREATE TABLE unidade (
    id       INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome     VARCHAR(80)  NOT NULL UNIQUE,
    endereco VARCHAR(150) NOT NULL,
    telefone VARCHAR(20),
    status   ENUM('Ativa','Inativa') NOT NULL DEFAULT 'Ativa'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   USUARIO
   NO DIA A DIA: quem pode entrar no sistema (dono, gerente, atendente).
   O PERFIL define o que cada um pode fazer: o Atendente so vende; o Gerente
   e o Administrador tambem cancelam vendas e veem relatorios gerenciais.
   A senha e guardada como "hash" (embaralhada), entao nem o dono do banco
   consegue ler a senha de ninguem. Funcionario que sai e Inativado, para
   o historico de vendas dele nao se perder. Nao existem dois logins iguais.
   --------------------------------------------------------------------- */
CREATE TABLE usuario (
    id         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome       VARCHAR(100) NOT NULL,
    login      VARCHAR(40)  NOT NULL UNIQUE,
    senha_hash VARCHAR(255) NOT NULL,              -- hash gerado no C# (BCrypt)
    perfil     ENUM('Administrador','Gerente','Atendente') NOT NULL DEFAULT 'Atendente',
    unidade_id INT UNSIGNED NOT NULL,
    status     ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo',
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   PRODUTO
   NO DIA A DIA: o "cardapio" do balcao: coxinha, pastel, refrigerante, etc.,
   com preco de venda. Tambem guarda o ESTOQUE MINIMO (quando o saldo ficar
   abaixo dele, o produto aparece como "estoque critico" no relatorio) e se o
   produto TEM VALIDADE (salgado tem, refrigerante em lata nao precisa).
   Produto que ja foi vendido nao pode ser apagado, so Inativado. O preco
   nunca pode ser zero ou negativo.
   --------------------------------------------------------------------- */
CREATE TABLE produto (
    id                INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome              VARCHAR(100) NOT NULL UNIQUE,
    categoria         VARCHAR(50)  NOT NULL,
    preco_venda       DECIMAL(10,2) NOT NULL CHECK (preco_venda > 0),
    unidade_medida    VARCHAR(10)  NOT NULL DEFAULT 'un',
    estoque_minimo    INT UNSIGNED NOT NULL DEFAULT 0,
    controla_validade TINYINT(1)   NOT NULL DEFAULT 0,
    status            ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   FORNECEDOR
   NO DIA A DIA: quem vende os salgados prontos para a rede (a salgaderia nao
   fabrica nada). Quando chega mercadoria, a entrada de estoque registra de
   qual fornecedor veio. Evita cadastrar o mesmo CNPJ/CPF duas vezes.
   --------------------------------------------------------------------- */
CREATE TABLE fornecedor (
    id       INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome     VARCHAR(120) NOT NULL,
    cnpj_cpf VARCHAR(18)  NOT NULL UNIQUE,
    telefone VARCHAR(20),
    email    VARCHAR(120),
    status   ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ESTOQUE
   NO DIA A DIA: responde "quantas coxinhas tem na loja Centro agora?".
   Uma linha por produto em cada unidade, sempre com o saldo ATUAL. Ninguem
   mexe aqui na mao: o saldo sobe quando chega mercadoria e desce quando
   vende ou perde (as triggers cuidam disso). O saldo nunca fica negativo.
   --------------------------------------------------------------------- */
CREATE TABLE estoque (
    unidade_id INT UNSIGNED NOT NULL,
    produto_id INT UNSIGNED NOT NULL,
    quantidade INT NOT NULL DEFAULT 0 CHECK (quantidade >= 0),
    PRIMARY KEY (unidade_id, produto_id),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   MOVIMENTACAO_ESTOQUE
   NO DIA A DIA: o "extrato" do estoque, como o extrato de uma conta bancaria.
   Cada entrada, venda, cancelamento e perda vira uma linha (+ entra, - sai).
   Serve para o relatorio de movimentacao e para descobrir "por que o saldo
   esta assim?". Preenchida automaticamente pelas triggers.
   --------------------------------------------------------------------- */
CREATE TABLE movimentacao_estoque (
    id         BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id INT UNSIGNED NOT NULL,
    produto_id INT UNSIGNED NOT NULL,
    tipo       ENUM('Entrada','Venda','Cancelamento','Perda') NOT NULL,
    quantidade INT NOT NULL,                       -- + entra, - sai
    usuario_id INT UNSIGNED,
    data_hora  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    KEY ix_mov (unidade_id, produto_id, data_hora),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ENTRADA_ESTOQUE
   NO DIA A DIA: o registro de quando o fornecedor entrega mercadoria na loja:
   o que chegou, quanto, por qual preco de custo e ate quando vale (validade).
   Ao gravar uma entrada, o saldo do estoque sobe sozinho. Tambem guarda quem
   recebeu e quando. A validade nao pode ser anterior a data de entrada.
   --------------------------------------------------------------------- */
CREATE TABLE entrada_estoque (
    id             INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id     INT UNSIGNED NOT NULL,
    produto_id     INT UNSIGNED NOT NULL,
    quantidade     INT NOT NULL CHECK (quantidade > 0),
    custo_unitario DECIMAL(10,2) NOT NULL DEFAULT 0,
    fornecedor_id  INT UNSIGNED,
    data_entrada   DATE NOT NULL,
    data_validade  DATE,
    usuario_id     INT UNSIGNED NOT NULL,
    data_hora      DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (unidade_id)    REFERENCES unidade (id),
    FOREIGN KEY (produto_id)    REFERENCES produto (id),
    FOREIGN KEY (fornecedor_id) REFERENCES fornecedor (id),
    FOREIGN KEY (usuario_id)    REFERENCES usuario (id),
    CHECK (data_validade IS NULL OR data_validade >= data_entrada)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   PERDA
   NO DIA A DIA: onde se registra o que foi jogado fora: salgado vencido,
   caido no chao (danificado) ou que sobrou e estragou (desperdicio). Ao
   registrar, o estoque diminui sozinho. Com isso o dono passa a ver quanto
   dinheiro esta sendo perdido, o que hoje nao e controlado.
   --------------------------------------------------------------------- */
CREATE TABLE perda (
    id         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id INT UNSIGNED NOT NULL,
    produto_id INT UNSIGNED NOT NULL,
    quantidade INT NOT NULL CHECK (quantidade > 0),
    motivo     ENUM('Vencido','Danificado','Desperdicio') NOT NULL,
    data       DATE NOT NULL,
    usuario_id INT UNSIGNED NOT NULL,
    data_hora  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id),
    FOREIGN KEY (usuario_id) REFERENCES usuario (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   CAIXA
   NO DIA A DIA: substitui o caderno do caixa. De manha o responsavel ABRE o
   caixa informando o troco inicial; a noite FECHA contando o dinheiro da
   gaveta. O sistema calcula a DIFERENCA entre o que deveria ter e o que foi
   contado (sobra ou falta). Cada loja so pode ter UM caixa aberto por vez,
   e depois de fechado ele nao pode mais ser alterado.
   --------------------------------------------------------------------- */
CREATE TABLE caixa (
    id                     INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id             INT UNSIGNED NOT NULL,
    responsavel_id         INT UNSIGNED NOT NULL,
    data_abertura          DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    valor_inicial          DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (valor_inicial >= 0),
    data_fechamento        DATETIME,
    valor_contado_dinheiro DECIMAL(10,2),
    diferenca              DECIMAL(10,2),
    status                 ENUM('Aberto','Fechado') NOT NULL DEFAULT 'Aberto',
    -- coluna auxiliar: garante no maximo 1 caixa aberto por unidade
    unidade_aberta INT UNSIGNED GENERATED ALWAYS AS (IF(status = 'Aberto', unidade_id, NULL)) STORED,
    UNIQUE KEY uq_caixa_aberto (unidade_aberta),
    FOREIGN KEY (unidade_id)     REFERENCES unidade (id),
    FOREIGN KEY (responsavel_id) REFERENCES usuario (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   VENDA
   NO DIA A DIA: o "cupom" de cada atendimento no balcao: quem vendeu, quando,
   como o cliente pagou (dinheiro, cartao, Pix, vale-refeicao), o total e o
   troco. NAO precisa de cliente cadastrado, porque no balcao quase ninguem
   e identificado. Venda cancelada nunca e apagada: fica marcada como
   Cancelada com quem cancelou, o motivo e a hora, para evitar fraude.
   --------------------------------------------------------------------- */
CREATE TABLE venda (
    id                  INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id          INT UNSIGNED NOT NULL,
    caixa_id            INT UNSIGNED NOT NULL,
    atendente_id        INT UNSIGNED NOT NULL,
    data_hora           DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    forma_pagamento     ENUM('Dinheiro','Cartao Credito','Cartao Debito','Pix','Vale Refeicao') NOT NULL,
    valor_total         DECIMAL(10,2) NOT NULL DEFAULT 0,
    valor_recebido      DECIMAL(10,2),
    troco               DECIMAL(10,2) NOT NULL DEFAULT 0,
    status              ENUM('Concluida','Cancelada') NOT NULL DEFAULT 'Concluida',
    cancelado_por       INT UNSIGNED,
    motivo_cancelamento VARCHAR(200),
    data_cancelamento   DATETIME,
    KEY ix_venda (unidade_id, data_hora),
    KEY ix_venda_caixa (caixa_id),
    FOREIGN KEY (unidade_id)    REFERENCES unidade (id),
    FOREIGN KEY (caixa_id)      REFERENCES caixa (id),
    FOREIGN KEY (atendente_id)  REFERENCES usuario (id),
    FOREIGN KEY (cancelado_por) REFERENCES usuario (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ITEM_VENDA
   NO DIA A DIA: as linhas do cupom: "3 coxinhas a R$ 6,00, 2 refrigerantes a
   R$ 6,00". Guarda o preco da hora da venda, entao se o preco do produto
   mudar depois, as vendas antigas continuam corretas. O subtotal de cada
   linha e calculado sozinho.
   --------------------------------------------------------------------- */
CREATE TABLE item_venda (
    id             INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    venda_id       INT UNSIGNED NOT NULL,
    produto_id     INT UNSIGNED NOT NULL,
    quantidade     INT NOT NULL CHECK (quantidade > 0),
    preco_unitario DECIMAL(10,2) NOT NULL CHECK (preco_unitario > 0),
    subtotal       DECIMAL(10,2) GENERATED ALWAYS AS (quantidade * preco_unitario) STORED,
    KEY ix_item (venda_id),
    FOREIGN KEY (venda_id)   REFERENCES venda (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   LANCAMENTO
   NO DIA A DIA: despesas e receitas que NAO sao vendas: conta de luz, aluguel,
   salario, compra de material de limpeza, uma receita extra. Substitui o
   caderno de despesas e alimenta o DRE (resultado do mes). O valor sempre
   e maior que zero; o tipo (Despesa ou Receita) diz se soma ou subtrai.
   --------------------------------------------------------------------- */
CREATE TABLE lancamento (
    id         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id INT UNSIGNED NOT NULL,
    tipo       ENUM('Despesa','Receita') NOT NULL,
    categoria  VARCHAR(50) NOT NULL,
    descricao  VARCHAR(200),
    valor      DECIMAL(10,2) NOT NULL CHECK (valor > 0),
    data       DATE NOT NULL,
    usuario_id INT UNSIGNED NOT NULL,
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (usuario_id) REFERENCES usuario (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   LOG_OPERACAO
   NO DIA A DIA: a "caixa-preta" do sistema. Anota quem mexeu em que e quando
   (ex.: "gerente alterou o preco do produto 3 as 14:32"). Se algo estranho
   acontecer, e aqui que o dono descobre o responsavel. E preenchida so por
   triggers e NINGUEM consegue editar ou apagar suas linhas, nem pelo sistema.
   --------------------------------------------------------------------- */
CREATE TABLE log_operacao (
    id          BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    usuario_id  INT UNSIGNED,
    operacao    VARCHAR(10) NOT NULL,
    tabela      VARCHAR(40) NOT NULL,
    registro_id BIGINT UNSIGNED,
    detalhes    VARCHAR(255),
    data_hora   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_id) REFERENCES usuario (id)
) ENGINE=InnoDB;

/* =====================================================================
   TRIGGERS E PROCEDURES
   ===================================================================== */
DELIMITER $$

/* ---------------------------------------------------------------------
   trg_log_bu / trg_log_bd  (protecao do log)
   NO DIA A DIA: trava de seguranca. Se alguem tentar editar ou apagar uma
   linha do log para esconder o que fez, o banco recusa com uma mensagem.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_log_bu BEFORE UPDATE ON log_operacao FOR EACH ROW
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Log nao pode ser alterado.'$$
CREATE TRIGGER trg_log_bd BEFORE DELETE ON log_operacao FOR EACH ROW
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Log nao pode ser excluido.'$$

/* ---------------------------------------------------------------------
   trg_usuario_*, trg_produto_*, trg_lancamento_*, trg_venda_au  (log automatico)
   NO DIA A DIA: toda vez que alguem cria ou altera um usuario, um produto ou
   um lancamento, ou cancela uma venda, estas triggers anotam no log quem
   fez e o que mudou. O funcionario nao precisa fazer nada, e nao da para
   "esquecer" de registrar. Exemplos: criar usuario, mudar preco, inativar
   produto, apagar uma despesa lancada errado.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_usuario_ai AFTER INSERT ON usuario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'INSERT', 'usuario', NEW.id, CONCAT('login=', NEW.login, '; perfil=', NEW.perfil))$$
CREATE TRIGGER trg_usuario_au AFTER UPDATE ON usuario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'UPDATE', 'usuario', NEW.id, CONCAT('login=', NEW.login, '; status=', NEW.status))$$
CREATE TRIGGER trg_produto_ai AFTER INSERT ON produto FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'INSERT', 'produto', NEW.id, CONCAT('nome=', NEW.nome, '; preco=', NEW.preco_venda))$$
CREATE TRIGGER trg_produto_au AFTER UPDATE ON produto FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'UPDATE', 'produto', NEW.id, CONCAT('nome=', NEW.nome, '; preco=', NEW.preco_venda, '; status=', NEW.status))$$
CREATE TRIGGER trg_lancamento_ai AFTER INSERT ON lancamento FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'INSERT', 'lancamento', NEW.id, CONCAT(NEW.tipo, ' R$ ', NEW.valor))$$
CREATE TRIGGER trg_lancamento_au AFTER UPDATE ON lancamento FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'UPDATE', 'lancamento', NEW.id, CONCAT(NEW.tipo, ' R$ ', NEW.valor))$$
CREATE TRIGGER trg_lancamento_ad AFTER DELETE ON lancamento FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'DELETE', 'lancamento', OLD.id, CONCAT(OLD.tipo, ' R$ ', OLD.valor))$$
CREATE TRIGGER trg_venda_au AFTER UPDATE ON venda FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes)
    VALUES (@usuario_id, 'UPDATE', 'venda', NEW.id, CONCAT('status=', NEW.status, '; total=', NEW.valor_total))$$

/* ---------------------------------------------------------------------
   trg_entrada_ai  (chegada de mercadoria)
   NO DIA A DIA: o fornecedor entregou 100 coxinhas e o funcionario registrou
   a entrada. Esta trigger soma 100 no saldo da loja e anota no extrato do
   estoque. Ninguem precisa atualizar o estoque a mao.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_entrada_ai AFTER INSERT ON entrada_estoque FOR EACH ROW
BEGIN
    INSERT INTO estoque (unidade_id, produto_id, quantidade)
    VALUES (NEW.unidade_id, NEW.produto_id, NEW.quantidade)
    ON DUPLICATE KEY UPDATE quantidade = quantidade + NEW.quantidade;
    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    VALUES (NEW.unidade_id, NEW.produto_id, 'Entrada', NEW.quantidade, NEW.usuario_id);
END$$

/* ---------------------------------------------------------------------
   trg_perda_bi / trg_perda_ai  (registro de perdas)
   NO DIA A DIA: o funcionario joga fora 10 salgados vencidos e registra a
   perda. A primeira trigger confere se existem mesmo 10 no estoque (nao da
   para perder mais do que se tem); a segunda tira 10 do saldo e anota no
   extrato.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_perda_bi BEFORE INSERT ON perda FOR EACH ROW
BEGIN
    DECLARE v_saldo INT;
    SELECT quantidade INTO v_saldo FROM estoque
     WHERE unidade_id = NEW.unidade_id AND produto_id = NEW.produto_id FOR UPDATE;
    IF v_saldo IS NULL OR v_saldo < NEW.quantidade THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Perda maior que o saldo disponivel.';
    END IF;
END$$
CREATE TRIGGER trg_perda_ai AFTER INSERT ON perda FOR EACH ROW
BEGIN
    UPDATE estoque SET quantidade = quantidade - NEW.quantidade
     WHERE unidade_id = NEW.unidade_id AND produto_id = NEW.produto_id;
    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    VALUES (NEW.unidade_id, NEW.produto_id, 'Perda', -NEW.quantidade, NEW.usuario_id);
END$$

/* ---------------------------------------------------------------------
   trg_caixa_bu  (caixa fechado e "lacrado")
   NO DIA A DIA: depois que o caixa do dia foi fechado e conferido, ninguem
   consegue mudar os valores. Evita "acertar" o caixa depois para esconder
   uma diferenca.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_caixa_bu BEFORE UPDATE ON caixa FOR EACH ROW
BEGIN
    IF OLD.status = 'Fechado' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa fechado nao pode ser alterado.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_abrir_caixa  (procedure: abrir o caixa)
   NO DIA A DIA: e o "botao" que o responsavel aperta ao abrir a loja de
   manha, informando o dinheiro de troco que ja esta na gaveta. Se a loja ja
   tiver um caixa aberto (esqueceram de fechar o de ontem, por exemplo), o
   sistema avisa e nao abre outro.
   Uso: CALL sp_abrir_caixa(id_unidade, id_usuario, valor_inicial);
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_abrir_caixa (IN p_unidade INT UNSIGNED, IN p_usuario INT UNSIGNED, IN p_inicial DECIMAL(10,2))
BEGIN
    DECLARE EXIT HANDLER FOR 1062
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Ja existe um caixa aberto nesta unidade.';
    INSERT INTO caixa (unidade_id, responsavel_id, valor_inicial) VALUES (p_unidade, p_usuario, p_inicial);
    SELECT LAST_INSERT_ID() AS caixa_id;
END$$

/* ---------------------------------------------------------------------
   sp_fechar_caixa  (procedure: fechar o caixa)
   NO DIA A DIA: no fim do expediente o responsavel conta o dinheiro da gaveta
   e informa o valor. O sistema soma as vendas em dinheiro do dia, soma o
   troco inicial e calcula a DIFERENCA:
       diferenca = dinheiro contado - (troco inicial + vendas em dinheiro)
   Diferenca negativa = faltou dinheiro; positiva = sobrou.
   Uso: CALL sp_fechar_caixa(id_caixa, valor_contado);
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_fechar_caixa (IN p_caixa INT UNSIGNED, IN p_contado DECIMAL(10,2))
BEGIN
    DECLARE v_din DECIMAL(10,2);
    SELECT COALESCE(SUM(valor_total), 0) INTO v_din FROM venda
     WHERE caixa_id = p_caixa AND status = 'Concluida' AND forma_pagamento = 'Dinheiro';
    UPDATE caixa
       SET data_fechamento = NOW(), valor_contado_dinheiro = p_contado,
           diferenca = p_contado - (valor_inicial + v_din), status = 'Fechado'
     WHERE id = p_caixa AND status = 'Aberto';
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa inexistente ou ja fechado.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_venda_bi  (so vende com o caixa aberto)
   NO DIA A DIA: se o atendente tentar vender antes de abrir o caixa, ou com o
   caixa de outra loja, o sistema recusa. Garante que toda venda entra no
   caixa do dia e que o fechamento bate.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_venda_bi BEFORE INSERT ON venda FOR EACH ROW
BEGIN
    IF (SELECT COUNT(*) FROM caixa
         WHERE id = NEW.caixa_id AND unidade_id = NEW.unidade_id AND status = 'Aberto') = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nao ha caixa aberto nesta unidade.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_item_bi  (nao vende o que nao tem)
   NO DIA A DIA: o cliente pede 5 pasteis e so ha 3 no estoque: o sistema
   avisa "Estoque insuficiente" e a venda nao e concluida. Evita vender
   produto que nao existe e corrige o problema do estoque no papel.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_item_bi BEFORE INSERT ON item_venda FOR EACH ROW
BEGIN
    DECLARE v_saldo INT;
    SELECT e.quantidade INTO v_saldo
      FROM estoque e JOIN venda v ON v.unidade_id = e.unidade_id
     WHERE v.id = NEW.venda_id AND e.produto_id = NEW.produto_id FOR UPDATE;
    IF v_saldo IS NULL OR v_saldo < NEW.quantidade THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Estoque insuficiente para o produto.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_item_ai  (baixa automatica do estoque + total da venda)
   NO DIA A DIA: a cada item colocado na venda, o sistema tira a quantidade do
   estoque da loja, anota no extrato e soma o valor no total do cupom. O
   atendente so digita o que o cliente pediu, e o resto acontece sozinho.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_item_ai AFTER INSERT ON item_venda FOR EACH ROW
BEGIN
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_atend INT UNSIGNED;
    SELECT unidade_id, atendente_id INTO v_unidade, v_atend FROM venda WHERE id = NEW.venda_id;
    UPDATE estoque SET quantidade = quantidade - NEW.quantidade
     WHERE unidade_id = v_unidade AND produto_id = NEW.produto_id;
    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    VALUES (v_unidade, NEW.produto_id, 'Venda', -NEW.quantidade, v_atend);
    UPDATE venda SET valor_total = valor_total + NEW.quantidade * NEW.preco_unitario WHERE id = NEW.venda_id;
END$$

/* ---------------------------------------------------------------------
   sp_finalizar_venda  (procedure: fechar a venda e calcular o troco)
   NO DIA A DIA: e o momento do "pagar". Depois que todos os itens foram
   lancados, o sistema confere se a venda tem itens e, se for dinheiro, se o
   valor entregue cobre o total. Calcula o TROCO na hora (substitui a
   calculadora). Ex.: total R$ 30,00, cliente deu R$ 50,00 -> troco R$ 20,00.
   Uso: CALL sp_finalizar_venda(id_venda);
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_finalizar_venda (IN p_venda INT UNSIGNED)
BEGIN
    DECLARE v_total DECIMAL(10,2);
    DECLARE v_forma VARCHAR(20);
    DECLARE v_receb DECIMAL(10,2);
    SELECT valor_total, forma_pagamento, valor_recebido INTO v_total, v_forma, v_receb
      FROM venda WHERE id = p_venda;
    IF v_total IS NULL OR v_total <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda sem itens.';
    END IF;
    IF v_forma = 'Dinheiro' THEN
        IF v_receb IS NULL OR v_receb < v_total THEN
            SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Valor recebido menor que o total.';
        END IF;
        UPDATE venda SET troco = v_receb - v_total WHERE id = p_venda;
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_cancelar_venda  (procedure: cancelar uma venda)
   NO DIA A DIA: o atendente digitou errado ou o cliente desistiu depois de
   pagar. Somente Gerente ou Administrador consegue cancelar, e e obrigatorio
   informar o motivo. O sistema devolve os produtos ao estoque e mantem a
   venda registrada como "Cancelada" (nao some), com quem cancelou e quando.
   Se algo falhar no meio, tudo e desfeito e nada fica pela metade.
   Uso: CALL sp_cancelar_venda(id_venda, id_usuario, 'motivo');
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_cancelar_venda (IN p_venda INT UNSIGNED, IN p_usuario INT UNSIGNED, IN p_motivo VARCHAR(200))
BEGIN
    DECLARE v_perfil VARCHAR(20);
    DECLARE v_status VARCHAR(20);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;

    SELECT perfil INTO v_perfil FROM usuario WHERE id = p_usuario AND status = 'Ativo';
    IF v_perfil IS NULL OR v_perfil = 'Atendente' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Apenas Administrador ou Gerente pode cancelar vendas.';
    END IF;
    IF p_motivo IS NULL OR TRIM(p_motivo) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Informe o motivo do cancelamento.';
    END IF;

    START TRANSACTION;
    SELECT status, unidade_id INTO v_status, v_unidade FROM venda WHERE id = p_venda FOR UPDATE;
    IF v_status IS NULL OR v_status <> 'Concluida' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda inexistente ou ja cancelada.';
    END IF;

    UPDATE estoque e
      JOIN (SELECT produto_id, SUM(quantidade) AS q FROM item_venda WHERE venda_id = p_venda GROUP BY produto_id) i
        ON i.produto_id = e.produto_id
       SET e.quantidade = e.quantidade + i.q
     WHERE e.unidade_id = v_unidade;

    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    SELECT v_unidade, produto_id, 'Cancelamento', SUM(quantidade), p_usuario
      FROM item_venda WHERE venda_id = p_venda GROUP BY produto_id;

    SET @usuario_id = p_usuario;
    UPDATE venda SET status = 'Cancelada', cancelado_por = p_usuario,
           motivo_cancelamento = p_motivo, data_cancelamento = NOW()
     WHERE id = p_venda;
    COMMIT;
END$$

DELIMITER ;

/* =====================================================================
   VIEWS (consultas prontas para os relatorios)
   No C#, basta fazer SELECT * FROM vw_... WHERE unidade_id = ... e filtrar.
   ===================================================================== */

/* ---------------------------------------------------------------------
   vw_estoque_atual
   NO DIA A DIA: a tela "como esta o estoque agora". Mostra o saldo de cada
   produto por loja e a coluna CRITICO = 1 quando o saldo esta abaixo do
   minimo, o alerta de que e hora de pedir mais ao fornecedor.
   --------------------------------------------------------------------- */
CREATE VIEW vw_estoque_atual AS
SELECT e.unidade_id, p.id AS produto_id, p.nome AS produto, e.quantidade AS saldo,
       p.estoque_minimo, (e.quantidade < p.estoque_minimo) AS critico
  FROM estoque e JOIN produto p ON p.id = e.produto_id;

/* ---------------------------------------------------------------------
   vw_validade
   NO DIA A DIA: o relatorio "o que esta para vencer". Classifica cada entrada
   como Vencido, Proximo do vencimento (ate 3 dias) ou Dentro do prazo.
   Ajuda a vender primeiro o que vence antes e a evitar perdas.
   --------------------------------------------------------------------- */
CREATE VIEW vw_validade AS
SELECT en.unidade_id, p.nome AS produto, en.data_validade,
       CASE WHEN en.data_validade < CURDATE() THEN 'Vencido'
            WHEN en.data_validade <= DATE_ADD(CURDATE(), INTERVAL 3 DAY) THEN 'Proximo do vencimento'
            ELSE 'Dentro do prazo' END AS situacao
  FROM entrada_estoque en JOIN produto p ON p.id = en.produto_id
 WHERE en.data_validade IS NOT NULL;

/* ---------------------------------------------------------------------
   vw_vendas_diarias
   NO DIA A DIA: o relatorio de faturamento. Mostra, por loja e por dia, quantas
   vendas foram feitas e quanto entrou, separado por forma de pagamento.
   Ignora vendas canceladas. Responde "quanto vendemos ontem no Centro?".
   --------------------------------------------------------------------- */
CREATE VIEW vw_vendas_diarias AS
SELECT unidade_id, DATE(data_hora) AS dia, forma_pagamento,
       COUNT(*) AS qtd_vendas, SUM(valor_total) AS faturamento
  FROM venda WHERE status = 'Concluida'
 GROUP BY unidade_id, DATE(data_hora), forma_pagamento;

/* ---------------------------------------------------------------------
   vw_produtos_mais_vendidos
   NO DIA A DIA: o ranking do que mais sai. Mostra, por loja e dia, a
   quantidade e o valor vendido de cada produto. Ajuda a decidir o que comprar
   mais do fornecedor e o que esta encalhado.
   --------------------------------------------------------------------- */
CREATE VIEW vw_produtos_mais_vendidos AS
SELECT v.unidade_id, DATE(v.data_hora) AS dia, p.nome AS produto,
       SUM(i.quantidade) AS qtd_vendida, SUM(i.subtotal) AS total_vendido
  FROM item_venda i
  JOIN venda v   ON v.id = i.venda_id AND v.status = 'Concluida'
  JOIN produto p ON p.id = i.produto_id
 GROUP BY v.unidade_id, DATE(v.data_hora), p.nome;

/* ---------------------------------------------------------------------
   vw_caixa_totais
   NO DIA A DIA: o resumo do fechamento do caixa: quanto entrou em dinheiro,
   cartao de credito, cartao de debito, Pix e vale-refeicao naquele caixa.
   Serve para conferir com a maquininha e com o extrato do Pix.
   Uso: SELECT * FROM vw_caixa_totais WHERE caixa_id = 1;
   --------------------------------------------------------------------- */
CREATE VIEW vw_caixa_totais AS
SELECT caixa_id, forma_pagamento, SUM(valor_total) AS total
  FROM venda WHERE status = 'Concluida' GROUP BY caixa_id, forma_pagamento;

/* ---------------------------------------------------------------------
   vw_dre_mensal
   NO DIA A DIA: o "resultado do mes" para o dono. Soma tudo que entrou (vendas
   + receitas avulsas), tudo que saiu (despesas) e mostra se sobrou ou
   faltou dinheiro: resultado = receitas - despesas. Por loja e por mes.
   --------------------------------------------------------------------- */
CREATE VIEW vw_dre_mensal AS
SELECT unidade_id, mes, SUM(receita) AS receitas, SUM(despesa) AS despesas,
       SUM(receita) - SUM(despesa) AS resultado
  FROM (SELECT unidade_id, DATE_FORMAT(data_hora, '%Y-%m') AS mes, valor_total AS receita, 0 AS despesa
          FROM venda WHERE status = 'Concluida'
        UNION ALL
        SELECT unidade_id, DATE_FORMAT(data, '%Y-%m'),
               IF(tipo = 'Receita', valor, 0), IF(tipo = 'Despesa', valor, 0)
          FROM lancamento) t
 GROUP BY unidade_id, mes;

/* =====================================================================
   SEGURANCA - usuario do banco usado pelo sistema
   NO DIA A DIA: o programa em C# NAO deve entrar no banco como "root" (dono
   de tudo). Este usuario so pode ler dados, cadastrar, vender e registrar
   entradas e perdas. Nao pode apagar vendas nem mexer direto no estoque ou
   no log: isso so acontece pelas triggers e procedures acima. Assim, mesmo
   que a senha do sistema vaze, o estrago possivel e pequeno.
   TROQUE A SENHA ANTES DE USAR DE VERDADE.
   ===================================================================== */
CREATE USER IF NOT EXISTS 'app_salgados'@'%' IDENTIFIED BY 'TROQUE_ESTA_SENHA_FORTE';
GRANT SELECT  ON salgados_cia.* TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.unidade    TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.usuario    TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.produto    TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.fornecedor TO 'app_salgados'@'%';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.lancamento TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.venda           TO 'app_salgados'@'%';
GRANT UPDATE (forma_pagamento, valor_recebido) ON salgados_cia.venda TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.item_venda      TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.entrada_estoque TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.perda           TO 'app_salgados'@'%';
GRANT EXECUTE ON salgados_cia.*              TO 'app_salgados'@'%';
FLUSH PRIVILEGES;

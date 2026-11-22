/* =====================================================================
   ERP SALGADOS & CIA - Banco de dados (versao enxuta, MySQL 8.0+)
   Ordem: 01_schema.sql -> 02_dados_teste.sql

   Esta versao aplica a "Especificacao tecnica - atualizacao do banco de
   dados" sobre o schema anterior. As marcas [ESPEC. n] indicam de qual
   secao da especificacao vem cada mudanca. O que nao foi citado na
   especificacao foi mantido como estava.

   COMO LER ESTE ARQUIVO
   Acima de cada tabela, trigger, procedure e view ha um comentario
   "NO DIA A DIA" explicando, em linguagem simples, para que ela serve
   na rotina da salgaderia.

   Vocabulario rapido:
   - TABELA     = onde os dados ficam guardados (como uma planilha).
   - TRIGGER    = regra automatica: o proprio banco executa quando algo
                  e gravado (ex.: pediu -> baixa o estoque sozinho).
   - PROCEDURE  = "botao" que o sistema C# chama para fazer uma tarefa
                  completa (ex.: cobrar a comanda).
   - VIEW       = consulta pronta, usada nos relatorios.

   O C# deve executar, ao abrir a conexao:
       SET @usuario_id = <id do usuario logado>;
       SET @unidade_id = <id da unidade de trabalho escolhida na hora>;
   para o log saber QUEM fez cada operacao e EM QUAL LOJA. O @unidade_id vem
   SEMPRE da aplicacao (Administrador/Gerente nao tem unidade fixa).

   FLUXO DE VENDA (comanda -> cobranca)
     1) INSERT em comanda + INSERT em item_comanda  -> o estoque baixa AQUI
     2) CALL sp_cobrar_comanda(...)  -> cria a venda e copia os itens
        (sem baixar o estoque de novo)
     3) INSERT em pagamento_venda (um por forma de pagamento)
     4) CALL sp_finalizar_venda(...) -> confere os pagamentos e calcula o troco
     Os passos 2, 3 e 4 devem rodar na MESMA transacao do C#: se algo falhar,
     ROLLBACK e a comanda continua aberta.
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
   Loja fechada e marcada como Inativo, nunca apagada.
   [ESPEC. 4] ganhou CNPJ e numero; status agora e Ativo/Inativo.
   --------------------------------------------------------------------- */
CREATE TABLE unidade (
    id       INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome     VARCHAR(80)  NOT NULL UNIQUE,
    cnpj     VARCHAR(18)  UNIQUE,
    endereco VARCHAR(150) NOT NULL,
    numero   VARCHAR(10),
    telefone VARCHAR(20),
    status   ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   USUARIO
   NO DIA A DIA: os LOGINS do sistema (o do patrao e o de cada lanchonete).
   Nao e uma pessoa: o login nao tem nome proprio; quem trabalha de fato
   no balcao esta na tabela FUNCIONARIO.
   O PERFIL define o que o login pode fazer: o Atendente so vende; o Gerente
   e o Administrador tambem cancelam vendas e veem relatorios gerenciais.
   Gerente/Administrador nao tem loja fixa (unidade_id fica vazio).
   A senha e guardada como "hash" (embaralhada), entao nem o dono do banco
   consegue ler a senha de ninguem. Login que sai e Inativado, para o
   historico nao se perder. Nao existem dois logins iguais.
   [ESPEC. 2 e 4] coluna nome removida; unidade_id pode ser NULL.
   --------------------------------------------------------------------- */
CREATE TABLE usuario (
    id         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    login      VARCHAR(40)  NOT NULL UNIQUE,
    senha_hash VARCHAR(255) NOT NULL,              -- gerado no C# com PBKDF2 (SHA-256, 10.000 iteracoes, sal), formato "sal:hash"
    perfil     ENUM('Administrador','Gerente','Atendente') NOT NULL DEFAULT 'Atendente',
    unidade_id INT UNSIGNED NULL,                  -- Gerente/Administrador nao tem unidade fixa
    status     ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo',
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   FUNCIONARIO   [ESPEC. 3 - tabela nova]
   NO DIA A DIA: as pessoas que trabalham em cada loja (Joao, Maria...). Elas
   NAO tem senha nem login: sao escolhidas numa lista na tela, para dizer
   "quem abriu o caixa", "quem recebeu a mercadoria", "quem registrou a
   perda". Nao pode haver dois funcionarios com o mesmo nome na mesma loja.
   --------------------------------------------------------------------- */
CREATE TABLE funcionario (
    id         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome       VARCHAR(100) NOT NULL,
    unidade_id INT UNSIGNED NOT NULL,
    status     ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo',
    UNIQUE KEY uq_funcionario (nome, unidade_id),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   CLIENTE   [ESPEC. 3 - tabela nova]
   NO DIA A DIA: o cadastro de quem compra "na notinha" (fiado). No balcao a
   maioria das vendas NAO tem cliente; ele so e obrigatorio quando parte da
   conta vai ficar para pagar depois (Notinha).
   --------------------------------------------------------------------- */
CREATE TABLE cliente (
    id       INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome     VARCHAR(100) NOT NULL,
    cpf_cnpj VARCHAR(18) UNIQUE,
    telefone VARCHAR(20),
    status   ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   PRODUTO
   NO DIA A DIA: o "cardapio" do balcao: coxinha, pastel, refrigerante, etc.,
   com preco de venda. Tambem guarda o ESTOQUE MINIMO (quando o saldo ficar
   abaixo dele, o produto aparece como "estoque critico" no relatorio).
   Produto que ja foi vendido nao pode ser apagado, so Inativado. O preco
   nunca pode ser zero ou negativo.
   [ESPEC. 2] coluna controla_validade removida.
   --------------------------------------------------------------------- */
CREATE TABLE produto (
    id                INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome              VARCHAR(100) NOT NULL UNIQUE,
    categoria         VARCHAR(50)  NOT NULL,
    preco_venda       DECIMAL(10,2) NOT NULL CHECK (preco_venda > 0),
    unidade_medida    VARCHAR(10)  NOT NULL DEFAULT 'un',
    estoque_minimo    INT UNSIGNED NOT NULL DEFAULT 0,
    status            ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   FORNECEDOR
   NO DIA A DIA: quem vende os salgados prontos para a rede (a salgaderia nao
   fabrica nada). Quando chega mercadoria, a entrada de estoque registra de
   qual fornecedor veio. Evita cadastrar o mesmo CNPJ duas vezes.
   [ESPEC. 2 e 4] cnpj_cpf virou cnpj; coluna email removida.
   --------------------------------------------------------------------- */
CREATE TABLE fornecedor (
    id       INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome     VARCHAR(120) NOT NULL,
    cnpj     VARCHAR(18)  NOT NULL UNIQUE,
    telefone VARCHAR(20),
    status   ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ESTOQUE
   NO DIA A DIA: responde "quantas coxinhas tem na loja Centro agora?".
   Uma linha por produto em cada unidade, sempre com o saldo ATUAL. Ninguem
   mexe aqui na mao: o saldo sobe quando chega mercadoria e desce quando
   o pedido e lancado na comanda ou quando ha perda (as triggers cuidam
   disso). O saldo nunca fica negativo.
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
   Cada entrada, venda, estorno, perda e ajuste vira uma linha (+ entra,
   - sai). Serve para o relatorio de movimentacao e para descobrir "por que
   o saldo esta assim?". Preenchida automaticamente pelas triggers.
   [ESPEC. 4] "Cancelamento" passou a se chamar "Estorno"; novo tipo "Ajuste".
   --------------------------------------------------------------------- */
CREATE TABLE movimentacao_estoque (
    id         BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id INT UNSIGNED NOT NULL,
    produto_id INT UNSIGNED NOT NULL,
    tipo       ENUM('Entrada','Venda','Estorno','Perda','Ajuste') NOT NULL,
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
   o que chegou, quanto e por qual preco de custo. Ao gravar uma entrada, o
   saldo do estoque sobe sozinho. Guarda o login que registrou e o
   FUNCIONARIO que recebeu a mercadoria (da mesma loja).
   [ESPEC. 2 e 4] sem data_validade; novo responsavel_funcionario_id.
   --------------------------------------------------------------------- */
CREATE TABLE entrada_estoque (
    id                         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id                 INT UNSIGNED NOT NULL,
    produto_id                 INT UNSIGNED NOT NULL,
    quantidade                 INT NOT NULL CHECK (quantidade > 0),
    custo_unitario             DECIMAL(10,2) NOT NULL DEFAULT 0,
    fornecedor_id              INT UNSIGNED,
    data_entrada               DATE NOT NULL,
    usuario_id                 INT UNSIGNED NOT NULL,
    data_hora                  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    responsavel_funcionario_id INT UNSIGNED NOT NULL,
    FOREIGN KEY (unidade_id)    REFERENCES unidade (id),
    FOREIGN KEY (produto_id)    REFERENCES produto (id),
    FOREIGN KEY (fornecedor_id) REFERENCES fornecedor (id),
    FOREIGN KEY (usuario_id)    REFERENCES usuario (id),
    FOREIGN KEY (responsavel_funcionario_id) REFERENCES funcionario (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   PERDA
   NO DIA A DIA: onde se registra o que foi jogado fora: salgado vencido,
   caido no chao (danificado) ou que sobrou e estragou (desperdicio). Ao
   registrar, o estoque diminui sozinho. Com isso o dono passa a ver quanto
   dinheiro esta sendo perdido. Guarda o FUNCIONARIO responsavel.
   [ESPEC. 4] novo responsavel_funcionario_id.
   --------------------------------------------------------------------- */
CREATE TABLE perda (
    id                         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id                 INT UNSIGNED NOT NULL,
    produto_id                 INT UNSIGNED NOT NULL,
    quantidade                 INT NOT NULL CHECK (quantidade > 0),
    motivo                     ENUM('Vencido','Danificado','Desperdicio') NOT NULL,
    data                       DATE NOT NULL,
    usuario_id                 INT UNSIGNED NOT NULL,
    data_hora                  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    responsavel_funcionario_id INT UNSIGNED NOT NULL,
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id),
    FOREIGN KEY (usuario_id) REFERENCES usuario (id),
    FOREIGN KEY (responsavel_funcionario_id) REFERENCES funcionario (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   CAIXA
   NO DIA A DIA: substitui o caderno do caixa. De manha o FUNCIONARIO
   responsavel ABRE o caixa informando o troco inicial; a noite FECHA contando
   o dinheiro da gaveta e o total da maquininha. O sistema calcula a
   DIFERENCA do dinheiro (sobra ou falta). Cada loja so pode ter UM caixa
   aberto por vez, so UM caixa por dia, e depois de fechado ele nao pode
   mais ser alterado.
   [ESPEC. 4] novo valor_contado_maquininha; o responsavel agora e um
   FUNCIONARIO (e nao mais um usuario).
   --------------------------------------------------------------------- */
CREATE TABLE caixa (
    id                         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id                 INT UNSIGNED NOT NULL,
    responsavel_funcionario_id INT UNSIGNED NOT NULL,
    data_abertura              DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    valor_inicial              DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (valor_inicial >= 0),
    data_fechamento            DATETIME,
    valor_contado_dinheiro     DECIMAL(10,2),
    valor_contado_maquininha   DECIMAL(10,2),
    diferenca                  DECIMAL(10,2),
    status                     ENUM('Aberto','Fechado') NOT NULL DEFAULT 'Aberto',
    -- coluna auxiliar: garante no maximo 1 caixa aberto por unidade
    unidade_aberta INT UNSIGNED GENERATED ALWAYS AS (IF(status = 'Aberto', unidade_id, NULL)) STORED,
    UNIQUE KEY uq_caixa_aberto (unidade_aberta),
    FOREIGN KEY (unidade_id)                 REFERENCES unidade (id),
    FOREIGN KEY (responsavel_funcionario_id) REFERENCES funcionario (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   COMANDA   [ESPEC. 3 - tabela nova]
   NO DIA A DIA: a "conta aberta" de uma mesa (ou do balcao, mesa 0). Enquanto
   o cliente consome, os pedidos vao sendo lancados na comanda. Quando ele
   vai pagar, a comanda e COBRADA e vira uma venda. Comanda cancelada nao e
   apagada: fica com status Cancelada, para auditoria.
   --------------------------------------------------------------------- */
CREATE TABLE comanda (
    id            INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id    INT UNSIGNED NOT NULL,
    numero_mesa   INT NOT NULL DEFAULT 0,          -- 0 = Balcao
    cliente_id    INT UNSIGNED,
    status        ENUM('Aberta','Cancelada','Fechada') NOT NULL DEFAULT 'Aberta',
    data_abertura DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (cliente_id) REFERENCES cliente (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ITEM_COMANDA   [ESPEC. 3 - tabela nova]
   NO DIA A DIA: cada pedido lancado na comanda ("3 coxinhas"). E AQUI que o
   estoque baixa: no momento em que o pedido e enviado, nao quando a conta e
   paga. Depois que a comanda e cobrada, estas linhas continuam guardadas
   (o item_venda recebe uma copia).
   --------------------------------------------------------------------- */
CREATE TABLE item_comanda (
    id             INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    comanda_id     INT UNSIGNED NOT NULL,
    produto_id     INT UNSIGNED NOT NULL,
    quantidade     INT NOT NULL CHECK (quantidade > 0),
    preco_unitario DECIMAL(10,2) NOT NULL CHECK (preco_unitario > 0),
    FOREIGN KEY (comanda_id) REFERENCES comanda (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   VENDA
   NO DIA A DIA: o "cupom" de cada atendimento, criado quando a comanda e
   cobrada: quando, de qual mesa, o total e o troco. A venda NAO grava o
   atendente: quem fica registrado e o responsavel pelo caixa. A forma de
   pagamento fica na tabela PAGAMENTO_VENDA (uma venda pode ser paga com
   varias formas). Cliente so e preenchido quando ha Notinha. Venda
   cancelada nunca e apagada: fica marcada como Cancelada com quem
   cancelou, o motivo e a hora, para evitar fraude.
   [ESPEC. 2 e 4] sem atendente_id e forma_pagamento; novos numero_mesa,
   cliente_id e comanda_id.
   --------------------------------------------------------------------- */
CREATE TABLE venda (
    id                  INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id          INT UNSIGNED NOT NULL,
    numero_mesa         INT NOT NULL DEFAULT 0,
    caixa_id            INT UNSIGNED NOT NULL,
    data_hora           DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    valor_total         DECIMAL(10,2) NOT NULL DEFAULT 0,
    valor_recebido      DECIMAL(10,2),
    troco               DECIMAL(10,2) NOT NULL DEFAULT 0,
    status              ENUM('Concluida','Cancelada') NOT NULL DEFAULT 'Concluida',
    cancelado_por       INT UNSIGNED,
    motivo_cancelamento VARCHAR(200),
    data_cancelamento   DATETIME,
    cliente_id          INT UNSIGNED NULL,
    comanda_id          INT UNSIGNED NULL,
    KEY ix_venda (unidade_id, data_hora),
    KEY ix_venda_caixa (caixa_id),
    FOREIGN KEY (unidade_id)    REFERENCES unidade (id),
    FOREIGN KEY (caixa_id)      REFERENCES caixa (id),
    FOREIGN KEY (cancelado_por) REFERENCES usuario (id),
    FOREIGN KEY (cliente_id)    REFERENCES cliente (id),
    FOREIGN KEY (comanda_id)    REFERENCES comanda (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ITEM_VENDA
   NO DIA A DIA: as linhas do cupom: "3 coxinhas a R$ 6,00, 2 refrigerantes a
   R$ 6,00". E uma COPIA dos itens da comanda feita na hora da cobranca.
   Guarda o preco da hora da venda, entao se o preco do produto mudar
   depois, as vendas antigas continuam corretas. O subtotal de cada linha
   e calculado sozinho.
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
   PAGAMENTO_VENDA   [ESPEC. 3 - tabela nova]
   NO DIA A DIA: como o cliente pagou. Uma venda pode ter varias linhas
   (ex.: R$ 20 em dinheiro + R$ 10 no Pix). Formas: Dinheiro, Pix, Credito,
   Debito e Notinha (fiado). Em Dinheiro, o valor guardado e o que o cliente
   ENTREGOU (o troco e calculado por sp_finalizar_venda).
   --------------------------------------------------------------------- */
CREATE TABLE pagamento_venda (
    id              INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    venda_id        INT UNSIGNED NOT NULL,
    forma_pagamento ENUM('Dinheiro','Pix','Credito','Debito','Notinha') NOT NULL,
    valor           DECIMAL(10,2) NOT NULL CHECK (valor > 0),
    FOREIGN KEY (venda_id) REFERENCES venda (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   NOTINHA   [ESPEC. 3 - tabela nova]
   NO DIA A DIA: a "dívida" do cliente: a parte da venda que ficou para pagar
   depois. Uma linha por venda paga (total ou parcialmente) na notinha.
   --------------------------------------------------------------------- */
CREATE TABLE notinha (
    id         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    cliente_id INT UNSIGNED NOT NULL,
    venda_id   INT UNSIGNED NOT NULL,
    unidade_id INT UNSIGNED NOT NULL,
    data_hora  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    valor      DECIMAL(10,2) NOT NULL CHECK (valor > 0),
    FOREIGN KEY (cliente_id) REFERENCES cliente (id),
    FOREIGN KEY (venda_id)   REFERENCES venda (id),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ABATIMENTO_NOTINHA   [ESPEC. 3 - tabela nova]
   NO DIA A DIA: quando o cliente volta para pagar (todo ou parte) o que
   deve na notinha. O dinheiro entra no caixa do dia. O saldo do cliente =
   soma das notinhas - soma dos abatimentos (veja vw_notinha_saldo).
   --------------------------------------------------------------------- */
CREATE TABLE abatimento_notinha (
    id              INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    cliente_id      INT UNSIGNED NOT NULL,
    caixa_id        INT UNSIGNED NOT NULL,
    unidade_id      INT UNSIGNED NOT NULL,
    data_hora       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    valor           DECIMAL(10,2) NOT NULL CHECK (valor > 0),
    forma_pagamento ENUM('Dinheiro','Pix','Credito','Debito') NOT NULL,
    FOREIGN KEY (cliente_id) REFERENCES cliente (id),
    FOREIGN KEY (caixa_id)   REFERENCES caixa (id),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   INVENTARIO e ITEM_INVENTARIO   [ESPEC. 3 - tabelas novas]
   NO DIA A DIA: a "contagem do estoque". O funcionario conta o que tem na
   prateleira; o sistema guarda o saldo que ELE achava que tinha
   (saldo_sistema) ao lado do que foi realmente contado, e o motivo da
   diferenca. Cada contagem e um INVENTARIO com varios ITENS.
   --------------------------------------------------------------------- */
CREATE TABLE inventario (
    id                         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id                 INT UNSIGNED NOT NULL,
    responsavel_funcionario_id INT UNSIGNED NOT NULL,
    usuario_id                 INT UNSIGNED NOT NULL,
    data_hora                  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    observacao                 VARCHAR(255),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (responsavel_funcionario_id) REFERENCES funcionario (id),
    FOREIGN KEY (usuario_id) REFERENCES usuario (id)
) ENGINE=InnoDB;

CREATE TABLE item_inventario (
    id                 INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    inventario_id      INT UNSIGNED NOT NULL,
    produto_id         INT UNSIGNED NOT NULL,
    saldo_sistema      INT NOT NULL,
    quantidade_contada INT,
    motivo             VARCHAR(200),
    FOREIGN KEY (inventario_id) REFERENCES inventario (id),
    FOREIGN KEY (produto_id)    REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   LOG_OPERACAO
   NO DIA A DIA: a "caixa-preta" do sistema. Anota quem mexeu em que, em qual
   loja e quando (ex.: "login do patrao alterou o preco do produto 3 as
   14:32, na loja Centro"). Se algo estranho acontecer, e aqui que o dono
   descobre o responsavel. E preenchida so por triggers e NINGUEM consegue
   editar ou apagar suas linhas, nem pelo sistema.
   [ESPEC. 4] nova coluna unidade_id, preenchida com @unidade_id (vem da
   aplicacao; nunca deduzida de usuario.unidade_id).
   --------------------------------------------------------------------- */
CREATE TABLE log_operacao (
    id          BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    usuario_id  INT UNSIGNED,
    operacao    VARCHAR(10) NOT NULL,
    tabela      VARCHAR(40) NOT NULL,
    registro_id BIGINT UNSIGNED,
    detalhes    VARCHAR(255),
    data_hora   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    unidade_id  INT UNSIGNED,
    FOREIGN KEY (usuario_id) REFERENCES usuario (id),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* =====================================================================
   TRIGGERS E PROCEDURES
   ===================================================================== */
DELIMITER $$

/* ---------------------------------------------------------------------
   fn_funcionario_ok  (funcao auxiliar)
   NO DIA A DIA: pergunta "este funcionario existe, esta ativo e trabalha
   mesmo nesta loja?". Usada pelas triggers do caixa, da entrada, da perda
   e do inventario para impedir, por exemplo, que a Maria da loja Bairro
   apareca como quem recebeu mercadoria na loja Centro.
   [ESPEC. 7]
   --------------------------------------------------------------------- */
CREATE FUNCTION fn_funcionario_ok (p_func INT UNSIGNED, p_unidade INT UNSIGNED)
RETURNS TINYINT DETERMINISTIC READS SQL DATA
BEGIN
    RETURN (SELECT COUNT(*) FROM funcionario
             WHERE id = p_func AND unidade_id = p_unidade AND status = 'Ativo') > 0;
END$$

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
   trg_usuario_*, trg_produto_*, trg_venda_au, trg_comanda_au  (log automatico)
   NO DIA A DIA: toda vez que alguem cria ou altera um login ou um produto,
   cancela uma venda ou muda o estado de uma comanda (cobrada/cancelada),
   estas triggers anotam no log quem fez e o que mudou. O funcionario nao
   precisa fazer nada, e nao da para "esquecer" de registrar.
   (A trigger da venda so anota quando o STATUS muda, para nao encher o log
   com os recalculos de total e troco.)
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_usuario_ai AFTER INSERT ON usuario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'usuario', NEW.id, CONCAT('login=', NEW.login, '; perfil=', NEW.perfil), @unidade_id)$$
CREATE TRIGGER trg_usuario_au AFTER UPDATE ON usuario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'usuario', NEW.id, CONCAT('login=', NEW.login, '; status=', NEW.status), @unidade_id)$$
CREATE TRIGGER trg_produto_ai AFTER INSERT ON produto FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'produto', NEW.id, CONCAT('nome=', NEW.nome, '; preco=', NEW.preco_venda), @unidade_id)$$
CREATE TRIGGER trg_produto_au AFTER UPDATE ON produto FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'produto', NEW.id, CONCAT('nome=', NEW.nome, '; preco=', NEW.preco_venda, '; status=', NEW.status), @unidade_id)$$
CREATE TRIGGER trg_venda_au AFTER UPDATE ON venda FOR EACH ROW
BEGIN
    IF NEW.status <> OLD.status THEN
        INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes, unidade_id)
        VALUES (@usuario_id, 'UPDATE', 'venda', NEW.id, CONCAT('status=', NEW.status, '; total=', NEW.valor_total), @unidade_id);
    END IF;
END$$
CREATE TRIGGER trg_comanda_au AFTER UPDATE ON comanda FOR EACH ROW
BEGIN
    IF NEW.status <> OLD.status THEN
        INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes, unidade_id)
        VALUES (@usuario_id, 'UPDATE', 'comanda', NEW.id, CONCAT('status=', NEW.status, '; mesa=', NEW.numero_mesa), @unidade_id);
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_entrada_bi / trg_entrada_ai  (chegada de mercadoria)
   NO DIA A DIA: o fornecedor entregou 100 coxinhas e o funcionario registrou
   a entrada. A primeira trigger confere se quem recebeu e funcionario DA
   MESMA LOJA; a segunda soma 100 no saldo da loja e anota no extrato do
   estoque. Ninguem precisa atualizar o estoque a mao.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_entrada_bi BEFORE INSERT ON entrada_estoque FOR EACH ROW
BEGIN
    IF fn_funcionario_ok(NEW.responsavel_funcionario_id, NEW.unidade_id) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Funcionario invalido: inexistente, inativo ou de outra unidade.';
    END IF;
END$$
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
   perda. A primeira trigger confere se o responsavel e da mesma loja e se
   existem mesmo 10 no estoque (nao da para perder mais do que se tem); a
   segunda tira 10 do saldo e anota no extrato.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_perda_bi BEFORE INSERT ON perda FOR EACH ROW
BEGIN
    DECLARE v_saldo INT;
    IF fn_funcionario_ok(NEW.responsavel_funcionario_id, NEW.unidade_id) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Funcionario invalido: inexistente, inativo ou de outra unidade.';
    END IF;
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
   trg_inventario_bi  (inventario so com funcionario da mesma loja)
   NO DIA A DIA: mesma protecao da entrada e da perda, aplicada a contagem
   de estoque. (Acrescentada por coerencia; a especificacao so cita
   caixa, entrada e perda.)
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_inventario_bi BEFORE INSERT ON inventario FOR EACH ROW
BEGIN
    IF fn_funcionario_ok(NEW.responsavel_funcionario_id, NEW.unidade_id) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Funcionario invalido: inexistente, inativo ou de outra unidade.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_caixa_bi / trg_caixa_bu  (responsavel da loja; caixa fechado e "lacrado")
   NO DIA A DIA: ao abrir o caixa, o responsavel precisa ser funcionario da
   propria loja. Depois que o caixa do dia foi fechado e conferido, ninguem
   consegue mudar os valores. Evita "acertar" o caixa depois para esconder
   uma diferenca.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_caixa_bi BEFORE INSERT ON caixa FOR EACH ROW
BEGIN
    IF fn_funcionario_ok(NEW.responsavel_funcionario_id, NEW.unidade_id) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Funcionario invalido: inexistente, inativo ou de outra unidade.';
    END IF;
END$$
CREATE TRIGGER trg_caixa_bu BEFORE UPDATE ON caixa FOR EACH ROW
BEGIN
    IF OLD.status = 'Fechado' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa fechado nao pode ser alterado.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_abrir_caixa  (procedure: abrir o caixa)
   NO DIA A DIA: e o "botao" que o funcionario responsavel aperta ao abrir a
   loja de manha, informando o dinheiro de troco que ja esta na gaveta. O
   sistema NAO abre se a loja ja tiver um caixa aberto (esqueceram de fechar
   o de ontem, por exemplo) e NAO abre um segundo caixa no mesmo dia: e um
   caixa por dia por loja.
   Uso: CALL sp_abrir_caixa(id_unidade, id_funcionario, valor_inicial);
   [ESPEC. 4 e 7] o 2o parametro agora e o FUNCIONARIO responsavel.
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_abrir_caixa (IN p_unidade INT UNSIGNED, IN p_funcionario INT UNSIGNED, IN p_inicial DECIMAL(10,2))
BEGIN
    DECLARE EXIT HANDLER FOR 1062
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Ja existe um caixa aberto nesta unidade.';
    IF EXISTS (SELECT 1 FROM caixa WHERE unidade_id = p_unidade AND status = 'Aberto') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Ja existe um caixa aberto nesta unidade.';
    END IF;
    IF EXISTS (SELECT 1 FROM caixa WHERE unidade_id = p_unidade AND DATE(data_abertura) = CURDATE()) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Esta unidade ja teve um caixa hoje (um caixa por dia).';
    END IF;
    INSERT INTO caixa (unidade_id, responsavel_funcionario_id, valor_inicial) VALUES (p_unidade, p_funcionario, p_inicial);
    SELECT LAST_INSERT_ID() AS caixa_id;
END$$

/* ---------------------------------------------------------------------
   sp_fechar_caixa  (procedure: fechar o caixa)
   NO DIA A DIA: no fim do expediente o responsavel conta o dinheiro da gaveta
   e o total da maquininha e informa os dois valores. O sistema soma o que
   entrou em DINHEIRO (vendas, ja descontado o troco, mais abatimentos de
   notinha), soma o troco inicial e calcula a DIFERENCA do dinheiro:
       diferenca = dinheiro contado - (troco inicial + dinheiro que entrou)
   Diferenca negativa = faltou dinheiro; positiva = sobrou.
   O valor da maquininha fica guardado para conferir com vw_caixa_totais
   (Credito + Debito).
   Uso: CALL sp_fechar_caixa(id_caixa, dinheiro_contado, maquininha_contado);
   [ESPEC. 4] ganhou o 3o parametro (maquininha); pode ser NULL.
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_fechar_caixa (IN p_caixa INT UNSIGNED, IN p_contado DECIMAL(10,2), IN p_maquininha DECIMAL(10,2))
BEGIN
    DECLARE v_din DECIMAL(10,2);
    SELECT COALESCE(SUM(total), 0) INTO v_din FROM vw_caixa_totais
     WHERE caixa_id = p_caixa AND forma_pagamento = 'Dinheiro';
    UPDATE caixa
       SET data_fechamento = NOW(), valor_contado_dinheiro = p_contado,
           valor_contado_maquininha = p_maquininha,
           diferenca = p_contado - (valor_inicial + v_din), status = 'Fechado'
     WHERE id = p_caixa AND status = 'Aberto';
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa inexistente ou ja fechado.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_comanda_bi (comanda so em loja ativa) - nao ha: mantida simples.
   trg_item_comanda_bi  (nao vende o que nao tem)
   NO DIA A DIA: o cliente pede 5 pasteis e so ha 3 no estoque: o sistema
   avisa "Estoque insuficiente" e o pedido nao e lancado. Tambem recusa
   pedido em comanda que ja foi cobrada ou cancelada.
   [ESPEC. 5] regra que antes ficava em item_venda.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_item_comanda_bi BEFORE INSERT ON item_comanda FOR EACH ROW
BEGIN
    DECLARE v_saldo INT;
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_status VARCHAR(20);
    SELECT unidade_id, status INTO v_unidade, v_status FROM comanda WHERE id = NEW.comanda_id;
    IF v_status IS NULL OR v_status <> 'Aberta' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Comanda inexistente ou nao esta aberta.';
    END IF;
    SELECT quantidade INTO v_saldo FROM estoque
     WHERE unidade_id = v_unidade AND produto_id = NEW.produto_id FOR UPDATE;
    IF v_saldo IS NULL OR v_saldo < NEW.quantidade THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Estoque insuficiente para o produto.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_item_comanda_ai  (baixa automatica do estoque)
   NO DIA A DIA: a cada pedido lancado na comanda, o sistema tira a quantidade
   do estoque da loja e anota no extrato (tipo Venda). O atendente so digita
   o que o cliente pediu. Quando a comanda for cobrada, o estoque NAO baixa
   de novo.
   [ESPEC. 5] regra que antes ficava em item_venda.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_item_comanda_ai AFTER INSERT ON item_comanda FOR EACH ROW
BEGIN
    DECLARE v_unidade INT UNSIGNED;
    SELECT unidade_id INTO v_unidade FROM comanda WHERE id = NEW.comanda_id;
    UPDATE estoque SET quantidade = quantidade - NEW.quantidade
     WHERE unidade_id = v_unidade AND produto_id = NEW.produto_id;
    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    VALUES (v_unidade, NEW.produto_id, 'Venda', -NEW.quantidade, @usuario_id);
END$$

/* ---------------------------------------------------------------------
   sp_cancelar_comanda  (procedure: cancelar uma comanda ainda aberta)
   NO DIA A DIA: o cliente desistiu antes de pagar. Os produtos voltam para o
   estoque (anotado no extrato como Estorno) e a comanda fica marcada como
   "Cancelada" (nao some). Comanda ja cobrada nao se cancela aqui: use
   sp_cancelar_venda.
   Uso: CALL sp_cancelar_comanda(id_comanda, id_usuario);
   [ESPEC. 5]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_cancelar_comanda (IN p_comanda INT UNSIGNED, IN p_usuario INT UNSIGNED)
BEGIN
    DECLARE v_status VARCHAR(20);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;

    IF NOT EXISTS (SELECT 1 FROM usuario WHERE id = p_usuario AND status = 'Ativo') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Usuario invalido ou inativo.';
    END IF;

    START TRANSACTION;
    SELECT status, unidade_id INTO v_status, v_unidade FROM comanda WHERE id = p_comanda FOR UPDATE;
    IF v_status IS NULL OR v_status <> 'Aberta' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Comanda inexistente ou nao esta aberta.';
    END IF;

    UPDATE estoque e
      JOIN (SELECT produto_id, SUM(quantidade) AS q FROM item_comanda WHERE comanda_id = p_comanda GROUP BY produto_id) i
        ON i.produto_id = e.produto_id
       SET e.quantidade = e.quantidade + i.q
     WHERE e.unidade_id = v_unidade;

    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    SELECT v_unidade, produto_id, 'Estorno', SUM(quantidade), p_usuario
      FROM item_comanda WHERE comanda_id = p_comanda GROUP BY produto_id;

    SET @usuario_id = p_usuario;
    UPDATE comanda SET status = 'Cancelada' WHERE id = p_comanda;
    COMMIT;
END$$

/* ---------------------------------------------------------------------
   trg_venda_bi  (so vende com o caixa aberto)
   NO DIA A DIA: se o atendente tentar cobrar antes de abrir o caixa, ou com o
   caixa de outra loja, o sistema recusa. Garante que toda venda entra no
   caixa do dia e que o fechamento bate. Agora vale para a cobranca da
   comanda (sp_cobrar_comanda).
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_venda_bi BEFORE INSERT ON venda FOR EACH ROW
BEGIN
    IF (SELECT COUNT(*) FROM caixa
         WHERE id = NEW.caixa_id AND unidade_id = NEW.unidade_id AND status = 'Aberto') = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nao ha caixa aberto nesta unidade.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_item_venda_ai  (soma o total da venda)
   NO DIA A DIA: a cada linha copiada para o cupom, soma o valor no total da
   venda. NAO mexe no estoque (ele ja baixou quando o pedido entrou na
   comanda).
   [ESPEC. 5] a baixa de estoque saiu daqui; ficou so o total.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_item_venda_ai AFTER INSERT ON item_venda FOR EACH ROW
    UPDATE venda SET valor_total = valor_total + NEW.quantidade * NEW.preco_unitario WHERE id = NEW.venda_id$$

/* ---------------------------------------------------------------------
   sp_cobrar_comanda  (procedure: cobrar a comanda e gerar a venda)
   NO DIA A DIA: o cliente pediu a conta. O sistema cria o cupom (venda) com a
   mesa e o cliente da comanda, copia os itens e marca a comanda como
   "Fechada". NAO baixa o estoque de novo. Depois o C# registra como o
   cliente pagou (pagamento_venda) e chama sp_finalizar_venda.
   IMPORTANTE: chame dentro de uma transacao do C# junto com os pagamentos e
   sp_finalizar_venda; se algo der erro, ROLLBACK e a comanda volta a aberta.
   Devolve o id da venda criada.
   Uso: CALL sp_cobrar_comanda(id_comanda, id_caixa, id_usuario);
   [ESPEC. 5]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_cobrar_comanda (IN p_comanda INT UNSIGNED, IN p_caixa INT UNSIGNED, IN p_usuario INT UNSIGNED)
BEGIN
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_mesa INT;
    DECLARE v_cliente INT UNSIGNED;
    DECLARE v_status VARCHAR(20);
    DECLARE v_venda INT UNSIGNED;

    IF NOT EXISTS (SELECT 1 FROM usuario WHERE id = p_usuario AND status = 'Ativo') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Usuario invalido ou inativo.';
    END IF;

    SELECT unidade_id, numero_mesa, cliente_id, status
      INTO v_unidade, v_mesa, v_cliente, v_status
      FROM comanda WHERE id = p_comanda FOR UPDATE;
    IF v_status IS NULL OR v_status <> 'Aberta' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Comanda inexistente ou nao esta aberta.';
    END IF;
    IF NOT EXISTS (SELECT 1 FROM item_comanda WHERE comanda_id = p_comanda) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Comanda sem itens.';
    END IF;

    SET @usuario_id = p_usuario;
    INSERT INTO venda (unidade_id, numero_mesa, caixa_id, cliente_id, comanda_id)
    VALUES (v_unidade, v_mesa, p_caixa, v_cliente, p_comanda);
    SET v_venda = LAST_INSERT_ID();

    INSERT INTO item_venda (venda_id, produto_id, quantidade, preco_unitario)
    SELECT v_venda, produto_id, quantidade, preco_unitario
      FROM item_comanda WHERE comanda_id = p_comanda;

    UPDATE comanda SET status = 'Fechada' WHERE id = p_comanda;
    SELECT v_venda AS venda_id;
END$$

/* ---------------------------------------------------------------------
   trg_pagamento_bi  (regras do pagamento)
   NO DIA A DIA: so aceita pagamento de venda que existe e esta concluida. E
   "Notinha" (fiado) so e aceita quando a venda tem um CLIENTE CADASTRADO:
   ninguem anota fiado para um desconhecido.
   [ESPEC. 7 - RN46]
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_pagamento_bi BEFORE INSERT ON pagamento_venda FOR EACH ROW
BEGIN
    DECLARE v_status VARCHAR(20);
    DECLARE v_cliente INT UNSIGNED;
    SELECT status, cliente_id INTO v_status, v_cliente FROM venda WHERE id = NEW.venda_id;
    IF v_status IS NULL OR v_status <> 'Concluida' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda inexistente ou cancelada.';
    END IF;
    IF NEW.forma_pagamento = 'Notinha' AND v_cliente IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Notinha exige cliente cadastrado na venda.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_abatimento_bi  (abatimento de notinha so com caixa aberto)
   NO DIA A DIA: quando o cliente volta para pagar a notinha, o dinheiro entra
   no caixa; entao o caixa precisa estar aberto e ser da mesma loja, igual
   a uma venda. (Pergunta em aberto 4 da especificacao: implementada como
   recomendado; basta apagar esta trigger se o grupo decidir o contrario.)
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_abatimento_bi BEFORE INSERT ON abatimento_notinha FOR EACH ROW
BEGIN
    IF (SELECT COUNT(*) FROM caixa
         WHERE id = NEW.caixa_id AND unidade_id = NEW.unidade_id AND status = 'Aberto') = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nao ha caixa aberto nesta unidade.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_finalizar_venda  (procedure: confere os pagamentos e calcula o troco)
   NO DIA A DIA: e o momento do "pagar". Depois que os pagamentos foram
   lancados, o sistema confere se eles cobrem o total e calcula o TROCO na
   hora (substitui a calculadora). O troco so pode sair do dinheiro: se o
   cliente pagou no Pix mais do que devia, o sistema recusa.
   Ex.: total R$ 30,00, cliente deu R$ 50,00 em dinheiro -> troco R$ 20,00.
   Uso: CALL sp_finalizar_venda(id_venda);
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_finalizar_venda (IN p_venda INT UNSIGNED)
BEGIN
    DECLARE v_total DECIMAL(10,2);
    DECLARE v_pago DECIMAL(10,2);
    DECLARE v_dinheiro DECIMAL(10,2);
    SELECT valor_total INTO v_total FROM venda WHERE id = p_venda AND status = 'Concluida';
    IF v_total IS NULL OR v_total <= 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda sem itens.';
    END IF;
    SELECT COALESCE(SUM(valor), 0), COALESCE(SUM(IF(forma_pagamento = 'Dinheiro', valor, 0)), 0)
      INTO v_pago, v_dinheiro FROM pagamento_venda WHERE venda_id = p_venda;
    IF v_pago < v_total THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Pagamentos somam menos que o total.';
    END IF;
    IF v_pago - v_total > v_dinheiro THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Troco maior que o valor pago em dinheiro.';
    END IF;
    UPDATE venda SET valor_recebido = v_pago, troco = v_pago - v_total WHERE id = p_venda;
END$$

/* ---------------------------------------------------------------------
   sp_cancelar_venda  (procedure: cancelar uma venda ja cobrada)
   NO DIA A DIA: o atendente digitou errado ou o cliente desistiu depois de
   pagar. Somente Gerente ou Administrador consegue cancelar, e e obrigatorio
   informar o motivo. O sistema devolve os produtos ao estoque (Estorno) e
   mantem a venda registrada como "Cancelada" (nao some), com quem cancelou
   e quando. Se a venda gerou NOTINHA e o cliente ja abateu a divida alem do
   que restaria, o cancelamento e recusado (RN46). Se algo falhar no meio,
   tudo e desfeito e nada fica pela metade.
   Uso: CALL sp_cancelar_venda(id_venda, id_usuario, 'motivo');
   Regra da notinha ja abatida (provisoria): ver observacao no final do
   arquivo e nas notas da atualizacao.
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_cancelar_venda (IN p_venda INT UNSIGNED, IN p_usuario INT UNSIGNED, IN p_motivo VARCHAR(200))
BEGIN
    DECLARE v_perfil VARCHAR(20);
    DECLARE v_status VARCHAR(20);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_cliente INT UNSIGNED;
    DECLARE v_outras DECIMAL(10,2);
    DECLARE v_abatido DECIMAL(10,2);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;

    SELECT perfil INTO v_perfil FROM usuario WHERE id = p_usuario AND status = 'Ativo';
    IF v_perfil IS NULL OR v_perfil = 'Atendente' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Apenas Administrador ou Gerente pode cancelar vendas.';
    END IF;
    IF p_motivo IS NULL OR TRIM(p_motivo) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Informe o motivo do cancelamento.';
    END IF;

    START TRANSACTION;
    SELECT status, unidade_id, cliente_id INTO v_status, v_unidade, v_cliente FROM venda WHERE id = p_venda FOR UPDATE;
    IF v_status IS NULL OR v_status <> 'Concluida' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda inexistente ou ja cancelada.';
    END IF;

    -- RN46: venda com notinha nao pode ser cancelada se a divida do cliente ja foi abatida
    IF EXISTS (SELECT 1 FROM notinha WHERE venda_id = p_venda) THEN
        SELECT COALESCE(SUM(n.valor), 0) INTO v_outras
          FROM notinha n JOIN venda v ON v.id = n.venda_id AND v.status = 'Concluida'
         WHERE n.cliente_id = v_cliente AND n.venda_id <> p_venda;
        SELECT COALESCE(SUM(valor), 0) INTO v_abatido FROM abatimento_notinha WHERE cliente_id = v_cliente;
        IF v_abatido > v_outras THEN
            SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda com notinha ja abatida: nao pode ser cancelada.';
        END IF;
    END IF;

    UPDATE estoque e
      JOIN (SELECT produto_id, SUM(quantidade) AS q FROM item_venda WHERE venda_id = p_venda GROUP BY produto_id) i
        ON i.produto_id = e.produto_id
       SET e.quantidade = e.quantidade + i.q
     WHERE e.unidade_id = v_unidade;

    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    SELECT v_unidade, produto_id, 'Estorno', SUM(quantidade), p_usuario
      FROM item_venda WHERE venda_id = p_venda GROUP BY produto_id;

    SET @usuario_id = p_usuario;
    UPDATE venda SET status = 'Cancelada', cancelado_por = p_usuario,
           motivo_cancelamento = p_motivo, data_cancelamento = NOW()
     WHERE id = p_venda;
    COMMIT;
END$$

/* ---------------------------------------------------------------------
   sp_dre  (procedure: resultado de um periodo, o "DRE")
   NO DIA A DIA: o "resultado do periodo" para o dono, com as datas escolhidas
   na tela (nao precisa ser mes fechado). Mostra, por loja (ou so de uma):
     receita   = vendas concluidas no periodo (a Notinha conta na data da
                 VENDA, nao na do abatimento);
     despesa   = o que foi comprado no periodo (entradas de estoque:
                 quantidade x custo);
     resultado = receita - despesa;
     perdas    = o custo do que foi jogado fora, mostrado A PARTE (nao e
                 somado de novo na despesa, pois ja esta dentro das compras).
   O custo da perda e o custo da ultima entrada daquele produto naquela loja
   ate a data da perda (0 se nunca houve entrada).
   Uso: CALL sp_dre(NULL, '2026-11-01', '2026-11-30');   -- todas as lojas
        CALL sp_dre(1,    '2026-11-01', '2026-11-30');   -- so a loja 1
   [ESPEC. 6]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_dre (IN p_unidade_id INT UNSIGNED, IN p_data_inicio DATE, IN p_data_fim DATE)
BEGIN
    IF p_data_inicio IS NULL OR p_data_fim IS NULL OR p_data_inicio > p_data_fim THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Periodo invalido.';
    END IF;
    SELECT u.id AS unidade_id, u.nome AS unidade,
           COALESCE(r.receita, 0)  AS receita,
           COALESCE(d.despesa, 0)  AS despesa,
           COALESCE(r.receita, 0) - COALESCE(d.despesa, 0) AS resultado,
           COALESCE(p.perdas, 0)   AS perdas
      FROM unidade u
      LEFT JOIN (SELECT unidade_id, SUM(valor_total) AS receita
                   FROM venda
                  WHERE status = 'Concluida'
                    AND data_hora >= p_data_inicio AND data_hora < DATE_ADD(p_data_fim, INTERVAL 1 DAY)
                  GROUP BY unidade_id) r ON r.unidade_id = u.id
      LEFT JOIN (SELECT unidade_id, SUM(quantidade * custo_unitario) AS despesa
                   FROM entrada_estoque
                  WHERE data_entrada BETWEEN p_data_inicio AND p_data_fim
                  GROUP BY unidade_id) d ON d.unidade_id = u.id
      LEFT JOIN (SELECT pe.unidade_id,
                        SUM(pe.quantidade * COALESCE(
                            (SELECT en.custo_unitario FROM entrada_estoque en
                              WHERE en.unidade_id = pe.unidade_id AND en.produto_id = pe.produto_id
                                AND en.data_entrada <= pe.data
                              ORDER BY en.data_entrada DESC, en.id DESC LIMIT 1), 0)) AS perdas
                   FROM perda pe
                  WHERE pe.data BETWEEN p_data_inicio AND p_data_fim
                  GROUP BY pe.unidade_id) p ON p.unidade_id = u.id
     WHERE p_unidade_id IS NULL OR u.id = p_unidade_id
     ORDER BY u.id;
END$$

DELIMITER ;

/* =====================================================================
   VIEWS (consultas prontas para os relatorios)
   No C#, basta fazer SELECT * FROM vw_... WHERE unidade_id = ... e filtrar.
   [ESPEC. 6] vw_validade e vw_dre_mensal foram removidas (o DRE agora e a
   procedure sp_dre). As views de vendas e de caixa leem pagamento_venda.
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
   vw_pagamento_liquido   (auxiliar das views abaixo)
   NO DIA A DIA: "quanto realmente ficou na loja por cada forma de pagamento".
   O cliente que da R$ 50 em dinheiro numa conta de R$ 30 recebe R$ 20 de
   troco: so R$ 30 ficaram. Esta view ja desconta o troco do Dinheiro, e
   ignora vendas canceladas. Uma linha por venda e forma de pagamento.
   --------------------------------------------------------------------- */
CREATE VIEW vw_pagamento_liquido AS
SELECT v.id AS venda_id, v.unidade_id, v.caixa_id, DATE(v.data_hora) AS dia, x.forma_pagamento,
       x.valor - IF(x.forma_pagamento = 'Dinheiro', v.troco, 0) AS valor_liquido
  FROM (SELECT venda_id, forma_pagamento, SUM(valor) AS valor
          FROM pagamento_venda GROUP BY venda_id, forma_pagamento) x
  JOIN venda v ON v.id = x.venda_id AND v.status = 'Concluida';

/* ---------------------------------------------------------------------
   vw_vendas_diarias
   NO DIA A DIA: o relatorio de faturamento. Mostra, por loja e por dia, quantas
   vendas foram feitas e quanto entrou, separado por forma de pagamento
   (Dinheiro, Pix, Credito, Debito e Notinha - esta ultima e "a receber").
   Ignora vendas canceladas. Responde "quanto vendemos ontem no Centro?".
   --------------------------------------------------------------------- */
CREATE VIEW vw_vendas_diarias AS
SELECT unidade_id, dia, forma_pagamento,
       COUNT(*) AS qtd_vendas, SUM(valor_liquido) AS faturamento
  FROM vw_pagamento_liquido
 GROUP BY unidade_id, dia, forma_pagamento;

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
   NO DIA A DIA: o resumo do fechamento do caixa: quanto entrou naquele caixa
   em Dinheiro, Pix, Credito, Debito e Notinha, separando o que veio de
   VENDAS e o que veio de ABATIMENTOS de notinha. Serve para conferir com a
   maquininha (Credito + Debito) e com o extrato do Pix.
   Uso: SELECT * FROM vw_caixa_totais WHERE caixa_id = 1;
   --------------------------------------------------------------------- */
CREATE VIEW vw_caixa_totais AS
SELECT caixa_id, forma_pagamento,
       SUM(vendas) AS total_vendas, SUM(abatimentos) AS total_abatimentos,
       SUM(vendas) + SUM(abatimentos) AS total
  FROM (SELECT caixa_id, forma_pagamento, valor_liquido AS vendas, 0 AS abatimentos
          FROM vw_pagamento_liquido
        UNION ALL
        SELECT caixa_id, forma_pagamento, 0, valor FROM abatimento_notinha) t
 GROUP BY caixa_id, forma_pagamento;

/* ---------------------------------------------------------------------
   vw_notinha_saldo
   NO DIA A DIA: "quanto cada cliente esta devendo". Soma as notinhas (de
   vendas nao canceladas) e subtrai o que ja foi abatido.
   --------------------------------------------------------------------- */
CREATE VIEW vw_notinha_saldo AS
SELECT c.id AS cliente_id, c.nome AS cliente,
       COALESCE(n.total, 0) AS total_notinhas, COALESCE(a.total, 0) AS total_abatido,
       COALESCE(n.total, 0) - COALESCE(a.total, 0) AS saldo_devedor
  FROM cliente c
  LEFT JOIN (SELECT n.cliente_id, SUM(n.valor) AS total
               FROM notinha n JOIN venda v ON v.id = n.venda_id AND v.status = 'Concluida'
              GROUP BY n.cliente_id) n ON n.cliente_id = c.id
  LEFT JOIN (SELECT cliente_id, SUM(valor) AS total FROM abatimento_notinha GROUP BY cliente_id) a
         ON a.cliente_id = c.id;

/* =====================================================================
   SEGURANCA - usuario do banco usado pelo sistema
   NO DIA A DIA: o programa em C# NAO deve entrar no banco como "root" (dono
   de tudo). Este usuario so pode ler dados, cadastrar, lancar comandas,
   registrar pagamentos, entradas e perdas. Nao pode apagar nada, nem mexer
   direto no estoque, no log, nem criar vendas "na mao": venda so nasce por
   sp_cobrar_comanda, e o estoque so muda pelas triggers e procedures acima.
   Assim, mesmo que a senha do sistema vaze, o estrago possivel e pequeno.
   TROQUE A SENHA ANTES DE USAR DE VERDADE.
   [ESPEC. 8] permissoes das tabelas novas; lancamento nao existe mais.
   ===================================================================== */
DROP USER IF EXISTS 'app_salgados'@'%';   -- recomeca as permissoes do zero ao rodar o script de novo
CREATE USER 'app_salgados'@'%' IDENTIFIED BY 'TROQUE_ESTA_SENHA_FORTE';
GRANT SELECT  ON salgados_cia.* TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.unidade     TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.usuario     TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.produto     TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.fornecedor  TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.funcionario TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.cliente     TO 'app_salgados'@'%';
-- comanda: o app cria e pode trocar mesa/cliente; o STATUS so muda pelas procedures
GRANT INSERT ON salgados_cia.comanda             TO 'app_salgados'@'%';
GRANT UPDATE (numero_mesa, cliente_id) ON salgados_cia.comanda TO 'app_salgados'@'%';
-- historico financeiro/estoque: so INSERT (nada de UPDATE/DELETE)
GRANT INSERT ON salgados_cia.item_comanda        TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.pagamento_venda     TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.notinha             TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.abatimento_notinha  TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.entrada_estoque     TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.perda               TO 'app_salgados'@'%';
-- inventario: a contagem pode ser corrigida enquanto e preenchida
GRANT INSERT, UPDATE ON salgados_cia.inventario      TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.item_inventario TO 'app_salgados'@'%';
GRANT EXECUTE ON salgados_cia.*                  TO 'app_salgados'@'%';
FLUSH PRIVILEGES;

/* =====================================================================
   ERP SALGADOS & CIA - Banco de dados (versao enxuta)
   Testado no MariaDB 10.4 (XAMPP) e no MySQL 8.0.
   Ordem: 01_schema.sql -> 02_dados_teste.sql

   Esta versao aplica a "Especificacao tecnica - atualizacao do banco de
   dados" e as correcoes da conferencia "Pendencias do schema 2.0"
   (marcadas [PEND. A1] ... [PEND. B6]). O que nao foi citado foi mantido.

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
   Login e logout: CALL sp_registrar_log('LOGIN', 'usuario', <id>, '<login>', NULL);

   FLUXO DE VENDA (comanda -> cobranca)
     1) INSERT em comanda + INSERT em item_comanda  -> o estoque baixa AQUI
     2) INSERT em pagamento_comanda (um por pagamento; pode ser PARCIAL: a
        comanda continua aberta ate cobrir o total)
     3) CALL sp_cobrar_comanda(...)  -> numa unica transacao: cria a venda,
        copia os itens (sem baixar o estoque de novo), copia os pagamentos,
        gera a notinha (se houver), confere o total e calcula o troco.
        Se algo falhar, nada e gravado e a comanda continua aberta.
   Balcao = comanda de mesa 0, criada e cobrada na mesma hora.
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
   Loja que ja teve movimento e marcada como Inativo; so uma loja sem
   nenhum historico pode ser excluida.
   --------------------------------------------------------------------- */
CREATE TABLE unidade (
    id       INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome     VARCHAR(80)  NOT NULL UNIQUE,
    cnpj     VARCHAR(18)  UNIQUE,
    endereco VARCHAR(150) NOT NULL,
    numero   VARCHAR(10),
    telefone VARCHAR(20),
    aceita_vale_alimentacao TINYINT(1) NOT NULL DEFAULT 0,   -- 1 = a loja aceita Vale Alimentacao
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
   Cada loja tem NO MAXIMO UM login de Atendente (o "login base").
   A senha e guardada como "hash" (embaralhada), entao nem o dono do banco
   consegue ler a senha de ninguem. Login que sai e Inativado, para o
   historico nao se perder. Nao existem dois logins iguais.
   [PEND. B4] um unico Atendente por unidade; Atendente exige unidade.
   BLOQUEIO POR TENTATIVAS: depois de 5 senhas erradas seguidas, o login fica
   bloqueado por 15 minutos (tentativas_falhas e bloqueado_ate; veja sp_login_falhou).
   --------------------------------------------------------------------- */
CREATE TABLE usuario (
    id         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    login      VARCHAR(40)  NOT NULL UNIQUE,
    senha_hash VARCHAR(255) NOT NULL,              -- gerado no C# com PBKDF2 (SHA-256, 10.000 iteracoes, sal), formato "sal:hash"
    perfil     ENUM('Administrador','Gerente','Atendente') NOT NULL DEFAULT 'Atendente',
    unidade_id INT UNSIGNED NULL,                  -- Gerente/Administrador nao tem unidade fixa
    status     ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo',
    tentativas_falhas INT UNSIGNED NOT NULL DEFAULT 0,   -- senhas erradas seguidas (zera no login certo)
    bloqueado_ate     DATETIME NULL,                     -- enquanto for no futuro, o login esta bloqueado
    -- coluna auxiliar: garante no maximo 1 login de Atendente por unidade
    unidade_login_base INT UNSIGNED GENERATED ALWAYS AS (IF(perfil = 'Atendente', unidade_id, NULL)) STORED,
    UNIQUE KEY uq_login_base (unidade_login_base),
    CHECK (perfil <> 'Atendente' OR unidade_id IS NOT NULL),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   FUNCIONARIO
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
   CLIENTE
   NO DIA A DIA: o cadastro de quem compra "na notinha" (fiado). No balcao a
   maioria das vendas NAO tem cliente; ele so e obrigatorio quando parte da
   conta vai ficar para pagar depois (Notinha). O cadastro e unico para a
   rede; ja a divida (notinha) e controlada por loja.
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
   com preco de venda. Tambem guarda o ESTOQUE MINIMO (quando o saldo chegar
   ao minimo ou ficar abaixo dele, o produto aparece como "estoque critico").
   Produto que ja foi vendido nao pode ser apagado, so Inativado. O preco
   nunca pode ser zero ou negativo. O estoque minimo aceita casas decimais
   (produto vendido por quilo ou litro).
   [PEND. A2] estoque_minimo agora e DECIMAL(10,2).
   --------------------------------------------------------------------- */
CREATE TABLE produto (
    id                INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome              VARCHAR(100) NOT NULL UNIQUE,
    categoria         VARCHAR(50)  NOT NULL,
    preco_venda       DECIMAL(10,2) NOT NULL CHECK (preco_venda > 0),
    unidade_medida    VARCHAR(10)  NOT NULL DEFAULT 'un',
    estoque_minimo    DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (estoque_minimo >= 0),
    status            ENUM('Ativo','Inativo') NOT NULL DEFAULT 'Ativo'
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   FORNECEDOR
   NO DIA A DIA: quem vende os salgados prontos para a rede (a salgaderia nao
   fabrica nada). Quando chega mercadoria, a entrada de estoque registra de
   qual fornecedor veio. Evita cadastrar o mesmo CNPJ duas vezes.
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
   disso). O saldo nunca fica negativo. Aceita casas decimais (kg, litro).
   Se o produto ou a unidade for excluido (so e possivel quando nunca teve
   movimento), o saldo zerado vai junto.
   [PEND. A2 e A8] quantidade DECIMAL(10,2); exclusao em cascata.
   --------------------------------------------------------------------- */
CREATE TABLE estoque (
    unidade_id INT UNSIGNED NOT NULL,
    produto_id INT UNSIGNED NOT NULL,
    quantidade DECIMAL(10,2) NOT NULL DEFAULT 0 CHECK (quantidade >= 0),
    PRIMARY KEY (unidade_id, produto_id),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id) ON DELETE CASCADE,
    FOREIGN KEY (produto_id) REFERENCES produto (id) ON DELETE CASCADE
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   MOVIMENTACAO_ESTOQUE
   NO DIA A DIA: o "extrato" do estoque, como o extrato de uma conta bancaria.
   Cada entrada, venda, estorno, perda e ajuste vira uma linha (+ entra,
   - sai). Serve para o relatorio de movimentacao e para descobrir "por que
   o saldo esta assim?". Preenchida automaticamente pelas triggers.
   --------------------------------------------------------------------- */
CREATE TABLE movimentacao_estoque (
    id         BIGINT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id INT UNSIGNED NOT NULL,
    produto_id INT UNSIGNED NOT NULL,
    tipo       ENUM('Entrada','Venda','Estorno','Perda','Ajuste') NOT NULL,
    quantidade DECIMAL(10,2) NOT NULL,             -- + entra, - sai
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
   --------------------------------------------------------------------- */
CREATE TABLE entrada_estoque (
    id                         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id                 INT UNSIGNED NOT NULL,
    produto_id                 INT UNSIGNED NOT NULL,
    quantidade                 DECIMAL(10,2) NOT NULL CHECK (quantidade > 0),
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
   --------------------------------------------------------------------- */
CREATE TABLE perda (
    id                         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id                 INT UNSIGNED NOT NULL,
    produto_id                 INT UNSIGNED NOT NULL,
    quantidade                 DECIMAL(10,2) NOT NULL CHECK (quantidade > 0),
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
   mais ser alterado. Enquanto aberto, o valor inicial e o responsavel podem
   ser corrigidos (sp_editar_caixa), com motivo e autorizacao.
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
   COMANDA
   NO DIA A DIA: a "conta aberta" de uma mesa (ou do balcao, mesa 0). Enquanto
   o cliente consome, os pedidos vao sendo lancados na comanda. Quando ele
   vai pagar, a comanda e COBRADA e vira uma venda. Comanda cancelada nao e
   apagada: fica com status Cancelada, para auditoria. Cada mesa (diferente
   de 0) so pode ter UMA comanda aberta por loja.
   [PEND. A10] mesa_aberta impede duas comandas abertas na mesma mesa.
   --------------------------------------------------------------------- */
CREATE TABLE comanda (
    id            INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id    INT UNSIGNED NOT NULL,
    numero_mesa   INT NOT NULL DEFAULT 0,          -- 0 = Balcao
    cliente_id    INT UNSIGNED,
    status        ENUM('Aberta','Cancelada','Fechada') NOT NULL DEFAULT 'Aberta',
    data_abertura DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    -- coluna auxiliar: garante no maximo 1 comanda aberta por mesa (mesa > 0) em cada unidade
    mesa_aberta   VARCHAR(30) GENERATED ALWAYS AS (IF(status = 'Aberta' AND numero_mesa > 0, CONCAT(unidade_id, '-', numero_mesa), NULL)) STORED,
    UNIQUE KEY uq_mesa_aberta (mesa_aberta),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (cliente_id) REFERENCES cliente (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   ITEM_COMANDA
   NO DIA A DIA: cada pedido lancado na comanda ("3 coxinhas"). E AQUI que o
   estoque baixa: no momento em que o pedido e enviado, nao quando a conta e
   paga. Depois que a comanda e cobrada, estas linhas continuam guardadas
   (o item_venda recebe uma copia).
   --------------------------------------------------------------------- */
CREATE TABLE item_comanda (
    id             INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    comanda_id     INT UNSIGNED NOT NULL,
    produto_id     INT UNSIGNED NOT NULL,
    quantidade     DECIMAL(10,2) NOT NULL CHECK (quantidade > 0),
    preco_unitario DECIMAL(10,2) NOT NULL CHECK (preco_unitario > 0),
    FOREIGN KEY (comanda_id) REFERENCES comanda (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   PAGAMENTO_COMANDA   [PEND. A1]
   NO DIA A DIA: o que o cliente ja pagou da conta AINDA ABERTA. O cliente
   pode pagar aos poucos (R$ 20 agora, o resto depois): cada pagamento vira
   uma linha aqui, e a comanda mostra "pago" e "falta". Notinha (fiado) so e
   aceita se a comanda tem cliente cadastrado. Quando a comanda e cobrada,
   estas linhas sao copiadas para PAGAMENTO_VENDA. Comanda com pagamento nao
   pode ser cancelada.
   --------------------------------------------------------------------- */
CREATE TABLE pagamento_comanda (
    id              INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    comanda_id      INT UNSIGNED NOT NULL,
    forma_pagamento ENUM('Dinheiro','Pix','Credito','Debito','ValeAlimentacao','Notinha') NOT NULL,
    valor           DECIMAL(10,2) NOT NULL CHECK (valor > 0),
    data_hora       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    KEY ix_pag_comanda (comanda_id),
    FOREIGN KEY (comanda_id) REFERENCES comanda (id)
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
   depois, as vendas antigas continuam corretas. O subtotal de cada
   linha e calculado sozinho.
   --------------------------------------------------------------------- */
CREATE TABLE item_venda (
    id             INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    venda_id       INT UNSIGNED NOT NULL,
    produto_id     INT UNSIGNED NOT NULL,
    quantidade     DECIMAL(10,2) NOT NULL CHECK (quantidade > 0),
    preco_unitario DECIMAL(10,2) NOT NULL CHECK (preco_unitario > 0),
    subtotal       DECIMAL(10,2) GENERATED ALWAYS AS (ROUND(quantidade * preco_unitario, 2)) STORED,
    KEY ix_item (venda_id),
    FOREIGN KEY (venda_id)   REFERENCES venda (id),
    FOREIGN KEY (produto_id) REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   PAGAMENTO_VENDA
   NO DIA A DIA: como o cliente pagou. Uma venda pode ter varias linhas
   (ex.: R$ 20 em dinheiro + R$ 10 no Pix). Formas: Dinheiro, Pix, Credito,
   Debito, ValeAlimentacao (so nas lojas que aceitam) e Notinha (fiado). Em Dinheiro, o valor guardado e o que o cliente
   ENTREGOU (o troco e calculado por sp_finalizar_venda). Nasce da
   sp_cobrar_comanda: o aplicativo nao insere aqui.
   --------------------------------------------------------------------- */
CREATE TABLE pagamento_venda (
    id              INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    venda_id        INT UNSIGNED NOT NULL,
    forma_pagamento ENUM('Dinheiro','Pix','Credito','Debito','ValeAlimentacao','Notinha') NOT NULL,
    valor           DECIMAL(10,2) NOT NULL CHECK (valor > 0),
    FOREIGN KEY (venda_id) REFERENCES venda (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   NOTINHA
   NO DIA A DIA: a "divida" do cliente: a parte da venda que ficou para pagar
   depois. Uma linha por pagamento em Notinha, criada sozinha (trigger) quando
   a comanda e cobrada. A divida e controlada POR LOJA.
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
   ABATIMENTO_NOTINHA
   NO DIA A DIA: quando o cliente volta para pagar (todo ou parte) o que
   deve na notinha. O dinheiro entra no caixa do dia. O saldo do cliente em
   cada loja = soma das notinhas - soma dos abatimentos (veja
   vw_notinha_saldo).
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
   INVENTARIO e ITEM_INVENTARIO
   NO DIA A DIA: a "contagem do estoque". O funcionario conta o que tem na
   prateleira; o sistema guarda o saldo que ELE achava que tinha
   (saldo_sistema) ao lado do que foi realmente contado, e o motivo da
   diferenca. Cada contagem e um INVENTARIO com varios ITENS. A contagem pode
   ser corrigida ate ser EFETIVADA (sp_efetivar_inventario); depois disso
   fica lacrada e o estoque ja foi ajustado.
   [PEND. A3] efetivado_em; itens sem repeticao de produto.
   --------------------------------------------------------------------- */
CREATE TABLE inventario (
    id                         INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    unidade_id                 INT UNSIGNED NOT NULL,
    responsavel_funcionario_id INT UNSIGNED NOT NULL,
    usuario_id                 INT UNSIGNED NOT NULL,
    data_hora                  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    observacao                 VARCHAR(255),
    efetivado_em               DATETIME NULL,
    FOREIGN KEY (unidade_id) REFERENCES unidade (id),
    FOREIGN KEY (responsavel_funcionario_id) REFERENCES funcionario (id),
    FOREIGN KEY (usuario_id) REFERENCES usuario (id)
) ENGINE=InnoDB;

CREATE TABLE item_inventario (
    id                 INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    inventario_id      INT UNSIGNED NOT NULL,
    produto_id         INT UNSIGNED NOT NULL,
    saldo_sistema      DECIMAL(10,2) NOT NULL,
    quantidade_contada DECIMAL(10,2) CHECK (quantidade_contada IS NULL OR quantidade_contada >= 0),
    motivo             VARCHAR(200),
    UNIQUE KEY uq_item_inventario (inventario_id, produto_id),
    FOREIGN KEY (inventario_id) REFERENCES inventario (id),
    FOREIGN KEY (produto_id)    REFERENCES produto (id)
) ENGINE=InnoDB;

/* ---------------------------------------------------------------------
   LOG_OPERACAO
   NO DIA A DIA: a "caixa-preta" do sistema. Anota quem mexeu em que, em qual
   loja e quando (ex.: "login do patrao alterou o preco do produto 3 as
   14:32, na loja Centro"). Se algo estranho acontecer, e aqui que o dono
   descobre o responsavel. E preenchida so por triggers e procedures e
   NINGUEM consegue editar ou apagar suas linhas, nem pelo sistema.
   Operacoes: INSERT, UPDATE, DELETE, LOGIN e LOGOUT. "registro" e o nome do
   item mexido (ex.: o nome do produto) e "detalhes" mostra o valor
   anterior e o novo.
   O unidade_id vem de @unidade_id (aplicacao; nunca de usuario.unidade_id).
   [PEND. A9] nova coluna registro; operacoes LOGIN e LOGOUT.
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
    registro    VARCHAR(120),
    KEY ix_log (unidade_id, data_hora),
    FOREIGN KEY (usuario_id) REFERENCES usuario (id),
    FOREIGN KEY (unidade_id) REFERENCES unidade (id)
) ENGINE=InnoDB;

/* =====================================================================
   FUNCOES, TRIGGERS E PROCEDURES
   ===================================================================== */
DELIMITER $$

/* ---------------------------------------------------------------------
   fn_funcionario_ok  (funcao auxiliar)
   NO DIA A DIA: pergunta "este funcionario existe, esta ativo e trabalha
   mesmo nesta loja?". Usada pelas triggers do caixa, da entrada, da perda
   e do inventario para impedir, por exemplo, que a Maria da loja Bairro
   apareca como quem recebeu mercadoria na loja Centro.
   --------------------------------------------------------------------- */
CREATE FUNCTION fn_funcionario_ok (p_func INT UNSIGNED, p_unidade INT UNSIGNED)
RETURNS TINYINT DETERMINISTIC READS SQL DATA
BEGIN
    RETURN (SELECT COUNT(*) FROM funcionario
             WHERE id = p_func AND unidade_id = p_unidade AND status = 'Ativo') > 0;
END$$

/* ---------------------------------------------------------------------
   fn_diff  (funcao auxiliar do log)
   NO DIA A DIA: monta o texto "campo: valor antigo -> valor novo" quando
   algo mudou (e nada quando o valor e o mesmo). Usada pelas triggers para
   o log mostrar o ANTES e o DEPOIS de cada alteracao.
   --------------------------------------------------------------------- */
CREATE FUNCTION fn_diff (p_campo VARCHAR(30), p_antes VARCHAR(100), p_depois VARCHAR(100))
RETURNS VARCHAR(250) DETERMINISTIC NO SQL
RETURN IF(p_antes <=> p_depois, NULL,
          CONCAT(p_campo, ': ', IFNULL(p_antes, '(vazio)'), ' -> ', IFNULL(p_depois, '(vazio)')))$$

/* ---------------------------------------------------------------------
   sp_registrar_log  (procedure: anotar um evento no log)
   NO DIA A DIA: usada pelo sistema para anotar o que o banco nao enxerga
   sozinho, como quem ENTROU e quem SAIU do sistema.
   Uso: CALL sp_registrar_log('LOGIN', 'usuario', id_do_login, 'login', NULL);
   [PEND. A9]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_registrar_log (IN p_operacao VARCHAR(10), IN p_tabela VARCHAR(40), IN p_registro_id BIGINT UNSIGNED, IN p_registro VARCHAR(120), IN p_detalhes VARCHAR(255))
BEGIN
    IF p_operacao NOT IN ('INSERT','UPDATE','DELETE','LOGIN','LOGOUT') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Operacao de log invalida.';
    END IF;
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, detalhes, unidade_id, registro)
    VALUES (@usuario_id, p_operacao, p_tabela, p_registro_id, LEFT(p_detalhes, 255), @unidade_id, LEFT(p_registro, 120));
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

CREATE PROCEDURE sp_login_ok (IN p_login VARCHAR(40))
BEGIN
    UPDATE usuario SET tentativas_falhas = 0, bloqueado_ate = NULL
     WHERE login = p_login AND (tentativas_falhas <> 0 OR bloqueado_ate IS NOT NULL);
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
   LOG AUTOMATICO DOS CADASTROS   [PEND. A9]
   trg_unidade_*, trg_fornecedor_*, trg_cliente_*, trg_funcionario_*,
   trg_produto_*, trg_usuario_*
   NO DIA A DIA: toda vez que alguem cria, altera ou exclui uma loja, um
   fornecedor, um cliente, um funcionario, um produto ou um login, estas
   triggers anotam no log quem fez e o que mudou (valor antigo -> novo).
   O funcionario nao precisa fazer nada, e nao da para "esquecer" de
   registrar. (Login nao e excluido: so inativado.)
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_unidade_ai AFTER INSERT ON unidade FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'unidade', NEW.id, NEW.nome,
            LEFT(CONCAT('cnpj=', IFNULL(NEW.cnpj, ''), '; endereco=', NEW.endereco, ', ', IFNULL(NEW.numero, ''), '; vale alimentacao=', IF(NEW.aceita_vale_alimentacao = 1, 'aceita', 'nao aceita')), 255), @unidade_id)$$
CREATE TRIGGER trg_unidade_au AFTER UPDATE ON unidade FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'unidade', NEW.id, NEW.nome,
            LEFT(CONCAT_WS('; ', fn_diff('nome', OLD.nome, NEW.nome), fn_diff('cnpj', OLD.cnpj, NEW.cnpj),
                                 fn_diff('endereco', OLD.endereco, NEW.endereco), fn_diff('numero', OLD.numero, NEW.numero),
                                 fn_diff('vale alimentacao', IF(OLD.aceita_vale_alimentacao = 1, 'aceita', 'nao aceita'), IF(NEW.aceita_vale_alimentacao = 1, 'aceita', 'nao aceita')),
                                 fn_diff('status', OLD.status, NEW.status)), 255), @unidade_id)$$
CREATE TRIGGER trg_unidade_ad AFTER DELETE ON unidade FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'DELETE', 'unidade', OLD.id, OLD.nome, NULL, @unidade_id)$$

CREATE TRIGGER trg_fornecedor_ai AFTER INSERT ON fornecedor FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'fornecedor', NEW.id, NEW.nome,
            LEFT(CONCAT('cnpj=', NEW.cnpj, '; telefone=', IFNULL(NEW.telefone, '')), 255), @unidade_id)$$
CREATE TRIGGER trg_fornecedor_au AFTER UPDATE ON fornecedor FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'fornecedor', NEW.id, NEW.nome,
            LEFT(CONCAT_WS('; ', fn_diff('nome', OLD.nome, NEW.nome), fn_diff('cnpj', OLD.cnpj, NEW.cnpj),
                                 fn_diff('telefone', OLD.telefone, NEW.telefone), fn_diff('status', OLD.status, NEW.status)), 255), @unidade_id)$$
CREATE TRIGGER trg_fornecedor_ad AFTER DELETE ON fornecedor FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'DELETE', 'fornecedor', OLD.id, OLD.nome, NULL, @unidade_id)$$

CREATE TRIGGER trg_cliente_ai AFTER INSERT ON cliente FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'cliente', NEW.id, NEW.nome,
            LEFT(CONCAT('cpf_cnpj=', IFNULL(NEW.cpf_cnpj, ''), '; telefone=', IFNULL(NEW.telefone, '')), 255), @unidade_id)$$
CREATE TRIGGER trg_cliente_au AFTER UPDATE ON cliente FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'cliente', NEW.id, NEW.nome,
            LEFT(CONCAT_WS('; ', fn_diff('nome', OLD.nome, NEW.nome), fn_diff('cpf_cnpj', OLD.cpf_cnpj, NEW.cpf_cnpj),
                                 fn_diff('telefone', OLD.telefone, NEW.telefone), fn_diff('status', OLD.status, NEW.status)), 255), @unidade_id)$$
CREATE TRIGGER trg_cliente_ad AFTER DELETE ON cliente FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'DELETE', 'cliente', OLD.id, OLD.nome, NULL, @unidade_id)$$

CREATE TRIGGER trg_funcionario_ai AFTER INSERT ON funcionario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'funcionario', NEW.id, NEW.nome,
            CONCAT('unidade=', IFNULL((SELECT nome FROM unidade WHERE id = NEW.unidade_id), '')), @unidade_id)$$
CREATE TRIGGER trg_funcionario_au AFTER UPDATE ON funcionario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'funcionario', NEW.id, NEW.nome,
            LEFT(CONCAT_WS('; ', fn_diff('nome', OLD.nome, NEW.nome),
                                 fn_diff('unidade', (SELECT nome FROM unidade WHERE id = OLD.unidade_id), (SELECT nome FROM unidade WHERE id = NEW.unidade_id)),
                                 fn_diff('status', OLD.status, NEW.status)), 255), @unidade_id)$$
CREATE TRIGGER trg_funcionario_ad AFTER DELETE ON funcionario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'DELETE', 'funcionario', OLD.id, OLD.nome, NULL, @unidade_id)$$

CREATE TRIGGER trg_produto_ai AFTER INSERT ON produto FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'produto', NEW.id, NEW.nome,
            LEFT(CONCAT('preco=', NEW.preco_venda, '; categoria=', NEW.categoria, '; unidade de medida=', NEW.unidade_medida, '; estoque minimo=', NEW.estoque_minimo), 255), @unidade_id)$$
CREATE TRIGGER trg_produto_au AFTER UPDATE ON produto FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'produto', NEW.id, NEW.nome,
            LEFT(CONCAT_WS('; ', fn_diff('nome', OLD.nome, NEW.nome), fn_diff('preco', OLD.preco_venda, NEW.preco_venda),
                                 fn_diff('categoria', OLD.categoria, NEW.categoria), fn_diff('unidade de medida', OLD.unidade_medida, NEW.unidade_medida),
                                 fn_diff('estoque minimo', OLD.estoque_minimo, NEW.estoque_minimo), fn_diff('status', OLD.status, NEW.status)), 255), @unidade_id)$$
CREATE TRIGGER trg_produto_ad AFTER DELETE ON produto FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'DELETE', 'produto', OLD.id, OLD.nome, NULL, @unidade_id)$$

CREATE TRIGGER trg_usuario_ai AFTER INSERT ON usuario FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'usuario', NEW.id, NEW.login,
            LEFT(CONCAT('perfil=', NEW.perfil, '; unidade=', IFNULL((SELECT nome FROM unidade WHERE id = NEW.unidade_id), '(nenhuma)')), 255), @unidade_id)$$
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
   trg_entrada_bi / trg_entrada_ai  (chegada de mercadoria)
   NO DIA A DIA: o fornecedor entregou 100 coxinhas e o funcionario registrou
   a entrada. A primeira trigger confere se quem recebeu e funcionario DA
   MESMA LOJA; a segunda soma 100 no saldo da loja, anota no extrato do
   estoque e no log. Ninguem precisa atualizar o estoque a mao.
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
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'entrada_estoque', NEW.id,
            (SELECT nome FROM produto WHERE id = NEW.produto_id),
            LEFT(CONCAT('quantidade=', NEW.quantidade, '; custo=', NEW.custo_unitario,
                        '; fornecedor=', IFNULL((SELECT nome FROM fornecedor WHERE id = NEW.fornecedor_id), '(nenhum)'),
                        '; responsavel=', (SELECT nome FROM funcionario WHERE id = NEW.responsavel_funcionario_id)), 255),
            @unidade_id);
END$$

/* ---------------------------------------------------------------------
   trg_perda_bi / trg_perda_ai  (registro de perdas)
   NO DIA A DIA: o funcionario joga fora 10 salgados vencidos e registra a
   perda. A primeira trigger confere se o responsavel e da mesma loja e se
   existem mesmo 10 no estoque (nao da para perder mais do que se tem); a
   segunda tira 10 do saldo e anota no extrato e no log.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_perda_bi BEFORE INSERT ON perda FOR EACH ROW
BEGIN
    DECLARE v_saldo DECIMAL(10,2);
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
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'perda', NEW.id,
            (SELECT nome FROM produto WHERE id = NEW.produto_id),
            LEFT(CONCAT('quantidade=', NEW.quantidade, '; motivo=', NEW.motivo, '; data=', NEW.data,
                        '; responsavel=', (SELECT nome FROM funcionario WHERE id = NEW.responsavel_funcionario_id)), 255),
            @unidade_id);
END$$

/* ---------------------------------------------------------------------
   trg_inventario_bi / trg_inventario_bu / trg_item_inv_bi / trg_item_inv_bu
   NO DIA A DIA: a contagem so aceita funcionario DA MESMA LOJA e, depois de
   efetivada (estoque ajustado), fica lacrada: ninguem muda a contagem para
   esconder uma diferenca.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_inventario_bi BEFORE INSERT ON inventario FOR EACH ROW
BEGIN
    IF fn_funcionario_ok(NEW.responsavel_funcionario_id, NEW.unidade_id) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Funcionario invalido: inexistente, inativo ou de outra unidade.';
    END IF;
END$$
CREATE TRIGGER trg_inventario_bu BEFORE UPDATE ON inventario FOR EACH ROW
BEGIN
    IF OLD.efetivado_em IS NOT NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Inventario efetivado nao pode ser alterado.';
    END IF;
END$$
CREATE TRIGGER trg_item_inv_bi BEFORE INSERT ON item_inventario FOR EACH ROW
BEGIN
    IF (SELECT efetivado_em FROM inventario WHERE id = NEW.inventario_id) IS NOT NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Inventario efetivado nao pode ser alterado.';
    END IF;
END$$
CREATE TRIGGER trg_item_inv_bu BEFORE UPDATE ON item_inventario FOR EACH ROW
BEGIN
    IF (SELECT efetivado_em FROM inventario WHERE id = OLD.inventario_id) IS NOT NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Inventario efetivado nao pode ser alterado.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_efetivar_inventario  (procedure: aplicar a contagem ao estoque)
   NO DIA A DIA: o funcionario terminou de contar e aperta "efetivar". Para
   cada produto contado, o saldo do estoque passa a ser o que foi contado e a
   diferenca vira um movimento do tipo "Ajuste" no extrato. Todo item com
   diferenca precisa de motivo. Se o saldo mudou enquanto contavam (alguem
   vendeu, por exemplo), o sistema AVISA e so continua se p_forcar = 1.
   Tudo ou nada: se algo falhar, o estoque nao muda. Um inventario so pode
   ser efetivado uma vez.
   Uso: CALL sp_efetivar_inventario(id_inventario, 0);   -- 1 para confirmar
   [PEND. A3]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_efetivar_inventario (IN p_inventario INT UNSIGNED, IN p_forcar TINYINT)
BEGIN
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_usuario INT UNSIGNED;
    DECLARE v_efetivado DATETIME;
    DECLARE v_alterados INT;
    DECLARE v_ajustes INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;

    SELECT unidade_id, usuario_id, efetivado_em INTO v_unidade, v_usuario, v_efetivado
      FROM inventario WHERE id = p_inventario;
    IF v_unidade IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Inventario inexistente.';
    END IF;
    IF v_efetivado IS NOT NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Inventario ja efetivado.';
    END IF;
    IF NOT EXISTS (SELECT 1 FROM item_inventario WHERE inventario_id = p_inventario AND quantidade_contada IS NOT NULL) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nenhum item foi contado.';
    END IF;
    IF EXISTS (SELECT 1 FROM item_inventario
                WHERE inventario_id = p_inventario AND quantidade_contada IS NOT NULL
                  AND quantidade_contada <> saldo_sistema AND (motivo IS NULL OR TRIM(motivo) = '')) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Informe o motivo de cada item com diferenca.';
    END IF;

    SELECT COUNT(*) INTO v_alterados
      FROM item_inventario i
      LEFT JOIN estoque e ON e.unidade_id = v_unidade AND e.produto_id = i.produto_id
     WHERE i.inventario_id = p_inventario AND i.quantidade_contada IS NOT NULL
       AND COALESCE(e.quantidade, 0) <> i.saldo_sistema;
    IF v_alterados > 0 AND IFNULL(p_forcar, 0) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Saldo alterado durante a contagem: confirme para efetivar.';
    END IF;

    START TRANSACTION;
    -- 1) extrato: a diferenca entre o contado e o saldo ATUAL de cada produto
    INSERT INTO movimentacao_estoque (unidade_id, produto_id, tipo, quantidade, usuario_id)
    SELECT v_unidade, i.produto_id, 'Ajuste', i.quantidade_contada - COALESCE(e.quantidade, 0), v_usuario
      FROM item_inventario i
      LEFT JOIN estoque e ON e.unidade_id = v_unidade AND e.produto_id = i.produto_id
     WHERE i.inventario_id = p_inventario AND i.quantidade_contada IS NOT NULL
       AND i.quantidade_contada <> COALESCE(e.quantidade, 0);
    SET v_ajustes = ROW_COUNT();
    -- 2) saldo: passa a ser o contado
    INSERT INTO estoque (unidade_id, produto_id, quantidade)
    SELECT v_unidade, produto_id, quantidade_contada
      FROM item_inventario WHERE inventario_id = p_inventario AND quantidade_contada IS NOT NULL
    ON DUPLICATE KEY UPDATE quantidade = VALUES(quantidade);
    -- 3) lacra a contagem
    UPDATE inventario SET efetivado_em = NOW() WHERE id = p_inventario;
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'inventario', p_inventario, CONCAT('Inventario ', p_inventario),
            CONCAT('efetivado; itens ajustados=', v_ajustes), @unidade_id);
    COMMIT;
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
   trg_caixa_ai / trg_caixa_au  (log do caixa)
   NO DIA A DIA: anota no log quem ABRIU o caixa e quem o FECHOU (com os
   valores conferidos). A correcao de um caixa aberto e anotada por
   sp_editar_caixa, com motivo e autorizacao.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_caixa_ai AFTER INSERT ON caixa FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'caixa', NEW.id, CONCAT('Caixa ', NEW.id),
            LEFT(CONCAT('aberto; responsavel=', (SELECT nome FROM funcionario WHERE id = NEW.responsavel_funcionario_id),
                        '; valor inicial=', NEW.valor_inicial), 255), @unidade_id)$$
CREATE TRIGGER trg_caixa_au AFTER UPDATE ON caixa FOR EACH ROW
BEGIN
    IF NEW.status <> OLD.status THEN
        INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
        VALUES (@usuario_id, 'UPDATE', 'caixa', NEW.id, CONCAT('Caixa ', NEW.id),
                LEFT(CONCAT('status: ', OLD.status, ' -> ', NEW.status,
                            '; dinheiro contado=', IFNULL(NEW.valor_contado_dinheiro, ''),
                            '; maquininha=', IFNULL(NEW.valor_contado_maquininha, ''),
                            '; diferenca=', IFNULL(NEW.diferenca, '')), 255), @unidade_id);
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
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_abrir_caixa (IN p_unidade INT UNSIGNED, IN p_funcionario INT UNSIGNED, IN p_inicial DECIMAL(10,2))
BEGIN
    DECLARE EXIT HANDLER FOR 1062
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Ja existe um caixa aberto nesta unidade.';
    IF p_inicial IS NULL OR p_inicial < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'O valor inicial nao pode ser negativo.';
    END IF;
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
   sp_editar_caixa  (procedure: corrigir o caixa ainda ABERTO)
   NO DIA A DIA: o funcionario abriu o caixa com o troco errado ou escolheu o
   responsavel errado. So um Gerente ou o Administrador autoriza a correcao
   (o autorizador digita o login dele na tela), e e obrigatorio dizer o
   motivo. So vale para caixa aberto, da loja de trabalho. O log guarda o
   antes e o depois, o motivo e quem autorizou.
   Uso: CALL sp_editar_caixa(id_caixa, novo_valor_inicial, id_funcionario, 'motivo', id_autorizador);
   [PEND. A4]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_editar_caixa (IN p_caixa INT UNSIGNED, IN p_valor_inicial DECIMAL(10,2), IN p_funcionario INT UNSIGNED, IN p_motivo VARCHAR(200), IN p_autorizador INT UNSIGNED)
BEGIN
    DECLARE v_status VARCHAR(10);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_valor DECIMAL(10,2);
    DECLARE v_func INT UNSIGNED;
    DECLARE v_perfil VARCHAR(20);
    DECLARE v_login VARCHAR(40);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;

    SELECT status, unidade_id, valor_inicial, responsavel_funcionario_id
      INTO v_status, v_unidade, v_valor, v_func
      FROM caixa WHERE id = p_caixa;
    IF v_status IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa inexistente.';
    END IF;
    IF v_status <> 'Aberto' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Somente um caixa aberto pode ser editado.';
    END IF;
    IF @unidade_id IS NOT NULL AND @unidade_id <> v_unidade THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Este caixa pertence a outra unidade.';
    END IF;
    IF p_valor_inicial IS NULL OR p_valor_inicial < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'O valor inicial nao pode ser negativo.';
    END IF;
    IF p_funcionario IS NULL OR fn_funcionario_ok(p_funcionario, v_unidade) = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Funcionario invalido: inexistente, inativo ou de outra unidade.';
    END IF;
    IF p_motivo IS NULL OR TRIM(p_motivo) = '' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Informe o motivo da edicao.';
    END IF;
    SELECT perfil, login INTO v_perfil, v_login FROM usuario WHERE id = p_autorizador AND status = 'Ativo';
    IF v_perfil IS NULL OR v_perfil = 'Atendente' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Apenas Administrador ou Gerente pode autorizar.';
    END IF;
    IF p_valor_inicial = v_valor AND p_funcionario = v_func THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nenhuma alteracao foi feita.';
    END IF;

    START TRANSACTION;
    UPDATE caixa SET valor_inicial = p_valor_inicial, responsavel_funcionario_id = p_funcionario
     WHERE id = p_caixa;
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'UPDATE', 'caixa', p_caixa, CONCAT('Caixa ', p_caixa),
            LEFT(CONCAT_WS('; ',
                 fn_diff('valor inicial', v_valor, p_valor_inicial),
                 fn_diff('responsavel', (SELECT nome FROM funcionario WHERE id = v_func), (SELECT nome FROM funcionario WHERE id = p_funcionario)),
                 CONCAT('motivo: ', TRIM(p_motivo)),
                 CONCAT('autorizado por: ', v_login)), 255), @unidade_id);
    COMMIT;
END$$

/* ---------------------------------------------------------------------
   sp_fechar_caixa  (procedure: fechar o caixa)
   NO DIA A DIA: no fim do expediente o responsavel conta o dinheiro da gaveta
   e o total da maquininha e informa os dois valores. O sistema NAO fecha se
   ainda houver comanda aberta na loja. Soma o que entrou em DINHEIRO
   (vendas, ja descontado o troco, mais abatimentos de notinha), soma o
   troco inicial e calcula a DIFERENCA do dinheiro:
       diferenca = dinheiro contado - (troco inicial + dinheiro que entrou)
   Diferenca negativa = faltou dinheiro; positiva = sobrou.
   O valor da maquininha fica guardado para conferir com vw_caixa_totais
   (Credito + Debito + ValeAlimentacao).
   Uso: CALL sp_fechar_caixa(id_caixa, dinheiro_contado, maquininha_contado);
   [PEND. A7]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_fechar_caixa (IN p_caixa INT UNSIGNED, IN p_contado DECIMAL(10,2), IN p_maquininha DECIMAL(10,2))
BEGIN
    DECLARE v_status VARCHAR(10);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_din DECIMAL(10,2);
    SELECT status, unidade_id INTO v_status, v_unidade FROM caixa WHERE id = p_caixa;
    IF v_status IS NULL OR v_status <> 'Aberto' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Caixa inexistente ou ja fechado.';
    END IF;
    IF p_contado IS NULL OR p_contado < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Informe o valor contado em dinheiro (zero ou maior).';
    END IF;
    IF p_maquininha IS NOT NULL AND p_maquininha < 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'O valor da maquininha nao pode ser negativo.';
    END IF;
    IF EXISTS (SELECT 1 FROM comanda WHERE unidade_id = v_unidade AND status = 'Aberta') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Fechamento bloqueado: existem comandas abertas.';
    END IF;
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
   trg_item_comanda_bi  (nao vende o que nao tem)
   NO DIA A DIA: o cliente pede 5 pasteis e so ha 3 no estoque: o sistema
   avisa "Estoque insuficiente" e o pedido nao e lancado. Tambem recusa
   pedido em comanda que ja foi cobrada ou cancelada e produto inativo.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_item_comanda_bi BEFORE INSERT ON item_comanda FOR EACH ROW
BEGIN
    DECLARE v_saldo DECIMAL(10,2);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_status VARCHAR(20);
    SELECT unidade_id, status INTO v_unidade, v_status FROM comanda WHERE id = NEW.comanda_id;
    IF v_status IS NULL OR v_status <> 'Aberta' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Comanda inexistente ou nao esta aberta.';
    END IF;
    IF (SELECT status FROM produto WHERE id = NEW.produto_id) <> 'Ativo' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Produto inativo.';
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
   trg_pag_comanda_bi  (regras do pagamento da comanda aberta)
   NO DIA A DIA: so aceita pagamento de comanda que esta aberta. E "Notinha"
   (fiado) so e aceita quando a comanda tem um CLIENTE CADASTRADO: ninguem
   anota fiado para um desconhecido.
   [PEND. A1]
   --------------------------------------------------------------------- */
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

/* ---------------------------------------------------------------------
   trg_comanda_bu  (cliente da comanda com notinha nao muda)
   NO DIA A DIA: se parte da conta ja foi marcada na notinha de um cliente,
   ninguem pode trocar o cliente da comanda (a divida e dele).
   [PEND. A1]
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_comanda_bu BEFORE UPDATE ON comanda FOR EACH ROW
BEGIN
    IF NOT (OLD.cliente_id <=> NEW.cliente_id)
       AND EXISTS (SELECT 1 FROM pagamento_comanda WHERE comanda_id = OLD.id AND forma_pagamento = 'Notinha') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nao e possivel trocar o cliente: a comanda ja possui pagamento em Notinha.';
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_comanda_au  (log da comanda)
   NO DIA A DIA: anota no log quando uma comanda e cobrada (Fechada) ou
   cancelada.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_comanda_au AFTER UPDATE ON comanda FOR EACH ROW
BEGIN
    IF NEW.status <> OLD.status THEN
        INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
        VALUES (@usuario_id, 'UPDATE', 'comanda', NEW.id, CONCAT('Comanda ', NEW.id),
                CONCAT('status: ', OLD.status, ' -> ', NEW.status, '; mesa=', NEW.numero_mesa), @unidade_id);
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_cancelar_comanda  (procedure: cancelar uma comanda ainda aberta)
   NO DIA A DIA: o cliente desistiu antes de pagar. Os produtos voltam para o
   estoque (anotado no extrato como Estorno) e a comanda fica marcada como
   "Cancelada" (nao some). Comanda que ja recebeu algum pagamento nao se
   cancela. Comanda ja cobrada nao se cancela aqui: use sp_cancelar_venda.
   Uso: CALL sp_cancelar_comanda(id_comanda, id_usuario);
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
    IF EXISTS (SELECT 1 FROM pagamento_comanda WHERE comanda_id = p_comanda) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cancelamento bloqueado: esta comanda ja possui pagamentos.';
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
   caixa do dia e que o fechamento bate. Vale para a cobranca da comanda
   (sp_cobrar_comanda).
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
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_item_venda_ai AFTER INSERT ON item_venda FOR EACH ROW
    UPDATE venda SET valor_total = valor_total + ROUND(NEW.quantidade * NEW.preco_unitario, 2) WHERE id = NEW.venda_id$$

/* ---------------------------------------------------------------------
   trg_pagamento_bi / trg_pagamento_ai  (regras do pagamento da venda)
   NO DIA A DIA: so aceita pagamento de venda que existe e esta concluida. A
   "Notinha" (fiado) so e aceita quando a venda tem um CLIENTE CADASTRADO.
   Quando o pagamento e em Notinha, a segunda trigger cria sozinha a linha da
   divida do cliente naquela loja: ninguem precisa lembrar de anotar.
   [PEND. B3]
   --------------------------------------------------------------------- */
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
CREATE TRIGGER trg_pagamento_ai AFTER INSERT ON pagamento_venda FOR EACH ROW
BEGIN
    DECLARE v_cliente INT UNSIGNED;
    DECLARE v_unidade INT UNSIGNED;
    IF NEW.forma_pagamento = 'Notinha' THEN
        SELECT cliente_id, unidade_id INTO v_cliente, v_unidade FROM venda WHERE id = NEW.venda_id;
        INSERT INTO notinha (cliente_id, venda_id, unidade_id, valor)
        VALUES (v_cliente, NEW.venda_id, v_unidade, NEW.valor);
    END IF;
END$$

/* ---------------------------------------------------------------------
   trg_abatimento_bi / trg_abatimento_ai  (abatimento de notinha)
   NO DIA A DIA: quando o cliente volta para pagar a notinha, o dinheiro entra
   no caixa; entao o caixa precisa estar aberto e ser da mesma loja, igual
   a uma venda. A segunda trigger anota o recebimento no log.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_abatimento_bi BEFORE INSERT ON abatimento_notinha FOR EACH ROW
BEGIN
    IF (SELECT COUNT(*) FROM caixa
         WHERE id = NEW.caixa_id AND unidade_id = NEW.unidade_id AND status = 'Aberto') = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Nao ha caixa aberto nesta unidade.';
    END IF;
END$$
CREATE TRIGGER trg_abatimento_ai AFTER INSERT ON abatimento_notinha FOR EACH ROW
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'abatimento_notinha', NEW.id,
            (SELECT nome FROM cliente WHERE id = NEW.cliente_id),
            LEFT(CONCAT('valor=', NEW.valor, '; forma=', NEW.forma_pagamento, '; caixa=', NEW.caixa_id), 255), @unidade_id)$$

/* ---------------------------------------------------------------------
   sp_finalizar_venda  (procedure: confere os pagamentos e calcula o troco)
   NO DIA A DIA: e o momento do "pagar". Depois que os pagamentos foram
   lancados, o sistema confere se eles cobrem o total e calcula o TROCO na
   hora (substitui a calculadora). O troco so pode sair do dinheiro: se o
   cliente pagou no Pix mais do que devia, o sistema recusa.
   Ex.: total R$ 30,00, cliente deu R$ 50,00 em dinheiro -> troco R$ 20,00.
   (Chamada por sp_cobrar_comanda; nao precisa ser chamada pelo aplicativo.)
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
   sp_cobrar_comanda  (procedure: cobrar a comanda e gerar a venda)
   NO DIA A DIA: o cliente pediu a conta e acabou de pagar o que faltava. O
   sistema cria o cupom (venda) com a mesa e o cliente da comanda, copia os
   itens e os pagamentos, marca a comanda como "Fechada", gera a notinha (se
   parte da conta ficou no fiado), confere se os pagamentos cobrem o total e
   calcula o troco. NAO baixa o estoque de novo. Tudo ou nada: se faltar
   pagamento ou der qualquer erro, nada e gravado e a comanda continua
   aberta. Devolve o id da venda criada. Este procedimento cuida da propria
   transacao: o aplicativo nao precisa abrir uma.
   Uso: CALL sp_cobrar_comanda(id_comanda, id_caixa, id_usuario);
   [PEND. A1]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_cobrar_comanda (IN p_comanda INT UNSIGNED, IN p_caixa INT UNSIGNED, IN p_usuario INT UNSIGNED)
BEGIN
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_mesa INT;
    DECLARE v_cliente INT UNSIGNED;
    DECLARE v_status VARCHAR(20);
    DECLARE v_venda INT UNSIGNED;
    DECLARE v_total DECIMAL(10,2);
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;

    IF NOT EXISTS (SELECT 1 FROM usuario WHERE id = p_usuario AND status = 'Ativo') THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Usuario invalido ou inativo.';
    END IF;

    START TRANSACTION;
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

    INSERT INTO pagamento_venda (venda_id, forma_pagamento, valor)
    SELECT v_venda, forma_pagamento, valor
      FROM pagamento_comanda WHERE comanda_id = p_comanda ORDER BY id;

    UPDATE comanda SET status = 'Fechada' WHERE id = p_comanda;
    CALL sp_finalizar_venda(v_venda);

    SELECT valor_total INTO v_total FROM venda WHERE id = v_venda;
    INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
    VALUES (@usuario_id, 'INSERT', 'venda', v_venda, CONCAT('Venda ', v_venda),
            LEFT(CONCAT('total=', v_total, '; ', IF(v_mesa = 0, 'Balcao', CONCAT('Mesa ', v_mesa)),
                        '; caixa=', p_caixa,
                        IF(v_cliente IS NULL, '', CONCAT('; cliente=', (SELECT nome FROM cliente WHERE id = v_cliente)))), 255),
            @unidade_id);
    COMMIT;
    SELECT v_venda AS venda_id;
END$$

/* ---------------------------------------------------------------------
   trg_venda_au  (log do cancelamento)
   NO DIA A DIA: anota no log quem cancelou a venda, o motivo e o valor. So
   anota quando o STATUS muda, para nao encher o log com os recalculos de
   total e troco.
   --------------------------------------------------------------------- */
CREATE TRIGGER trg_venda_au AFTER UPDATE ON venda FOR EACH ROW
BEGIN
    IF NEW.status <> OLD.status THEN
        INSERT INTO log_operacao (usuario_id, operacao, tabela, registro_id, registro, detalhes, unidade_id)
        VALUES (@usuario_id, 'UPDATE', 'venda', NEW.id, CONCAT('Venda ', NEW.id),
                LEFT(CONCAT('status: ', OLD.status, ' -> ', NEW.status, '; total=', NEW.valor_total,
                            '; motivo: ', IFNULL(NEW.motivo_cancelamento, ''),
                            '; autorizado por: ', IFNULL((SELECT login FROM usuario WHERE id = NEW.cancelado_por), '')), 255),
                @unidade_id);
    END IF;
END$$

/* ---------------------------------------------------------------------
   sp_cancelar_venda  (procedure: cancelar uma venda ja cobrada)
   NO DIA A DIA: o atendente digitou errado ou o cliente desistiu depois de
   pagar. Somente Gerente ou Administrador consegue cancelar, e e obrigatorio
   informar o motivo. So da para cancelar enquanto o caixa daquela venda
   ainda esta ABERTO e se a venda e da loja de trabalho. O sistema devolve os
   produtos ao estoque (Estorno) e mantem a venda registrada como
   "Cancelada" (nao some), com quem cancelou e quando. Se a venda gerou
   NOTINHA e o cliente ja abateu a divida alem do que restaria NAQUELA LOJA,
   o cancelamento e recusado (RN46). Se algo falhar no meio, tudo e desfeito.
   Uso: CALL sp_cancelar_venda(id_venda, id_usuario, 'motivo');
   [PEND. A5 e A6]
   --------------------------------------------------------------------- */
CREATE PROCEDURE sp_cancelar_venda (IN p_venda INT UNSIGNED, IN p_usuario INT UNSIGNED, IN p_motivo VARCHAR(200))
BEGIN
    DECLARE v_perfil VARCHAR(20);
    DECLARE v_status VARCHAR(20);
    DECLARE v_unidade INT UNSIGNED;
    DECLARE v_cliente INT UNSIGNED;
    DECLARE v_caixa INT UNSIGNED;
    DECLARE v_status_caixa VARCHAR(10);
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
    SELECT status, unidade_id, cliente_id, caixa_id INTO v_status, v_unidade, v_cliente, v_caixa
      FROM venda WHERE id = p_venda FOR UPDATE;
    IF v_status IS NULL OR v_status <> 'Concluida' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Venda inexistente ou ja cancelada.';
    END IF;
    IF @unidade_id IS NOT NULL AND @unidade_id <> v_unidade THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cancelamento bloqueado: esta venda pertence a outra unidade.';
    END IF;
    SELECT status INTO v_status_caixa FROM caixa WHERE id = v_caixa;
    IF v_status_caixa <> 'Aberto' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cancelamento bloqueado: o caixa desta venda ja foi fechado.';
    END IF;

    -- RN46: venda com notinha nao pode ser cancelada se a divida do cliente NESTA LOJA ja foi abatida
    IF EXISTS (SELECT 1 FROM notinha WHERE venda_id = p_venda) THEN
        SELECT COALESCE(SUM(n.valor), 0) INTO v_outras
          FROM notinha n JOIN venda v ON v.id = n.venda_id AND v.status = 'Concluida'
         WHERE n.cliente_id = v_cliente AND n.unidade_id = v_unidade AND n.venda_id <> p_venda;
        SELECT COALESCE(SUM(valor), 0) INTO v_abatido FROM abatimento_notinha
         WHERE cliente_id = v_cliente AND unidade_id = v_unidade;
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
   O custo da perda e o custo da ultima entrada daquele produto ate a data
   da perda: da propria loja; se a loja nunca recebeu o produto, o da ultima
   entrada em qualquer loja; sem nenhuma entrada, 0 (e a perda e contada em
   perdas_sem_custo).
   Informativos: recebido (sem Notinha), vendido_notinha, abatimentos do
   periodo e notinha_a_receber (= vendido em notinha - abatimentos).
   Uso: CALL sp_dre(NULL, '2026-11-01', '2026-11-30');   -- todas as lojas
        CALL sp_dre(1,    '2026-11-01', '2026-11-30');   -- so a loja 1
   [PEND. B2]
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
           COALESCE(p.perdas, 0)   AS perdas,
           COALESCE(p.sem_custo, 0) AS perdas_sem_custo,
           COALESCE(pg.recebido, 0)    AS recebido,
           COALESCE(pg.em_notinha, 0)  AS vendido_notinha,
           COALESCE(a.abatimentos, 0)  AS abatimentos,
           COALESCE(pg.em_notinha, 0) - COALESCE(a.abatimentos, 0) AS notinha_a_receber
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
      LEFT JOIN (SELECT x.unidade_id,
                        SUM(x.quantidade * COALESCE(x.custo_loja, x.custo_rede, 0)) AS perdas,
                        SUM(x.custo_loja IS NULL AND x.custo_rede IS NULL) AS sem_custo
                   FROM (SELECT pe.unidade_id, pe.quantidade,
                                (SELECT en.custo_unitario FROM entrada_estoque en
                                  WHERE en.unidade_id = pe.unidade_id AND en.produto_id = pe.produto_id
                                    AND en.data_entrada <= pe.data
                                  ORDER BY en.data_entrada DESC, en.id DESC LIMIT 1) AS custo_loja,
                                (SELECT en2.custo_unitario FROM entrada_estoque en2
                                  WHERE en2.produto_id = pe.produto_id
                                    AND en2.data_entrada <= pe.data
                                  ORDER BY en2.data_entrada DESC, en2.id DESC LIMIT 1) AS custo_rede
                           FROM perda pe
                          WHERE pe.data BETWEEN p_data_inicio AND p_data_fim) x
                  GROUP BY x.unidade_id) p ON p.unidade_id = u.id
      LEFT JOIN (SELECT unidade_id,
                        SUM(IF(forma_pagamento = 'Notinha', 0, valor_liquido)) AS recebido,
                        SUM(IF(forma_pagamento = 'Notinha', valor_liquido, 0)) AS em_notinha
                   FROM vw_pagamento_liquido
                  WHERE dia BETWEEN p_data_inicio AND p_data_fim
                  GROUP BY unidade_id) pg ON pg.unidade_id = u.id
      LEFT JOIN (SELECT unidade_id, SUM(valor) AS abatimentos
                   FROM abatimento_notinha
                  WHERE data_hora >= p_data_inicio AND data_hora < DATE_ADD(p_data_fim, INTERVAL 1 DAY)
                  GROUP BY unidade_id) a ON a.unidade_id = u.id
     WHERE p_unidade_id IS NULL OR u.id = p_unidade_id
     ORDER BY u.id;
END$$

DELIMITER ;

/* =====================================================================
   VIEWS (consultas prontas para os relatorios)
   No C#, basta fazer SELECT * FROM vw_... WHERE unidade_id = ... e filtrar.
   ===================================================================== */

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
   vw_estoque_atual
   NO DIA A DIA: a tela "como esta o estoque agora". Mostra o saldo de cada
   produto em cada loja (produto que nunca teve entrada aparece com saldo 0)
   e a coluna CRITICO = 1 quando o saldo chegou ao minimo ou ficou abaixo
   dele, o alerta de que e hora de pedir mais ao fornecedor.
   [PEND. B1] critico agora e saldo <= minimo.
   --------------------------------------------------------------------- */
CREATE VIEW vw_estoque_atual AS
SELECT u.id AS unidade_id, p.id AS produto_id, p.nome AS produto, COALESCE(e.quantidade, 0) AS saldo,
       p.estoque_minimo, (COALESCE(e.quantidade, 0) <= p.estoque_minimo) AS critico,
       p.categoria, p.unidade_medida, p.preco_venda, p.status AS status_produto
  FROM unidade u
  CROSS JOIN produto p
  LEFT JOIN estoque e ON e.unidade_id = u.id AND e.produto_id = p.id;

/* ---------------------------------------------------------------------
   vw_vendas_diarias
   NO DIA A DIA: o relatorio de faturamento. Mostra, por loja e por dia, quantas
   vendas foram feitas e quanto entrou, separado por forma de pagamento
   (Dinheiro, Pix, Credito, Debito, ValeAlimentacao e Notinha - esta ultima e "a receber").
   Ignora vendas canceladas. Responde "quanto vendemos ontem no Centro?".
   Atencao: qtd_vendas conta cada venda uma vez POR FORMA de pagamento; para o
   total de vendas do dia, conte as linhas de venda.
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
   em Dinheiro, Pix, Credito, Debito, ValeAlimentacao e Notinha, separando o que veio de
   VENDAS e o que veio de ABATIMENTOS de notinha. Serve para conferir com a
   maquininha (Credito + Debito + ValeAlimentacao) e com o extrato do Pix.
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
   NO DIA A DIA: "quanto cada cliente esta devendo EM CADA LOJA". Soma as
   notinhas (de vendas nao canceladas) e subtrai o que ja foi abatido. So
   aparece o cliente que ja teve movimento de notinha na loja.
   [PEND. A6] agora por cliente e por unidade.
   --------------------------------------------------------------------- */
CREATE VIEW vw_notinha_saldo AS
SELECT c.id AS cliente_id, c.nome AS cliente, m.unidade_id,
       COALESCE(n.total, 0) AS total_notinhas, COALESCE(a.total, 0) AS total_abatido,
       COALESCE(n.total, 0) - COALESCE(a.total, 0) AS saldo_devedor
  FROM cliente c
  JOIN (SELECT cliente_id, unidade_id FROM notinha
        UNION
        SELECT cliente_id, unidade_id FROM abatimento_notinha) m ON m.cliente_id = c.id
  LEFT JOIN (SELECT n.cliente_id, n.unidade_id, SUM(n.valor) AS total
               FROM notinha n JOIN venda v ON v.id = n.venda_id AND v.status = 'Concluida'
              GROUP BY n.cliente_id, n.unidade_id) n ON n.cliente_id = c.id AND n.unidade_id = m.unidade_id
  LEFT JOIN (SELECT cliente_id, unidade_id, SUM(valor) AS total
               FROM abatimento_notinha GROUP BY cliente_id, unidade_id) a ON a.cliente_id = c.id AND a.unidade_id = m.unidade_id;

/* =====================================================================
   SEGURANCA - usuario do banco usado pelo sistema
   NO DIA A DIA: o programa em C# NAO deve entrar no banco como "root" (dono
   de tudo). Este usuario so pode ler dados, fazer cadastros, lancar comandas
   e pagamentos, registrar entradas e perdas, e chamar as procedures. Nao pode
   mexer direto no estoque, no caixa, no log, nem criar vendas "na mao":
   venda so nasce por sp_cobrar_comanda, e o estoque so muda pelas triggers
   e procedures acima. So os cadastros (loja, produto, fornecedor,
   funcionario, cliente) podem ser excluidos, e o proprio banco recusa
   quando ha historico ligado a eles.
   Assim, mesmo que a senha do sistema vaze, o estrago possivel e pequeno.
   TROQUE A SENHA ANTES DE USAR DE VERDADE.
   Esta conta so aceita conexoes da PROPRIA maquina (localhost). Para o
   aplicativo acessar o banco de outro computador, crie tambem a conta para
   aquele endereco (ou '%'), repetindo o CREATE USER e os GRANT abaixo.
   [PEND. A8, A9 e B5]
   ===================================================================== */
DROP USER IF EXISTS 'app_salgados'@'localhost';
DROP USER IF EXISTS 'app_salgados'@'%';   -- remove a conta da versao anterior do script
CREATE USER 'app_salgados'@'localhost' IDENTIFIED BY 'TROQUE_ESTA_SENHA_FORTE';
GRANT SELECT  ON salgados_cia.* TO 'app_salgados'@'localhost';
-- cadastros: podem ser criados, alterados e (sem historico) excluidos
GRANT INSERT, UPDATE, DELETE ON salgados_cia.unidade     TO 'app_salgados'@'localhost';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.produto     TO 'app_salgados'@'localhost';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.fornecedor  TO 'app_salgados'@'localhost';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.funcionario TO 'app_salgados'@'localhost';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.cliente     TO 'app_salgados'@'localhost';
-- logins: criados e inativados, nunca excluidos
GRANT INSERT, UPDATE ON salgados_cia.usuario     TO 'app_salgados'@'localhost';
-- comanda: o app cria e pode trocar mesa/cliente; o STATUS so muda pelas procedures
GRANT INSERT ON salgados_cia.comanda             TO 'app_salgados'@'localhost';
GRANT UPDATE (numero_mesa, cliente_id) ON salgados_cia.comanda TO 'app_salgados'@'localhost';
-- historico financeiro/estoque: so INSERT (nada de UPDATE/DELETE)
-- (pagamento_venda e notinha nascem por sp_cobrar_comanda e triggers)
GRANT INSERT ON salgados_cia.item_comanda        TO 'app_salgados'@'localhost';
GRANT INSERT ON salgados_cia.pagamento_comanda   TO 'app_salgados'@'localhost';
GRANT INSERT ON salgados_cia.abatimento_notinha  TO 'app_salgados'@'localhost';
GRANT INSERT ON salgados_cia.entrada_estoque     TO 'app_salgados'@'localhost';
GRANT INSERT ON salgados_cia.perda               TO 'app_salgados'@'localhost';
-- inventario: a contagem pode ser corrigida enquanto e preenchida (ate ser efetivada)
GRANT INSERT, UPDATE ON salgados_cia.inventario      TO 'app_salgados'@'localhost';
GRANT INSERT, UPDATE ON salgados_cia.item_inventario TO 'app_salgados'@'localhost';
GRANT EXECUTE ON salgados_cia.*                  TO 'app_salgados'@'localhost';
FLUSH PRIVILEGES;

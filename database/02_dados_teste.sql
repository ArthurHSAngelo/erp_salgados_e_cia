/* =====================================================================
   ERP SALGADOS & CIA - Dados ficticios de teste
   Rodar DEPOIS do 01_schema.sql (que apaga e recria o banco).

   Logins de teste (todos com a senha  123):
     admin     Administrador
     gerente1  Gerente
     gerente2  Gerente
     centro    Atendente da Lanchonete Centro
     norte     Atendente da Lanchonete Norte

   As senhas ficam guardadas como hash PBKDF2 ("sal:hash"), gerado pelo
   proprio sistema (classe ServicoSenha). Para trocar uma senha, gere um novo
   hash com o gerador (database/gerar_hash.ps1) e atualize a coluna senha_hash.

   Os saldos iniciais de estoque sao gravados direto (como "saldo de abertura"),
   sem entrada de estoque, para nao criar despesas no DRE.
   ===================================================================== */
USE salgados_cia;

INSERT INTO unidade (nome, cnpj, endereco, numero, aceita_vale_alimentacao) VALUES
    ('Lanchonete Centro', '11.222.333/0001-44', 'Rua Principal',  '100', 1),
    ('Lanchonete Norte',  '22.333.444/0001-55', 'Avenida Norte',  '200', 0);

INSERT INTO usuario (login, senha_hash, perfil, unidade_id) VALUES
    ('admin',    'htKLj4yluy/Xlx5OIX1O9Q==:UtjSYy3Eh1ThIrqvNiKnbs8+ecKfAW5pX5Zavhm7qjU=', 'Administrador', NULL),
    ('gerente1', 'gm8pcwF8fdW/lWHdHxK+sA==:aV3yOtCs1Hj4cOngLyLG96MC3rwbvDfz0iTCbqpZ6Zk=', 'Gerente',       NULL),
    ('gerente2', '4jrsZVB3huJLGTKUC2TSGQ==:aCd37FEMjh6OH+wY1LLQsY/3cAFOdm1spVI9sNuHEkc=', 'Gerente',       NULL),
    ('centro',   '0OSrcxcrBsrsHZsdmloXIw==:2b5LobBuxwj5l06yqjGh/jDdH/d1M+1Q32Xlk+ZIbA0=', 'Atendente',     1),
    ('norte',    'yRYUf77oKubQJTB62zse+A==:1JIRW5/kuJ6wRdnkWQPvBRIewqBMTi5wlOvz1B+Mi3U=', 'Atendente',     2);

INSERT INTO funcionario (nome, unidade_id) VALUES
    ('Ana Lima',     1),
    ('Carlos Souza', 1),
    ('Bia Rocha',    2);

INSERT INTO produto (nome, categoria, preco_venda, unidade_medida, estoque_minimo) VALUES
    ('Coxinha',           'Salgado', 5.00, 'Unidade', 10),
    ('Bolinha de queijo', 'Salgado', 4.50, 'Unidade', 10),
    ('Refrigerante',      'Bebida',  6.00, 'Unidade',  5),
    ('Pastel de carne',   'Salgado', 7.00, 'Unidade', 10),
    ('Suco de laranja',   'Bebida',  5.50, 'Unidade',  5);

INSERT INTO estoque (unidade_id, produto_id, quantidade) VALUES
    (1, 1, 50), (2, 1, 20),
    (1, 2, 50), (2, 2, 15),
    (1, 3, 50), (2, 3, 30),
    (1, 4, 30), (2, 4, 10),
    (1, 5, 20), (2, 5, 12);

INSERT INTO fornecedor (nome, cnpj, telefone) VALUES
    ('Distribuidora Central', '12.345.678/0001-90', '(11) 3333-4444'),
    ('Fornecedor Bom Sabor',  '98.765.432/0001-10', '(11) 99999-8888');

INSERT INTO cliente (nome, cpf_cnpj, telefone) VALUES
    ('Maria Souza',   '123.456.789-09', '(11) 98888-1111'),
    ('Joao Pereira',  NULL,             '(11) 97777-2222');

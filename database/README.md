# Banco de dados

Scripts do ERP Salgados & Cia. Testados no **MariaDB 10.4** (XAMPP) e no **MySQL 8.0**.

| Arquivo | O que faz |
|---|---|
| `01_schema.sql` | Cria o banco `salgados_cia`: tabelas, funcoes, triggers, procedures, views e o usuario de aplicacao `app_salgados` |
| `02_dados_teste.sql` | Dados ficticios de teste (unidades, logins, funcionarios, produtos, estoque). Rodar **depois** do schema |

## Como criar o banco

1. Inicie o MySQL pelo painel do XAMPP (botao **Start** em MySQL).
2. Abra o phpMyAdmin (ou o MySQL Workbench, conectando em `127.0.0.1:3306` como `root`).
3. Execute o `01_schema.sql` inteiro. Ele **apaga e recria** o banco `salgados_cia`, entao so rode em ambiente de teste.
4. Execute `02_dados_teste.sql` (dados de teste e logins).

## Logins de teste

Todos com a senha `123`: `admin` (Administrador), `gerente1` e `gerente2` (Gerente), `centro` (Atendente da Lanchonete Centro) e `norte` (Atendente da Lanchonete Norte).

As senhas ficam como hash PBKDF2 (`sal:hash`). Para trocar uma senha, gere o hash com `.\gerar_hash.ps1 -Senha "nova"` e rode `UPDATE usuario SET senha_hash = '<hash>' WHERE login = 'admin';`.

## Usuario do aplicativo

O script cria `app_salgados`@`localhost` com a senha provisoria `TROQUE_ESTA_SENHA_FORTE`.
Antes de usar de verdade, troque a senha:

```sql
ALTER USER 'app_salgados'@'localhost' IDENTIFIED BY 'sua_senha_forte';
```

Use a mesma senha na string de conexao `salgados_cia` do arquivo `erp_salgados_e_cia/App.config`.

Essa conta so aceita conexao da propria maquina. Para o aplicativo acessar o banco de outro computador, repita o `CREATE USER` e os `GRANT` do final do `01_schema.sql` para o endereco desejado (ou `'%'`).

O usuario pode ler tudo, cadastrar/alterar/excluir (sem historico) unidade, produto, fornecedor, funcionario e cliente, criar e inativar logins, lancar comandas, itens, pagamentos, entradas, perdas, abatimentos e contagens de inventario, e executar as procedures. Nao altera estoque, caixa, venda, notinha nem log diretamente.

## Regras importantes para quem acessa o banco pelo C#

- Ao abrir cada conexao, execute `SET @usuario_id = <id do login>;` e `SET @unidade_id = <id da unidade de trabalho>;` para o log saber quem fez a operacao e em qual loja.
- Fluxo de venda: `INSERT` em `comanda` e `item_comanda` (o estoque baixa aqui) -> `INSERT` em `pagamento_comanda` (pode ser parcial) -> `CALL sp_cobrar_comanda(comanda, caixa, usuario)`. A procedure cuida da propria transacao: cria a venda, copia itens e pagamentos, gera a notinha, calcula o troco e devolve `venda_id`. O balcao e a comanda de mesa 0.
- Login e logout: `CALL sp_registrar_log('LOGIN' ou 'LOGOUT', 'usuario', <id>, '<login>', NULL);`. As demais operacoes (inclusao, alteracao, exclusao, caixa, entrada, perda...) sao gravadas pelas triggers, com "antes -> depois".
- Caixa: `sp_abrir_caixa`, `sp_editar_caixa` (valor inicial/responsavel, com motivo e autorizador) e `sp_fechar_caixa`. Inventario: `sp_efetivar_inventario(id, forcar)`. Cancelamentos: `sp_cancelar_comanda` e `sp_cancelar_venda`. DRE: `sp_dre(unidade ou NULL, inicio, fim)`.
- Quantidades usam `DECIMAL(10,2)` (kg, litro...). Estoque critico: saldo `<=` minimo (`vw_estoque_atual.critico`).
- O estoque, o caixa fechado e o log so mudam por triggers e procedures; o aplicativo nao altera essas tabelas diretamente.

O modelo de dados esta descrito no documento de DER e dicionario de dados.

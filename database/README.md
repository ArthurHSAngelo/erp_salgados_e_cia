# Banco de dados

Scripts do ERP Salgados & Cia. Testados no **MariaDB 10.4** (XAMPP), no **MariaDB 10.11 LTS** (o servidor que o instalador embute) e no **MySQL 8.0**.

| Arquivo | O que faz |
|---|---|
| `01_schema.sql` | Cria o banco `salgados_cia`: tabelas, funcoes, triggers, procedures, views e o usuario de aplicacao `app_salgados` |
| `02_dados_teste.sql` | Dados ficticios de teste (unidades, logins, funcionarios, produtos, estoque). Rodar **depois** do schema |
| `03_acesso_rede.sql` | **Opcional.** Cria o usuario `app_salgados` para acesso pela rede (varias unidades no mesmo banco). Leia o cabecalho antes de rodar |
| `04_atualizar_bloqueio_login.sql` | **Só para banco já existente** (criado antes de 20/11/2026): acrescenta o bloqueio de login por tentativas sem apagar dados. Pode rodar mais de uma vez. Banco novo não precisa, pois o `01_schema.sql` já traz isso |
| `criar_banco.ps1` | Cria o banco sozinho num servidor (roda o 01 e o 02, define a senha do `app_salgados`, opcionalmente roda o 03 e confere a entrada do programa). E o que o instalador usa; tambem roda a mao: `.\criar_banco.ps1 -Interativo` |
| `gerar_hash.ps1` | Gera o hash de uma senha (`sal:hash`) para a coluna `usuario.senha_hash` |
| `testes_integracao.ps1` | Testes de integracao: chama o programa compilado contra o banco e confere as regras (75 verificacoes) |
| `testes_desempenho.ps1` | Testes de desempenho: enche o banco com 1.000 vendas e mede o tempo de registro, consultas e relatorios contra os limites de 2 e 3 segundos (16 medicoes) |

## Como criar o banco

**Automatico:** o instalador do programa cria o banco (e, se quiser, instala um servidor MariaDB junto). Sem o instalador, rode `.\criar_banco.ps1 -Interativo` nesta pasta (precisa do `MySql.Data.dll`, que fica na pasta do programa). Se o banco ja existir com dados, o script para, a menos que voce confirme apagar. A desinstalacao do programa nunca apaga o banco.

**Manual:**

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

## Como o programa usa o banco

Todos os dados e regras ficam no banco: o programa nao guarda vendas, estoque, caixa nem log em memoria. As listas de cadastros (unidades, produtos, fornecedores, clientes, funcionarios e logins) guardam apenas a ultima leitura do banco, para preencher as telas, e sao recarregadas a cada login e a cada gravacao.

| Tela | O que o banco faz |
|---|---|
| Cadastros (unidades, produtos, fornecedores, funcionarios, clientes, logins) | `INSERT`/`UPDATE`/`DELETE`; o banco recusa duplicidade, valor invalido e exclusao de registro com historico |
| Entrada de estoque | `INSERT` em `entrada_estoque`; a trigger soma o saldo e anota o extrato |
| Perdas | `INSERT` em `perda`; a trigger baixa o saldo e recusa perda maior que o saldo |
| Inventario | `INSERT` em `inventario` e `item_inventario` + `CALL sp_efetivar_inventario` |
| Estoque completo e Movimentacoes | `estoque`, `entrada_estoque` e `movimentacao_estoque` (o "saldo apos" e calculado a partir do saldo atual) |
| Caixa | `sp_abrir_caixa`, `sp_editar_caixa`, `sp_fechar_caixa`; totais por forma de pagamento em `vw_caixa_totais` |
| Vendas (mesa e balcao) | `comanda`, `item_comanda` (baixa o estoque), `pagamento_comanda` e `sp_cobrar_comanda` |
| Notinha | saldo em `vw_notinha_saldo`; abatimento em `abatimento_notinha` |
| Cancelamentos | `sp_cancelar_comanda` e `sp_cancelar_venda` |
| Relatorio de vendas | consultas em `venda`, `item_venda` e `pagamento_venda` |
| DRE | `sp_dre` |
| Painel | vendas do dia, `vw_notinha_saldo`, `vw_estoque_atual` e o log |
| Log de operacoes | `log_operacao` (gravado so por triggers e procedures) |

## Regras importantes para quem acessa o banco pelo C#

- Ao abrir cada conexao, execute `SET NAMES utf8mb4 COLLATE utf8mb4_unicode_ci` (o MySQL 8 usaria `utf8mb4_0900_ai_ci` e daria "Illegal mix of collations" nos filtros), `SET @usuario_id = <id do login>;` e `SET @unidade_id = <id da unidade de trabalho>;` para o log saber quem fez a operacao e em qual loja. A classe `BancoDados` do projeto `erp_salgados_e_cia.Data` ja faz isso.
- Fluxo de venda: `INSERT` em `comanda` e `item_comanda` (o estoque baixa aqui) -> `INSERT` em `pagamento_comanda` (pode ser parcial) -> `CALL sp_cobrar_comanda(comanda, caixa, usuario)`. A procedure cuida da propria transacao: cria a venda, copia itens e pagamentos, gera a notinha, calcula o troco e devolve `venda_id`. O balcao e a comanda de mesa 0.
- As procedures cuidam da propria transacao (`START TRANSACTION`/`COMMIT`): nao chame uma dentro de outra transacao aberta, porque o `START TRANSACTION` fecha a anterior.
- Login e logout: `CALL sp_registrar_log('LOGIN' ou 'LOGOUT', 'usuario', <id>, '<login>', NULL);`. As demais operacoes (inclusao, alteracao, exclusao, caixa, entrada, perda...) sao gravadas pelas triggers, com "antes -> depois".
- Caixa: `sp_abrir_caixa`, `sp_editar_caixa` (valor inicial/responsavel, com motivo e autorizador) e `sp_fechar_caixa`. Inventario: `sp_efetivar_inventario(id, forcar)`. Cancelamentos: `sp_cancelar_comanda` e `sp_cancelar_venda`. DRE: `sp_dre(unidade ou NULL, inicio, fim)`.
- Quantidades usam `DECIMAL(10,2)` (kg, litro...). Estoque critico: saldo `<=` minimo (`vw_estoque_atual.critico`).
- O estoque, o caixa fechado e o log so mudam por triggers e procedures; o aplicativo nao altera essas tabelas diretamente.
- Erros de regra chegam como `SIGNAL` (codigo 1644) e a classe `BancoDados` os converte em mensagem para o usuario; duplicidade (1062) e registro com historico (1217/1451) tambem sao traduzidos.

## Testes de integracao

O arquivo `testes_integracao.ps1` roda o programa compilado contra o banco e confere 75 pontos: login e bloqueio por tentativas (5 senhas erradas bloqueiam por 15 minutos, guardado no banco), cadastros (duplicidade, valor invalido, exclusao com historico), caixa (abrir, editar com autorizacao, fechar, um por dia), estoque (entrada, perda, inventario, extrato), comandas (pedido, pagamento parcial, cobranca com troco e Notinha, cancelamento), vendas no balcao, abatimento da Notinha, cancelamento de venda, fechamento do caixa e relatorios (vendas, DRE, painel e log).

1. Compile a solucao no Visual Studio.
2. Recrie o banco de teste: execute `01_schema.sql` e depois `02_dados_teste.sql`.
3. Rode no PowerShell, na pasta `database`, apontando para a pasta onde o programa foi compilado:

```powershell
.\testes_integracao.ps1 -Pasta "..\erp_salgados_e_cia\bin\Debug" -Porta 3306
```

Se trocou a senha do usuario `app_salgados`, acrescente `-Senha "sua_senha"`. O resultado esperado e `PASSOU: 75   FALHOU: 0`. Como os testes gravam dados (vendas, caixa, estoque), so podem ser repetidos depois de recriar o banco.

Resultado: em 17/11/2026 eram 68 verificacoes (0 falhas). Em 20/11/2026, com o bloqueio de login, sao 75 verificacoes, 0 falhas, no MariaDB 10.4.32, no MySQL 8.0.46 e no MariaDB 10.11.19.

### Testes de desempenho

O `testes_desempenho.ps1` confere os requisitos de tempo (cadastro, consulta e registro em ate 2 segundos; listagens e relatorios em ate 3 segundos). Ele registra 1.000 vendas no balcao, mede o tempo de cada uma e depois mede consultas e relatorios com o banco cheio. Como o `testes_integracao.ps1`, precisa de um banco de teste recem-criado (01 e 02) e grava dados:

```powershell
.\testes_desempenho.ps1 -Pasta "..\erp_salgados_e_cia\bin\Debug" -Porta 3306 -Saida desempenho.json
```

Termina com `DESEMPENHO: 16 de 16 operacoes dentro do limite`. Resultado de 20/11/2026 (MariaDB 10.11.19, banco no mesmo computador): o pior caso foi o login, com 167 ms; cada venda levou em media 5 ms.

O modelo de dados esta descrito no documento de DER e dicionario de dados.

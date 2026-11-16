# Notas da atualização do banco (aplicação da especificação)

Testado em MariaDB 10.11 (compatível com MySQL 8): `01_schema.sql` e `02_dados_teste.sql` rodam sem erro, inclusive rodando de novo. As marcas `[ESPEC. n]` no schema indicam a seção da especificação.

## O que foi feito, por seção
1. **Remoções (§1, §2):** tabela `lancamento` e seus 3 triggers; colunas `produto.controla_validade`, `entrada_estoque.data_validade`, `venda.atendente_id`, `venda.forma_pagamento`, `usuario.nome`, `fornecedor.email`.
2. **Tabelas novas (§3):** funcionario, cliente, comanda, item_comanda, pagamento_venda, notinha, abatimento_notinha, inventario, item_inventario, exatamente como na especificação.
3. **Alterações (§4):** unidade (cnpj, numero, status Ativo/Inativo), usuario.unidade_id NULL, fornecedor.cnpj, caixa (maquininha + responsável funcionário), venda (numero_mesa, cliente_id, comanda_id), responsável funcionário em entrada e perda, log_operacao.unidade_id, movimentacao tipo Estorno/Ajuste. Comentário do hash corrigido para PBKDF2.
4. **Fluxo comanda → cobrança (§5):** baixa de estoque movida para `item_comanda`; novas `sp_cancelar_comanda` e `sp_cobrar_comanda`; `item_venda` só soma o total; `sp_finalizar_venda` calcula o troco a partir dos pagamentos; `sp_cancelar_venda` com Estorno e checagem RN46.
5. **Views/procedure (§6):** `vw_validade` e `vw_dre_mensal` removidas; `sp_dre(unidade|NULL, inicio, fim)` criada; `vw_vendas_diarias` e `vw_caixa_totais` leem `pagamento_venda`.
6. **Regras novas (§7):** um caixa por dia por unidade; funcionário da mesma unidade (caixa, entrada, perda); Notinha só com cliente na venda.
7. **GRANT (§8):** tabelas novas liberadas; `lancamento` removida.

## Decisões minhas (pontos que a especificação deixou abertos ou não cobriu) — confirmar com o Marco
- **Perguntas em aberto 1, 2, 3, 5:** segui as recomendações da própria especificação (manter comanda cancelada, manter item_comanda, manter `venda.comanda_id`, `@unidade_id` vem do C#).
- **Pergunta 4:** implementei o trigger `trg_abatimento_bi` (abatimento exige caixa aberto da mesma unidade). Para desfazer, basta apagar esse trigger.
- **Pergunta 6:** mantive `VARCHAR` em categoria e unidade_medida.
- **RN46 "notinha já abatida" (provisório):** `abatimento_notinha` não tem ligação com `notinha`, então o banco não sabe qual notinha foi abatida. Regra usada: a venda não pode ser cancelada se o total abatido pelo cliente for maior que o total das OUTRAS notinhas dele (ou seja, o cancelamento deixaria o saldo negativo). Confirmar se é a regra do aplicativo.
- **Valor do pagamento em dinheiro:** `pagamento_venda.valor` guarda o que o cliente ENTREGOU; troco = soma dos pagamentos − total. O troco só pode sair de dinheiro (Pix a mais é recusado). As views descontam o troco do Dinheiro (view auxiliar `vw_pagamento_liquido`).
- **`sp_fechar_caixa` ganhou 3º parâmetro** `p_maquininha` (pode ser NULL). A `diferenca` continua sendo só do dinheiro; a maquininha fica guardada para conferir com `vw_caixa_totais`.
- **`sp_abrir_caixa`:** o 2º parâmetro agora é o id do FUNCIONÁRIO.
- **Transação:** `sp_cobrar_comanda` NÃO abre transação própria; o C# deve envolver cobrar + pagamentos + finalizar numa única transação (ROLLBACK reabre a comanda). Testado.
- **Segurança (diferente do que estava):** `app_salgados` não tem mais INSERT em `venda`/`item_venda` (venda só nasce por `sp_cobrar_comanda`), nem UPDATE em venda. Tabelas históricas (item_comanda, pagamento_venda, notinha, abatimento_notinha) só INSERT; `comanda` só UPDATE de mesa/cliente (status muda só pelas procedures). Se o C# precisar de algo a mais, é uma linha de GRANT.
- **Extras pequenos:** função `fn_funcionario_ok` (também recusa funcionário Inativo); trigger do inventário (mesma regra de unidade); `trg_venda_au` só loga quando o status muda; `trg_comanda_au` loga cobrança/cancelamento; view `vw_notinha_saldo`.
- **Não coberto pela especificação (não implementei):** procedure que aplica o inventário no estoque (tipo `Ajuste` existe, mas nada o usa ainda); editar/remover item de comanda já lançado (só cancelar a comanda inteira); CHECK de dígitos em CPF/CNPJ/telefone (existia na versão anterior, a especificação não pede — é o front que valida, ou quer no banco?).

## Pendências fora do banco
- `MELHORIAS_BANCO.md` e o simulador publicado descrevem o modelo antigo; atualizar depois se quiser.
- Subir no GitHub: copiar `01_schema.sql` e `02_dados_teste.sql` para a pasta `database/` e fazer `git add`, `git commit`, `git push` (nunca colocar senha real no arquivo).

using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using erp_salgados_e_cia.Data.Connection;
using erp_salgados_e_cia.Domain.Entities;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>
    /// Comandas, vendas, notinha e cancelamentos. O estoque baixa quando o item entra na comanda
    /// (trigger); a cobrança, o troco, a notinha e o cancelamento são feitos pelas procedures do banco.
    /// </summary>
    public static class VendaRepositorio
    {
        // ---- Comandas abertas ----

        /// <summary>Comandas abertas de mesa (mesa maior que zero) da unidade.</summary>
        public static DataTable ListarComandasAbertas(uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT id, numero_mesa, cliente_id, data_abertura FROM comanda " +
                "WHERE unidade_id = @un AND status = 'Aberta' AND numero_mesa > 0 " +
                "ORDER BY numero_mesa",
                BancoDados.Parametro("@un", unidadeId));
        }

        public static DataTable ListarItensDasComandasAbertas(uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT i.comanda_id, p.nome AS produto, i.quantidade, i.preco_unitario " +
                "FROM item_comanda i " +
                "JOIN comanda c ON c.id = i.comanda_id " +
                "JOIN produto p ON p.id = i.produto_id " +
                "WHERE c.unidade_id = @un AND c.status = 'Aberta' AND c.numero_mesa > 0 " +
                "ORDER BY i.id",
                BancoDados.Parametro("@un", unidadeId));
        }

        public static DataTable ListarPagamentosDasComandasAbertas(uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT g.comanda_id, g.forma_pagamento, g.valor " +
                "FROM pagamento_comanda g " +
                "JOIN comanda c ON c.id = g.comanda_id " +
                "WHERE c.unidade_id = @un AND c.status = 'Aberta' AND c.numero_mesa > 0 " +
                "ORDER BY g.id",
                BancoDados.Parametro("@un", unidadeId));
        }

        /// <summary>
        /// Envia o pedido para a comanda da mesa (abre a comanda se ainda não existir) e lança os itens.
        /// Tudo ou nada: se faltar estoque em um item, nada é gravado. Devolve o id da comanda.
        /// </summary>
        public static uint EnviarPedido(
            uint unidadeId,
            int numeroMesa,
            uint? clienteId,
            List<ItemPedido> itens)
        {
            uint comandaId = 0;

            BancoDados.EmTransacao((cn, tx) =>
            {
                comandaId = ObterOuAbrirComanda(cn, tx, unidadeId, numeroMesa, clienteId);
                InserirItens(cn, tx, comandaId, itens);
            });

            return comandaId;
        }

        /// <summary>Troca o cliente de uma comanda aberta (o banco recusa se já há pagamento em Notinha).</summary>
        public static void TrocarCliente(uint comandaId, uint? clienteId)
        {
            BancoDados.Executar(
                "UPDATE comanda SET cliente_id = @cli WHERE id = @id AND status = 'Aberta'",
                BancoDados.Parametro("@cli", clienteId.HasValue ? (object)clienteId.Value : null),
                BancoDados.Parametro("@id", comandaId));
        }

        /// <summary>Registra pagamentos (inclusive parciais) numa comanda aberta.</summary>
        public static void RegistrarPagamentos(uint comandaId, List<PagamentoNovo> pagamentos)
        {
            BancoDados.EmTransacao((cn, tx) => InserirPagamentos(cn, tx, comandaId, pagamentos));
        }

        /// <summary>Cobra a comanda: cria a venda, copia itens e pagamentos, gera a notinha e calcula o troco.</summary>
        public static uint Cobrar(uint comandaId, uint caixaId, uint usuarioId)
        {
            object id = BancoDados.Escalar(
                "CALL sp_cobrar_comanda(@comanda, @caixa, @usuario)",
                BancoDados.Parametro("@comanda", comandaId),
                BancoDados.Parametro("@caixa", caixaId),
                BancoDados.Parametro("@usuario", usuarioId));

            return Convert.ToUInt32(id);
        }

        /// <summary>Cancela a comanda aberta (sem pagamentos): os itens voltam ao estoque.</summary>
        public static void CancelarComanda(uint comandaId, uint usuarioId)
        {
            BancoDados.Executar(
                "CALL sp_cancelar_comanda(@comanda, @usuario)",
                BancoDados.Parametro("@comanda", comandaId),
                BancoDados.Parametro("@usuario", usuarioId));
        }

        /// <summary>
        /// Venda de balcão (comanda de mesa 0): lança os itens e, em seguida, os pagamentos e a cobrança.
        /// Se os itens ou os pagamentos falharem antes da cobrança, a comanda é cancelada e o estoque volta.
        /// </summary>
        public static uint VenderNoBalcao(
            uint unidadeId,
            uint? clienteId,
            List<ItemPedido> itens,
            List<PagamentoNovo> pagamentos,
            uint caixaId,
            uint usuarioId)
        {
            uint comandaId = 0;

            // 1) comanda + itens (se um item falhar, nem a comanda fica)
            BancoDados.EmTransacao((cn, tx) =>
            {
                comandaId = AbrirComanda(cn, tx, unidadeId, 0, clienteId);
                InserirItens(cn, tx, comandaId, itens);
            });

            // 2) pagamentos (se falhar, nada foi pago ainda: a comanda pode ser cancelada)
            try
            {
                RegistrarPagamentos(comandaId, pagamentos);
            }
            catch
            {
                try { CancelarComanda(comandaId, usuarioId); }
                catch { /* a mensagem original é a que interessa */ }
                throw;
            }

            // 3) cobrança (a procedure cuida da própria transação)
            return Cobrar(comandaId, caixaId, usuarioId);
        }

        // ---- Vendas ----

        /// <summary>Vendas (da unidade, ou de todas se unidadeId for nulo), da mais antiga para a mais nova.</summary>
        public static DataTable ListarVendas(uint? unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT v.id, un.nome AS unidade, v.numero_mesa, v.caixa_id, v.data_hora, v.valor_total, " +
                "v.valor_recebido, v.troco, v.status, u.login AS cancelado_por, v.motivo_cancelamento, " +
                "v.data_cancelamento, v.cliente_id " +
                "FROM venda v " +
                "JOIN unidade un ON un.id = v.unidade_id " +
                "LEFT JOIN usuario u ON u.id = v.cancelado_por " +
                "WHERE (@un IS NULL OR v.unidade_id = @un) " +
                "ORDER BY v.data_hora, v.id",
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null));
        }

        public static DataTable ListarItensDasVendas(uint? unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT i.venda_id, p.nome AS produto, i.quantidade, i.preco_unitario " +
                "FROM item_venda i " +
                "JOIN venda v ON v.id = i.venda_id " +
                "JOIN produto p ON p.id = i.produto_id " +
                "WHERE (@un IS NULL OR v.unidade_id = @un) " +
                "ORDER BY i.id",
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null));
        }

        public static DataTable ListarPagamentosDasVendas(uint? unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT g.venda_id, g.forma_pagamento, g.valor " +
                "FROM pagamento_venda g " +
                "JOIN venda v ON v.id = g.venda_id " +
                "WHERE (@un IS NULL OR v.unidade_id = @un) " +
                "ORDER BY g.id",
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null));
        }

        /// <summary>Cancela a venda (somente Gerente/Administrador, caixa aberto, unidade de trabalho, com motivo).</summary>
        public static void CancelarVenda(uint vendaId, uint usuarioId, string motivo)
        {
            BancoDados.Executar(
                "CALL sp_cancelar_venda(@venda, @usuario, @motivo)",
                BancoDados.Parametro("@venda", vendaId),
                BancoDados.Parametro("@usuario", usuarioId),
                BancoDados.Parametro("@motivo", motivo));
        }

        // ---- Notinha ----

        /// <summary>Saldo devedor de um cliente na unidade (notinhas de vendas não canceladas menos abatimentos).</summary>
        public static decimal SaldoNotinha(uint clienteId, uint unidadeId)
        {
            object r = BancoDados.Escalar(
                "SELECT saldo_devedor FROM vw_notinha_saldo WHERE cliente_id = @cli AND unidade_id = @un",
                BancoDados.Parametro("@cli", clienteId),
                BancoDados.Parametro("@un", unidadeId));

            return r == null ? 0m : Convert.ToDecimal(r);
        }

        /// <summary>Saldo devedor de todos os clientes com movimento de Notinha na unidade.</summary>
        public static DataTable SaldosNotinhaDaUnidade(uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT cliente_id, saldo_devedor FROM vw_notinha_saldo WHERE unidade_id = @un",
                BancoDados.Parametro("@un", unidadeId));
        }

        /// <summary>Notinhas do cliente na unidade (de vendas não canceladas).</summary>
        public static DataTable ListarNotinhasDoCliente(uint clienteId, uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT n.id, n.venda_id, n.data_hora, n.valor " +
                "FROM notinha n JOIN venda v ON v.id = n.venda_id AND v.status = 'Concluida' " +
                "WHERE n.cliente_id = @cli AND n.unidade_id = @un " +
                "ORDER BY n.data_hora, n.id",
                BancoDados.Parametro("@cli", clienteId),
                BancoDados.Parametro("@un", unidadeId));
        }

        /// <summary>Produtos das vendas que geraram Notinha para o cliente na unidade.</summary>
        public static DataTable ListarItensDasNotinhasDoCliente(uint clienteId, uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT n.venda_id, p.nome AS produto, i.quantidade " +
                "FROM notinha n " +
                "JOIN item_venda i ON i.venda_id = n.venda_id " +
                "JOIN produto p ON p.id = i.produto_id " +
                "WHERE n.cliente_id = @cli AND n.unidade_id = @un " +
                "ORDER BY i.id",
                BancoDados.Parametro("@cli", clienteId),
                BancoDados.Parametro("@un", unidadeId));
        }

        public static DataTable ListarAbatimentosDoCliente(uint clienteId, uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT id, data_hora, valor, forma_pagamento FROM abatimento_notinha " +
                "WHERE cliente_id = @cli AND unidade_id = @un ORDER BY data_hora, id",
                BancoDados.Parametro("@cli", clienteId),
                BancoDados.Parametro("@un", unidadeId));
        }

        /// <summary>Grava os abatimentos de uma vez (tudo ou nada). Cada pagamento vira uma linha.</summary>
        public static void RegistrarAbatimentos(
            uint clienteId,
            uint caixaId,
            uint unidadeId,
            List<PagamentoNovo> pagamentos)
        {
            BancoDados.EmTransacao((cn, tx) =>
            {
                foreach (PagamentoNovo pagamento in pagamentos)
                {
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO abatimento_notinha (cliente_id, caixa_id, unidade_id, valor, forma_pagamento) " +
                        "VALUES (@cli, @caixa, @un, @valor, @forma)", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@cli", clienteId);
                        cmd.Parameters.AddWithValue("@caixa", caixaId);
                        cmd.Parameters.AddWithValue("@un", unidadeId);
                        cmd.Parameters.AddWithValue("@valor", pagamento.Valor);
                        cmd.Parameters.AddWithValue("@forma", pagamento.Forma);
                        cmd.ExecuteNonQuery();
                    }
                }
            });
        }

        // ---- Apoio ----

        private static uint AbrirComanda(
            MySqlConnection cn,
            MySqlTransaction tx,
            uint unidadeId,
            int numeroMesa,
            uint? clienteId)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO comanda (unidade_id, numero_mesa, cliente_id) VALUES (@un, @mesa, @cli); " +
                "SELECT LAST_INSERT_ID();", cn, tx))
            {
                cmd.Parameters.AddWithValue("@un", unidadeId);
                cmd.Parameters.AddWithValue("@mesa", numeroMesa);
                cmd.Parameters.AddWithValue("@cli", clienteId.HasValue ? (object)clienteId.Value : DBNull.Value);
                return Convert.ToUInt32(cmd.ExecuteScalar());
            }
        }

        private static uint ObterOuAbrirComanda(
            MySqlConnection cn,
            MySqlTransaction tx,
            uint unidadeId,
            int numeroMesa,
            uint? clienteId)
        {
            using (MySqlCommand cmd = new MySqlCommand(
                "SELECT id FROM comanda WHERE unidade_id = @un AND numero_mesa = @mesa AND status = 'Aberta' " +
                "ORDER BY id LIMIT 1 FOR UPDATE", cn, tx))
            {
                cmd.Parameters.AddWithValue("@un", unidadeId);
                cmd.Parameters.AddWithValue("@mesa", numeroMesa);
                object existente = cmd.ExecuteScalar();

                if (existente != null && existente != DBNull.Value)
                {
                    return Convert.ToUInt32(existente);
                }
            }

            return AbrirComanda(cn, tx, unidadeId, numeroMesa, clienteId);
        }

        private static void InserirItens(
            MySqlConnection cn,
            MySqlTransaction tx,
            uint comandaId,
            List<ItemPedido> itens)
        {
            foreach (ItemPedido item in itens)
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO item_comanda (comanda_id, produto_id, quantidade, preco_unitario) " +
                    "VALUES (@comanda, @produto, @qtd, @preco)", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@comanda", comandaId);
                    cmd.Parameters.AddWithValue("@produto", item.ProdutoId);
                    cmd.Parameters.AddWithValue("@qtd", item.Quantidade);
                    cmd.Parameters.AddWithValue("@preco", item.PrecoUnitario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void InserirPagamentos(
            MySqlConnection cn,
            MySqlTransaction tx,
            uint comandaId,
            List<PagamentoNovo> pagamentos)
        {
            foreach (PagamentoNovo pagamento in pagamentos)
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO pagamento_comanda (comanda_id, forma_pagamento, valor) " +
                    "VALUES (@comanda, @forma, @valor)", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@comanda", comandaId);
                    cmd.Parameters.AddWithValue("@forma", pagamento.Forma);
                    cmd.Parameters.AddWithValue("@valor", pagamento.Valor);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

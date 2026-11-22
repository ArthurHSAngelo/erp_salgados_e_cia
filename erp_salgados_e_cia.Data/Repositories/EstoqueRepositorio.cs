using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using erp_salgados_e_cia.Data.Connection;
using erp_salgados_e_cia.Domain.Entities;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>
    /// Estoque: saldos, entradas, perdas, inventário e extrato de movimentações.
    /// O saldo e o extrato são gravados pelas triggers e procedures do banco; aqui só
    /// se inserem as entradas, perdas e contagens e se consulta o resultado.
    /// </summary>
    public static class EstoqueRepositorio
    {
        // ---- Saldos ----

        /// <summary>Todos os saldos gravados (unidade_id, produto_id, quantidade).</summary>
        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT unidade_id, produto_id, quantidade FROM estoque");
        }

        /// <summary>Saldo de cada produto da unidade (produto sem linha = sem saldo).</summary>
        public static Dictionary<uint, decimal> SaldosDaUnidade(uint unidadeId)
        {
            DataTable tabela = BancoDados.Consultar(
                "SELECT produto_id, quantidade FROM estoque WHERE unidade_id = @un",
                BancoDados.Parametro("@un", unidadeId));

            Dictionary<uint, decimal> saldos = new Dictionary<uint, decimal>();

            foreach (DataRow linha in tabela.Rows)
            {
                saldos[Convert.ToUInt32(linha["produto_id"])] =
                    Convert.ToDecimal(linha["quantidade"]);
            }

            return saldos;
        }

        /// <summary>Saldo de um produto numa unidade (0 se nunca teve movimento).</summary>
        public static decimal SaldoDe(uint unidadeId, uint produtoId)
        {
            object r = BancoDados.Escalar(
                "SELECT quantidade FROM estoque WHERE unidade_id = @un AND produto_id = @pr",
                BancoDados.Parametro("@un", unidadeId),
                BancoDados.Parametro("@pr", produtoId));

            return r == null ? 0m : Convert.ToDecimal(r);
        }

        // ---- Entradas ----

        public static DataTable ListarEntradas()
        {
            return BancoDados.Consultar(
                "SELECT e.id, p.nome AS produto, e.quantidade, e.custo_unitario, " +
                "f.nome AS fornecedor, fu.nome AS responsavel, un.nome AS unidade, e.data_hora " +
                "FROM entrada_estoque e " +
                "JOIN produto p ON p.id = e.produto_id " +
                "JOIN unidade un ON un.id = e.unidade_id " +
                "JOIN funcionario fu ON fu.id = e.responsavel_funcionario_id " +
                "LEFT JOIN fornecedor f ON f.id = e.fornecedor_id " +
                "ORDER BY e.data_hora, e.id");
        }

        /// <summary>
        /// Grava todos os itens da entrada de uma vez (tudo ou nada). O saldo, o extrato e o log
        /// são atualizados pelas triggers do banco.
        /// </summary>
        public static void InserirEntrada(
            uint unidadeId,
            uint usuarioId,
            uint? fornecedorId,
            uint responsavelId,
            DateTime dataEntrada,
            List<ItemEntradaEstoque> itens)
        {
            BancoDados.EmTransacao((cn, tx) =>
            {
                foreach (ItemEntradaEstoque item in itens)
                {
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO entrada_estoque (unidade_id, produto_id, quantidade, custo_unitario, " +
                        "fornecedor_id, data_entrada, usuario_id, responsavel_funcionario_id) " +
                        "VALUES (@un, @pr, @qtd, @custo, @forn, @data, @usu, @resp)", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@un", unidadeId);
                        cmd.Parameters.AddWithValue("@pr", item.ProdutoId);
                        cmd.Parameters.AddWithValue("@qtd", item.Quantidade);
                        cmd.Parameters.AddWithValue("@custo", item.CustoUnitario);
                        cmd.Parameters.AddWithValue("@forn", fornecedorId.HasValue ? (object)fornecedorId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@data", dataEntrada.Date);
                        cmd.Parameters.AddWithValue("@usu", usuarioId);
                        cmd.Parameters.AddWithValue("@resp", responsavelId);
                        cmd.ExecuteNonQuery();
                    }
                }
            });
        }

        /// <summary>
        /// Último custo de compra de cada produto (id do produto, custo). Com unidade, só as
        /// entradas dela; sem unidade (null), todas.
        /// </summary>
        public static Dictionary<uint, decimal> UltimosCustos(uint? unidadeId)
        {
            string filtro = unidadeId.HasValue ? " AND e2.unidade_id = @un" : "";

            DataTable tabela = BancoDados.Consultar(
                "SELECT e.produto_id, e.custo_unitario FROM entrada_estoque e " +
                "WHERE e.id = (SELECT e2.id FROM entrada_estoque e2 " +
                "WHERE e2.produto_id = e.produto_id" + filtro + " " +
                "ORDER BY e2.data_hora DESC, e2.id DESC LIMIT 1)",
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null));

            Dictionary<uint, decimal> custos = new Dictionary<uint, decimal>();

            foreach (DataRow linha in tabela.Rows)
            {
                custos[Convert.ToUInt32(linha["produto_id"])] =
                    Convert.ToDecimal(linha["custo_unitario"]);
            }

            return custos;
        }

        // ---- Perdas ----

        public static DataTable ListarPerdas()
        {
            return BancoDados.Consultar(
                "SELECT pe.id, pe.data, un.nome AS unidade, p.nome AS produto, pe.quantidade, pe.motivo, " +
                "fu.nome AS responsavel, u.login AS usuario, pe.data_hora " +
                "FROM perda pe " +
                "JOIN produto p ON p.id = pe.produto_id " +
                "JOIN unidade un ON un.id = pe.unidade_id " +
                "JOIN funcionario fu ON fu.id = pe.responsavel_funcionario_id " +
                "JOIN usuario u ON u.id = pe.usuario_id " +
                "ORDER BY pe.data, pe.id");
        }

        /// <summary>Registra a perda. O banco baixa o saldo e recusa perda maior que o saldo.</summary>
        public static void InserirPerda(
            uint unidadeId,
            uint produtoId,
            decimal quantidade,
            string motivo,
            DateTime data,
            uint usuarioId,
            uint responsavelId)
        {
            BancoDados.Executar(
                "INSERT INTO perda (unidade_id, produto_id, quantidade, motivo, data, usuario_id, responsavel_funcionario_id) " +
                "VALUES (@un, @pr, @qtd, @motivo, @data, @usu, @resp)",
                BancoDados.Parametro("@un", unidadeId),
                BancoDados.Parametro("@pr", produtoId),
                BancoDados.Parametro("@qtd", quantidade),
                BancoDados.Parametro("@motivo", motivo),
                BancoDados.Parametro("@data", data.Date),
                BancoDados.Parametro("@usu", usuarioId),
                BancoDados.Parametro("@resp", responsavelId));
        }

        // ---- Inventário ----

        /// <summary>
        /// Grava a contagem e a efetiva (ajusta o estoque). Devolve o id do inventário.
        /// A efetivação é feita pela procedure sp_efetivar_inventario, que cuida da própria transação.
        /// </summary>
        public static uint EfetivarInventario(
            uint unidadeId,
            uint responsavelId,
            uint usuarioId,
            string observacao,
            List<ItemContagemInventario> itens)
        {
            uint inventarioId = 0;

            BancoDados.EmTransacao((cn, tx) =>
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO inventario (unidade_id, responsavel_funcionario_id, usuario_id, observacao) " +
                    "VALUES (@un, @resp, @usu, @obs); SELECT LAST_INSERT_ID();", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@un", unidadeId);
                    cmd.Parameters.AddWithValue("@resp", responsavelId);
                    cmd.Parameters.AddWithValue("@usu", usuarioId);
                    cmd.Parameters.AddWithValue("@obs", string.IsNullOrWhiteSpace(observacao) ? (object)DBNull.Value : observacao.Trim());
                    inventarioId = Convert.ToUInt32(cmd.ExecuteScalar());
                }

                foreach (ItemContagemInventario item in itens)
                {
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO item_inventario (inventario_id, produto_id, saldo_sistema, quantidade_contada, motivo) " +
                        "VALUES (@inv, @pr, @saldo, @contada, @motivo)", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@inv", inventarioId);
                        cmd.Parameters.AddWithValue("@pr", item.ProdutoId);
                        cmd.Parameters.AddWithValue("@saldo", item.SaldoSistema);
                        cmd.Parameters.AddWithValue("@contada", item.QuantidadeContada);
                        cmd.Parameters.AddWithValue("@motivo", string.IsNullOrWhiteSpace(item.Motivo) ? (object)DBNull.Value : item.Motivo.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
            });

            // Fora da transação acima: a procedure abre e fecha a sua própria.
            // forcar = 1: a tela já avisou o usuário se o saldo mudou durante a contagem.
            BancoDados.Executar(
                "CALL sp_efetivar_inventario(@inv, 1)",
                BancoDados.Parametro("@inv", inventarioId));

            return inventarioId;
        }

        // ---- Extrato (movimentações) ----

        /// <summary>
        /// Extrato do estoque no período. "saldo_apos" é calculado a partir do saldo atual,
        /// voltando os movimentos posteriores (vale também para saldo de abertura sem movimento).
        /// </summary>
        public static DataTable ConsultarMovimentacoes(
            DateTime inicio,
            DateTime fim,
            uint? unidadeId,
            uint? produtoId,
            string tipo)
        {
            return BancoDados.Consultar(
                "SELECT x.id, x.data_hora, x.produto, x.tipo, x.quantidade, x.saldo_apos, x.unidade, x.login FROM (" +
                "SELECT m.id, m.data_hora, m.unidade_id, m.produto_id, p.nome AS produto, m.tipo, m.quantidade, " +
                "un.nome AS unidade, u.login, " +
                "COALESCE(e.quantidade, 0) - (" +
                "SUM(m.quantidade) OVER (PARTITION BY m.unidade_id, m.produto_id) - " +
                "SUM(m.quantidade) OVER (PARTITION BY m.unidade_id, m.produto_id ORDER BY m.data_hora, m.id)) AS saldo_apos " +
                "FROM movimentacao_estoque m " +
                "JOIN produto p ON p.id = m.produto_id " +
                "JOIN unidade un ON un.id = m.unidade_id " +
                "LEFT JOIN usuario u ON u.id = m.usuario_id " +
                "LEFT JOIN estoque e ON e.unidade_id = m.unidade_id AND e.produto_id = m.produto_id" +
                ") x " +
                "WHERE DATE(x.data_hora) BETWEEN @ini AND @fim " +
                "AND (@un IS NULL OR x.unidade_id = @un) " +
                "AND (@pr IS NULL OR x.produto_id = @pr) " +
                "AND (@tp IS NULL OR x.tipo = @tp) " +
                "ORDER BY x.data_hora DESC, x.id DESC",
                BancoDados.Parametro("@ini", inicio.Date),
                BancoDados.Parametro("@fim", fim.Date),
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null),
                BancoDados.Parametro("@pr", produtoId.HasValue ? (object)produtoId.Value : null),
                BancoDados.Parametro("@tp", tipo));
        }
    }
}

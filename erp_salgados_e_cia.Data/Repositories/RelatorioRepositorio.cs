using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>
    /// Consultas dos relatórios (vendas, DRE) e dos números do painel.
    /// Formas de pagamento usam os nomes do banco: Dinheiro, Pix, Credito, Debito, Notinha.
    /// </summary>
    public static class RelatorioRepositorio
    {
        // Vendas concluídas do período. Quando há filtro de forma de pagamento ou de produto,
        // entram as vendas que têm aquela forma / aquele produto (a venda inteira conta).
        private const string FiltroVendas =
            "v.status = 'Concluida' " +
            "AND v.data_hora >= @ini AND v.data_hora < DATE_ADD(@fim, INTERVAL 1 DAY) " +
            "AND (@un IS NULL OR v.unidade_id = @un) " +
            "AND (@forma IS NULL OR EXISTS (SELECT 1 FROM pagamento_venda fp WHERE fp.venda_id = v.id AND fp.forma_pagamento = @forma)) " +
            "AND (@pr IS NULL OR EXISTS (SELECT 1 FROM item_venda fi WHERE fi.venda_id = v.id AND fi.produto_id = @pr))";

        private static MySql.Data.MySqlClient.MySqlParameter[] Filtros(
            DateTime inicio,
            DateTime fim,
            uint? unidadeId,
            string forma,
            uint? produtoId)
        {
            return new[]
            {
                BancoDados.Parametro("@ini", inicio.Date),
                BancoDados.Parametro("@fim", fim.Date),
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null),
                BancoDados.Parametro("@forma", forma),
                BancoDados.Parametro("@pr", produtoId.HasValue ? (object)produtoId.Value : null)
            };
        }

        /// <summary>Por dia: quantidade de vendas, faturamento e troco.</summary>
        public static DataTable VendasPorDia(
            DateTime inicio,
            DateTime fim,
            uint? unidadeId,
            string forma,
            uint? produtoId)
        {
            return BancoDados.Consultar(
                "SELECT DATE(v.data_hora) AS dia, COUNT(*) AS quantidade, " +
                "SUM(v.valor_total) AS faturamento, SUM(v.troco) AS troco " +
                "FROM venda v WHERE " + FiltroVendas + " GROUP BY DATE(v.data_hora) ORDER BY dia",
                Filtros(inicio, fim, unidadeId, forma, produtoId));
        }

        /// <summary>Valor pago em cada forma de pagamento nas vendas do período (o troco não está descontado).</summary>
        public static DataTable PagamentosDasVendas(
            DateTime inicio,
            DateTime fim,
            uint? unidadeId,
            string forma,
            uint? produtoId)
        {
            return BancoDados.Consultar(
                "SELECT pg.forma_pagamento, SUM(pg.valor) AS valor " +
                "FROM pagamento_venda pg JOIN venda v ON v.id = pg.venda_id " +
                "WHERE " + FiltroVendas + " GROUP BY pg.forma_pagamento",
                Filtros(inicio, fim, unidadeId, forma, produtoId));
        }

        /// <summary>Produtos vendidos no período (quantidade e receita).</summary>
        public static DataTable ProdutosVendidos(
            DateTime inicio,
            DateTime fim,
            uint? unidadeId,
            string forma,
            uint? produtoId)
        {
            return BancoDados.Consultar(
                "SELECT p.nome AS produto, SUM(i.quantidade) AS quantidade, " +
                "SUM(i.quantidade * i.preco_unitario) AS receita " +
                "FROM item_venda i " +
                "JOIN venda v ON v.id = i.venda_id " +
                "JOIN produto p ON p.id = i.produto_id " +
                "WHERE " + FiltroVendas + " AND (@pr IS NULL OR i.produto_id = @pr) " +
                "GROUP BY p.nome",
                Filtros(inicio, fim, unidadeId, forma, produtoId));
        }

        /// <summary>DRE do período, uma linha por unidade (procedure sp_dre).</summary>
        public static DataTable Dre(uint? unidadeId, DateTime inicio, DateTime fim)
        {
            return BancoDados.Consultar(
                "CALL sp_dre(@un, @ini, @fim)",
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null),
                BancoDados.Parametro("@ini", inicio.Date),
                BancoDados.Parametro("@fim", fim.Date));
        }

        /// <summary>As últimas vendas da unidade (painel), concluídas ou canceladas.</summary>
        public static DataTable UltimasVendas(uint unidadeId, int quantidade)
        {
            return BancoDados.Consultar(
                "SELECT id, data_hora, valor_total, status FROM venda WHERE unidade_id = @un " +
                "ORDER BY data_hora DESC, id DESC LIMIT " + Convert.ToInt32(quantidade),
                BancoDados.Parametro("@un", unidadeId));
        }

        /// <summary>Quantos clientes devem Notinha na unidade e o total devido (painel).</summary>
        public static DataTable ResumoNotinha(uint unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT COUNT(*) AS clientes, COALESCE(SUM(saldo_devedor), 0) AS total " +
                "FROM vw_notinha_saldo WHERE unidade_id = @un AND saldo_devedor > 0",
                BancoDados.Parametro("@un", unidadeId));
        }
    }
}

using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Cadastro de produtos (tabela produto).</summary>
    public static class ProdutoRepositorio
    {
        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT id, nome, categoria, preco_venda, unidade_medida, estoque_minimo, status " +
                "FROM produto ORDER BY nome");
        }

        public static uint Inserir(
            string nome,
            string categoria,
            decimal preco,
            string unidadeMedida,
            decimal estoqueMinimo)
        {
            object id = BancoDados.Escalar(
                "INSERT INTO produto (nome, categoria, preco_venda, unidade_medida, estoque_minimo) " +
                "VALUES (@nome, @categoria, @preco, @medida, @minimo); " +
                "SELECT LAST_INSERT_ID();",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@categoria", categoria),
                BancoDados.Parametro("@preco", preco),
                BancoDados.Parametro("@medida", unidadeMedida),
                BancoDados.Parametro("@minimo", estoqueMinimo));

            return Convert.ToUInt32(id);
        }

        public static void Atualizar(
            uint id,
            string nome,
            string categoria,
            decimal preco,
            string unidadeMedida,
            decimal estoqueMinimo,
            bool ativo)
        {
            BancoDados.Executar(
                "UPDATE produto SET nome = @nome, categoria = @categoria, preco_venda = @preco, " +
                "unidade_medida = @medida, estoque_minimo = @minimo, status = @status WHERE id = @id",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@categoria", categoria),
                BancoDados.Parametro("@preco", preco),
                BancoDados.Parametro("@medida", unidadeMedida),
                BancoDados.Parametro("@minimo", estoqueMinimo),
                BancoDados.Parametro("@status", ativo ? "Ativo" : "Inativo"),
                BancoDados.Parametro("@id", id));
        }

        public static void Excluir(uint id)
        {
            BancoDados.Executar(
                "DELETE FROM produto WHERE id = @id",
                BancoDados.Parametro("@id", id));
        }

        /// <summary>True se o produto já aparece em movimentação, perda, comanda, venda ou inventário.</summary>
        public static bool PossuiVinculos(uint id)
        {
            object r = BancoDados.Escalar(
                "SELECT (EXISTS (SELECT 1 FROM movimentacao_estoque WHERE produto_id = @id) " +
                "OR EXISTS (SELECT 1 FROM entrada_estoque WHERE produto_id = @id) " +
                "OR EXISTS (SELECT 1 FROM perda WHERE produto_id = @id) " +
                "OR EXISTS (SELECT 1 FROM item_comanda WHERE produto_id = @id) " +
                "OR EXISTS (SELECT 1 FROM item_venda WHERE produto_id = @id) " +
                "OR EXISTS (SELECT 1 FROM item_inventario WHERE produto_id = @id))",
                BancoDados.Parametro("@id", id));

            return Convert.ToInt32(r) != 0;
        }
    }
}

using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Cadastro de fornecedores (tabela fornecedor).</summary>
    public static class FornecedorRepositorio
    {
        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT id, nome, cnpj, telefone, status FROM fornecedor ORDER BY nome");
        }

        public static uint Inserir(string nome, string cnpj, string telefone)
        {
            object id = BancoDados.Escalar(
                "INSERT INTO fornecedor (nome, cnpj, telefone) VALUES (@nome, @cnpj, @telefone); " +
                "SELECT LAST_INSERT_ID();",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@cnpj", cnpj),
                BancoDados.Parametro("@telefone", VazioParaNulo(telefone)));

            return Convert.ToUInt32(id);
        }

        public static void Atualizar(uint id, string nome, string cnpj, string telefone, bool ativo)
        {
            BancoDados.Executar(
                "UPDATE fornecedor SET nome = @nome, cnpj = @cnpj, telefone = @telefone, status = @status " +
                "WHERE id = @id",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@cnpj", cnpj),
                BancoDados.Parametro("@telefone", VazioParaNulo(telefone)),
                BancoDados.Parametro("@status", ativo ? "Ativo" : "Inativo"),
                BancoDados.Parametro("@id", id));
        }

        public static void Excluir(uint id)
        {
            BancoDados.Executar(
                "DELETE FROM fornecedor WHERE id = @id",
                BancoDados.Parametro("@id", id));
        }

        /// <summary>True se o fornecedor já aparece em alguma entrada de estoque.</summary>
        public static bool PossuiVinculos(uint id)
        {
            object r = BancoDados.Escalar(
                "SELECT EXISTS (SELECT 1 FROM entrada_estoque WHERE fornecedor_id = @id)",
                BancoDados.Parametro("@id", id));

            return Convert.ToInt32(r) != 0;
        }

        private static string VazioParaNulo(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        }
    }
}

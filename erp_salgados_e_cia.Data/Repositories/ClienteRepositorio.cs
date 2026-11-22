using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Cadastro de clientes (tabela cliente).</summary>
    public static class ClienteRepositorio
    {
        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT id, nome, cpf_cnpj, telefone, status FROM cliente ORDER BY nome");
        }

        public static uint Inserir(string nome, string cpfCnpj, string telefone)
        {
            object id = BancoDados.Escalar(
                "INSERT INTO cliente (nome, cpf_cnpj, telefone) VALUES (@nome, @cpf, @telefone); " +
                "SELECT LAST_INSERT_ID();",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@cpf", VazioParaNulo(cpfCnpj)),
                BancoDados.Parametro("@telefone", VazioParaNulo(telefone)));

            return Convert.ToUInt32(id);
        }

        public static void Atualizar(uint id, string nome, string cpfCnpj, string telefone, bool ativo)
        {
            BancoDados.Executar(
                "UPDATE cliente SET nome = @nome, cpf_cnpj = @cpf, telefone = @telefone, status = @status " +
                "WHERE id = @id",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@cpf", VazioParaNulo(cpfCnpj)),
                BancoDados.Parametro("@telefone", VazioParaNulo(telefone)),
                BancoDados.Parametro("@status", ativo ? "Ativo" : "Inativo"),
                BancoDados.Parametro("@id", id));
        }

        public static void Excluir(uint id)
        {
            BancoDados.Executar(
                "DELETE FROM cliente WHERE id = @id",
                BancoDados.Parametro("@id", id));
        }

        /// <summary>True se o cliente já aparece em comanda, venda, notinha ou abatimento.</summary>
        public static bool PossuiVinculos(uint id)
        {
            object r = BancoDados.Escalar(
                "SELECT (EXISTS (SELECT 1 FROM comanda WHERE cliente_id = @id) " +
                "OR EXISTS (SELECT 1 FROM venda WHERE cliente_id = @id) " +
                "OR EXISTS (SELECT 1 FROM notinha WHERE cliente_id = @id) " +
                "OR EXISTS (SELECT 1 FROM abatimento_notinha WHERE cliente_id = @id))",
                BancoDados.Parametro("@id", id));

            return Convert.ToInt32(r) != 0;
        }

        private static string VazioParaNulo(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
        }
    }
}

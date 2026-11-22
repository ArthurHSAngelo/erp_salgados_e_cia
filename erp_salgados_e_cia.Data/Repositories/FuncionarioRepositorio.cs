using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Cadastro de funcionários (tabela funcionario).</summary>
    public static class FuncionarioRepositorio
    {
        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT f.id, f.nome, f.unidade_id, u.nome AS unidade_nome, f.status " +
                "FROM funcionario f JOIN unidade u ON u.id = f.unidade_id " +
                "ORDER BY f.nome");
        }

        public static uint Inserir(string nome, uint unidadeId)
        {
            object id = BancoDados.Escalar(
                "INSERT INTO funcionario (nome, unidade_id) VALUES (@nome, @unidade); " +
                "SELECT LAST_INSERT_ID();",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@unidade", unidadeId));

            return Convert.ToUInt32(id);
        }

        public static void Atualizar(uint id, string nome, uint unidadeId, bool ativo)
        {
            BancoDados.Executar(
                "UPDATE funcionario SET nome = @nome, unidade_id = @unidade, status = @status WHERE id = @id",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@unidade", unidadeId),
                BancoDados.Parametro("@status", ativo ? "Ativo" : "Inativo"),
                BancoDados.Parametro("@id", id));
        }

        public static void Excluir(uint id)
        {
            BancoDados.Executar(
                "DELETE FROM funcionario WHERE id = @id",
                BancoDados.Parametro("@id", id));
        }

        /// <summary>True se o funcionário já foi responsável por caixa, entrada, perda ou inventário.</summary>
        public static bool PossuiVinculos(uint id)
        {
            object r = BancoDados.Escalar(
                "SELECT (EXISTS (SELECT 1 FROM caixa WHERE responsavel_funcionario_id = @id) " +
                "OR EXISTS (SELECT 1 FROM entrada_estoque WHERE responsavel_funcionario_id = @id) " +
                "OR EXISTS (SELECT 1 FROM perda WHERE responsavel_funcionario_id = @id) " +
                "OR EXISTS (SELECT 1 FROM inventario WHERE responsavel_funcionario_id = @id))",
                BancoDados.Parametro("@id", id));

            return Convert.ToInt32(r) != 0;
        }
    }
}

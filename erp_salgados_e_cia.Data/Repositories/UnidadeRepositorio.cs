using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;
using erp_salgados_e_cia.Domain.Entities;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Consultas de unidades usadas pela sessão (tabela unidade).</summary>
    public static class UnidadeRepositorio
    {
        /// <summary>Busca uma unidade pelo nome. Devolve null se não existir.</summary>
        public static UnidadeResumo PorNome(string nome)
        {
            return Primeira(
                "SELECT id, nome FROM unidade WHERE nome = @nome LIMIT 1",
                BancoDados.Parametro("@nome", nome));
        }

        /// <summary>Primeira unidade ativa em ordem alfabética (unidade inicial de Gerente e Administrador).</summary>
        public static UnidadeResumo PrimeiraAtiva()
        {
            return Primeira("SELECT id, nome FROM unidade WHERE status = 'Ativo' ORDER BY nome LIMIT 1");
        }

        // ---- Cadastro de unidades (tela Unidades) ----

        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT id, nome, cnpj, endereco, numero, status FROM unidade ORDER BY nome");
        }

        public static uint Inserir(string nome, string cnpj, string endereco, string numero)
        {
            object id = BancoDados.Escalar(
                "INSERT INTO unidade (nome, cnpj, endereco, numero) " +
                "VALUES (@nome, @cnpj, @endereco, @numero); " +
                "SELECT LAST_INSERT_ID();",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@cnpj", cnpj),
                BancoDados.Parametro("@endereco", endereco),
                BancoDados.Parametro("@numero", numero));

            return Convert.ToUInt32(id);
        }

        public static void Atualizar(uint id, string nome, string cnpj, string endereco, string numero, bool ativo)
        {
            BancoDados.Executar(
                "UPDATE unidade SET nome = @nome, cnpj = @cnpj, endereco = @endereco, numero = @numero, " +
                "status = @status WHERE id = @id",
                BancoDados.Parametro("@nome", nome),
                BancoDados.Parametro("@cnpj", cnpj),
                BancoDados.Parametro("@endereco", endereco),
                BancoDados.Parametro("@numero", numero),
                BancoDados.Parametro("@status", ativo ? "Ativo" : "Inativo"),
                BancoDados.Parametro("@id", id));
        }

        public static void Excluir(uint id)
        {
            BancoDados.Executar(
                "DELETE FROM unidade WHERE id = @id",
                BancoDados.Parametro("@id", id));
        }

        /// <summary>
        /// True se a unidade já tem login, funcionário ou qualquer movimento
        /// (o saldo de estoque zerado não conta: ele é apagado junto).
        /// </summary>
        public static bool PossuiVinculos(uint id)
        {
            object r = BancoDados.Escalar(
                "SELECT (EXISTS (SELECT 1 FROM usuario WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM funcionario WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM movimentacao_estoque WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM entrada_estoque WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM perda WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM caixa WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM comanda WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM venda WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM notinha WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM abatimento_notinha WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM inventario WHERE unidade_id = @id) " +
                "OR EXISTS (SELECT 1 FROM log_operacao WHERE unidade_id = @id))",
                BancoDados.Parametro("@id", id));

            return Convert.ToInt32(r) != 0;
        }

        private static UnidadeResumo Primeira(string sql, params MySql.Data.MySqlClient.MySqlParameter[] parametros)
        {
            DataTable tabela = BancoDados.Consultar(sql, parametros);

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            return new UnidadeResumo
            {
                Id = Convert.ToUInt32(tabela.Rows[0]["id"]),
                Nome = Convert.ToString(tabela.Rows[0]["nome"])
            };
        }
    }
}

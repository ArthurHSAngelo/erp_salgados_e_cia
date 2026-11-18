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

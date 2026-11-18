using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;
using erp_salgados_e_cia.Domain.Entities;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Consultas de login (tabela usuario).</summary>
    public static class UsuarioRepositorio
    {
        /// <summary>
        /// Busca o login informado (sem diferenciar maiúsculas de minúsculas).
        /// Devolve null se não existir.
        /// </summary>
        public static UsuarioAcesso BuscarPorLogin(string login)
        {
            DataTable tabela = BancoDados.Consultar(
                "SELECT u.id, u.login, u.senha_hash, u.perfil, u.status, u.unidade_id, un.nome AS unidade_nome " +
                "FROM usuario u LEFT JOIN unidade un ON un.id = u.unidade_id " +
                "WHERE u.login = @login LIMIT 1",
                BancoDados.Parametro("@login", (login ?? "").Trim()));

            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];

            return new UsuarioAcesso
            {
                Id = Convert.ToUInt32(linha["id"]),
                Login = Convert.ToString(linha["login"]),
                SenhaHash = Convert.ToString(linha["senha_hash"]),
                Perfil = Convert.ToString(linha["perfil"]),
                Ativo = Convert.ToString(linha["status"]) == "Ativo",
                UnidadeId = linha["unidade_id"] == DBNull.Value ? (uint?)null : Convert.ToUInt32(linha["unidade_id"]),
                UnidadeNome = linha["unidade_nome"] == DBNull.Value ? null : Convert.ToString(linha["unidade_nome"])
            };
        }
    }
}

using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;
using erp_salgados_e_cia.Domain.Entities;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Consultas de login (tabela usuario).</summary>
    public static class UsuarioRepositorio
    {
        // ---- Cadastro de logins (tela Usuários) ----

        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT u.id, u.login, u.senha_hash, u.perfil, u.status, u.unidade_id, un.nome AS unidade_nome " +
                "FROM usuario u LEFT JOIN unidade un ON un.id = u.unidade_id " +
                "ORDER BY u.login");
        }

        public static uint Inserir(string login, string senhaHash, string perfil, uint? unidadeId)
        {
            object id = BancoDados.Escalar(
                "INSERT INTO usuario (login, senha_hash, perfil, unidade_id) " +
                "VALUES (@login, @hash, @perfil, @unidade); " +
                "SELECT LAST_INSERT_ID();",
                BancoDados.Parametro("@login", login),
                BancoDados.Parametro("@hash", senhaHash),
                BancoDados.Parametro("@perfil", perfil),
                BancoDados.Parametro("@unidade", unidadeId.HasValue ? (object)unidadeId.Value : null));

            return Convert.ToUInt32(id);
        }

        /// <summary>Atualiza o login. Se novaSenhaHash for null, a senha atual é mantida.</summary>
        public static void Atualizar(uint id, string login, string perfil, uint? unidadeId, bool ativo, string novaSenhaHash)
        {
            BancoDados.Executar(
                "UPDATE usuario SET login = @login, perfil = @perfil, unidade_id = @unidade, " +
                "status = @status, senha_hash = COALESCE(@hash, senha_hash), " +
                // trocar a senha também desbloqueia o login
                "tentativas_falhas = IF(@hash IS NULL, tentativas_falhas, 0), " +
                "bloqueado_ate = IF(@hash IS NULL, bloqueado_ate, NULL) WHERE id = @id",
                BancoDados.Parametro("@login", login),
                BancoDados.Parametro("@perfil", perfil),
                BancoDados.Parametro("@unidade", unidadeId.HasValue ? (object)unidadeId.Value : null),
                BancoDados.Parametro("@status", ativo ? "Ativo" : "Inativo"),
                BancoDados.Parametro("@hash", novaSenhaHash),
                BancoDados.Parametro("@id", id));
        }

        /// <summary>
        /// Busca o login informado (sem diferenciar maiúsculas de minúsculas).
        /// Devolve null se não existir.
        /// </summary>
        public static UsuarioAcesso BuscarPorLogin(string login)
        {
            DataTable tabela = BancoDados.Consultar(
                "SELECT u.id, u.login, u.senha_hash, u.perfil, u.status, u.unidade_id, un.nome AS unidade_nome, " +
                "u.tentativas_falhas, " +
                "IF(u.bloqueado_ate IS NOT NULL AND u.bloqueado_ate > NOW(), " +
                "CEIL(TIMESTAMPDIFF(SECOND, NOW(), u.bloqueado_ate) / 60), 0) AS minutos_bloqueio " +
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
                UnidadeNome = linha["unidade_nome"] == DBNull.Value ? null : Convert.ToString(linha["unidade_nome"]),
                TentativasFalhas = Convert.ToInt32(linha["tentativas_falhas"]),
                MinutosBloqueio = Convert.ToInt32(linha["minutos_bloqueio"])
            };
        }

        /// <summary>
        /// Conta mais uma senha errada para o login (no banco). Depois de 5 seguidas, o login é
        /// bloqueado por 15 minutos. Devolve os minutos que faltam para liberar (0 se não bloqueou).
        /// </summary>
        public static int RegistrarFalhaLogin(string login)
        {
            DataTable resultado = BancoDados.Consultar(
                "CALL sp_login_falhou(@login)",
                BancoDados.Parametro("@login", (login ?? "").Trim()));

            if (resultado.Rows.Count == 0 || Convert.ToInt32(resultado.Rows[0]["bloqueado"]) == 0)
            {
                return 0;
            }

            return Convert.ToInt32(resultado.Rows[0]["minutos"]);
        }

        /// <summary>Zera a contagem de senhas erradas depois de um login certo.</summary>
        public static void ZerarFalhasLogin(string login)
        {
            BancoDados.Executar(
                "CALL sp_login_ok(@login)",
                BancoDados.Parametro("@login", (login ?? "").Trim()));
        }
    }
}

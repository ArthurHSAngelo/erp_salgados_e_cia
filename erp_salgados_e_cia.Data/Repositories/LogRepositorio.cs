using System;
using System.Collections.Generic;
using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>
    /// Log de operações. As operações são gravadas pelas triggers e procedures do banco; daqui o
    /// programa só registra a entrada e a saída do sistema e consulta o histórico.
    /// </summary>
    public static class LogRepositorio
    {
        public static void RegistrarLogin(uint usuarioId, string login)
        {
            Registrar("LOGIN", usuarioId, login);
        }

        public static void RegistrarLogout(uint usuarioId, string login)
        {
            Registrar("LOGOUT", usuarioId, login);
        }

        private static void Registrar(string operacao, uint usuarioId, string login)
        {
            BancoDados.Executar(
                "CALL sp_registrar_log(@op, 'usuario', @id, @login, NULL)",
                BancoDados.Parametro("@op", operacao),
                BancoDados.Parametro("@id", usuarioId),
                BancoDados.Parametro("@login", login));
        }

        /// <summary>
        /// Consulta o log de uma unidade no período. Operação usa os nomes do banco
        /// (INSERT, UPDATE, DELETE, LOGIN, LOGOUT). Filtros nulos não restringem.
        /// </summary>
        public static DataTable Consultar(
            uint unidadeId,
            DateTime inicio,
            DateTime fim,
            string usuario,
            string operacao,
            string tabela)
        {
            return BancoDados.Consultar(
                "SELECT l.id, l.data_hora, u.login AS usuario, l.operacao, l.tabela, l.registro, l.detalhes, un.nome AS unidade " +
                "FROM log_operacao l " +
                "LEFT JOIN usuario u ON u.id = l.usuario_id " +
                "LEFT JOIN unidade un ON un.id = l.unidade_id " +
                "WHERE l.unidade_id = @un " +
                "AND l.data_hora >= @ini AND l.data_hora < DATE_ADD(@fim, INTERVAL 1 DAY) " +
                "AND (@usuario IS NULL OR u.login = @usuario) " +
                "AND (@operacao IS NULL OR l.operacao = @operacao) " +
                "AND (@tabela IS NULL OR l.tabela = @tabela) " +
                "ORDER BY l.data_hora DESC, l.id DESC",
                BancoDados.Parametro("@un", unidadeId),
                BancoDados.Parametro("@ini", inicio.Date),
                BancoDados.Parametro("@fim", fim.Date),
                BancoDados.Parametro("@usuario", usuario),
                BancoDados.Parametro("@operacao", operacao),
                BancoDados.Parametro("@tabela", tabela));
        }

        /// <summary>Os registros mais recentes da unidade (usado no painel).</summary>
        public static DataTable Ultimos(uint unidadeId, int quantidade)
        {
            return BancoDados.Consultar(
                "SELECT l.id, l.data_hora, u.login AS usuario, l.operacao, l.tabela, l.registro, l.detalhes, un.nome AS unidade " +
                "FROM log_operacao l " +
                "LEFT JOIN usuario u ON u.id = l.usuario_id " +
                "LEFT JOIN unidade un ON un.id = l.unidade_id " +
                "WHERE l.unidade_id = @un " +
                "ORDER BY l.data_hora DESC, l.id DESC LIMIT " + Convert.ToInt32(quantidade),
                BancoDados.Parametro("@un", unidadeId));
        }

        /// <summary>Logins que aparecem no log da unidade e logins que podem trabalhar nela.</summary>
        public static List<string> Usuarios(uint unidadeId)
        {
            DataTable tabela = BancoDados.Consultar(
                "SELECT u.login FROM log_operacao l JOIN usuario u ON u.id = l.usuario_id WHERE l.unidade_id = @un " +
                "UNION " +
                "SELECT login FROM usuario WHERE unidade_id IS NULL OR unidade_id = @un " +
                "ORDER BY 1",
                BancoDados.Parametro("@un", unidadeId));

            List<string> logins = new List<string>();

            foreach (DataRow linha in tabela.Rows)
            {
                logins.Add(Convert.ToString(linha["login"]));
            }

            return logins;
        }

        /// <summary>Tabelas que aparecem no log da unidade.</summary>
        public static List<string> Tabelas(uint unidadeId)
        {
            DataTable tabela = BancoDados.Consultar(
                "SELECT DISTINCT tabela FROM log_operacao WHERE unidade_id = @un ORDER BY tabela",
                BancoDados.Parametro("@un", unidadeId));

            List<string> nomes = new List<string>();

            foreach (DataRow linha in tabela.Rows)
            {
                nomes.Add(Convert.ToString(linha["tabela"]));
            }

            return nomes;
        }
    }
}

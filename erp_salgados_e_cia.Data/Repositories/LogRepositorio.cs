using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>
    /// Registros no log que o banco não enxerga sozinho (entrada e saída do sistema).
    /// As demais operações são gravadas pelas triggers.
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
    }
}

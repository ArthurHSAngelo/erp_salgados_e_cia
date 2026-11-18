using System;

namespace erp_salgados_e_cia.Domain.Exceptions
{
    /// <summary>
    /// Erro "esperado" que o usuário precisa ler: uma regra de negócio recusou a
    /// operação (vem das procedures/triggers do banco) ou o banco está indisponível.
    /// A tela captura este erro e mostra a Message em um MessageBox.
    /// </summary>
    public class RegraNegocioException : Exception
    {
        public RegraNegocioException(string mensagem) : base(mensagem) { }

        public RegraNegocioException(string mensagem, Exception interna) : base(mensagem, interna) { }
    }
}

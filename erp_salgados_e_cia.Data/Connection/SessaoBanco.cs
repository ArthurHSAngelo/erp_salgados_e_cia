namespace erp_salgados_e_cia.Data.Connection
{
    /// <summary>
    /// Estado da sessão que o banco precisa conhecer. Ao abrir cada conexão,
    /// BancoDados executa SET @usuario_id e SET @unidade_id com estes valores,
    /// para o log registrar QUEM fez a operação e EM QUAL unidade.
    /// Administrador e Gerente não têm unidade fixa: UnidadeId vem da unidade
    /// de trabalho escolhida na tela.
    /// </summary>
    public static class SessaoBanco
    {
        public static uint? UsuarioId { get; set; }

        public static uint? UnidadeId { get; set; }

        public static void Limpar()
        {
            UsuarioId = null;
            UnidadeId = null;
        }
    }
}

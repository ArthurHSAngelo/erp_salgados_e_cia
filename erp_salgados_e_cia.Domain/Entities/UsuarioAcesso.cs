namespace erp_salgados_e_cia.Domain.Entities
{
    /// <summary>
    /// Dados de um login lidos do banco para autenticar e montar a sessão.
    /// UnidadeId/UnidadeNome ficam vazios para Administrador e Gerente.
    /// </summary>
    public class UsuarioAcesso
    {
        public uint Id { get; set; }

        public string Login { get; set; }

        public string SenhaHash { get; set; }

        /// <summary>"Administrador", "Gerente" ou "Atendente".</summary>
        public string Perfil { get; set; }

        public bool Ativo { get; set; }

        public uint? UnidadeId { get; set; }

        public string UnidadeNome { get; set; }
    }
}

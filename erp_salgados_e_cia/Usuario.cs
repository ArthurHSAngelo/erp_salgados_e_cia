using System.ComponentModel;

namespace erp_salgados_e_cia
{
    public enum PerfilUsuario
    {
        Administrador,
        Gerente,
        Atendente
    }

    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        [Browsable(false)]
        public string SenhaHash { get; set; }

        public PerfilUsuario Perfil { get; set; }

        public string Unidade { get; set; }

        public dadossistema.StatusCadastro Status { get; set; }
    }
}

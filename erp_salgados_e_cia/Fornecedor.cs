using static erp_salgados_e_cia.dadossistema;

namespace erp_salgados_e_cia
{
    public class Fornecedor
    {
        [System.ComponentModel.Browsable(false)]
        public uint Id { get; set; }

        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public string Telefone { get; set; }
        public StatusCadastro Status { get; set; }
    }
}
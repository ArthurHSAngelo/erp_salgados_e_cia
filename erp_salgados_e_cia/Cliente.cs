using static erp_salgados_e_cia.dadossistema;

namespace erp_salgados_e_cia
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string Nome { get; set; }

        public string CpfCnpj { get; set; }

        public string Telefone { get; set; }

        public StatusCadastro Status { get; set; }

        public string Descricao
        {
            get
            {
                return string.IsNullOrWhiteSpace(CpfCnpj)
                    ? Nome
                    : $"{Nome} ({CpfCnpj})";
            }
        }

        public decimal SaldoNotinha
        {
            get
            {
                return dadossistema.SaldoNotinha(IdCliente);
            }
        }
    }
}
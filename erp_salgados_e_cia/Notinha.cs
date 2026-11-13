using System;

namespace erp_salgados_e_cia
{
    public class Notinha
    {
        public int IdNotinha { get; set; }

        public int IdCliente { get; set; }

        public int IdVenda { get; set; }

        public DateTime DataHora { get; set; }

        public decimal Valor { get; set; }

        public string Unidade { get; set; }
    }
}
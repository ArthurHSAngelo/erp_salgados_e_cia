using System;

namespace erp_salgados_e_cia
{
    public class AbatimentoNotinha
    {
        public int IdAbatimento { get; set; }

        public int IdCliente { get; set; }

        public DateTime DataHora { get; set; }

        public decimal Valor { get; set; }

        public string FormaPagamento { get; set; }

        public int IdCaixa { get; set; }
    }
}
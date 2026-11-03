using System;

namespace erp_salgados_e_cia
{
    public class MovimentoNotinha
    {
        public DateTime DataHora { get; set; }

        public string Tipo { get; set; }

        public decimal Valor { get; set; }

        public string Detalhe { get; set; }
    }
}
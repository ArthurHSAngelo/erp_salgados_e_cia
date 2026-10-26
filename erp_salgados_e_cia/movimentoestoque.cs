using System;

namespace erp_salgados_e_cia
{
    public class movimentoestoque
    {
        public string Produto { get; set; }

        public decimal Quantidade { get; set; }

        public decimal CustoUnitario { get; set; }

        public string Fornecedor { get; set; }

        public string Responsavel { get; set; }

        public string Unidade { get; set; }

        public DateTime DataEntrada { get; set; }
    }
}
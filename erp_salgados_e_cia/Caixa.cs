using System;
using System.ComponentModel;

namespace erp_salgados_e_cia
{
    public class Caixa
    {
        public DateTime DataAbertura { get; set; }

        public DateTime? DataFechamento { get; set; }

        public string Responsavel { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal TotalVendas
        {
            get
            {
                return dadossistema.TotalVendasDoCaixa(IdCaixa);
            }
        }

        public decimal TotalNotinha
        {
            get
            {
                return dadossistema.TotalNotinhaDoCaixa(IdCaixa);
            }
        }

        public decimal TotalAbatimentos
        {
            get
            {
                return dadossistema.TotalAbatimentosDoCaixa(IdCaixa);
            }
        }

        public decimal DinheiroEsperado
        {
            get
            {
                return ValorInicial +
                    dadossistema.TotalRecebidoPorForma(
                        IdCaixa,
                        "Dinheiro");
            }
        }

        public decimal? ValorContado { get; set; }

        public decimal? Diferenca
        {
            get
            {
                return ValorContado.HasValue
                    ? ValorContado.Value - DinheiroEsperado
                    : (decimal?)null;
            }
        }

        public string Status { get; set; }

        public string Unidade { get; set; }

        [Browsable(false)]
        public int IdCaixa { get; set; }
    }
}
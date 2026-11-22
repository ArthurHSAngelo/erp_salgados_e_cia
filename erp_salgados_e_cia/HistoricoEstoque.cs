using System;

namespace erp_salgados_e_cia
{
    public enum TipoMovimentoEstoque
    {
        Entrada,
        Venda,
        Estorno,
        Perda,
        Ajuste
    }

    public class HistoricoEstoque
    {
        public DateTime Data { get; set; }

        public string Produto { get; set; }

        public TipoMovimentoEstoque Tipo { get; set; }

        public decimal Quantidade { get; set; }

        public decimal SaldoApos { get; set; }

        public string Unidade { get; set; }

        public string Responsavel { get; set; }

        public string Origem { get; set; }
    }
}
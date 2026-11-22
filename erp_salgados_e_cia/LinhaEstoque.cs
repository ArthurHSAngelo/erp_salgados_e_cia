namespace erp_salgados_e_cia
{
    public class LinhaEstoque
    {
        public string Produto { get; set; }

        public string Categoria { get; set; }

        public string UnidadeMedida { get; set; }

        public decimal EstoqueAtual { get; set; }

        public decimal EstoqueMinimo { get; set; }

        public string Situacao { get; set; }

        public decimal PrecoVenda { get; set; }

        public decimal? UltimoCusto { get; set; }

        public decimal ValorACusto { get; set; }

        public decimal ValorAPrecoVenda { get; set; }

        public string Status { get; set; }
    }
}
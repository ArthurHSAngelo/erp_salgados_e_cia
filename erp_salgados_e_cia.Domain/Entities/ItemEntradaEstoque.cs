namespace erp_salgados_e_cia.Domain.Entities
{
    /// <summary>Uma linha de uma entrada de estoque (produto, quantidade e custo).</summary>
    public class ItemEntradaEstoque
    {
        public uint ProdutoId { get; set; }

        public decimal Quantidade { get; set; }

        public decimal CustoUnitario { get; set; }
    }
}

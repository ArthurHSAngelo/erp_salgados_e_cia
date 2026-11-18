namespace erp_salgados_e_cia.Domain.Entities
{
    /// <summary>Um item de pedido (produto, quantidade e o preço da hora).</summary>
    public class ItemPedido
    {
        public uint ProdutoId { get; set; }

        public decimal Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }
    }
}

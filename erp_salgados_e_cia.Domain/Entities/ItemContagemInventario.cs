namespace erp_salgados_e_cia.Domain.Entities
{
    /// <summary>Uma linha da contagem de inventário (saldo visto na tela, contagem e motivo).</summary>
    public class ItemContagemInventario
    {
        public uint ProdutoId { get; set; }

        public decimal SaldoSistema { get; set; }

        public decimal QuantidadeContada { get; set; }

        public string Motivo { get; set; }
    }
}

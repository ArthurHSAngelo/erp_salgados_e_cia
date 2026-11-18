namespace erp_salgados_e_cia.Domain.Entities
{
    /// <summary>Um pagamento a gravar. A forma usa os nomes do banco: Dinheiro, Pix, Credito, Debito, Notinha.</summary>
    public class PagamentoNovo
    {
        public string Forma { get; set; }

        public decimal Valor { get; set; }
    }
}

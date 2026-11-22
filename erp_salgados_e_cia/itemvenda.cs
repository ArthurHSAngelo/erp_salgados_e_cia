namespace erp_salgados_e_cia
{
    public class ItemVenda
    {
        public string Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Total
        {
            get
            {
                return Quantidade * PrecoUnitario;
            }
        }
    }
}
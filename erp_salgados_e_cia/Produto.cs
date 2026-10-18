namespace erp_salgados_e_cia
{
    public class Produto
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public decimal EstoqueMinimo { get; set; }
        public string UnidadeMedida { get; set; }
    }
}
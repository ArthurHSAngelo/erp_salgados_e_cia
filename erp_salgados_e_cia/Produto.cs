using static erp_salgados_e_cia.dadossistema;

public class Produto
{
    [System.ComponentModel.Browsable(false)]
    public uint Id { get; set; }

    public string Nome { get; set; }

    public string Categoria { get; set; }

    public decimal Preco { get; set; }

    public decimal EstoqueMinimo { get; set; }

    // Saldo da unidade de trabalho, lido do banco por CarregarProdutos().
    public decimal EstoqueAtual { get; set; }

    public string UnidadeMedida { get; set; }
    public StatusCadastro Status { get; set; }
}
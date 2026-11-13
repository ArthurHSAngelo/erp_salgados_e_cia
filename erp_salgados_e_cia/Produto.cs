using static erp_salgados_e_cia.dadossistema;

public class Produto
{
    public string Nome { get; set; }

    public string Categoria { get; set; }

    public decimal Preco { get; set; }

    public decimal EstoqueMinimo { get; set; }

    public decimal EstoqueAtual
    {
        get { return SaldoDoProduto(this, UnidadeAtual); }
        set { DefinirSaldoDoProduto(this, UnidadeAtual, value); }
    }

    public string UnidadeMedida { get; set; }
    public StatusCadastro Status { get; set; }
}
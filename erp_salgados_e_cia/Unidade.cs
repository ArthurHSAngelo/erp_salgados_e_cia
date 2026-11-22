using static erp_salgados_e_cia.dadossistema;

public class Unidade
{
    [System.ComponentModel.Browsable(false)]
    public uint Id { get; set; }

    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public string Endereco { get; set; }
    public string Numero { get; set; }
    public bool AceitaValeAlimentacao { get; set; }
    public StatusCadastro Status { get; set; }
}
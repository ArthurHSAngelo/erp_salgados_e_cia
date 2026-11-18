using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>Saldos de estoque por unidade (tabela estoque).</summary>
    public static class EstoqueRepositorio
    {
        public static DataTable Listar()
        {
            return BancoDados.Consultar(
                "SELECT unidade_id, produto_id, quantidade FROM estoque");
        }
    }
}

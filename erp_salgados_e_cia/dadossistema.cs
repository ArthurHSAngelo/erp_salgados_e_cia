using System.Collections.Generic;
using System.ComponentModel;

namespace erp_salgados_e_cia
{
    public static class dadossistema
    {
        public static BindingList<Produto> produtos =
            new BindingList<Produto>();

        public static BindingList<Fornecedor> fornecedores =
            new BindingList<Fornecedor>();

        public static BindingList<Unidade> unidades =
            new BindingList<Unidade>();

        public enum StatusCadastro
        {
            Inativo = 0,
            Ativo = 1
        }
    }
}
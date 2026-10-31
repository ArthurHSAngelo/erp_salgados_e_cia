using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace erp_salgados_e_cia
{
    public static class dadossistema
    {
        public static bool ProdutoPossuiVinculos(Produto produto)
        {
            return produto != null &&
                   movimentacoes.Any(m =>
                       m.Produto == produto.Nome);
        }

        public static bool FornecedorPossuiVinculos(
            Fornecedor fornecedor)
        {
            return fornecedor != null &&
                   movimentacoes.Any(m =>
                       m.Fornecedor == fornecedor.Nome);
        }

        public static bool UnidadePossuiVinculos(
            Unidade unidade)
        {
            return unidade != null &&
                   movimentacoes.Any(m =>
                       m.Unidade == unidade.Nome);
        }

        public static BindingList<movimentoestoque> movimentacoes =
            new BindingList<movimentoestoque>();
        public static BindingList<Venda> vendas =
            new BindingList<Venda>();
        public static BindingList<Produto> produtos =
            new BindingList<Produto>();

        public static BindingList<Fornecedor> fornecedores =
            new BindingList<Fornecedor>();

        public static BindingList<Unidade> unidades =
            new BindingList<Unidade>();

        static dadossistema()
        {
            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Coxinha",
                Preco = 5.00m,
                Categoria = "Salgados",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 10,
                EstoqueAtual = 0
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Bolinha de queijo",
                Preco = 4.50m,
                Categoria = "Salgados",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 10,
                EstoqueAtual = 0
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Refrigerante",
                Preco = 6.00m,
                Categoria = "Bebidas",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 5,
                EstoqueAtual = 0
            });

            fornecedores.Add(new Fornecedor
            {
                Status = StatusCadastro.Ativo,
                Nome = "Distribuidora Central",
                Cnpj = "12.345.678/0001-90",
                Telefone = "(11) 3333-4444"
            });

            fornecedores.Add(new Fornecedor
            {
                Status = StatusCadastro.Ativo,
                Nome = "Fornecedor Bom Sabor",
                Cnpj = "98.765.432/0001-10",
                Telefone = "(11) 99999-8888"
            });
            unidades.Add(new Unidade
            {
                Status = StatusCadastro.Ativo,
                Nome = "Lanchonete Centro",
                Cnpj = "11.222.333/0001-44",
                Endereco = "Rua Principal",
                Numero = "100"
            });
        }

        public enum StatusCadastro
        {
            Inativo = 0,
            Ativo = 1
        }
    }
}
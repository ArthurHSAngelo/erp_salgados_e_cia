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
                   (movimentacoes.Any(m =>
                       m.Produto == produto.Nome) ||
                    vendas.Any(v =>
                       v.Itens.Any(item =>
                           item.Produto == produto.Nome)) ||
                    comandas.Any(c =>
                       c.Itens.Any(item =>
                           item.Produto == produto.Nome)));
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

        public static bool ClientePossuiVinculos(
            Cliente cliente)
        {
            return cliente != null &&
                   (vendas.Any(v =>
                       v.IdCliente == cliente.IdCliente) ||
                    comandas.Any(c =>
                       c.IdCliente == cliente.IdCliente) ||
                    notinhas.Any(n =>
                       n.IdCliente == cliente.IdCliente) ||
                    abatimentos.Any(a =>
                       a.IdCliente == cliente.IdCliente));
        }

        public static Comanda ComandaDaMesa(int numeroMesa)
        {
            return comandas.FirstOrDefault(c =>
                c.NumeroMesa == numeroMesa);
        }

        public static int ProximoIdNotinha()
        {
            return notinhas.Any()
                ? notinhas.Max(n => n.IdNotinha) + 1
                : 1;
        }

        public static int ProximoIdAbatimento()
        {
            return abatimentos.Any()
                ? abatimentos.Max(a => a.IdAbatimento) + 1
                : 1;
        }

        public static decimal SaldoNotinha(int idCliente)
        {
            return notinhas
                       .Where(n => n.IdCliente == idCliente)
                       .Sum(n => n.Valor)
                   - abatimentos
                       .Where(a => a.IdCliente == idCliente)
                       .Sum(a => a.Valor);
        }

        public static void LancarNotinhaDaVenda(Venda venda)
        {
            if (venda == null || venda.IdCliente == null)
            {
                return;
            }

            decimal valorNotinha =
                venda.Pagamentos
                .Where(pagamento =>
                    pagamento.FormaPagamento == "Notinha")
                .Sum(pagamento => pagamento.Valor);

            if (valorNotinha <= 0)
            {
                return;
            }

            notinhas.Add(
                new Notinha
                {
                    IdNotinha = ProximoIdNotinha(),
                    IdCliente = venda.IdCliente.Value,
                    IdVenda = venda.IdVenda,
                    DataHora = venda.DataHora,
                    Valor = valorNotinha
                }
            );
        }

        public static int ProximoIdCliente()
        {
            return clientes.Any()
                ? clientes.Max(c => c.IdCliente) + 1
                : 1;
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

        public static BindingList<Cliente> clientes =
            new BindingList<Cliente>();

        public static BindingList<Comanda> comandas =
            new BindingList<Comanda>();

        public static BindingList<Notinha> notinhas =
            new BindingList<Notinha>();

        public static BindingList<AbatimentoNotinha> abatimentos =
            new BindingList<AbatimentoNotinha>();

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
                EstoqueAtual = 50
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Bolinha de queijo",
                Preco = 4.50m,
                Categoria = "Salgados",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 10,
                EstoqueAtual = 50
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Refrigerante",
                Preco = 6.00m,
                Categoria = "Bebidas",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 5,
                EstoqueAtual = 50
            });
            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Pastel de carne",
                Preco = 7.00m,
                Categoria = "Salgados",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 10,
                EstoqueAtual = 30
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Suco de laranja",
                Preco = 5.50m,
                Categoria = "Bebidas",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 5,
                EstoqueAtual = 20
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

            clientes.Add(new Cliente
            {
                IdCliente = 1,
                Status = StatusCadastro.Ativo,
                Nome = "Maria Souza",
                CpfCnpj = "123.456.789-09",
                Telefone = "(11) 98888-1111"
            });

            clientes.Add(new Cliente
            {
                IdCliente = 2,
                Status = StatusCadastro.Ativo,
                Nome = "João Pereira",
                Telefone = "(11) 97777-2222"
            });
        }

        public enum StatusCadastro
        {
            Inativo = 0,
            Ativo = 1
        }
    }
}
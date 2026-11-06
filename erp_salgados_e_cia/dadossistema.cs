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

        public const string UnidadeAtual = "Lanchonete Centro";

        public static readonly string[] Responsaveis =
        {
            "Administrador",
            "Gerente",
            "Atendente"
        };

        public static Caixa CaixaAberto()
        {
            return caixas.FirstOrDefault(c =>
                c.Unidade == UnidadeAtual &&
                c.Status == "Aberto");
        }

        public static Caixa CaixaDeHoje()
        {
            return caixas.FirstOrDefault(c =>
                c.Unidade == UnidadeAtual &&
                c.DataAbertura.Date == System.DateTime.Today);
        }

        public static int ProximoIdCaixa()
        {
            return caixas.Any()
                ? caixas.Max(c => c.IdCaixa) + 1
                : 1;
        }

        public static decimal TotalRecebidoPorForma(
            int idCaixa,
            string formaPagamento)
        {
            IEnumerable<Venda> vendasDoCaixa =
                vendas.Where(v =>
                    v.IdCaixa == idCaixa &&
                    v.Status == "Concluída");

            decimal total =
                vendasDoCaixa
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento == formaPagamento)
                .Sum(p => p.Valor);

            if (formaPagamento == "Dinheiro")
            {
                total -= vendasDoCaixa.Sum(v => v.Troco);
            }

            total += abatimentos
                .Where(a =>
                    a.IdCaixa == idCaixa &&
                    a.FormaPagamento == formaPagamento)
                .Sum(a => a.Valor);

            return total;
        }

        public static decimal TotalVendasDoCaixa(int idCaixa)
        {
            IEnumerable<Venda> vendasDoCaixa =
                vendas.Where(v =>
                    v.IdCaixa == idCaixa &&
                    v.Status == "Concluída");

            return vendasDoCaixa
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento != "Notinha")
                .Sum(p => p.Valor)
                - vendasDoCaixa.Sum(v => v.Troco);
        }

        public static decimal TotalNotinhaDoCaixa(int idCaixa)
        {
            return vendas
                .Where(v =>
                    v.IdCaixa == idCaixa &&
                    v.Status == "Concluída")
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento == "Notinha")
                .Sum(p => p.Valor);
        }

        public static decimal TotalAbatimentosDoCaixa(int idCaixa)
        {
            return abatimentos
                .Where(a => a.IdCaixa == idCaixa)
                .Sum(a => a.Valor);
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

        public static decimal? UltimoCustoDoProduto(string nomeProduto)
        {
            var ultima = movimentacoes
                .Select((m, posicao) => new { m, posicao })
                .Where(x => x.m.Produto == nomeProduto)
                .OrderByDescending(x => x.m.DataEntrada)
                .ThenByDescending(x => x.posicao)
                .FirstOrDefault();

            if (ultima == null)
            {
                return null;
            }

            return ultima.m.CustoUnitario;
        }

        public static List<LinhaEstoque> MontarRelatorioEstoque()
        {
            List<LinhaEstoque> linhas = new List<LinhaEstoque>();

            foreach (Produto produto in produtos)
            {
                decimal? ultimoCusto = UltimoCustoDoProduto(produto.Nome);

                linhas.Add(
                    new LinhaEstoque
                    {
                        Produto = produto.Nome,
                        Categoria = produto.Categoria,
                        UnidadeMedida = produto.UnidadeMedida,
                        EstoqueAtual = produto.EstoqueAtual,
                        EstoqueMinimo = produto.EstoqueMinimo,
                        Situacao =
                            produto.EstoqueAtual < produto.EstoqueMinimo
                                ? "Crítico"
                                : "Normal",
                        PrecoVenda = produto.Preco,
                        UltimoCusto = ultimoCusto,
                        ValorACusto =
                            produto.EstoqueAtual * (ultimoCusto ?? 0m),
                        ValorAPrecoVenda =
                            produto.EstoqueAtual * produto.Preco,
                        Status = produto.Status.ToString()
                    }
                );
            }

            return linhas;
        }
        public static void RegistrarHistoricoEstoque(
            Produto produto,
            TipoMovimentoEstoque tipo,
            decimal quantidade,
            string responsavel,
            string origem
        )
        {
            historicoestoque.Add(
                new HistoricoEstoque
                {
                    Data = System.DateTime.Now,
                    Produto = produto.Nome,
                    Tipo = tipo,
                    Quantidade = quantidade,
                    SaldoApos = produto.EstoqueAtual,
                    Unidade = UnidadeAtual,
                    Responsavel = responsavel,
                    Origem = origem
                }
            );
        }
        public static void DefinirPeriodoPadrao(
            System.DateTime? inicio,
            System.DateTime? fim,
            out System.DateTime dataInicial,
            out System.DateTime dataFinal
        )
        {
            dataFinal = (fim ?? System.DateTime.Today).Date;
            dataInicial = (inicio ?? dataFinal.AddDays(-29)).Date;
        }

        public static List<HistoricoEstoque> ConsultarHistoricoEstoque(
            System.DateTime? inicio,
            System.DateTime? fim,
            string unidade,
            string produto,
            TipoMovimentoEstoque? tipo
        )
        {
            System.DateTime dataInicial;
            System.DateTime dataFinal;

            DefinirPeriodoPadrao(
                inicio,
                fim,
                out dataInicial,
                out dataFinal
            );

            return historicoestoque
                .Where(h =>
                    h.Data.Date >= dataInicial &&
                    h.Data.Date <= dataFinal &&
                    (string.IsNullOrEmpty(unidade) ||
                        h.Unidade == unidade) &&
                    (string.IsNullOrEmpty(produto) ||
                        h.Produto == produto) &&
                    (tipo == null || h.Tipo == tipo))
                .OrderByDescending(h => h.Data)
                .ToList();
        }
        public static int ProximoIdCliente()
        {
            return clientes.Any()
                ? clientes.Max(c => c.IdCliente) + 1
                : 1;
        }

        public static BindingList<movimentoestoque> movimentacoes =
            new BindingList<movimentoestoque>();        public static BindingList<HistoricoEstoque> historicoestoque =
            new BindingList<HistoricoEstoque>();
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

        public static BindingList<Caixa> caixas =
            new BindingList<Caixa>();

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
using System;
using System.ComponentModel;
using System.Linq;

namespace erp_salgados_e_cia
{
    public class Venda
    {
        public int NumeroMesa { get; set; }

        public int IdVenda { get; set; }

        public DateTime DataHora { get; set; }

        public string FormaPagamento { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal ValorRecebido { get; set; }

        public decimal Troco { get; set; }

        public string Status { get; set; }

        public int? IdCliente { get; set; }

        public BindingList<ItemVenda> Itens { get; set; } =
            new BindingList<ItemVenda>();
        public BindingList<PagamentoVenda> Pagamentos { get; set; } =
             new BindingList<PagamentoVenda>();

        public string Produtos
        {
            get
            {
                return string.Join(
                    ", ",
                    Itens.Select(item =>
                        $"{item.Produto} x{item.Quantidade:0.##}")
                );
            }
        }
    }
    public class PagamentoVenda
    {
        public string FormaPagamento { get; set; }

        public decimal Valor { get; set; }
    }
}
using System;
using System.ComponentModel;
using System.Linq;

namespace erp_salgados_e_cia
{
    public class Comanda
    {
        public int NumeroMesa { get; set; }

        public int? IdCliente { get; set; }

        public BindingList<ItemVenda> Itens { get; set; } =
            new BindingList<ItemVenda>();

        public BindingList<PagamentoVenda> Pagamentos { get; set; } =
            new BindingList<PagamentoVenda>();

        public decimal QuantidadeItens
        {
            get
            {
                return Itens.Sum(item => item.Quantidade);
            }
        }

        public decimal Total
        {
            get
            {
                return Itens.Sum(item => item.Total);
            }
        }

        public decimal TotalPago
        {
            get
            {
                return Pagamentos.Sum(pagamento => pagamento.Valor);
            }
        }

        public decimal Falta
        {
            get
            {
                return Math.Max(0m, Total - TotalPago);
            }
        }

        public DateTime Abertura { get; set; } =
            DateTime.Now;
    }
}
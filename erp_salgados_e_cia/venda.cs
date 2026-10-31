using System;
using System.ComponentModel;

namespace erp_salgados_e_cia
{
    public class Venda
    {
        public int IdVenda { get; set; }

        public DateTime DataHora { get; set; }

        public string FormaPagamento { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal ValorRecebido { get; set; }

        public decimal Troco { get; set; }

        public string Status { get; set; }

        public BindingList<ItemVenda> Itens { get; set; } =
            new BindingList<ItemVenda>();
    }
}
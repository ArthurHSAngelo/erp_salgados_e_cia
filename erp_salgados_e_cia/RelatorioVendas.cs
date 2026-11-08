using System;
using System.Collections.Generic;

namespace erp_salgados_e_cia
{
    public class LinhaFaturamentoDia
    {
        public DateTime Dia { get; set; }

        public int QuantidadeVendas { get; set; }

        public decimal Faturamento { get; set; }
    }

    public class LinhaProdutoVendido
    {
        public string Produto { get; set; }

        public decimal Quantidade { get; set; }

        public decimal Receita { get; set; }
    }

    public class LinhaFormaPagamento
    {
        public string Forma { get; set; }

        public decimal Valor { get; set; }
    }

    public class ResumoVendas
    {
        public int QuantidadeVendas { get; set; }

        public decimal Faturamento { get; set; }

        public decimal Recebido { get; set; }

        public decimal EmNotinha { get; set; }

        public decimal TicketMedio { get; set; }
    }

    public class RelatorioVendas
    {
        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public ResumoVendas Resumo { get; set; } =
            new ResumoVendas();

        public List<LinhaFaturamentoDia> PorDia { get; set; } =
            new List<LinhaFaturamentoDia>();

        public List<LinhaProdutoVendido> Produtos { get; set; } =
            new List<LinhaProdutoVendido>();

        public List<LinhaFormaPagamento> Formas { get; set; } =
            new List<LinhaFormaPagamento>();
    }
}
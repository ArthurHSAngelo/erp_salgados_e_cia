using System;
using System.Collections.Generic;

namespace erp_salgados_e_cia
{
    public class LinhaCriticoPainel
    {
        public string Produto { get; set; }

        public decimal Saldo { get; set; }

        public decimal Minimo { get; set; }
    }

    public class LinhaVendaPainel
    {
        public int Venda { get; set; }

        public DateTime DataHora { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; }
    }

    public class LinhaAtividadePainel
    {
        public DateTime DataHora { get; set; }

        public string Usuario { get; set; }

        public string Operacao { get; set; }

        public string Registro { get; set; }
    }

    public class DadosPainel
    {
        public string Unidade { get; set; }

        public string StatusCaixa { get; set; } = "Não aberto hoje";

        public bool CaixaAberto { get; set; }

        public string ResponsavelCaixa { get; set; }

        public DateTime? AberturaCaixa { get; set; }

        public decimal ValorInicialCaixa { get; set; }

        public decimal DinheiroEsperado { get; set; }

        public int VendasHoje { get; set; }

        public decimal FaturamentoHoje { get; set; }

        public decimal TicketMedioHoje { get; set; }

        public decimal EmNotinhaHoje { get; set; }

        public int TotalCriticos { get; set; }

        public List<LinhaCriticoPainel> Criticos { get; set; } =
            new List<LinhaCriticoPainel>();

        public decimal NotinhaTotal { get; set; }

        public int NotinhaClientes { get; set; }

        public List<LinhaVendaPainel> UltimasVendas { get; set; } =
            new List<LinhaVendaPainel>();

        public List<LinhaAtividadePainel> Atividade { get; set; } =
            new List<LinhaAtividadePainel>();
    }
}

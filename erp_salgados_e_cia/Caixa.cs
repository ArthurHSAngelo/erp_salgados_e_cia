using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace erp_salgados_e_cia
{
    public class Caixa
    {
        public DateTime DataAbertura { get; set; }

        public DateTime? DataFechamento { get; set; }

        public string Responsavel { get; set; }

        public decimal ValorInicial { get; set; }

        // O que entrou no caixa por forma de pagamento (nomes da tela: Dinheiro, Pix, Crédito,
        // Débito, Notinha), lido do banco. As vendas já vêm sem o troco e o dinheiro inclui
        // os abatimentos de Notinha.
        [Browsable(false)]
        public Dictionary<string, decimal> TotaisPorForma { get; set; } =
            new Dictionary<string, decimal>();

        // Parte das vendas (sem Notinha e sem o troco) e dos abatimentos, para o resumo.
        [Browsable(false)]
        public decimal TotalVendasSemNotinha { get; set; }

        [Browsable(false)]
        public decimal TotalNotinhaDoCaixa { get; set; }

        [Browsable(false)]
        public decimal TotalAbatimentosRecebidos { get; set; }

        public decimal RecebidoPorForma(string forma)
        {
            decimal valor;

            return TotaisPorForma.TryGetValue(forma, out valor)
                ? valor
                : 0m;
        }

        public decimal TotalVendas
        {
            get { return TotalVendasSemNotinha; }
        }

        public decimal TotalNotinha
        {
            get { return TotalNotinhaDoCaixa; }
        }

        public decimal TotalAbatimentos
        {
            get { return TotalAbatimentosRecebidos; }
        }

        public decimal DinheiroEsperado
        {
            get
            {
                return ValorInicial + RecebidoPorForma("Dinheiro");
            }
        }

        public decimal? ValorContado { get; set; }

        public decimal? Diferenca
        {
            get
            {
                return ValorContado.HasValue
                    ? ValorContado.Value - DinheiroEsperado
                    : (decimal?)null;
            }
        }

        public decimal MaquininhaEsperada
        {
            get
            {
                return RecebidoPorForma("Crédito") +
                    RecebidoPorForma("Débito");
            }
        }

        public decimal? ValorMaquininha { get; set; }

        public decimal? DiferencaMaquininha
        {
            get
            {
                return ValorMaquininha.HasValue
                    ? ValorMaquininha.Value - MaquininhaEsperada
                    : (decimal?)null;
            }
        }

        public string Status { get; set; }

        public string Unidade { get; set; }

        [Browsable(false)]
        public int IdCaixa { get; set; }
    }
}

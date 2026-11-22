using System;
using System.Collections.Generic;

namespace erp_salgados_e_cia
{
    public class DreUnidade
    {
        public string Unidade { get; set; }

        public decimal Receita { get; set; }

        public decimal Recebido { get; set; }

        public decimal VendidoEmNotinha { get; set; }

        public decimal Abatimentos { get; set; }

        public decimal NotinhaAReceber
        {
            get { return VendidoEmNotinha - Abatimentos; }
        }

        public decimal Despesas { get; set; }

        public decimal Perdas { get; set; }

        public int PerdasSemCusto { get; set; }

        public decimal Resultado
        {
            get { return Receita - Despesas; }
        }
    }

    public class RelatorioDre
    {
        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public bool Consolidado { get; set; }

        public List<DreUnidade> Unidades { get; set; } =
            new List<DreUnidade>();

        public DreUnidade Total { get; set; } =
            new DreUnidade { Unidade = "Total" };
    }
}

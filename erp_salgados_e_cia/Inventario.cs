using System;
using System.Collections.Generic;
using System.Linq;

namespace erp_salgados_e_cia
{
    public class ItemInventario
    {
        public string Produto { get; set; }

        public decimal SaldoSistema { get; set; }

        public decimal? QuantidadeContada { get; set; }

        public decimal? Diferenca
        {
            get
            {
                return QuantidadeContada.HasValue
                    ? QuantidadeContada.Value - SaldoSistema
                    : (decimal?)null;
            }
        }

        public string Motivo { get; set; }
    }

    public class Inventario
    {
        public int IdInventario { get; set; }

        public DateTime DataHora { get; set; }

        public string Unidade { get; set; }

        public string Usuario { get; set; }

        public string Responsavel { get; set; }

        public string Observacao { get; set; }

        public List<ItemInventario> Itens { get; set; } =
            new List<ItemInventario>();

        public int QuantidadeAjustes
        {
            get
            {
                return Itens.Count(i =>
                    i.Diferenca.HasValue &&
                    i.Diferenca.Value != 0);
            }
        }
    }
}

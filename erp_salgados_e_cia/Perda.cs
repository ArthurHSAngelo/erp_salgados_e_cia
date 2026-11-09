using System;
using System.ComponentModel;

namespace erp_salgados_e_cia
{
    public enum MotivoPerda
    {
        Vencido,
        Danificado,
        Desperdicio
    }

    public class Perda
    {
        public int IdPerda { get; set; }

        public DateTime Data { get; set; }

        public string Unidade { get; set; }

        public string Produto { get; set; }

        public decimal Quantidade { get; set; }

        [Browsable(false)]
        public MotivoPerda Motivo { get; set; }

        public string MotivoTexto
        {
            get
            {
                switch (Motivo)
                {
                    case MotivoPerda.Vencido:
                        return "Vencido";

                    case MotivoPerda.Danificado:
                        return "Danificado";

                    default:
                        return "Desperdício";
                }
            }
        }

        public string Responsavel { get; set; }

        public string Usuario { get; set; }

        [Browsable(false)]
        public DateTime DataHoraRegistro { get; set; }
    }
}

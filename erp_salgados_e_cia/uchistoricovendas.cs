using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class uchistoricovendas : UserControl
    {
        private BindingList<Venda> vendas =
             dadossistema.vendas;

        private BindingSource fonteVendas =
            new BindingSource();

        private void AtualizarTabela()
        {
            fonteVendas.DataSource = null;
            fonteVendas.DataSource = vendas;

            if (dgvhistoricovendas.Columns.Count > 0)
            {
                dgvhistoricovendas.Columns["IdVenda"].HeaderText =
                    "Venda";

                dgvhistoricovendas.Columns["DataHora"].HeaderText =
                    "Data e hora";

                dgvhistoricovendas.Columns["FormaPagamento"].HeaderText =
                    "Forma de pagamento";

                dgvhistoricovendas.Columns["ValorTotal"].HeaderText =
                    "Total";

                dgvhistoricovendas.Columns["ValorRecebido"].HeaderText =
                    "Valor recebido";

                dgvhistoricovendas.Columns["Troco"].HeaderText =
                    "Troco";

                dgvhistoricovendas.Columns["Status"].HeaderText =
                    "Status";

                dgvhistoricovendas.Columns["Itens"].Visible = false;

                dgvhistoricovendas.Columns["DataHora"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgvhistoricovendas.Columns["ValorTotal"]
                    .DefaultCellStyle.Format = "N2";

                dgvhistoricovendas.Columns["ValorRecebido"]
                    .DefaultCellStyle.Format = "N2";

                dgvhistoricovendas.Columns["Troco"]
                    .DefaultCellStyle.Format = "N2";
            }
        } 

        public uchistoricovendas()
        {
            InitializeComponent();
        }
    }
}

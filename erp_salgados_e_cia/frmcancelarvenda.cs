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
    public partial class frmcancelarvenda : Form
    {
        private Venda vendaParaCancelar;
        private string autorizadoPor;

        public frmcancelarvenda(Venda venda, string autorizadoPor)
        {
            InitializeComponent();

            vendaParaCancelar = venda;
            this.autorizadoPor = autorizadoPor;

            lcancelamentovenda.Text =
                $"Venda nº {venda.IdVenda}";

            lcancelamentototal.Text =
                $"Total: R$ {venda.ValorTotal:N2}";

            bconfirmarcancelamento.Click +=
                bconfirmarcancelamento_Click;
        }

        private void bconfirmarcancelamento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbmotivo.Text))
            {
                MessageBox.Show(
                    "Informe o motivo do cancelamento.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbmotivo.Focus();
                return;
            }

            DialogResult resposta = MessageBox.Show(
                $"Deseja realmente cancelar a venda nº {vendaParaCancelar.IdVenda}? Os produtos voltarão ao estoque e a ação não pode ser desfeita.",
                "Confirmar cancelamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            string mensagem;

            if (!dadossistema.CancelarVenda(
                    vendaParaCancelar,
                    tbmotivo.Text,
                    autorizadoPor,
                    out mensagem))
            {
                MessageBox.Show(
                    mensagem,
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            MessageBox.Show(
                "Venda cancelada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            DialogResult = DialogResult.OK;
        }
    }
}

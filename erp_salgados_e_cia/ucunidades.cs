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
    public partial class ucunidades : UserControl
    {
        private List<Unidade> unidades =
            new List<Unidade>();

        private void AtualizarTabela()
        {
            dgvunidades.DataSource = null;
            dgvunidades.DataSource = unidades;
        }
        public ucunidades()
        {
            InitializeComponent();
        }

        private void bsalvarunidade_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbnomeu.Text))
            {
                MessageBox.Show(
                    "Informe o nome da unidade.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnomeu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbenderecou.Text))
            {
                MessageBox.Show(
                    "Informe o endereço da unidade.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbenderecou.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbnumerou.Text))
            {
                MessageBox.Show(
                    "Informe o número da unidade.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnumerou.Focus();
                return;
            }

            Unidade unidade = new Unidade
            {
                Nome = tbnomeu.Text.Trim(),
                Endereco = tbenderecou.Text.Trim(),
                Numero = tbnumerou.Text.Trim()
            };

            unidades.Add(unidade);

            AtualizarTabela();

            MessageBox.Show(
                "Unidade salva com sucesso!\n\n" +
                "Nome: " + unidade.Nome + "\n" +
                "Endereço: " + unidade.Endereco + "\n" +
                "Número: " + unidade.Numero,
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            tbnomeu.Clear();
            tbenderecou.Clear();
            tbnumerou.Clear();

            tbnomeu.Focus();
        }

        private void bexcluirunidade_Click(object sender, EventArgs e)
        {
            if (dgvunidades.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione uma unidade para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Unidade unidadeSelecionada =
                dgvunidades.SelectedRows[0]
                .DataBoundItem as Unidade;

            if (unidadeSelecionada == null)
            {
                return;
            }

            DialogResult resposta =
                MessageBox.Show(
                    "Deseja excluir a unidade selecionada?",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            unidades.Remove(unidadeSelecionada);

            AtualizarTabela();

            MessageBox.Show(
                "Unidade excluída com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void dgvunidades_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string coluna =
       dgvunidades.Columns[e.ColumnIndex].Name;

            string valor =
                e.FormattedValue == null
                ? ""
                : e.FormattedValue.ToString().Trim();

            if (coluna == "Nome" &&
                string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show(
                    "O nome da unidade não pode ficar vazio.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                e.Cancel = true;
                return;
            }

            if (coluna == "Endereco" &&
                string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show(
                    "O endereço da unidade não pode ficar vazio.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                e.Cancel = true;
                return;
            }

            if (coluna == "Numero" &&
                string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show(
                    "O número da unidade não pode ficar vazio.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                e.Cancel = true;
            }
        }
    }
}

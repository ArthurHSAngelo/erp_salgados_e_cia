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
    public partial class CadastroFornecedores : Form
    {
        private List<Fornecedor> fornecedores =
    new List<Fornecedor>();
        private void AtualizarTabela()
        {
            dgvfornecedores.DataSource = null;
            dgvfornecedores.DataSource = fornecedores;
        }

        public CadastroFornecedores()
        {
            InitializeComponent();
        }

        private void bsalvarfornecedor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbnomef.Text))
            {
                MessageBox.Show(
                    "Informe o nome do fornecedor.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnomef.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbcnpjf.Text))
            {
                MessageBox.Show(
                    "Informe o CNPJ do fornecedor.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcnpjf.Focus();
                return;
            }

            string cnpjDigitado =
                new string(
                    tbcnpjf.Text
                        .Where(char.IsDigit)
                        .ToArray()
                );

            bool cnpjExiste = fornecedores.Exists(f =>
                new string(
                    f.Cnpj
                        .Where(char.IsDigit)
                        .ToArray()
                ) == cnpjDigitado
            );

            if (cnpjExiste)
            {
                MessageBox.Show(
                    "Esse CNPJ já está cadastrado.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcnpjf.Focus();
                return;
            }

            Fornecedor fornecedor = new Fornecedor
            {
                Nome = tbnomef.Text.Trim(),
                Cnpj = tbcnpjf.Text.Trim(),
                Telefone = tbtelefonef.Text.Trim()
            };

            fornecedores.Add(fornecedor);

            AtualizarTabela();

            MessageBox.Show(
                "Fornecedor salvo com sucesso!\n\n" +
                "Nome: " + fornecedor.Nome + "\n" +
                "CNPJ: " + fornecedor.Cnpj + "\n" +
                "Telefone: " + fornecedor.Telefone,
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            tbnomef.Clear();
            tbcnpjf.Clear();
            tbtelefonef.Clear();

            tbnomef.Focus();
        }

        private void dgvfornecedores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            {
                string coluna =
                    dgvfornecedores.Columns[e.ColumnIndex].Name;

                string valor =
                    e.FormattedValue == null
                    ? ""
                    : e.FormattedValue.ToString().Trim();

                if (coluna == "Nome" &&
                    string.IsNullOrWhiteSpace(valor))
                {
                    MessageBox.Show(
                        "O nome do fornecedor não pode ficar vazio.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    e.Cancel = true;
                    return;
                }

                if (coluna == "Cnpj" &&
                    string.IsNullOrWhiteSpace(valor))
                {
                    MessageBox.Show(
                        "O CNPJ do fornecedor não pode ficar vazio.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    e.Cancel = true;
                    return;
                }

                if (coluna == "Cnpj")
                {
                    string cnpjEditado =
                        new string(
                            valor
                                .Where(char.IsDigit)
                                .ToArray()
                        );

                    for (int linha = 0;
                         linha < dgvfornecedores.Rows.Count;
                         linha++)
                    {
                        if (linha == e.RowIndex)
                        {
                            continue;
                        }

                        object valorOutraLinha =
                            dgvfornecedores.Rows[linha]
                            .Cells["Cnpj"]
                            .Value;

                        string outroCnpj =
                            valorOutraLinha == null
                            ? ""
                            : valorOutraLinha.ToString();

                        outroCnpj =
                            new string(
                                outroCnpj
                                    .Where(char.IsDigit)
                                    .ToArray()
                            );

                        if (cnpjEditado == outroCnpj)
                        {
                            MessageBox.Show(
                                "Esse CNPJ já está cadastrado.",
                                "Atenção",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );

                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        private void bexcluirfornecedor_Click(object sender, EventArgs e)
        {
            if (dgvfornecedores.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um fornecedor para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Fornecedor fornecedorSelecionado =
                dgvfornecedores.SelectedRows[0]
                .DataBoundItem as Fornecedor;

            if (fornecedorSelecionado == null)
            {
                return;
            }

            DialogResult resposta =
                MessageBox.Show(
                    "Deseja excluir o fornecedor selecionado?",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            fornecedores.Remove(fornecedorSelecionado);

            AtualizarTabela();

            MessageBox.Show(
                "Fornecedor excluído com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}

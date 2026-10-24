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
    public partial class ucfornecedores : UserControl
    {
        private bool modoEdicao = false;
        private int indiceFornecedorEditando = -1;

        private List<Fornecedor> fornecedores =
        dadossistema.fornecedores;
        private void AtualizarTabela()
        {
            dgvfornecedores.DataSource = null;
            dgvfornecedores.DataSource = fornecedores;
        }
        public ucfornecedores()
        {
            InitializeComponent();
            AtualizarTabela();
        }

        private void LimparCampos()
        {
            tbnomef.Clear();
            tbcnpjf.Clear();
            tbtelefonef.Clear();

            tbnomef.Focus();
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

            Fornecedor fornecedorAtual = null;

            if (modoEdicao && indiceFornecedorEditando >= 0)
            {
                fornecedorAtual = fornecedores[indiceFornecedorEditando];
            }

            bool cnpjExiste = fornecedores.Any(f =>
                f != fornecedorAtual &&
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

            if (modoEdicao)
            {
                fornecedorAtual.Nome = tbnomef.Text.Trim();
                fornecedorAtual.Cnpj = tbcnpjf.Text.Trim();
                fornecedorAtual.Telefone = tbtelefonef.Text.Trim();

                MessageBox.Show(
                    "Fornecedor atualizado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Fornecedor fornecedor = new Fornecedor
                {
                    Nome = tbnomef.Text.Trim(),
                    Cnpj = tbcnpjf.Text.Trim(),
                    Telefone = tbtelefonef.Text.Trim()
                };

                fornecedores.Add(fornecedor);

                MessageBox.Show(
                    "Fornecedor salvo com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            AtualizarTabela();

            modoEdicao = false;
            indiceFornecedorEditando = -1;

            bsalvarfornecedor.Text = "Salvar fornecedor";
            beditarf.Enabled = true;

            LimparCampos();
        }

        private void bexcluirf_Click(object sender, EventArgs e)
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

            DialogResult resposta = MessageBox.Show(
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

        private void beditarf_Click(object sender, EventArgs e)
        {
            if (dgvfornecedores.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um fornecedor para editar.",
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

            indiceFornecedorEditando =
                fornecedores.IndexOf(fornecedorSelecionado);

            if (indiceFornecedorEditando == -1)
            {
                return;
            }

            tbnomef.Text = fornecedorSelecionado.Nome;
            tbcnpjf.Text = fornecedorSelecionado.Cnpj;
            tbtelefonef.Text = fornecedorSelecionado.Telefone;

            modoEdicao = true;

            bsalvarfornecedor.Text = "Atualizar fornecedor";
            beditarf.Enabled = false;

            tbnomef.Focus();
        }
    }
}

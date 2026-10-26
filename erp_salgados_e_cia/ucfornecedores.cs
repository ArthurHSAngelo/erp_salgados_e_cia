using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static erp_salgados_e_cia.dadossistema;

namespace erp_salgados_e_cia
{
    public partial class ucfornecedores : UserControl
    {
        private bool modoEdicao = false;
        private int indiceFornecedorEditando = -1;

        private BindingList<Fornecedor> fornecedores =
            dadossistema.fornecedores;
        private BindingSource fonteFornecedores =
            new BindingSource();
        private void AtualizarTabela()
        {
            string filtro =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Fornecedor> fornecedoresFiltrados =
                fornecedores;

            if (filtro == "Ativos")
            {
                fornecedoresFiltrados =
                    fornecedores.Where(f =>
                        f.Status == StatusCadastro.Ativo);
            }
            else if (filtro == "Inativos")
            {
                fornecedoresFiltrados =
                    fornecedores.Where(f =>
                        f.Status == StatusCadastro.Inativo);
            }

            fonteFornecedores.DataSource =
                fornecedoresFiltrados.ToList();
        }
        public ucfornecedores()
        {
            InitializeComponent();

            cbfiltrostatus.Items.AddRange(
                new object[]
                {
            "Ativos",
            "Inativos",
            "Todos"
                }
            );

            cbfiltrostatus.SelectedIndexChanged +=
                cbfiltrostatus_SelectedIndexChanged;

            fonteFornecedores.DataSource =
                fornecedores;

            dgvfornecedores.DataSource =
                fonteFornecedores;

            cbfiltrostatus.SelectedIndex =
                0;

            AtualizarTabela();
        }

        private void cbfiltrostatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AtualizarTabela();
        }

        private void LimparCampos()
        {
            tbnomef.Clear();
            tbcnpjf.Clear();
            tbtelefonef.Clear();
            cbativo.Checked = true;
            cbativo.Visible = false;

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
                if (!cbativo.Checked &&
                     fornecedorAtual.Status == StatusCadastro.Ativo)
                {
                    DialogResult resposta = MessageBox.Show(
                        "Deseja realmente inativar este fornecedor?",
                        "Confirmar inativação",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                    if (resposta != DialogResult.Yes)
                    {
                        cbativo.Checked = true;
                        return;
                    }
                }

                fornecedorAtual.Nome = tbnomef.Text.Trim();
                fornecedorAtual.Cnpj = tbcnpjf.Text.Trim();
                fornecedorAtual.Telefone = tbtelefonef.Text.Trim();

                fornecedorAtual.Status =
                    cbativo.Checked
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo;

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
                    Status = StatusCadastro.Ativo,
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
            dgvfornecedores.Refresh();

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

            if (dadossistema.FornecedorPossuiVinculos(
                    fornecedorSelecionado))
            {
                MessageBox.Show(
                    "Exclusão bloqueada: este fornecedor possui movimentações.",
                    "Exclusão não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

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
            dgvfornecedores.Refresh();

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

            tbnomef.Text =
                fornecedorSelecionado.Nome;

            tbcnpjf.Text =
                fornecedorSelecionado.Cnpj;

            tbtelefonef.Text =
                fornecedorSelecionado.Telefone;

            cbativo.Checked =
                fornecedorSelecionado.Status == StatusCadastro.Ativo;

            cbativo.Visible = true;

            modoEdicao = true;

            bsalvarfornecedor.Text = "Atualizar fornecedor";
            beditarf.Enabled = false;

            tbnomef.Focus();
        }
    }
}

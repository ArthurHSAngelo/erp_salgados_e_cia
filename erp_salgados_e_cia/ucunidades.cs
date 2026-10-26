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
    public partial class ucunidades : UserControl
    {
        private bool modoEdicao = false;
        private int indiceUnidadeEditando = -1;
        private BindingList<Unidade> unidades =
            dadossistema.unidades;
        private BindingSource fonteUnidades 
            = new BindingSource();
        private void AtualizarTabela()
        {
            string filtro =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Unidade> unidadesFiltradas = unidades;

            if (filtro == "Ativos")
            {
                unidadesFiltradas = unidades.Where(u =>
                    u.Status == StatusCadastro.Ativo);
            }
            else if (filtro == "Inativos")
            {
                unidadesFiltradas = unidades.Where(u =>
                    u.Status == StatusCadastro.Inativo);
            }

            fonteUnidades.DataSource =
                unidadesFiltradas.ToList();
        }

        private void cbfiltrostatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AtualizarTabela();
        }
        public ucunidades()
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

            fonteUnidades.DataSource = unidades;
            dgvunidades.DataSource = fonteUnidades;

            cbfiltrostatus.SelectedIndex = 0;

            AtualizarTabela();
        }

        private void LimparCampos()
        {
            tbnomeu.Clear();
            tbcnpju.Clear();
            tbenderecou.Clear();
            tbnumerou.Clear();
            cbativo.Checked = true;
            cbativo.Visible = false;

            tbnomeu.Focus();
        }

        private void bsalvaru_Click(object sender, EventArgs e)
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

            if (string.IsNullOrWhiteSpace(tbcnpju.Text))
            {
                MessageBox.Show(
                    "Informe o CNPJ da unidade.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcnpju.Focus();
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

            if (modoEdicao)
            {
                Unidade unidadeAtual =
                    unidades[indiceUnidadeEditando];

                if (!cbativo.Checked &&
                    unidadeAtual.Status == StatusCadastro.Ativo)
                {
                    DialogResult resposta = MessageBox.Show(
                        "Deseja realmente inativar esta unidade?",
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

                unidadeAtual.Nome = tbnomeu.Text.Trim();
                unidadeAtual.Cnpj = tbcnpju.Text.Trim();
                unidadeAtual.Endereco = tbenderecou.Text.Trim();
                unidadeAtual.Numero = tbnumerou.Text.Trim();

                unidadeAtual.Status =
                    cbativo.Checked
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo;

                MessageBox.Show(
                    "Unidade atualizada com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Unidade unidade = new Unidade
                {
                    Status = StatusCadastro.Ativo,
                    Nome = tbnomeu.Text.Trim(),
                    Cnpj = tbcnpju.Text.Trim(),
                    Endereco = tbenderecou.Text.Trim(),
                    Numero = tbnumerou.Text.Trim()
                };

                unidades.Add(unidade);

                MessageBox.Show(
                    "Unidade salva com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            AtualizarTabela();
            dgvunidades.Refresh();

            modoEdicao = false;
            indiceUnidadeEditando = -1;

            bsalvaru.Text = "Salvar unidade";
            beditaru.Enabled = true;

            LimparCampos();
        }

        private void bexcluiru_Click(object sender, EventArgs e)
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

            DialogResult resposta = MessageBox.Show(
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
            dgvunidades.Refresh();

            modoEdicao = false;
            indiceUnidadeEditando = -1;

            bsalvaru.Text = "Salvar unidade";
            beditaru.Enabled = true;

            LimparCampos();

            MessageBox.Show(
                "Unidade excluída com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void beditaru_Click(object sender, EventArgs e)
        {
            if (dgvunidades.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione uma unidade para editar.",
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

            indiceUnidadeEditando =
                unidades.IndexOf(unidadeSelecionada);

            if (indiceUnidadeEditando == -1)
            {
                return;
            }

            tbnomeu.Text =
                unidadeSelecionada.Nome;

            tbenderecou.Text =
                unidadeSelecionada.Endereco;

            tbnumerou.Text =
                unidadeSelecionada.Numero;

            tbcnpju.Text =
                unidadeSelecionada.Cnpj;

            cbativo.Checked =
                unidadeSelecionada.Status == StatusCadastro.Ativo;

            cbativo.Visible = true;

            modoEdicao = true;

            bsalvaru.Text = "Atualizar unidade";
            beditaru.Enabled = false;

            tbnomeu.Focus();
        }
    }
}

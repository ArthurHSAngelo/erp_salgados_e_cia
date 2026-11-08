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
    public partial class ucfuncionarios : UserControl
    {
        private bool modoEdicao = false;
        private int indiceFuncionarioEditando = -1;

        private BindingList<Funcionario> funcionarios =
            dadossistema.funcionarios;

        private BindingSource fonteFuncionarios =
            new BindingSource();

        public void AtualizarTabela()
        {
            string filtro =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Funcionario> funcionariosFiltrados =
                funcionarios;

            if (filtro == "Ativos")
            {
                funcionariosFiltrados =
                    funcionarios.Where(f =>
                        f.Status == StatusCadastro.Ativo);
            }
            else if (filtro == "Inativos")
            {
                funcionariosFiltrados =
                    funcionarios.Where(f =>
                        f.Status == StatusCadastro.Inativo);
            }

            fonteFuncionarios.DataSource =
                funcionariosFiltrados.ToList();

            if (dgvfuncionarios.Columns.Count > 0)
            {
                dgvfuncionarios.Columns["IdFuncionario"].Visible = false;

                dgvfuncionarios.Columns["Nome"].HeaderText = "Nome";
                dgvfuncionarios.Columns["Unidade"].HeaderText = "Unidade";
                dgvfuncionarios.Columns["Status"].HeaderText = "Status";
            }
        }

        public void AtualizarCombos()
        {
            string unidadeSelecionada =
                cbunidadefuncionario.SelectedItem as string;

            cbunidadefuncionario.Items.Clear();

            foreach (Unidade unidade in
                dadossistema.unidades
                .Where(u => u.Status == StatusCadastro.Ativo)
                .OrderBy(u => u.Nome))
            {
                cbunidadefuncionario.Items.Add(unidade.Nome);
            }

            if (unidadeSelecionada != null &&
                cbunidadefuncionario.Items.Contains(unidadeSelecionada))
            {
                cbunidadefuncionario.SelectedItem = unidadeSelecionada;
            }
            else if (cbunidadefuncionario.Items.Contains(
                dadossistema.UnidadeAtual))
            {
                cbunidadefuncionario.SelectedItem =
                    dadossistema.UnidadeAtual;
            }
            else
            {
                cbunidadefuncionario.SelectedIndex = -1;
            }
        }

        public void AtualizarTela()
        {
            AtualizarCombos();

            AtualizarTabela();
        }

        public ucfuncionarios()
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

            fonteFuncionarios.DataSource =
                funcionarios;

            dgvfuncionarios.DataSource =
                fonteFuncionarios;

            cbfiltrostatus.SelectedIndexChanged +=
                cbfiltrostatus_SelectedIndexChanged;

            bsalvarfuncionario.Click +=
                bsalvarfuncionario_Click;

            beditarfuncionario.Click +=
                beditarfuncionario_Click;

            bexcluirfuncionario.Click +=
                bexcluirfuncionario_Click;

            cbfiltrostatus.SelectedIndex =
                0;

            AtualizarCombos();

            LimparCampos();

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
            tbnomefuncionario.Clear();

            if (cbunidadefuncionario.Items.Contains(
                dadossistema.UnidadeAtual))
            {
                cbunidadefuncionario.SelectedItem =
                    dadossistema.UnidadeAtual;
            }
            else
            {
                cbunidadefuncionario.SelectedIndex = -1;
            }

            cbativo.Checked = true;
            cbativo.Visible = false;

            tbnomefuncionario.Focus();
        }

        private void bsalvarfuncionario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbnomefuncionario.Text))
            {
                MessageBox.Show(
                    "Informe o nome do funcionário.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnomefuncionario.Focus();
                return;
            }

            if (cbunidadefuncionario.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione a unidade do funcionário.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbunidadefuncionario.Focus();
                return;
            }

            string nomeDigitado =
                tbnomefuncionario.Text.Trim();

            string unidadeEscolhida =
                cbunidadefuncionario.SelectedItem.ToString();

            Funcionario funcionarioAtual = null;

            if (modoEdicao && indiceFuncionarioEditando >= 0)
            {
                funcionarioAtual =
                    funcionarios[indiceFuncionarioEditando];
            }

            bool nomeExiste = funcionarios.Any(f =>
                f != funcionarioAtual &&
                f.Unidade == unidadeEscolhida &&
                string.Equals(
                    f.Nome.Trim(),
                    nomeDigitado,
                    StringComparison.OrdinalIgnoreCase)
            );

            if (nomeExiste)
            {
                MessageBox.Show(
                    "Já existe um funcionário com esse nome nesta unidade.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnomefuncionario.Focus();
                return;
            }

            if (modoEdicao)
            {
                bool mudouNomeOuUnidade =
                    funcionarioAtual.Nome != nomeDigitado ||
                    funcionarioAtual.Unidade != unidadeEscolhida;

                if (mudouNomeOuUnidade &&
                    dadossistema.FuncionarioPossuiVinculos(
                        funcionarioAtual))
                {
                    MessageBox.Show(
                        "Este funcionário já aparece em caixas ou entradas de estoque. O nome e a unidade não podem ser alterados; inative-o e cadastre outro.",
                        "Alteração não permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (!cbativo.Checked &&
                    funcionarioAtual.Status == StatusCadastro.Ativo)
                {
                    DialogResult resposta = MessageBox.Show(
                        "Deseja realmente inativar este funcionário?",
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

                funcionarioAtual.Nome = nomeDigitado;
                funcionarioAtual.Unidade = unidadeEscolhida;

                funcionarioAtual.Status =
                    cbativo.Checked
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo;

                MessageBox.Show(
                    "Funcionário atualizado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Funcionario funcionario = new Funcionario
                {
                    IdFuncionario = dadossistema.ProximoIdFuncionario(),
                    Nome = nomeDigitado,
                    Unidade = unidadeEscolhida,
                    Status = StatusCadastro.Ativo
                };

                funcionarios.Add(funcionario);

                MessageBox.Show(
                    "Funcionário salvo com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            AtualizarTabela();
            dgvfuncionarios.Refresh();

            modoEdicao = false;
            indiceFuncionarioEditando = -1;

            bsalvarfuncionario.Text = "Salvar";
            beditarfuncionario.Enabled = true;

            AtualizarCombos();

            LimparCampos();
        }

        private void bexcluirfuncionario_Click(object sender, EventArgs e)
        {
            if (dgvfuncionarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um funcionário para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Funcionario funcionarioSelecionado =
                dgvfuncionarios.SelectedRows[0]
                .DataBoundItem as Funcionario;

            if (funcionarioSelecionado == null)
            {
                return;
            }

            if (dadossistema.FuncionarioPossuiVinculos(
                    funcionarioSelecionado))
            {
                MessageBox.Show(
                    "Exclusão bloqueada: este funcionário já foi responsável por caixas ou entradas de estoque. Inative-o.",
                    "Exclusão não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult resposta = MessageBox.Show(
                "Deseja excluir o funcionário selecionado?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            funcionarios.Remove(funcionarioSelecionado);

            AtualizarTabela();
            dgvfuncionarios.Refresh();

            MessageBox.Show(
                "Funcionário excluído com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void beditarfuncionario_Click(object sender, EventArgs e)
        {
            if (dgvfuncionarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um funcionário para editar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Funcionario funcionarioSelecionado =
                dgvfuncionarios.SelectedRows[0]
                .DataBoundItem as Funcionario;

            if (funcionarioSelecionado == null)
            {
                return;
            }

            indiceFuncionarioEditando =
                funcionarios.IndexOf(funcionarioSelecionado);

            if (indiceFuncionarioEditando == -1)
            {
                return;
            }

            tbnomefuncionario.Text =
                funcionarioSelecionado.Nome;

            if (!cbunidadefuncionario.Items.Contains(
                funcionarioSelecionado.Unidade))
            {
                cbunidadefuncionario.Items.Add(
                    funcionarioSelecionado.Unidade);
            }

            cbunidadefuncionario.SelectedItem =
                funcionarioSelecionado.Unidade;

            cbativo.Checked =
                funcionarioSelecionado.Status == StatusCadastro.Ativo;

            cbativo.Visible = true;

            modoEdicao = true;

            bsalvarfuncionario.Text = "Atualizar";
            beditarfuncionario.Enabled = false;

            tbnomefuncionario.Focus();
        }
    }
}

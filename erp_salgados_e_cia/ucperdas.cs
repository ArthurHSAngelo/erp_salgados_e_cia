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
    public partial class ucperdas : UserControl
    {
        private readonly MotivoPerda[] motivos =
        {
            MotivoPerda.Vencido,
            MotivoPerda.Danificado,
            MotivoPerda.Desperdicio
        };

        private BindingSource fontePerdas =
            new BindingSource();

        private void CarregarProdutos()
        {
            string produtoSelecionado =
                cbprodutoperda.SelectedItem as string;

            cbprodutoperda.Items.Clear();

            foreach (Produto produto in
                dadossistema.produtos
                .Where(p =>
                    p.Status == StatusCadastro.Ativo &&
                    p.EstoqueAtual > 0)
                .OrderBy(p => p.Nome))
            {
                cbprodutoperda.Items.Add(produto.Nome);
            }

            if (produtoSelecionado != null &&
                cbprodutoperda.Items.Contains(produtoSelecionado))
            {
                cbprodutoperda.SelectedItem = produtoSelecionado;
            }
            else
            {
                cbprodutoperda.SelectedIndex = -1;
            }

            AtualizarSaldo();
        }

        private void CarregarResponsaveis()
        {
            string responsavelSelecionado =
                cbresponsavelperda.SelectedItem as string;

            cbresponsavelperda.Items.Clear();

            foreach (string nome in
                dadossistema.NomesFuncionariosAtivos(
                    dadossistema.UnidadeAtual))
            {
                cbresponsavelperda.Items.Add(nome);
            }

            if (responsavelSelecionado != null &&
                cbresponsavelperda.Items.Contains(responsavelSelecionado))
            {
                cbresponsavelperda.SelectedItem = responsavelSelecionado;
            }
            else
            {
                cbresponsavelperda.SelectedIndex = -1;
            }
        }

        private void AtualizarSaldo()
        {
            Produto produto =
                cbprodutoperda.SelectedItem == null
                ? null
                : dadossistema.produtos.FirstOrDefault(p =>
                    p.Nome == cbprodutoperda.SelectedItem.ToString());

            lsaldoperda.Text =
                produto == null
                ? "Saldo disponível: -"
                : $"Saldo disponível: {produto.EstoqueAtual:N2}";
        }

        private void AtualizarPerdas()
        {
            fontePerdas.DataSource =
                dadossistema.PerdasDaUnidade(
                    dadossistema.UnidadeAtual,
                    30);

            if (dgvperdas.Columns.Count > 0)
            {
                dgvperdas.Columns["IdPerda"].Visible = false;
                dgvperdas.Columns["Unidade"].Visible = false;

                dgvperdas.Columns["Data"].HeaderText = "Data";
                dgvperdas.Columns["Produto"].HeaderText = "Produto";
                dgvperdas.Columns["Quantidade"].HeaderText = "Quantidade";
                dgvperdas.Columns["MotivoTexto"].HeaderText = "Motivo";
                dgvperdas.Columns["Responsavel"].HeaderText = "Responsável";
                dgvperdas.Columns["Usuario"].HeaderText = "Registrado por";

                dgvperdas.Columns["Data"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvperdas.Columns["Quantidade"]
                    .DefaultCellStyle.Format = "N2";
            }
        }

        private void LimparCampos()
        {
            cbprodutoperda.SelectedIndex = -1;

            nudquantidadeperda.Value = 1;

            cbmotivoperda.SelectedIndex = -1;

            dtpdataperda.Value = DateTime.Today;

            cbresponsavelperda.SelectedIndex = -1;

            AtualizarSaldo();

            cbprodutoperda.Focus();
        }

        public void AtualizarTela()
        {
            dtpdataperda.MaxDate = DateTime.Today;

            CarregarProdutos();

            CarregarResponsaveis();

            AtualizarPerdas();
        }

        public ucperdas()
        {
            InitializeComponent();

            nudquantidadeperda.Minimum = 0.01m;
            nudquantidadeperda.Maximum = 100000m;
            nudquantidadeperda.Value = 1;

            cbmotivoperda.Items.AddRange(
                new object[]
                {
                    "Vencido",
                    "Danificado",
                    "Desperdício"
                }
            );

            fontePerdas.DataSource =
                new List<Perda>();

            dgvperdas.DataSource =
                fontePerdas;

            cbprodutoperda.SelectedIndexChanged +=
                cbprodutoperda_SelectedIndexChanged;

            bregistrarperda.Click +=
                bregistrarperda_Click;

            AtualizarTela();
        }

        private void cbprodutoperda_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AtualizarSaldo();
        }

        private void bregistrarperda_Click(object sender, EventArgs e)
        {
            if (cbprodutoperda.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione um produto.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbprodutoperda.Focus();
                return;
            }

            if (cbmotivoperda.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o motivo da perda.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbmotivoperda.Focus();
                return;
            }

            if (cbresponsavelperda.Items.Count == 0)
            {
                MessageBox.Show(
                    "Esta unidade não tem funcionário ativo. Cadastre um em Cadastros, Funcionários.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cbresponsavelperda.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o responsável.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbresponsavelperda.Focus();
                return;
            }

            string nomeProduto =
                cbprodutoperda.SelectedItem.ToString();

            DialogResult resposta = MessageBox.Show(
                $"Registrar a perda de {nudquantidadeperda.Value:N2} de {nomeProduto}? O estoque será baixado.",
                "Confirmar perda",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            string mensagem;

            if (!dadossistema.RegistrarPerda(
                    nomeProduto,
                    nudquantidadeperda.Value,
                    motivos[cbmotivoperda.SelectedIndex],
                    dtpdataperda.Value.Date,
                    cbresponsavelperda.SelectedItem.ToString(),
                    out mensagem))
            {
                MessageBox.Show(
                    mensagem,
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                AtualizarSaldo();
                nudquantidadeperda.Focus();
                return;
            }

            MessageBox.Show(
                "Perda registrada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            AtualizarTela();

            LimparCampos();
        }
    }
}

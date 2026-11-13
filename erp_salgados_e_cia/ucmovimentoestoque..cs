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
    public partial class ucmovimentoestoque : UserControl
    {
        private BindingList<movimentoestoque> movimentacoes =
            dadossistema.movimentacoes;

        private BindingList<movimentoestoque> itensEntrada =
             new BindingList<movimentoestoque>();

        private BindingSource fonteItensEntrada =
            new BindingSource();

        private void AtualizarTabela()
        {
            dgvmovimentacoes.DataSource = null;
            dgvmovimentacoes.DataSource =
                movimentacoes
                .Where(m => m.Unidade == dadossistema.UnidadeAtual)
                .OrderByDescending(m => m.DataEntrada)
                .ToList();

            if (dgvmovimentacoes.Columns.Count > 0)
            {
                dgvmovimentacoes.Columns["Produto"].HeaderText = "Produto";
                dgvmovimentacoes.Columns["Quantidade"].HeaderText = "Quantidade";
                dgvmovimentacoes.Columns["CustoUnitario"].HeaderText = "Custo unitário";
                dgvmovimentacoes.Columns["Fornecedor"].HeaderText = "Fornecedor";
                dgvmovimentacoes.Columns["Responsavel"].HeaderText = "Responsável";
                dgvmovimentacoes.Columns["Unidade"].HeaderText = "Unidade";
                dgvmovimentacoes.Columns["DataEntrada"].HeaderText = "Data da entrada";

                dgvmovimentacoes.Columns["Quantidade"]
                    .DefaultCellStyle.Format = "N2";

                dgvmovimentacoes.Columns["CustoUnitario"]
                    .DefaultCellStyle.Format = "C2";

                dgvmovimentacoes.Columns["DataEntrada"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }
        private void ConfigurarColunasItens()
        {
            if (dgvitensentrada.Columns.Count == 0)
            {
                return;
            }

            dgvitensentrada.Columns["Produto"].HeaderText = "Produto";
            dgvitensentrada.Columns["Quantidade"].HeaderText = "Quantidade";
            dgvitensentrada.Columns["CustoUnitario"].HeaderText = "Custo unitário";
            dgvitensentrada.Columns["Fornecedor"].HeaderText = "Fornecedor";
            dgvitensentrada.Columns["Responsavel"].HeaderText = "Responsável";
            dgvitensentrada.Columns["Unidade"].HeaderText = "Unidade";
            dgvitensentrada.Columns["DataEntrada"].HeaderText = "Data da entrada";

            dgvitensentrada.Columns["Quantidade"]
                .DefaultCellStyle.Format = "N2";

            dgvitensentrada.Columns["CustoUnitario"]
                .DefaultCellStyle.Format = "C2";

            dgvitensentrada.Columns["DataEntrada"]
                .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        public ucmovimentoestoque()
        {
            InitializeComponent();

            fonteItensEntrada.DataSource =
                itensEntrada;

            dgvitensentrada.DataSource =
                fonteItensEntrada;

            ConfigurarColunasItens();

            AtualizarTabela();

            AtualizarCombos();
        }

        private void CarregarResponsaveis()
        {
            if (!cbresponsavel.Enabled)
            {
                return;
            }

            string responsavelSelecionado =
                cbresponsavel.SelectedItem as string;

            cbresponsavel.Items.Clear();

            foreach (string nome in
                dadossistema.NomesFuncionariosAtivos(
                    dadossistema.UnidadeAtual))
            {
                cbresponsavel.Items.Add(nome);
            }

            if (responsavelSelecionado != null &&
                cbresponsavel.Items.Contains(responsavelSelecionado))
            {
                cbresponsavel.SelectedItem = responsavelSelecionado;
            }
            else
            {
                cbresponsavel.SelectedIndex = -1;
            }
        }

        public void AtualizarCombos()
        {
            CarregarResponsaveis();

            cbproduto.DataSource = null;
            cbproduto.DataSource =
                dadossistema.produtos
                .Where(p => p.Status == StatusCadastro.Ativo)
                .ToList();

            cbproduto.DisplayMember = "Nome";
            cbproduto.SelectedIndex = -1;

            cbfornecedor.DataSource = null;
            cbfornecedor.DataSource =
                dadossistema.fornecedores
                .Where(f => f.Status == StatusCadastro.Ativo)
                .ToList();

            cbfornecedor.DisplayMember = "Nome";
            cbfornecedor.SelectedIndex = -1;
        }

        private void bregistrar_Click(object sender, EventArgs e)
        {
            decimal custoUnitario;

            if (cbproduto.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione um produto.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbproduto.Focus();
                return;
            }

            if (nudquantidade.Value <= 0)
            {
                MessageBox.Show(
                    "Informe uma quantidade válida.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                nudquantidade.Focus();
                return;
            }

            if (!decimal.TryParse(
                    tbcustouni.Text,
                    out custoUnitario)
                || custoUnitario <= 0)
            {
                MessageBox.Show(
                    "Informe um custo unitário válido.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcustouni.Focus();
                return;
            }

            if (cbfornecedor.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione um fornecedor.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbfornecedor.Focus();
                return;
            }

            if (cbresponsavel.Items.Count == 0)
            {
                MessageBox.Show(
                    "Esta unidade não tem funcionário ativo. Cadastre um em Cadastros, Funcionários.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cbresponsavel.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o responsável.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbresponsavel.Focus();
                return;
            }

            Produto produtoSelecionado =
                cbproduto.SelectedItem as Produto;

            if (produtoSelecionado == null)
            {
                return;
            }

            movimentoestoque item = new movimentoestoque
            {
                Produto = produtoSelecionado.Nome,
                Quantidade = nudquantidade.Value,
                CustoUnitario = custoUnitario,
                Fornecedor = cbfornecedor.Text,
                Responsavel = cbresponsavel.Text,
                Unidade = dadossistema.UnidadeAtual,
                DataEntrada = DateTime.Now
            };

            itensEntrada.Add(item);
            cbfornecedor.Enabled = false;
            cbresponsavel.Enabled = false;

            fonteItensEntrada.ResetBindings(false);

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            tbcustouni.Clear();

            cbproduto.Focus();
        }

        private void bremoveritem_Click(object sender, EventArgs e)
        {
            if (dgvitensentrada.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um item para remover.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            movimentoestoque itemSelecionado =
                dgvitensentrada.SelectedRows[0]
                .DataBoundItem as movimentoestoque;

            if (itemSelecionado == null)
            {
                return;
            }

            itensEntrada.Remove(itemSelecionado);
            if (itensEntrada.Count == 0)
            {
                cbfornecedor.Enabled = true;
                cbresponsavel.Enabled = true;
            }

            fonteItensEntrada.ResetBindings(false);
        }

        private void bfinalizarentrada_Click(object sender, EventArgs e)
        {
            if (itensEntrada.Count == 0)
            {
                MessageBox.Show(
                    "Adicione pelo menos um item à entrada.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult resposta = MessageBox.Show(
                "Deseja finalizar esta entrada de estoque?",
                "Confirmar entrada",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            foreach (movimentoestoque item in itensEntrada.ToList())
            {
                movimentacoes.Add(item);

                dadossistema.RegistrarLog(
                    TipoOperacaoLog.Inclusao,
                    "MovimentoEstoque",
                    $"Entrada: {item.Produto}",
                    $"Quantidade: {item.Quantidade:N2}; Custo unitário: {item.CustoUnitario:N2}; Fornecedor: {item.Fornecedor}; Responsável: {item.Responsavel}; Unidade: {item.Unidade}");

                Produto produto =
                    dadossistema.produtos.FirstOrDefault(p =>
                        p.Nome == item.Produto);

                if (produto != null)
                {
                    produto.EstoqueAtual += item.Quantidade;

                    dadossistema.RegistrarHistoricoEstoque(
                        produto,
                        TipoMovimentoEstoque.Entrada,
                        item.Quantidade,
                        item.Responsavel,
                        "Fornecedor " + item.Fornecedor
                    );
                }
            }

            itensEntrada.Clear();
            cbfornecedor.Enabled = true;
            cbresponsavel.Enabled = true;

            fonteItensEntrada.ResetBindings(false);

            AtualizarTabela();

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            tbcustouni.Clear();
            cbfornecedor.SelectedIndex = -1;
            cbresponsavel.SelectedIndex = -1;

            cbproduto.Focus();
        }
    }
}

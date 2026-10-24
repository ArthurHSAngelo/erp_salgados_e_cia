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
    public partial class ucprodutos : UserControl
    {
        private bool modoEdicao = false;
        private int indiceProdutoEditando = -1;
        private List<Produto> produtos =
            dadossistema.produtos;
        private void AtualizarTabela()
        {
            dgvprodutos.DataSource = null;
            dgvprodutos.DataSource = produtos;

            if (dgvprodutos.Columns.Count > 0)
            {
                dgvprodutos.Columns["Nome"].HeaderText = "Nome";
                dgvprodutos.Columns["Preco"].HeaderText = "Preço";
                dgvprodutos.Columns["Categoria"].HeaderText = "Categoria";
                dgvprodutos.Columns["UnidadeMedida"].HeaderText = "Unidade de medida";
                dgvprodutos.Columns["EstoqueMinimo"].HeaderText = "Estoque mínimo";

                dgvprodutos.Columns["Nome"].DisplayIndex = 0;
                dgvprodutos.Columns["Preco"].DisplayIndex = 1;
                dgvprodutos.Columns["Categoria"].DisplayIndex = 2;
                dgvprodutos.Columns["UnidadeMedida"].DisplayIndex = 3;
                dgvprodutos.Columns["EstoqueMinimo"].DisplayIndex = 4;

                dgvprodutos.Columns["Preco"].DefaultCellStyle.Format = "N2";
                dgvprodutos.Columns["EstoqueMinimo"].DefaultCellStyle.Format = "N2";
            }
        }
        public ucprodutos()
        {
            InitializeComponent();
            AtualizarTabela();
        }

        private void LimparCampos()
        {
            tbnomeproduto.Clear();
            tbpreco.Clear();

            cbcategoria.SelectedIndex = -1;
            cbunidademedida.SelectedIndex = -1;

            nudestoqueminimo.Value = 1;

            tbnomeproduto.Focus();
        }

        private void bsalvar_Click(object sender, EventArgs e)
        {
            decimal preco;

            if (string.IsNullOrWhiteSpace(tbnomeproduto.Text))
            {
                MessageBox.Show(
                    "Informe o nome do produto.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnomeproduto.Focus();
                return;
            }

            if (!decimal.TryParse(tbpreco.Text, out preco) || preco <= 0)
            {
                MessageBox.Show(
                    "Informe um preço válido.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbpreco.Focus();
                return;
            }

            if (cbcategoria.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Informe a categoria do produto.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbcategoria.Focus();
                return;
            }

            if (cbunidademedida.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Informe a unidade de medida.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbunidademedida.Focus();
                return;
            }

            if (modoEdicao)
            {
                Produto produto = produtos[indiceProdutoEditando];

                produto.Nome = tbnomeproduto.Text;
                produto.Preco = preco;
                produto.Categoria = cbcategoria.Text;
                produto.UnidadeMedida = cbunidademedida.Text;
                produto.EstoqueMinimo = nudestoqueminimo.Value;

                MessageBox.Show(
                    "Produto atualizado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Produto produto = new Produto
                {
                    Nome = tbnomeproduto.Text,
                    Preco = preco,
                    Categoria = cbcategoria.Text,
                    UnidadeMedida = cbunidademedida.Text,
                    EstoqueMinimo = nudestoqueminimo.Value
                };

                produtos.Add(produto);

                MessageBox.Show(
                    "Produto cadastrado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            AtualizarTabela();

            modoEdicao = false;
            indiceProdutoEditando = -1;

            bsalvar.Text = "Salvar";
            beditar.Enabled = true;

            LimparCampos();
        }

        private void bexcluir_Click(object sender, EventArgs e)
        {
            if (dgvprodutos.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um produto para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult resposta = MessageBox.Show(
                "Deseja realmente excluir o produto selecionado?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            Produto produtoSelecionado =
                dgvprodutos.SelectedRows[0].DataBoundItem as Produto;

            if (produtoSelecionado != null)
            {
                produtos.Remove(produtoSelecionado);
                AtualizarTabela();

                MessageBox.Show(
                    "Produto excluído com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void dgvprodutos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string coluna =
                dgvprodutos.Columns[e.ColumnIndex].Name;

            string valor =
                e.FormattedValue == null
                ? ""
                : e.FormattedValue.ToString().Trim();

            if (coluna == "Nome" && string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show(
                    "O nome do produto não pode ficar vazio.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                e.Cancel = true;
                return;
            }

            if (coluna == "Categoria" && string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show(
                    "A categoria não pode ficar vazia.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                e.Cancel = true;
                return;
            }

            if (coluna == "Preco")
            {
                decimal preco;

                bool valorValido =
                    decimal.TryParse(valor, out preco) &&
                    preco > 0;

                if (!valorValido)
                {
                    MessageBox.Show(
                        "Informe um preço válido.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    e.Cancel = true;
                }
            }
        }

        private void beditar_Click(object sender, EventArgs e)
        {
            if (dgvprodutos.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um produto para editar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Produto produtoSelecionado =
                dgvprodutos.SelectedRows[0].DataBoundItem as Produto;

            if (produtoSelecionado == null)
            {
                return;
            }

            indiceProdutoEditando = produtos.IndexOf(produtoSelecionado);

            if (indiceProdutoEditando == -1)
            {
                return;
            }

            tbnomeproduto.Text = produtoSelecionado.Nome;
            tbpreco.Text = produtoSelecionado.Preco.ToString("N2");
            cbcategoria.Text = produtoSelecionado.Categoria;
            cbunidademedida.Text = produtoSelecionado.UnidadeMedida;
            nudestoqueminimo.Value = produtoSelecionado.EstoqueMinimo;

            modoEdicao = true;

            bsalvar.Text = "Atualizar";
            beditar.Enabled = false;

            tbnomeproduto.Focus();
        }
    }
}

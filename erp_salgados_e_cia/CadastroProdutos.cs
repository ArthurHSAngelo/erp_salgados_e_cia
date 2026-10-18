using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace erp_salgados_e_cia
{
    public partial class CadastroProdutos : Form
    {
        private List<Produto> produtos = new List<Produto>();
        private void AtualizarTabela()
        {
            dgvprodutos.DataSource = null;
            dgvprodutos.DataSource = produtos;

            if (dgvprodutos.Columns.Count > 0)
            {
                dgvprodutos.Columns["Nome"].HeaderText = "Nome";
                dgvprodutos.Columns["Preco"].HeaderText = "Preço";
                dgvprodutos.Columns["Categoria"].HeaderText = "Categoria";

                dgvprodutos.Columns["Nome"].DisplayIndex = 0;
                dgvprodutos.Columns["Preco"].DisplayIndex = 1;
                dgvprodutos.Columns["Categoria"].DisplayIndex = 2;

                dgvprodutos.Columns["Preco"].DefaultCellStyle.Format = "N2";
            }
        }
        public CadastroProdutos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

            Produto produto = new Produto
            {
                Nome = tbnomeproduto.Text,
                Categoria = cbcategoria.Text,
                Preco = preco,
                EstoqueMinimo = nudestoqueminimo.Value,
                UnidadeMedida = cbunidademedida.Text
            };


            produtos.Add(produto);

            AtualizarTabela();

            MessageBox.Show(
                 "Produto salvo com sucesso!\n\n" +
                 "Nome: " + produto.Nome + "\n" +
                 "Preço: R$ " + produto.Preco.ToString("N2") + "\n" +
                 "Categoria: " + produto.Categoria + "\n",
                 "Sucesso",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Information
             );

            tbnomeproduto.Clear();
            cbcategoria.SelectedIndex = -1;
            tbpreco.Clear();
            tbnomeproduto.Focus();
            nudestoqueminimo.Value = 1;
            cbunidademedida.SelectedIndex = -1;

            tbnomeproduto.Focus();


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

        private void dgvprodutos_CellValidating(
        object sender,
        DataGridViewCellValidatingEventArgs e)
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
    }
}
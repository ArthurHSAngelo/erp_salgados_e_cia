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
    public partial class ucprodutos : UserControl
    {
        private bool modoEdicao = false;
        private int indiceProdutoEditando = -1;
        private BindingList<Produto> produtos =
            dadossistema.produtos;
        private BindingSource fonteProdutos =
            new BindingSource();

        private void AtualizarTabela()
        {
            string filtro =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Produto> produtosFiltrados =
                produtos;

            if (filtro == "Ativos")
            {
                produtosFiltrados =
                    produtos.Where(p =>
                        p.Status == StatusCadastro.Ativo);
            }
            else if (filtro == "Inativos")
            {
                produtosFiltrados =
                    produtos.Where(p =>
                        p.Status == StatusCadastro.Inativo);
            }

            fonteProdutos.DataSource =
                produtosFiltrados.ToList();

            if (dgvprodutos.Columns.Count > 0)
            {
                dgvprodutos.Columns["Nome"].HeaderText =
                    "Nome";

                dgvprodutos.Columns["Preco"].HeaderText =
                    "Preço";

                dgvprodutos.Columns["Categoria"].HeaderText =
                    "Categoria";

                dgvprodutos.Columns["UnidadeMedida"].HeaderText =
                    "Unidade de medida";

                dgvprodutos.Columns["EstoqueMinimo"].HeaderText =
                    "Estoque mínimo";

                dgvprodutos.Columns["Nome"].DisplayIndex = 0;
                dgvprodutos.Columns["Preco"].DisplayIndex = 1;
                dgvprodutos.Columns["Categoria"].DisplayIndex = 2;
                dgvprodutos.Columns["UnidadeMedida"].DisplayIndex = 3;
                dgvprodutos.Columns["EstoqueMinimo"].DisplayIndex = 4;

                dgvprodutos.Columns["Preco"]
                    .DefaultCellStyle.Format = "N2";

                dgvprodutos.Columns["EstoqueMinimo"]
                    .DefaultCellStyle.Format = "N2";
            }
        }
        private void cbfiltrostatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
                {
                    AtualizarTabela();
                }

        public ucprodutos()
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

            fonteProdutos.DataSource =
                produtos;

            dgvprodutos.DataSource =
                fonteProdutos;

            cbfiltrostatus.SelectedIndex =
                0;

            AtualizarTabela();
        }

        private void LimparCampos()
                {
                    tbnomeproduto.Clear();
                    tbpreco.Clear();

                    cbcategoria.SelectedIndex = -1;
                    cbunidademedida.SelectedIndex = -1;

                    nudestoqueminimo.Value = 1;

                    cbativo.Checked = true;
                    cbativo.Visible = false;

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

                if (!cbativo.Checked &&
                    produto.Status == StatusCadastro.Ativo)
                {
                    DialogResult resposta = MessageBox.Show(
                        "Deseja realmente inativar este produto?",
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


                produto.Nome = tbnomeproduto.Text;
                produto.Preco = preco;
                produto.Categoria = cbcategoria.Text;
                produto.UnidadeMedida = cbunidademedida.Text;
                produto.EstoqueMinimo = nudestoqueminimo.Value;

                produto.Status =
                    cbativo.Checked
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo;

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
                    Status = StatusCadastro.Ativo,
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
            dgvprodutos.Refresh();

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

            Produto produtoSelecionado =
                dgvprodutos.SelectedRows[0]
                .DataBoundItem as Produto;

            if (produtoSelecionado == null)
            {
                return;
            }

            if (dadossistema.ProdutoPossuiVinculos(
                    produtoSelecionado))
            {
                MessageBox.Show(
                    "Exclusão bloqueada: este produto possui movimentações.",
                    "Exclusão não permitida",
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

            if (produtoSelecionado != null)
            {
                produtos.Remove(produtoSelecionado);

                AtualizarTabela();
                dgvprodutos.Refresh();

                MessageBox.Show(
                    "Produto excluído com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
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

            indiceProdutoEditando =
                produtos.IndexOf(produtoSelecionado);

            if (indiceProdutoEditando == -1)
            {
                return;
            }

            tbnomeproduto.Text =
                produtoSelecionado.Nome;

            tbpreco.Text =
                produtoSelecionado.Preco.ToString("N2");

            cbcategoria.Text =
                produtoSelecionado.Categoria;

            cbunidademedida.Text =
                produtoSelecionado.UnidadeMedida;

            nudestoqueminimo.Value =
                produtoSelecionado.EstoqueMinimo;

            cbativo.Checked =
                produtoSelecionado.Status == StatusCadastro.Ativo;

            cbativo.Visible = true;

            modoEdicao = true;

            bsalvar.Text = "Atualizar";
            beditar.Enabled = false;

            tbnomeproduto.Focus();


        }
    }
}

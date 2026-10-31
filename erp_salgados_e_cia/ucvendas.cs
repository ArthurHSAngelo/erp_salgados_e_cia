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
    public partial class ucvendas : UserControl
    {
        private BindingList<ItemVenda> itensVenda =
    new BindingList<ItemVenda>();

        private BindingSource fonteItensVenda =
            new BindingSource();

        private void AtualizarTotalVenda()
        {
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            ltotalvenda.Text =
                $"Total da venda: R$ {totalVenda:N2}";
                AtualizarTroco();
        }

                private void AtualizarTroco()
        {
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            decimal.TryParse(
                tbvalorrecebido.Text,
                out decimal valorRecebido
            );

            decimal troco =
                valorRecebido - totalVenda;

            if (troco < 0)
            {
                ltroco.Text = "Troco: R$ 0,00";
                return;
            }

            ltroco.Text =
                $"Troco: R$ {troco:N2}";
        }
        public ucvendas()
        {
            InitializeComponent();

            fonteItensVenda.DataSource = itensVenda;
            dgvitensvenda.DataSource = fonteItensVenda;
            AtualizarTotalVenda();

            cbproduto.DataSource =
                dadossistema.produtos
                .Where(p =>
                    p.Status == dadossistema.StatusCadastro.Ativo)
                .ToList();

            cbproduto.DisplayMember = "Nome";
            cbproduto.SelectedIndex = -1;
        }

        private void badicionaritem_Click(object sender, EventArgs e)
        {
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


            Produto produtoSelecionado =
                cbproduto.SelectedItem as Produto;

            if (produtoSelecionado == null)
            {
                return;
            }

            decimal quantidadeJaAdicionada =
                 itensVenda
                 .Where(itemExistente =>
                     itemExistente.Produto == produtoSelecionado.Nome)
                 .Sum(itemExistente =>
                     itemExistente.Quantidade);

            decimal quantidadeSolicitada =
                quantidadeJaAdicionada +
                nudquantidade.Value;

            if (quantidadeSolicitada >
                produtoSelecionado.EstoqueAtual)
            {
                MessageBox.Show(
                    $"Estoque insuficiente para o produto {produtoSelecionado.Nome}.",
                    "Venda não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                nudquantidade.Focus();
                return;
            }

            ItemVenda item = new ItemVenda

            {
                Produto = produtoSelecionado.Nome,
                Quantidade = nudquantidade.Value,
                PrecoUnitario = produtoSelecionado.Preco
            };

            itensVenda.Add(item);

            fonteItensVenda.ResetBindings(false);
            AtualizarTotalVenda();

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            cbproduto.Focus();
        }

        private void cbformapagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool pagamentoEmDinheiro =
                cbformapagamento.SelectedItem != null &&
                cbformapagamento.SelectedItem.ToString() == "Dinheiro";

            lvalorrecebido.Visible = pagamentoEmDinheiro;
            tbvalorrecebido.Visible = pagamentoEmDinheiro;
            ltroco.Visible = pagamentoEmDinheiro;

            if (!pagamentoEmDinheiro)
            {
                tbvalorrecebido.Clear();
                ltroco.Text = "Troco: R$ 0,00";
            }
        }

        private void tbvalorrecebido_TextChanged(object sender, EventArgs e)
        {
            AtualizarTroco();
        }

        private void bremoveritem_Click(object sender, EventArgs e)
        {
            if (dgvitensvenda.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um item para remover.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            ItemVenda itemSelecionado =
                dgvitensvenda.SelectedRows[0]
                .DataBoundItem as ItemVenda;

            if (itemSelecionado == null)
            {
                return;
            }

            itensVenda.Remove(itemSelecionado);

            fonteItensVenda.ResetBindings(false);

            AtualizarTotalVenda();

        }

        private void bfinalizarvenda_Click(object sender, EventArgs e)
        {
            if (itensVenda.Count == 0)
            {
                MessageBox.Show(
                    "Adicione pelo menos um item.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cbformapagamento.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione a forma de pagamento.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbformapagamento.Focus();
                return;
            }

            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            if (cbformapagamento.Text == "Dinheiro")
            {
                if (!decimal.TryParse(
                        tbvalorrecebido.Text,
                        out decimal valorRecebido)
                    || valorRecebido < totalVenda)
                {
                    MessageBox.Show(
                        "O valor recebido é insuficiente.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    tbvalorrecebido.Focus();
                    return;
                }
            }

            foreach (var grupo in itensVenda.GroupBy(item => item.Produto))
            {
                Produto produto =
                    dadossistema.produtos
                    .FirstOrDefault(p => p.Nome == grupo.Key);

                decimal quantidadeTotal =
                    grupo.Sum(item => item.Quantidade);

                if (produto == null ||
                    produto.EstoqueAtual < quantidadeTotal)
                {
                    MessageBox.Show(
                        $"Estoque insuficiente para o produto: {grupo.Key}.",
                        "Venda não realizada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }
            }

            DialogResult resposta = MessageBox.Show(
                "Deseja finalizar esta venda?",
                "Confirmar venda",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            foreach (var grupo in itensVenda.GroupBy(item => item.Produto))
            {
                Produto produto =
                    dadossistema.produtos
                    .First(p => p.Nome == grupo.Key);

                produto.EstoqueAtual -=
                    grupo.Sum(item => item.Quantidade);
            }

            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            decimal.TryParse(
                tbvalorrecebido.Text,
                out decimal valorRecebido
            );

            decimal troco =
                valorRecebido - totalVenda;

            Venda venda = new Venda
            {
                IdVenda = dadossistema.vendas.Count + 1,
                DataHora = DateTime.Now,
                FormaPagamento = cbformapagamento.Text,
                ValorTotal = totalVenda,
                ValorRecebido = valorRecebido,
                Troco = troco < 0 ? 0 : troco,
                Status = "Concluída",
                Itens = new BindingList<ItemVenda>(
                    itensVenda
                    .Select(item => new ItemVenda
                    {
                        Produto = item.Produto,
                        Quantidade = item.Quantidade,
                        PrecoUnitario = item.PrecoUnitario
                    })
                    .ToList()
                )
            };

            dadossistema.vendas.Add(venda);

            itensVenda.Clear();

            fonteItensVenda.ResetBindings(false);

            AtualizarTotalVenda();

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            cbformapagamento.SelectedIndex = -1;
            tbvalorrecebido.Clear();

            lvalorrecebido.Visible = false;
            tbvalorrecebido.Visible = false;
            ltroco.Visible = false;

            MessageBox.Show(
                "Venda finalizada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}

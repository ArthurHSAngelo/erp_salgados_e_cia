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

        private BindingList<PagamentoVenda> pagamentosVenda =
            new BindingList<PagamentoVenda>();

        private BindingSource fontePagamentosVenda =
            new BindingSource();

        private void AtualizarTotalVenda()
        {
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            ltotalvenda.Text =
                $"Total da venda: R$ {totalVenda:N2}";

            AtualizarResumoPagamento();
        }

        private void AtualizarResumoPagamento()
        {
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            decimal totalPago =
                pagamentosVenda.Sum(pagamento => pagamento.Valor);

            decimal falta =
                Math.Max(0m, totalVenda - totalPago);

            decimal troco =
                Math.Max(0m, totalPago - totalVenda);

            lpago.Text =
                $"Pago: R$ {totalPago:N2}";

            lfalta.Text =
                $"Falta: R$ {falta:N2}";

            ltroco.Text =
                $"Troco: R$ {troco:N2}";
        }

        public ucvendas()
        {
            InitializeComponent();

            fonteItensVenda.DataSource = itensVenda;
            dgvitensvenda.DataSource = fonteItensVenda;

            fontePagamentosVenda.DataSource = pagamentosVenda;
            dgvpagamentosvenda.DataSource = fontePagamentosVenda;

            if (dgvpagamentosvenda.Columns.Count > 0)
            {
                dgvpagamentosvenda.Columns["FormaPagamento"].HeaderText =
                    "Forma de pagamento";

                dgvpagamentosvenda.Columns["Valor"]
                    .DefaultCellStyle.Format = "N2";
            }

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
            if (cbformapagamento.SelectedIndex == -1)
            {
                tbvalorrecebido.Clear();
                return;
            }

            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            decimal totalPago =
                pagamentosVenda.Sum(pagamento => pagamento.Valor);

            decimal falta =
                totalVenda - totalPago;

            tbvalorrecebido.Text =
                falta > 0
                ? falta.ToString("F2")
                : "";
        }

        private void tbvalorrecebido_TextChanged(object sender, EventArgs e)
        {
            AtualizarResumoPagamento();
        }

        private void badicionarpagamento_Click(object sender, EventArgs e)
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

            string formaPagamento =
                cbformapagamento.Text;

            if (formaPagamento == "Notinha")
            {
                MessageBox.Show(
                    "Notinha disponível em breve.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!decimal.TryParse(
                    tbvalorrecebido.Text,
                    out decimal valorPagamento)
                || valorPagamento <= 0)
            {
                MessageBox.Show(
                    "Informe um valor válido.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbvalorrecebido.Focus();
                return;
            }

            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            decimal totalPago =
                pagamentosVenda.Sum(pagamento => pagamento.Valor);

            decimal falta =
                totalVenda - totalPago;

            if (falta <= 0)
            {
                MessageBox.Show(
                    "Esta venda já está totalmente paga.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (formaPagamento != "Dinheiro" &&
                valorPagamento > falta)
            {
                MessageBox.Show(
                    $"O valor em {formaPagamento} não pode ser maior que o que falta pagar (R$ {falta:N2}).",
                    "Pagamento não permitido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbvalorrecebido.Focus();
                return;
            }

            PagamentoVenda pagamentoExistente =
                pagamentosVenda
                .FirstOrDefault(pagamento =>
                    pagamento.FormaPagamento == formaPagamento);

            if (pagamentoExistente != null)
            {
                pagamentoExistente.Valor += valorPagamento;
            }
            else
            {
                pagamentosVenda.Add(
                    new PagamentoVenda
                    {
                        FormaPagamento = formaPagamento,
                        Valor = valorPagamento
                    }
                );
            }

            fontePagamentosVenda.ResetBindings(false);
            AtualizarResumoPagamento();

            cbformapagamento.SelectedIndex = -1;
            cbformapagamento.Focus();
        }

        private void bremoverpagamento_Click(object sender, EventArgs e)
        {
            if (dgvpagamentosvenda.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um pagamento para remover.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            PagamentoVenda pagamentoSelecionado =
                dgvpagamentosvenda.SelectedRows[0]
                .DataBoundItem as PagamentoVenda;

            if (pagamentoSelecionado == null)
            {
                return;
            }

            pagamentosVenda.Remove(pagamentoSelecionado);

            fontePagamentosVenda.ResetBindings(false);
            AtualizarResumoPagamento();
        }

        private void bremoveritem_Click(object sender, EventArgs e)
        {
            if (pagamentosVenda.Count > 0)
            {
                MessageBox.Show(
                    "Remova os pagamentos antes de remover itens.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

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
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            decimal totalPago =
                pagamentosVenda.Sum(pagamento => pagamento.Valor);

            decimal totalDinheiro =
                pagamentosVenda
                .Where(pagamento => pagamento.FormaPagamento == "Dinheiro")
                .Sum(pagamento => pagamento.Valor);

            decimal troco =
                Math.Max(0m, totalPago - totalVenda);

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

            if (pagamentosVenda.Count == 0)
            {
                MessageBox.Show(
                    "Adicione pelo menos um pagamento.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbformapagamento.Focus();
                return;
            }

            if (totalPago < totalVenda)
            {
                MessageBox.Show(
                    $"Ainda faltam R$ {totalVenda - totalPago:N2} para cobrir o total da venda.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (troco > totalDinheiro)
            {
                MessageBox.Show(
                    "O troco não pode ser maior que o valor pago em dinheiro.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
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

            Venda venda = new Venda
            {
                NumeroMesa = (int)nudnumeromesa.Value,
                IdVenda = dadossistema.vendas.Count + 1,
                DataHora = DateTime.Now,
                FormaPagamento = string.Join(
                    " + ",
                    pagamentosVenda.Select(pagamento =>
                        $"{pagamento.FormaPagamento} R$ {pagamento.Valor:N2}")
                ),
                ValorTotal = totalVenda,
                ValorRecebido = totalPago,
                Troco = troco,
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
                ),
                Pagamentos = new BindingList<PagamentoVenda>(
                    pagamentosVenda
                    .Select(pagamento => new PagamentoVenda
                    {
                        FormaPagamento = pagamento.FormaPagamento,
                        Valor = pagamento.Valor
                    })
                    .ToList()
                )
            };

            dadossistema.vendas.Add(venda);

            itensVenda.Clear();
            pagamentosVenda.Clear();

            fonteItensVenda.ResetBindings(false);
            fontePagamentosVenda.ResetBindings(false);

            AtualizarTotalVenda();

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            cbformapagamento.SelectedIndex = -1;
            tbvalorrecebido.Clear();
            nudnumeromesa.Value = 1;

            MessageBox.Show(
                "Venda finalizada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}

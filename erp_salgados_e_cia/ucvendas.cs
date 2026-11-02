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
        }

        public void AtualizarCombos()
        {
            Produto produtoAnterior =
                cbproduto.SelectedItem as Produto;

            List<Produto> produtosAtivos =
                dadossistema.produtos
                .Where(p =>
                    p.Status == dadossistema.StatusCadastro.Ativo)
                .ToList();

            cbproduto.DataSource = produtosAtivos;
            cbproduto.DisplayMember = "Nome";

            cbproduto.SelectedIndex =
                produtoAnterior == null
                ? -1
                : produtosAtivos.FindIndex(p =>
                    p.Nome == produtoAnterior.Nome);

            Cliente clienteAnterior =
                cbcliente.SelectedItem as Cliente;

            int idAnterior =
                clienteAnterior == null
                ? 0
                : clienteAnterior.IdCliente;

            List<Cliente> opcoesCliente =
                new List<Cliente>();

            opcoesCliente.Add(
                new Cliente
                {
                    IdCliente = 0,
                    Nome = "Sem cliente"
                }
            );

            opcoesCliente.AddRange(
                dadossistema.clientes
                .Where(c =>
                    c.Status == dadossistema.StatusCadastro.Ativo)
            );

            cbcliente.DataSource = opcoesCliente;
            cbcliente.DisplayMember = "Descricao";

            cbcliente.SelectedIndex =
                Math.Max(
                    0,
                    opcoesCliente.FindIndex(c =>
                        c.IdCliente == idAnterior)
                );
        }

        public ucvendas()
        {
            InitializeComponent();

            fonteItensVenda.DataSource = itensVenda;
            dgvitensvenda.DataSource = fonteItensVenda;

            AtualizarTotalVenda();

            AtualizarCombos();
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

            ItemVenda itemNaLista =
                itensVenda
                .FirstOrDefault(itemExistente =>
                    itemExistente.Produto == produtoSelecionado.Nome);

            if (itemNaLista != null)
            {
                itemNaLista.Quantidade += nudquantidade.Value;
            }
            else
            {
                itensVenda.Add(
                    new ItemVenda
                    {
                        Produto = produtoSelecionado.Nome,
                        Quantidade = nudquantidade.Value,
                        PrecoUnitario = produtoSelecionado.Preco
                    }
                );
            }

            fonteItensVenda.ResetBindings(false);
            AtualizarTotalVenda();

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            cbproduto.Focus();
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
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

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

            BindingList<PagamentoVenda> pagamentosCobrados;

            using (frmcobranca cobranca = new frmcobranca(totalVenda))
            {
                if (cobranca.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                pagamentosCobrados = cobranca.Pagamentos;
            }

            decimal totalPago =
                pagamentosCobrados.Sum(pagamento => pagamento.Valor);

            decimal troco =
                Math.Max(0m, totalPago - totalVenda);

            foreach (var grupo in itensVenda.GroupBy(item => item.Produto))
            {
                Produto produto =
                    dadossistema.produtos
                    .First(p => p.Nome == grupo.Key);

                produto.EstoqueAtual -=
                    grupo.Sum(item => item.Quantidade);
            }

            Cliente clienteSelecionado =
                cbcliente.SelectedItem as Cliente;

            int? idClienteVenda =
                clienteSelecionado == null ||
                clienteSelecionado.IdCliente == 0
                ? (int?)null
                : clienteSelecionado.IdCliente;

            Venda venda = new Venda
            {
                NumeroMesa = (int)nudnumeromesa.Value,
                IdVenda = dadossistema.vendas.Count + 1,
                DataHora = DateTime.Now,
                FormaPagamento = string.Join(
                    " + ",
                    pagamentosCobrados.Select(pagamento =>
                        $"{pagamento.FormaPagamento} R$ {pagamento.Valor:N2}")
                ),
                ValorTotal = totalVenda,
                ValorRecebido = totalPago,
                Troco = troco,
                Status = "Concluída",
                IdCliente = idClienteVenda,
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
                    pagamentosCobrados
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

            fonteItensVenda.ResetBindings(false);

            AtualizarTotalVenda();

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            nudnumeromesa.Value = 1;
            cbcliente.SelectedIndex = 0;

            MessageBox.Show(
                "Venda finalizada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
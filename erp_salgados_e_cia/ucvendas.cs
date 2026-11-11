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

        private BindingList<ItemVenda> itensComandaVazia =
            new BindingList<ItemVenda>();

        private BindingSource fonteItensComanda =
            new BindingSource();

        private bool visaoTinhaComanda;

        private void AtualizarTotalVenda()
        {
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            ltotalvenda.Text =
                $"Total do pedido: R$ {totalVenda:N2}";
        }

        private bool CaixaEstaAberto()
        {
            if (dadossistema.CaixaAberto() != null)
            {
                return true;
            }

            MessageBox.Show(
                "Abra o caixa antes de registrar vendas.",
                "Caixa fechado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return false;
        }

        private void AtualizarVisaoComanda()
        {
            lavisocaixa.Visible =
                dadossistema.CaixaAberto() == null;

            int numeroMesa =
                (int)nudnumeromesa.Value;

            Comanda comanda =
                numeroMesa == 0
                ? null
                : dadossistema.ComandaDaMesa(numeroMesa);

            bool temComanda =
                comanda != null;

            lcomandamesa.Text =
                numeroMesa == 0
                ? "Balcão"
                : $"Comanda - Mesa {numeroMesa}";

            bfinalizarvenda.Text =
                numeroMesa == 0
                ? "Cobrar"
                : "Enviar pedido";

            fonteItensComanda.DataSource =
                temComanda
                ? comanda.Itens
                : itensComandaVazia;

            fonteItensComanda.ResetBindings(false);

            lsemcomanda.Text =
                numeroMesa == 0
                ? "Balcão: venda direta, sem comanda"
                : "Mesa sem comanda aberta";

            lsemcomanda.Visible = !temComanda;
            dgvitenscomanda.Visible = temComanda;

            ltotalcomanda.Visible = temComanda;
            lpagocomanda.Visible = temComanda;
            lfaltacomanda.Visible = temComanda;
            bcobrar.Visible = temComanda;
            bcancelarcomanda.Visible = temComanda;

            if (temComanda)
            {
                ltotalcomanda.Text =
                    $"Total: R$ {comanda.Total:N2}";

                lpagocomanda.Text =
                    $"Pago: R$ {comanda.TotalPago:N2}";

                lfaltacomanda.Text =
                    $"Falta: R$ {comanda.Falta:N2}";

                List<Cliente> opcoesCliente =
                    cbcliente.DataSource as List<Cliente>;

                if (opcoesCliente != null)
                {
                    int idComanda =
                        comanda.IdCliente ?? 0;

                    cbcliente.SelectedIndex =
                        Math.Max(
                            0,
                            opcoesCliente.FindIndex(c =>
                                c.IdCliente == idComanda)
                        );
                }
            }
            else if (visaoTinhaComanda &&
                     cbcliente.Items.Count > 0)
            {
                cbcliente.SelectedIndex = 0;
            }

            visaoTinhaComanda = temComanda;
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

            AtualizarVisaoComanda();
        }

        public ucvendas()
        {
            InitializeComponent();

            fonteItensVenda.DataSource = itensVenda;
            dgvitensvenda.DataSource = fonteItensVenda;

            fonteItensComanda.DataSource = itensComandaVazia;
            dgvitenscomanda.DataSource = fonteItensComanda;

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

        private void bmesasabertas_Click(object sender, EventArgs e)
        {
            using (frmmesas janela = new frmmesas())
            {
                if (janela.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                nudnumeromesa.Value = janela.MesaSelecionada;
            }

            AtualizarVisaoComanda();
        }

        private void nudnumeromesa_ValueChanged(object sender, EventArgs e)
        {
            AtualizarVisaoComanda();
        }

        private void cbcliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Comanda comanda =
                dadossistema.ComandaDaMesa((int)nudnumeromesa.Value);

            if (comanda == null)
            {
                return;
            }

            Cliente clienteSelecionado =
                cbcliente.SelectedItem as Cliente;

            int? novoCliente =
                clienteSelecionado == null ||
                clienteSelecionado.IdCliente == 0
                ? (int?)null
                : clienteSelecionado.IdCliente;

            if (novoCliente != comanda.IdCliente &&
                comanda.Pagamentos.Any(pagamento =>
                    pagamento.FormaPagamento == "Notinha"))
            {
                MessageBox.Show(
                    "Não é possível trocar o cliente: a comanda já possui pagamento em Notinha.",
                    "Cliente bloqueado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                AtualizarVisaoComanda();
                return;
            }

            comanda.IdCliente = novoCliente;
        }

        private void LimparPedido()
        {
            itensVenda.Clear();

            fonteItensVenda.ResetBindings(false);

            AtualizarTotalVenda();

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
        }

        private void BaixarEstoquePedido(int numeroMesa)
        {
            foreach (var grupo in itensVenda.GroupBy(item => item.Produto))
            {
                Produto produto =
                    dadossistema.produtos
                    .First(p => p.Nome == grupo.Key);

                decimal quantidadeBaixada =
                    grupo.Sum(item => item.Quantidade);

                produto.EstoqueAtual -= quantidadeBaixada;

                dadossistema.RegistrarHistoricoEstoque(
                    produto,
                    TipoMovimentoEstoque.Venda,
                    quantidadeBaixada,
                    dadossistema.CaixaAberto()?.Responsavel ?? "",
                    numeroMesa == 0 ? "Balcão" : $"Mesa {numeroMesa}"
                );
            }
        }

        private void EnviarPedidoParaComanda(int numeroMesa, int? idCliente)
        {
            Comanda comanda =
                dadossistema.ComandaDaMesa(numeroMesa);

            if (comanda == null)
            {
                comanda = new Comanda
                {
                    NumeroMesa = numeroMesa,
                    IdCliente = idCliente
                };

                dadossistema.comandas.Add(comanda);
            }

            foreach (ItemVenda item in itensVenda)
            {
                ItemVenda itemNaComanda =
                    comanda.Itens
                    .FirstOrDefault(itemExistente =>
                        itemExistente.Produto == item.Produto &&
                        itemExistente.PrecoUnitario == item.PrecoUnitario);

                if (itemNaComanda != null)
                {
                    itemNaComanda.Quantidade += item.Quantidade;
                }
                else
                {
                    comanda.Itens.Add(
                        new ItemVenda
                        {
                            Produto = item.Produto,
                            Quantidade = item.Quantidade,
                            PrecoUnitario = item.PrecoUnitario
                        }
                    );
                }
            }

            BaixarEstoquePedido(numeroMesa);

            LimparPedido();

            AtualizarVisaoComanda();
        }

        private Venda CriarVenda(
            int numeroMesa,
            decimal totalVenda,
            int? idCliente,
            IEnumerable<ItemVenda> itens,
            IEnumerable<PagamentoVenda> pagamentos)
        {
            decimal totalPago =
                pagamentos.Sum(pagamento => pagamento.Valor);

            return new Venda
            {
                NumeroMesa = numeroMesa,
                IdCaixa = dadossistema.CaixaAberto().IdCaixa,
                IdVenda = dadossistema.vendas.Count + 1,
                DataHora = DateTime.Now,
                FormaPagamento = string.Join(
                    " + ",
                    pagamentos.Select(pagamento =>
                        $"{pagamento.FormaPagamento} R$ {pagamento.Valor:N2}")
                ),
                ValorTotal = totalVenda,
                ValorRecebido = pagamentos
                    .Where(pagamento => pagamento.FormaPagamento != "Notinha")
                    .Sum(pagamento => pagamento.Valor),
                Troco = Math.Max(0m, totalPago - totalVenda),
                Status = "Concluída",
                IdCliente = idCliente,
                Itens = new BindingList<ItemVenda>(
                    itens
                    .Select(item => new ItemVenda
                    {
                        Produto = item.Produto,
                        Quantidade = item.Quantidade,
                        PrecoUnitario = item.PrecoUnitario
                    })
                    .ToList()
                ),
                Pagamentos = new BindingList<PagamentoVenda>(
                    pagamentos
                    .Select(pagamento => new PagamentoVenda
                    {
                        FormaPagamento = pagamento.FormaPagamento,
                        Valor = pagamento.Valor
                    })
                    .ToList()
                )
            };
        }

        private void CobrarPedidoNoBalcao(int? idCliente)
        {
            decimal totalVenda =
                itensVenda.Sum(item => item.Total);

            BindingList<PagamentoVenda> pagamentosCobrados;

            using (frmcobranca cobranca = new frmcobranca(
                totalVenda,
                0m,
                false,
                idCliente != null))
            {
                if (cobranca.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                pagamentosCobrados = cobranca.Pagamentos;
            }

            BaixarEstoquePedido(0);

            Venda vendaBalcao = CriarVenda(
                0,
                totalVenda,
                idCliente,
                itensVenda,
                pagamentosCobrados
            );

            dadossistema.vendas.Add(vendaBalcao);

            dadossistema.RegistrarLogVenda(vendaBalcao);

            dadossistema.LancarNotinhaDaVenda(vendaBalcao);

            LimparPedido();

            cbcliente.SelectedIndex = 0;

            MessageBox.Show(
                "Venda finalizada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void bcobrar_Click(object sender, EventArgs e)
        {
            int numeroMesa =
                (int)nudnumeromesa.Value;

            Comanda comanda =
                dadossistema.ComandaDaMesa(numeroMesa);

            if (comanda == null)
            {
                return;
            }

            if (!CaixaEstaAberto())
            {
                return;
            }

            BindingList<PagamentoVenda> pagamentosNovos;
            bool pagamentoParcial;

            using (frmcobranca cobranca = new frmcobranca(
                comanda.Total,
                comanda.TotalPago,
                true,
                comanda.IdCliente != null))
            {
                if (cobranca.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                pagamentosNovos = cobranca.Pagamentos;
                pagamentoParcial = cobranca.PagamentoParcial;
            }

            foreach (PagamentoVenda pagamentoNovo in pagamentosNovos)
            {
                PagamentoVenda pagamentoExistente =
                    comanda.Pagamentos
                    .FirstOrDefault(pagamento =>
                        pagamento.FormaPagamento ==
                        pagamentoNovo.FormaPagamento);

                if (pagamentoExistente != null)
                {
                    pagamentoExistente.Valor += pagamentoNovo.Valor;
                }
                else
                {
                    comanda.Pagamentos.Add(
                        new PagamentoVenda
                        {
                            FormaPagamento = pagamentoNovo.FormaPagamento,
                            Valor = pagamentoNovo.Valor
                        }
                    );
                }
            }

            if (pagamentoParcial)
            {
                AtualizarVisaoComanda();
                return;
            }

            Venda vendaComanda = CriarVenda(
                comanda.NumeroMesa,
                comanda.Total,
                comanda.IdCliente,
                comanda.Itens,
                comanda.Pagamentos
            );

            dadossistema.vendas.Add(vendaComanda);

            dadossistema.RegistrarLogVenda(vendaComanda);

            dadossistema.LancarNotinhaDaVenda(vendaComanda);

            dadossistema.comandas.Remove(comanda);

            AtualizarVisaoComanda();

            MessageBox.Show(
                "Venda finalizada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void bcancelarcomanda_Click(object sender, EventArgs e)
        {
            Comanda comanda =
                dadossistema.ComandaDaMesa((int)nudnumeromesa.Value);

            if (comanda == null)
            {
                return;
            }

            if (comanda.Pagamentos.Count > 0)
            {
                MessageBox.Show(
                    "Cancelamento bloqueado: esta comanda já possui pagamentos.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult resposta = MessageBox.Show(
                $"Deseja cancelar a comanda da mesa {comanda.NumeroMesa}? Os itens voltarão ao estoque.",
                "Cancelar comanda",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            foreach (ItemVenda item in comanda.Itens)
            {
                Produto produto =
                    dadossistema.produtos
                    .FirstOrDefault(p => p.Nome == item.Produto);

                if (produto != null)
                {
                    produto.EstoqueAtual += item.Quantidade;

                    dadossistema.RegistrarHistoricoEstoque(
                        produto,
                        TipoMovimentoEstoque.Estorno,
                        item.Quantidade,
                        dadossistema.CaixaAberto()?.Responsavel ?? "",
                        $"Mesa {comanda.NumeroMesa}"
                    );
                }
            }

            dadossistema.comandas.Remove(comanda);

            AtualizarVisaoComanda();
        }
        private void bfinalizarvenda_Click(object sender, EventArgs e)
        {
            if (!CaixaEstaAberto())
            {
                return;
            }

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

            Cliente clienteSelecionado =
                cbcliente.SelectedItem as Cliente;

            int? idClienteVenda =
                clienteSelecionado == null ||
                clienteSelecionado.IdCliente == 0
                ? (int?)null
                : clienteSelecionado.IdCliente;

            int numeroMesa =
                (int)nudnumeromesa.Value;

            if (numeroMesa == 0)
            {
                CobrarPedidoNoBalcao(idClienteVenda);
            }
            else
            {
                EnviarPedidoParaComanda(numeroMesa, idClienteVenda);
            }
        }

        private void lnumeromesa_Click(object sender, EventArgs e)
        {

        }
    }
}
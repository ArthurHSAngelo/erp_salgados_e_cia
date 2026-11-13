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
    public partial class ucclientes : UserControl
    {
        private bool modoEdicao = false;
        private int indiceClienteEditando = -1;

        private BindingList<Cliente> clientes =
            dadossistema.clientes;

        private BindingSource fonteClientes =
            new BindingSource();

        private BindingSource fonteHistoricoNotinha =
            new BindingSource();

        private Cliente ClienteSelecionadoNaGrade()
        {
            if (dgvclientes.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvclientes.SelectedRows[0]
                .DataBoundItem as Cliente;
        }

        private void AtualizarPainelNotinha()
        {
            Cliente cliente = ClienteSelecionadoNaGrade();

            if (cliente == null)
            {
                lsemclientenotinha.Visible = true;
                bpagarnotinha.Enabled = false;

                lnotinhacliente.Text = "Notinha do cliente";
                lsaldonotinha.Text = "Saldo devido: R$ 0,00";

                fonteHistoricoNotinha.DataSource =
                    new List<MovimentoNotinha>();

                return;
            }

            lsemclientenotinha.Visible = false;
            bpagarnotinha.Enabled = true;

            lnotinhacliente.Text = $"Notinha - {cliente.Nome}";

            lsaldonotinha.Text =
                $"Saldo devido: R$ {dadossistema.SaldoNotinha(cliente.IdCliente):N2}";

            List<MovimentoNotinha> movimentos =
                dadossistema.notinhas
                .Where(n =>
                    n.IdCliente == cliente.IdCliente &&
                    n.Unidade == dadossistema.UnidadeAtual)
                .Select(n => new MovimentoNotinha
                {
                    DataHora = n.DataHora,
                    Tipo = "Notinha",
                    Valor = n.Valor,
                    Detalhe = ProdutosDaVenda(n.IdVenda)
                })
                .Concat(
                    dadossistema.abatimentos
                    .Where(a =>
                        a.IdCliente == cliente.IdCliente &&
                        a.Unidade == dadossistema.UnidadeAtual)
                    .Select(a => new MovimentoNotinha
                    {
                        DataHora = a.DataHora,
                        Tipo = "Abatimento",
                        Valor = a.Valor,
                        Detalhe = a.FormaPagamento
                    })
                )
                .OrderByDescending(m => m.DataHora)
                .ToList();

            fonteHistoricoNotinha.DataSource = movimentos;

            dgvhistoriconotinha.Columns["DataHora"].HeaderText =
                "Data/Hora";

            dgvhistoriconotinha.Columns["DataHora"]
                .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvhistoriconotinha.Columns["Valor"]
                .DefaultCellStyle.Format = "C2";
        }

        private string ProdutosDaVenda(int idVenda)
        {
            Venda venda =
                dadossistema.vendas
                .FirstOrDefault(v => v.IdVenda == idVenda);

            if (venda == null)
            {
                return "";
            }

            return string.Join(
                ", ",
                venda.Itens.Select(item =>
                    $"{item.Quantidade:0.##}x {item.Produto}")
            );
        }

        private void SelecionarClienteNaGrade(int idCliente)
        {
            foreach (DataGridViewRow linha in dgvclientes.Rows)
            {
                Cliente cliente = linha.DataBoundItem as Cliente;

                if (cliente != null && cliente.IdCliente == idCliente)
                {
                    dgvclientes.ClearSelection();
                    linha.Selected = true;

                    DataGridViewCell celula =
                        linha.Cells.Cast<DataGridViewCell>()
                        .FirstOrDefault(c => c.Visible);

                    if (celula != null)
                    {
                        dgvclientes.CurrentCell = celula;
                    }

                    return;
                }
            }
        }

        private string ApenasDigitos(string texto)
        {
            return new string(
                (texto ?? "")
                    .Where(char.IsDigit)
                    .ToArray()
            );
        }

        public void AtualizarTabela()
        {
            string filtro =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Cliente> clientesFiltrados =
                clientes;

            if (filtro == "Ativos")
            {
                clientesFiltrados =
                    clientes.Where(c =>
                        c.Status == StatusCadastro.Ativo);
            }
            else if (filtro == "Inativos")
            {
                clientesFiltrados =
                    clientes.Where(c =>
                        c.Status == StatusCadastro.Inativo);
            }

            fonteClientes.DataSource =
                clientesFiltrados.ToList();

            if (dgvclientes.Columns.Count > 0)
            {
                dgvclientes.Columns["IdCliente"].HeaderText =
                    "ID";

                dgvclientes.Columns["CpfCnpj"].HeaderText =
                    "CPF/CNPJ";

                dgvclientes.Columns["Descricao"].Visible = false;

                dgvclientes.Columns["SaldoNotinha"].HeaderText =
                    "Saldo";

                dgvclientes.Columns["SaldoNotinha"].DefaultCellStyle.Format =
                    "C2";
            }

            AtualizarPainelNotinha();
        }

        public ucclientes()
        {
            InitializeComponent();

            dadossistema.AplicarMascara(
                tbcpfcnpjc,
                dadossistema.FormatarCpfCnpj,
                14);

            dadossistema.AplicarMascara(
                tbtelefonec,
                dadossistema.FormatarTelefone,
                11);

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

            fonteClientes.DataSource =
                clientes;

            dgvclientes.DataSource =
                fonteClientes;

            dgvhistoriconotinha.DataSource =
                fonteHistoricoNotinha;

            cbfiltrostatus.SelectedIndex =
                0;

            AtualizarTabela();

            dgvclientes.SelectionChanged +=
                dgvclientes_SelectionChanged;

            AtualizarPainelNotinha();
        }

        private void dgvclientes_SelectionChanged(
            object sender,
            EventArgs e)
        {
            AtualizarPainelNotinha();
        }

        private void bpagarnotinha_Click(object sender, EventArgs e)
        {
            Caixa caixaAberto = dadossistema.CaixaAberto();

            if (caixaAberto == null)
            {
                MessageBox.Show(
                    "Abra o caixa antes de registrar pagamentos.",
                    "Caixa fechado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Cliente cliente = ClienteSelecionadoNaGrade();

            if (cliente == null)
            {
                MessageBox.Show(
                    "Selecione um cliente para pagar a notinha.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            decimal saldo =
                dadossistema.SaldoNotinha(cliente.IdCliente);

            if (saldo <= 0)
            {
                MessageBox.Show(
                    "Este cliente não possui saldo de Notinha em aberto.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            BindingList<PagamentoVenda> pagamentosCobrados;

            using (frmcobranca cobranca = new frmcobranca(
                saldo,
                0m,
                true,
                true,
                true))
            {
                if (cobranca.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                pagamentosCobrados = cobranca.Pagamentos;
            }

            decimal totalPago =
                pagamentosCobrados.Sum(pagamento => pagamento.Valor);

            decimal restante = saldo;

            foreach (PagamentoVenda pagamento in pagamentosCobrados
                .OrderBy(p => p.FormaPagamento == "Dinheiro"))
            {
                decimal valorAbatido =
                    Math.Min(pagamento.Valor, restante);

                if (valorAbatido <= 0)
                {
                    break;
                }

                dadossistema.abatimentos.Add(
                    new AbatimentoNotinha
                    {
                        IdAbatimento = dadossistema.ProximoIdAbatimento(),
                        IdCliente = cliente.IdCliente,
                        DataHora = DateTime.Now,
                        Valor = valorAbatido,
                        FormaPagamento = pagamento.FormaPagamento,
                        IdCaixa = caixaAberto.IdCaixa,
                        Unidade = caixaAberto.Unidade
                    }
                );

                dadossistema.RegistrarLog(
                    TipoOperacaoLog.Inclusao,
                    "AbatimentoNotinha",
                    $"Cliente {cliente.IdCliente}: {cliente.Nome}",
                    $"Valor: {valorAbatido:N2}; Forma: {pagamento.FormaPagamento}; Caixa {caixaAberto.IdCaixa}");

                restante -= valorAbatido;
            }

            decimal troco = Math.Max(0m, totalPago - saldo);

            AtualizarTabela();
            SelecionarClienteNaGrade(cliente.IdCliente);
            AtualizarPainelNotinha();

            MessageBox.Show(
                troco > 0
                ? $"Pagamento registrado com sucesso.{Environment.NewLine}Troco: R$ {troco:N2}"
                : "Pagamento registrado com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void cbfiltrostatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AtualizarTabela();
        }

        protected override bool ProcessCmdKey(
            ref Message msg,
            Keys keyData)
        {
            if (keyData == Keys.Escape && modoEdicao)
            {
                CancelarEdicao();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CancelarEdicao()
        {
            modoEdicao = false;
            indiceClienteEditando = -1;

            bsalvarcliente.Text = "Salvar cliente";
            beditarc.Enabled = true;

            LimparCampos();
        }

        private void LimparCampos()
        {
            tbnomec.Clear();
            tbcpfcnpjc.Clear();
            tbtelefonec.Clear();
            cbativo.Checked = true;
            cbativo.Visible = false;
            ldicaedicao.Visible = false;

            tbnomec.Focus();
        }

        private void bsalvarcliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbnomec.Text))
            {
                MessageBox.Show(
                    "Informe o nome do cliente.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnomec.Focus();
                return;
            }

            string cpfCnpjDigitado =
                ApenasDigitos(tbcpfcnpjc.Text);

            if (cpfCnpjDigitado.Length > 0 &&
                cpfCnpjDigitado.Length != 11 &&
                cpfCnpjDigitado.Length != 14)
            {
                MessageBox.Show(
                    "Informe um CPF (11 dígitos) ou CNPJ (14 dígitos) válido.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcpfcnpjc.Focus();
                return;
            }

            int digitosTelefone =
                ApenasDigitos(tbtelefonec.Text).Length;

            if (digitosTelefone > 0 &&
                digitosTelefone != 10 &&
                digitosTelefone != 11)
            {
                MessageBox.Show(
                    "Informe o telefone com DDD (10 ou 11 dígitos).",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbtelefonec.Focus();
                return;
            }

            Cliente clienteAtual = null;

            if (modoEdicao && indiceClienteEditando >= 0)
            {
                clienteAtual = clientes[indiceClienteEditando];
            }

            bool cpfCnpjExiste =
                cpfCnpjDigitado.Length > 0 &&
                clientes.Any(c =>
                    c != clienteAtual &&
                    ApenasDigitos(c.CpfCnpj) == cpfCnpjDigitado
                );

            if (cpfCnpjExiste)
            {
                MessageBox.Show(
                    "Esse CPF/CNPJ já está cadastrado.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcpfcnpjc.Focus();
                return;
            }

            if (modoEdicao)
            {
                if (!cbativo.Checked &&
                    clienteAtual.Status == StatusCadastro.Ativo)
                {
                    if (dadossistema.SaldoNotinha(
                            clienteAtual.IdCliente) > 0)
                    {
                        MessageBox.Show(
                            "Cliente possui saldo de Notinha em aberto.",
                            "Inativação não permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        cbativo.Checked = true;
                        return;
                    }

                    DialogResult resposta = MessageBox.Show(
                        "Deseja realmente inativar este cliente?",
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

                string registroLog =
                    $"Cliente {clienteAtual.IdCliente}: {clienteAtual.Nome}";

                string detalhesLog = dadossistema.JuntarDetalhes(
                    dadossistema.Alteracao(
                        "Nome", clienteAtual.Nome, tbnomec.Text.Trim()),
                    dadossistema.Alteracao(
                        "CPF/CNPJ", clienteAtual.CpfCnpj, tbcpfcnpjc.Text.Trim()),
                    dadossistema.Alteracao(
                        "Telefone", clienteAtual.Telefone, tbtelefonec.Text.Trim()),
                    dadossistema.Alteracao(
                        "Status",
                        clienteAtual.Status.ToString(),
                        (cbativo.Checked
                            ? StatusCadastro.Ativo
                            : StatusCadastro.Inativo).ToString())
                );

                clienteAtual.Nome = tbnomec.Text.Trim();
                clienteAtual.CpfCnpj = tbcpfcnpjc.Text.Trim();
                clienteAtual.Telefone = tbtelefonec.Text.Trim();

                clienteAtual.Status =
                    cbativo.Checked
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo;

                if (detalhesLog != null)
                {
                    dadossistema.RegistrarLog(
                        TipoOperacaoLog.Alteracao,
                        "Cliente",
                        registroLog,
                        detalhesLog);
                }

                MessageBox.Show(
                    "Cliente atualizado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Cliente cliente = new Cliente
                {
                    IdCliente = dadossistema.ProximoIdCliente(),
                    Status = StatusCadastro.Ativo,
                    Nome = tbnomec.Text.Trim(),
                    CpfCnpj = tbcpfcnpjc.Text.Trim(),
                    Telefone = tbtelefonec.Text.Trim()
                };

                clientes.Add(cliente);

                dadossistema.RegistrarLog(
                    TipoOperacaoLog.Inclusao,
                    "Cliente",
                    $"Cliente {cliente.IdCliente}: {cliente.Nome}",
                    $"CPF/CNPJ: {cliente.CpfCnpj}; Telefone: {cliente.Telefone}");

                MessageBox.Show(
                    "Cliente salvo com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            AtualizarTabela();
            dgvclientes.Refresh();

            modoEdicao = false;
            indiceClienteEditando = -1;

            bsalvarcliente.Text = "Salvar cliente";
            beditarc.Enabled = true;

            LimparCampos();
        }

        private void bexcluirc_Click(object sender, EventArgs e)
        {
            if (dgvclientes.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um cliente para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Cliente clienteSelecionado =
                dgvclientes.SelectedRows[0]
                .DataBoundItem as Cliente;

            if (clienteSelecionado == null)
            {
                return;
            }

            if (dadossistema.ClientePossuiVinculos(
                    clienteSelecionado))
            {
                MessageBox.Show(
                    "Exclusão bloqueada: este cliente possui vendas.",
                    "Exclusão não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult resposta = MessageBox.Show(
                "Deseja excluir o cliente selecionado?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            string registroExcluido =
                $"Cliente {clienteSelecionado.IdCliente}: {clienteSelecionado.Nome}";

            clientes.Remove(clienteSelecionado);

            dadossistema.RegistrarLog(
                TipoOperacaoLog.Exclusao,
                "Cliente",
                registroExcluido,
                null);

            AtualizarTabela();
            dgvclientes.Refresh();

            MessageBox.Show(
                "Cliente excluído com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void beditarc_Click(object sender, EventArgs e)
        {
            if (dgvclientes.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um cliente para editar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Cliente clienteSelecionado =
                dgvclientes.SelectedRows[0]
                .DataBoundItem as Cliente;

            if (clienteSelecionado == null)
            {
                return;
            }

            indiceClienteEditando =
                clientes.IndexOf(clienteSelecionado);

            if (indiceClienteEditando == -1)
            {
                return;
            }

            tbnomec.Text =
                clienteSelecionado.Nome;

            tbcpfcnpjc.Text =
                clienteSelecionado.CpfCnpj;

            tbtelefonec.Text =
                clienteSelecionado.Telefone;

            cbativo.Checked =
                clienteSelecionado.Status == StatusCadastro.Ativo;

            cbativo.Visible = true;

            modoEdicao = true;
            ldicaedicao.Visible = true;

            bsalvarcliente.Text = "Atualizar cliente";
            beditarc.Enabled = false;

            tbnomec.Focus();
        }
    }
}
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class frmcobranca : Form
    {
        private decimal totalVenda;

        private decimal jaPago;

        private bool aceitaParcial;

        private bool temCliente;

        private bool modoAbatimento;

        public bool PagamentoParcial { get; private set; }

        private BindingList<PagamentoVenda> pagamentos =
            new BindingList<PagamentoVenda>();

        private BindingSource fontePagamentos =
            new BindingSource();

        public BindingList<PagamentoVenda> Pagamentos
        {
            get { return pagamentos; }
        }

        private void AtualizarResumoPagamento()
        {
            decimal totalPago =
                jaPago + pagamentos.Sum(pagamento => pagamento.Valor);

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

        public frmcobranca()
        {
            InitializeComponent();
        }

        public frmcobranca(
            decimal total,
            decimal pagoAnterior,
            bool permiteParcial,
            bool clienteInformado,
            bool abatimento = false) : this()
        {
            totalVenda = total;
            jaPago = pagoAnterior;
            aceitaParcial = permiteParcial;
            temCliente = clienteInformado;
            modoAbatimento = abatimento;

            fontePagamentos.DataSource = pagamentos;
            dgvpagamentosvenda.DataSource = fontePagamentos;

            if (dgvpagamentosvenda.Columns.Count > 0)
            {
                dgvpagamentosvenda.Columns["FormaPagamento"].HeaderText =
                    "Forma de pagamento";

                dgvpagamentosvenda.Columns["Valor"]
                    .DefaultCellStyle.Format = "N2";
            }

            if (modoAbatimento)
            {
                cbformapagamento.Items.Remove("Notinha");

                Text = "Pagamento de Notinha";

                bconfirmarvenda.Text = "Confirmar pagamento";

                ltotalvenda.Text =
                    $"Saldo devido: R$ {totalVenda:N2}";
            }
            else
            {
                ltotalvenda.Text =
                    $"Total da venda: R$ {totalVenda:N2}";
            }

            AtualizarResumoPagamento();
        }

        private void cbformapagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbformapagamento.SelectedIndex == -1)
            {
                tbvalorrecebido.Clear();
                return;
            }

            decimal totalPago =
                jaPago + pagamentos.Sum(pagamento => pagamento.Valor);

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

            if (formaPagamento == "Notinha" && !temCliente)
            {
                MessageBox.Show(
                    "Para usar Notinha, selecione um cliente na venda.",
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

            decimal totalPago =
                jaPago + pagamentos.Sum(pagamento => pagamento.Valor);

            decimal falta =
                totalVenda - totalPago;

            if (falta <= 0)
            {
                MessageBox.Show(
                    modoAbatimento
                    ? "A dívida já está totalmente paga."
                    : "Esta venda já está totalmente paga.",
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
                pagamentos
                .FirstOrDefault(pagamento =>
                    pagamento.FormaPagamento == formaPagamento);

            if (pagamentoExistente != null)
            {
                pagamentoExistente.Valor += valorPagamento;
            }
            else
            {
                pagamentos.Add(
                    new PagamentoVenda
                    {
                        FormaPagamento = formaPagamento,
                        Valor = valorPagamento
                    }
                );
            }

            fontePagamentos.ResetBindings(false);
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

            pagamentos.Remove(pagamentoSelecionado);

            fontePagamentos.ResetBindings(false);
            AtualizarResumoPagamento();
        }

        private void bconfirmarvenda_Click(object sender, EventArgs e)
        {
            decimal totalPago =
                jaPago + pagamentos.Sum(pagamento => pagamento.Valor);

            decimal totalDinheiro =
                pagamentos
                .Where(pagamento => pagamento.FormaPagamento == "Dinheiro")
                .Sum(pagamento => pagamento.Valor);

            decimal troco =
                Math.Max(0m, totalPago - totalVenda);

            if (pagamentos.Count == 0)
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
                if (!aceitaParcial)
                {
                    MessageBox.Show(
                        $"Ainda faltam R$ {totalVenda - totalPago:N2} para cobrir o total da venda.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                DialogResult resposta = MessageBox.Show(
                    modoAbatimento
                    ? $"Ainda restarão R$ {totalVenda - totalPago:N2} de dívida. Registrar este pagamento parcial?"
                    : $"Ainda faltam R$ {totalVenda - totalPago:N2}. Registrar este pagamento parcial e manter a comanda aberta?",
                    "Pagamento parcial",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resposta != DialogResult.Yes)
                {
                    return;
                }

                PagamentoParcial = true;
                DialogResult = DialogResult.OK;
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

            PagamentoParcial = false;
            DialogResult = DialogResult.OK;
        }
    }
}
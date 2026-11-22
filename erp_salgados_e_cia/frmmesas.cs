using System;
using System.Linq;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class frmmesas : Form
    {
        private BindingSource fonteMesas =
            new BindingSource();

        public int MesaSelecionada { get; private set; }

        private void AtualizarTabela()
        {
            fonteMesas.DataSource =
                dadossistema.ComandasDaUnidade()
                .OrderBy(c => c.NumeroMesa)
                .ToList();

            lsemmesas.Visible =
                dadossistema.ComandasDaUnidade().Count == 0;

            if (dgvmesas.Columns.Count > 0)
            {
                dgvmesas.Columns["NumeroMesa"].HeaderText =
                    "Mesa";

                dgvmesas.Columns["IdCliente"].HeaderText =
                    "Cliente";

                dgvmesas.Columns["QuantidadeItens"].HeaderText =
                    "Itens";

                dgvmesas.Columns["TotalPago"].HeaderText =
                    "Pago";

                dgvmesas.Columns["Abertura"].HeaderText =
                    "Abertura";

                if (dgvmesas.Columns.Contains("Itens"))
                {
                    dgvmesas.Columns["Itens"].Visible = false;
                }

                if (dgvmesas.Columns.Contains("Pagamentos"))
                {
                    dgvmesas.Columns["Pagamentos"].Visible = false;
                }

                if (dgvmesas.Columns.Contains("Unidade"))
                {
                    dgvmesas.Columns["Unidade"].Visible = false;
                }

                dgvmesas.Columns["QuantidadeItens"]
                    .DefaultCellStyle.Format = "0.##";

                dgvmesas.Columns["Total"]
                    .DefaultCellStyle.Format = "C2";

                dgvmesas.Columns["TotalPago"]
                    .DefaultCellStyle.Format = "C2";

                dgvmesas.Columns["Falta"]
                    .DefaultCellStyle.Format = "C2";

                dgvmesas.Columns["Abertura"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        public frmmesas()
        {
            InitializeComponent();

            dgvmesas.DataSource = fonteMesas;

            dgvmesas.CellFormatting += dgvmesas_CellFormatting;

            AtualizarTabela();
        }

        private void dgvmesas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                dgvmesas.Columns[e.ColumnIndex].Name != "IdCliente")
            {
                return;
            }

            Comanda comandaLinha =
                dgvmesas.Rows[e.RowIndex].DataBoundItem as Comanda;

            if (comandaLinha == null)
            {
                return;
            }

            Cliente clienteLinha =
                dadossistema.clientes
                .FirstOrDefault(c =>
                    c.IdCliente == comandaLinha.IdCliente);

            e.Value =
                clienteLinha == null
                ? ""
                : clienteLinha.Descricao;

            e.FormattingApplied = true;
        }

        private void dgvmesas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            babrirmesa_Click(sender, EventArgs.Empty);
        }

        private void babrirmesa_Click(object sender, EventArgs e)
        {
            if (dadossistema.ComandasDaUnidade().Count == 0)
            {
                MessageBox.Show(
                    "Nenhuma mesa aberta.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Comanda comandaSelecionada =
                dgvmesas.CurrentRow == null
                ? null
                : dgvmesas.CurrentRow.DataBoundItem as Comanda;

            if (comandaSelecionada == null)
            {
                MessageBox.Show(
                    "Selecione uma mesa.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            MesaSelecionada = comandaSelecionada.NumeroMesa;

            DialogResult = DialogResult.OK;
        }
    }
}
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
    public partial class uchistoricovendas : UserControl
    {
        private BindingList<Venda> vendas =
             dadossistema.vendas;

        private BindingSource fonteVendas =
            new BindingSource();

        public void AtualizarTabela()
        {
            string filtroStatus =
                cbfiltrostatus.SelectedItem == null
                ? "Todas"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Venda> vendasFiltradas = vendas;

            if (filtroStatus == "Concluídas")
            {
                vendasFiltradas =
                    vendas.Where(v => v.Status == "Concluída");
            }
            else if (filtroStatus == "Canceladas")
            {
                vendasFiltradas =
                    vendas.Where(v => v.Status == "Cancelada");
            }

            vendasFiltradas =
                vendasFiltradas.Where(v =>
                    dadossistema.caixas.Any(c =>
                        c.IdCaixa == v.IdCaixa &&
                        c.Unidade == dadossistema.UnidadeAtual));

            fonteVendas.DataSource = vendasFiltradas.ToList();

            if (dgvhistoricovendas.Columns.Count > 0)
            {
                dgvhistoricovendas.Columns["IdVenda"].HeaderText =
                    "Venda";

                dgvhistoricovendas.Columns["NumeroMesa"].HeaderText =
                    "Mesa";

                dgvhistoricovendas.Columns["DataHora"].HeaderText =
                    "Data e hora";

                dgvhistoricovendas.Columns["FormaPagamento"].HeaderText =
                    "Forma de pagamento";

                dgvhistoricovendas.Columns["ValorTotal"].HeaderText =
                    "Total";

                dgvhistoricovendas.Columns["ValorRecebido"].HeaderText =
                    "Valor recebido";

                dgvhistoricovendas.Columns["Troco"].HeaderText =
                    "Troco";

                dgvhistoricovendas.Columns["Status"].HeaderText =
                    "Status";

                dgvhistoricovendas.Columns["IdCliente"].HeaderText =
                    "Cliente";

                dgvhistoricovendas.Columns["Produtos"].HeaderText =
                    "Produtos";

                dgvhistoricovendas.Columns["MotivoCancelamento"].HeaderText =
                    "Motivo do cancelamento";

                dgvhistoricovendas.Columns["CanceladaPor"].HeaderText =
                    "Cancelada por";

                dgvhistoricovendas.Columns["DataCancelamento"].HeaderText =
                    "Cancelada em";

                dgvhistoricovendas.Columns["DataCancelamento"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgvhistoricovendas.Columns["Produtos"]
                    .DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgvhistoricovendas.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.AllCells;

                if (dgvhistoricovendas.Columns.Contains("Itens"))
                {
                    dgvhistoricovendas.Columns["Itens"].Visible = false;
                }

                if (dgvhistoricovendas.Columns.Contains("Pagamentos"))
                {
                    dgvhistoricovendas.Columns["Pagamentos"].Visible = false;
                }

                dgvhistoricovendas.Columns["DataHora"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgvhistoricovendas.Columns["ValorTotal"]
                    .DefaultCellStyle.Format = "C2";

                dgvhistoricovendas.Columns["ValorRecebido"]
                    .DefaultCellStyle.Format = "C2";

                dgvhistoricovendas.Columns["Troco"]
                    .DefaultCellStyle.Format = "C2";
            }
        }

        private void dgvhistoricovendas_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Venda vendaLinha =
                dgvhistoricovendas.Rows[e.RowIndex]
                .DataBoundItem as Venda;

            if (vendaLinha == null)
            {
                return;
            }

            if (vendaLinha.Status == "Cancelada")
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 224, 224);
                e.CellStyle.ForeColor = Color.Firebrick;
            }

            if (dgvhistoricovendas.Columns[e.ColumnIndex].Name == "NumeroMesa" &&
                vendaLinha.NumeroMesa == 0)
            {
                e.Value = "Balcão";
                e.FormattingApplied = true;
                return;
            }

            if (dgvhistoricovendas.Columns[e.ColumnIndex].Name != "IdCliente")
            {
                return;
            }

            Cliente clienteVenda =
                dadossistema.clientes
                .FirstOrDefault(c =>
                    c.IdCliente == vendaLinha.IdCliente);

            e.Value =
                clienteVenda == null
                ? ""
                : clienteVenda.Descricao;

            e.FormattingApplied = true;
        }

        private void bcancelarvenda_Click(object sender, EventArgs e)
        {
            Venda vendaSelecionada =
                dgvhistoricovendas.CurrentRow == null
                ? null
                : dgvhistoricovendas.CurrentRow.DataBoundItem as Venda;

            if (vendaSelecionada == null)
            {
                MessageBox.Show(
                    "Selecione uma venda.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (vendaSelecionada.Status != "Concluída")
            {
                MessageBox.Show(
                    "Somente vendas concluídas podem ser canceladas.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            string autorizadoPor;

            if (dadossistema.LoginEhDeAtendente)
            {
                using (frmlogin autorizacao = new frmlogin(true))
                {
                    if (autorizacao.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    autorizadoPor = autorizacao.LoginAutorizador;
                }
            }
            else
            {
                autorizadoPor = dadossistema.UsuarioLogado.Login;
            }

            using (frmcancelarvenda janela =
                new frmcancelarvenda(vendaSelecionada, autorizadoPor))
            {
                if (janela.ShowDialog() == DialogResult.OK)
                {
                    AtualizarTabela();
                }
            }
        }

        private void cbfiltrostatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AtualizarTabela();
        }

        public uchistoricovendas()
        {
            InitializeComponent();

            cbfiltrostatus.Items.AddRange(
                new object[]
                {
                    "Todas",
                    "Concluídas",
                    "Canceladas"
                }
            );

            cbfiltrostatus.SelectedIndex = 0;

            fonteVendas.DataSource = vendas;
            dgvhistoricovendas.DataSource = fonteVendas;

            dgvhistoricovendas.CellFormatting +=
                dgvhistoricovendas_CellFormatting;

            bcancelarvenda.Click +=
                bcancelarvenda_Click;

            cbfiltrostatus.SelectedIndexChanged +=
                cbfiltrostatus_SelectedIndexChanged;

            AtualizarTabela();
        }
    }
}

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
    public partial class ucmovimentoestoque : UserControl
    {
        private List<movimentoestoque> movimentacoes =
            new List<movimentoestoque>();

        private void AtualizarTabela()
        {
            dgvmovimentacoes.DataSource = null;
            dgvmovimentacoes.DataSource = movimentacoes;

            if (dgvmovimentacoes.Columns.Count > 0)
            {
                dgvmovimentacoes.Columns["Produto"].HeaderText = "Produto";
                dgvmovimentacoes.Columns["Quantidade"].HeaderText = "Quantidade";
                dgvmovimentacoes.Columns["CustoUnitario"].HeaderText = "Custo unitário";
                dgvmovimentacoes.Columns["Fornecedor"].HeaderText = "Fornecedor";
                dgvmovimentacoes.Columns["Responsavel"].HeaderText = "Responsável";
                dgvmovimentacoes.Columns["Unidade"].HeaderText = "Unidade";
                dgvmovimentacoes.Columns["DataEntrada"].HeaderText = "Data da entrada";

                dgvmovimentacoes.Columns["Quantidade"]
                    .DefaultCellStyle.Format = "N2";

                dgvmovimentacoes.Columns["CustoUnitario"]
                    .DefaultCellStyle.Format = "N2";

                dgvmovimentacoes.Columns["DataEntrada"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }
        public ucmovimentoestoque()
        {
            InitializeComponent();

            cbproduto.Items.AddRange(
    new object[]
    {
        "Coxinha",
        "Risole",
        "Bolinha de queijo"
    }
);

            cbfornecedor.Items.AddRange(
                new object[]
                {
        "Fornecedor principal",
        "Distribuidora de alimentos"
                }
            );

            cbresponsavel.Items.AddRange(
                new object[]
                {
        "Administrador",
        "Gerente",
        "Atendente"
                }
            );
        }

        private void bregistrar_Click(object sender, EventArgs e)
        {
            decimal custoUnitario;

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

            if (!decimal.TryParse(
                    tbcustouni.Text,
                    out custoUnitario)
                || custoUnitario <= 0)
            {
                MessageBox.Show(
                    "Informe um custo unitário válido.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcustouni.Focus();
                return;
            }

            if (cbfornecedor.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione um fornecedor.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbfornecedor.Focus();
                return;
            }

            if (cbresponsavel.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o responsável.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbresponsavel.Focus();
                return;
            }

            movimentoestoque movimento = new movimentoestoque
            {
                Produto = cbproduto.Text,
                Quantidade = nudquantidade.Value,
                CustoUnitario = custoUnitario,
                Fornecedor = cbfornecedor.Text,
                Responsavel = cbresponsavel.Text,
                Unidade = "Lanchonete Centro",
                DataEntrada = DateTime.Now
            };

            movimentacoes.Add(movimento);

            AtualizarTabela();

            MessageBox.Show(
                "Entrada de estoque registrada com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            cbproduto.SelectedIndex = -1;
            nudquantidade.Value = 1;
            tbcustouni.Clear();
            cbfornecedor.SelectedIndex = -1;
            cbresponsavel.SelectedIndex = -1;
            cbproduto.Focus();
        }
    }
}

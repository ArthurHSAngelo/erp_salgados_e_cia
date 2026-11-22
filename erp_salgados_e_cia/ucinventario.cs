using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class ucinventario : UserControl
    {
        private List<ItemInventario> itens =
            new List<ItemInventario>();

        private BindingSource fonteInventario =
            new BindingSource();

        private Dictionary<string, Tuple<decimal, decimal>> avisosSaldo =
            new Dictionary<string, Tuple<decimal, decimal>>();

        private void ConfigurarColunas()
        {
            if (dgvinventario.Columns.Count == 0)
            {
                return;
            }

            dgvinventario.Columns["Produto"].HeaderText =
                "Produto";

            dgvinventario.Columns["SaldoSistema"].HeaderText =
                "Saldo do sistema";

            dgvinventario.Columns["QuantidadeContada"].HeaderText =
                "Contada";

            dgvinventario.Columns["Diferenca"].HeaderText =
                "Diferença";

            dgvinventario.Columns["Motivo"].HeaderText =
                "Motivo";

            dgvinventario.Columns["Produto"].ReadOnly = true;
            dgvinventario.Columns["SaldoSistema"].ReadOnly = true;
            dgvinventario.Columns["Diferenca"].ReadOnly = true;

            dgvinventario.Columns["SaldoSistema"]
                .DefaultCellStyle.Format = "N2";

            dgvinventario.Columns["QuantidadeContada"]
                .DefaultCellStyle.Format = "N2";

            dgvinventario.Columns["Diferenca"]
                .DefaultCellStyle.Format = "N2";
        }

        private void AtualizarResumo()
        {
            int contados =
                itens.Count(i => i.QuantidadeContada.HasValue);

            int divergencias =
                itens.Count(i =>
                    i.Diferenca.HasValue &&
                    i.Diferenca.Value != 0);

            lcontadosinventario.Text =
                $"Contados: {contados}";

            ldivergenciasinventario.Text =
                $"Divergências: {divergencias}";
        }

        private void AtualizarAviso()
        {
            lavisoinventario.ForeColor = Color.Firebrick;

            if (avisosSaldo.Count == 0)
            {
                lavisoinventario.Text = "";
                lavisoinventario.Visible = false;
                return;
            }

            lavisoinventario.Text =
                "Atenção: o saldo mudou desde que você começou a contar: " +
                string.Join(
                    "; ",
                    avisosSaldo.Select(a =>
                        $"{a.Key} ({a.Value.Item1:N2} → {a.Value.Item2:N2})")) +
                ".";

            lavisoinventario.Visible = true;
        }

        private void AtualizarSaldos()
        {
            List<ItemInventario> novos =
                dadossistema.MontarItensInventario();

            foreach (ItemInventario novo in novos)
            {
                ItemInventario antigo =
                    itens.FirstOrDefault(i => i.Produto == novo.Produto);

                if (antigo == null)
                {
                    continue;
                }

                novo.QuantidadeContada = antigo.QuantidadeContada;
                novo.Motivo = antigo.Motivo;

                if (antigo.QuantidadeContada.HasValue &&
                    antigo.SaldoSistema != novo.SaldoSistema)
                {
                    decimal saldoOriginal =
                        avisosSaldo.ContainsKey(novo.Produto)
                        ? avisosSaldo[novo.Produto].Item1
                        : antigo.SaldoSistema;

                    avisosSaldo[novo.Produto] =
                        Tuple.Create(saldoOriginal, novo.SaldoSistema);
                }
            }

            itens = novos;

            fonteInventario.DataSource = itens;

            ConfigurarColunas();

            AtualizarAviso();

            AtualizarResumo();
        }

        private void CarregarResponsaveis()
        {
            string responsavelSelecionado =
                cbresponsavelinventario.SelectedItem as string;

            cbresponsavelinventario.Items.Clear();

            foreach (string nome in
                dadossistema.NomesFuncionariosAtivos(
                    dadossistema.UnidadeAtual))
            {
                cbresponsavelinventario.Items.Add(nome);
            }

            if (responsavelSelecionado != null &&
                cbresponsavelinventario.Items.Contains(responsavelSelecionado))
            {
                cbresponsavelinventario.SelectedItem = responsavelSelecionado;
            }
            else
            {
                cbresponsavelinventario.SelectedIndex = -1;
            }
        }

        private void ReiniciarContagem()
        {
            CarregarResponsaveis();

            cbresponsavelinventario.SelectedIndex = -1;

            avisosSaldo.Clear();

            itens = dadossistema.MontarItensInventario();

            fonteInventario.DataSource = itens;

            ConfigurarColunas();

            tbobservacaoinventario.Clear();

            AtualizarAviso();

            AtualizarResumo();
        }

        public void AtualizarTela()
        {
            CarregarResponsaveis();

            AtualizarSaldos();
        }

        public ucinventario()
        {
            InitializeComponent();

            dgvinventario.DataSource =
                fonteInventario;

            dgvinventario.CellValidating +=
                dgvinventario_CellValidating;

            dgvinventario.CellEndEdit +=
                dgvinventario_CellEndEdit;

            dgvinventario.DataError +=
                dgvinventario_DataError;

            dgvinventario.CellFormatting +=
                dgvinventario_CellFormatting;

            befetivarinventario.Click +=
                befetivarinventario_Click;

            blimparinventario.Click +=
                blimparinventario_Click;

            ReiniciarContagem();
        }

        private void dgvinventario_CellValidating(
            object sender,
            DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                dgvinventario.Columns[e.ColumnIndex].Name !=
                    "QuantidadeContada")
            {
                return;
            }

            string texto =
                (e.FormattedValue == null
                    ? ""
                    : e.FormattedValue.ToString()).Trim();

            if (texto.Length == 0)
            {
                return;
            }

            decimal valor;

            if (!decimal.TryParse(
                    texto,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out valor) ||
                valor < 0)
            {
                MessageBox.Show(
                    "Informe uma quantidade válida (zero ou maior).",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                e.Cancel = true;
            }
        }

        private void dgvinventario_CellEndEdit(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                fonteInventario.ResetItem(e.RowIndex);
            }

            AtualizarResumo();
        }

        private void dgvinventario_DataError(
            object sender,
            DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void dgvinventario_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                dgvinventario.Columns[e.ColumnIndex].Name != "Diferenca")
            {
                return;
            }

            decimal? diferenca = e.Value as decimal?;

            if (diferenca.HasValue && diferenca.Value < 0)
            {
                e.CellStyle.ForeColor = Color.Firebrick;
            }
        }

        private void befetivarinventario_Click(object sender, EventArgs e)
        {
            dgvinventario.EndEdit();

            int contados =
                itens.Count(i => i.QuantidadeContada.HasValue);

            if (contados == 0)
            {
                MessageBox.Show(
                    "Informe a contagem de pelo menos um produto.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            ItemInventario semMotivo =
                itens.FirstOrDefault(i =>
                    i.Diferenca.HasValue &&
                    i.Diferenca.Value != 0 &&
                    string.IsNullOrWhiteSpace(i.Motivo));

            if (semMotivo != null)
            {
                MessageBox.Show(
                    $"Informe o motivo da divergência de {semMotivo.Produto}.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cbresponsavelinventario.Items.Count == 0)
            {
                MessageBox.Show(
                    "Esta unidade não tem funcionário ativo. Cadastre um em Cadastros, Funcionários.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cbresponsavelinventario.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o responsável.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbresponsavelinventario.Focus();
                return;
            }

            AtualizarSaldos();

            if (avisosSaldo.Count > 0)
            {
                DialogResult continuar = MessageBox.Show(
                    lavisoinventario.Text +
                    Environment.NewLine + Environment.NewLine +
                    "Efetivar mesmo assim? A diferença será recalculada com o saldo atual.",
                    "Saldo alterado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (continuar != DialogResult.Yes)
                {
                    return;
                }
            }

            int divergencias =
                itens.Count(i =>
                    i.Diferenca.HasValue &&
                    i.Diferenca.Value != 0);

            DialogResult resposta = MessageBox.Show(
                $"Efetivar o inventário? {contados} produto(s) contado(s) e {divergencias} ajuste(s). Esta ação não pode ser desfeita.",
                "Confirmar inventário",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            int ajustes;
            string mensagem;

            if (!dadossistema.EfetivarInventario(
                    itens,
                    tbobservacaoinventario.Text,
                    out ajustes,
                    out mensagem,
                    cbresponsavelinventario.SelectedItem.ToString()))
            {
                MessageBox.Show(
                    mensagem,
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            MessageBox.Show(
                ajustes == 0
                ? "Inventário registrado. Nenhuma divergência encontrada."
                : $"Inventário registrado com sucesso. {ajustes} ajuste(s) realizado(s).",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ReiniciarContagem();
        }

        private void blimparinventario_Click(object sender, EventArgs e)
        {
            dgvinventario.EndEdit();

            if (itens.Any(i => i.QuantidadeContada.HasValue))
            {
                DialogResult resposta = MessageBox.Show(
                    "Deseja descartar as contagens digitadas?",
                    "Limpar contagem",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resposta != DialogResult.Yes)
                {
                    return;
                }
            }

            ReiniciarContagem();
        }
    }
}

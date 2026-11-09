using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class ucmovimentacoes : UserControl
    {
        private BindingSource fonteMovimentacoes =
            new BindingSource();

        private CultureInfo culturaBrasil =
            new CultureInfo("pt-BR");

        private List<HistoricoEstoque> ultimaConsulta =
            new List<HistoricoEstoque>();

        private bool carregandoFiltros = false;

        private string periodoAplicado = "-";
        private string unidadeAplicada = "Todas";
        private string produtoAplicado = "Todos";
        private string tipoAplicado = "Todos";

        private void CarregarFiltros()
        {
            carregandoFiltros = true;

            string unidadeSelecionada =
                cbfiltrounidade.SelectedItem as string;

            string produtoSelecionado =
                cbfiltroproduto.SelectedItem as string;

            cbfiltrounidade.Items.Clear();
            cbfiltrounidade.Items.Add("Todas");

            foreach (Unidade unidade in
                dadossistema.unidades.OrderBy(u => u.Nome))
            {
                cbfiltrounidade.Items.Add(unidade.Nome);
            }

            if (unidadeSelecionada != null &&
                cbfiltrounidade.Items.Contains(unidadeSelecionada))
            {
                cbfiltrounidade.SelectedItem = unidadeSelecionada;
            }
            else
            {
                cbfiltrounidade.SelectedIndex = 0;
            }

            cbfiltroproduto.Items.Clear();
            cbfiltroproduto.Items.Add("Todos");

            foreach (Produto produto in
                dadossistema.produtos.OrderBy(p => p.Nome))
            {
                cbfiltroproduto.Items.Add(produto.Nome);
            }

            if (produtoSelecionado != null &&
                cbfiltroproduto.Items.Contains(produtoSelecionado))
            {
                cbfiltroproduto.SelectedItem = produtoSelecionado;
            }
            else
            {
                cbfiltroproduto.SelectedIndex = 0;
            }

            carregandoFiltros = false;
        }

        private void ConfigurarColunas()
        {
            if (dgvmovimentacoes.Columns.Count == 0)
            {
                return;
            }

            dgvmovimentacoes.Columns["Data"].HeaderText =
                "Data e hora";

            dgvmovimentacoes.Columns["Produto"].HeaderText =
                "Produto";

            dgvmovimentacoes.Columns["Tipo"].HeaderText =
                "Tipo";

            dgvmovimentacoes.Columns["Quantidade"].HeaderText =
                "Quantidade";

            dgvmovimentacoes.Columns["SaldoApos"].HeaderText =
                "Saldo após";

            dgvmovimentacoes.Columns["Unidade"].HeaderText =
                "Unidade";

            dgvmovimentacoes.Columns["Responsavel"].HeaderText =
                "Responsável";

            dgvmovimentacoes.Columns["Origem"].HeaderText =
                "Origem";

            dgvmovimentacoes.Columns["Data"]
                .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvmovimentacoes.Columns["Quantidade"]
                .DefaultCellStyle.Format = "N2";

            dgvmovimentacoes.Columns["Quantidade"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgvmovimentacoes.Columns["SaldoApos"]
                .DefaultCellStyle.Format = "N2";

            dgvmovimentacoes.Columns["SaldoApos"]
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
        }

        private void GerarRelatorio()
        {
            DateTime? inicio =
                dtpfiltroinicio.Checked
                ? (DateTime?)dtpfiltroinicio.Value.Date
                : null;

            DateTime? fim =
                dtpfiltrofim.Checked
                ? (DateTime?)dtpfiltrofim.Value.Date
                : null;

            DateTime dataInicial;
            DateTime dataFinal;

            dadossistema.DefinirPeriodoPadrao(
                inicio,
                fim,
                out dataInicial,
                out dataFinal
            );

            if (dataInicial > dataFinal)
            {
                ultimaConsulta = new List<HistoricoEstoque>();

                fonteMovimentacoes.DataSource =
                    ultimaConsulta;

                ConfigurarColunas();

                lperiodoaplicado.Text =
                    "Período: -";

                lresultadomovimentacoes.Text =
                    "A data final não pode ser anterior à data inicial.";


                return;
            }

            string unidadeFiltro =
                cbfiltrounidade.SelectedItem == null
                ? "Todas"
                : cbfiltrounidade.SelectedItem.ToString();

            string produtoFiltro =
                cbfiltroproduto.SelectedItem == null
                ? "Todos"
                : cbfiltroproduto.SelectedItem.ToString();

            string tipoFiltro =
                cbfiltrotipo.SelectedItem == null
                ? "Todos"
                : cbfiltrotipo.SelectedItem.ToString();

            TipoMovimentoEstoque? tipoConsulta = null;

            if (tipoFiltro != "Todos")
            {
                tipoConsulta =
                    (TipoMovimentoEstoque)Enum.Parse(
                        typeof(TipoMovimentoEstoque),
                        tipoFiltro);
            }

            List<HistoricoEstoque> resultadoConsulta =
                dadossistema.ConsultarHistoricoEstoque(
                    inicio,
                    fim,
                    unidadeFiltro == "Todas" ? null : unidadeFiltro,
                    produtoFiltro == "Todos" ? null : produtoFiltro,
                    tipoConsulta
                );

            periodoAplicado =
                $"{dataInicial:dd/MM/yyyy} a {dataFinal:dd/MM/yyyy}";

            if (inicio == null && fim == null)
            {
                periodoAplicado +=
                    " (padrão: últimos 30 dias)";
            }

            unidadeAplicada = unidadeFiltro;
            produtoAplicado = produtoFiltro;
            tipoAplicado = tipoFiltro;

            ultimaConsulta = resultadoConsulta;

            fonteMovimentacoes.DataSource =
                resultadoConsulta;

            ConfigurarColunas();

            lperiodoaplicado.Text =
                $"Período: {periodoAplicado}";

            lresultadomovimentacoes.Text =
                resultadoConsulta.Count == 0
                ? "Não há registros para os filtros informados."
                : $"{resultadoConsulta.Count} registro(s) encontrado(s).";
        }

        public void AtualizarTela()
        {
            CarregarFiltros();

            GerarRelatorio();
        }

        public ucmovimentacoes()
        {
            InitializeComponent();

            fonteMovimentacoes.DataSource =
                new List<HistoricoEstoque>();

            dgvmovimentacoes.DataSource =
                fonteMovimentacoes;

            cbfiltrotipo.Items.AddRange(
                new object[]
                {
                    "Todos",
                    "Entrada",
                    "Venda",
                    "Estorno",
                    "Perda",
                    "Ajuste"
                }
            );

            cbfiltrotipo.SelectedIndex =
                0;

            dtpfiltroinicio.Value =
                DateTime.Today.AddDays(-30);

            dtpfiltroinicio.Checked =
                true;

            CarregarFiltros();

            bexportarmovimentacoes.Click +=
                bexportarmovimentacoes_Click;

            dtpfiltroinicio.ValueChanged +=
                FiltroAlterado;

            dtpfiltrofim.ValueChanged +=
                FiltroAlterado;

            cbfiltrounidade.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltroproduto.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltrotipo.SelectedIndexChanged +=
                FiltroAlterado;

            GerarRelatorio();
        }

        private void FiltroAlterado(object sender, EventArgs e)
        {
            if (carregandoFiltros)
            {
                return;
            }

            GerarRelatorio();
        }


        private void bexportarmovimentacoes_Click(object sender, EventArgs e)
        {
            if (ultimaConsulta.Count == 0)
            {
                MessageBox.Show(
                    "Não há registros para exportar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            using (SaveFileDialog janela = new SaveFileDialog())
            {
                janela.Filter = "Arquivo CSV (*.csv)|*.csv";

                janela.FileName =
                    $"movimentacoes_estoque_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (janela.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                StringBuilder csv = new StringBuilder();

                csv.AppendLine(
                    "Relatório de estoque - Movimentações");

                csv.AppendLine(
                    $"Gerado em;{DateTime.Now:dd/MM/yyyy HH:mm}");

                csv.AppendLine("Filtros aplicados");

                csv.AppendLine(
                    "Período;" + dadossistema.CampoCsv(periodoAplicado));

                csv.AppendLine(
                    "Unidade;" + dadossistema.CampoCsv(unidadeAplicada));

                csv.AppendLine(
                    "Produto;" + dadossistema.CampoCsv(produtoAplicado));

                csv.AppendLine(
                    "Tipo;" + tipoAplicado);

                csv.AppendLine();

                csv.AppendLine(
                    "Data e hora;Produto;Tipo;Quantidade;Saldo após;" +
                    "Unidade;Responsável;Origem");

                foreach (HistoricoEstoque registro in ultimaConsulta)
                {
                    csv.AppendLine(
                        string.Join(
                            ";",
                            registro.Data.ToString("dd/MM/yyyy HH:mm"),
                            dadossistema.CampoCsv(registro.Produto),
                            registro.Tipo.ToString(),
                            registro.Quantidade.ToString("N2", culturaBrasil),
                            registro.SaldoApos.ToString("N2", culturaBrasil),
                            dadossistema.CampoCsv(registro.Unidade),
                            dadossistema.CampoCsv(registro.Responsavel),
                            dadossistema.CampoCsv(registro.Origem)
                        )
                    );
                }

                csv.AppendLine();

                csv.AppendLine(
                    $"Total de registros;{ultimaConsulta.Count}");

                File.WriteAllText(
                    janela.FileName,
                    csv.ToString(),
                    new UTF8Encoding(true));

                MessageBox.Show(
                    "Relatório exportado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}
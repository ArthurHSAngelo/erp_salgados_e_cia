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
using System.Windows.Forms.DataVisualization.Charting;

namespace erp_salgados_e_cia
{
    public partial class ucrelatoriovendas : UserControl
    {
        private CultureInfo culturaBrasil =
            new CultureInfo("pt-BR");

        private bool carregandoFiltros = false;

        private RelatorioVendas ultimoRelatorio =
            new RelatorioVendas();

        private string periodoAplicado = "-";
        private string unidadeAplicada = "Todas";
        private string formaAplicada = "Todas";
        private string produtoAplicado = "Todos";

        private Series serieDia;
        private Series serieProdutos;
        private Series serieFormas;

        private void CarregarFiltros()
        {
            carregandoFiltros = true;

            string unidadeSelecionada =
                cbfiltrounidade.SelectedItem as string;

            string formaSelecionada =
                cbfiltroformapagamento.SelectedItem as string;

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

            cbfiltroformapagamento.Items.Clear();
            cbfiltroformapagamento.Items.Add("Todas");

            foreach (string forma in dadossistema.FormasPagamento)
            {
                cbfiltroformapagamento.Items.Add(forma);
            }

            if (formaSelecionada != null &&
                cbfiltroformapagamento.Items.Contains(formaSelecionada))
            {
                cbfiltroformapagamento.SelectedItem = formaSelecionada;
            }
            else
            {
                cbfiltroformapagamento.SelectedIndex = 0;
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

        private void ConfigurarGrade(DataGridView grade)
        {
            grade.AllowUserToAddRows = false;
            grade.AllowUserToDeleteRows = false;
            grade.ReadOnly = true;
            grade.MultiSelect = false;
            grade.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigurarGraficos()
        {
            chartdia.Series.Clear();

            serieDia = new Series("Faturamento");
            serieDia.ChartType = SeriesChartType.Column;
            chartdia.Series.Add(serieDia);

            chartprodutos.Series.Clear();

            serieProdutos = new Series("Quantidade");
            serieProdutos.ChartType = SeriesChartType.Bar;
            chartprodutos.Series.Add(serieProdutos);

            serieFormas = chartformas.Series[0];
        }

        private bool semDadosDia;

        private bool semDadosProdutos;

        private bool semDadosFormas;

        private void AtualizarGraficos()
        {
            serieDia.Points.Clear();

            bool semVendas =
                ultimoRelatorio.Resumo.QuantidadeVendas == 0;

            if (!semVendas)
            {
                foreach (LinhaFaturamentoDia linha in ultimoRelatorio.PorDia)
                {
                    int indice = serieDia.Points.AddXY(
                        linha.Dia.ToString("dd/MM"),
                        (double)linha.Faturamento);

                    serieDia.Points[indice].ToolTip =
                        $"{linha.Dia:dd/MM/yyyy}\n" +
                        $"Faturamento: R$ {linha.Faturamento:N2}\n" +
                        $"Vendas: {linha.QuantidadeVendas}";
                }
            }

            semDadosDia = serieDia.Points.Count == 0;

            serieProdutos.Points.Clear();

            foreach (LinhaProdutoVendido linha in
                ultimoRelatorio.Produtos.Take(10).Reverse())
            {
                int indice = serieProdutos.Points.AddXY(
                    linha.Produto,
                    (double)linha.Quantidade);

                serieProdutos.Points[indice].Label =
                    linha.Quantidade.ToString("0.##");

                serieProdutos.Points[indice].ToolTip =
                    $"{linha.Produto}\n" +
                    $"Quantidade: {linha.Quantidade:0.##}\n" +
                    $"Receita: R$ {linha.Receita:N2}";
            }

            semDadosProdutos = serieProdutos.Points.Count == 0;

            serieFormas.Points.Clear();

            decimal totalFormas =
                ultimoRelatorio.Formas
                .Where(f => f.Valor > 0)
                .Sum(f => f.Valor);

            for (int i = 0; i < ultimoRelatorio.Formas.Count; i++)
            {
                LinhaFormaPagamento linha =
                    ultimoRelatorio.Formas[i];

                if (linha.Valor <= 0)
                {
                    continue;
                }

                int indice = serieFormas.Points.AddXY(
                    linha.Forma,
                    (double)linha.Valor);

                DataPoint ponto = serieFormas.Points[indice];

                decimal percentual =
                    totalFormas == 0
                    ? 0
                    : linha.Valor / totalFormas;

                ponto.Label =
                    percentual >= 0.08m
                    ? linha.Forma
                    : " ";

                ponto.Font =
                    new Font("Segoe UI", 8.5f, FontStyle.Regular);

                ponto.LegendText =
                    $"{linha.Forma}: R$ {linha.Valor:N2}";

                ponto.ToolTip =
                    $"{linha.Forma}\n" +
                    $"R$ {linha.Valor:N2} " +
                    $"({linha.Valor / totalFormas:P0})";
            }

            semDadosFormas = serieFormas.Points.Count == 0;
        }

        private void AtualizarGrades()
        {
            dgvdia.DataSource = null;
            dgvdia.DataSource =
                ultimoRelatorio.PorDia
                .Where(linha => linha.QuantidadeVendas > 0)
                .ToList();

            if (dgvdia.Columns.Count > 0)
            {
                dgvdia.Columns["Dia"].HeaderText = "Dia";
                dgvdia.Columns["QuantidadeVendas"].HeaderText = "Vendas";
                dgvdia.Columns["Faturamento"].HeaderText = "Faturamento";

                dgvdia.Columns["Dia"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvdia.Columns["Faturamento"]
                    .DefaultCellStyle.Format = "C2";
            }

            dgvprodutos.DataSource = null;
            dgvprodutos.DataSource = ultimoRelatorio.Produtos;

            if (dgvprodutos.Columns.Count > 0)
            {
                dgvprodutos.Columns["Produto"].HeaderText = "Produto";
                dgvprodutos.Columns["Quantidade"].HeaderText = "Quantidade";
                dgvprodutos.Columns["Receita"].HeaderText = "Receita";

                dgvprodutos.Columns["Quantidade"]
                    .DefaultCellStyle.Format = "N2";

                dgvprodutos.Columns["Receita"]
                    .DefaultCellStyle.Format = "C2";
            }

            dgvformas.DataSource = null;
            dgvformas.DataSource = ultimoRelatorio.Formas;

            if (dgvformas.Columns.Count > 0)
            {
                dgvformas.Columns["Forma"].HeaderText = "Forma de pagamento";
                dgvformas.Columns["Valor"].HeaderText = "Valor";

                dgvformas.Columns["Valor"]
                    .DefaultCellStyle.Format = "C2";
            }
        }

        private void AtualizarVisualizacoes()
        {
            bool graficoDia = cbvisualizacaodia.SelectedIndex != 1;
            chartdia.Visible = graficoDia && !semDadosDia;
            lsemdadosprodutos.Visible = graficoDia && semDadosDia;
            dgvdia.Visible = !graficoDia;

            bool graficoProdutos = cbvisualizacaoprodutos.SelectedIndex != 1;
            chartprodutos.Visible = graficoProdutos && !semDadosProdutos;
            lsemdadosproduto.Visible = graficoProdutos && semDadosProdutos;
            dgvprodutos.Visible = !graficoProdutos;

            bool graficoFormas = cbvisualizacaoformas.SelectedIndex != 1;
            chartformas.Visible = graficoFormas && !semDadosFormas;
            lsemdadosformas.Visible = graficoFormas && semDadosFormas;
            dgvformas.Visible = !graficoFormas;
        }

        private void AtualizarResumo()
        {
            ResumoVendas resumo = ultimoRelatorio.Resumo;

            lresumovendas.Text =
                $"Vendas: {resumo.QuantidadeVendas}";

            lresumofaturamento.Text =
                $"Faturamento: R$ {resumo.Faturamento:N2}";

            lresumorecebido.Text =
                $"Recebido: R$ {resumo.Recebido:N2}";

            lresumonotinha.Text =
                $"Em Notinha: R$ {resumo.EmNotinha:N2}";

            lresumoticket.Text =
                $"Ticket médio: R$ {resumo.TicketMedio:N2}";
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
                ultimoRelatorio = new RelatorioVendas();

                AtualizarResumo();
                AtualizarGraficos();
                AtualizarGrades();
                AtualizarVisualizacoes();

                lperiodoaplicado.Text =
                    "Período: -";

                lresultadovendas.Text =
                    "A data final não pode ser anterior à data inicial.";

                return;
            }

            string unidadeFiltro =
                cbfiltrounidade.SelectedItem == null
                ? "Todas"
                : cbfiltrounidade.SelectedItem.ToString();

            string formaFiltro =
                cbfiltroformapagamento.SelectedItem == null
                ? "Todas"
                : cbfiltroformapagamento.SelectedItem.ToString();

            string produtoFiltro =
                cbfiltroproduto.SelectedItem == null
                ? "Todos"
                : cbfiltroproduto.SelectedItem.ToString();

            ultimoRelatorio =
                dadossistema.GerarRelatorioVendas(
                    inicio,
                    fim,
                    unidadeFiltro == "Todas" ? null : unidadeFiltro,
                    formaFiltro == "Todas" ? null : formaFiltro,
                    produtoFiltro == "Todos" ? null : produtoFiltro
                );

            periodoAplicado =
                $"{dataInicial:dd/MM/yyyy} a {dataFinal:dd/MM/yyyy}";

            if (inicio == null && fim == null)
            {
                periodoAplicado +=
                    " (padrão: últimos 30 dias)";
            }

            unidadeAplicada = unidadeFiltro;
            formaAplicada = formaFiltro;
            produtoAplicado = produtoFiltro;

            AtualizarResumo();
            AtualizarGraficos();
            AtualizarGrades();
            AtualizarVisualizacoes();

            lperiodoaplicado.Text =
                $"Período: {periodoAplicado}";

            lresultadovendas.Text =
                ultimoRelatorio.Resumo.QuantidadeVendas == 0
                ? "Não há vendas para os filtros informados."
                : $"{ultimoRelatorio.Resumo.QuantidadeVendas} venda(s) encontrada(s).";
        }

        public void AtualizarTela()
        {
            CarregarFiltros();

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

        private void VisualizacaoAlterada(object sender, EventArgs e)
        {
            AtualizarVisualizacoes();
        }

        private List<string> FiltrosAplicados()
        {
            return new List<string>
            {
                "Período: " + periodoAplicado,
                "Unidade: " + unidadeAplicada,
                "Forma de pagamento: " + formaAplicada,
                "Produto: " + produtoAplicado
            };
        }

        private void bexportarvenda_Click(object sender, EventArgs e)
        {
            if (ultimoRelatorio.Resumo.QuantidadeVendas == 0)
            {
                MessageBox.Show(
                    "Não há vendas para exportar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            bool pdf;

            string caminho =
                ExportacaoRelatorio.PedirArquivo(
                    "relatorio_vendas",
                    out pdf);

            if (caminho == null)
            {
                return;
            }

            try
            {
                if (pdf)
                {
                    GerarPdfVendas(caminho);
                }
                else
                {
                    GerarCsvVendas(caminho);
                }
            }
            catch (IOException)
            {
                ExportacaoRelatorio.AvisarFalhaAoSalvar();
                return;
            }
            catch (UnauthorizedAccessException)
            {
                ExportacaoRelatorio.AvisarFalhaAoSalvar();
                return;
            }

            ExportacaoRelatorio.Concluir(caminho);
        }

        private string Moeda(decimal valor)
        {
            return "R$ " + valor.ToString("N2", culturaBrasil);
        }

        private void GerarPdfVendas(string caminho)
        {
            ResumoVendas resumo = ultimoRelatorio.Resumo;

            RelatorioPdf relatorio = new RelatorioPdf(
                "Relatório de vendas",
                false,
                FiltrosAplicados());

            relatorio.AdicionarResumo(
                "Resumo",
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(
                        "Vendas",
                        resumo.QuantidadeVendas.ToString()),
                    new KeyValuePair<string, string>(
                        "Faturamento",
                        Moeda(resumo.Faturamento)),
                    new KeyValuePair<string, string>(
                        "Recebido",
                        Moeda(resumo.Recebido)),
                    new KeyValuePair<string, string>(
                        "Em Notinha",
                        Moeda(resumo.EmNotinha)),
                    new KeyValuePair<string, string>(
                        "Ticket médio",
                        Moeda(resumo.TicketMedio))
                });

            List<string[]> linhasFormas = new List<string[]>();

            foreach (LinhaFormaPagamento linha in ultimoRelatorio.Formas)
            {
                linhasFormas.Add(
                    new string[]
                    {
                        linha.Forma,
                        linha.Valor.ToString("N2", culturaBrasil)
                    }
                );
            }

            relatorio.AdicionarTabela(
                "Formas de pagamento",
                new List<ColunaPdf>
                {
                    new ColunaPdf("Forma de pagamento", 4.0),
                    new ColunaPdf("Valor (R$)", 2.0, true)
                },
                linhasFormas,
                new string[]
                {
                    "Total",
                    ultimoRelatorio.Formas.Sum(f => f.Valor)
                        .ToString("N2", culturaBrasil)
                });

            List<string[]> linhasProdutos = new List<string[]>();

            foreach (LinhaProdutoVendido linha in ultimoRelatorio.Produtos)
            {
                linhasProdutos.Add(
                    new string[]
                    {
                        linha.Produto,
                        linha.Quantidade.ToString("N2", culturaBrasil),
                        linha.Receita.ToString("N2", culturaBrasil)
                    }
                );
            }

            relatorio.AdicionarTabela(
                "Produtos vendidos",
                new List<ColunaPdf>
                {
                    new ColunaPdf("Produto", 4.0),
                    new ColunaPdf("Quantidade", 1.6, true),
                    new ColunaPdf("Receita (R$)", 1.8, true)
                },
                linhasProdutos,
                new string[]
                {
                    "Total",
                    ultimoRelatorio.Produtos.Sum(p => p.Quantidade)
                        .ToString("N2", culturaBrasil),
                    ultimoRelatorio.Produtos.Sum(p => p.Receita)
                        .ToString("N2", culturaBrasil)
                });

            List<string[]> linhasDias = new List<string[]>();

            foreach (LinhaFaturamentoDia linha in
                ultimoRelatorio.PorDia.Where(d => d.QuantidadeVendas > 0))
            {
                linhasDias.Add(
                    new string[]
                    {
                        linha.Dia.ToString("dd/MM/yyyy"),
                        linha.QuantidadeVendas.ToString(),
                        linha.Faturamento.ToString("N2", culturaBrasil)
                    }
                );
            }

            relatorio.AdicionarTabela(
                "Faturamento por dia",
                new List<ColunaPdf>
                {
                    new ColunaPdf("Dia", 2.2),
                    new ColunaPdf("Vendas", 1.4, true),
                    new ColunaPdf("Faturamento (R$)", 2.0, true)
                },
                linhasDias,
                new string[]
                {
                    "Total",
                    ultimoRelatorio.PorDia.Sum(d => d.QuantidadeVendas)
                        .ToString(),
                    ultimoRelatorio.PorDia.Sum(d => d.Faturamento)
                        .ToString("N2", culturaBrasil)
                });

            relatorio.Salvar(caminho);
        }

        private void GerarCsvVendas(string caminho)
        {
            ResumoVendas resumo = ultimoRelatorio.Resumo;

            StringBuilder csv = new StringBuilder();

            csv.AppendLine("Relatório de vendas");

            csv.AppendLine(
                $"Gerado em;{DateTime.Now:dd/MM/yyyy HH:mm}");

            csv.AppendLine("Filtros aplicados");

            csv.AppendLine(
                "Período;" + dadossistema.CampoCsv(periodoAplicado));

            csv.AppendLine(
                "Unidade;" + dadossistema.CampoCsv(unidadeAplicada));

            csv.AppendLine(
                "Forma de pagamento;" +
                dadossistema.CampoCsv(formaAplicada));

            csv.AppendLine(
                "Produto;" + dadossistema.CampoCsv(produtoAplicado));

            csv.AppendLine();

            csv.AppendLine("Resumo");

            csv.AppendLine(
                $"Vendas;{resumo.QuantidadeVendas}");

            csv.AppendLine(
                "Faturamento;" +
                resumo.Faturamento.ToString("N2", culturaBrasil));

            csv.AppendLine(
                "Recebido;" +
                resumo.Recebido.ToString("N2", culturaBrasil));

            csv.AppendLine(
                "Em Notinha;" +
                resumo.EmNotinha.ToString("N2", culturaBrasil));

            csv.AppendLine(
                "Ticket médio;" +
                resumo.TicketMedio.ToString("N2", culturaBrasil));

            csv.AppendLine();

            csv.AppendLine("Faturamento por dia");
            csv.AppendLine("Dia;Vendas;Faturamento");

            foreach (LinhaFaturamentoDia linha in
                ultimoRelatorio.PorDia.Where(d => d.QuantidadeVendas > 0))
            {
                csv.AppendLine(
                    string.Join(
                        ";",
                        linha.Dia.ToString("dd/MM/yyyy"),
                        linha.QuantidadeVendas,
                        linha.Faturamento.ToString("N2", culturaBrasil)
                    )
                );
            }

            csv.AppendLine();

            csv.AppendLine("Produtos vendidos");
            csv.AppendLine("Produto;Quantidade;Receita");

            foreach (LinhaProdutoVendido linha in ultimoRelatorio.Produtos)
            {
                csv.AppendLine(
                    string.Join(
                        ";",
                        dadossistema.CampoCsv(linha.Produto),
                        linha.Quantidade.ToString("N2", culturaBrasil),
                        linha.Receita.ToString("N2", culturaBrasil)
                    )
                );
            }

            csv.AppendLine();

            csv.AppendLine("Formas de pagamento");
            csv.AppendLine("Forma de pagamento;Valor");

            foreach (LinhaFormaPagamento linha in ultimoRelatorio.Formas)
            {
                csv.AppendLine(
                    string.Join(
                        ";",
                        dadossistema.CampoCsv(linha.Forma),
                        linha.Valor.ToString("N2", culturaBrasil)
                    )
                );
            }

            File.WriteAllText(
                caminho,
                csv.ToString(),
                new UTF8Encoding(true));
        }
        public ucrelatoriovendas()
        {
            InitializeComponent();

            ConfigurarGrade(dgvdia);
            ConfigurarGrade(dgvprodutos);
            ConfigurarGrade(dgvformas);

            ConfigurarGraficos();

            cbvisualizacaodia.SelectedIndex = 0;
            cbvisualizacaoprodutos.SelectedIndex = 0;
            cbvisualizacaoformas.SelectedIndex = 0;

            AtualizarVisualizacoes();

            dtpfiltroinicio.Value =
                DateTime.Today.AddDays(-30);

            dtpfiltroinicio.Checked =
                true;

            CarregarFiltros();

            bexportarvenda.Click +=
                bexportarvenda_Click;

            dtpfiltroinicio.ValueChanged +=
                FiltroAlterado;

            dtpfiltrofim.ValueChanged +=
                FiltroAlterado;

            cbfiltrounidade.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltroformapagamento.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltroproduto.SelectedIndexChanged +=
                FiltroAlterado;

            cbvisualizacaodia.SelectedIndexChanged +=
                VisualizacaoAlterada;

            cbvisualizacaoprodutos.SelectedIndexChanged +=
                VisualizacaoAlterada;

            cbvisualizacaoformas.SelectedIndexChanged +=
                VisualizacaoAlterada;

            GerarRelatorio();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class ucdre : UserControl
    {
        private const int LinhaReceita = 0;
        private const int LinhaRecebido = 1;
        private const int LinhaVendidoNotinha = 2;
        private const int LinhaDespesas = 3;
        private const int LinhaPerdas = 4;
        private const int LinhaResultado = 5;
        private const int LinhaAbatimentos = 6;
        private const int LinhaAReceber = 7;

        private bool carregandoFiltros = false;

        private RelatorioDre ultimoRelatorio;

        private string periodoAplicado = "-";

        private string unidadeAplicada = "Todas";

        private CultureInfo culturaBrasil =
            new CultureInfo("pt-BR");

        private static readonly string[] Descricoes =
        {
            "Receita (vendas concluídas)",
            "     Recebido (sem Notinha)",
            "     Vendido em Notinha",
            "(−) Despesas: compras de estoque",
            "     Perdas (já incluídas nas compras)",
            "(=) Resultado",
            "Notinha: abatimentos recebidos no período",
            "Notinha a receber (vendido menos abatimentos)"
        };

        private static decimal Valor(DreUnidade dre, int linha)
        {
            switch (linha)
            {
                case LinhaReceita:
                    return dre.Receita;

                case LinhaRecebido:
                    return dre.Recebido;

                case LinhaVendidoNotinha:
                    return dre.VendidoEmNotinha;

                case LinhaDespesas:
                    return dre.Despesas;

                case LinhaPerdas:
                    return dre.Perdas;

                case LinhaResultado:
                    return dre.Resultado;

                case LinhaAbatimentos:
                    return dre.Abatimentos;

                default:
                    return dre.NotinhaAReceber;
            }
        }

        public ucdre()
        {
            InitializeComponent();

            dtpdreinicio.Value = DateTime.Today.AddDays(-30);
            dtpdreinicio.Checked = true;

            dtpdrefim.Value = DateTime.Today;
            dtpdrefim.Checked = true;

            CarregarUnidades();

            dtpdreinicio.ValueChanged += FiltroAlterado;
            dtpdrefim.ValueChanged += FiltroAlterado;
            cbdreunidade.SelectedIndexChanged += FiltroAlterado;

            bexportardre.Click += bexportardre_Click;

            dgvdre.CellFormatting += dgvdre_CellFormatting;

            GerarRelatorio();
        }

        public void AtualizarTela()
        {
            CarregarUnidades();

            GerarRelatorio();
        }

        private void CarregarUnidades()
        {
            carregandoFiltros = true;

            string selecionada =
                cbdreunidade.SelectedItem as string;

            cbdreunidade.Items.Clear();
            cbdreunidade.Items.Add("Todas");

            foreach (Unidade unidade in
                dadossistema.unidades.OrderBy(u => u.Nome))
            {
                cbdreunidade.Items.Add(unidade.Nome);
            }

            if (selecionada != null &&
                cbdreunidade.Items.Contains(selecionada))
            {
                cbdreunidade.SelectedItem = selecionada;
            }
            else
            {
                cbdreunidade.SelectedIndex = 0;
            }

            carregandoFiltros = false;
        }

        private void FiltroAlterado(object sender, EventArgs e)
        {
            if (carregandoFiltros)
            {
                return;
            }

            GerarRelatorio();
        }

        private void GerarRelatorio()
        {
            DateTime? inicio =
                dtpdreinicio.Checked
                ? (DateTime?)dtpdreinicio.Value.Date
                : null;

            DateTime? fim =
                dtpdrefim.Checked
                ? (DateTime?)dtpdrefim.Value.Date
                : null;

            string unidadeFiltro =
                cbdreunidade.SelectedItem == null
                ? "Todas"
                : cbdreunidade.SelectedItem.ToString();

            string mensagem;

            RelatorioDre relatorio =
                dadossistema.GerarDre(
                    inicio,
                    fim,
                    unidadeFiltro == "Todas" ? null : unidadeFiltro,
                    out mensagem);

            if (relatorio == null)
            {
                ultimoRelatorio = null;

                dgvdre.DataSource = null;

                lperiododre.Text = "Período: -";

                ldreaviso.ForeColor = Color.Firebrick;
                ldreaviso.Text = mensagem;

                return;
            }

            ultimoRelatorio = relatorio;

            periodoAplicado =
                $"{relatorio.DataInicial:dd/MM/yyyy} a {relatorio.DataFinal:dd/MM/yyyy}";

            if (inicio == null && fim == null)
            {
                periodoAplicado += " (padrão: últimos 30 dias)";
            }

            unidadeAplicada = unidadeFiltro;

            lperiododre.Text =
                $"Período: {periodoAplicado}";

            if (relatorio.Total.PerdasSemCusto > 0)
            {
                ldreaviso.ForeColor = Color.DarkOrange;

                ldreaviso.Text =
                    $"Atenção: {relatorio.Total.PerdasSemCusto} perda(s) sem custo conhecido entraram com valor zero.";
            }
            else
            {
                ldreaviso.Text = "";
            }

            PreencherGrade(relatorio);
        }

        private void PreencherGrade(RelatorioDre relatorio)
        {
            DataTable tabela = new DataTable();

            tabela.Columns.Add("Descricao", typeof(string));

            foreach (DreUnidade unidade in relatorio.Unidades)
            {
                tabela.Columns.Add(unidade.Unidade, typeof(decimal));
            }

            if (relatorio.Consolidado)
            {
                tabela.Columns.Add("Total", typeof(decimal));
            }

            for (int linha = 0; linha < Descricoes.Length; linha++)
            {
                DataRow nova = tabela.NewRow();

                nova[0] = Descricoes[linha];

                int coluna = 1;

                foreach (DreUnidade unidade in relatorio.Unidades)
                {
                    nova[coluna] = Valor(unidade, linha);
                    coluna++;
                }

                if (relatorio.Consolidado)
                {
                    nova[coluna] = Valor(relatorio.Total, linha);
                }

                tabela.Rows.Add(nova);
            }

            dgvdre.DataSource = tabela;

            dgvdre.Columns[0].HeaderText = "Demonstrativo";
            dgvdre.Columns[0].FillWeight = 260;

            for (int i = 1; i < dgvdre.Columns.Count; i++)
            {
                dgvdre.Columns[i].DefaultCellStyle.Format = "C2";

                dgvdre.Columns[i].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            dgvdre.ClearSelection();
        }

        private void dgvdre_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.RowIndex == LinhaResultado ||
                e.RowIndex == LinhaReceita)
            {
                e.CellStyle.Font =
                    new Font(
                        dgvdre.Font,
                        FontStyle.Bold);
            }

            if (e.RowIndex == LinhaRecebido ||
                e.RowIndex == LinhaVendidoNotinha ||
                e.RowIndex == LinhaPerdas ||
                e.RowIndex == LinhaAbatimentos ||
                e.RowIndex == LinhaAReceber)
            {
                e.CellStyle.ForeColor = Color.DimGray;
            }

            if (e.RowIndex == LinhaResultado &&
                e.ColumnIndex > 0 &&
                e.Value is decimal)
            {
                decimal resultado = (decimal)e.Value;

                e.CellStyle.ForeColor =
                    resultado < 0
                    ? Color.Firebrick
                    : Color.SeaGreen;
            }
        }

        private string Moeda(decimal valor)
        {
            return "R$ " + valor.ToString("N2", culturaBrasil);
        }

        private List<string> FiltrosAplicados()
        {
            return new List<string>
            {
                "Período: " + periodoAplicado,
                "Unidade: " + unidadeAplicada
            };
        }

        private List<DreUnidade> ColunasDoRelatorio()
        {
            List<DreUnidade> colunas =
                new List<DreUnidade>(ultimoRelatorio.Unidades);

            if (ultimoRelatorio.Consolidado)
            {
                colunas.Add(ultimoRelatorio.Total);
            }

            return colunas;
        }

        private void bexportardre_Click(object sender, EventArgs e)
        {
            if (ultimoRelatorio == null)
            {
                MessageBox.Show(
                    "Informe um período válido antes de exportar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            bool pdf;

            string caminho =
                ExportacaoRelatorio.PedirArquivo(
                    "dre",
                    out pdf);

            if (caminho == null)
            {
                return;
            }

            try
            {
                if (pdf)
                {
                    GerarPdfDre(caminho);
                }
                else
                {
                    GerarCsvDre(caminho);
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

        private void GerarPdfDre(string caminho)
        {
            List<DreUnidade> colunas = ColunasDoRelatorio();

            RelatorioPdf relatorio = new RelatorioPdf(
                "DRE - Demonstrativo de resultados",
                colunas.Count > 3,
                FiltrosAplicados());

            List<ColunaPdf> colunasPdf = new List<ColunaPdf>
            {
                new ColunaPdf("Demonstrativo", 6.0)
            };

            foreach (DreUnidade coluna in colunas)
            {
                colunasPdf.Add(new ColunaPdf(coluna.Unidade, 2.6, true));
            }

            List<string[]> linhasPrincipais = new List<string[]>();

            foreach (int linha in new[]
            {
                LinhaReceita,
                LinhaRecebido,
                LinhaVendidoNotinha,
                LinhaDespesas,
                LinhaPerdas
            })
            {
                linhasPrincipais.Add(MontarLinha(linha, colunas));
            }

            relatorio.AdicionarTabela(
                "Demonstrativo",
                colunasPdf,
                linhasPrincipais,
                MontarLinha(LinhaResultado, colunas));

            List<string[]> linhasNotinha = new List<string[]>
            {
                MontarLinha(LinhaAbatimentos, colunas),
                MontarLinha(LinhaAReceber, colunas)
            };

            relatorio.AdicionarTabela(
                "Notinha (informativo)",
                colunasPdf,
                linhasNotinha);

            List<KeyValuePair<string, string>> observacoes =
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(
                        "Despesas",
                        "Entradas de estoque do período"),
                    new KeyValuePair<string, string>(
                        "Perdas",
                        "Já incluídas nas compras, não somadas de novo")
                };

            if (ultimoRelatorio.Total.PerdasSemCusto > 0)
            {
                observacoes.Add(
                    new KeyValuePair<string, string>(
                        "Perdas sem custo conhecido",
                        ultimoRelatorio.Total.PerdasSemCusto +
                        " (valor zero)"));
            }

            relatorio.AdicionarResumo("Observações", observacoes);

            relatorio.Salvar(caminho);
        }

        private string[] MontarLinha(int linha, List<DreUnidade> colunas)
        {
            string[] texto = new string[colunas.Count + 1];

            texto[0] = Descricoes[linha].Trim();

            for (int i = 0; i < colunas.Count; i++)
            {
                texto[i + 1] = Moeda(Valor(colunas[i], linha));
            }

            return texto;
        }

        private void GerarCsvDre(string caminho)
        {
            List<DreUnidade> colunas = ColunasDoRelatorio();

            StringBuilder csv = new StringBuilder();

            csv.AppendLine("DRE - Demonstrativo de resultados");

            csv.AppendLine(
                $"Gerado em;{DateTime.Now:dd/MM/yyyy HH:mm}");

            csv.AppendLine("Filtros aplicados");

            csv.AppendLine(
                "Período;" + dadossistema.CampoCsv(periodoAplicado));

            csv.AppendLine(
                "Unidade;" + dadossistema.CampoCsv(unidadeAplicada));

            csv.AppendLine();

            StringBuilder cabecalho = new StringBuilder("Demonstrativo");

            foreach (DreUnidade coluna in colunas)
            {
                cabecalho.Append(";");
                cabecalho.Append(dadossistema.CampoCsv(coluna.Unidade));
            }

            csv.AppendLine(cabecalho.ToString());

            for (int linha = 0; linha < Descricoes.Length; linha++)
            {
                StringBuilder texto =
                    new StringBuilder(
                        dadossistema.CampoCsv(Descricoes[linha].Trim()));

                foreach (DreUnidade coluna in colunas)
                {
                    texto.Append(";");

                    texto.Append(
                        Valor(coluna, linha)
                        .ToString("N2", culturaBrasil));
                }

                csv.AppendLine(texto.ToString());
            }

            csv.AppendLine();

            csv.AppendLine(
                "Observação;Perdas já incluídas nas compras, não somadas de novo");

            if (ultimoRelatorio.Total.PerdasSemCusto > 0)
            {
                csv.AppendLine(
                    "Perdas sem custo conhecido (valor zero);" +
                    ultimoRelatorio.Total.PerdasSemCusto);
            }

            File.WriteAllText(
                caminho,
                csv.ToString(),
                new UTF8Encoding(true));
        }
    }
}

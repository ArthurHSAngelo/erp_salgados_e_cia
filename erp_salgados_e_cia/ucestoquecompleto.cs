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
    public partial class ucestoquecompleto : UserControl
    {
        private BindingSource fonteEstoque =
            new BindingSource();

        private bool carregandoFiltros = false;

        private CultureInfo culturaBrasil =
            new CultureInfo("pt-BR");

        private void CarregarCategorias()
        {
            carregandoFiltros = true;

            string categoriaSelecionada =
                cbfiltrocategoria.SelectedItem as string;

            cbfiltrocategoria.Items.Clear();
            cbfiltrocategoria.Items.Add("Todas");

            foreach (string categoria in
                dadossistema.produtos
                .Select(p => p.Categoria)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c))
            {
                cbfiltrocategoria.Items.Add(categoria);
            }

            if (categoriaSelecionada != null &&
                cbfiltrocategoria.Items.Contains(categoriaSelecionada))
            {
                cbfiltrocategoria.SelectedItem =
                    categoriaSelecionada;
            }
            else
            {
                cbfiltrocategoria.SelectedIndex = 0;
            }

            carregandoFiltros = false;
        }

        private void CarregarUnidades()
        {
            carregandoFiltros = true;

            string unidadeSelecionada =
                cbfiltrounidade.SelectedItem as string;

            cbfiltrounidade.Items.Clear();

            if (dadossistema.LoginEhDeAtendente)
            {
                cbfiltrounidade.Items.Add(dadossistema.UnidadeAtual);
                cbfiltrounidade.SelectedIndex = 0;
                cbfiltrounidade.Enabled = false;
            }
            else
            {
                cbfiltrounidade.Enabled = true;

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
            }

            carregandoFiltros = false;
        }

        private string UnidadeFiltrada()
        {
            if (dadossistema.LoginEhDeAtendente)
            {
                return dadossistema.UnidadeAtual;
            }

            string escolhida =
                cbfiltrounidade.SelectedItem as string;

            return escolhida == null || escolhida == "Todas"
                ? null
                : escolhida;
        }

        private void AtualizarTabela()
        {
            string filtroProduto =
                tbfiltroproduto.Text.Trim();

            string filtroCategoria =
                cbfiltrocategoria.SelectedItem == null
                ? "Todas"
                : cbfiltrocategoria.SelectedItem.ToString();

            string filtroStatus =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<LinhaEstoque> linhasFiltradas =
                dadossistema.MontarRelatorioEstoque(UnidadeFiltrada());

            if (filtroProduto != "")
            {
                linhasFiltradas =
                    linhasFiltradas.Where(linha =>
                        linha.Produto.IndexOf(
                            filtroProduto,
                            StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (filtroCategoria != "Todas")
            {
                linhasFiltradas =
                    linhasFiltradas.Where(linha =>
                        linha.Categoria == filtroCategoria);
            }

            if (filtroStatus == "Ativos")
            {
                linhasFiltradas =
                    linhasFiltradas.Where(linha =>
                        linha.Status ==
                        dadossistema.StatusCadastro.Ativo.ToString());
            }
            else if (filtroStatus == "Inativos")
            {
                linhasFiltradas =
                    linhasFiltradas.Where(linha =>
                        linha.Status ==
                        dadossistema.StatusCadastro.Inativo.ToString());
            }

            if (cbfiltrocriticos.Checked)
            {
                linhasFiltradas =
                    linhasFiltradas.Where(linha =>
                        linha.Situacao == "Crítico");
            }

            List<LinhaEstoque> linhasExibidas =
                linhasFiltradas.ToList();

            fonteEstoque.DataSource =
                linhasExibidas;

            ConfigurarColunas();
            AtualizarTotais(linhasExibidas);
        }

        private void ConfigurarColunas()
        {
            if (dgvestoquecompleto.Columns.Count == 0)
            {
                return;
            }

            dgvestoquecompleto.Columns["Produto"].HeaderText =
                "Produto";

            dgvestoquecompleto.Columns["Categoria"].HeaderText =
                "Categoria";

            dgvestoquecompleto.Columns["UnidadeMedida"].HeaderText =
                "Unidade de medida";

            dgvestoquecompleto.Columns["EstoqueAtual"].HeaderText =
                "Estoque atual";

            dgvestoquecompleto.Columns["EstoqueMinimo"].HeaderText =
                "Estoque mínimo";

            dgvestoquecompleto.Columns["Situacao"].HeaderText =
                "Situação";

            dgvestoquecompleto.Columns["PrecoVenda"].HeaderText =
                "Preço de venda";

            dgvestoquecompleto.Columns["UltimoCusto"].HeaderText =
                "Último custo";

            dgvestoquecompleto.Columns["ValorACusto"].HeaderText =
                "Valor a custo";

            dgvestoquecompleto.Columns["ValorAPrecoVenda"].HeaderText =
                "Valor a preço de venda";

            dgvestoquecompleto.Columns["Status"].HeaderText =
                "Status";

            string[] colunasNumericas =
            {
                "EstoqueAtual",
                "EstoqueMinimo",
                "PrecoVenda",
                "UltimoCusto",
                "ValorACusto",
                "ValorAPrecoVenda"
            };

            foreach (string nomeColuna in colunasNumericas)
            {
                dgvestoquecompleto.Columns[nomeColuna]
                    .DefaultCellStyle.Format = "N2";

                dgvestoquecompleto.Columns[nomeColuna]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            foreach (string nomeColuna in new[]
            {
                "PrecoVenda",
                "UltimoCusto",
                "ValorACusto",
                "ValorAPrecoVenda"
            })
            {
                dgvestoquecompleto.Columns[nomeColuna]
                    .DefaultCellStyle.Format = "C2";
            }
        }

        private void AtualizarTotais(List<LinhaEstoque> linhas)
        {
            decimal valorCusto =
                linhas.Sum(linha => linha.ValorACusto);

            decimal valorVenda =
                linhas.Sum(linha => linha.ValorAPrecoVenda);

            int totalCriticos =
                linhas.Count(linha => linha.Situacao == "Crítico");

            ltotalprodutos.Text =
                $"Produtos: {linhas.Count}";

            ltotalcriticos.Text =
                $"Em estoque crítico: {totalCriticos}";

            ltotalvalorcusto.Text =
                $"Valor a custo: R$ {valorCusto.ToString("N2", culturaBrasil)}";

            ltotalvalorvenda.Text =
                $"Valor a preço de venda: R$ {valorVenda.ToString("N2", culturaBrasil)}";
        }

        public void AtualizarTela()
        {
            CarregarUnidades();

            CarregarCategorias();

            AtualizarTabela();
        }

        public ucestoquecompleto()
        {
            InitializeComponent();

            fonteEstoque.DataSource =
                new List<LinhaEstoque>();

            dgvestoquecompleto.DataSource =
                fonteEstoque;

            cbfiltrostatus.SelectedIndex =
                0;

            CarregarUnidades();

            CarregarCategorias();

            cbfiltrounidade.SelectedIndexChanged +=
                FiltroAlterado;

            tbfiltroproduto.TextChanged +=
                FiltroAlterado;

            cbfiltrocategoria.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltrostatus.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltrocriticos.CheckedChanged +=
                FiltroAlterado;

            dgvestoquecompleto.CellFormatting +=
                dgvestoquecompleto_CellFormatting;

            bexportarestoque.Click +=
                bexportarestoque_Click;

            AtualizarTabela();
        }

        public void DefinirSomenteCriticos()
        {
            carregandoFiltros = true;

            if (cbfiltrounidade.Items.Contains(dadossistema.UnidadeAtual))
            {
                cbfiltrounidade.SelectedItem = dadossistema.UnidadeAtual;
            }

            tbfiltroproduto.Clear();

            if (cbfiltrocategoria.Items.Count > 0)
            {
                cbfiltrocategoria.SelectedIndex = 0;
            }

            cbfiltrostatus.SelectedItem = "Ativos";

            cbfiltrocriticos.Checked = true;

            carregandoFiltros = false;

            AtualizarTabela();
        }

        private void FiltroAlterado(object sender, EventArgs e)
        {
            if (carregandoFiltros)
            {
                return;
            }

            AtualizarTabela();
        }

        private void dgvestoquecompleto_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            LinhaEstoque linha =
                dgvestoquecompleto.Rows[e.RowIndex]
                .DataBoundItem as LinhaEstoque;

            if (linha == null)
            {
                return;
            }

            if (linha.Situacao == "Crítico")
            {
                e.CellStyle.ForeColor = Color.Firebrick;
            }

            if (dgvestoquecompleto.Columns[e.ColumnIndex].Name ==
                "UltimoCusto" &&
                linha.UltimoCusto == null)
            {
                e.Value = "Sem entrada";
                e.FormattingApplied = true;
            }
        }

        private List<string> FiltrosAplicados()
        {
            string filtroProduto =
                tbfiltroproduto.Text.Trim() == ""
                ? "Todos"
                : tbfiltroproduto.Text.Trim();

            return new List<string>
            {
                "Unidade: " + (UnidadeFiltrada() ?? "Todas"),
                "Produto: " + filtroProduto,
                "Categoria: " + cbfiltrocategoria.SelectedItem,
                "Status: " + cbfiltrostatus.SelectedItem,
                "Somente estoque crítico: " +
                    (cbfiltrocriticos.Checked ? "Sim" : "Não")
            };
        }

        private void bexportarestoque_Click(object sender, EventArgs e)
        {
            List<LinhaEstoque> linhas =
                fonteEstoque.DataSource as List<LinhaEstoque>;

            if (linhas == null || linhas.Count == 0)
            {
                MessageBox.Show(
                    "Não há registros para exportar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            bool pdf;

            string caminho =
                ExportacaoRelatorio.PedirArquivo(
                    "estoque_completo",
                    out pdf);

            if (caminho == null)
            {
                return;
            }

            try
            {
                if (pdf)
                {
                    GerarPdfEstoque(linhas, caminho);
                }
                else
                {
                    GerarCsvEstoque(linhas, caminho);
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

        private void GerarPdfEstoque(
            List<LinhaEstoque> linhas,
            string caminho)
        {
            RelatorioPdf relatorio = new RelatorioPdf(
                "Relatório de estoque - Posição do estoque",
                true,
                FiltrosAplicados());

            List<ColunaPdf> colunas = new List<ColunaPdf>
            {
                new ColunaPdf("Produto", 3.2),
                new ColunaPdf("Categoria", 2.0),
                new ColunaPdf("Unidade", 1.6),
                new ColunaPdf("Estoque atual", 1.6, true),
                new ColunaPdf("Estoque mínimo", 1.6, true),
                new ColunaPdf("Situação", 1.5),
                new ColunaPdf("Preço de venda", 1.7, true),
                new ColunaPdf("Último custo", 1.8, true),
                new ColunaPdf("Valor a custo", 1.9, true),
                new ColunaPdf("Valor a preço de venda", 2.1, true),
                new ColunaPdf("Status", 1.3)
            };

            List<string[]> linhasPdf = new List<string[]>();
            List<int> criticas = new List<int>();

            for (int i = 0; i < linhas.Count; i++)
            {
                LinhaEstoque linha = linhas[i];

                if (linha.Situacao == "Crítico")
                {
                    criticas.Add(i);
                }

                linhasPdf.Add(
                    new string[]
                    {
                        linha.Produto,
                        linha.Categoria,
                        linha.UnidadeMedida,
                        linha.EstoqueAtual.ToString("N2", culturaBrasil),
                        linha.EstoqueMinimo.ToString("N2", culturaBrasil),
                        linha.Situacao,
                        linha.PrecoVenda.ToString("N2", culturaBrasil),
                        linha.UltimoCusto == null
                            ? "Sem entrada"
                            : linha.UltimoCusto.Value.ToString(
                                "N2",
                                culturaBrasil),
                        linha.ValorACusto.ToString("N2", culturaBrasil),
                        linha.ValorAPrecoVenda.ToString(
                            "N2",
                            culturaBrasil),
                        linha.Status
                    }
                );
            }

            relatorio.AdicionarTabela(
                "Posição do estoque",
                colunas,
                linhasPdf,
                null,
                criticas);

            relatorio.AdicionarResumo(
                "Resumo",
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(
                        "Total de produtos",
                        linhas.Count.ToString()),
                    new KeyValuePair<string, string>(
                        "Em estoque crítico",
                        criticas.Count.ToString()),
                    new KeyValuePair<string, string>(
                        "Valor a custo",
                        "R$ " + linhas.Sum(l => l.ValorACusto)
                            .ToString("N2", culturaBrasil)),
                    new KeyValuePair<string, string>(
                        "Valor a preço de venda",
                        "R$ " + linhas.Sum(l => l.ValorAPrecoVenda)
                            .ToString("N2", culturaBrasil))
                });

            relatorio.Salvar(caminho);
        }

        private void GerarCsvEstoque(
            List<LinhaEstoque> linhas,
            string caminho)
        {
            string filtroProduto =
                tbfiltroproduto.Text.Trim() == ""
                ? "Todos"
                : tbfiltroproduto.Text.Trim();

            StringBuilder csv = new StringBuilder();

            csv.AppendLine("Relatório de estoque - Posição do estoque");

            csv.AppendLine(
                $"Gerado em;{DateTime.Now:dd/MM/yyyy HH:mm}");

            csv.AppendLine("Filtros aplicados");

            csv.AppendLine(
                "Unidade;" +
                dadossistema.CampoCsv(UnidadeFiltrada() ?? "Todas"));

            csv.AppendLine(
                "Produto;" + dadossistema.CampoCsv(filtroProduto));

            csv.AppendLine(
                "Categoria;" +
                dadossistema.CampoCsv(
                    cbfiltrocategoria.SelectedItem.ToString()));

            csv.AppendLine(
                "Status;" + cbfiltrostatus.SelectedItem);

            csv.AppendLine(
                "Somente estoque crítico;" +
                (cbfiltrocriticos.Checked ? "Sim" : "Não"));

            csv.AppendLine();

            csv.AppendLine(
                "Produto;Categoria;Unidade de medida;Estoque atual;" +
                "Estoque mínimo;Situação;Preço de venda;Último custo;" +
                "Valor a custo;Valor a preço de venda;Status");

            foreach (LinhaEstoque linha in linhas)
            {
                csv.AppendLine(
                    string.Join(
                        ";",
                        dadossistema.CampoCsv(linha.Produto),
                        dadossistema.CampoCsv(linha.Categoria),
                        dadossistema.CampoCsv(linha.UnidadeMedida),
                        linha.EstoqueAtual.ToString("N2", culturaBrasil),
                        linha.EstoqueMinimo.ToString("N2", culturaBrasil),
                        linha.Situacao,
                        linha.PrecoVenda.ToString("N2", culturaBrasil),
                        linha.UltimoCusto == null
                            ? "Sem entrada"
                            : linha.UltimoCusto.Value.ToString(
                                "N2",
                                culturaBrasil),
                        linha.ValorACusto.ToString("N2", culturaBrasil),
                        linha.ValorAPrecoVenda.ToString(
                            "N2",
                            culturaBrasil),
                        linha.Status
                    )
                );
            }

            csv.AppendLine();

            csv.AppendLine(
                $"Total de produtos;{linhas.Count}");

            csv.AppendLine(
                "Em estoque crítico;" +
                linhas.Count(linha => linha.Situacao == "Crítico"));

            csv.AppendLine(
                "Valor a custo;" +
                linhas.Sum(linha => linha.ValorACusto)
                    .ToString("N2", culturaBrasil));

            csv.AppendLine(
                "Valor a preço de venda;" +
                linhas.Sum(linha => linha.ValorAPrecoVenda)
                    .ToString("N2", culturaBrasil));

            File.WriteAllText(
                caminho,
                csv.ToString(),
                new UTF8Encoding(true));
        }
    }
}
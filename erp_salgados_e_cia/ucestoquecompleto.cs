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
                dadossistema.MontarRelatorioEstoque();

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

            CarregarCategorias();

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

            using (SaveFileDialog janela = new SaveFileDialog())
            {
                janela.Filter = "Arquivo CSV (*.csv)|*.csv";

                janela.FileName =
                    $"estoque_completo_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (janela.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

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
using System;
using System.Collections.Generic;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace erp_salgados_e_cia
{
    public class ColunaPdf
    {
        public ColunaPdf(
            string titulo,
            double peso,
            bool alinharDireita = false)
        {
            Titulo = titulo;
            Peso = peso;
            AlinharDireita = alinharDireita;
        }

        public string Titulo { get; private set; }

        public double Peso { get; private set; }

        public bool AlinharDireita { get; private set; }
    }

    public class RelatorioPdf
    {
        private static readonly Color Laranja =
            new Color(255, 140, 0);

        private static readonly Color LaranjaClaro =
            new Color(255, 236, 214);

        private static readonly Color CinzaClaro =
            new Color(245, 245, 245);

        private static readonly Color CinzaBorda =
            new Color(210, 210, 210);

        private static readonly Color CinzaTexto =
            new Color(100, 100, 100);

        private readonly Document documento =
            new Document();

        private readonly Section secao;

        private readonly double larguraUtil;

        public RelatorioPdf(
            string titulo,
            bool paisagem,
            IEnumerable<string> filtros)
        {
            documento.Info.Title = titulo;
            documento.Info.Author = "ERP Salgados & Cia";

            Style normal = documento.Styles[StyleNames.Normal];
            normal.Font.Name = "Arial";
            normal.Font.Size = 9;

            secao = documento.AddSection();

            secao.PageSetup.PageFormat = PageFormat.A4;

            secao.PageSetup.Orientation =
                paisagem
                ? Orientation.Landscape
                : Orientation.Portrait;

            secao.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            secao.PageSetup.RightMargin = Unit.FromCentimeter(1.5);
            secao.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
            secao.PageSetup.BottomMargin = Unit.FromCentimeter(1.8);

            larguraUtil = paisagem ? 29.7 - 3.0 : 21.0 - 3.0;

            MontarCabecalho(titulo, filtros);

            MontarRodape();
        }

        private void MontarCabecalho(
            string titulo,
            IEnumerable<string> filtros)
        {
            Paragraph sistema = secao.AddParagraph("ERP Salgados & Cia");
            sistema.Format.Font.Size = 10;
            sistema.Format.Font.Bold = true;
            sistema.Format.Font.Color = Laranja;

            Paragraph tituloRelatorio = secao.AddParagraph(titulo);
            tituloRelatorio.Format.Font.Size = 18;
            tituloRelatorio.Format.Font.Bold = true;
            tituloRelatorio.Format.SpaceBefore = Unit.FromPoint(2);
            tituloRelatorio.Format.Borders.Bottom.Color = Laranja;
            tituloRelatorio.Format.Borders.Bottom.Width = 1.5;
            tituloRelatorio.Format.Borders.DistanceFromBottom =
                Unit.FromPoint(4);

            string usuario =
                dadossistema.UsuarioLogado == null
                ? "-"
                : dadossistema.UsuarioLogado.Login;

            Paragraph geracao = secao.AddParagraph(
                $"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}  |  " +
                $"Usuário: {usuario}  |  " +
                $"Unidade de trabalho: {dadossistema.UnidadeAtual}");

            geracao.Format.Font.Size = 8;
            geracao.Format.Font.Color = CinzaTexto;
            geracao.Format.SpaceBefore = Unit.FromPoint(6);

            Paragraph tituloFiltros = secao.AddParagraph("Filtros aplicados");
            tituloFiltros.Format.Font.Bold = true;
            tituloFiltros.Format.Font.Size = 10;
            tituloFiltros.Format.SpaceBefore = Unit.FromPoint(10);

            List<string> lista =
                filtros == null
                ? new List<string>()
                : filtros.ToList();

            if (lista.Count == 0)
            {
                Paragraph nenhum = secao.AddParagraph("Nenhum filtro.");
                nenhum.Format.Font.Color = CinzaTexto;
            }

            foreach (string filtro in lista)
            {
                secao.AddParagraph(filtro);
            }
        }

        private void MontarRodape()
        {
            Paragraph rodape = secao.Footers.Primary.AddParagraph();

            rodape.Format.Font.Size = 8;
            rodape.Format.Font.Color = CinzaTexto;

            rodape.Format.TabStops.AddTabStop(
                Unit.FromCentimeter(larguraUtil),
                TabAlignment.Right);

            rodape.AddText("ERP Salgados & Cia");
            rodape.AddTab();
            rodape.AddText("Página ");
            rodape.AddPageField();
            rodape.AddText(" de ");
            rodape.AddNumPagesField();
        }

        private void AdicionarTitulo(string titulo)
        {
            Paragraph paragrafo = secao.AddParagraph(titulo);
            paragrafo.Format.Font.Size = 11;
            paragrafo.Format.Font.Bold = true;
            paragrafo.Format.SpaceBefore = Unit.FromPoint(14);
            paragrafo.Format.SpaceAfter = Unit.FromPoint(4);
            paragrafo.Format.KeepWithNext = true;
        }

        private Table CriarTabela()
        {
            Table tabela = secao.AddTable();

            tabela.Borders.Color = CinzaBorda;
            tabela.Borders.Width = 0.25;

            tabela.LeftPadding = Unit.FromPoint(4);
            tabela.RightPadding = Unit.FromPoint(4);
            tabela.TopPadding = Unit.FromPoint(3);
            tabela.BottomPadding = Unit.FromPoint(3);

            return tabela;
        }

        public void AdicionarResumo(
            string titulo,
            IEnumerable<KeyValuePair<string, string>> itens)
        {
            AdicionarTitulo(titulo);

            Table tabela = CriarTabela();

            double larguraRotulo = larguraUtil * 0.5;
            double larguraValor = larguraUtil * 0.5;

            tabela.AddColumn(Unit.FromCentimeter(larguraRotulo));

            Column colunaValor =
                tabela.AddColumn(Unit.FromCentimeter(larguraValor));

            colunaValor.Format.Alignment = ParagraphAlignment.Right;

            int indice = 0;

            foreach (KeyValuePair<string, string> item in itens)
            {
                Row linha = tabela.AddRow();

                linha.Cells[0].AddParagraph(item.Key ?? "");
                linha.Cells[1].AddParagraph(item.Value ?? "");

                linha.Cells[1].Format.Font.Bold = true;

                if (indice % 2 == 1)
                {
                    linha.Shading.Color = CinzaClaro;
                }

                indice++;
            }
        }

        public void AdicionarTabela(
            string titulo,
            List<ColunaPdf> colunas,
            List<string[]> linhas,
            string[] linhaTotal = null,
            ICollection<int> linhasDestacadas = null)
        {
            AdicionarTitulo(titulo);

            if (linhas == null || linhas.Count == 0)
            {
                Paragraph vazio = secao.AddParagraph("Nenhum registro.");
                vazio.Format.Font.Color = CinzaTexto;
                return;
            }

            Table tabela = CriarTabela();

            double somaPesos = colunas.Sum(c => c.Peso);

            foreach (ColunaPdf coluna in colunas)
            {
                tabela.AddColumn(
                    Unit.FromCentimeter(
                        larguraUtil * coluna.Peso / somaPesos));
            }

            Row cabecalho = tabela.AddRow();
            cabecalho.HeadingFormat = true;
            cabecalho.Shading.Color = Laranja;
            cabecalho.Format.Font.Color = Colors.White;
            cabecalho.Format.Font.Bold = true;
            cabecalho.VerticalAlignment = VerticalAlignment.Center;

            for (int j = 0; j < colunas.Count; j++)
            {
                cabecalho.Cells[j].AddParagraph(colunas[j].Titulo);

                cabecalho.Cells[j].Format.Alignment =
                    colunas[j].AlinharDireita
                    ? ParagraphAlignment.Right
                    : ParagraphAlignment.Left;
            }

            for (int i = 0; i < linhas.Count; i++)
            {
                Row linha = tabela.AddRow();
                linha.VerticalAlignment = VerticalAlignment.Center;

                if (i % 2 == 1)
                {
                    linha.Shading.Color = CinzaClaro;
                }

                if (linhasDestacadas != null &&
                    linhasDestacadas.Contains(i))
                {
                    linha.Format.Font.Color = new Color(178, 34, 34);
                }

                for (int j = 0; j < colunas.Count; j++)
                {
                    string texto =
                        j < linhas[i].Length
                        ? linhas[i][j] ?? ""
                        : "";

                    linha.Cells[j].AddParagraph(texto);

                    linha.Cells[j].Format.Alignment =
                        colunas[j].AlinharDireita
                        ? ParagraphAlignment.Right
                        : ParagraphAlignment.Left;
                }
            }

            if (linhaTotal != null)
            {
                Row total = tabela.AddRow();
                total.Format.Font.Bold = true;
                total.Shading.Color = LaranjaClaro;
                total.VerticalAlignment = VerticalAlignment.Center;

                bool somentePrimeiraCelula =
                    linhaTotal.Skip(1).All(string.IsNullOrEmpty);

                for (int j = 0; j < colunas.Count; j++)
                {
                    string texto =
                        j < linhaTotal.Length
                        ? linhaTotal[j] ?? ""
                        : "";

                    total.Cells[j].AddParagraph(texto);

                    total.Cells[j].Format.Alignment =
                        colunas[j].AlinharDireita
                        ? ParagraphAlignment.Right
                        : ParagraphAlignment.Left;
                }

                if (somentePrimeiraCelula && colunas.Count > 1)
                {
                    total.Cells[0].MergeRight = colunas.Count - 1;

                    total.Cells[0].Format.Alignment =
                        ParagraphAlignment.Left;
                }
            }
        }

        public void Salvar(string caminho)
        {
            PdfDocumentRenderer renderizador =
                new PdfDocumentRenderer(true);

            renderizador.Document = documento;

            renderizador.RenderDocument();

            renderizador.PdfDocument.Save(caminho);
        }
    }
}

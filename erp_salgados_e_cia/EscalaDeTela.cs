using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace erp_salgados_e_cia
{
    /// <summary>
    /// Faz as telas caberem em monitores e notebooks menores.
    /// Cada tela é medida no tamanho de que realmente precisa (o quanto seus controles ocupam) e,
    /// se a área disponível for menor que isso, a tela inteira é reduzida na mesma proporção
    /// (tamanho, posição e fonte de cada controle), sem cortes. Em telas grandes nada muda: as
    /// âncoras do Designer cuidam do ajuste. Se nem reduzindo couber (janela muito pequena),
    /// a área ganha barra de rolagem.
    /// </summary>
    internal static class EscalaDeTela
    {
        // Tamanho mínimo de partida: abaixo disso as âncoras já não organizam bem os controles.
        private static readonly Size Base = new Size(1400, 800);

        // Folga deixada além do controle mais afastado.
        private const int Margem = 16;

        // Abaixo desse fator as letras ficam pequenas demais: prefere-se rolar a tela.
        private const float FatorMinimo = 0.55f;

        private class Original
        {
            public Rectangle Limites;
            public Font Fonte;
            public DockStyle Dock;
            public Font FonteLinha;
            public Font FonteCabecalho;
            public Font FonteLinhaAlternada;
            public int AlturaCabecalho;
            public int AlturaLinha;
            public int[] LarguraColunas;
            public Dictionary<string, Font> FontesGrafico;
        }

        private class Tela
        {
            public bool Registrada;
            public Size Referencia;
            public float Fator = 1f;
            public Dictionary<Control, Original> Controles = new Dictionary<Control, Original>();
        }

        private static readonly ConditionalWeakTable<UserControl, Tela> telas =
            new ConditionalWeakTable<UserControl, Tela>();

        /// <summary>Ajusta a tela ao espaço do painel que a hospeda e a deixa preenchendo o painel.</summary>
        public static void Ajustar(UserControl tela, Panel hospedeiro)
        {
            Size area = hospedeiro.ClientSize;

            // Antes de a janela aparecer não dá para medir direito: só encaixa. O ajuste vem depois,
            // quando a janela for exibida e redimensionada.
            if (!hospedeiro.Visible || area.Width <= 0 || area.Height <= 0)
            {
                Preencher(tela, hospedeiro);
                return;
            }

            Tela estado = telas.GetOrCreateValue(tela);

            if (!estado.Registrada)
            {
                Registrar(tela, estado);
            }

            float fator = Math.Min(
                (float)area.Width / estado.Referencia.Width,
                (float)area.Height / estado.Referencia.Height);

            fator = Math.Max(FatorMinimo, Math.Min(1f, fator));

            if (Math.Abs(fator - estado.Fator) > 0.001f)
            {
                tela.Dock = DockStyle.None;

                // Com o layout ativo, os painéis encaixados (Dock) já assumem a largura nova
                // antes de os controles de dentro deles serem ajustados.
                tela.Size = Reduzir(estado.Referencia, fator);
                tela.PerformLayout();

                tela.SuspendLayout();

                foreach (Control filho in tela.Controls)
                {
                    Aplicar(filho, estado, fator);
                }

                estado.Fator = fator;

                tela.ResumeLayout(false);
            }

            Size ocupado = Reduzir(estado.Referencia, fator);

            if (ocupado.Width <= area.Width + 1 && ocupado.Height <= area.Height + 1)
            {
                Preencher(tela, hospedeiro);
            }
            else
            {
                // Nem no menor tamanho legível a tela cabe: usa barra de rolagem.
                tela.Dock = DockStyle.None;
                tela.Location = Point.Empty;
                tela.Size = ocupado;
                hospedeiro.AutoScroll = true;
            }
        }

        private static Size Reduzir(Size tamanho, float fator)
        {
            return new Size(
                (int)Math.Round(tamanho.Width * fator),
                (int)Math.Round(tamanho.Height * fator));
        }

        private static void Preencher(UserControl tela, Panel hospedeiro)
        {
            hospedeiro.AutoScroll = false;
            tela.Dock = DockStyle.Fill;
        }

        // Mede o tamanho de que a tela precisa e guarda o desenho original dos controles nesse tamanho.
        private static void Registrar(UserControl tela, Tela estado)
        {
            tela.Dock = DockStyle.None;
            tela.AutoScaleMode = AutoScaleMode.None;

            // O layout precisa estar ativo aqui, senão as âncoras não reorganizam os controles.
            tela.Size = Base;
            tela.PerformLayout();

            Size extensao = Extensao(tela);

            Size referencia = new Size(
                extensao.Width > Base.Width ? extensao.Width + Margem : Base.Width,
                extensao.Height > Base.Height ? extensao.Height + Margem : Base.Height);

            if (referencia != Base)
            {
                tela.Size = referencia;
                tela.PerformLayout();
            }

            estado.Referencia = referencia;

            Guardar(tela, estado, true);

            estado.Registrada = true;
            estado.Fator = 1f;
        }

        // Maior extensão (direita e base) ocupada pelos controles visíveis da tela.
        private static Size Extensao(Control raiz)
        {
            int direita = 0;
            int base_ = 0;

            Percorrer(raiz, 0, 0, ref direita, ref base_);

            return new Size(direita, base_);
        }

        private static void Percorrer(
            Control pai, int deslocX, int deslocY, ref int direita, ref int base_)
        {
            foreach (Control c in pai.Controls)
            {
                if (!c.Visible)
                {
                    continue;
                }

                direita = Math.Max(direita, deslocX + c.Right);
                base_ = Math.Max(base_, deslocY + c.Bottom);

                bool rola = c is ScrollableControl && ((ScrollableControl)c).AutoScroll;

                if (c.Controls.Count > 0 &&
                    !rola &&
                    !(c is DataGridView) &&
                    !(c is Chart) &&
                    !(c is ListView) &&
                    !(c is TreeView) &&
                    !(c is TabControl))
                {
                    Percorrer(c, deslocX + c.Left, deslocY + c.Top, ref direita, ref base_);
                }
            }
        }

        private static void Guardar(Control controle, Tela estado, bool raiz)
        {
            if (!raiz)
            {
                Original o = new Original
                {
                    Limites = controle.Bounds,
                    Fonte = controle.Font,
                    Dock = controle.Dock
                };

                DataGridView grade = controle as DataGridView;

                if (grade != null)
                {
                    o.FonteLinha = grade.DefaultCellStyle.Font;
                    o.FonteCabecalho = grade.ColumnHeadersDefaultCellStyle.Font;
                    o.FonteLinhaAlternada = grade.AlternatingRowsDefaultCellStyle.Font;
                    o.AlturaCabecalho = grade.ColumnHeadersHeight;
                    o.AlturaLinha = grade.RowTemplate.Height;
                    o.LarguraColunas = new int[grade.Columns.Count];

                    for (int i = 0; i < grade.Columns.Count; i++)
                    {
                        o.LarguraColunas[i] = grade.Columns[i].Width;
                    }
                }

                Chart grafico = controle as Chart;

                if (grafico != null)
                {
                    o.FontesGrafico = new Dictionary<string, Font>();

                    foreach (ChartArea area in grafico.ChartAreas)
                    {
                        o.FontesGrafico["AX" + area.Name] = area.AxisX.LabelStyle.Font;
                        o.FontesGrafico["AY" + area.Name] = area.AxisY.LabelStyle.Font;
                        o.FontesGrafico["TX" + area.Name] = area.AxisX.TitleFont;
                        o.FontesGrafico["TY" + area.Name] = area.AxisY.TitleFont;
                    }

                    foreach (Legend legenda in grafico.Legends)
                    {
                        o.FontesGrafico["L" + legenda.Name] = legenda.Font;
                    }

                    foreach (Title titulo in grafico.Titles)
                    {
                        o.FontesGrafico["T" + titulo.Name] = titulo.Font;
                    }
                }

                estado.Controles[controle] = o;
            }

            foreach (Control filho in controle.Controls)
            {
                Guardar(filho, estado, false);
            }
        }

        private static Font Escalar(Font original, float fator)
        {
            if (original == null)
            {
                return null;
            }

            return new Font(
                original.FontFamily,
                Math.Max(5f, original.Size * fator),
                original.Style,
                original.Unit);
        }

        private static void Aplicar(Control controle, Tela estado, float fator)
        {
            Original o;

            if (estado.Controles.TryGetValue(controle, out o))
            {
                controle.SuspendLayout();

                controle.Font = Escalar(o.Fonte, fator);

                bool filhoDeLayout =
                    controle.Parent is TableLayoutPanel ||
                    controle.Parent is FlowLayoutPanel;

                if (!filhoDeLayout)
                {
                    int x = (int)Math.Round(o.Limites.X * fator);
                    int y = (int)Math.Round(o.Limites.Y * fator);
                    int largura = (int)Math.Round(o.Limites.Width * fator);
                    int altura = (int)Math.Round(o.Limites.Height * fator);

                    switch (o.Dock)
                    {
                        case DockStyle.None:
                            // Vale também para controles com AutoSize (como caixas de texto, que só
                            // ajustam a altura): a largura precisa acompanhar a posição, senão se sobrepõem.
                            controle.Bounds = new Rectangle(x, y, largura, altura);
                            break;

                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            controle.Height = altura;
                            break;

                        case DockStyle.Left:
                        case DockStyle.Right:
                            controle.Width = largura;
                            break;
                    }
                }

                DataGridView grade = controle as DataGridView;

                if (grade != null)
                {
                    AplicarNaGrade(grade, o, fator);
                }

                Chart grafico = controle as Chart;

                if (grafico != null)
                {
                    AplicarNoGrafico(grafico, o, fator);
                }
            }

            foreach (Control filho in controle.Controls)
            {
                Aplicar(filho, estado, fator);
            }

            controle.ResumeLayout(false);
        }

        private static void AplicarNaGrade(DataGridView grade, Original o, float fator)
        {
            if (o.FonteLinha != null)
            {
                grade.DefaultCellStyle.Font = Escalar(o.FonteLinha, fator);
            }

            if (o.FonteCabecalho != null)
            {
                grade.ColumnHeadersDefaultCellStyle.Font = Escalar(o.FonteCabecalho, fator);
            }

            if (o.FonteLinhaAlternada != null)
            {
                grade.AlternatingRowsDefaultCellStyle.Font = Escalar(o.FonteLinhaAlternada, fator);
            }

            grade.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grade.ColumnHeadersHeight = Math.Max(18, (int)Math.Round(o.AlturaCabecalho * fator));

            int alturaLinha = Math.Max(16, (int)Math.Round(o.AlturaLinha * fator));

            grade.RowTemplate.Height = alturaLinha;

            foreach (DataGridViewRow linha in grade.Rows)
            {
                linha.Height = alturaLinha;
            }

            if (grade.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.None &&
                o.LarguraColunas != null)
            {
                for (int i = 0; i < grade.Columns.Count && i < o.LarguraColunas.Length; i++)
                {
                    grade.Columns[i].Width =
                        Math.Max(20, (int)Math.Round(o.LarguraColunas[i] * fator));
                }
            }
        }

        private static void AplicarNoGrafico(Chart grafico, Original o, float fator)
        {
            if (o.FontesGrafico == null)
            {
                return;
            }

            Font f;

            foreach (ChartArea area in grafico.ChartAreas)
            {
                if (o.FontesGrafico.TryGetValue("AX" + area.Name, out f)) area.AxisX.LabelStyle.Font = Escalar(f, fator);
                if (o.FontesGrafico.TryGetValue("AY" + area.Name, out f)) area.AxisY.LabelStyle.Font = Escalar(f, fator);
                if (o.FontesGrafico.TryGetValue("TX" + area.Name, out f)) area.AxisX.TitleFont = Escalar(f, fator);
                if (o.FontesGrafico.TryGetValue("TY" + area.Name, out f)) area.AxisY.TitleFont = Escalar(f, fator);
            }

            foreach (Legend legenda in grafico.Legends)
            {
                if (o.FontesGrafico.TryGetValue("L" + legenda.Name, out f)) legenda.Font = Escalar(f, fator);
            }

            foreach (Title titulo in grafico.Titles)
            {
                if (o.FontesGrafico.TryGetValue("T" + titulo.Name, out f)) titulo.Font = Escalar(f, fator);
            }
        }
    }
}

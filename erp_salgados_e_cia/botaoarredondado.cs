using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public class botaoarredondado : Button
    {
        public int raio { get; set; } = 20;

        public botaoarredondado()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseVisualStyleBackColor = false;

            TextAlign = ContentAlignment.MiddleLeft;
            Padding = new Padding(10, 0, 0, 0);

            TextImageRelation = TextImageRelation.ImageBeforeText;
            ImageAlign = ContentAlignment.MiddleLeft;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );
        }

        private TextFormatFlags ObterAlinhamentoTexto()
        {
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    return TextFormatFlags.Left |
                           TextFormatFlags.Top;

                case ContentAlignment.TopCenter:
                    return TextFormatFlags.HorizontalCenter |
                           TextFormatFlags.Top;

                case ContentAlignment.TopRight:
                    return TextFormatFlags.Right |
                           TextFormatFlags.Top;

                case ContentAlignment.MiddleLeft:
                    return TextFormatFlags.Left |
                           TextFormatFlags.VerticalCenter;

                case ContentAlignment.MiddleCenter:
                    return TextFormatFlags.HorizontalCenter |
                           TextFormatFlags.VerticalCenter;

                case ContentAlignment.MiddleRight:
                    return TextFormatFlags.Right |
                           TextFormatFlags.VerticalCenter;

                case ContentAlignment.BottomLeft:
                    return TextFormatFlags.Left |
                           TextFormatFlags.Bottom;

                case ContentAlignment.BottomCenter:
                    return TextFormatFlags.HorizontalCenter |
                           TextFormatFlags.Bottom;

                case ContentAlignment.BottomRight:
                    return TextFormatFlags.Right |
                           TextFormatFlags.Bottom;

                default:
                    return TextFormatFlags.HorizontalCenter |
                           TextFormatFlags.VerticalCenter;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(
             Parent == null
                ? SystemColors.Control
                : Parent.BackColor
);

            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int tamanhoRaio = Math.Min(
                raio,
                Math.Min(Width, Height)
            );

            GraphicsPath caminho = new GraphicsPath();

            caminho.AddArc(
                0,
                0,
                tamanhoRaio,
                tamanhoRaio,
                180,
                90
            );

            caminho.AddArc(
                Width - tamanhoRaio,
                0,
                tamanhoRaio,
                tamanhoRaio,
                270,
                90
            );

            caminho.AddArc(
                Width - tamanhoRaio,
                Height - tamanhoRaio,
                tamanhoRaio,
                tamanhoRaio,
                0,
                90
            );

            caminho.AddArc(
                0,
                Height - tamanhoRaio,
                tamanhoRaio,
                tamanhoRaio,
                90,
                90
            );

            caminho.CloseFigure();

            using (SolidBrush pincel = new SolidBrush(BackColor))
            {
                e.Graphics.FillPath(pincel, caminho);
            }

            int margemEsquerda = Padding.Left;

            if (Image != null)
            {
                int tamanhoIcone = 24;
                int imagemY = (Height - tamanhoIcone) / 2;

                Rectangle areaImagem = new Rectangle(
                    margemEsquerda,
                    imagemY,
                    tamanhoIcone,
                    tamanhoIcone
                );

                e.Graphics.DrawImage(
                    Image,
                    areaImagem
                );

                margemEsquerda += tamanhoIcone + 12;
            }

            Rectangle areaTexto = new Rectangle(
                margemEsquerda,
                Padding.Top,
                Width - margemEsquerda - Padding.Right,
                Height - Padding.Top - Padding.Bottom
            );

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                areaTexto,
                ForeColor,
                ObterAlinhamentoTexto() |
                TextFormatFlags.NoPadding
            );
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
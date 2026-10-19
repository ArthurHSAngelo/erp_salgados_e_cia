using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace erp_salgados_e_cia
{
    public partial class MenuPrincipal : Form
    {
        private void ArredondarBotao(Button botao, int raio)
        {
            GraphicsPath caminho = new GraphicsPath();

            caminho.AddArc(0, 0, raio, raio, 180, 90);
            caminho.AddArc(botao.Width - raio, 0, raio, raio, 270, 90);
            caminho.AddArc(
                botao.Width - raio,
                botao.Height - raio,
                raio,
                raio,
                0,
                90
            );
            caminho.AddArc(
                0,
                botao.Height - raio,
                raio,
                raio,
                90,
                90
            );

            caminho.CloseFigure();
            botao.Region = new Region(caminho);
        }
        private ucprodutos telaprodutos;
        private ucfornecedores telafornecedores;
        private ucunidades telaunidades;
        private ucmovimentoestoque telamovimentoestoque;

        private void AbrirTela(UserControl tela)
        {
            pnltelas.Controls.Clear();

            tela.Dock = DockStyle.Fill;

            pnltelas.Controls.Add(tela);

            tela.BringToFront();
        }
        public MenuPrincipal()
        {
            InitializeComponent();

            ArredondarBotao(bpainel, 20);
            ArredondarBotao(bvendas, 20);
            ArredondarBotao(bcaixa, 20);
            ArredondarBotao(bestoque, 20);
            ArredondarBotao(bcadastros, 20);
            ArredondarBotao(brelatorios, 20);
            ArredondarBotao(busuarios, 20);
        }

        private void bprodutos_Click(object sender, EventArgs e)
        {
            if (telaprodutos == null)
            {
                telaprodutos = new ucprodutos();
            }

            AbrirTela(telaprodutos);
        }

        private void bfornecedores_Click(object sender, EventArgs e)
        {
            if (telafornecedores == null)
            {
                telafornecedores = new ucfornecedores();
            }

            AbrirTela(telafornecedores);
        }

        private void bunidades_Click(object sender, EventArgs e)
        {
            if (telaunidades == null)
            {
                telaunidades = new ucunidades();
            }

            AbrirTela(telaunidades);
        }

        private void bmovimentoestoque_Click(object sender, EventArgs e)
        {
            if (telamovimentoestoque == null)
            {
                telamovimentoestoque = new ucmovimentoestoque();
            }

            telamovimentoestoque.AtualizarCombos();

            AbrirTela(telamovimentoestoque);
        }
    }
}

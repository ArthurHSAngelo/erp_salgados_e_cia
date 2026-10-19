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
        private void SelecionarBotao(Button botaoSelecionado)
        {
            Color corMenu = Color.FromArgb(255, 145, 0);

            Button[] botoes =
            {
        bapainel,
        bavendas,
        bacaixa,
        baestoque,
        bacadastros,
        barelatorios,
        bausuarios
    };

            foreach (Button botao in botoes)
            {
                botao.BackColor = corMenu;
                botao.ForeColor = Color.White;
            }

            botaoSelecionado.BackColor = Color.White;
            botaoSelecionado.ForeColor = corMenu;

            botaoSelecionado.Invalidate();
        }
        private void ArredondarBotao(Button botao, int raio)
        {
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.UseVisualStyleBackColor = false;

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

            ArredondarBotao(bapainel, 20);
            ArredondarBotao(bavendas, 20);
            ArredondarBotao(bacaixa, 20);
            ArredondarBotao(baestoque, 20);
            ArredondarBotao(bacadastros, 20);
            ArredondarBotao(barelatorios, 20);
            ArredondarBotao(bausuarios, 20);

            SelecionarBotao(bapainel);
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

        private void botaoarredondado2_Click(object sender, EventArgs e)
        {
            SelecionarBotao(bavendas);

        }

        private void bapainel_Click(object sender, EventArgs e)
        {
            SelecionarBotao(bapainel);
        }

        private void bacaixa_Click(object sender, EventArgs e)
        {
            SelecionarBotao(bacaixa);
        }

        private void baestoque_Click(object sender, EventArgs e)
        {
            SelecionarBotao(baestoque);
        }

        private void bacadastros_Click(object sender, EventArgs e)
        {
            SelecionarBotao(bacadastros);
        }

        private void barelatorios_Click(object sender, EventArgs e)
        {
            SelecionarBotao(barelatorios);
        }

        private void bausuarios_Click(object sender, EventArgs e)
        {
            SelecionarBotao(bausuarios);
        }
    }
}

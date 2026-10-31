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

        private void FecharSubMenus()
        {
            panelCadastros.Visible = false;
            panelestoque.Visible = false;

            bacadastros.Text = "Cadastros ▼";
            baestoque.Text = "Estoque ▼";
        }
        private void SelecionarBotao(Button botaoSelecionado)
        {
            Color corMenu = Color.DarkOrange;

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

                if (botao is botaoarredondado botaoArredondado)
                {
                    botaoArredondado.CorHover = Color.FromArgb(255, 165, 0);
                }
            }

            botaoSelecionado.BackColor = Color.White;
            botaoSelecionado.ForeColor = corMenu;

            if (botaoSelecionado is botaoarredondado botaoAtivo)
            {
                botaoAtivo.CorHover = Color.FromArgb(245, 245, 245);
            }

            botaoSelecionado.Invalidate();

            bapainel.Image =
                botaoSelecionado == bapainel
                ? Properties.Resources.painel_laranja
                : Properties.Resources.painel_branco1;

            bavendas.Image =
                botaoSelecionado == bavendas
                ? Properties.Resources.vendas_laranja
                : Properties.Resources.vendas_branco;

            bacaixa.Image =
                botaoSelecionado == bacaixa
                ? Properties.Resources.caixa_laranja
                : Properties.Resources.caixa_branco;

            baestoque.Image =
                botaoSelecionado == baestoque
                ? Properties.Resources.estoque_laranja
                : Properties.Resources.estoque_branco;

            bacadastros.Image =
                botaoSelecionado == bacadastros
                ? Properties.Resources.cadastros_laranja
                : Properties.Resources.cadastros_branco;

            barelatorios.Image =
                botaoSelecionado == barelatorios
                ? Properties.Resources.relatorios_laranja
                : Properties.Resources.relatorios_branco;

            bausuarios.Image =
                botaoSelecionado == bausuarios
                ? Properties.Resources.usuarios_laranja
                : Properties.Resources.usuarios_branco;

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
        private ucvendas telavendas;

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

            bapainel.CorHover = Color.FromArgb(255, 165, 0);
            bavendas.CorHover = Color.FromArgb(255, 165, 0);
            bacaixa.CorHover = Color.FromArgb(255, 165, 0);
            baestoque.CorHover = Color.FromArgb(255, 165, 0);
            bacadastros.CorHover = Color.FromArgb(255, 165, 0);
            barelatorios.CorHover = Color.FromArgb(255, 165, 0);
            bausuarios.CorHover = Color.FromArgb(255, 165, 0);

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
            FecharSubMenus();

            if (telaprodutos == null)
            {
                telaprodutos = new ucprodutos();
            }

            AbrirTela(telaprodutos);
        }

        private void bfornecedores_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telafornecedores == null)
            {
                telafornecedores = new ucfornecedores();
            }

            AbrirTela(telafornecedores);
        }

        private void bunidades_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telaunidades == null)
            {
                telaunidades = new ucunidades();
            }

            AbrirTela(telaunidades);
        }

        private void bmovimentoestoque_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telamovimentoestoque == null)
            {
                telamovimentoestoque = new ucmovimentoestoque();
            }

            telamovimentoestoque.AtualizarCombos();

            AbrirTela(telamovimentoestoque);
        }

        private void botaoarredondado2_Click(
     object sender,
     EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(bavendas);

            if (telavendas == null)
            {
                telavendas = new ucvendas();
            }

            AbrirTela(telavendas);
        }

        private void bapainel_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(bapainel);
        }

        private void bacaixa_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(bacaixa);
        }

        private void baestoque_Click(object sender, EventArgs e)
        {
            SelecionarBotao(baestoque);

            bool abrir = !panelestoque.Visible;

            FecharSubMenus();

            panelestoque.Visible = abrir;

            if (abrir)
            {
                baestoque.Text = "Estoque ▲";
            }
        }

        private void bacadastros_Click(object sender, EventArgs e)
        {
            SelecionarBotao(bacadastros);

            bool abrir = !panelCadastros.Visible;

            FecharSubMenus();

            panelCadastros.Visible = abrir;

            if (abrir)
            {
                bacadastros.Text = "Cadastros ▲";
            }
        }

        private void barelatorios_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(barelatorios);
        }

        private void bausuarios_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(bausuarios);
        }
    }
}

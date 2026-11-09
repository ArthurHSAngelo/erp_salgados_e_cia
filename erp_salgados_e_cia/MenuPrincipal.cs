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
            panelrelatorios.Visible = false;
            pvendas.Visible = false; 

            bacadastros.Text = "Cadastros ▼";
            baestoque.Text = "Estoque ▼";
            barelatorios.Text = "Relatórios ▼";
            bavendas.Text = "Vendas ▼";
        }
        private void SelecionarBotao(Button botaoSelecionado)
        {
            Color corMenu = Color.DarkOrange;

            Button[] botoes =
            {
                bapainel,
                bavendas,
                baclientes,
                bacaixa,
                baestoque,
                bacadastros,
                barelatorios,
                bausuarios,
                balog
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

            baclientes.Image =
                botaoSelecionado == baclientes
                ? Properties.Resources.clientes_laranja
                : Properties.Resources.clientes_branco;

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

            balog.Image =
                botaoSelecionado == balog
                ? Properties.Resources.log_laranja
                : Properties.Resources.log_branco;
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
        private ucclientes telaclientes;
        private ucmovimentoestoque telamovimentoestoque;
        private ucvendas telavendas;
        private uchistoricovendas telahistoricovendas;
        private uccaixa telacaixa;
        private ucestoquecompleto telaestoquecompleto;
        private ucmovimentacoes telamovimentacoes;
        private ucrelatoriovendas telarelatoriovendas;
        private ucfuncionarios telafuncionarios;
        private ucperdas telaperdas;
        private ucinventario telainventario;
        private ucusuarios telausuarios;
        private uclogoperacoes telalogoperacoes;

        private int alturaPanelRelatorios;

        public bool TrocarUsuario { get; private set; }

        private void AplicarPerfil()
        {
            bool atendente =
                dadossistema.LoginEhDeAtendente;

            bacadastros.Visible = !atendente;
            bausuarios.Visible = !atendente;
            balog.Visible = !atendente;

            bmovimentacoes.Visible = !atendente;
            brelatoriovendas.Visible = !atendente;

            panelrelatorios.Height =
                atendente
                ? 51
                : alturaPanelRelatorios;

            bperfil.Text =
                dadossistema.UsuarioLogado == null
                ? "Usuário ▲"
                : $"{dadossistema.UsuarioLogado.Login} ▲";
        }

        private void ReiniciarTelas()
        {
            telavendas = null;
            telacaixa = null;
            telahistoricovendas = null;
            telamovimentoestoque = null;
            telaestoquecompleto = null;
            telamovimentacoes = null;
            telarelatoriovendas = null;
            telaperdas = null;
            telainventario = null;

            pnltelas.Controls.Clear();

            FecharSubMenus();

            SelecionarBotao(bapainel);
        }

        private void TrocarUnidadeTrabalho(string nomeUnidade)
        {
            if (nomeUnidade == dadossistema.UnidadeAtual)
            {
                return;
            }

            if (dadossistema.comandas.Count > 0)
            {
                MessageBox.Show(
                    "Não é possível trocar de unidade com comandas abertas. Finalize ou cancele as comandas antes.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            dadossistema.UnidadeAtual = nomeUnidade;

            ReiniciarTelas();
        }

        private void SairDaConta()
        {
            DialogResult resposta = MessageBox.Show(
                "Deseja sair da conta?",
                "Sair",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            dadossistema.Sair();

            TrocarUsuario = true;

            Close();
        }

        private void bperfil_Click(object sender, EventArgs e)
        {
            Usuario usuario = dadossistema.UsuarioLogado;

            if (usuario == null)
            {
                return;
            }

            tsmusuario.Text =
                $"{usuario.Login} · {usuario.Perfil}";

            bool atendente =
                dadossistema.LoginEhDeAtendente;

            tsmunidadeatendente.Visible = atendente;
            tsmunidade.Visible = !atendente;

            if (atendente)
            {
                tsmunidadeatendente.Text =
                    $"Unidade: {dadossistema.UnidadeAtual}";
            }
            else
            {
                tsmunidade.DropDownItems.Clear();

                foreach (Unidade unidade in
                    dadossistema.unidades
                    .Where(u =>
                        u.Status == dadossistema.StatusCadastro.Ativo)
                    .OrderBy(u => u.Nome))
                {
                    string nomeUnidade = unidade.Nome;

                    ToolStripMenuItem opcaoUnidade =
                        new ToolStripMenuItem(nomeUnidade);

                    opcaoUnidade.Checked =
                        nomeUnidade == dadossistema.UnidadeAtual;

                    opcaoUnidade.Click += (origem, argumentos) =>
                        TrocarUnidadeTrabalho(nomeUnidade);

                    tsmunidade.DropDownItems.Add(opcaoUnidade);
                }
            }

            cmsperfil.Show(
                bperfil,
                new Point(0, -cmsperfil.PreferredSize.Height));
        }

        private void tsmsair_Click(object sender, EventArgs e)
        {
            SairDaConta();
        }
        private void AbrirTela(UserControl tela)
        {
            pnltelas.Controls.Clear();

            tela.Dock = DockStyle.Fill;

            pnltelas.Controls.Add(tela);

            tela.BringToFront();
        }
        private void MenuPrincipal_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            if (dadossistema.UsuarioLogado != null)
            {
                dadossistema.Sair();
            }
        }

        public MenuPrincipal()
        {
            InitializeComponent();

            FormClosed += MenuPrincipal_FormClosed;

            bapainel.CorHover = Color.FromArgb(255, 165, 0);
            bavendas.CorHover = Color.FromArgb(255, 165, 0);
            baclientes.CorHover = Color.FromArgb(255, 165, 0);
            bacaixa.CorHover = Color.FromArgb(255, 165, 0);
            baestoque.CorHover = Color.FromArgb(255, 165, 0);
            bacadastros.CorHover = Color.FromArgb(255, 165, 0);
            barelatorios.CorHover = Color.FromArgb(255, 165, 0);
            bausuarios.CorHover = Color.FromArgb(255, 165, 0);

            ArredondarBotao(bapainel, 20);
            ArredondarBotao(bavendas, 20);
            ArredondarBotao(baclientes, 20);
            ArredondarBotao(bacaixa, 20);
            ArredondarBotao(baestoque, 20);
            ArredondarBotao(bacadastros, 20);
            ArredondarBotao(barelatorios, 20);
            ArredondarBotao(bausuarios, 20);
            ArredondarBotao(balog, 20);

            balog.CorHover = Color.FromArgb(255, 165, 0);

            balog.Click += balog_Click;
            ArredondarBotao(bperfil, 20);

            bperfil.CorHover = Color.FromArgb(255, 165, 0);

            bperfil.Click += bperfil_Click;

            tsmsair.Click += tsmsair_Click;

            bfuncionarios.Click += bfuncionarios_Click;

            bperdas.Click += bperdas_Click;

            binventario.Click += binventario_Click;

            alturaPanelRelatorios = panelrelatorios.Height;

            AplicarPerfil();

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

        private void bfuncionarios_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telafuncionarios == null)
            {
                telafuncionarios = new ucfuncionarios();
            }

            telafuncionarios.AtualizarTela();

            AbrirTela(telafuncionarios);
        }

        private void bclientes_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(baclientes);

            if (telaclientes == null)
            {
                telaclientes = new ucclientes();
            }

            telaclientes.AtualizarTabela();

            AbrirTela(telaclientes);
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

        private void binventario_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telainventario == null)
            {
                telainventario = new ucinventario();
            }

            telainventario.AtualizarTela();

            AbrirTela(telainventario);
        }

        private void bperdas_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telaperdas == null)
            {
                telaperdas = new ucperdas();
            }

            telaperdas.AtualizarTela();

            AbrirTela(telaperdas);
        }

        private void bavendas_Click(object sender, EventArgs e)
        {
            SelecionarBotao(bavendas);

            if (telavendas == null)
            {
                telavendas = new ucvendas();
            }

            telavendas.AtualizarCombos();

            AbrirTela(telavendas);

            bool abrir = !pvendas.Visible;

            FecharSubMenus();

            pvendas.Visible = abrir;

            if (abrir)
            {
                bavendas.Text = "Vendas ▲";
            }
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

            if (telacaixa == null)
            {
                telacaixa = new uccaixa();
            }

            telacaixa.AtualizarTela();

            AbrirTela(telacaixa);
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
            SelecionarBotao(barelatorios);

            bool abrir = !panelrelatorios.Visible;

            FecharSubMenus();

            panelrelatorios.Visible = abrir;

            if (abrir)
            {
                barelatorios.Text = "Relatórios ▲";
            }
        }

        private void bestoquecompleto_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telaestoquecompleto == null)
            {
                telaestoquecompleto = new ucestoquecompleto();
            }

            telaestoquecompleto.AtualizarTela();

            AbrirTela(telaestoquecompleto);
        }

        private void bmovimentacoes_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telamovimentacoes == null)
            {
                telamovimentacoes = new ucmovimentacoes();
            }

            telamovimentacoes.AtualizarTela();

            AbrirTela(telamovimentacoes);
        }

        private void brelatoriovendas_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telarelatoriovendas == null)
            {
                telarelatoriovendas = new ucrelatoriovendas();
            }

            telarelatoriovendas.AtualizarTela();

            AbrirTela(telarelatoriovendas);
        }

        private void balog_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(balog);

            if (telalogoperacoes == null)
            {
                telalogoperacoes = new uclogoperacoes();
            }

            telalogoperacoes.AtualizarTela();

            AbrirTela(telalogoperacoes);
        }

        private void bausuarios_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            SelecionarBotao(bausuarios);

            if (telausuarios == null)
            {
                telausuarios = new ucusuarios();
            }

            telausuarios.AtualizarTela();

            AbrirTela(telausuarios);
        }

        private void bhistoricovendas_Click(object sender, EventArgs e)
        {
            FecharSubMenus();

            if (telahistoricovendas == null)
            {
                telahistoricovendas = new uchistoricovendas();
            }

            telahistoricovendas.AtualizarTabela();

            AbrirTela(telahistoricovendas);
        }
    }
}

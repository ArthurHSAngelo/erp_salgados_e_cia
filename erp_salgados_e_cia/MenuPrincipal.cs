using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class MenuPrincipal : Form
    {
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

            AbrirTela(telamovimentoestoque);
        }
    }
}

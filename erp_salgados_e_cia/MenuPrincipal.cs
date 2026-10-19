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
        private void AbrirTela(UserControl tela)
        {
            foreach (Control controle in pnltelas.Controls)
            {
                controle.Dispose();
            }

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
            AbrirTela(new ucprodutos());
        }

        private void bfornecedores_Click(object sender, EventArgs e)
        {
            CadastroFornecedores tela =
                new CadastroFornecedores();

            tela.Show();
        }

        private void bunidades_Click(object sender, EventArgs e)
        {
            CadastroUnidades tela =
                new CadastroUnidades();

            tela.Show();
        }
    }
}

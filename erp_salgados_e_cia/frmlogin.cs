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
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();

            bsairlogin.DialogResult = DialogResult.Cancel;

            bentrar.Click += bentrar_Click;
        }

        private void bentrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tblogin.Text) ||
                tbsenha.Text.Length == 0)
            {
                lmensagemlogin.Text =
                    "Informe o login e a senha.";

                if (string.IsNullOrWhiteSpace(tblogin.Text))
                {
                    tblogin.Focus();
                }
                else
                {
                    tbsenha.Focus();
                }

                return;
            }

            Usuario usuario;
            string mensagem;

            if (!dadossistema.Autenticar(
                    tblogin.Text,
                    tbsenha.Text,
                    out usuario,
                    out mensagem))
            {
                lmensagemlogin.Text = mensagem;

                tbsenha.Clear();
                tbsenha.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}

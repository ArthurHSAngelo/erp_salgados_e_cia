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
        private bool modoAutorizacao;

        public string LoginAutorizador { get; private set; }

        public frmlogin() : this(false)
        {
        }

        public frmlogin(bool autorizacao)
        {
            InitializeComponent();

            modoAutorizacao = autorizacao;

            bsairlogin.DialogResult = DialogResult.Cancel;

            if (modoAutorizacao)
            {
                Text = "Autorização do Gerente";
                llogintitulo.Text = "Autorização do Gerente";
                bentrar.Text = "Autorizar";
                bsairlogin.Text = "Voltar";

                lmensagemlogin.Text =
                    "Informe o login e a senha de um Gerente ou do Administrador.";
            }

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

            bool aprovado =
                modoAutorizacao
                ? dadossistema.AutorizarGerente(
                    tblogin.Text,
                    tbsenha.Text,
                    out usuario,
                    out mensagem)
                : dadossistema.Autenticar(
                    tblogin.Text,
                    tbsenha.Text,
                    out usuario,
                    out mensagem);

            if (!aprovado)
            {
                lmensagemlogin.Text = mensagem;

                tbsenha.Clear();
                tbsenha.Focus();
                return;
            }

            LoginAutorizador = usuario.Login;

            DialogResult = DialogResult.OK;
        }
    }
}

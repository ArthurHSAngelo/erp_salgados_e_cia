using System;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo cultura =
                new System.Globalization.CultureInfo("pt-BR");

            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultura;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultura;
            System.Threading.Thread.CurrentThread.CurrentCulture = cultura;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultura;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Erro vindo do banco que a tela não tratou (ex.: MySQL desligado):
            // mostra a mensagem em vez de derrubar o programa.
            Application.SetUnhandledExceptionMode(
                UnhandledExceptionMode.CatchException);

            Application.ThreadException += (remetente, evento) =>
            {
                MessageBox.Show(
                    evento.Exception is Domain.Exceptions.RegraNegocioException
                        ? evento.Exception.Message
                        : "Ocorreu um erro inesperado: " + evento.Exception.Message,
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            };

            while (true)
            {
                using (frmlogin login = new frmlogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }

                MenuPrincipal menu = new MenuPrincipal();

                Application.Run(menu);

                if (!menu.TrocarUsuario)
                {
                    return;
                }
            }
        }
    }
}

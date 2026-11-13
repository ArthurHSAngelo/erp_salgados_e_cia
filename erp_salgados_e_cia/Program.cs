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

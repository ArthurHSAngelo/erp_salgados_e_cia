using System;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (frmlogin login = new frmlogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            Application.Run(new MenuPrincipal());
        }
    }
}

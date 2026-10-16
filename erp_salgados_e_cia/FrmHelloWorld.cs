using System;
using System.Drawing;
using System.Windows.Forms;
using erp_salgados_e_cia.Data;
using erp_salgados_e_cia.Domain;

namespace erp_salgados_e_cia
{
    public class FrmHelloWorld : Form
    {
        public FrmHelloWorld()
        {
            Text = InfoDominio.NomeSistema + " - Hello World";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            ClientSize = new Size(560, 230);

            var titulo = new Label
            {
                Text = "Hello World!",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 16)
            };

            var camadas = new Label
            {
                Text = "UI: camada de apresentação carregada." + Environment.NewLine +
                       InfoDominio.Mensagem() + Environment.NewLine +
                       InfoDados.Mensagem(),
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                UseMnemonic = false,    
                Location = new Point(24, 78)
            };

            var fechar = new Button
            {
                Text = "Fechar",
                Size = new Size(100, 32),
                Location = new Point(440, 182)
            };
            fechar.Click += (sender, e) => Close();

            Controls.Add(titulo);
            Controls.Add(camadas);
            Controls.Add(fechar);
            AcceptButton = fechar;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(640, 413);
            this.Name = "FrmHelloWorld";
            this.Load += new System.EventHandler(this.FrmHelloWorld_Load);
            this.ResumeLayout(false);

        }

        private void FrmHelloWorld_Load(object sender, EventArgs e)
        {

        }
    }
}

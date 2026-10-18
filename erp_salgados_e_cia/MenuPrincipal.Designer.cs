namespace erp_salgados_e_cia
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bprodutos = new System.Windows.Forms.Button();
            this.bfornecedores = new System.Windows.Forms.Button();
            this.bunidades = new System.Windows.Forms.Button();
            this.pnltelas = new System.Windows.Forms.Panel();
            this.pnlmenu = new System.Windows.Forms.Panel();
            this.pnlmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bprodutos
            // 
            this.bprodutos.Location = new System.Drawing.Point(9, 31);
            this.bprodutos.Name = "bprodutos";
            this.bprodutos.Size = new System.Drawing.Size(113, 23);
            this.bprodutos.TabIndex = 0;
            this.bprodutos.Text = "Produtos";
            this.bprodutos.UseVisualStyleBackColor = true;
            this.bprodutos.Click += new System.EventHandler(this.bprodutos_Click);
            // 
            // bfornecedores
            // 
            this.bfornecedores.Location = new System.Drawing.Point(9, 85);
            this.bfornecedores.Name = "bfornecedores";
            this.bfornecedores.Size = new System.Drawing.Size(113, 23);
            this.bfornecedores.TabIndex = 1;
            this.bfornecedores.Text = "Fornecedores";
            this.bfornecedores.UseVisualStyleBackColor = true;
            this.bfornecedores.Click += new System.EventHandler(this.bfornecedores_Click);
            // 
            // bunidades
            // 
            this.bunidades.Location = new System.Drawing.Point(9, 138);
            this.bunidades.Name = "bunidades";
            this.bunidades.Size = new System.Drawing.Size(113, 23);
            this.bunidades.TabIndex = 2;
            this.bunidades.Text = "Unidades";
            this.bunidades.UseVisualStyleBackColor = true;
            this.bunidades.Click += new System.EventHandler(this.bunidades_Click);
            // 
            // pnltelas
            // 
            this.pnltelas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltelas.Location = new System.Drawing.Point(170, 0);
            this.pnltelas.Name = "pnltelas";
            this.pnltelas.Size = new System.Drawing.Size(2245, 1119);
            this.pnltelas.TabIndex = 3;
            // 
            // pnlmenu
            // 
            this.pnlmenu.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlmenu.Controls.Add(this.bprodutos);
            this.pnlmenu.Controls.Add(this.bfornecedores);
            this.pnlmenu.Controls.Add(this.bunidades);
            this.pnlmenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlmenu.Location = new System.Drawing.Point(0, 0);
            this.pnlmenu.Name = "pnlmenu";
            this.pnlmenu.Size = new System.Drawing.Size(170, 1119);
            this.pnlmenu.TabIndex = 4;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2415, 1119);
            this.Controls.Add(this.pnltelas);
            this.Controls.Add(this.pnlmenu);
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlmenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bprodutos;
        private System.Windows.Forms.Button bfornecedores;
        private System.Windows.Forms.Button bunidades;
        private System.Windows.Forms.Panel pnltelas;
        private System.Windows.Forms.Panel pnlmenu;
    }
}
namespace erp_salgados_e_cia
{
    partial class frmlogin
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
            this.llogintitulo = new System.Windows.Forms.Label();
            this.llogin = new System.Windows.Forms.Label();
            this.lsenha = new System.Windows.Forms.Label();
            this.lmensagemlogin = new System.Windows.Forms.Label();
            this.tblogin = new System.Windows.Forms.TextBox();
            this.tbsenha = new System.Windows.Forms.TextBox();
            this.bentrar = new System.Windows.Forms.Button();
            this.bsairlogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // llogintitulo
            // 
            this.llogintitulo.AutoSize = true;
            this.llogintitulo.Location = new System.Drawing.Point(20, 20);
            this.llogintitulo.Name = "llogintitulo";
            this.llogintitulo.Size = new System.Drawing.Size(123, 16);
            this.llogintitulo.TabIndex = 0;
            this.llogintitulo.Text = "ERP Salgados & Cia";
            // 
            // llogin
            // 
            this.llogin.AutoSize = true;
            this.llogin.Location = new System.Drawing.Point(20, 75);
            this.llogin.Name = "llogin";
            this.llogin.Size = new System.Drawing.Size(43, 16);
            this.llogin.TabIndex = 1;
            this.llogin.Text = "Login:";
            // 
            // lsenha
            // 
            this.lsenha.AutoSize = true;
            this.lsenha.Location = new System.Drawing.Point(20, 140);
            this.lsenha.Name = "lsenha";
            this.lsenha.Size = new System.Drawing.Size(49, 16);
            this.lsenha.TabIndex = 2;
            this.lsenha.Text = "Senha:";
            // 
            // lmensagemlogin
            // 
            this.lmensagemlogin.AutoSize = true;
            this.lmensagemlogin.Location = new System.Drawing.Point(20, 200);
            this.lmensagemlogin.Name = "lmensagemlogin";
            this.lmensagemlogin.Size = new System.Drawing.Size(0, 16);
            this.lmensagemlogin.TabIndex = 3;
            // 
            // tblogin
            // 
            this.tblogin.Location = new System.Drawing.Point(20, 98);
            this.tblogin.Name = "tblogin";
            this.tblogin.Size = new System.Drawing.Size(158, 22);
            this.tblogin.TabIndex = 5;
            // 
            // tbsenha
            // 
            this.tbsenha.Location = new System.Drawing.Point(20, 163);
            this.tbsenha.Name = "tbsenha";
            this.tbsenha.Size = new System.Drawing.Size(158, 22);
            this.tbsenha.TabIndex = 6;
            this.tbsenha.UseSystemPasswordChar = true;
            // 
            // bentrar
            // 
            this.bentrar.Location = new System.Drawing.Point(20, 235);
            this.bentrar.Name = "bentrar";
            this.bentrar.Size = new System.Drawing.Size(75, 23);
            this.bentrar.TabIndex = 7;
            this.bentrar.Text = "Entrar";
            this.bentrar.UseVisualStyleBackColor = true;
            // 
            // bsairlogin
            // 
            this.bsairlogin.Location = new System.Drawing.Point(175, 235);
            this.bsairlogin.Name = "bsairlogin";
            this.bsairlogin.Size = new System.Drawing.Size(75, 23);
            this.bsairlogin.TabIndex = 8;
            this.bsairlogin.Text = "Sair";
            this.bsairlogin.UseVisualStyleBackColor = true;
            // 
            // frmlogin
            // 
            this.AcceptButton = this.bentrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bsairlogin;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bsairlogin);
            this.Controls.Add(this.bentrar);
            this.Controls.Add(this.tbsenha);
            this.Controls.Add(this.tblogin);
            this.Controls.Add(this.lmensagemlogin);
            this.Controls.Add(this.lsenha);
            this.Controls.Add(this.llogin);
            this.Controls.Add(this.llogintitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmlogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salgados & Cia - Entrar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label llogintitulo;
        private System.Windows.Forms.Label llogin;
        private System.Windows.Forms.Label lsenha;
        private System.Windows.Forms.Label lmensagemlogin;
        private System.Windows.Forms.TextBox tblogin;
        private System.Windows.Forms.TextBox tbsenha;
        private System.Windows.Forms.Button bentrar;
        private System.Windows.Forms.Button bsairlogin;
    }
}
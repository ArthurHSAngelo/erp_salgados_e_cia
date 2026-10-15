namespace erp_salgados_e_cia
{
    partial class Form1
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
            this.teste = new System.Windows.Forms.Button();
            this.lteste = new System.Windows.Forms.Label();
            this.bteste = new System.Windows.Forms.Button();
            this.pbteste = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // teste
            // 
            this.teste.Location = new System.Drawing.Point(404, 162);
            this.teste.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teste.Name = "teste";
            this.teste.Size = new System.Drawing.Size(232, 164);
            this.teste.TabIndex = 0;
            this.teste.Text = "Teste de Integridade";
            this.teste.UseVisualStyleBackColor = true;
            // 
            // lteste
            // 
            this.lteste.AutoSize = true;
            this.lteste.Location = new System.Drawing.Point(43, 63);
            this.lteste.Name = "lteste";
            this.lteste.Size = new System.Drawing.Size(124, 16);
            this.lteste.TabIndex = 1;
            this.lteste.Text = "para testar o github:";
            // 
            // bteste
            // 
            this.bteste.Location = new System.Drawing.Point(46, 82);
            this.bteste.Name = "bteste";
            this.bteste.Size = new System.Drawing.Size(172, 36);
            this.bteste.TabIndex = 2;
            this.bteste.Text = "Testando o push";
            this.bteste.UseVisualStyleBackColor = true;
            this.bteste.Click += new System.EventHandler(this.bteste_Click);
            // 
            // pbteste
            // 
            this.pbteste.Location = new System.Drawing.Point(46, 134);
            this.pbteste.Name = "pbteste";
            this.pbteste.Size = new System.Drawing.Size(172, 23);
            this.pbteste.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.pbteste);
            this.Controls.Add(this.bteste);
            this.Controls.Add(this.lteste);
            this.Controls.Add(this.teste);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button teste;
        private System.Windows.Forms.Label lteste;
        private System.Windows.Forms.Button bteste;
        private System.Windows.Forms.ProgressBar pbteste;
    }
}
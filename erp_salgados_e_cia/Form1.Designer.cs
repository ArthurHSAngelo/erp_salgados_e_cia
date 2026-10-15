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
            this.btn_apagar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // teste
            // 
            this.teste.Location = new System.Drawing.Point(244, 157);
            this.teste.Name = "teste";
            this.teste.Size = new System.Drawing.Size(174, 133);
            this.teste.TabIndex = 0;
            this.teste.Text = "Teste de Integridade";
            this.teste.UseVisualStyleBackColor = true;
            // 
            // lteste
            // 
            this.lteste.AutoSize = true;
            this.lteste.Location = new System.Drawing.Point(32, 51);
            this.lteste.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lteste.Name = "lteste";
            this.lteste.Size = new System.Drawing.Size(101, 13);
            this.lteste.TabIndex = 1;
            this.lteste.Text = "para testar o github:";
            // 
            // bteste
            // 
            this.bteste.Location = new System.Drawing.Point(34, 67);
            this.bteste.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bteste.Name = "bteste";
            this.bteste.Size = new System.Drawing.Size(129, 29);
            this.bteste.TabIndex = 2;
            this.bteste.Text = "Testando o push";
            this.bteste.UseVisualStyleBackColor = true;
            this.bteste.Click += new System.EventHandler(this.bteste_Click);
            // 
            // pbteste
            // 
            this.pbteste.Location = new System.Drawing.Point(34, 109);
            this.pbteste.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbteste.Name = "pbteste";
            this.pbteste.Size = new System.Drawing.Size(129, 19);
            this.pbteste.TabIndex = 3;
            // 
            // btn_apagar
            // 
            this.btn_apagar.Location = new System.Drawing.Point(537, 73);
            this.btn_apagar.Name = "btn_apagar";
            this.btn_apagar.Size = new System.Drawing.Size(163, 192);
            this.btn_apagar.TabIndex = 4;
            this.btn_apagar.Text = "Apagar esse carinha depois";
            this.btn_apagar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(430, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Texto do Arthur após ter colocado o negocio na pasta certa";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_apagar);
            this.Controls.Add(this.pbteste);
            this.Controls.Add(this.bteste);
            this.Controls.Add(this.lteste);
            this.Controls.Add(this.teste);
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
        private System.Windows.Forms.Button btn_apagar;
        private System.Windows.Forms.Label label1;
    }
}
namespace erp_salgados_e_cia
{
    partial class EntradaEstoque
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
            this.lproduto = new System.Windows.Forms.Label();
            this.cbproduto = new System.Windows.Forms.ComboBox();
            this.lquantidade = new System.Windows.Forms.Label();
            this.nudquantidade = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpdatarecebimento = new System.Windows.Forms.DateTimePicker();
            this.lobs = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bregistrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            this.SuspendLayout();
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Location = new System.Drawing.Point(51, 43);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(57, 16);
            this.lproduto.TabIndex = 0;
            this.lproduto.Text = "Produto:";
            // 
            // cbproduto
            // 
            this.cbproduto.FormattingEnabled = true;
            this.cbproduto.Location = new System.Drawing.Point(125, 43);
            this.cbproduto.Name = "cbproduto";
            this.cbproduto.Size = new System.Drawing.Size(121, 24);
            this.cbproduto.TabIndex = 1;
            // 
            // lquantidade
            // 
            this.lquantidade.AutoSize = true;
            this.lquantidade.Location = new System.Drawing.Point(51, 91);
            this.lquantidade.Name = "lquantidade";
            this.lquantidade.Size = new System.Drawing.Size(80, 16);
            this.lquantidade.TabIndex = 2;
            this.lquantidade.Text = "Quantidade:";
            // 
            // nudquantidade
            // 
            this.nudquantidade.Location = new System.Drawing.Point(137, 89);
            this.nudquantidade.Name = "nudquantidade";
            this.nudquantidade.Size = new System.Drawing.Size(120, 22);
            this.nudquantidade.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data:";
            // 
            // dtpdatarecebimento
            // 
            this.dtpdatarecebimento.Location = new System.Drawing.Point(125, 132);
            this.dtpdatarecebimento.Name = "dtpdatarecebimento";
            this.dtpdatarecebimento.Size = new System.Drawing.Size(200, 22);
            this.dtpdatarecebimento.TabIndex = 5;
            // 
            // lobs
            // 
            this.lobs.AutoSize = true;
            this.lobs.Location = new System.Drawing.Point(51, 181);
            this.lobs.Name = "lobs";
            this.lobs.Size = new System.Drawing.Size(85, 16);
            this.lobs.TabIndex = 6;
            this.lobs.Text = "Observação:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 181);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 7;
            // 
            // bregistrar
            // 
            this.bregistrar.Location = new System.Drawing.Point(125, 225);
            this.bregistrar.Name = "bregistrar";
            this.bregistrar.Size = new System.Drawing.Size(158, 38);
            this.bregistrar.TabIndex = 8;
            this.bregistrar.Text = "Resgistrar Entrada";
            this.bregistrar.UseVisualStyleBackColor = true;
            // 
            // EntradaEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bregistrar);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lobs);
            this.Controls.Add(this.dtpdatarecebimento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudquantidade);
            this.Controls.Add(this.lquantidade);
            this.Controls.Add(this.cbproduto);
            this.Controls.Add(this.lproduto);
            this.Name = "EntradaEstoque";
            this.Text = "EntradaEstoque";
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lproduto;
        private System.Windows.Forms.ComboBox cbproduto;
        private System.Windows.Forms.Label lquantidade;
        private System.Windows.Forms.NumericUpDown nudquantidade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpdatarecebimento;
        private System.Windows.Forms.Label lobs;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bregistrar;
    }
}
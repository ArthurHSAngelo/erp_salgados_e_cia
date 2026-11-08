namespace erp_salgados_e_cia
{
    partial class frmcancelarvenda
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
            this.lcancelamentovenda = new System.Windows.Forms.Label();
            this.lcancelamentototal = new System.Windows.Forms.Label();
            this.lmotivo = new System.Windows.Forms.Label();
            this.tbmotivo = new System.Windows.Forms.TextBox();
            this.laviso = new System.Windows.Forms.Label();
            this.bconfirmarcancelamento = new System.Windows.Forms.Button();
            this.bvoltar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lcancelamentovenda
            // 
            this.lcancelamentovenda.AutoSize = true;
            this.lcancelamentovenda.Location = new System.Drawing.Point(20, 15);
            this.lcancelamentovenda.Name = "lcancelamentovenda";
            this.lcancelamentovenda.Size = new System.Drawing.Size(72, 16);
            this.lcancelamentovenda.TabIndex = 0;
            this.lcancelamentovenda.Text = "Venda nº 0";
            // 
            // lcancelamentototal
            // 
            this.lcancelamentototal.AutoSize = true;
            this.lcancelamentototal.Location = new System.Drawing.Point(20, 45);
            this.lcancelamentototal.Name = "lcancelamentototal";
            this.lcancelamentototal.Size = new System.Drawing.Size(88, 16);
            this.lcancelamentototal.TabIndex = 1;
            this.lcancelamentototal.Text = "Total: R$ 0,00";
            // 
            // lmotivo
            // 
            this.lmotivo.AutoSize = true;
            this.lmotivo.Location = new System.Drawing.Point(20, 85);
            this.lmotivo.Name = "lmotivo";
            this.lmotivo.Size = new System.Drawing.Size(157, 16);
            this.lmotivo.TabIndex = 2;
            this.lmotivo.Text = "Motivo do cancelamento:";
            // 
            // tbmotivo
            // 
            this.tbmotivo.Location = new System.Drawing.Point(20, 110);
            this.tbmotivo.MaxLength = 300;
            this.tbmotivo.Multiline = true;
            this.tbmotivo.Name = "tbmotivo";
            this.tbmotivo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbmotivo.Size = new System.Drawing.Size(380, 90);
            this.tbmotivo.TabIndex = 3;
            // 
            // laviso
            // 
            this.laviso.AutoSize = true;
            this.laviso.Location = new System.Drawing.Point(20, 215);
            this.laviso.Name = "laviso";
            this.laviso.Size = new System.Drawing.Size(407, 16);
            this.laviso.TabIndex = 4;
            this.laviso.Text = "Os produtos voltarão ao estoque. Esta ação não pode ser desfeita.\t";
            // 
            // bconfirmarcancelamento
            // 
            this.bconfirmarcancelamento.Location = new System.Drawing.Point(215, 245);
            this.bconfirmarcancelamento.Name = "bconfirmarcancelamento";
            this.bconfirmarcancelamento.Size = new System.Drawing.Size(185, 30);
            this.bconfirmarcancelamento.TabIndex = 5;
            this.bconfirmarcancelamento.Text = "Confirmar cancelamento";
            this.bconfirmarcancelamento.UseVisualStyleBackColor = true;
            // 
            // bvoltar
            // 
            this.bvoltar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bvoltar.Location = new System.Drawing.Point(110, 245);
            this.bvoltar.Name = "bvoltar";
            this.bvoltar.Size = new System.Drawing.Size(90, 30);
            this.bvoltar.TabIndex = 6;
            this.bvoltar.Text = "Voltar";
            this.bvoltar.UseVisualStyleBackColor = true;
            // 
            // frmcancelarvenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bvoltar;
            this.ClientSize = new System.Drawing.Size(442, 297);
            this.Controls.Add(this.bvoltar);
            this.Controls.Add(this.bconfirmarcancelamento);
            this.Controls.Add(this.laviso);
            this.Controls.Add(this.tbmotivo);
            this.Controls.Add(this.lmotivo);
            this.Controls.Add(this.lcancelamentototal);
            this.Controls.Add(this.lcancelamentovenda);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmcancelarvenda";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cancelar venda";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lcancelamentovenda;
        private System.Windows.Forms.Label lcancelamentototal;
        private System.Windows.Forms.Label lmotivo;
        private System.Windows.Forms.TextBox tbmotivo;
        private System.Windows.Forms.Label laviso;
        private System.Windows.Forms.Button bconfirmarcancelamento;
        private System.Windows.Forms.Button bvoltar;
    }
}
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
            this.lcancelamentovenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lcancelamentovenda.Location = new System.Drawing.Point(8, 9);
            this.lcancelamentovenda.Name = "lcancelamentovenda";
            this.lcancelamentovenda.Size = new System.Drawing.Size(92, 21);
            this.lcancelamentovenda.TabIndex = 0;
            this.lcancelamentovenda.Text = "Venda nº 0";
            // 
            // lcancelamentototal
            // 
            this.lcancelamentototal.AutoSize = true;
            this.lcancelamentototal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lcancelamentototal.Location = new System.Drawing.Point(8, 58);
            this.lcancelamentototal.Name = "lcancelamentototal";
            this.lcancelamentototal.Size = new System.Drawing.Size(110, 21);
            this.lcancelamentototal.TabIndex = 1;
            this.lcancelamentototal.Text = "Total: R$ 0,00";
            // 
            // lmotivo
            // 
            this.lmotivo.AutoSize = true;
            this.lmotivo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lmotivo.Location = new System.Drawing.Point(12, 120);
            this.lmotivo.Name = "lmotivo";
            this.lmotivo.Size = new System.Drawing.Size(205, 21);
            this.lmotivo.TabIndex = 2;
            this.lmotivo.Text = "Motivo do cancelamento:";
            // 
            // tbmotivo
            // 
            this.tbmotivo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbmotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbmotivo.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbmotivo.Location = new System.Drawing.Point(12, 144);
            this.tbmotivo.MaxLength = 300;
            this.tbmotivo.Multiline = true;
            this.tbmotivo.Name = "tbmotivo";
            this.tbmotivo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbmotivo.Size = new System.Drawing.Size(663, 123);
            this.tbmotivo.TabIndex = 3;
            // 
            // laviso
            // 
            this.laviso.AutoSize = true;
            this.laviso.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.laviso.ForeColor = System.Drawing.Color.Firebrick;
            this.laviso.Location = new System.Drawing.Point(8, 298);
            this.laviso.Name = "laviso";
            this.laviso.Size = new System.Drawing.Size(508, 21);
            this.laviso.TabIndex = 4;
            this.laviso.Text = "Os produtos voltarão ao estoque. Esta ação não pode ser desfeita.\t";
            // 
            // bconfirmarcancelamento
            // 
            this.bconfirmarcancelamento.BackColor = System.Drawing.Color.White;
            this.bconfirmarcancelamento.FlatAppearance.BorderSize = 2;
            this.bconfirmarcancelamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bconfirmarcancelamento.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bconfirmarcancelamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bconfirmarcancelamento.Location = new System.Drawing.Point(466, 434);
            this.bconfirmarcancelamento.Name = "bconfirmarcancelamento";
            this.bconfirmarcancelamento.Size = new System.Drawing.Size(209, 42);
            this.bconfirmarcancelamento.TabIndex = 5;
            this.bconfirmarcancelamento.Text = "Confirmar cancelamento";
            this.bconfirmarcancelamento.UseVisualStyleBackColor = false;
            // 
            // bvoltar
            // 
            this.bvoltar.BackColor = System.Drawing.Color.White;
            this.bvoltar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bvoltar.FlatAppearance.BorderSize = 2;
            this.bvoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bvoltar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bvoltar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bvoltar.Location = new System.Drawing.Point(309, 434);
            this.bvoltar.Name = "bvoltar";
            this.bvoltar.Size = new System.Drawing.Size(127, 42);
            this.bvoltar.TabIndex = 6;
            this.bvoltar.Text = "Voltar";
            this.bvoltar.UseVisualStyleBackColor = false;
            // 
            // frmcancelarvenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.CancelButton = this.bvoltar;
            this.ClientSize = new System.Drawing.Size(687, 485);
            this.Controls.Add(this.bvoltar);
            this.Controls.Add(this.bconfirmarcancelamento);
            this.Controls.Add(this.laviso);
            this.Controls.Add(this.tbmotivo);
            this.Controls.Add(this.lmotivo);
            this.Controls.Add(this.lcancelamentototal);
            this.Controls.Add(this.lcancelamentovenda);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
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
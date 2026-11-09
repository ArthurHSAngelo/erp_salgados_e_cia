namespace erp_salgados_e_cia
{
    partial class ucperdas
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lprodutoperda = new System.Windows.Forms.Label();
            this.cbprodutoperda = new System.Windows.Forms.ComboBox();
            this.lsaldoperda = new System.Windows.Forms.Label();
            this.lquantidadeperda = new System.Windows.Forms.Label();
            this.nudquantidadeperda = new System.Windows.Forms.NumericUpDown();
            this.lmotivoperda = new System.Windows.Forms.Label();
            this.cbmotivoperda = new System.Windows.Forms.ComboBox();
            this.ldataperda = new System.Windows.Forms.Label();
            this.dtpdataperda = new System.Windows.Forms.DateTimePicker();
            this.lresponsavelperda = new System.Windows.Forms.Label();
            this.cbresponsavelperda = new System.Windows.Forms.ComboBox();
            this.bregistrarperda = new System.Windows.Forms.Button();
            this.lperdasrecentes = new System.Windows.Forms.Label();
            this.dgvperdas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidadeperda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvperdas)).BeginInit();
            this.SuspendLayout();
            // 
            // lprodutoperda
            // 
            this.lprodutoperda.AutoSize = true;
            this.lprodutoperda.Location = new System.Drawing.Point(785, 84);
            this.lprodutoperda.Name = "lprodutoperda";
            this.lprodutoperda.Size = new System.Drawing.Size(57, 16);
            this.lprodutoperda.TabIndex = 2;
            this.lprodutoperda.Text = "Produto:";
            // 
            // cbprodutoperda
            // 
            this.cbprodutoperda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbprodutoperda.FormattingEnabled = true;
            this.cbprodutoperda.Location = new System.Drawing.Point(866, 84);
            this.cbprodutoperda.Name = "cbprodutoperda";
            this.cbprodutoperda.Size = new System.Drawing.Size(121, 24);
            this.cbprodutoperda.TabIndex = 7;
            // 
            // lsaldoperda
            // 
            this.lsaldoperda.AutoSize = true;
            this.lsaldoperda.Location = new System.Drawing.Point(785, 128);
            this.lsaldoperda.Name = "lsaldoperda";
            this.lsaldoperda.Size = new System.Drawing.Size(118, 16);
            this.lsaldoperda.TabIndex = 8;
            this.lsaldoperda.Text = "Saldo disponível: -";
            // 
            // lquantidadeperda
            // 
            this.lquantidadeperda.AutoSize = true;
            this.lquantidadeperda.Location = new System.Drawing.Point(785, 177);
            this.lquantidadeperda.Name = "lquantidadeperda";
            this.lquantidadeperda.Size = new System.Drawing.Size(80, 16);
            this.lquantidadeperda.TabIndex = 9;
            this.lquantidadeperda.Text = "Quantidade:";
            // 
            // nudquantidadeperda
            // 
            this.nudquantidadeperda.DecimalPlaces = 2;
            this.nudquantidadeperda.Location = new System.Drawing.Point(871, 175);
            this.nudquantidadeperda.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudquantidadeperda.Name = "nudquantidadeperda";
            this.nudquantidadeperda.Size = new System.Drawing.Size(54, 22);
            this.nudquantidadeperda.TabIndex = 10;
            this.nudquantidadeperda.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lmotivoperda
            // 
            this.lmotivoperda.AutoSize = true;
            this.lmotivoperda.Location = new System.Drawing.Point(785, 225);
            this.lmotivoperda.Name = "lmotivoperda";
            this.lmotivoperda.Size = new System.Drawing.Size(50, 16);
            this.lmotivoperda.TabIndex = 11;
            this.lmotivoperda.Text = "Motivo:";
            // 
            // cbmotivoperda
            // 
            this.cbmotivoperda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmotivoperda.FormattingEnabled = true;
            this.cbmotivoperda.Location = new System.Drawing.Point(866, 222);
            this.cbmotivoperda.Name = "cbmotivoperda";
            this.cbmotivoperda.Size = new System.Drawing.Size(121, 24);
            this.cbmotivoperda.TabIndex = 12;
            // 
            // ldataperda
            // 
            this.ldataperda.AutoSize = true;
            this.ldataperda.Location = new System.Drawing.Point(785, 282);
            this.ldataperda.Name = "ldataperda";
            this.ldataperda.Size = new System.Drawing.Size(97, 16);
            this.ldataperda.TabIndex = 13;
            this.ldataperda.Text = "Data da perda:";
            // 
            // dtpdataperda
            // 
            this.dtpdataperda.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdataperda.Location = new System.Drawing.Point(888, 282);
            this.dtpdataperda.Name = "dtpdataperda";
            this.dtpdataperda.Size = new System.Drawing.Size(99, 22);
            this.dtpdataperda.TabIndex = 14;
            // 
            // lresponsavelperda
            // 
            this.lresponsavelperda.AutoSize = true;
            this.lresponsavelperda.Location = new System.Drawing.Point(785, 337);
            this.lresponsavelperda.Name = "lresponsavelperda";
            this.lresponsavelperda.Size = new System.Drawing.Size(91, 16);
            this.lresponsavelperda.TabIndex = 15;
            this.lresponsavelperda.Text = "Responsável:";
            // 
            // cbresponsavelperda
            // 
            this.cbresponsavelperda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbresponsavelperda.FormattingEnabled = true;
            this.cbresponsavelperda.Location = new System.Drawing.Point(888, 334);
            this.cbresponsavelperda.Name = "cbresponsavelperda";
            this.cbresponsavelperda.Size = new System.Drawing.Size(121, 24);
            this.cbresponsavelperda.TabIndex = 16;
            // 
            // bregistrarperda
            // 
            this.bregistrarperda.Location = new System.Drawing.Point(1105, 334);
            this.bregistrarperda.Name = "bregistrarperda";
            this.bregistrarperda.Size = new System.Drawing.Size(125, 24);
            this.bregistrarperda.TabIndex = 19;
            this.bregistrarperda.Text = "Registrar perda";
            this.bregistrarperda.UseVisualStyleBackColor = true;
            // 
            // lperdasrecentes
            // 
            this.lperdasrecentes.AutoSize = true;
            this.lperdasrecentes.Location = new System.Drawing.Point(800, 438);
            this.lperdasrecentes.Name = "lperdasrecentes";
            this.lperdasrecentes.Size = new System.Drawing.Size(171, 16);
            this.lperdasrecentes.TabIndex = 20;
            this.lperdasrecentes.Text = "Perdas dos últimos 30 dias:";
            // 
            // dgvperdas
            // 
            this.dgvperdas.AllowUserToAddRows = false;
            this.dgvperdas.AllowUserToDeleteRows = false;
            this.dgvperdas.AllowUserToResizeColumns = false;
            this.dgvperdas.AllowUserToResizeRows = false;
            this.dgvperdas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvperdas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvperdas.Location = new System.Drawing.Point(803, 500);
            this.dgvperdas.MultiSelect = false;
            this.dgvperdas.Name = "dgvperdas";
            this.dgvperdas.ReadOnly = true;
            this.dgvperdas.RowHeadersVisible = false;
            this.dgvperdas.RowHeadersWidth = 51;
            this.dgvperdas.RowTemplate.Height = 24;
            this.dgvperdas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvperdas.Size = new System.Drawing.Size(726, 665);
            this.dgvperdas.TabIndex = 22;
            // 
            // ucperdas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvperdas);
            this.Controls.Add(this.lperdasrecentes);
            this.Controls.Add(this.bregistrarperda);
            this.Controls.Add(this.cbresponsavelperda);
            this.Controls.Add(this.lresponsavelperda);
            this.Controls.Add(this.dtpdataperda);
            this.Controls.Add(this.ldataperda);
            this.Controls.Add(this.cbmotivoperda);
            this.Controls.Add(this.lmotivoperda);
            this.Controls.Add(this.nudquantidadeperda);
            this.Controls.Add(this.lquantidadeperda);
            this.Controls.Add(this.lsaldoperda);
            this.Controls.Add(this.cbprodutoperda);
            this.Controls.Add(this.lprodutoperda);
            this.Name = "ucperdas";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidadeperda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvperdas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lprodutoperda;
        private System.Windows.Forms.ComboBox cbprodutoperda;
        private System.Windows.Forms.Label lsaldoperda;
        private System.Windows.Forms.Label lquantidadeperda;
        private System.Windows.Forms.NumericUpDown nudquantidadeperda;
        private System.Windows.Forms.Label lmotivoperda;
        private System.Windows.Forms.ComboBox cbmotivoperda;
        private System.Windows.Forms.Label ldataperda;
        private System.Windows.Forms.DateTimePicker dtpdataperda;
        private System.Windows.Forms.Label lresponsavelperda;
        private System.Windows.Forms.ComboBox cbresponsavelperda;
        private System.Windows.Forms.Button bregistrarperda;
        private System.Windows.Forms.Label lperdasrecentes;
        private System.Windows.Forms.DataGridView dgvperdas;
    }
}

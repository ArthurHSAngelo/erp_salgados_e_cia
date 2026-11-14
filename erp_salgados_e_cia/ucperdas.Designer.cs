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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.pperdas = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidadeperda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvperdas)).BeginInit();
            this.pperdas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lprodutoperda
            // 
            this.lprodutoperda.AutoSize = true;
            this.lprodutoperda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lprodutoperda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lprodutoperda.Location = new System.Drawing.Point(107, 64);
            this.lprodutoperda.Name = "lprodutoperda";
            this.lprodutoperda.Size = new System.Drawing.Size(76, 21);
            this.lprodutoperda.TabIndex = 2;
            this.lprodutoperda.Text = "Produto:";
            // 
            // cbprodutoperda
            // 
            this.cbprodutoperda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbprodutoperda.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbprodutoperda.FormattingEnabled = true;
            this.cbprodutoperda.Location = new System.Drawing.Point(111, 89);
            this.cbprodutoperda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbprodutoperda.Name = "cbprodutoperda";
            this.cbprodutoperda.Size = new System.Drawing.Size(207, 31);
            this.cbprodutoperda.TabIndex = 7;
            // 
            // lsaldoperda
            // 
            this.lsaldoperda.AutoSize = true;
            this.lsaldoperda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lsaldoperda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lsaldoperda.Location = new System.Drawing.Point(1383, 91);
            this.lsaldoperda.Name = "lsaldoperda";
            this.lsaldoperda.Size = new System.Drawing.Size(151, 21);
            this.lsaldoperda.TabIndex = 8;
            this.lsaldoperda.Text = "Saldo disponível: -";
            // 
            // lquantidadeperda
            // 
            this.lquantidadeperda.AutoSize = true;
            this.lquantidadeperda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lquantidadeperda.Location = new System.Drawing.Point(409, 64);
            this.lquantidadeperda.Name = "lquantidadeperda";
            this.lquantidadeperda.Size = new System.Drawing.Size(104, 21);
            this.lquantidadeperda.TabIndex = 9;
            this.lquantidadeperda.Text = "Quantidade:";
            // 
            // nudquantidadeperda
            // 
            this.nudquantidadeperda.DecimalPlaces = 2;
            this.nudquantidadeperda.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.nudquantidadeperda.Location = new System.Drawing.Point(413, 89);
            this.nudquantidadeperda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudquantidadeperda.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudquantidadeperda.Name = "nudquantidadeperda";
            this.nudquantidadeperda.Size = new System.Drawing.Size(108, 30);
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
            this.lmotivoperda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lmotivoperda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lmotivoperda.Location = new System.Drawing.Point(619, 64);
            this.lmotivoperda.Name = "lmotivoperda";
            this.lmotivoperda.Size = new System.Drawing.Size(69, 21);
            this.lmotivoperda.TabIndex = 11;
            this.lmotivoperda.Text = "Motivo:";
            // 
            // cbmotivoperda
            // 
            this.cbmotivoperda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmotivoperda.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbmotivoperda.FormattingEnabled = true;
            this.cbmotivoperda.Location = new System.Drawing.Point(623, 89);
            this.cbmotivoperda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmotivoperda.Name = "cbmotivoperda";
            this.cbmotivoperda.Size = new System.Drawing.Size(207, 31);
            this.cbmotivoperda.TabIndex = 12;
            // 
            // ldataperda
            // 
            this.ldataperda.AutoSize = true;
            this.ldataperda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ldataperda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.ldataperda.Location = new System.Drawing.Point(880, 64);
            this.ldataperda.Name = "ldataperda";
            this.ldataperda.Size = new System.Drawing.Size(121, 21);
            this.ldataperda.TabIndex = 13;
            this.ldataperda.Text = "Data da perda:";
            // 
            // dtpdataperda
            // 
            this.dtpdataperda.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dtpdataperda.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdataperda.Location = new System.Drawing.Point(884, 89);
            this.dtpdataperda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpdataperda.Name = "dtpdataperda";
            this.dtpdataperda.Size = new System.Drawing.Size(173, 30);
            this.dtpdataperda.TabIndex = 14;
            // 
            // lresponsavelperda
            // 
            this.lresponsavelperda.AutoSize = true;
            this.lresponsavelperda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lresponsavelperda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lresponsavelperda.Location = new System.Drawing.Point(1153, 64);
            this.lresponsavelperda.Name = "lresponsavelperda";
            this.lresponsavelperda.Size = new System.Drawing.Size(109, 21);
            this.lresponsavelperda.TabIndex = 15;
            this.lresponsavelperda.Text = "Responsável:";
            // 
            // cbresponsavelperda
            // 
            this.cbresponsavelperda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbresponsavelperda.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbresponsavelperda.FormattingEnabled = true;
            this.cbresponsavelperda.Location = new System.Drawing.Point(1157, 89);
            this.cbresponsavelperda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbresponsavelperda.Name = "cbresponsavelperda";
            this.cbresponsavelperda.Size = new System.Drawing.Size(121, 31);
            this.cbresponsavelperda.TabIndex = 16;
            // 
            // bregistrarperda
            // 
            this.bregistrarperda.FlatAppearance.BorderSize = 2;
            this.bregistrarperda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bregistrarperda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bregistrarperda.ForeColor = System.Drawing.Color.Firebrick;
            this.bregistrarperda.Location = new System.Drawing.Point(1831, 76);
            this.bregistrarperda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bregistrarperda.Name = "bregistrarperda";
            this.bregistrarperda.Size = new System.Drawing.Size(172, 52);
            this.bregistrarperda.TabIndex = 19;
            this.bregistrarperda.Text = "Registrar perda";
            this.bregistrarperda.UseVisualStyleBackColor = true;
            // 
            // lperdasrecentes
            // 
            this.lperdasrecentes.AutoSize = true;
            this.lperdasrecentes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lperdasrecentes.Location = new System.Drawing.Point(31, 255);
            this.lperdasrecentes.Name = "lperdasrecentes";
            this.lperdasrecentes.Size = new System.Drawing.Size(269, 28);
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
            this.dgvperdas.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvperdas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvperdas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvperdas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvperdas.Location = new System.Drawing.Point(36, 286);
            this.dgvperdas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvperdas.MultiSelect = false;
            this.dgvperdas.Name = "dgvperdas";
            this.dgvperdas.ReadOnly = true;
            this.dgvperdas.RowHeadersVisible = false;
            this.dgvperdas.RowHeadersWidth = 51;
            this.dgvperdas.RowTemplate.Height = 24;
            this.dgvperdas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvperdas.Size = new System.Drawing.Size(2371, 993);
            this.dgvperdas.TabIndex = 22;
            // 
            // pperdas
            // 
            this.pperdas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pperdas.Controls.Add(this.nudquantidadeperda);
            this.pperdas.Controls.Add(this.lquantidadeperda);
            this.pperdas.Controls.Add(this.cbresponsavelperda);
            this.pperdas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pperdas.Location = new System.Drawing.Point(0, 0);
            this.pperdas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pperdas.Name = "pperdas";
            this.pperdas.Size = new System.Drawing.Size(2445, 178);
            this.pperdas.TabIndex = 23;
            // 
            // ucperdas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvperdas);
            this.Controls.Add(this.lperdasrecentes);
            this.Controls.Add(this.bregistrarperda);
            this.Controls.Add(this.lresponsavelperda);
            this.Controls.Add(this.dtpdataperda);
            this.Controls.Add(this.ldataperda);
            this.Controls.Add(this.cbmotivoperda);
            this.Controls.Add(this.lmotivoperda);
            this.Controls.Add(this.lsaldoperda);
            this.Controls.Add(this.cbprodutoperda);
            this.Controls.Add(this.lprodutoperda);
            this.Controls.Add(this.pperdas);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucperdas";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidadeperda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvperdas)).EndInit();
            this.pperdas.ResumeLayout(false);
            this.pperdas.PerformLayout();
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
        private System.Windows.Forms.Panel pperdas;
    }
}

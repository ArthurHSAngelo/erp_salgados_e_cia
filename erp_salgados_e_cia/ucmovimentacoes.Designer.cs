namespace erp_salgados_e_cia
{
    partial class ucmovimentacoes
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
            this.lfiltroinicio = new System.Windows.Forms.Label();
            this.dtpfiltroinicio = new System.Windows.Forms.DateTimePicker();
            this.lfiltrounidade = new System.Windows.Forms.Label();
            this.lfiltrofim = new System.Windows.Forms.Label();
            this.dtpfiltrofim = new System.Windows.Forms.DateTimePicker();
            this.cbfiltrounidade = new System.Windows.Forms.ComboBox();
            this.lfiltroproduto = new System.Windows.Forms.Label();
            this.cbfiltroproduto = new System.Windows.Forms.ComboBox();
            this.lfiltrotipo = new System.Windows.Forms.Label();
            this.cbfiltrotipo = new System.Windows.Forms.ComboBox();
            this.bexportarmovimentacoes = new System.Windows.Forms.Button();
            this.lperiodoaplicado = new System.Windows.Forms.Label();
            this.lresultadomovimentacoes = new System.Windows.Forms.Label();
            this.pmovimentacoes = new System.Windows.Forms.Panel();
            this.dgvmovimentacoes = new System.Windows.Forms.DataGridView();
            this.pmovimentacoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).BeginInit();
            this.SuspendLayout();
            // 
            // lfiltroinicio
            // 
            this.lfiltroinicio.AutoSize = true;
            this.lfiltroinicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lfiltroinicio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltroinicio.Location = new System.Drawing.Point(107, 64);
            this.lfiltroinicio.Name = "lfiltroinicio";
            this.lfiltroinicio.Size = new System.Drawing.Size(125, 21);
            this.lfiltroinicio.TabIndex = 0;
            this.lfiltroinicio.Text = "Período inicial:";
            // 
            // dtpfiltroinicio
            // 
            this.dtpfiltroinicio.Checked = false;
            this.dtpfiltroinicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfiltroinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltroinicio.Location = new System.Drawing.Point(111, 88);
            this.dtpfiltroinicio.Name = "dtpfiltroinicio";
            this.dtpfiltroinicio.ShowCheckBox = true;
            this.dtpfiltroinicio.Size = new System.Drawing.Size(140, 27);
            this.dtpfiltroinicio.TabIndex = 1;
            // 
            // lfiltrounidade
            // 
            this.lfiltrounidade.AutoSize = true;
            this.lfiltrounidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrounidade.Location = new System.Drawing.Point(569, 64);
            this.lfiltrounidade.Name = "lfiltrounidade";
            this.lfiltrounidade.Size = new System.Drawing.Size(79, 21);
            this.lfiltrounidade.TabIndex = 2;
            this.lfiltrounidade.Text = "Unidade:";
            // 
            // lfiltrofim
            // 
            this.lfiltrofim.AutoSize = true;
            this.lfiltrofim.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrofim.Location = new System.Drawing.Point(341, 64);
            this.lfiltrofim.Name = "lfiltrofim";
            this.lfiltrofim.Size = new System.Drawing.Size(113, 21);
            this.lfiltrofim.TabIndex = 3;
            this.lfiltrofim.Text = "Período final:";
            // 
            // dtpfiltrofim
            // 
            this.dtpfiltrofim.Checked = false;
            this.dtpfiltrofim.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfiltrofim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltrofim.Location = new System.Drawing.Point(345, 88);
            this.dtpfiltrofim.Name = "dtpfiltrofim";
            this.dtpfiltrofim.ShowCheckBox = true;
            this.dtpfiltrofim.Size = new System.Drawing.Size(140, 27);
            this.dtpfiltrofim.TabIndex = 4;
            // 
            // cbfiltrounidade
            // 
            this.cbfiltrounidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrounidade.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrounidade.FormattingEnabled = true;
            this.cbfiltrounidade.Location = new System.Drawing.Point(573, 88);
            this.cbfiltrounidade.Name = "cbfiltrounidade";
            this.cbfiltrounidade.Size = new System.Drawing.Size(182, 31);
            this.cbfiltrounidade.TabIndex = 5;
            // 
            // lfiltroproduto
            // 
            this.lfiltroproduto.AutoSize = true;
            this.lfiltroproduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltroproduto.Location = new System.Drawing.Point(848, 64);
            this.lfiltroproduto.Name = "lfiltroproduto";
            this.lfiltroproduto.Size = new System.Drawing.Size(76, 21);
            this.lfiltroproduto.TabIndex = 6;
            this.lfiltroproduto.Text = "Produto:";
            // 
            // cbfiltroproduto
            // 
            this.cbfiltroproduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltroproduto.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltroproduto.FormattingEnabled = true;
            this.cbfiltroproduto.Location = new System.Drawing.Point(852, 88);
            this.cbfiltroproduto.Name = "cbfiltroproduto";
            this.cbfiltroproduto.Size = new System.Drawing.Size(263, 31);
            this.cbfiltroproduto.TabIndex = 7;
            // 
            // lfiltrotipo
            // 
            this.lfiltrotipo.AutoSize = true;
            this.lfiltrotipo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrotipo.Location = new System.Drawing.Point(1187, 64);
            this.lfiltrotipo.Name = "lfiltrotipo";
            this.lfiltrotipo.Size = new System.Drawing.Size(48, 21);
            this.lfiltrotipo.TabIndex = 8;
            this.lfiltrotipo.Text = "Tipo:";
            // 
            // cbfiltrotipo
            // 
            this.cbfiltrotipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrotipo.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrotipo.FormattingEnabled = true;
            this.cbfiltrotipo.Location = new System.Drawing.Point(1191, 88);
            this.cbfiltrotipo.Name = "cbfiltrotipo";
            this.cbfiltrotipo.Size = new System.Drawing.Size(121, 31);
            this.cbfiltrotipo.TabIndex = 9;
            // 
            // bexportarmovimentacoes
            // 
            this.bexportarmovimentacoes.FlatAppearance.BorderSize = 2;
            this.bexportarmovimentacoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexportarmovimentacoes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexportarmovimentacoes.Location = new System.Drawing.Point(1635, 76);
            this.bexportarmovimentacoes.Name = "bexportarmovimentacoes";
            this.bexportarmovimentacoes.Size = new System.Drawing.Size(172, 52);
            this.bexportarmovimentacoes.TabIndex = 11;
            this.bexportarmovimentacoes.Text = "Exportar";
            this.bexportarmovimentacoes.UseVisualStyleBackColor = true;
            // 
            // lperiodoaplicado
            // 
            this.lperiodoaplicado.AutoSize = true;
            this.lperiodoaplicado.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lperiodoaplicado.Location = new System.Drawing.Point(66, 225);
            this.lperiodoaplicado.Name = "lperiodoaplicado";
            this.lperiodoaplicado.Size = new System.Drawing.Size(94, 25);
            this.lperiodoaplicado.TabIndex = 12;
            this.lperiodoaplicado.Text = "Período: -";
            // 
            // lresultadomovimentacoes
            // 
            this.lresultadomovimentacoes.AutoSize = true;
            this.lresultadomovimentacoes.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.lresultadomovimentacoes.Location = new System.Drawing.Point(454, 225);
            this.lresultadomovimentacoes.Name = "lresultadomovimentacoes";
            this.lresultadomovimentacoes.Size = new System.Drawing.Size(0, 25);
            this.lresultadomovimentacoes.TabIndex = 13;
            // 
            // pmovimentacoes
            // 
            this.pmovimentacoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pmovimentacoes.Controls.Add(this.dtpfiltroinicio);
            this.pmovimentacoes.Controls.Add(this.lfiltrofim);
            this.pmovimentacoes.Controls.Add(this.dtpfiltrofim);
            this.pmovimentacoes.Controls.Add(this.lfiltrounidade);
            this.pmovimentacoes.Controls.Add(this.cbfiltrotipo);
            this.pmovimentacoes.Controls.Add(this.cbfiltrounidade);
            this.pmovimentacoes.Controls.Add(this.lfiltrotipo);
            this.pmovimentacoes.Controls.Add(this.lfiltroproduto);
            this.pmovimentacoes.Controls.Add(this.cbfiltroproduto);
            this.pmovimentacoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pmovimentacoes.Location = new System.Drawing.Point(0, 0);
            this.pmovimentacoes.Name = "pmovimentacoes";
            this.pmovimentacoes.Size = new System.Drawing.Size(2445, 178);
            this.pmovimentacoes.TabIndex = 15;
            // 
            // dgvmovimentacoes
            // 
            this.dgvmovimentacoes.AllowUserToAddRows = false;
            this.dgvmovimentacoes.AllowUserToDeleteRows = false;
            this.dgvmovimentacoes.AllowUserToResizeColumns = false;
            this.dgvmovimentacoes.AllowUserToResizeRows = false;
            this.dgvmovimentacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvmovimentacoes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvmovimentacoes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvmovimentacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvmovimentacoes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvmovimentacoes.Location = new System.Drawing.Point(36, 286);
            this.dgvmovimentacoes.MultiSelect = false;
            this.dgvmovimentacoes.Name = "dgvmovimentacoes";
            this.dgvmovimentacoes.ReadOnly = true;
            this.dgvmovimentacoes.RowHeadersVisible = false;
            this.dgvmovimentacoes.RowHeadersWidth = 51;
            this.dgvmovimentacoes.RowTemplate.Height = 24;
            this.dgvmovimentacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmovimentacoes.Size = new System.Drawing.Size(2371, 993);
            this.dgvmovimentacoes.TabIndex = 16;
            // 
            // ucmovimentacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lresultadomovimentacoes);
            this.Controls.Add(this.lperiodoaplicado);
            this.Controls.Add(this.bexportarmovimentacoes);
            this.Controls.Add(this.lfiltroinicio);
            this.Controls.Add(this.pmovimentacoes);
            this.Controls.Add(this.dgvmovimentacoes);
            this.Name = "ucmovimentacoes";
            this.Size = new System.Drawing.Size(2445, 1332);
            this.pmovimentacoes.ResumeLayout(false);
            this.pmovimentacoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lfiltroinicio;
        private System.Windows.Forms.DateTimePicker dtpfiltroinicio;
        private System.Windows.Forms.Label lfiltrounidade;
        private System.Windows.Forms.Label lfiltrofim;
        private System.Windows.Forms.DateTimePicker dtpfiltrofim;
        private System.Windows.Forms.ComboBox cbfiltrounidade;
        private System.Windows.Forms.Label lfiltroproduto;
        private System.Windows.Forms.ComboBox cbfiltroproduto;
        private System.Windows.Forms.Label lfiltrotipo;
        private System.Windows.Forms.ComboBox cbfiltrotipo;
        private System.Windows.Forms.Button bexportarmovimentacoes;
        private System.Windows.Forms.Label lperiodoaplicado;
        private System.Windows.Forms.Label lresultadomovimentacoes;
        private System.Windows.Forms.Panel pmovimentacoes;
        private System.Windows.Forms.DataGridView dgvmovimentacoes;
    }
}

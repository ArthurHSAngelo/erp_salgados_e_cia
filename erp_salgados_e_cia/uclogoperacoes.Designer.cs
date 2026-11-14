namespace erp_salgados_e_cia
{
    partial class uclogoperacoes
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
            this.dtpfiltrofim = new System.Windows.Forms.DateTimePicker();
            this.dtpfiltroinicio = new System.Windows.Forms.DateTimePicker();
            this.lfiltrofim = new System.Windows.Forms.Label();
            this.lfiltroinicio = new System.Windows.Forms.Label();
            this.lfiltrousuario = new System.Windows.Forms.Label();
            this.cbfiltrousuario = new System.Windows.Forms.ComboBox();
            this.cbfiltrotabela = new System.Windows.Forms.ComboBox();
            this.cbfiltrooperacao = new System.Windows.Forms.ComboBox();
            this.lfiltrotabela = new System.Windows.Forms.Label();
            this.lfiltrooperacao = new System.Windows.Forms.Label();
            this.lperiodoaplicado = new System.Windows.Forms.Label();
            this.lresultadolog = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvlog = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlog)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpfiltrofim
            // 
            this.dtpfiltrofim.Checked = false;
            this.dtpfiltrofim.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpfiltrofim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltrofim.Location = new System.Drawing.Point(345, 88);
            this.dtpfiltrofim.Name = "dtpfiltrofim";
            this.dtpfiltrofim.ShowCheckBox = true;
            this.dtpfiltrofim.Size = new System.Drawing.Size(136, 27);
            this.dtpfiltrofim.TabIndex = 15;
            // 
            // dtpfiltroinicio
            // 
            this.dtpfiltroinicio.Checked = false;
            this.dtpfiltroinicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpfiltroinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltroinicio.Location = new System.Drawing.Point(111, 88);
            this.dtpfiltroinicio.Name = "dtpfiltroinicio";
            this.dtpfiltroinicio.ShowCheckBox = true;
            this.dtpfiltroinicio.Size = new System.Drawing.Size(130, 27);
            this.dtpfiltroinicio.TabIndex = 14;
            // 
            // lfiltrofim
            // 
            this.lfiltrofim.AutoSize = true;
            this.lfiltrofim.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrofim.Location = new System.Drawing.Point(341, 64);
            this.lfiltrofim.Name = "lfiltrofim";
            this.lfiltrofim.Size = new System.Drawing.Size(113, 21);
            this.lfiltrofim.TabIndex = 13;
            this.lfiltrofim.Text = "Período final:";
            // 
            // lfiltroinicio
            // 
            this.lfiltroinicio.AutoSize = true;
            this.lfiltroinicio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltroinicio.Location = new System.Drawing.Point(107, 64);
            this.lfiltroinicio.Name = "lfiltroinicio";
            this.lfiltroinicio.Size = new System.Drawing.Size(125, 21);
            this.lfiltroinicio.TabIndex = 12;
            this.lfiltroinicio.Text = "Período inicial:";
            // 
            // lfiltrousuario
            // 
            this.lfiltrousuario.AutoSize = true;
            this.lfiltrousuario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrousuario.Location = new System.Drawing.Point(569, 64);
            this.lfiltrousuario.Name = "lfiltrousuario";
            this.lfiltrousuario.Size = new System.Drawing.Size(73, 21);
            this.lfiltrousuario.TabIndex = 16;
            this.lfiltrousuario.Text = "Usuário:";
            // 
            // cbfiltrousuario
            // 
            this.cbfiltrousuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrousuario.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrousuario.FormattingEnabled = true;
            this.cbfiltrousuario.Location = new System.Drawing.Point(573, 88);
            this.cbfiltrousuario.Name = "cbfiltrousuario";
            this.cbfiltrousuario.Size = new System.Drawing.Size(187, 31);
            this.cbfiltrousuario.TabIndex = 17;
            // 
            // cbfiltrotabela
            // 
            this.cbfiltrotabela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrotabela.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrotabela.FormattingEnabled = true;
            this.cbfiltrotabela.Location = new System.Drawing.Point(832, 90);
            this.cbfiltrotabela.Name = "cbfiltrotabela";
            this.cbfiltrotabela.Size = new System.Drawing.Size(184, 31);
            this.cbfiltrotabela.TabIndex = 18;
            // 
            // cbfiltrooperacao
            // 
            this.cbfiltrooperacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrooperacao.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrooperacao.FormattingEnabled = true;
            this.cbfiltrooperacao.Location = new System.Drawing.Point(1094, 88);
            this.cbfiltrooperacao.Name = "cbfiltrooperacao";
            this.cbfiltrooperacao.Size = new System.Drawing.Size(172, 31);
            this.cbfiltrooperacao.TabIndex = 19;
            // 
            // lfiltrotabela
            // 
            this.lfiltrotabela.AutoSize = true;
            this.lfiltrotabela.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrotabela.Location = new System.Drawing.Point(828, 64);
            this.lfiltrotabela.Name = "lfiltrotabela";
            this.lfiltrotabela.Size = new System.Drawing.Size(64, 21);
            this.lfiltrotabela.TabIndex = 20;
            this.lfiltrotabela.Text = "Tabela:";
            // 
            // lfiltrooperacao
            // 
            this.lfiltrooperacao.AutoSize = true;
            this.lfiltrooperacao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrooperacao.Location = new System.Drawing.Point(1090, 64);
            this.lfiltrooperacao.Name = "lfiltrooperacao";
            this.lfiltrooperacao.Size = new System.Drawing.Size(87, 21);
            this.lfiltrooperacao.TabIndex = 21;
            this.lfiltrooperacao.Text = "Operação:";
            // 
            // lperiodoaplicado
            // 
            this.lperiodoaplicado.AutoSize = true;
            this.lperiodoaplicado.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.lperiodoaplicado.Location = new System.Drawing.Point(1387, 91);
            this.lperiodoaplicado.Name = "lperiodoaplicado";
            this.lperiodoaplicado.Size = new System.Drawing.Size(94, 25);
            this.lperiodoaplicado.TabIndex = 22;
            this.lperiodoaplicado.Text = "Período: -";
            // 
            // lresultadolog
            // 
            this.lresultadolog.AutoSize = true;
            this.lresultadolog.Location = new System.Drawing.Point(725, 105);
            this.lresultadolog.Name = "lresultadolog";
            this.lresultadolog.Size = new System.Drawing.Size(0, 16);
            this.lresultadolog.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.cbfiltrousuario);
            this.panel1.Controls.Add(this.lfiltroinicio);
            this.panel1.Controls.Add(this.lfiltrotabela);
            this.panel1.Controls.Add(this.lresultadolog);
            this.panel1.Controls.Add(this.cbfiltrotabela);
            this.panel1.Controls.Add(this.lfiltrofim);
            this.panel1.Controls.Add(this.lperiodoaplicado);
            this.panel1.Controls.Add(this.dtpfiltroinicio);
            this.panel1.Controls.Add(this.lfiltrooperacao);
            this.panel1.Controls.Add(this.dtpfiltrofim);
            this.panel1.Controls.Add(this.lfiltrousuario);
            this.panel1.Controls.Add(this.cbfiltrooperacao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2445, 178);
            this.panel1.TabIndex = 25;
            // 
            // dgvlog
            // 
            this.dgvlog.AllowUserToAddRows = false;
            this.dgvlog.AllowUserToDeleteRows = false;
            this.dgvlog.AllowUserToResizeColumns = false;
            this.dgvlog.AllowUserToResizeRows = false;
            this.dgvlog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvlog.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvlog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvlog.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvlog.Location = new System.Drawing.Point(36, 286);
            this.dgvlog.MultiSelect = false;
            this.dgvlog.Name = "dgvlog";
            this.dgvlog.ReadOnly = true;
            this.dgvlog.RowHeadersVisible = false;
            this.dgvlog.RowHeadersWidth = 51;
            this.dgvlog.RowTemplate.Height = 24;
            this.dgvlog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvlog.Size = new System.Drawing.Size(2371, 993);
            this.dgvlog.TabIndex = 26;
            // 
            // uclogoperacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvlog);
            this.Name = "uclogoperacoes";
            this.Size = new System.Drawing.Size(2445, 1332);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpfiltrofim;
        private System.Windows.Forms.DateTimePicker dtpfiltroinicio;
        private System.Windows.Forms.Label lfiltrofim;
        private System.Windows.Forms.Label lfiltroinicio;
        private System.Windows.Forms.Label lfiltrousuario;
        private System.Windows.Forms.ComboBox cbfiltrousuario;
        private System.Windows.Forms.ComboBox cbfiltrotabela;
        private System.Windows.Forms.ComboBox cbfiltrooperacao;
        private System.Windows.Forms.Label lfiltrotabela;
        private System.Windows.Forms.Label lfiltrooperacao;
        private System.Windows.Forms.Label lperiodoaplicado;
        private System.Windows.Forms.Label lresultadolog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvlog;
    }
}

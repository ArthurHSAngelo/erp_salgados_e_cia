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
            this.dgvlog = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlog)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpfiltrofim
            // 
            this.dtpfiltrofim.Checked = false;
            this.dtpfiltrofim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltrofim.Location = new System.Drawing.Point(1251, 79);
            this.dtpfiltrofim.Name = "dtpfiltrofim";
            this.dtpfiltrofim.ShowCheckBox = true;
            this.dtpfiltrofim.Size = new System.Drawing.Size(136, 22);
            this.dtpfiltrofim.TabIndex = 15;
            // 
            // dtpfiltroinicio
            // 
            this.dtpfiltroinicio.Checked = false;
            this.dtpfiltroinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltroinicio.Location = new System.Drawing.Point(985, 79);
            this.dtpfiltroinicio.Name = "dtpfiltroinicio";
            this.dtpfiltroinicio.ShowCheckBox = true;
            this.dtpfiltroinicio.Size = new System.Drawing.Size(130, 22);
            this.dtpfiltroinicio.TabIndex = 14;
            // 
            // lfiltrofim
            // 
            this.lfiltrofim.AutoSize = true;
            this.lfiltrofim.Location = new System.Drawing.Point(1146, 84);
            this.lfiltrofim.Name = "lfiltrofim";
            this.lfiltrofim.Size = new System.Drawing.Size(85, 16);
            this.lfiltrofim.TabIndex = 13;
            this.lfiltrofim.Text = "Período final:";
            // 
            // lfiltroinicio
            // 
            this.lfiltroinicio.AutoSize = true;
            this.lfiltroinicio.Location = new System.Drawing.Point(867, 79);
            this.lfiltroinicio.Name = "lfiltroinicio";
            this.lfiltroinicio.Size = new System.Drawing.Size(95, 16);
            this.lfiltroinicio.TabIndex = 12;
            this.lfiltroinicio.Text = "Período inicial:";
            // 
            // lfiltrousuario
            // 
            this.lfiltrousuario.AutoSize = true;
            this.lfiltrousuario.Location = new System.Drawing.Point(867, 136);
            this.lfiltrousuario.Name = "lfiltrousuario";
            this.lfiltrousuario.Size = new System.Drawing.Size(57, 16);
            this.lfiltrousuario.TabIndex = 16;
            this.lfiltrousuario.Text = "Usuário:";
            // 
            // cbfiltrousuario
            // 
            this.cbfiltrousuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrousuario.FormattingEnabled = true;
            this.cbfiltrousuario.Location = new System.Drawing.Point(985, 133);
            this.cbfiltrousuario.Name = "cbfiltrousuario";
            this.cbfiltrousuario.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrousuario.TabIndex = 17;
            // 
            // cbfiltrotabela
            // 
            this.cbfiltrotabela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrotabela.FormattingEnabled = true;
            this.cbfiltrotabela.Location = new System.Drawing.Point(985, 255);
            this.cbfiltrotabela.Name = "cbfiltrotabela";
            this.cbfiltrotabela.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrotabela.TabIndex = 18;
            // 
            // cbfiltrooperacao
            // 
            this.cbfiltrooperacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrooperacao.FormattingEnabled = true;
            this.cbfiltrooperacao.Location = new System.Drawing.Point(985, 196);
            this.cbfiltrooperacao.Name = "cbfiltrooperacao";
            this.cbfiltrooperacao.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrooperacao.TabIndex = 19;
            // 
            // lfiltrotabela
            // 
            this.lfiltrotabela.AutoSize = true;
            this.lfiltrotabela.Location = new System.Drawing.Point(867, 255);
            this.lfiltrotabela.Name = "lfiltrotabela";
            this.lfiltrotabela.Size = new System.Drawing.Size(54, 16);
            this.lfiltrotabela.TabIndex = 20;
            this.lfiltrotabela.Text = "Tabela:";
            // 
            // lfiltrooperacao
            // 
            this.lfiltrooperacao.AutoSize = true;
            this.lfiltrooperacao.Location = new System.Drawing.Point(867, 196);
            this.lfiltrooperacao.Name = "lfiltrooperacao";
            this.lfiltrooperacao.Size = new System.Drawing.Size(71, 16);
            this.lfiltrooperacao.TabIndex = 21;
            this.lfiltrooperacao.Text = "Operação:";
            // 
            // lperiodoaplicado
            // 
            this.lperiodoaplicado.AutoSize = true;
            this.lperiodoaplicado.Location = new System.Drawing.Point(1477, 85);
            this.lperiodoaplicado.Name = "lperiodoaplicado";
            this.lperiodoaplicado.Size = new System.Drawing.Size(65, 16);
            this.lperiodoaplicado.TabIndex = 22;
            this.lperiodoaplicado.Text = "Período: -";
            // 
            // lresultadolog
            // 
            this.lresultadolog.AutoSize = true;
            this.lresultadolog.Location = new System.Drawing.Point(1311, 154);
            this.lresultadolog.Name = "lresultadolog";
            this.lresultadolog.Size = new System.Drawing.Size(0, 16);
            this.lresultadolog.TabIndex = 23;
            // 
            // dgvlog
            // 
            this.dgvlog.AllowUserToAddRows = false;
            this.dgvlog.AllowUserToDeleteRows = false;
            this.dgvlog.AllowUserToResizeColumns = false;
            this.dgvlog.AllowUserToResizeRows = false;
            this.dgvlog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlog.Location = new System.Drawing.Point(966, 328);
            this.dgvlog.MultiSelect = false;
            this.dgvlog.Name = "dgvlog";
            this.dgvlog.ReadOnly = true;
            this.dgvlog.RowHeadersVisible = false;
            this.dgvlog.RowHeadersWidth = 51;
            this.dgvlog.RowTemplate.Height = 24;
            this.dgvlog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvlog.Size = new System.Drawing.Size(726, 665);
            this.dgvlog.TabIndex = 24;
            // 
            // uclogoperacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvlog);
            this.Controls.Add(this.lresultadolog);
            this.Controls.Add(this.lperiodoaplicado);
            this.Controls.Add(this.lfiltrooperacao);
            this.Controls.Add(this.lfiltrotabela);
            this.Controls.Add(this.cbfiltrooperacao);
            this.Controls.Add(this.cbfiltrotabela);
            this.Controls.Add(this.cbfiltrousuario);
            this.Controls.Add(this.lfiltrousuario);
            this.Controls.Add(this.dtpfiltrofim);
            this.Controls.Add(this.dtpfiltroinicio);
            this.Controls.Add(this.lfiltrofim);
            this.Controls.Add(this.lfiltroinicio);
            this.Name = "uclogoperacoes";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.DataGridView dgvlog;
    }
}

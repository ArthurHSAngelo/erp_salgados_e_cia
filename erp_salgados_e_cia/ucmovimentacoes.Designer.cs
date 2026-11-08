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
            this.dgvmovimentacoes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).BeginInit();
            this.SuspendLayout();
            // 
            // lfiltroinicio
            // 
            this.lfiltroinicio.AutoSize = true;
            this.lfiltroinicio.Location = new System.Drawing.Point(80, 56);
            this.lfiltroinicio.Name = "lfiltroinicio";
            this.lfiltroinicio.Size = new System.Drawing.Size(95, 16);
            this.lfiltroinicio.TabIndex = 0;
            this.lfiltroinicio.Text = "Período inicial:";
            // 
            // dtpfiltroinicio
            // 
            this.dtpfiltroinicio.Checked = false;
            this.dtpfiltroinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltroinicio.Location = new System.Drawing.Point(198, 56);
            this.dtpfiltroinicio.Name = "dtpfiltroinicio";
            this.dtpfiltroinicio.ShowCheckBox = true;
            this.dtpfiltroinicio.Size = new System.Drawing.Size(140, 22);
            this.dtpfiltroinicio.TabIndex = 1;
            // 
            // lfiltrounidade
            // 
            this.lfiltrounidade.AutoSize = true;
            this.lfiltrounidade.Location = new System.Drawing.Point(699, 62);
            this.lfiltrounidade.Name = "lfiltrounidade";
            this.lfiltrounidade.Size = new System.Drawing.Size(62, 16);
            this.lfiltrounidade.TabIndex = 2;
            this.lfiltrounidade.Text = "Unidade:";
            // 
            // lfiltrofim
            // 
            this.lfiltrofim.AutoSize = true;
            this.lfiltrofim.Location = new System.Drawing.Point(370, 62);
            this.lfiltrofim.Name = "lfiltrofim";
            this.lfiltrofim.Size = new System.Drawing.Size(85, 16);
            this.lfiltrofim.TabIndex = 3;
            this.lfiltrofim.Text = "Período final:";
            // 
            // dtpfiltrofim
            // 
            this.dtpfiltrofim.Checked = false;
            this.dtpfiltrofim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfiltrofim.Location = new System.Drawing.Point(512, 62);
            this.dtpfiltrofim.Name = "dtpfiltrofim";
            this.dtpfiltrofim.ShowCheckBox = true;
            this.dtpfiltrofim.Size = new System.Drawing.Size(140, 22);
            this.dtpfiltrofim.TabIndex = 4;
            // 
            // cbfiltrounidade
            // 
            this.cbfiltrounidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrounidade.FormattingEnabled = true;
            this.cbfiltrounidade.Location = new System.Drawing.Point(767, 58);
            this.cbfiltrounidade.Name = "cbfiltrounidade";
            this.cbfiltrounidade.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrounidade.TabIndex = 5;
            // 
            // lfiltroproduto
            // 
            this.lfiltroproduto.AutoSize = true;
            this.lfiltroproduto.Location = new System.Drawing.Point(957, 66);
            this.lfiltroproduto.Name = "lfiltroproduto";
            this.lfiltroproduto.Size = new System.Drawing.Size(57, 16);
            this.lfiltroproduto.TabIndex = 6;
            this.lfiltroproduto.Text = "Produto:";
            // 
            // cbfiltroproduto
            // 
            this.cbfiltroproduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltroproduto.FormattingEnabled = true;
            this.cbfiltroproduto.Location = new System.Drawing.Point(1020, 58);
            this.cbfiltroproduto.Name = "cbfiltroproduto";
            this.cbfiltroproduto.Size = new System.Drawing.Size(121, 24);
            this.cbfiltroproduto.TabIndex = 7;
            // 
            // lfiltrotipo
            // 
            this.lfiltrotipo.AutoSize = true;
            this.lfiltrotipo.Location = new System.Drawing.Point(1188, 62);
            this.lfiltrotipo.Name = "lfiltrotipo";
            this.lfiltrotipo.Size = new System.Drawing.Size(38, 16);
            this.lfiltrotipo.TabIndex = 8;
            this.lfiltrotipo.Text = "Tipo:";
            // 
            // cbfiltrotipo
            // 
            this.cbfiltrotipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrotipo.FormattingEnabled = true;
            this.cbfiltrotipo.Location = new System.Drawing.Point(1232, 56);
            this.cbfiltrotipo.Name = "cbfiltrotipo";
            this.cbfiltrotipo.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrotipo.TabIndex = 9;
            // 
            // bexportarmovimentacoes
            // 
            this.bexportarmovimentacoes.Location = new System.Drawing.Point(1576, 49);
            this.bexportarmovimentacoes.Name = "bexportarmovimentacoes";
            this.bexportarmovimentacoes.Size = new System.Drawing.Size(111, 23);
            this.bexportarmovimentacoes.TabIndex = 11;
            this.bexportarmovimentacoes.Text = "Exportar CSV";
            this.bexportarmovimentacoes.UseVisualStyleBackColor = true;
            // 
            // lperiodoaplicado
            // 
            this.lperiodoaplicado.AutoSize = true;
            this.lperiodoaplicado.Location = new System.Drawing.Point(1017, 169);
            this.lperiodoaplicado.Name = "lperiodoaplicado";
            this.lperiodoaplicado.Size = new System.Drawing.Size(65, 16);
            this.lperiodoaplicado.TabIndex = 12;
            this.lperiodoaplicado.Text = "Período: -";
            // 
            // lresultadomovimentacoes
            // 
            this.lresultadomovimentacoes.AutoSize = true;
            this.lresultadomovimentacoes.Location = new System.Drawing.Point(1201, 255);
            this.lresultadomovimentacoes.Name = "lresultadomovimentacoes";
            this.lresultadomovimentacoes.Size = new System.Drawing.Size(0, 16);
            this.lresultadomovimentacoes.TabIndex = 13;
            // 
            // dgvmovimentacoes
            // 
            this.dgvmovimentacoes.AllowUserToAddRows = false;
            this.dgvmovimentacoes.AllowUserToDeleteRows = false;
            this.dgvmovimentacoes.AllowUserToResizeColumns = false;
            this.dgvmovimentacoes.AllowUserToResizeRows = false;
            this.dgvmovimentacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvmovimentacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvmovimentacoes.Location = new System.Drawing.Point(13, 131);
            this.dgvmovimentacoes.MultiSelect = false;
            this.dgvmovimentacoes.Name = "dgvmovimentacoes";
            this.dgvmovimentacoes.ReadOnly = true;
            this.dgvmovimentacoes.RowHeadersVisible = false;
            this.dgvmovimentacoes.RowHeadersWidth = 51;
            this.dgvmovimentacoes.RowTemplate.Height = 24;
            this.dgvmovimentacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmovimentacoes.Size = new System.Drawing.Size(915, 482);
            this.dgvmovimentacoes.TabIndex = 14;
            // 
            // ucmovimentacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvmovimentacoes);
            this.Controls.Add(this.lresultadomovimentacoes);
            this.Controls.Add(this.lperiodoaplicado);
            this.Controls.Add(this.bexportarmovimentacoes);
            this.Controls.Add(this.cbfiltrotipo);
            this.Controls.Add(this.lfiltrotipo);
            this.Controls.Add(this.cbfiltroproduto);
            this.Controls.Add(this.lfiltroproduto);
            this.Controls.Add(this.cbfiltrounidade);
            this.Controls.Add(this.dtpfiltrofim);
            this.Controls.Add(this.lfiltrofim);
            this.Controls.Add(this.lfiltrounidade);
            this.Controls.Add(this.dtpfiltroinicio);
            this.Controls.Add(this.lfiltroinicio);
            this.Name = "ucmovimentacoes";
            this.Size = new System.Drawing.Size(2445, 1332);
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
        private System.Windows.Forms.DataGridView dgvmovimentacoes;
    }
}

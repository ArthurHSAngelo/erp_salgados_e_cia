namespace erp_salgados_e_cia
{
    partial class ucfornecedores
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
            this.bexcluirf = new System.Windows.Forms.Button();
            this.tbtelefonef = new System.Windows.Forms.TextBox();
            this.ltelefone = new System.Windows.Forms.Label();
            this.dgvfornecedores = new System.Windows.Forms.DataGridView();
            this.bsalvarfornecedor = new System.Windows.Forms.Button();
            this.tbcnpjf = new System.Windows.Forms.TextBox();
            this.tbnomef = new System.Windows.Forms.TextBox();
            this.lcnpjfornecedor = new System.Windows.Forms.Label();
            this.lnomefornecedor = new System.Windows.Forms.Label();
            this.beditarf = new System.Windows.Forms.Button();
            this.cbativo = new System.Windows.Forms.CheckBox();
            this.cbfiltrostatus = new System.Windows.Forms.ComboBox();
            this.ldicaedicao = new System.Windows.Forms.Label();
            this.pfornecedores = new System.Windows.Forms.Panel();
            this.lregistrodefornecedor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).BeginInit();
            this.pfornecedores.SuspendLayout();
            this.SuspendLayout();
            // 
            // bexcluirf
            // 
            this.bexcluirf.BackColor = System.Drawing.Color.White;
            this.bexcluirf.FlatAppearance.BorderSize = 2;
            this.bexcluirf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexcluirf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexcluirf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bexcluirf.Location = new System.Drawing.Point(1516, 62);
            this.bexcluirf.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bexcluirf.Name = "bexcluirf";
            this.bexcluirf.Size = new System.Drawing.Size(129, 42);
            this.bexcluirf.TabIndex = 17;
            this.bexcluirf.Text = "Excluir";
            this.bexcluirf.UseVisualStyleBackColor = false;
            this.bexcluirf.Click += new System.EventHandler(this.bexcluirf_Click);
            // 
            // tbtelefonef
            // 
            this.tbtelefonef.BackColor = System.Drawing.Color.White;
            this.tbtelefonef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbtelefonef.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbtelefonef.Location = new System.Drawing.Point(513, 72);
            this.tbtelefonef.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbtelefonef.Name = "tbtelefonef";
            this.tbtelefonef.Size = new System.Drawing.Size(124, 26);
            this.tbtelefonef.TabIndex = 16;
            // 
            // ltelefone
            // 
            this.ltelefone.AutoSize = true;
            this.ltelefone.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.ltelefone.Location = new System.Drawing.Point(510, 53);
            this.ltelefone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ltelefone.Name = "ltelefone";
            this.ltelefone.Size = new System.Drawing.Size(65, 17);
            this.ltelefone.TabIndex = 15;
            this.ltelefone.Text = "Telefone:";
            // 
            // dgvfornecedores
            // 
            this.dgvfornecedores.AllowUserToAddRows = false;
            this.dgvfornecedores.AllowUserToDeleteRows = false;
            this.dgvfornecedores.AllowUserToResizeColumns = false;
            this.dgvfornecedores.AllowUserToResizeRows = false;
            this.dgvfornecedores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvfornecedores.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvfornecedores.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvfornecedores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvfornecedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvfornecedores.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvfornecedores.Location = new System.Drawing.Point(27, 232);
            this.dgvfornecedores.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvfornecedores.MultiSelect = false;
            this.dgvfornecedores.Name = "dgvfornecedores";
            this.dgvfornecedores.ReadOnly = true;
            this.dgvfornecedores.RowHeadersWidth = 51;
            this.dgvfornecedores.RowTemplate.Height = 24;
            this.dgvfornecedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvfornecedores.Size = new System.Drawing.Size(1778, 807);
            this.dgvfornecedores.TabIndex = 20;
            // 
            // bsalvarfornecedor
            // 
            this.bsalvarfornecedor.BackColor = System.Drawing.Color.White;
            this.bsalvarfornecedor.FlatAppearance.BorderSize = 2;
            this.bsalvarfornecedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsalvarfornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bsalvarfornecedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvarfornecedor.Location = new System.Drawing.Point(1226, 62);
            this.bsalvarfornecedor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bsalvarfornecedor.Name = "bsalvarfornecedor";
            this.bsalvarfornecedor.Size = new System.Drawing.Size(129, 42);
            this.bsalvarfornecedor.TabIndex = 13;
            this.bsalvarfornecedor.Text = "Salvar";
            this.bsalvarfornecedor.UseVisualStyleBackColor = false;
            this.bsalvarfornecedor.Click += new System.EventHandler(this.bsalvarfornecedor_Click);
            // 
            // tbcnpjf
            // 
            this.tbcnpjf.BackColor = System.Drawing.Color.White;
            this.tbcnpjf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbcnpjf.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbcnpjf.Location = new System.Drawing.Point(304, 72);
            this.tbcnpjf.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbcnpjf.Name = "tbcnpjf";
            this.tbcnpjf.Size = new System.Drawing.Size(156, 26);
            this.tbcnpjf.TabIndex = 12;
            // 
            // tbnomef
            // 
            this.tbnomef.BackColor = System.Drawing.Color.White;
            this.tbnomef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbnomef.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbnomef.Location = new System.Drawing.Point(83, 73);
            this.tbnomef.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbnomef.Name = "tbnomef";
            this.tbnomef.Size = new System.Drawing.Size(176, 26);
            this.tbnomef.TabIndex = 11;
            // 
            // lcnpjfornecedor
            // 
            this.lcnpjfornecedor.AutoSize = true;
            this.lcnpjfornecedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lcnpjfornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lcnpjfornecedor.Location = new System.Drawing.Point(302, 54);
            this.lcnpjfornecedor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lcnpjfornecedor.Name = "lcnpjfornecedor";
            this.lcnpjfornecedor.Size = new System.Drawing.Size(43, 17);
            this.lcnpjfornecedor.TabIndex = 10;
            this.lcnpjfornecedor.Text = "CNPJ:";
            // 
            // lnomefornecedor
            // 
            this.lnomefornecedor.AutoSize = true;
            this.lnomefornecedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lnomefornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lnomefornecedor.Location = new System.Drawing.Point(80, 54);
            this.lnomefornecedor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnomefornecedor.Name = "lnomefornecedor";
            this.lnomefornecedor.Size = new System.Drawing.Size(143, 17);
            this.lnomefornecedor.TabIndex = 9;
            this.lnomefornecedor.Text = "Nome do Fornecedor:";
            // 
            // beditarf
            // 
            this.beditarf.BackColor = System.Drawing.Color.White;
            this.beditarf.FlatAppearance.BorderSize = 2;
            this.beditarf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beditarf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.beditarf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditarf.Location = new System.Drawing.Point(1373, 62);
            this.beditarf.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.beditarf.Name = "beditarf";
            this.beditarf.Size = new System.Drawing.Size(129, 42);
            this.beditarf.TabIndex = 18;
            this.beditarf.Text = "Editar";
            this.beditarf.UseVisualStyleBackColor = false;
            this.beditarf.Click += new System.EventHandler(this.beditarf_Click);
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cbativo.Location = new System.Drawing.Point(735, 75);
            this.cbativo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(60, 21);
            this.cbativo.TabIndex = 19;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.BackColor = System.Drawing.Color.White;
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(1684, 204);
            this.cbfiltrostatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(122, 27);
            this.cbfiltrostatus.TabIndex = 20;
            // 
            // ldicaedicao
            // 
            this.ldicaedicao.AutoSize = true;
            this.ldicaedicao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ldicaedicao.ForeColor = System.Drawing.Color.Firebrick;
            this.ldicaedicao.Location = new System.Drawing.Point(23, 170);
            this.ldicaedicao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ldicaedicao.Name = "ldicaedicao";
            this.ldicaedicao.Size = new System.Drawing.Size(366, 21);
            this.ldicaedicao.TabIndex = 29;
            this.ldicaedicao.Text = "Editando: pressione Esc para cancelar a edição.";
            this.ldicaedicao.Visible = false;
            // 
            // pfornecedores
            // 
            this.pfornecedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pfornecedores.Controls.Add(this.tbcnpjf);
            this.pfornecedores.Controls.Add(this.tbtelefonef);
            this.pfornecedores.Controls.Add(this.cbativo);
            this.pfornecedores.Controls.Add(this.ltelefone);
            this.pfornecedores.Controls.Add(this.lcnpjfornecedor);
            this.pfornecedores.Dock = System.Windows.Forms.DockStyle.Top;
            this.pfornecedores.Location = new System.Drawing.Point(0, 0);
            this.pfornecedores.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pfornecedores.Name = "pfornecedores";
            this.pfornecedores.Size = new System.Drawing.Size(1834, 145);
            this.pfornecedores.TabIndex = 30;
            // 
            // lregistrodefornecedor
            // 
            this.lregistrodefornecedor.AutoSize = true;
            this.lregistrodefornecedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lregistrodefornecedor.Location = new System.Drawing.Point(23, 207);
            this.lregistrodefornecedor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lregistrodefornecedor.Name = "lregistrodefornecedor";
            this.lregistrodefornecedor.Size = new System.Drawing.Size(191, 21);
            this.lregistrodefornecedor.TabIndex = 32;
            this.lregistrodefornecedor.Text = "Registro de Fornecedor:";
            // 
            // ucfornecedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lregistrodefornecedor);
            this.Controls.Add(this.ldicaedicao);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.beditarf);
            this.Controls.Add(this.bexcluirf);
            this.Controls.Add(this.dgvfornecedores);
            this.Controls.Add(this.bsalvarfornecedor);
            this.Controls.Add(this.tbnomef);
            this.Controls.Add(this.lnomefornecedor);
            this.Controls.Add(this.pfornecedores);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucfornecedores";
            this.Size = new System.Drawing.Size(1834, 1082);
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).EndInit();
            this.pfornecedores.ResumeLayout(false);
            this.pfornecedores.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bexcluirf;
        private System.Windows.Forms.TextBox tbtelefonef;
        private System.Windows.Forms.Label ltelefone;
        private System.Windows.Forms.DataGridView dgvfornecedores;
        private System.Windows.Forms.Button bsalvarfornecedor;
        private System.Windows.Forms.TextBox tbcnpjf;
        private System.Windows.Forms.TextBox tbnomef;
        private System.Windows.Forms.Label lcnpjfornecedor;
        private System.Windows.Forms.Label lnomefornecedor;
        private System.Windows.Forms.Button beditarf;
        private System.Windows.Forms.CheckBox cbativo;
        private System.Windows.Forms.ComboBox cbfiltrostatus;
        private System.Windows.Forms.Label ldicaedicao;
        private System.Windows.Forms.Panel pfornecedores;
        private System.Windows.Forms.Label lregistrodefornecedor;
    }
}

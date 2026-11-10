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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lregistrodefornecedor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bexcluirf
            // 
            this.bexcluirf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.bexcluirf.FlatAppearance.BorderSize = 2;
            this.bexcluirf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexcluirf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexcluirf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bexcluirf.Location = new System.Drawing.Point(2021, 76);
            this.bexcluirf.Name = "bexcluirf";
            this.bexcluirf.Size = new System.Drawing.Size(172, 52);
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
            this.tbtelefonef.Location = new System.Drawing.Point(684, 89);
            this.tbtelefonef.Name = "tbtelefonef";
            this.tbtelefonef.Size = new System.Drawing.Size(165, 30);
            this.tbtelefonef.TabIndex = 16;
            // 
            // ltelefone
            // 
            this.ltelefone.AutoSize = true;
            this.ltelefone.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.ltelefone.Location = new System.Drawing.Point(680, 65);
            this.ltelefone.Name = "ltelefone";
            this.ltelefone.Size = new System.Drawing.Size(80, 21);
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
            this.dgvfornecedores.Location = new System.Drawing.Point(36, 286);
            this.dgvfornecedores.MultiSelect = false;
            this.dgvfornecedores.Name = "dgvfornecedores";
            this.dgvfornecedores.ReadOnly = true;
            this.dgvfornecedores.RowHeadersWidth = 51;
            this.dgvfornecedores.RowTemplate.Height = 24;
            this.dgvfornecedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvfornecedores.Size = new System.Drawing.Size(2371, 993);
            this.dgvfornecedores.TabIndex = 20;
            // 
            // bsalvarfornecedor
            // 
            this.bsalvarfornecedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.bsalvarfornecedor.FlatAppearance.BorderSize = 2;
            this.bsalvarfornecedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsalvarfornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bsalvarfornecedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvarfornecedor.Location = new System.Drawing.Point(1634, 76);
            this.bsalvarfornecedor.Name = "bsalvarfornecedor";
            this.bsalvarfornecedor.Size = new System.Drawing.Size(172, 52);
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
            this.tbcnpjf.Location = new System.Drawing.Point(406, 89);
            this.tbcnpjf.Name = "tbcnpjf";
            this.tbcnpjf.Size = new System.Drawing.Size(208, 30);
            this.tbcnpjf.TabIndex = 12;
            // 
            // tbnomef
            // 
            this.tbnomef.BackColor = System.Drawing.Color.White;
            this.tbnomef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbnomef.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbnomef.Location = new System.Drawing.Point(111, 90);
            this.tbnomef.Name = "tbnomef";
            this.tbnomef.Size = new System.Drawing.Size(234, 30);
            this.tbnomef.TabIndex = 11;
            // 
            // lcnpjfornecedor
            // 
            this.lcnpjfornecedor.AutoSize = true;
            this.lcnpjfornecedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lcnpjfornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lcnpjfornecedor.Location = new System.Drawing.Point(402, 66);
            this.lcnpjfornecedor.Name = "lcnpjfornecedor";
            this.lcnpjfornecedor.Size = new System.Drawing.Size(53, 21);
            this.lcnpjfornecedor.TabIndex = 10;
            this.lcnpjfornecedor.Text = "CNPJ:";
            // 
            // lnomefornecedor
            // 
            this.lnomefornecedor.AutoSize = true;
            this.lnomefornecedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lnomefornecedor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lnomefornecedor.Location = new System.Drawing.Point(107, 66);
            this.lnomefornecedor.Name = "lnomefornecedor";
            this.lnomefornecedor.Size = new System.Drawing.Size(176, 21);
            this.lnomefornecedor.TabIndex = 9;
            this.lnomefornecedor.Text = "Nome do Fornecedor:";
            // 
            // beditarf
            // 
            this.beditarf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.beditarf.FlatAppearance.BorderSize = 2;
            this.beditarf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beditarf.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.beditarf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditarf.Location = new System.Drawing.Point(1831, 76);
            this.beditarf.Name = "beditarf";
            this.beditarf.Size = new System.Drawing.Size(172, 52);
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
            this.cbativo.Location = new System.Drawing.Point(980, 92);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(73, 25);
            this.cbativo.TabIndex = 19;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(2245, 251);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(162, 31);
            this.cbfiltrostatus.TabIndex = 20;
            // 
            // ldicaedicao
            // 
            this.ldicaedicao.AutoSize = true;
            this.ldicaedicao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ldicaedicao.ForeColor = System.Drawing.Color.Firebrick;
            this.ldicaedicao.Location = new System.Drawing.Point(31, 209);
            this.ldicaedicao.Name = "ldicaedicao";
            this.ldicaedicao.Size = new System.Drawing.Size(456, 28);
            this.ldicaedicao.TabIndex = 29;
            this.ldicaedicao.Text = "Editando: pressione Esc para cancelar a edição.";
            this.ldicaedicao.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.tbcnpjf);
            this.panel1.Controls.Add(this.tbtelefonef);
            this.panel1.Controls.Add(this.cbativo);
            this.panel1.Controls.Add(this.ltelefone);
            this.panel1.Controls.Add(this.lcnpjfornecedor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2445, 178);
            this.panel1.TabIndex = 30;
            // 
            // lregistrodefornecedor
            // 
            this.lregistrodefornecedor.AutoSize = true;
            this.lregistrodefornecedor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lregistrodefornecedor.Location = new System.Drawing.Point(31, 255);
            this.lregistrodefornecedor.Name = "lregistrodefornecedor";
            this.lregistrodefornecedor.Size = new System.Drawing.Size(238, 28);
            this.lregistrodefornecedor.TabIndex = 32;
            this.lregistrodefornecedor.Text = "Registro de Fornecedor:";
            // 
            // ucfornecedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
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
            this.Controls.Add(this.panel1);
            this.Name = "ucfornecedores";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lregistrodefornecedor;
    }
}

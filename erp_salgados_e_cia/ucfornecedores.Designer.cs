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
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).BeginInit();
            this.SuspendLayout();
            // 
            // bexcluirf
            // 
            this.bexcluirf.Location = new System.Drawing.Point(575, 142);
            this.bexcluirf.Name = "bexcluirf";
            this.bexcluirf.Size = new System.Drawing.Size(75, 23);
            this.bexcluirf.TabIndex = 17;
            this.bexcluirf.Text = "Excluir";
            this.bexcluirf.UseVisualStyleBackColor = true;
            this.bexcluirf.Click += new System.EventHandler(this.bexcluirf_Click);
            // 
            // tbtelefonef
            // 
            this.tbtelefonef.Location = new System.Drawing.Point(152, 129);
            this.tbtelefonef.Name = "tbtelefonef";
            this.tbtelefonef.Size = new System.Drawing.Size(100, 22);
            this.tbtelefonef.TabIndex = 16;
            // 
            // ltelefone
            // 
            this.ltelefone.AutoSize = true;
            this.ltelefone.Location = new System.Drawing.Point(82, 129);
            this.ltelefone.Name = "ltelefone";
            this.ltelefone.Size = new System.Drawing.Size(64, 16);
            this.ltelefone.TabIndex = 15;
            this.ltelefone.Text = "Telefone:";
            // 
            // dgvfornecedores
            // 
            this.dgvfornecedores.AllowUserToAddRows = false;
            this.dgvfornecedores.AllowUserToDeleteRows = false;
            this.dgvfornecedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvfornecedores.Location = new System.Drawing.Point(110, 188);
            this.dgvfornecedores.MultiSelect = false;
            this.dgvfornecedores.Name = "dgvfornecedores";
            this.dgvfornecedores.ReadOnly = true;
            this.dgvfornecedores.RowHeadersWidth = 51;
            this.dgvfornecedores.RowTemplate.Height = 24;
            this.dgvfornecedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvfornecedores.Size = new System.Drawing.Size(540, 191);
            this.dgvfornecedores.TabIndex = 14;
            // 
            // bsalvarfornecedor
            // 
            this.bsalvarfornecedor.Location = new System.Drawing.Point(360, 59);
            this.bsalvarfornecedor.Name = "bsalvarfornecedor";
            this.bsalvarfornecedor.Size = new System.Drawing.Size(75, 23);
            this.bsalvarfornecedor.TabIndex = 13;
            this.bsalvarfornecedor.Text = "Salvar";
            this.bsalvarfornecedor.UseVisualStyleBackColor = true;
            this.bsalvarfornecedor.Click += new System.EventHandler(this.bsalvarfornecedor_Click);
            // 
            // tbcnpjf
            // 
            this.tbcnpjf.Location = new System.Drawing.Point(143, 96);
            this.tbcnpjf.Name = "tbcnpjf";
            this.tbcnpjf.Size = new System.Drawing.Size(100, 22);
            this.tbcnpjf.TabIndex = 12;
            // 
            // tbnomef
            // 
            this.tbnomef.Location = new System.Drawing.Point(227, 62);
            this.tbnomef.Name = "tbnomef";
            this.tbnomef.Size = new System.Drawing.Size(100, 22);
            this.tbnomef.TabIndex = 11;
            // 
            // lcnpjfornecedor
            // 
            this.lcnpjfornecedor.AutoSize = true;
            this.lcnpjfornecedor.Location = new System.Drawing.Point(82, 102);
            this.lcnpjfornecedor.Name = "lcnpjfornecedor";
            this.lcnpjfornecedor.Size = new System.Drawing.Size(45, 16);
            this.lcnpjfornecedor.TabIndex = 10;
            this.lcnpjfornecedor.Text = "CNPJ:";
            // 
            // lnomefornecedor
            // 
            this.lnomefornecedor.AutoSize = true;
            this.lnomefornecedor.Location = new System.Drawing.Point(82, 62);
            this.lnomefornecedor.Name = "lnomefornecedor";
            this.lnomefornecedor.Size = new System.Drawing.Size(139, 16);
            this.lnomefornecedor.TabIndex = 9;
            this.lnomefornecedor.Text = "Nome do Fornecedor:";
            // 
            // beditarf
            // 
            this.beditarf.Location = new System.Drawing.Point(431, 112);
            this.beditarf.Name = "beditarf";
            this.beditarf.Size = new System.Drawing.Size(75, 23);
            this.beditarf.TabIndex = 18;
            this.beditarf.Text = "Editar";
            this.beditarf.UseVisualStyleBackColor = true;
            this.beditarf.Click += new System.EventHandler(this.beditarf_Click);
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Location = new System.Drawing.Point(605, 63);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(59, 20);
            this.cbativo.TabIndex = 19;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // ucfornecedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbativo);
            this.Controls.Add(this.beditarf);
            this.Controls.Add(this.bexcluirf);
            this.Controls.Add(this.tbtelefonef);
            this.Controls.Add(this.ltelefone);
            this.Controls.Add(this.dgvfornecedores);
            this.Controls.Add(this.bsalvarfornecedor);
            this.Controls.Add(this.tbcnpjf);
            this.Controls.Add(this.tbnomef);
            this.Controls.Add(this.lcnpjfornecedor);
            this.Controls.Add(this.lnomefornecedor);
            this.Name = "ucfornecedores";
            this.Size = new System.Drawing.Size(2391, 1163);
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).EndInit();
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
    }
}

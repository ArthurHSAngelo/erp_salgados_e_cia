namespace erp_salgados_e_cia
{
    partial class CadastroFornecedores
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
            this.lnomefornecedor = new System.Windows.Forms.Label();
            this.lcnpjfornecedor = new System.Windows.Forms.Label();
            this.tbnomef = new System.Windows.Forms.TextBox();
            this.tbcnpjf = new System.Windows.Forms.TextBox();
            this.bsalvarfornecedor = new System.Windows.Forms.Button();
            this.dgvfornecedores = new System.Windows.Forms.DataGridView();
            this.ltelefone = new System.Windows.Forms.Label();
            this.tbtelefonef = new System.Windows.Forms.TextBox();
            this.bexcluirfornecedor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).BeginInit();
            this.SuspendLayout();
            // 
            // lnomefornecedor
            // 
            this.lnomefornecedor.AutoSize = true;
            this.lnomefornecedor.Location = new System.Drawing.Point(81, 65);
            this.lnomefornecedor.Name = "lnomefornecedor";
            this.lnomefornecedor.Size = new System.Drawing.Size(139, 16);
            this.lnomefornecedor.TabIndex = 0;
            this.lnomefornecedor.Text = "Nome do Fornecedor:";
            // 
            // lcnpjfornecedor
            // 
            this.lcnpjfornecedor.AutoSize = true;
            this.lcnpjfornecedor.Location = new System.Drawing.Point(81, 105);
            this.lcnpjfornecedor.Name = "lcnpjfornecedor";
            this.lcnpjfornecedor.Size = new System.Drawing.Size(45, 16);
            this.lcnpjfornecedor.TabIndex = 1;
            this.lcnpjfornecedor.Text = "CNPJ:";
            // 
            // tbnomef
            // 
            this.tbnomef.Location = new System.Drawing.Point(226, 65);
            this.tbnomef.Name = "tbnomef";
            this.tbnomef.Size = new System.Drawing.Size(100, 22);
            this.tbnomef.TabIndex = 2;
            // 
            // tbcnpjf
            // 
            this.tbcnpjf.Location = new System.Drawing.Point(142, 99);
            this.tbcnpjf.Name = "tbcnpjf";
            this.tbcnpjf.Size = new System.Drawing.Size(100, 22);
            this.tbcnpjf.TabIndex = 3;
            // 
            // bsalvarfornecedor
            // 
            this.bsalvarfornecedor.Location = new System.Drawing.Point(359, 62);
            this.bsalvarfornecedor.Name = "bsalvarfornecedor";
            this.bsalvarfornecedor.Size = new System.Drawing.Size(75, 23);
            this.bsalvarfornecedor.TabIndex = 4;
            this.bsalvarfornecedor.Text = "Salvar";
            this.bsalvarfornecedor.UseVisualStyleBackColor = true;
            this.bsalvarfornecedor.Click += new System.EventHandler(this.bsalvarfornecedor_Click);
            // 
            // dgvfornecedores
            // 
            this.dgvfornecedores.AllowUserToAddRows = false;
            this.dgvfornecedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvfornecedores.Location = new System.Drawing.Point(109, 191);
            this.dgvfornecedores.MultiSelect = false;
            this.dgvfornecedores.Name = "dgvfornecedores";
            this.dgvfornecedores.RowHeadersWidth = 51;
            this.dgvfornecedores.RowTemplate.Height = 24;
            this.dgvfornecedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvfornecedores.Size = new System.Drawing.Size(540, 191);
            this.dgvfornecedores.TabIndex = 5;
            this.dgvfornecedores.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvfornecedores_CellValidating);
            // 
            // ltelefone
            // 
            this.ltelefone.AutoSize = true;
            this.ltelefone.Location = new System.Drawing.Point(81, 132);
            this.ltelefone.Name = "ltelefone";
            this.ltelefone.Size = new System.Drawing.Size(64, 16);
            this.ltelefone.TabIndex = 6;
            this.ltelefone.Text = "Telefone:";
            // 
            // tbtelefonef
            // 
            this.tbtelefonef.Location = new System.Drawing.Point(151, 132);
            this.tbtelefonef.Name = "tbtelefonef";
            this.tbtelefonef.Size = new System.Drawing.Size(100, 22);
            this.tbtelefonef.TabIndex = 7;
            // 
            // bexcluirfornecedor
            // 
            this.bexcluirfornecedor.Location = new System.Drawing.Point(574, 145);
            this.bexcluirfornecedor.Name = "bexcluirfornecedor";
            this.bexcluirfornecedor.Size = new System.Drawing.Size(75, 23);
            this.bexcluirfornecedor.TabIndex = 8;
            this.bexcluirfornecedor.Text = "Excluir";
            this.bexcluirfornecedor.UseVisualStyleBackColor = true;
            this.bexcluirfornecedor.Click += new System.EventHandler(this.bexcluirfornecedor_Click);
            // 
            // CadastroFornecedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bexcluirfornecedor);
            this.Controls.Add(this.tbtelefonef);
            this.Controls.Add(this.ltelefone);
            this.Controls.Add(this.dgvfornecedores);
            this.Controls.Add(this.bsalvarfornecedor);
            this.Controls.Add(this.tbcnpjf);
            this.Controls.Add(this.tbnomef);
            this.Controls.Add(this.lcnpjfornecedor);
            this.Controls.Add(this.lnomefornecedor);
            this.Name = "CadastroFornecedores";
            this.Text = "Cadastro de Fornecedores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvfornecedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lnomefornecedor;
        private System.Windows.Forms.Label lcnpjfornecedor;
        private System.Windows.Forms.TextBox tbnomef;
        private System.Windows.Forms.TextBox tbcnpjf;
        private System.Windows.Forms.Button bsalvarfornecedor;
        private System.Windows.Forms.DataGridView dgvfornecedores;
        private System.Windows.Forms.Label ltelefone;
        private System.Windows.Forms.TextBox tbtelefonef;
        private System.Windows.Forms.Button bexcluirfornecedor;
    }
}
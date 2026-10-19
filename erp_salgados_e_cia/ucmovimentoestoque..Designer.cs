namespace erp_salgados_e_cia
{
    partial class ucmovimentoestoque
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
            this.bregistrar = new System.Windows.Forms.Button();
            this.tbcustouni = new System.Windows.Forms.TextBox();
            this.lcustouni = new System.Windows.Forms.Label();
            this.nudquantidade = new System.Windows.Forms.NumericUpDown();
            this.lquantidade = new System.Windows.Forms.Label();
            this.cbproduto = new System.Windows.Forms.ComboBox();
            this.lproduto = new System.Windows.Forms.Label();
            this.lresponsavel = new System.Windows.Forms.Label();
            this.cbresponsavel = new System.Windows.Forms.ComboBox();
            this.cbfornecedor = new System.Windows.Forms.ComboBox();
            this.lfornecedore = new System.Windows.Forms.Label();
            this.dgvmovimentacoes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).BeginInit();
            this.SuspendLayout();
            // 
            // bregistrar
            // 
            this.bregistrar.Location = new System.Drawing.Point(160, 286);
            this.bregistrar.Name = "bregistrar";
            this.bregistrar.Size = new System.Drawing.Size(158, 38);
            this.bregistrar.TabIndex = 17;
            this.bregistrar.Text = "Resgistrar Entrada";
            this.bregistrar.UseVisualStyleBackColor = true;
            this.bregistrar.Click += new System.EventHandler(this.bregistrar_Click);
            // 
            // tbcustouni
            // 
            this.tbcustouni.Location = new System.Drawing.Point(211, 165);
            this.tbcustouni.Name = "tbcustouni";
            this.tbcustouni.Size = new System.Drawing.Size(100, 22);
            this.tbcustouni.TabIndex = 16;
            // 
            // lcustouni
            // 
            this.lcustouni.AutoSize = true;
            this.lcustouni.Location = new System.Drawing.Point(86, 161);
            this.lcustouni.Name = "lcustouni";
            this.lcustouni.Size = new System.Drawing.Size(119, 16);
            this.lcustouni.TabIndex = 15;
            this.lcustouni.Text = "Custo por unidade:";
            // 
            // nudquantidade
            // 
            this.nudquantidade.DecimalPlaces = 2;
            this.nudquantidade.Location = new System.Drawing.Point(172, 115);
            this.nudquantidade.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudquantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudquantidade.Name = "nudquantidade";
            this.nudquantidade.Size = new System.Drawing.Size(62, 22);
            this.nudquantidade.TabIndex = 12;
            this.nudquantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lquantidade
            // 
            this.lquantidade.AutoSize = true;
            this.lquantidade.Location = new System.Drawing.Point(86, 117);
            this.lquantidade.Name = "lquantidade";
            this.lquantidade.Size = new System.Drawing.Size(80, 16);
            this.lquantidade.TabIndex = 11;
            this.lquantidade.Text = "Quantidade:";
            // 
            // cbproduto
            // 
            this.cbproduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbproduto.FormattingEnabled = true;
            this.cbproduto.Location = new System.Drawing.Point(160, 69);
            this.cbproduto.Name = "cbproduto";
            this.cbproduto.Size = new System.Drawing.Size(121, 24);
            this.cbproduto.TabIndex = 10;
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Location = new System.Drawing.Point(86, 69);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(57, 16);
            this.lproduto.TabIndex = 9;
            this.lproduto.Text = "Produto:";
            // 
            // lresponsavel
            // 
            this.lresponsavel.AutoSize = true;
            this.lresponsavel.Location = new System.Drawing.Point(98, 239);
            this.lresponsavel.Name = "lresponsavel";
            this.lresponsavel.Size = new System.Drawing.Size(91, 16);
            this.lresponsavel.TabIndex = 18;
            this.lresponsavel.Text = "Responsável:";
            // 
            // cbresponsavel
            // 
            this.cbresponsavel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbresponsavel.FormattingEnabled = true;
            this.cbresponsavel.Location = new System.Drawing.Point(209, 236);
            this.cbresponsavel.Name = "cbresponsavel";
            this.cbresponsavel.Size = new System.Drawing.Size(102, 24);
            this.cbresponsavel.TabIndex = 19;
            // 
            // cbfornecedor
            // 
            this.cbfornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfornecedor.FormattingEnabled = true;
            this.cbfornecedor.Location = new System.Drawing.Point(209, 195);
            this.cbfornecedor.Name = "cbfornecedor";
            this.cbfornecedor.Size = new System.Drawing.Size(102, 24);
            this.cbfornecedor.TabIndex = 21;
            // 
            // lfornecedore
            // 
            this.lfornecedore.AutoSize = true;
            this.lfornecedore.Location = new System.Drawing.Point(98, 198);
            this.lfornecedore.Name = "lfornecedore";
            this.lfornecedore.Size = new System.Drawing.Size(80, 16);
            this.lfornecedore.TabIndex = 20;
            this.lfornecedore.Text = "Fornecedor:";
            // 
            // dgvmovimentacoes
            // 
            this.dgvmovimentacoes.AllowUserToAddRows = false;
            this.dgvmovimentacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvmovimentacoes.Location = new System.Drawing.Point(650, 81);
            this.dgvmovimentacoes.Name = "dgvmovimentacoes";
            this.dgvmovimentacoes.ReadOnly = true;
            this.dgvmovimentacoes.RowHeadersWidth = 51;
            this.dgvmovimentacoes.RowTemplate.Height = 24;
            this.dgvmovimentacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmovimentacoes.Size = new System.Drawing.Size(509, 326);
            this.dgvmovimentacoes.TabIndex = 22;
            // 
            // ucmovimentoestoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvmovimentacoes);
            this.Controls.Add(this.cbfornecedor);
            this.Controls.Add(this.lfornecedore);
            this.Controls.Add(this.cbresponsavel);
            this.Controls.Add(this.lresponsavel);
            this.Controls.Add(this.bregistrar);
            this.Controls.Add(this.tbcustouni);
            this.Controls.Add(this.lcustouni);
            this.Controls.Add(this.nudquantidade);
            this.Controls.Add(this.lquantidade);
            this.Controls.Add(this.cbproduto);
            this.Controls.Add(this.lproduto);
            this.Name = "ucmovimentoestoque";
            this.Size = new System.Drawing.Size(2371, 1192);
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bregistrar;
        private System.Windows.Forms.TextBox tbcustouni;
        private System.Windows.Forms.Label lcustouni;
        private System.Windows.Forms.NumericUpDown nudquantidade;
        private System.Windows.Forms.Label lquantidade;
        private System.Windows.Forms.ComboBox cbproduto;
        private System.Windows.Forms.Label lproduto;
        private System.Windows.Forms.Label lresponsavel;
        private System.Windows.Forms.ComboBox cbresponsavel;
        private System.Windows.Forms.ComboBox cbfornecedor;
        private System.Windows.Forms.Label lfornecedore;
        private System.Windows.Forms.DataGridView dgvmovimentacoes;
    }
}

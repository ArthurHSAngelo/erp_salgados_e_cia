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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvmovimentacoes = new System.Windows.Forms.DataGridView();
            this.dgvitensentrada = new System.Windows.Forms.DataGridView();
            this.litensentrada = new System.Windows.Forms.Label();
            this.lhistorico = new System.Windows.Forms.Label();
            this.cbresponsavel = new System.Windows.Forms.ComboBox();
            this.lresponsavel = new System.Windows.Forms.Label();
            this.lfornecedore = new System.Windows.Forms.Label();
            this.tbcustouni = new System.Windows.Forms.TextBox();
            this.cbfornecedor = new System.Windows.Forms.ComboBox();
            this.nudquantidade = new System.Windows.Forms.NumericUpDown();
            this.bregistrar = new System.Windows.Forms.Button();
            this.lquantidade = new System.Windows.Forms.Label();
            this.bfinalizarentrada = new System.Windows.Forms.Button();
            this.cbproduto = new System.Windows.Forms.ComboBox();
            this.lproduto = new System.Windows.Forms.Label();
            this.bremoveritem = new System.Windows.Forms.Button();
            this.lcustouni = new System.Windows.Forms.Label();
            this.pmovimentoestoque = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensentrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            this.pmovimentoestoque.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvmovimentacoes
            // 
            this.dgvmovimentacoes.AllowUserToAddRows = false;
            this.dgvmovimentacoes.AllowUserToDeleteRows = false;
            this.dgvmovimentacoes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvmovimentacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvmovimentacoes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.dgvmovimentacoes.Location = new System.Drawing.Point(922, 232);
            this.dgvmovimentacoes.Margin = new System.Windows.Forms.Padding(2);
            this.dgvmovimentacoes.MultiSelect = false;
            this.dgvmovimentacoes.Name = "dgvmovimentacoes";
            this.dgvmovimentacoes.ReadOnly = true;
            this.dgvmovimentacoes.RowHeadersWidth = 51;
            this.dgvmovimentacoes.RowTemplate.Height = 24;
            this.dgvmovimentacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmovimentacoes.Size = new System.Drawing.Size(886, 807);
            this.dgvmovimentacoes.TabIndex = 22;
            // 
            // dgvitensentrada
            // 
            this.dgvitensentrada.AllowUserToAddRows = false;
            this.dgvitensentrada.AllowUserToDeleteRows = false;
            this.dgvitensentrada.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvitensentrada.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvitensentrada.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvitensentrada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvitensentrada.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvitensentrada.Location = new System.Drawing.Point(27, 232);
            this.dgvitensentrada.Margin = new System.Windows.Forms.Padding(2);
            this.dgvitensentrada.MultiSelect = false;
            this.dgvitensentrada.Name = "dgvitensentrada";
            this.dgvitensentrada.ReadOnly = true;
            this.dgvitensentrada.RowHeadersWidth = 51;
            this.dgvitensentrada.RowTemplate.Height = 24;
            this.dgvitensentrada.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvitensentrada.Size = new System.Drawing.Size(886, 807);
            this.dgvitensentrada.TabIndex = 24;
            // 
            // litensentrada
            // 
            this.litensentrada.AutoSize = true;
            this.litensentrada.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.litensentrada.Location = new System.Drawing.Point(23, 209);
            this.litensentrada.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.litensentrada.Name = "litensentrada";
            this.litensentrada.Size = new System.Drawing.Size(180, 21);
            this.litensentrada.TabIndex = 27;
            this.litensentrada.Text = "Itens da entrada atual:";
            // 
            // lhistorico
            // 
            this.lhistorico.AutoSize = true;
            this.lhistorico.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lhistorico.Location = new System.Drawing.Point(919, 209);
            this.lhistorico.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lhistorico.Name = "lhistorico";
            this.lhistorico.Size = new System.Drawing.Size(176, 21);
            this.lhistorico.TabIndex = 28;
            this.lhistorico.Text = "Histórico de entradas:";
            // 
            // cbresponsavel
            // 
            this.cbresponsavel.BackColor = System.Drawing.Color.White;
            this.cbresponsavel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbresponsavel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbresponsavel.FormattingEnabled = true;
            this.cbresponsavel.Location = new System.Drawing.Point(754, 72);
            this.cbresponsavel.Margin = new System.Windows.Forms.Padding(2);
            this.cbresponsavel.Name = "cbresponsavel";
            this.cbresponsavel.Size = new System.Drawing.Size(168, 27);
            this.cbresponsavel.TabIndex = 19;
            // 
            // lresponsavel
            // 
            this.lresponsavel.AutoSize = true;
            this.lresponsavel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lresponsavel.Location = new System.Drawing.Point(752, 53);
            this.lresponsavel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lresponsavel.Name = "lresponsavel";
            this.lresponsavel.Size = new System.Drawing.Size(88, 17);
            this.lresponsavel.TabIndex = 18;
            this.lresponsavel.Text = "Responsável:";
            // 
            // lfornecedore
            // 
            this.lfornecedore.AutoSize = true;
            this.lfornecedore.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfornecedore.Location = new System.Drawing.Point(542, 54);
            this.lfornecedore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lfornecedore.Name = "lfornecedore";
            this.lfornecedore.Size = new System.Drawing.Size(82, 17);
            this.lfornecedore.TabIndex = 20;
            this.lfornecedore.Text = "Fornecedor:";
            // 
            // tbcustouni
            // 
            this.tbcustouni.BackColor = System.Drawing.Color.White;
            this.tbcustouni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbcustouni.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcustouni.Location = new System.Drawing.Point(419, 72);
            this.tbcustouni.Margin = new System.Windows.Forms.Padding(2);
            this.tbcustouni.Name = "tbcustouni";
            this.tbcustouni.Size = new System.Drawing.Size(72, 26);
            this.tbcustouni.TabIndex = 16;
            // 
            // cbfornecedor
            // 
            this.cbfornecedor.BackColor = System.Drawing.Color.White;
            this.cbfornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfornecedor.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbfornecedor.FormattingEnabled = true;
            this.cbfornecedor.Location = new System.Drawing.Point(545, 73);
            this.cbfornecedor.Margin = new System.Windows.Forms.Padding(2);
            this.cbfornecedor.Name = "cbfornecedor";
            this.cbfornecedor.Size = new System.Drawing.Size(182, 27);
            this.cbfornecedor.TabIndex = 21;
            // 
            // nudquantidade
            // 
            this.nudquantidade.BackColor = System.Drawing.Color.White;
            this.nudquantidade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudquantidade.Cursor = System.Windows.Forms.Cursors.Default;
            this.nudquantidade.DecimalPlaces = 2;
            this.nudquantidade.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudquantidade.Location = new System.Drawing.Point(279, 73);
            this.nudquantidade.Margin = new System.Windows.Forms.Padding(2);
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
            this.nudquantidade.Size = new System.Drawing.Size(72, 26);
            this.nudquantidade.TabIndex = 12;
            this.nudquantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bregistrar
            // 
            this.bregistrar.BackColor = System.Drawing.Color.White;
            this.bregistrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.bregistrar.FlatAppearance.BorderSize = 2;
            this.bregistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bregistrar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bregistrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.bregistrar.Location = new System.Drawing.Point(1102, 56);
            this.bregistrar.Margin = new System.Windows.Forms.Padding(2);
            this.bregistrar.Name = "bregistrar";
            this.bregistrar.Size = new System.Drawing.Size(129, 42);
            this.bregistrar.TabIndex = 17;
            this.bregistrar.Text = "Adicionar item";
            this.bregistrar.UseVisualStyleBackColor = false;
            this.bregistrar.Click += new System.EventHandler(this.bregistrar_Click);
            // 
            // lquantidade
            // 
            this.lquantidade.AutoSize = true;
            this.lquantidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lquantidade.Location = new System.Drawing.Point(261, 54);
            this.lquantidade.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lquantidade.Name = "lquantidade";
            this.lquantidade.Size = new System.Drawing.Size(84, 17);
            this.lquantidade.TabIndex = 11;
            this.lquantidade.Text = "Quantidade:";
            // 
            // bfinalizarentrada
            // 
            this.bfinalizarentrada.BackColor = System.Drawing.Color.White;
            this.bfinalizarentrada.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bfinalizarentrada.FlatAppearance.BorderSize = 2;
            this.bfinalizarentrada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bfinalizarentrada.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bfinalizarentrada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bfinalizarentrada.Location = new System.Drawing.Point(1265, 56);
            this.bfinalizarentrada.Margin = new System.Windows.Forms.Padding(2);
            this.bfinalizarentrada.Name = "bfinalizarentrada";
            this.bfinalizarentrada.Size = new System.Drawing.Size(126, 42);
            this.bfinalizarentrada.TabIndex = 23;
            this.bfinalizarentrada.Text = "Finalizar entrada";
            this.bfinalizarentrada.UseVisualStyleBackColor = false;
            this.bfinalizarentrada.Click += new System.EventHandler(this.bfinalizarentrada_Click);
            // 
            // cbproduto
            // 
            this.cbproduto.BackColor = System.Drawing.Color.White;
            this.cbproduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbproduto.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbproduto.ForeColor = System.Drawing.Color.Black;
            this.cbproduto.FormattingEnabled = true;
            this.cbproduto.ItemHeight = 19;
            this.cbproduto.Location = new System.Drawing.Point(83, 73);
            this.cbproduto.Margin = new System.Windows.Forms.Padding(2);
            this.cbproduto.Name = "cbproduto";
            this.cbproduto.Size = new System.Drawing.Size(138, 27);
            this.cbproduto.TabIndex = 10;
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lproduto.Location = new System.Drawing.Point(80, 54);
            this.lproduto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(62, 17);
            this.lproduto.TabIndex = 9;
            this.lproduto.Text = "Produto:";
            // 
            // bremoveritem
            // 
            this.bremoveritem.BackColor = System.Drawing.Color.White;
            this.bremoveritem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bremoveritem.FlatAppearance.BorderSize = 2;
            this.bremoveritem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bremoveritem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bremoveritem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bremoveritem.Location = new System.Drawing.Point(1420, 56);
            this.bremoveritem.Margin = new System.Windows.Forms.Padding(2);
            this.bremoveritem.Name = "bremoveritem";
            this.bremoveritem.Size = new System.Drawing.Size(127, 42);
            this.bremoveritem.TabIndex = 25;
            this.bremoveritem.Text = "Remover item";
            this.bremoveritem.UseVisualStyleBackColor = false;
            this.bremoveritem.Click += new System.EventHandler(this.bremoveritem_Click);
            // 
            // lcustouni
            // 
            this.lcustouni.AutoSize = true;
            this.lcustouni.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lcustouni.Location = new System.Drawing.Point(397, 54);
            this.lcustouni.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lcustouni.Name = "lcustouni";
            this.lcustouni.Size = new System.Drawing.Size(104, 17);
            this.lcustouni.TabIndex = 15;
            this.lcustouni.Text = "Custo por unid:";
            // 
            // pmovimentoestoque
            // 
            this.pmovimentoestoque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pmovimentoestoque.Controls.Add(this.lcustouni);
            this.pmovimentoestoque.Controls.Add(this.bremoveritem);
            this.pmovimentoestoque.Controls.Add(this.lproduto);
            this.pmovimentoestoque.Controls.Add(this.cbproduto);
            this.pmovimentoestoque.Controls.Add(this.bfinalizarentrada);
            this.pmovimentoestoque.Controls.Add(this.lquantidade);
            this.pmovimentoestoque.Controls.Add(this.bregistrar);
            this.pmovimentoestoque.Controls.Add(this.nudquantidade);
            this.pmovimentoestoque.Controls.Add(this.cbfornecedor);
            this.pmovimentoestoque.Controls.Add(this.tbcustouni);
            this.pmovimentoestoque.Controls.Add(this.lfornecedore);
            this.pmovimentoestoque.Controls.Add(this.lresponsavel);
            this.pmovimentoestoque.Controls.Add(this.cbresponsavel);
            this.pmovimentoestoque.Dock = System.Windows.Forms.DockStyle.Top;
            this.pmovimentoestoque.Location = new System.Drawing.Point(0, 0);
            this.pmovimentoestoque.Margin = new System.Windows.Forms.Padding(2);
            this.pmovimentoestoque.Name = "pmovimentoestoque";
            this.pmovimentoestoque.Size = new System.Drawing.Size(1834, 145);
            this.pmovimentoestoque.TabIndex = 26;
            // 
            // ucmovimentoestoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lhistorico);
            this.Controls.Add(this.litensentrada);
            this.Controls.Add(this.pmovimentoestoque);
            this.Controls.Add(this.dgvitensentrada);
            this.Controls.Add(this.dgvmovimentacoes);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucmovimentoestoque";
            this.Size = new System.Drawing.Size(1834, 1082);
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensentrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).EndInit();
            this.pmovimentoestoque.ResumeLayout(false);
            this.pmovimentoestoque.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvmovimentacoes;
        private System.Windows.Forms.DataGridView dgvitensentrada;
        private System.Windows.Forms.Label litensentrada;
        private System.Windows.Forms.Label lhistorico;
        private System.Windows.Forms.ComboBox cbresponsavel;
        private System.Windows.Forms.Label lresponsavel;
        private System.Windows.Forms.Label lfornecedore;
        private System.Windows.Forms.TextBox tbcustouni;
        private System.Windows.Forms.ComboBox cbfornecedor;
        private System.Windows.Forms.NumericUpDown nudquantidade;
        private System.Windows.Forms.Button bregistrar;
        private System.Windows.Forms.Label lquantidade;
        private System.Windows.Forms.Button bfinalizarentrada;
        private System.Windows.Forms.ComboBox cbproduto;
        private System.Windows.Forms.Label lproduto;
        private System.Windows.Forms.Button bremoveritem;
        private System.Windows.Forms.Label lcustouni;
        private System.Windows.Forms.Panel pmovimentoestoque;
    }
}

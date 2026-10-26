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
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensentrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvmovimentacoes
            // 
            this.dgvmovimentacoes.AllowUserToAddRows = false;
            this.dgvmovimentacoes.AllowUserToDeleteRows = false;
            this.dgvmovimentacoes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvmovimentacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvmovimentacoes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvmovimentacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvmovimentacoes.Location = new System.Drawing.Point(1229, 286);
            this.dgvmovimentacoes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvmovimentacoes.MultiSelect = false;
            this.dgvmovimentacoes.Name = "dgvmovimentacoes";
            this.dgvmovimentacoes.ReadOnly = true;
            this.dgvmovimentacoes.RowHeadersWidth = 51;
            this.dgvmovimentacoes.RowTemplate.Height = 24;
            this.dgvmovimentacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmovimentacoes.Size = new System.Drawing.Size(1181, 993);
            this.dgvmovimentacoes.TabIndex = 22;
            // 
            // dgvitensentrada
            // 
            this.dgvitensentrada.AllowUserToAddRows = false;
            this.dgvitensentrada.AllowUserToDeleteRows = false;
            this.dgvitensentrada.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvitensentrada.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvitensentrada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvitensentrada.Location = new System.Drawing.Point(36, 286);
            this.dgvitensentrada.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvitensentrada.MultiSelect = false;
            this.dgvitensentrada.Name = "dgvitensentrada";
            this.dgvitensentrada.ReadOnly = true;
            this.dgvitensentrada.RowHeadersWidth = 51;
            this.dgvitensentrada.RowTemplate.Height = 24;
            this.dgvitensentrada.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvitensentrada.Size = new System.Drawing.Size(1181, 993);
            this.dgvitensentrada.TabIndex = 24;
            // 
            // litensentrada
            // 
            this.litensentrada.AutoSize = true;
            this.litensentrada.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.litensentrada.Location = new System.Drawing.Point(31, 257);
            this.litensentrada.Name = "litensentrada";
            this.litensentrada.Size = new System.Drawing.Size(225, 28);
            this.litensentrada.TabIndex = 27;
            this.litensentrada.Text = "Itens da entrada atual:";
            // 
            // lhistorico
            // 
            this.lhistorico.AutoSize = true;
            this.lhistorico.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lhistorico.Location = new System.Drawing.Point(1225, 257);
            this.lhistorico.Name = "lhistorico";
            this.lhistorico.Size = new System.Drawing.Size(220, 28);
            this.lhistorico.TabIndex = 28;
            this.lhistorico.Text = "Histórico de entradas:";
            // 
            // cbresponsavel
            // 
            this.cbresponsavel.BackColor = System.Drawing.Color.White;
            this.cbresponsavel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbresponsavel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbresponsavel.FormattingEnabled = true;
            this.cbresponsavel.Location = new System.Drawing.Point(1006, 89);
            this.cbresponsavel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbresponsavel.Name = "cbresponsavel";
            this.cbresponsavel.Size = new System.Drawing.Size(223, 31);
            this.cbresponsavel.TabIndex = 19;
            // 
            // lresponsavel
            // 
            this.lresponsavel.AutoSize = true;
            this.lresponsavel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lresponsavel.Location = new System.Drawing.Point(1002, 65);
            this.lresponsavel.Name = "lresponsavel";
            this.lresponsavel.Size = new System.Drawing.Size(109, 21);
            this.lresponsavel.TabIndex = 18;
            this.lresponsavel.Text = "Responsável:";
            // 
            // lfornecedore
            // 
            this.lfornecedore.AutoSize = true;
            this.lfornecedore.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfornecedore.Location = new System.Drawing.Point(723, 66);
            this.lfornecedore.Name = "lfornecedore";
            this.lfornecedore.Size = new System.Drawing.Size(101, 21);
            this.lfornecedore.TabIndex = 20;
            this.lfornecedore.Text = "Fornecedor:";
            // 
            // tbcustouni
            // 
            this.tbcustouni.BackColor = System.Drawing.Color.White;
            this.tbcustouni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbcustouni.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcustouni.Location = new System.Drawing.Point(559, 89);
            this.tbcustouni.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbcustouni.Name = "tbcustouni";
            this.tbcustouni.Size = new System.Drawing.Size(96, 30);
            this.tbcustouni.TabIndex = 16;
            // 
            // cbfornecedor
            // 
            this.cbfornecedor.BackColor = System.Drawing.Color.White;
            this.cbfornecedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfornecedor.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbfornecedor.FormattingEnabled = true;
            this.cbfornecedor.Location = new System.Drawing.Point(727, 90);
            this.cbfornecedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbfornecedor.Name = "cbfornecedor";
            this.cbfornecedor.Size = new System.Drawing.Size(242, 31);
            this.cbfornecedor.TabIndex = 21;
            // 
            // nudquantidade
            // 
            this.nudquantidade.BackColor = System.Drawing.Color.White;
            this.nudquantidade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudquantidade.Cursor = System.Windows.Forms.Cursors.Default;
            this.nudquantidade.DecimalPlaces = 2;
            this.nudquantidade.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudquantidade.Location = new System.Drawing.Point(372, 90);
            this.nudquantidade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.nudquantidade.Size = new System.Drawing.Size(96, 30);
            this.nudquantidade.TabIndex = 12;
            this.nudquantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bregistrar
            // 
            this.bregistrar.BackColor = System.Drawing.Color.Transparent;
            this.bregistrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.bregistrar.FlatAppearance.BorderSize = 2;
            this.bregistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bregistrar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bregistrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.bregistrar.Location = new System.Drawing.Point(1470, 69);
            this.bregistrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bregistrar.Name = "bregistrar";
            this.bregistrar.Size = new System.Drawing.Size(172, 52);
            this.bregistrar.TabIndex = 17;
            this.bregistrar.Text = "Adicionar item";
            this.bregistrar.UseVisualStyleBackColor = false;
            this.bregistrar.Click += new System.EventHandler(this.bregistrar_Click);
            // 
            // lquantidade
            // 
            this.lquantidade.AutoSize = true;
            this.lquantidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lquantidade.Location = new System.Drawing.Point(348, 66);
            this.lquantidade.Name = "lquantidade";
            this.lquantidade.Size = new System.Drawing.Size(104, 21);
            this.lquantidade.TabIndex = 11;
            this.lquantidade.Text = "Quantidade:";
            // 
            // bfinalizarentrada
            // 
            this.bfinalizarentrada.BackColor = System.Drawing.Color.Transparent;
            this.bfinalizarentrada.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bfinalizarentrada.FlatAppearance.BorderSize = 2;
            this.bfinalizarentrada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bfinalizarentrada.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bfinalizarentrada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bfinalizarentrada.Location = new System.Drawing.Point(1687, 69);
            this.bfinalizarentrada.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bfinalizarentrada.Name = "bfinalizarentrada";
            this.bfinalizarentrada.Size = new System.Drawing.Size(168, 52);
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
            this.cbproduto.ItemHeight = 23;
            this.cbproduto.Location = new System.Drawing.Point(111, 90);
            this.cbproduto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbproduto.Name = "cbproduto";
            this.cbproduto.Size = new System.Drawing.Size(183, 31);
            this.cbproduto.TabIndex = 10;
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lproduto.Location = new System.Drawing.Point(107, 66);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(76, 21);
            this.lproduto.TabIndex = 9;
            this.lproduto.Text = "Produto:";
            // 
            // bremoveritem
            // 
            this.bremoveritem.BackColor = System.Drawing.Color.Transparent;
            this.bremoveritem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bremoveritem.FlatAppearance.BorderSize = 2;
            this.bremoveritem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bremoveritem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bremoveritem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bremoveritem.Location = new System.Drawing.Point(1894, 69);
            this.bremoveritem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bremoveritem.Name = "bremoveritem";
            this.bremoveritem.Size = new System.Drawing.Size(169, 52);
            this.bremoveritem.TabIndex = 25;
            this.bremoveritem.Text = "Remover item";
            this.bremoveritem.UseVisualStyleBackColor = false;
            this.bremoveritem.Click += new System.EventHandler(this.bremoveritem_Click);
            // 
            // lcustouni
            // 
            this.lcustouni.AutoSize = true;
            this.lcustouni.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lcustouni.Location = new System.Drawing.Point(529, 66);
            this.lcustouni.Name = "lcustouni";
            this.lcustouni.Size = new System.Drawing.Size(126, 21);
            this.lcustouni.TabIndex = 15;
            this.lcustouni.Text = "Custo por unid:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.lcustouni);
            this.panel1.Controls.Add(this.bremoveritem);
            this.panel1.Controls.Add(this.lproduto);
            this.panel1.Controls.Add(this.cbproduto);
            this.panel1.Controls.Add(this.bfinalizarentrada);
            this.panel1.Controls.Add(this.lquantidade);
            this.panel1.Controls.Add(this.bregistrar);
            this.panel1.Controls.Add(this.nudquantidade);
            this.panel1.Controls.Add(this.cbfornecedor);
            this.panel1.Controls.Add(this.tbcustouni);
            this.panel1.Controls.Add(this.lfornecedore);
            this.panel1.Controls.Add(this.lresponsavel);
            this.panel1.Controls.Add(this.cbresponsavel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2445, 178);
            this.panel1.TabIndex = 26;
            // 
            // ucmovimentoestoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lhistorico);
            this.Controls.Add(this.litensentrada);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvitensentrada);
            this.Controls.Add(this.dgvmovimentacoes);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucmovimentoestoque";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvmovimentacoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensentrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
    }
}

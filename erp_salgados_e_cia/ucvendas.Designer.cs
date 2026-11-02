namespace erp_salgados_e_cia
{
    partial class ucvendas
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
            this.lprodutos = new System.Windows.Forms.Label();
            this.cbproduto = new System.Windows.Forms.ComboBox();
            this.lquantidade = new System.Windows.Forms.Label();
            this.nudquantidade = new System.Windows.Forms.NumericUpDown();
            this.badicionaritem = new System.Windows.Forms.Button();
            this.litensdavenda = new System.Windows.Forms.Label();
            this.dgvitensvenda = new System.Windows.Forms.DataGridView();
            this.bfinalizarvenda = new System.Windows.Forms.Button();
            this.bremoveritem = new System.Windows.Forms.Button();
            this.ltotalvenda = new System.Windows.Forms.Label();
            this.lpagamento = new System.Windows.Forms.Label();
            this.cbformapagamento = new System.Windows.Forms.ComboBox();
            this.ltroco = new System.Windows.Forms.Label();
            this.tbvalorrecebido = new System.Windows.Forms.TextBox();
            this.lvalorrecebido = new System.Windows.Forms.Label();
            this.lnumeromesa = new System.Windows.Forms.Label();
            this.nudnumeromesa = new System.Windows.Forms.NumericUpDown();
            this.dgvpagamentosvenda = new System.Windows.Forms.DataGridView();
            this.lpagamentosvenda = new System.Windows.Forms.Label();
            this.badicionarpagamento = new System.Windows.Forms.Button();
            this.bremoverpagamento = new System.Windows.Forms.Button();
            this.lpago = new System.Windows.Forms.Label();
            this.lfalta = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensvenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudnumeromesa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpagamentosvenda)).BeginInit();
            this.SuspendLayout();
            // 
            // lprodutos
            // 
            this.lprodutos.AutoSize = true;
            this.lprodutos.Location = new System.Drawing.Point(71, 47);
            this.lprodutos.Name = "lprodutos";
            this.lprodutos.Size = new System.Drawing.Size(57, 16);
            this.lprodutos.TabIndex = 0;
            this.lprodutos.Text = "Produto:";
            // 
            // cbproduto
            // 
            this.cbproduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbproduto.FormattingEnabled = true;
            this.cbproduto.Location = new System.Drawing.Point(134, 45);
            this.cbproduto.Name = "cbproduto";
            this.cbproduto.Size = new System.Drawing.Size(120, 24);
            this.cbproduto.TabIndex = 1;
            // 
            // lquantidade
            // 
            this.lquantidade.AutoSize = true;
            this.lquantidade.Location = new System.Drawing.Point(303, 47);
            this.lquantidade.Name = "lquantidade";
            this.lquantidade.Size = new System.Drawing.Size(80, 16);
            this.lquantidade.TabIndex = 2;
            this.lquantidade.Text = "Quantidade:";
            // 
            // nudquantidade
            // 
            this.nudquantidade.Location = new System.Drawing.Point(389, 47);
            this.nudquantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudquantidade.Name = "nudquantidade";
            this.nudquantidade.Size = new System.Drawing.Size(61, 22);
            this.nudquantidade.TabIndex = 3;
            this.nudquantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // badicionaritem
            // 
            this.badicionaritem.Location = new System.Drawing.Point(480, 44);
            this.badicionaritem.Name = "badicionaritem";
            this.badicionaritem.Size = new System.Drawing.Size(112, 23);
            this.badicionaritem.TabIndex = 5;
            this.badicionaritem.Text = "Adicionar item";
            this.badicionaritem.UseVisualStyleBackColor = true;
            this.badicionaritem.Click += new System.EventHandler(this.badicionaritem_Click);
            // 
            // litensdavenda
            // 
            this.litensdavenda.AutoSize = true;
            this.litensdavenda.Location = new System.Drawing.Point(108, 248);
            this.litensdavenda.Name = "litensdavenda";
            this.litensdavenda.Size = new System.Drawing.Size(98, 16);
            this.litensdavenda.TabIndex = 6;
            this.litensdavenda.Text = "Itens da venda:";
            // 
            // dgvitensvenda
            // 
            this.dgvitensvenda.AllowUserToAddRows = false;
            this.dgvitensvenda.AllowUserToDeleteRows = false;
            this.dgvitensvenda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvitensvenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvitensvenda.Location = new System.Drawing.Point(111, 290);
            this.dgvitensvenda.Name = "dgvitensvenda";
            this.dgvitensvenda.ReadOnly = true;
            this.dgvitensvenda.RowHeadersWidth = 51;
            this.dgvitensvenda.RowTemplate.Height = 24;
            this.dgvitensvenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvitensvenda.Size = new System.Drawing.Size(522, 275);
            this.dgvitensvenda.TabIndex = 7;
            // 
            // bfinalizarvenda
            // 
            this.bfinalizarvenda.Location = new System.Drawing.Point(722, 47);
            this.bfinalizarvenda.Name = "bfinalizarvenda";
            this.bfinalizarvenda.Size = new System.Drawing.Size(106, 23);
            this.bfinalizarvenda.TabIndex = 8;
            this.bfinalizarvenda.Text = "Finalizar venda";
            this.bfinalizarvenda.UseVisualStyleBackColor = true;
            this.bfinalizarvenda.Click += new System.EventHandler(this.bfinalizarvenda_Click);
            // 
            // bremoveritem
            // 
            this.bremoveritem.Location = new System.Drawing.Point(598, 45);
            this.bremoveritem.Name = "bremoveritem";
            this.bremoveritem.Size = new System.Drawing.Size(118, 23);
            this.bremoveritem.TabIndex = 9;
            this.bremoveritem.Text = "Remover item";
            this.bremoveritem.UseVisualStyleBackColor = true;
            this.bremoveritem.Click += new System.EventHandler(this.bremoveritem_Click);
            // 
            // ltotalvenda
            // 
            this.ltotalvenda.AutoSize = true;
            this.ltotalvenda.Location = new System.Drawing.Point(131, 819);
            this.ltotalvenda.Name = "ltotalvenda";
            this.ltotalvenda.Size = new System.Drawing.Size(145, 16);
            this.ltotalvenda.TabIndex = 11;
            this.ltotalvenda.Text = "Total da venda: R$0,00";
            // 
            // lpagamento
            // 
            this.lpagamento.AutoSize = true;
            this.lpagamento.Location = new System.Drawing.Point(117, 742);
            this.lpagamento.Name = "lpagamento";
            this.lpagamento.Size = new System.Drawing.Size(140, 16);
            this.lpagamento.TabIndex = 12;
            this.lpagamento.Text = "Forma de pagamento:";
            // 
            // cbformapagamento
            // 
            this.cbformapagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbformapagamento.FormattingEnabled = true;
            this.cbformapagamento.Items.AddRange(new object[] {
            "Dinheiro",
            "Pix",
            "Cartão",
            "Notinha"});
            this.cbformapagamento.Location = new System.Drawing.Point(263, 739);
            this.cbformapagamento.Name = "cbformapagamento";
            this.cbformapagamento.Size = new System.Drawing.Size(120, 24);
            this.cbformapagamento.TabIndex = 13;
            this.cbformapagamento.SelectedIndexChanged += new System.EventHandler(this.cbformapagamento_SelectedIndexChanged);
            // 
            // ltroco
            // 
            this.ltroco.AutoSize = true;
            this.ltroco.Location = new System.Drawing.Point(641, 819);
            this.ltroco.Name = "ltroco";
            this.ltroco.Size = new System.Drawing.Size(93, 16);
            this.ltroco.TabIndex = 15;
            this.ltroco.Text = "Troco: R$ 0,00";
            // 
            // tbvalorrecebido
            // 
            this.tbvalorrecebido.Location = new System.Drawing.Point(429, 813);
            this.tbvalorrecebido.Name = "tbvalorrecebido";
            this.tbvalorrecebido.Size = new System.Drawing.Size(100, 22);
            this.tbvalorrecebido.TabIndex = 16;
            this.tbvalorrecebido.TextChanged += new System.EventHandler(this.tbvalorrecebido_TextChanged);
            // 
            // lvalorrecebido
            // 
            this.lvalorrecebido.AutoSize = true;
            this.lvalorrecebido.Location = new System.Drawing.Point(332, 794);
            this.lvalorrecebido.Name = "lvalorrecebido";
            this.lvalorrecebido.Size = new System.Drawing.Size(99, 16);
            this.lvalorrecebido.TabIndex = 17;
            this.lvalorrecebido.Text = "Valor recebido:";
            // 
            // lnumeromesa
            // 
            this.lnumeromesa.AutoSize = true;
            this.lnumeromesa.Location = new System.Drawing.Point(402, 92);
            this.lnumeromesa.Name = "lnumeromesa";
            this.lnumeromesa.Size = new System.Drawing.Size(44, 16);
            this.lnumeromesa.TabIndex = 19;
            this.lnumeromesa.Text = "Mesa:";
            // 
            // nudnumeromesa
            // 
            this.nudnumeromesa.Location = new System.Drawing.Point(452, 89);
            this.nudnumeromesa.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudnumeromesa.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudnumeromesa.Name = "nudnumeromesa";
            this.nudnumeromesa.Size = new System.Drawing.Size(42, 22);
            this.nudnumeromesa.TabIndex = 20;
            this.nudnumeromesa.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dgvpagamentosvenda
            // 
            this.dgvpagamentosvenda.AllowUserToAddRows = false;
            this.dgvpagamentosvenda.AllowUserToDeleteRows = false;
            this.dgvpagamentosvenda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvpagamentosvenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpagamentosvenda.Location = new System.Drawing.Point(960, 406);
            this.dgvpagamentosvenda.Name = "dgvpagamentosvenda";
            this.dgvpagamentosvenda.ReadOnly = true;
            this.dgvpagamentosvenda.RowHeadersWidth = 51;
            this.dgvpagamentosvenda.RowTemplate.Height = 24;
            this.dgvpagamentosvenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvpagamentosvenda.Size = new System.Drawing.Size(522, 275);
            this.dgvpagamentosvenda.TabIndex = 21;
            // 
            // lpagamentosvenda
            // 
            this.lpagamentosvenda.AutoSize = true;
            this.lpagamentosvenda.Location = new System.Drawing.Point(957, 371);
            this.lpagamentosvenda.Name = "lpagamentosvenda";
            this.lpagamentosvenda.Size = new System.Drawing.Size(147, 16);
            this.lpagamentosvenda.TabIndex = 22;
            this.lpagamentosvenda.Text = "Pagamentos da venda:";
            // 
            // badicionarpagamento
            // 
            this.badicionarpagamento.Location = new System.Drawing.Point(1222, 354);
            this.badicionarpagamento.Name = "badicionarpagamento";
            this.badicionarpagamento.Size = new System.Drawing.Size(154, 23);
            this.badicionarpagamento.TabIndex = 23;
            this.badicionarpagamento.Text = "Adicionar pagamento";
            this.badicionarpagamento.UseVisualStyleBackColor = true;
            this.badicionarpagamento.Click += new System.EventHandler(this.badicionarpagamento_Click);
            // 
            // bremoverpagamento
            // 
            this.bremoverpagamento.Location = new System.Drawing.Point(1456, 354);
            this.bremoverpagamento.Name = "bremoverpagamento";
            this.bremoverpagamento.Size = new System.Drawing.Size(174, 23);
            this.bremoverpagamento.TabIndex = 24;
            this.bremoverpagamento.Text = "Remover pagamento";
            this.bremoverpagamento.UseVisualStyleBackColor = true;
            this.bremoverpagamento.Click += new System.EventHandler(this.bremoverpagamento_Click);
            // 
            // lpago
            // 
            this.lpago.AutoSize = true;
            this.lpago.Location = new System.Drawing.Point(1051, 727);
            this.lpago.Name = "lpago";
            this.lpago.Size = new System.Drawing.Size(90, 16);
            this.lpago.TabIndex = 25;
            this.lpago.Text = "Pago: R$ 0,00";
            // 
            // lfalta
            // 
            this.lfalta.AutoSize = true;
            this.lfalta.Location = new System.Drawing.Point(1219, 742);
            this.lfalta.Name = "lfalta";
            this.lfalta.Size = new System.Drawing.Size(87, 16);
            this.lfalta.TabIndex = 26;
            this.lfalta.Text = "Falta: R$ 0,00";
            // 
            // ucvendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lfalta);
            this.Controls.Add(this.lpago);
            this.Controls.Add(this.bremoverpagamento);
            this.Controls.Add(this.badicionarpagamento);
            this.Controls.Add(this.lpagamentosvenda);
            this.Controls.Add(this.dgvpagamentosvenda);
            this.Controls.Add(this.nudnumeromesa);
            this.Controls.Add(this.lnumeromesa);
            this.Controls.Add(this.lvalorrecebido);
            this.Controls.Add(this.tbvalorrecebido);
            this.Controls.Add(this.ltroco);
            this.Controls.Add(this.cbformapagamento);
            this.Controls.Add(this.lpagamento);
            this.Controls.Add(this.ltotalvenda);
            this.Controls.Add(this.bremoveritem);
            this.Controls.Add(this.bfinalizarvenda);
            this.Controls.Add(this.dgvitensvenda);
            this.Controls.Add(this.litensdavenda);
            this.Controls.Add(this.badicionaritem);
            this.Controls.Add(this.nudquantidade);
            this.Controls.Add(this.lquantidade);
            this.Controls.Add(this.cbproduto);
            this.Controls.Add(this.lprodutos);
            this.Name = "ucvendas";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensvenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudnumeromesa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpagamentosvenda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lprodutos;
        private System.Windows.Forms.ComboBox cbproduto;
        private System.Windows.Forms.Label lquantidade;
        private System.Windows.Forms.NumericUpDown nudquantidade;
        private System.Windows.Forms.Button badicionaritem;
        private System.Windows.Forms.Label litensdavenda;
        private System.Windows.Forms.DataGridView dgvitensvenda;
        private System.Windows.Forms.Button bfinalizarvenda;
        private System.Windows.Forms.Button bremoveritem;
        private System.Windows.Forms.Label ltotalvenda;
        private System.Windows.Forms.Label lpagamento;
        private System.Windows.Forms.ComboBox cbformapagamento;
        private System.Windows.Forms.Label ltroco;
        private System.Windows.Forms.TextBox tbvalorrecebido;
        private System.Windows.Forms.Label lvalorrecebido;
        private System.Windows.Forms.Label lnumeromesa;
        private System.Windows.Forms.NumericUpDown nudnumeromesa;
        private System.Windows.Forms.DataGridView dgvpagamentosvenda;
        private System.Windows.Forms.Label lpagamentosvenda;
        private System.Windows.Forms.Button badicionarpagamento;
        private System.Windows.Forms.Button bremoverpagamento;
        private System.Windows.Forms.Label lpago;
        private System.Windows.Forms.Label lfalta;
    }
}

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
            this.lnumeromesa = new System.Windows.Forms.Label();
            this.nudnumeromesa = new System.Windows.Forms.NumericUpDown();
            this.lcliente = new System.Windows.Forms.Label();
            this.cbcliente = new System.Windows.Forms.ComboBox();
            this.ltotalvenda = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensvenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudnumeromesa)).BeginInit();
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
            // lcliente
            // 
            this.lcliente.AutoSize = true;
            this.lcliente.Location = new System.Drawing.Point(690, 121);
            this.lcliente.Name = "lcliente";
            this.lcliente.Size = new System.Drawing.Size(51, 16);
            this.lcliente.TabIndex = 27;
            this.lcliente.Text = "Cliente:";
            // 
            // cbcliente
            // 
            this.cbcliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcliente.FormattingEnabled = true;
            this.cbcliente.Location = new System.Drawing.Point(740, 118);
            this.cbcliente.Name = "cbcliente";
            this.cbcliente.Size = new System.Drawing.Size(120, 24);
            this.cbcliente.TabIndex = 28;
            // 
            // ltotalvenda
            // 
            this.ltotalvenda.AutoSize = true;
            this.ltotalvenda.Location = new System.Drawing.Point(109, 152);
            this.ltotalvenda.Name = "ltotalvenda";
            this.ltotalvenda.Size = new System.Drawing.Size(145, 16);
            this.ltotalvenda.TabIndex = 29;
            this.ltotalvenda.Text = "Total da venda: R$0,00";
            // 
            // ucvendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ltotalvenda);
            this.Controls.Add(this.cbcliente);
            this.Controls.Add(this.lcliente);
            this.Controls.Add(this.nudnumeromesa);
            this.Controls.Add(this.lnumeromesa);
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
        private System.Windows.Forms.Label lnumeromesa;
        private System.Windows.Forms.NumericUpDown nudnumeromesa;
        private System.Windows.Forms.Label lcliente;
        private System.Windows.Forms.ComboBox cbcliente;
        private System.Windows.Forms.Label ltotalvenda;
    }
}

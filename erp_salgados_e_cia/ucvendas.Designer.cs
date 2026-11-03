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
            this.lcomandamesa = new System.Windows.Forms.Label();
            this.dgvitenscomanda = new System.Windows.Forms.DataGridView();
            this.lsemcomanda = new System.Windows.Forms.Label();
            this.ltotalcomanda = new System.Windows.Forms.Label();
            this.bcobrar = new System.Windows.Forms.Button();
            this.bcancelarcomanda = new System.Windows.Forms.Button();
            this.lfaltacomanda = new System.Windows.Forms.Label();
            this.lpagocomanda = new System.Windows.Forms.Label();
            this.bmesasabertas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensvenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudnumeromesa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitenscomanda)).BeginInit();
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
            this.litensdavenda.Location = new System.Drawing.Point(108, 259);
            this.litensdavenda.Name = "litensdavenda";
            this.litensdavenda.Size = new System.Drawing.Size(103, 16);
            this.litensdavenda.TabIndex = 6;
            this.litensdavenda.Text = "Itens do pedido:";
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
            this.bfinalizarvenda.Text = "Enviar pedido";
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
            this.nudnumeromesa.Name = "nudnumeromesa";
            this.nudnumeromesa.Size = new System.Drawing.Size(42, 22);
            this.nudnumeromesa.TabIndex = 20;
            this.nudnumeromesa.ThousandsSeparator = true;
            this.nudnumeromesa.ValueChanged += new System.EventHandler(this.nudnumeromesa_ValueChanged);
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
            this.cbcliente.SelectionChangeCommitted += new System.EventHandler(this.cbcliente_SelectionChangeCommitted);
            // 
            // ltotalvenda
            // 
            this.ltotalvenda.AutoSize = true;
            this.ltotalvenda.Location = new System.Drawing.Point(108, 590);
            this.ltotalvenda.Name = "ltotalvenda";
            this.ltotalvenda.Size = new System.Drawing.Size(153, 16);
            this.ltotalvenda.TabIndex = 29;
            this.ltotalvenda.Text = "Total do pedido: R$ 0,00";
            // 
            // lcomandamesa
            // 
            this.lcomandamesa.AutoSize = true;
            this.lcomandamesa.Location = new System.Drawing.Point(1305, 259);
            this.lcomandamesa.Name = "lcomandamesa";
            this.lcomandamesa.Size = new System.Drawing.Size(120, 16);
            this.lcomandamesa.TabIndex = 30;
            this.lcomandamesa.Text = "Comanda - Mesa 1";
            // 
            // dgvitenscomanda
            // 
            this.dgvitenscomanda.AllowUserToAddRows = false;
            this.dgvitenscomanda.AllowUserToDeleteRows = false;
            this.dgvitenscomanda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvitenscomanda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvitenscomanda.Location = new System.Drawing.Point(1308, 301);
            this.dgvitenscomanda.Name = "dgvitenscomanda";
            this.dgvitenscomanda.ReadOnly = true;
            this.dgvitenscomanda.RowHeadersWidth = 51;
            this.dgvitenscomanda.RowTemplate.Height = 24;
            this.dgvitenscomanda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvitenscomanda.Size = new System.Drawing.Size(522, 275);
            this.dgvitenscomanda.TabIndex = 31;
            // 
            // lsemcomanda
            // 
            this.lsemcomanda.AutoSize = true;
            this.lsemcomanda.Location = new System.Drawing.Point(1018, 523);
            this.lsemcomanda.Name = "lsemcomanda";
            this.lsemcomanda.Size = new System.Drawing.Size(172, 16);
            this.lsemcomanda.TabIndex = 32;
            this.lsemcomanda.Text = "Mesa sem comanda aberta";
            // 
            // ltotalcomanda
            // 
            this.ltotalcomanda.AutoSize = true;
            this.ltotalcomanda.Location = new System.Drawing.Point(1323, 605);
            this.ltotalcomanda.Name = "ltotalcomanda";
            this.ltotalcomanda.Size = new System.Drawing.Size(88, 16);
            this.ltotalcomanda.TabIndex = 33;
            this.ltotalcomanda.Text = "Total: R$ 0,00";
            // 
            // bcobrar
            // 
            this.bcobrar.Location = new System.Drawing.Point(1290, 767);
            this.bcobrar.Name = "bcobrar";
            this.bcobrar.Size = new System.Drawing.Size(135, 23);
            this.bcobrar.TabIndex = 34;
            this.bcobrar.Text = "Cobrar comanda";
            this.bcobrar.UseVisualStyleBackColor = true;
            this.bcobrar.Click += new System.EventHandler(this.bcobrar_Click);
            // 
            // bcancelarcomanda
            // 
            this.bcancelarcomanda.Location = new System.Drawing.Point(1466, 767);
            this.bcancelarcomanda.Name = "bcancelarcomanda";
            this.bcancelarcomanda.Size = new System.Drawing.Size(144, 23);
            this.bcancelarcomanda.TabIndex = 35;
            this.bcancelarcomanda.Text = "Cancelar comanda";
            this.bcancelarcomanda.UseVisualStyleBackColor = true;
            this.bcancelarcomanda.Click += new System.EventHandler(this.bcancelarcomanda_Click);
            // 
            // lfaltacomanda
            // 
            this.lfaltacomanda.AutoSize = true;
            this.lfaltacomanda.Location = new System.Drawing.Point(1656, 621);
            this.lfaltacomanda.Name = "lfaltacomanda";
            this.lfaltacomanda.Size = new System.Drawing.Size(87, 16);
            this.lfaltacomanda.TabIndex = 36;
            this.lfaltacomanda.Text = "Falta: R$ 0,00";
            // 
            // lpagocomanda
            // 
            this.lpagocomanda.AutoSize = true;
            this.lpagocomanda.Location = new System.Drawing.Point(1482, 621);
            this.lpagocomanda.Name = "lpagocomanda";
            this.lpagocomanda.Size = new System.Drawing.Size(90, 16);
            this.lpagocomanda.TabIndex = 37;
            this.lpagocomanda.Text = "Pago: R$ 0,00";
            // 
            // bmesasabertas
            // 
            this.bmesasabertas.Location = new System.Drawing.Point(1366, 888);
            this.bmesasabertas.Name = "bmesasabertas";
            this.bmesasabertas.Size = new System.Drawing.Size(135, 23);
            this.bmesasabertas.TabIndex = 38;
            this.bmesasabertas.Text = "Mesas abertas";
            this.bmesasabertas.UseVisualStyleBackColor = true;
            this.bmesasabertas.Click += new System.EventHandler(this.bmesasabertas_Click);
            // 
            // ucvendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bmesasabertas);
            this.Controls.Add(this.lpagocomanda);
            this.Controls.Add(this.lfaltacomanda);
            this.Controls.Add(this.bcancelarcomanda);
            this.Controls.Add(this.bcobrar);
            this.Controls.Add(this.ltotalcomanda);
            this.Controls.Add(this.lsemcomanda);
            this.Controls.Add(this.dgvitenscomanda);
            this.Controls.Add(this.lcomandamesa);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvitenscomanda)).EndInit();
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
        private System.Windows.Forms.Label lcomandamesa;
        private System.Windows.Forms.DataGridView dgvitenscomanda;
        private System.Windows.Forms.Label lsemcomanda;
        private System.Windows.Forms.Label ltotalcomanda;
        private System.Windows.Forms.Button bcobrar;
        private System.Windows.Forms.Button bcancelarcomanda;
        private System.Windows.Forms.Label lfaltacomanda;
        private System.Windows.Forms.Label lpagocomanda;
        private System.Windows.Forms.Button bmesasabertas;
    }
}

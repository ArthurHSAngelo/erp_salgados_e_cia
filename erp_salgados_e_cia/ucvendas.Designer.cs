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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.lavisocaixa = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensvenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudnumeromesa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitenscomanda)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lprodutos
            // 
            this.lprodutos.AutoSize = true;
            this.lprodutos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lprodutos.Location = new System.Drawing.Point(80, 52);
            this.lprodutos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lprodutos.Name = "lprodutos";
            this.lprodutos.Size = new System.Drawing.Size(62, 17);
            this.lprodutos.TabIndex = 0;
            this.lprodutos.Text = "Produto:";
            // 
            // cbproduto
            // 
            this.cbproduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbproduto.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbproduto.FormattingEnabled = true;
            this.cbproduto.Location = new System.Drawing.Point(83, 72);
            this.cbproduto.Margin = new System.Windows.Forms.Padding(2);
            this.cbproduto.Name = "cbproduto";
            this.cbproduto.Size = new System.Drawing.Size(198, 27);
            this.cbproduto.TabIndex = 1;
            // 
            // lquantidade
            // 
            this.lquantidade.AutoSize = true;
            this.lquantidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lquantidade.Location = new System.Drawing.Point(330, 52);
            this.lquantidade.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lquantidade.Name = "lquantidade";
            this.lquantidade.Size = new System.Drawing.Size(84, 17);
            this.lquantidade.TabIndex = 2;
            this.lquantidade.Text = "Quantidade:";
            // 
            // nudquantidade
            // 
            this.nudquantidade.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.nudquantidade.Location = new System.Drawing.Point(333, 72);
            this.nudquantidade.Margin = new System.Windows.Forms.Padding(2);
            this.nudquantidade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudquantidade.Name = "nudquantidade";
            this.nudquantidade.Size = new System.Drawing.Size(54, 26);
            this.nudquantidade.TabIndex = 3;
            this.nudquantidade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // badicionaritem
            // 
            this.badicionaritem.BackColor = System.Drawing.Color.White;
            this.badicionaritem.FlatAppearance.BorderSize = 2;
            this.badicionaritem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.badicionaritem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.badicionaritem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.badicionaritem.Location = new System.Drawing.Point(1226, 62);
            this.badicionaritem.Margin = new System.Windows.Forms.Padding(2);
            this.badicionaritem.Name = "badicionaritem";
            this.badicionaritem.Size = new System.Drawing.Size(129, 42);
            this.badicionaritem.TabIndex = 5;
            this.badicionaritem.Text = "Adicionar item";
            this.badicionaritem.UseVisualStyleBackColor = false;
            this.badicionaritem.Click += new System.EventHandler(this.badicionaritem_Click);
            // 
            // litensdavenda
            // 
            this.litensdavenda.AutoSize = true;
            this.litensdavenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.litensdavenda.Location = new System.Drawing.Point(23, 209);
            this.litensdavenda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.litensdavenda.Name = "litensdavenda";
            this.litensdavenda.Size = new System.Drawing.Size(133, 21);
            this.litensdavenda.TabIndex = 6;
            this.litensdavenda.Text = "Itens do pedido:";
            // 
            // dgvitensvenda
            // 
            this.dgvitensvenda.AllowUserToAddRows = false;
            this.dgvitensvenda.AllowUserToDeleteRows = false;
            this.dgvitensvenda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvitensvenda.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvitensvenda.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvitensvenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvitensvenda.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvitensvenda.Location = new System.Drawing.Point(27, 232);
            this.dgvitensvenda.Margin = new System.Windows.Forms.Padding(2);
            this.dgvitensvenda.Name = "dgvitensvenda";
            this.dgvitensvenda.ReadOnly = true;
            this.dgvitensvenda.RowHeadersWidth = 51;
            this.dgvitensvenda.RowTemplate.Height = 24;
            this.dgvitensvenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvitensvenda.Size = new System.Drawing.Size(886, 807);
            this.dgvitensvenda.TabIndex = 7;
            // 
            // bfinalizarvenda
            // 
            this.bfinalizarvenda.BackColor = System.Drawing.Color.White;
            this.bfinalizarvenda.FlatAppearance.BorderSize = 2;
            this.bfinalizarvenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bfinalizarvenda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bfinalizarvenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bfinalizarvenda.Location = new System.Drawing.Point(1516, 62);
            this.bfinalizarvenda.Margin = new System.Windows.Forms.Padding(2);
            this.bfinalizarvenda.Name = "bfinalizarvenda";
            this.bfinalizarvenda.Size = new System.Drawing.Size(129, 42);
            this.bfinalizarvenda.TabIndex = 8;
            this.bfinalizarvenda.Text = "Enviar pedido";
            this.bfinalizarvenda.UseVisualStyleBackColor = false;
            this.bfinalizarvenda.Click += new System.EventHandler(this.bfinalizarvenda_Click);
            // 
            // bremoveritem
            // 
            this.bremoveritem.BackColor = System.Drawing.Color.White;
            this.bremoveritem.FlatAppearance.BorderSize = 2;
            this.bremoveritem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bremoveritem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bremoveritem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bremoveritem.Location = new System.Drawing.Point(1373, 62);
            this.bremoveritem.Margin = new System.Windows.Forms.Padding(2);
            this.bremoveritem.Name = "bremoveritem";
            this.bremoveritem.Size = new System.Drawing.Size(129, 42);
            this.bremoveritem.TabIndex = 9;
            this.bremoveritem.Text = "Remover item";
            this.bremoveritem.UseVisualStyleBackColor = false;
            this.bremoveritem.Click += new System.EventHandler(this.bremoveritem_Click);
            // 
            // lnumeromesa
            // 
            this.lnumeromesa.AutoSize = true;
            this.lnumeromesa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lnumeromesa.Location = new System.Drawing.Point(464, 52);
            this.lnumeromesa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnumeromesa.Name = "lnumeromesa";
            this.lnumeromesa.Size = new System.Drawing.Size(44, 17);
            this.lnumeromesa.TabIndex = 19;
            this.lnumeromesa.Text = "Mesa:";
            this.lnumeromesa.Click += new System.EventHandler(this.lnumeromesa_Click);
            // 
            // nudnumeromesa
            // 
            this.nudnumeromesa.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.nudnumeromesa.Location = new System.Drawing.Point(467, 72);
            this.nudnumeromesa.Margin = new System.Windows.Forms.Padding(2);
            this.nudnumeromesa.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudnumeromesa.Name = "nudnumeromesa";
            this.nudnumeromesa.Size = new System.Drawing.Size(41, 26);
            this.nudnumeromesa.TabIndex = 20;
            this.nudnumeromesa.ThousandsSeparator = true;
            this.nudnumeromesa.ValueChanged += new System.EventHandler(this.nudnumeromesa_ValueChanged);
            // 
            // lcliente
            // 
            this.lcliente.AutoSize = true;
            this.lcliente.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lcliente.Location = new System.Drawing.Point(585, 52);
            this.lcliente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lcliente.Name = "lcliente";
            this.lcliente.Size = new System.Drawing.Size(55, 17);
            this.lcliente.TabIndex = 27;
            this.lcliente.Text = "Cliente:";
            // 
            // cbcliente
            // 
            this.cbcliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcliente.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbcliente.FormattingEnabled = true;
            this.cbcliente.Location = new System.Drawing.Point(585, 72);
            this.cbcliente.Margin = new System.Windows.Forms.Padding(2);
            this.cbcliente.Name = "cbcliente";
            this.cbcliente.Size = new System.Drawing.Size(91, 27);
            this.cbcliente.TabIndex = 28;
            this.cbcliente.SelectionChangeCommitted += new System.EventHandler(this.cbcliente_SelectionChangeCommitted);
            // 
            // ltotalvenda
            // 
            this.ltotalvenda.AutoSize = true;
            this.ltotalvenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ltotalvenda.Location = new System.Drawing.Point(250, 209);
            this.ltotalvenda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ltotalvenda.Name = "ltotalvenda";
            this.ltotalvenda.Size = new System.Drawing.Size(192, 21);
            this.ltotalvenda.TabIndex = 29;
            this.ltotalvenda.Text = "Total do pedido: R$ 0,00";
            // 
            // lcomandamesa
            // 
            this.lcomandamesa.AutoSize = true;
            this.lcomandamesa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lcomandamesa.Location = new System.Drawing.Point(919, 209);
            this.lcomandamesa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lcomandamesa.Name = "lcomandamesa";
            this.lcomandamesa.Size = new System.Drawing.Size(150, 21);
            this.lcomandamesa.TabIndex = 30;
            this.lcomandamesa.Text = "Comanda - Mesa 1";
            // 
            // dgvitenscomanda
            // 
            this.dgvitenscomanda.AllowUserToAddRows = false;
            this.dgvitenscomanda.AllowUserToDeleteRows = false;
            this.dgvitenscomanda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvitenscomanda.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvitenscomanda.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvitenscomanda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvitenscomanda.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvitenscomanda.Location = new System.Drawing.Point(922, 232);
            this.dgvitenscomanda.Margin = new System.Windows.Forms.Padding(2);
            this.dgvitenscomanda.Name = "dgvitenscomanda";
            this.dgvitenscomanda.ReadOnly = true;
            this.dgvitenscomanda.RowHeadersWidth = 51;
            this.dgvitenscomanda.RowTemplate.Height = 24;
            this.dgvitenscomanda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvitenscomanda.Size = new System.Drawing.Size(886, 562);
            this.dgvitenscomanda.TabIndex = 31;
            // 
            // lsemcomanda
            // 
            this.lsemcomanda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsemcomanda.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsemcomanda.Location = new System.Drawing.Point(922, 232);
            this.lsemcomanda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lsemcomanda.Name = "lsemcomanda";
            this.lsemcomanda.Size = new System.Drawing.Size(886, 667);
            this.lsemcomanda.TabIndex = 32;
            this.lsemcomanda.Text = "Mesa sem comanda aberta";
            this.lsemcomanda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ltotalcomanda
            // 
            this.ltotalcomanda.AutoSize = true;
            this.ltotalcomanda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ltotalcomanda.Location = new System.Drawing.Point(1427, 796);
            this.ltotalcomanda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ltotalcomanda.Name = "ltotalcomanda";
            this.ltotalcomanda.Size = new System.Drawing.Size(110, 21);
            this.ltotalcomanda.TabIndex = 33;
            this.ltotalcomanda.Text = "Total: R$ 0,00";
            // 
            // bcobrar
            // 
            this.bcobrar.FlatAppearance.BorderSize = 2;
            this.bcobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bcobrar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bcobrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bcobrar.Location = new System.Drawing.Point(940, 815);
            this.bcobrar.Margin = new System.Windows.Forms.Padding(2);
            this.bcobrar.Name = "bcobrar";
            this.bcobrar.Size = new System.Drawing.Size(129, 57);
            this.bcobrar.TabIndex = 34;
            this.bcobrar.Text = "Cobrar comanda";
            this.bcobrar.UseVisualStyleBackColor = true;
            this.bcobrar.Click += new System.EventHandler(this.bcobrar_Click);
            // 
            // bcancelarcomanda
            // 
            this.bcancelarcomanda.FlatAppearance.BorderSize = 2;
            this.bcancelarcomanda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bcancelarcomanda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bcancelarcomanda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bcancelarcomanda.Location = new System.Drawing.Point(1084, 815);
            this.bcancelarcomanda.Margin = new System.Windows.Forms.Padding(2);
            this.bcancelarcomanda.Name = "bcancelarcomanda";
            this.bcancelarcomanda.Size = new System.Drawing.Size(129, 57);
            this.bcancelarcomanda.TabIndex = 35;
            this.bcancelarcomanda.Text = "Cancelar comanda";
            this.bcancelarcomanda.UseVisualStyleBackColor = true;
            this.bcancelarcomanda.Click += new System.EventHandler(this.bcancelarcomanda_Click);
            // 
            // lfaltacomanda
            // 
            this.lfaltacomanda.AutoSize = true;
            this.lfaltacomanda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lfaltacomanda.Location = new System.Drawing.Point(1660, 796);
            this.lfaltacomanda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lfaltacomanda.Name = "lfaltacomanda";
            this.lfaltacomanda.Size = new System.Drawing.Size(109, 21);
            this.lfaltacomanda.TabIndex = 36;
            this.lfaltacomanda.Text = "Falta: R$ 0,00";
            // 
            // lpagocomanda
            // 
            this.lpagocomanda.AutoSize = true;
            this.lpagocomanda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lpagocomanda.Location = new System.Drawing.Point(1545, 796);
            this.lpagocomanda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lpagocomanda.Name = "lpagocomanda";
            this.lpagocomanda.Size = new System.Drawing.Size(111, 21);
            this.lpagocomanda.TabIndex = 37;
            this.lpagocomanda.Text = "Pago: R$ 0,00";
            // 
            // bmesasabertas
            // 
            this.bmesasabertas.FlatAppearance.BorderSize = 2;
            this.bmesasabertas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bmesasabertas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bmesasabertas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.bmesasabertas.Location = new System.Drawing.Point(1693, 965);
            this.bmesasabertas.Margin = new System.Windows.Forms.Padding(2);
            this.bmesasabertas.Name = "bmesasabertas";
            this.bmesasabertas.Size = new System.Drawing.Size(129, 57);
            this.bmesasabertas.TabIndex = 38;
            this.bmesasabertas.Text = "Mesas abertas";
            this.bmesasabertas.UseVisualStyleBackColor = true;
            this.bmesasabertas.Click += new System.EventHandler(this.bmesasabertas_Click);
            // 
            // lavisocaixa
            // 
            this.lavisocaixa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lavisocaixa.ForeColor = System.Drawing.Color.Firebrick;
            this.lavisocaixa.Location = new System.Drawing.Point(0, 0);
            this.lavisocaixa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lavisocaixa.Name = "lavisocaixa";
            this.lavisocaixa.Size = new System.Drawing.Size(1834, 145);
            this.lavisocaixa.TabIndex = 39;
            this.lavisocaixa.Text = "O caixa ainda não foi aberto. Abra o caixa para registrar vendas.";
            this.lavisocaixa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.panel2.Controls.Add(this.lavisocaixa);
            this.panel2.Controls.Add(this.lprodutos);
            this.panel2.Controls.Add(this.cbproduto);
            this.panel2.Controls.Add(this.lquantidade);
            this.panel2.Controls.Add(this.nudquantidade);
            this.panel2.Controls.Add(this.lnumeromesa);
            this.panel2.Controls.Add(this.nudnumeromesa);
            this.panel2.Controls.Add(this.lcliente);
            this.panel2.Controls.Add(this.cbcliente);
            this.panel2.Controls.Add(this.badicionaritem);
            this.panel2.Controls.Add(this.bremoveritem);
            this.panel2.Controls.Add(this.bfinalizarvenda);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1834, 145);
            this.panel2.TabIndex = 41;
            // 
            // ucvendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lsemcomanda);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bmesasabertas);
            this.Controls.Add(this.bcobrar);
            this.Controls.Add(this.lpagocomanda);
            this.Controls.Add(this.lfaltacomanda);
            this.Controls.Add(this.ltotalcomanda);
            this.Controls.Add(this.bcancelarcomanda);
            this.Controls.Add(this.dgvitenscomanda);
            this.Controls.Add(this.lcomandamesa);
            this.Controls.Add(this.ltotalvenda);
            this.Controls.Add(this.dgvitensvenda);
            this.Controls.Add(this.litensdavenda);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucvendas";
            this.Size = new System.Drawing.Size(1834, 1082);
            ((System.ComponentModel.ISupportInitialize)(this.nudquantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitensvenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudnumeromesa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitenscomanda)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Label lavisocaixa;
        private System.Windows.Forms.Panel panel2;
    }
}

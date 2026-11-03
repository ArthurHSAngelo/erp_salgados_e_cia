namespace erp_salgados_e_cia
{
    partial class ucclientes
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
            this.cbfiltrostatus = new System.Windows.Forms.ComboBox();
            this.cbativo = new System.Windows.Forms.CheckBox();
            this.beditarc = new System.Windows.Forms.Button();
            this.bexcluirc = new System.Windows.Forms.Button();
            this.tbtelefonec = new System.Windows.Forms.TextBox();
            this.ltelefonecliente = new System.Windows.Forms.Label();
            this.dgvclientes = new System.Windows.Forms.DataGridView();
            this.bsalvarcliente = new System.Windows.Forms.Button();
            this.tbcpfcnpjc = new System.Windows.Forms.TextBox();
            this.tbnomec = new System.Windows.Forms.TextBox();
            this.lcpfcnpjcliente = new System.Windows.Forms.Label();
            this.lnomecliente = new System.Windows.Forms.Label();
            this.lnotinhacliente = new System.Windows.Forms.Label();
            this.dgvhistoriconotinha = new System.Windows.Forms.DataGridView();
            this.lsaldonotinha = new System.Windows.Forms.Label();
            this.lsemclientenotinha = new System.Windows.Forms.Label();
            this.bpagarnotinha = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvclientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvhistoriconotinha)).BeginInit();
            this.SuspendLayout();
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(1005, 274);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrostatus.TabIndex = 32;
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Location = new System.Drawing.Point(855, 196);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(59, 20);
            this.cbativo.TabIndex = 31;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // beditarc
            // 
            this.beditarc.Location = new System.Drawing.Point(681, 245);
            this.beditarc.Name = "beditarc";
            this.beditarc.Size = new System.Drawing.Size(75, 23);
            this.beditarc.TabIndex = 30;
            this.beditarc.Text = "Editar";
            this.beditarc.UseVisualStyleBackColor = true;
            this.beditarc.Click += new System.EventHandler(this.beditarc_Click);
            // 
            // bexcluirc
            // 
            this.bexcluirc.Location = new System.Drawing.Point(825, 275);
            this.bexcluirc.Name = "bexcluirc";
            this.bexcluirc.Size = new System.Drawing.Size(75, 23);
            this.bexcluirc.TabIndex = 29;
            this.bexcluirc.Text = "Excluir";
            this.bexcluirc.UseVisualStyleBackColor = true;
            this.bexcluirc.Click += new System.EventHandler(this.bexcluirc_Click);
            // 
            // tbtelefonec
            // 
            this.tbtelefonec.Location = new System.Drawing.Point(402, 262);
            this.tbtelefonec.Name = "tbtelefonec";
            this.tbtelefonec.Size = new System.Drawing.Size(100, 22);
            this.tbtelefonec.TabIndex = 28;
            // 
            // ltelefonecliente
            // 
            this.ltelefonecliente.AutoSize = true;
            this.ltelefonecliente.Location = new System.Drawing.Point(332, 265);
            this.ltelefonecliente.Name = "ltelefonecliente";
            this.ltelefonecliente.Size = new System.Drawing.Size(64, 16);
            this.ltelefonecliente.TabIndex = 27;
            this.ltelefonecliente.Text = "Telefone:";
            // 
            // dgvclientes
            // 
            this.dgvclientes.AllowUserToAddRows = false;
            this.dgvclientes.AllowUserToDeleteRows = false;
            this.dgvclientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvclientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvclientes.Location = new System.Drawing.Point(360, 321);
            this.dgvclientes.MultiSelect = false;
            this.dgvclientes.Name = "dgvclientes";
            this.dgvclientes.ReadOnly = true;
            this.dgvclientes.RowHeadersWidth = 51;
            this.dgvclientes.RowTemplate.Height = 24;
            this.dgvclientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvclientes.Size = new System.Drawing.Size(737, 191);
            this.dgvclientes.TabIndex = 26;
            // 
            // bsalvarcliente
            // 
            this.bsalvarcliente.Location = new System.Drawing.Point(610, 192);
            this.bsalvarcliente.Name = "bsalvarcliente";
            this.bsalvarcliente.Size = new System.Drawing.Size(75, 23);
            this.bsalvarcliente.TabIndex = 25;
            this.bsalvarcliente.Text = "Salvar";
            this.bsalvarcliente.UseVisualStyleBackColor = true;
            this.bsalvarcliente.Click += new System.EventHandler(this.bsalvarcliente_Click);
            // 
            // tbcpfcnpjc
            // 
            this.tbcpfcnpjc.Location = new System.Drawing.Point(393, 229);
            this.tbcpfcnpjc.Name = "tbcpfcnpjc";
            this.tbcpfcnpjc.Size = new System.Drawing.Size(100, 22);
            this.tbcpfcnpjc.TabIndex = 24;
            // 
            // tbnomec
            // 
            this.tbnomec.Location = new System.Drawing.Point(477, 195);
            this.tbnomec.Name = "tbnomec";
            this.tbnomec.Size = new System.Drawing.Size(100, 22);
            this.tbnomec.TabIndex = 23;
            // 
            // lcpfcnpjcliente
            // 
            this.lcpfcnpjcliente.AutoSize = true;
            this.lcpfcnpjcliente.Location = new System.Drawing.Point(312, 229);
            this.lcpfcnpjcliente.Name = "lcpfcnpjcliente";
            this.lcpfcnpjcliente.Size = new System.Drawing.Size(75, 16);
            this.lcpfcnpjcliente.TabIndex = 22;
            this.lcpfcnpjcliente.Text = "CPF/CNPJ:";
            // 
            // lnomecliente
            // 
            this.lnomecliente.AutoSize = true;
            this.lnomecliente.Location = new System.Drawing.Point(332, 195);
            this.lnomecliente.Name = "lnomecliente";
            this.lnomecliente.Size = new System.Drawing.Size(110, 16);
            this.lnomecliente.TabIndex = 21;
            this.lnomecliente.Text = "Nome do Cliente:";
            // 
            // lnotinhacliente
            // 
            this.lnotinhacliente.AutoSize = true;
            this.lnotinhacliente.Location = new System.Drawing.Point(1305, 259);
            this.lnotinhacliente.Name = "lnotinhacliente";
            this.lnotinhacliente.Size = new System.Drawing.Size(117, 16);
            this.lnotinhacliente.TabIndex = 33;
            this.lnotinhacliente.Text = "Notinha do cliente:";
            // 
            // dgvhistoriconotinha
            // 
            this.dgvhistoriconotinha.AllowUserToAddRows = false;
            this.dgvhistoriconotinha.AllowUserToDeleteRows = false;
            this.dgvhistoriconotinha.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvhistoriconotinha.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvhistoriconotinha.Location = new System.Drawing.Point(1308, 320);
            this.dgvhistoriconotinha.MultiSelect = false;
            this.dgvhistoriconotinha.Name = "dgvhistoriconotinha";
            this.dgvhistoriconotinha.ReadOnly = true;
            this.dgvhistoriconotinha.RowHeadersWidth = 51;
            this.dgvhistoriconotinha.RowTemplate.Height = 24;
            this.dgvhistoriconotinha.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvhistoriconotinha.Size = new System.Drawing.Size(522, 275);
            this.dgvhistoriconotinha.TabIndex = 35;
            // 
            // lsaldonotinha
            // 
            this.lsaldonotinha.AutoSize = true;
            this.lsaldonotinha.Location = new System.Drawing.Point(1308, 285);
            this.lsaldonotinha.Name = "lsaldonotinha";
            this.lsaldonotinha.Size = new System.Drawing.Size(138, 16);
            this.lsaldonotinha.TabIndex = 34;
            this.lsaldonotinha.Text = "Saldo devido: R$ 0,00";
            // 
            // lsemclientenotinha
            // 
            this.lsemclientenotinha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsemclientenotinha.Location = new System.Drawing.Point(1308, 320);
            this.lsemclientenotinha.Name = "lsemclientenotinha";
            this.lsemclientenotinha.Size = new System.Drawing.Size(522, 275);
            this.lsemclientenotinha.TabIndex = 36;
            this.lsemclientenotinha.Text = "Selecione um cliente";
            this.lsemclientenotinha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bpagarnotinha
            // 
            this.bpagarnotinha.Location = new System.Drawing.Point(1308, 610);
            this.bpagarnotinha.Name = "bpagarnotinha";
            this.bpagarnotinha.Size = new System.Drawing.Size(138, 23);
            this.bpagarnotinha.TabIndex = 37;
            this.bpagarnotinha.Text = "Pagar notinha";
            this.bpagarnotinha.UseVisualStyleBackColor = true;
            // 
            // ucclientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bpagarnotinha);
            this.Controls.Add(this.lsemclientenotinha);
            this.Controls.Add(this.dgvhistoriconotinha);
            this.Controls.Add(this.lsaldonotinha);
            this.Controls.Add(this.lnotinhacliente);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.cbativo);
            this.Controls.Add(this.beditarc);
            this.Controls.Add(this.bexcluirc);
            this.Controls.Add(this.tbtelefonec);
            this.Controls.Add(this.ltelefonecliente);
            this.Controls.Add(this.dgvclientes);
            this.Controls.Add(this.bsalvarcliente);
            this.Controls.Add(this.tbcpfcnpjc);
            this.Controls.Add(this.tbnomec);
            this.Controls.Add(this.lcpfcnpjcliente);
            this.Controls.Add(this.lnomecliente);
            this.Name = "ucclientes";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvclientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvhistoriconotinha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbfiltrostatus;
        private System.Windows.Forms.CheckBox cbativo;
        private System.Windows.Forms.Button beditarc;
        private System.Windows.Forms.Button bexcluirc;
        private System.Windows.Forms.TextBox tbtelefonec;
        private System.Windows.Forms.Label ltelefonecliente;
        private System.Windows.Forms.DataGridView dgvclientes;
        private System.Windows.Forms.Button bsalvarcliente;
        private System.Windows.Forms.TextBox tbcpfcnpjc;
        private System.Windows.Forms.TextBox tbnomec;
        private System.Windows.Forms.Label lcpfcnpjcliente;
        private System.Windows.Forms.Label lnomecliente;
        private System.Windows.Forms.Label lnotinhacliente;
        private System.Windows.Forms.DataGridView dgvhistoriconotinha;
        private System.Windows.Forms.Label lsaldonotinha;
        private System.Windows.Forms.Label lsemclientenotinha;
        private System.Windows.Forms.Button bpagarnotinha;
    }
}

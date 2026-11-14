namespace erp_salgados_e_cia
{
    partial class ucusuarios
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
            this.lconfirmarsenha = new System.Windows.Forms.Label();
            this.tbconfirmarsenha = new System.Windows.Forms.TextBox();
            this.llogin = new System.Windows.Forms.Label();
            this.lsenha = new System.Windows.Forms.Label();
            this.tblogin = new System.Windows.Forms.TextBox();
            this.tbsenha = new System.Windows.Forms.TextBox();
            this.ldicasenha = new System.Windows.Forms.Label();
            this.lacesso = new System.Windows.Forms.Label();
            this.cbacesso = new System.Windows.Forms.ComboBox();
            this.cbativo = new System.Windows.Forms.CheckBox();
            this.bsalvarusuario = new System.Windows.Forms.Button();
            this.beditarusuario = new System.Windows.Forms.Button();
            this.cbfiltrostatus = new System.Windows.Forms.ComboBox();
            this.dgvusuarios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvusuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // lconfirmarsenha
            // 
            this.lconfirmarsenha.AutoSize = true;
            this.lconfirmarsenha.Location = new System.Drawing.Point(844, 228);
            this.lconfirmarsenha.Name = "lconfirmarsenha";
            this.lconfirmarsenha.Size = new System.Drawing.Size(107, 16);
            this.lconfirmarsenha.TabIndex = 11;
            this.lconfirmarsenha.Text = "Confirmar senha:";
            // 
            // tbconfirmarsenha
            // 
            this.tbconfirmarsenha.Location = new System.Drawing.Point(966, 225);
            this.tbconfirmarsenha.MaxLength = 30;
            this.tbconfirmarsenha.Name = "tbconfirmarsenha";
            this.tbconfirmarsenha.Size = new System.Drawing.Size(100, 22);
            this.tbconfirmarsenha.TabIndex = 13;
            this.tbconfirmarsenha.UseSystemPasswordChar = true;
            // 
            // llogin
            // 
            this.llogin.AutoSize = true;
            this.llogin.Location = new System.Drawing.Point(844, 153);
            this.llogin.Name = "llogin";
            this.llogin.Size = new System.Drawing.Size(43, 16);
            this.llogin.TabIndex = 14;
            this.llogin.Text = "Login:";
            // 
            // lsenha
            // 
            this.lsenha.AutoSize = true;
            this.lsenha.Location = new System.Drawing.Point(844, 193);
            this.lsenha.Name = "lsenha";
            this.lsenha.Size = new System.Drawing.Size(49, 16);
            this.lsenha.TabIndex = 15;
            this.lsenha.Text = "Senha:";
            // 
            // tblogin
            // 
            this.tblogin.Location = new System.Drawing.Point(907, 153);
            this.tblogin.MaxLength = 30;
            this.tblogin.Name = "tblogin";
            this.tblogin.Size = new System.Drawing.Size(100, 22);
            this.tblogin.TabIndex = 16;
            // 
            // tbsenha
            // 
            this.tbsenha.Location = new System.Drawing.Point(907, 193);
            this.tbsenha.MaxLength = 30;
            this.tbsenha.Name = "tbsenha";
            this.tbsenha.Size = new System.Drawing.Size(100, 22);
            this.tbsenha.TabIndex = 17;
            this.tbsenha.UseSystemPasswordChar = true;
            // 
            // ldicasenha
            // 
            this.ldicasenha.AutoSize = true;
            this.ldicasenha.Location = new System.Drawing.Point(1097, 225);
            this.ldicasenha.Name = "ldicasenha";
            this.ldicasenha.Size = new System.Drawing.Size(439, 16);
            this.ldicasenha.TabIndex = 18;
            this.ldicasenha.Text = "Editando: senha em branco mantém a atual. Esc para cancelar a edição.";
            this.ldicasenha.Visible = false;
            // 
            // lacesso
            // 
            this.lacesso.AutoSize = true;
            this.lacesso.Location = new System.Drawing.Point(844, 272);
            this.lacesso.Name = "lacesso";
            this.lacesso.Size = new System.Drawing.Size(56, 16);
            this.lacesso.TabIndex = 19;
            this.lacesso.Text = "Acesso:";
            // 
            // cbacesso
            // 
            this.cbacesso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbacesso.FormattingEnabled = true;
            this.cbacesso.Location = new System.Drawing.Point(907, 269);
            this.cbacesso.Name = "cbacesso";
            this.cbacesso.Size = new System.Drawing.Size(121, 24);
            this.cbacesso.TabIndex = 27;
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Location = new System.Drawing.Point(1257, 96);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(59, 20);
            this.cbativo.TabIndex = 28;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // bsalvarusuario
            // 
            this.bsalvarusuario.Location = new System.Drawing.Point(1053, 344);
            this.bsalvarusuario.Name = "bsalvarusuario";
            this.bsalvarusuario.Size = new System.Drawing.Size(75, 23);
            this.bsalvarusuario.TabIndex = 29;
            this.bsalvarusuario.Text = "Salvar";
            this.bsalvarusuario.UseVisualStyleBackColor = true;
            // 
            // beditarusuario
            // 
            this.beditarusuario.Location = new System.Drawing.Point(1160, 344);
            this.beditarusuario.Name = "beditarusuario";
            this.beditarusuario.Size = new System.Drawing.Size(75, 23);
            this.beditarusuario.TabIndex = 30;
            this.beditarusuario.Text = "Editar";
            this.beditarusuario.UseVisualStyleBackColor = true;
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(1435, 344);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrostatus.TabIndex = 32;
            // 
            // dgvusuarios
            // 
            this.dgvusuarios.AllowUserToAddRows = false;
            this.dgvusuarios.AllowUserToDeleteRows = false;
            this.dgvusuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvusuarios.Location = new System.Drawing.Point(952, 571);
            this.dgvusuarios.MultiSelect = false;
            this.dgvusuarios.Name = "dgvusuarios";
            this.dgvusuarios.ReadOnly = true;
            this.dgvusuarios.RowHeadersWidth = 51;
            this.dgvusuarios.RowTemplate.Height = 24;
            this.dgvusuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvusuarios.Size = new System.Drawing.Size(540, 191);
            this.dgvusuarios.TabIndex = 33;
            // 
            // ucusuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvusuarios);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.beditarusuario);
            this.Controls.Add(this.bsalvarusuario);
            this.Controls.Add(this.cbativo);
            this.Controls.Add(this.cbacesso);
            this.Controls.Add(this.lacesso);
            this.Controls.Add(this.ldicasenha);
            this.Controls.Add(this.tbsenha);
            this.Controls.Add(this.tblogin);
            this.Controls.Add(this.lsenha);
            this.Controls.Add(this.llogin);
            this.Controls.Add(this.tbconfirmarsenha);
            this.Controls.Add(this.lconfirmarsenha);
            this.Name = "ucusuarios";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvusuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lconfirmarsenha;
        private System.Windows.Forms.TextBox tbconfirmarsenha;
        private System.Windows.Forms.Label llogin;
        private System.Windows.Forms.Label lsenha;
        private System.Windows.Forms.TextBox tblogin;
        private System.Windows.Forms.TextBox tbsenha;
        private System.Windows.Forms.Label ldicasenha;
        private System.Windows.Forms.Label lacesso;
        private System.Windows.Forms.ComboBox cbacesso;
        private System.Windows.Forms.CheckBox cbativo;
        private System.Windows.Forms.Button bsalvarusuario;
        private System.Windows.Forms.Button beditarusuario;
        private System.Windows.Forms.ComboBox cbfiltrostatus;
        private System.Windows.Forms.DataGridView dgvusuarios;
    }
}

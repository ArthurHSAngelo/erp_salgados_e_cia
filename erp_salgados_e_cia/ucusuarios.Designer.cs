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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.pusuarios = new System.Windows.Forms.Panel();
            this.dgvusuarios = new System.Windows.Forms.DataGridView();
            this.pusuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvusuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // lconfirmarsenha
            // 
            this.lconfirmarsenha.AutoSize = true;
            this.lconfirmarsenha.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lconfirmarsenha.Location = new System.Drawing.Point(665, 64);
            this.lconfirmarsenha.Name = "lconfirmarsenha";
            this.lconfirmarsenha.Size = new System.Drawing.Size(140, 21);
            this.lconfirmarsenha.TabIndex = 11;
            this.lconfirmarsenha.Text = "Confirmar senha:";
            // 
            // tbconfirmarsenha
            // 
            this.tbconfirmarsenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbconfirmarsenha.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbconfirmarsenha.Location = new System.Drawing.Point(669, 88);
            this.tbconfirmarsenha.MaxLength = 30;
            this.tbconfirmarsenha.Name = "tbconfirmarsenha";
            this.tbconfirmarsenha.Size = new System.Drawing.Size(202, 30);
            this.tbconfirmarsenha.TabIndex = 13;
            this.tbconfirmarsenha.UseSystemPasswordChar = true;
            // 
            // llogin
            // 
            this.llogin.AutoSize = true;
            this.llogin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.llogin.Location = new System.Drawing.Point(107, 64);
            this.llogin.Name = "llogin";
            this.llogin.Size = new System.Drawing.Size(57, 21);
            this.llogin.TabIndex = 14;
            this.llogin.Text = "Login:";
            // 
            // lsenha
            // 
            this.lsenha.AutoSize = true;
            this.lsenha.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lsenha.Location = new System.Drawing.Point(395, 64);
            this.lsenha.Name = "lsenha";
            this.lsenha.Size = new System.Drawing.Size(61, 21);
            this.lsenha.TabIndex = 15;
            this.lsenha.Text = "Senha:";
            // 
            // tblogin
            // 
            this.tblogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblogin.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tblogin.Location = new System.Drawing.Point(111, 89);
            this.tblogin.MaxLength = 30;
            this.tblogin.Name = "tblogin";
            this.tblogin.Size = new System.Drawing.Size(198, 30);
            this.tblogin.TabIndex = 16;
            // 
            // tbsenha
            // 
            this.tbsenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbsenha.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbsenha.Location = new System.Drawing.Point(399, 89);
            this.tbsenha.MaxLength = 30;
            this.tbsenha.Name = "tbsenha";
            this.tbsenha.Size = new System.Drawing.Size(202, 30);
            this.tbsenha.TabIndex = 17;
            this.tbsenha.UseSystemPasswordChar = true;
            // 
            // ldicasenha
            // 
            this.ldicasenha.AutoSize = true;
            this.ldicasenha.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ldicasenha.ForeColor = System.Drawing.Color.Firebrick;
            this.ldicasenha.Location = new System.Drawing.Point(31, 209);
            this.ldicasenha.Name = "ldicasenha";
            this.ldicasenha.Size = new System.Drawing.Size(687, 28);
            this.ldicasenha.TabIndex = 18;
            this.ldicasenha.Text = "Editando: senha em branco mantém a atual. Esc para cancelar a edição.";
            this.ldicasenha.Visible = false;
            // 
            // lacesso
            // 
            this.lacesso.AutoSize = true;
            this.lacesso.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lacesso.Location = new System.Drawing.Point(938, 63);
            this.lacesso.Name = "lacesso";
            this.lacesso.Size = new System.Drawing.Size(66, 21);
            this.lacesso.TabIndex = 19;
            this.lacesso.Text = "Acesso:";
            // 
            // cbacesso
            // 
            this.cbacesso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbacesso.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbacesso.FormattingEnabled = true;
            this.cbacesso.Location = new System.Drawing.Point(942, 87);
            this.cbacesso.Name = "cbacesso";
            this.cbacesso.Size = new System.Drawing.Size(259, 31);
            this.cbacesso.TabIndex = 27;
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cbativo.Location = new System.Drawing.Point(1244, 90);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(73, 25);
            this.cbativo.TabIndex = 28;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // bsalvarusuario
            // 
            this.bsalvarusuario.BackColor = System.Drawing.Color.White;
            this.bsalvarusuario.FlatAppearance.BorderSize = 2;
            this.bsalvarusuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsalvarusuario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bsalvarusuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvarusuario.Location = new System.Drawing.Point(1635, 76);
            this.bsalvarusuario.Name = "bsalvarusuario";
            this.bsalvarusuario.Size = new System.Drawing.Size(172, 52);
            this.bsalvarusuario.TabIndex = 29;
            this.bsalvarusuario.Text = "Salvar";
            this.bsalvarusuario.UseVisualStyleBackColor = false;
            // 
            // beditarusuario
            // 
            this.beditarusuario.BackColor = System.Drawing.Color.White;
            this.beditarusuario.FlatAppearance.BorderSize = 2;
            this.beditarusuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beditarusuario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.beditarusuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditarusuario.Location = new System.Drawing.Point(1831, 76);
            this.beditarusuario.Name = "beditarusuario";
            this.beditarusuario.Size = new System.Drawing.Size(172, 52);
            this.beditarusuario.TabIndex = 30;
            this.beditarusuario.Text = "Editar";
            this.beditarusuario.UseVisualStyleBackColor = false;
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.BackColor = System.Drawing.Color.White;
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(2245, 251);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(161, 31);
            this.cbfiltrostatus.TabIndex = 32;
            // 
            // pusuarios
            // 
            this.pusuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pusuarios.Controls.Add(this.llogin);
            this.pusuarios.Controls.Add(this.tblogin);
            this.pusuarios.Controls.Add(this.beditarusuario);
            this.pusuarios.Controls.Add(this.lconfirmarsenha);
            this.pusuarios.Controls.Add(this.bsalvarusuario);
            this.pusuarios.Controls.Add(this.tbconfirmarsenha);
            this.pusuarios.Controls.Add(this.cbativo);
            this.pusuarios.Controls.Add(this.lsenha);
            this.pusuarios.Controls.Add(this.cbacesso);
            this.pusuarios.Controls.Add(this.tbsenha);
            this.pusuarios.Controls.Add(this.lacesso);
            this.pusuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.pusuarios.Location = new System.Drawing.Point(0, 0);
            this.pusuarios.Name = "pusuarios";
            this.pusuarios.Size = new System.Drawing.Size(2445, 178);
            this.pusuarios.TabIndex = 34;
            // 
            // dgvusuarios
            // 
            this.dgvusuarios.AllowUserToAddRows = false;
            this.dgvusuarios.AllowUserToDeleteRows = false;
            this.dgvusuarios.AllowUserToResizeColumns = false;
            this.dgvusuarios.AllowUserToResizeRows = false;
            this.dgvusuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvusuarios.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvusuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvusuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvusuarios.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvusuarios.Location = new System.Drawing.Point(36, 286);
            this.dgvusuarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvusuarios.MultiSelect = false;
            this.dgvusuarios.Name = "dgvusuarios";
            this.dgvusuarios.ReadOnly = true;
            this.dgvusuarios.RowHeadersWidth = 51;
            this.dgvusuarios.RowTemplate.Height = 24;
            this.dgvusuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvusuarios.Size = new System.Drawing.Size(2371, 993);
            this.dgvusuarios.TabIndex = 35;
            // 
            // ucusuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.ldicasenha);
            this.Controls.Add(this.pusuarios);
            this.Controls.Add(this.dgvusuarios);
            this.Name = "ucusuarios";
            this.Size = new System.Drawing.Size(2445, 1332);
            this.pusuarios.ResumeLayout(false);
            this.pusuarios.PerformLayout();
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
        private System.Windows.Forms.Panel pusuarios;
        private System.Windows.Forms.DataGridView dgvusuarios;
    }
}

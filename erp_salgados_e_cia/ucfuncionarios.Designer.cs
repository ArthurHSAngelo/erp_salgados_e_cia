namespace erp_salgados_e_cia
{
    partial class ucfuncionarios
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
            this.lnomefuncionario = new System.Windows.Forms.Label();
            this.tbnomefuncionario = new System.Windows.Forms.TextBox();
            this.lunidadefuncionario = new System.Windows.Forms.Label();
            this.cbunidadefuncionario = new System.Windows.Forms.ComboBox();
            this.cbativo = new System.Windows.Forms.CheckBox();
            this.bsalvarfuncionario = new System.Windows.Forms.Button();
            this.bexcluirfuncionario = new System.Windows.Forms.Button();
            this.beditarfuncionario = new System.Windows.Forms.Button();
            this.cbfiltrostatus = new System.Windows.Forms.ComboBox();
            this.dgvfuncionarios = new System.Windows.Forms.DataGridView();
            this.ldicaedicao = new System.Windows.Forms.Label();
            this.pfuncionarios = new System.Windows.Forms.Panel();
            this.lregistrodefuncionarios = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfuncionarios)).BeginInit();
            this.pfuncionarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // lnomefuncionario
            // 
            this.lnomefuncionario.AutoSize = true;
            this.lnomefuncionario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lnomefuncionario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lnomefuncionario.Location = new System.Drawing.Point(80, 54);
            this.lnomefuncionario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnomefuncionario.Name = "lnomefuncionario";
            this.lnomefuncionario.Size = new System.Drawing.Size(144, 17);
            this.lnomefuncionario.TabIndex = 10;
            this.lnomefuncionario.Text = "Nome do funcionário:";
            // 
            // tbnomefuncionario
            // 
            this.tbnomefuncionario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbnomefuncionario.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbnomefuncionario.Location = new System.Drawing.Point(83, 72);
            this.tbnomefuncionario.Margin = new System.Windows.Forms.Padding(2);
            this.tbnomefuncionario.Name = "tbnomefuncionario";
            this.tbnomefuncionario.Size = new System.Drawing.Size(198, 26);
            this.tbnomefuncionario.TabIndex = 12;
            // 
            // lunidadefuncionario
            // 
            this.lunidadefuncionario.AutoSize = true;
            this.lunidadefuncionario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lunidadefuncionario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lunidadefuncionario.Location = new System.Drawing.Point(369, 52);
            this.lunidadefuncionario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lunidadefuncionario.Name = "lunidadefuncionario";
            this.lunidadefuncionario.Size = new System.Drawing.Size(63, 17);
            this.lunidadefuncionario.TabIndex = 13;
            this.lunidadefuncionario.Text = "Unidade:";
            // 
            // cbunidadefuncionario
            // 
            this.cbunidadefuncionario.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbunidadefuncionario.FormattingEnabled = true;
            this.cbunidadefuncionario.Location = new System.Drawing.Point(372, 72);
            this.cbunidadefuncionario.Margin = new System.Windows.Forms.Padding(2);
            this.cbunidadefuncionario.Name = "cbunidadefuncionario";
            this.cbunidadefuncionario.Size = new System.Drawing.Size(171, 27);
            this.cbunidadefuncionario.TabIndex = 21;
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cbativo.Location = new System.Drawing.Point(614, 74);
            this.cbativo.Margin = new System.Windows.Forms.Padding(2);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(60, 21);
            this.cbativo.TabIndex = 22;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // bsalvarfuncionario
            // 
            this.bsalvarfuncionario.FlatAppearance.BorderSize = 2;
            this.bsalvarfuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsalvarfuncionario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bsalvarfuncionario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvarfuncionario.Location = new System.Drawing.Point(1226, 62);
            this.bsalvarfuncionario.Margin = new System.Windows.Forms.Padding(2);
            this.bsalvarfuncionario.Name = "bsalvarfuncionario";
            this.bsalvarfuncionario.Size = new System.Drawing.Size(129, 42);
            this.bsalvarfuncionario.TabIndex = 23;
            this.bsalvarfuncionario.Text = "Salvar";
            this.bsalvarfuncionario.UseVisualStyleBackColor = true;
            // 
            // bexcluirfuncionario
            // 
            this.bexcluirfuncionario.FlatAppearance.BorderSize = 2;
            this.bexcluirfuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexcluirfuncionario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexcluirfuncionario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bexcluirfuncionario.Location = new System.Drawing.Point(1516, 62);
            this.bexcluirfuncionario.Margin = new System.Windows.Forms.Padding(2);
            this.bexcluirfuncionario.Name = "bexcluirfuncionario";
            this.bexcluirfuncionario.Size = new System.Drawing.Size(129, 42);
            this.bexcluirfuncionario.TabIndex = 24;
            this.bexcluirfuncionario.Text = "Excluir";
            this.bexcluirfuncionario.UseVisualStyleBackColor = true;
            // 
            // beditarfuncionario
            // 
            this.beditarfuncionario.FlatAppearance.BorderSize = 2;
            this.beditarfuncionario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beditarfuncionario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.beditarfuncionario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditarfuncionario.Location = new System.Drawing.Point(1373, 62);
            this.beditarfuncionario.Margin = new System.Windows.Forms.Padding(2);
            this.beditarfuncionario.Name = "beditarfuncionario";
            this.beditarfuncionario.Size = new System.Drawing.Size(129, 42);
            this.beditarfuncionario.TabIndex = 25;
            this.beditarfuncionario.Text = "Editar";
            this.beditarfuncionario.UseVisualStyleBackColor = true;
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.BackColor = System.Drawing.Color.White;
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(1684, 204);
            this.cbfiltrostatus.Margin = new System.Windows.Forms.Padding(2);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(122, 27);
            this.cbfiltrostatus.TabIndex = 26;
            // 
            // dgvfuncionarios
            // 
            this.dgvfuncionarios.AllowUserToAddRows = false;
            this.dgvfuncionarios.AllowUserToDeleteRows = false;
            this.dgvfuncionarios.AllowUserToResizeColumns = false;
            this.dgvfuncionarios.AllowUserToResizeRows = false;
            this.dgvfuncionarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvfuncionarios.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvfuncionarios.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvfuncionarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvfuncionarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvfuncionarios.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvfuncionarios.Location = new System.Drawing.Point(34, 232);
            this.dgvfuncionarios.Margin = new System.Windows.Forms.Padding(2);
            this.dgvfuncionarios.MultiSelect = false;
            this.dgvfuncionarios.Name = "dgvfuncionarios";
            this.dgvfuncionarios.ReadOnly = true;
            this.dgvfuncionarios.RowHeadersWidth = 51;
            this.dgvfuncionarios.RowTemplate.Height = 24;
            this.dgvfuncionarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvfuncionarios.Size = new System.Drawing.Size(1778, 807);
            this.dgvfuncionarios.TabIndex = 27;
            // 
            // ldicaedicao
            // 
            this.ldicaedicao.AutoSize = true;
            this.ldicaedicao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ldicaedicao.ForeColor = System.Drawing.Color.Firebrick;
            this.ldicaedicao.Location = new System.Drawing.Point(23, 170);
            this.ldicaedicao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ldicaedicao.Name = "ldicaedicao";
            this.ldicaedicao.Size = new System.Drawing.Size(366, 21);
            this.ldicaedicao.TabIndex = 28;
            this.ldicaedicao.Text = "Editando: pressione Esc para cancelar a edição.";
            this.ldicaedicao.Visible = false;
            // 
            // pfuncionarios
            // 
            this.pfuncionarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pfuncionarios.Controls.Add(this.cbativo);
            this.pfuncionarios.Controls.Add(this.cbunidadefuncionario);
            this.pfuncionarios.Controls.Add(this.lunidadefuncionario);
            this.pfuncionarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.pfuncionarios.Location = new System.Drawing.Point(0, 0);
            this.pfuncionarios.Margin = new System.Windows.Forms.Padding(2);
            this.pfuncionarios.Name = "pfuncionarios";
            this.pfuncionarios.Size = new System.Drawing.Size(1834, 145);
            this.pfuncionarios.TabIndex = 29;
            // 
            // lregistrodefuncionarios
            // 
            this.lregistrodefuncionarios.AutoSize = true;
            this.lregistrodefuncionarios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lregistrodefuncionarios.Location = new System.Drawing.Point(23, 207);
            this.lregistrodefuncionarios.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lregistrodefuncionarios.Name = "lregistrodefuncionarios";
            this.lregistrodefuncionarios.Size = new System.Drawing.Size(200, 21);
            this.lregistrodefuncionarios.TabIndex = 32;
            this.lregistrodefuncionarios.Text = "Registro de funcionários:";
            // 
            // ucfuncionarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lregistrodefuncionarios);
            this.Controls.Add(this.ldicaedicao);
            this.Controls.Add(this.dgvfuncionarios);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.beditarfuncionario);
            this.Controls.Add(this.bexcluirfuncionario);
            this.Controls.Add(this.bsalvarfuncionario);
            this.Controls.Add(this.tbnomefuncionario);
            this.Controls.Add(this.lnomefuncionario);
            this.Controls.Add(this.pfuncionarios);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucfuncionarios";
            this.Size = new System.Drawing.Size(1834, 1082);
            ((System.ComponentModel.ISupportInitialize)(this.dgvfuncionarios)).EndInit();
            this.pfuncionarios.ResumeLayout(false);
            this.pfuncionarios.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lnomefuncionario;
        private System.Windows.Forms.TextBox tbnomefuncionario;
        private System.Windows.Forms.Label lunidadefuncionario;
        private System.Windows.Forms.ComboBox cbunidadefuncionario;
        private System.Windows.Forms.CheckBox cbativo;
        private System.Windows.Forms.Button bsalvarfuncionario;
        private System.Windows.Forms.Button bexcluirfuncionario;
        private System.Windows.Forms.Button beditarfuncionario;
        private System.Windows.Forms.ComboBox cbfiltrostatus;
        private System.Windows.Forms.DataGridView dgvfuncionarios;
        private System.Windows.Forms.Label ldicaedicao;
        private System.Windows.Forms.Panel pfuncionarios;
        private System.Windows.Forms.Label lregistrodefuncionarios;
    }
}

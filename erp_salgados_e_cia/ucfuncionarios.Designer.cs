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
            ((System.ComponentModel.ISupportInitialize)(this.dgvfuncionarios)).BeginInit();
            this.SuspendLayout();
            // 
            // lnomefuncionario
            // 
            this.lnomefuncionario.AutoSize = true;
            this.lnomefuncionario.Location = new System.Drawing.Point(772, 72);
            this.lnomefuncionario.Name = "lnomefuncionario";
            this.lnomefuncionario.Size = new System.Drawing.Size(134, 16);
            this.lnomefuncionario.TabIndex = 10;
            this.lnomefuncionario.Text = "Nome do funcionário:";
            // 
            // tbnomefuncionario
            // 
            this.tbnomefuncionario.Location = new System.Drawing.Point(929, 66);
            this.tbnomefuncionario.Name = "tbnomefuncionario";
            this.tbnomefuncionario.Size = new System.Drawing.Size(100, 22);
            this.tbnomefuncionario.TabIndex = 12;
            // 
            // lunidadefuncionario
            // 
            this.lunidadefuncionario.AutoSize = true;
            this.lunidadefuncionario.Location = new System.Drawing.Point(772, 105);
            this.lunidadefuncionario.Name = "lunidadefuncionario";
            this.lunidadefuncionario.Size = new System.Drawing.Size(62, 16);
            this.lunidadefuncionario.TabIndex = 13;
            this.lunidadefuncionario.Text = "Unidade:";
            // 
            // cbunidadefuncionario
            // 
            this.cbunidadefuncionario.FormattingEnabled = true;
            this.cbunidadefuncionario.Location = new System.Drawing.Point(839, 102);
            this.cbunidadefuncionario.Name = "cbunidadefuncionario";
            this.cbunidadefuncionario.Size = new System.Drawing.Size(121, 24);
            this.cbunidadefuncionario.TabIndex = 21;
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Location = new System.Drawing.Point(1105, 68);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(59, 20);
            this.cbativo.TabIndex = 22;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // bsalvarfuncionario
            // 
            this.bsalvarfuncionario.Location = new System.Drawing.Point(1105, 152);
            this.bsalvarfuncionario.Name = "bsalvarfuncionario";
            this.bsalvarfuncionario.Size = new System.Drawing.Size(75, 23);
            this.bsalvarfuncionario.TabIndex = 23;
            this.bsalvarfuncionario.Text = "Salvar";
            this.bsalvarfuncionario.UseVisualStyleBackColor = true;
            // 
            // bexcluirfuncionario
            // 
            this.bexcluirfuncionario.Location = new System.Drawing.Point(1105, 243);
            this.bexcluirfuncionario.Name = "bexcluirfuncionario";
            this.bexcluirfuncionario.Size = new System.Drawing.Size(75, 23);
            this.bexcluirfuncionario.TabIndex = 24;
            this.bexcluirfuncionario.Text = "Excluir";
            this.bexcluirfuncionario.UseVisualStyleBackColor = true;
            // 
            // beditarfuncionario
            // 
            this.beditarfuncionario.Location = new System.Drawing.Point(1105, 197);
            this.beditarfuncionario.Name = "beditarfuncionario";
            this.beditarfuncionario.Size = new System.Drawing.Size(75, 23);
            this.beditarfuncionario.TabIndex = 25;
            this.beditarfuncionario.Text = "Editar";
            this.beditarfuncionario.UseVisualStyleBackColor = true;
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(1393, 119);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrostatus.TabIndex = 26;
            // 
            // dgvfuncionarios
            // 
            this.dgvfuncionarios.AllowUserToAddRows = false;
            this.dgvfuncionarios.AllowUserToDeleteRows = false;
            this.dgvfuncionarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvfuncionarios.Location = new System.Drawing.Point(952, 571);
            this.dgvfuncionarios.MultiSelect = false;
            this.dgvfuncionarios.Name = "dgvfuncionarios";
            this.dgvfuncionarios.ReadOnly = true;
            this.dgvfuncionarios.RowHeadersWidth = 51;
            this.dgvfuncionarios.RowTemplate.Height = 24;
            this.dgvfuncionarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvfuncionarios.Size = new System.Drawing.Size(540, 191);
            this.dgvfuncionarios.TabIndex = 27;
            // 
            // ldicaedicao
            // 
            this.ldicaedicao.AutoSize = true;
            this.ldicaedicao.Location = new System.Drawing.Point(1348, 228);
            this.ldicaedicao.Name = "ldicaedicao";
            this.ldicaedicao.Size = new System.Drawing.Size(298, 16);
            this.ldicaedicao.TabIndex = 28;
            this.ldicaedicao.Text = "Editando: pressione Esc para cancelar a edição.";
            this.ldicaedicao.Visible = false;
            // 
            // ucfuncionarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ldicaedicao);
            this.Controls.Add(this.dgvfuncionarios);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.beditarfuncionario);
            this.Controls.Add(this.bexcluirfuncionario);
            this.Controls.Add(this.bsalvarfuncionario);
            this.Controls.Add(this.cbativo);
            this.Controls.Add(this.cbunidadefuncionario);
            this.Controls.Add(this.lunidadefuncionario);
            this.Controls.Add(this.tbnomefuncionario);
            this.Controls.Add(this.lnomefuncionario);
            this.Name = "ucfuncionarios";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvfuncionarios)).EndInit();
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
    }
}

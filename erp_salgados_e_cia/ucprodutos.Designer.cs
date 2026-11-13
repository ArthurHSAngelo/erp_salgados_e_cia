namespace erp_salgados_e_cia
{
    partial class ucprodutos
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
            this.lunidadedemedida = new System.Windows.Forms.Label();
            this.cbunidademedida = new System.Windows.Forms.ComboBox();
            this.nudestoqueminimo = new System.Windows.Forms.NumericUpDown();
            this.cbcategoria = new System.Windows.Forms.ComboBox();
            this.lestoqueminimo = new System.Windows.Forms.Label();
            this.bexcluir = new System.Windows.Forms.Button();
            this.dgvprodutos = new System.Windows.Forms.DataGridView();
            this.lcategoria = new System.Windows.Forms.Label();
            this.tbpreco = new System.Windows.Forms.TextBox();
            this.lpreco = new System.Windows.Forms.Label();
            this.bsalvar = new System.Windows.Forms.Button();
            this.tbnomeproduto = new System.Windows.Forms.TextBox();
            this.lproduto = new System.Windows.Forms.Label();
            this.beditar = new System.Windows.Forms.Button();
            this.cbativo = new System.Windows.Forms.CheckBox();
            this.cbfiltrostatus = new System.Windows.Forms.ComboBox();
            this.pprodutos = new System.Windows.Forms.Panel();
            this.ldicaedicao = new System.Windows.Forms.Label();
            this.lregistrodeprodutos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).BeginInit();
            this.pprodutos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lunidadedemedida
            // 
            this.lunidadedemedida.AutoSize = true;
            this.lunidadedemedida.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lunidadedemedida.Location = new System.Drawing.Point(880, 64);
            this.lunidadedemedida.Name = "lunidadedemedida";
            this.lunidadedemedida.Size = new System.Drawing.Size(164, 21);
            this.lunidadedemedida.TabIndex = 26;
            this.lunidadedemedida.Text = "Unidade de medida:";
            // 
            // cbunidademedida
            // 
            this.cbunidademedida.BackColor = System.Drawing.Color.White;
            this.cbunidademedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbunidademedida.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbunidademedida.FormattingEnabled = true;
            this.cbunidademedida.Items.AddRange(new object[] {
            "Unidade",
            "Quilograma",
            "Grama",
            "Litro",
            "Mililitro",
            "Pacote",
            "Caixa"});
            this.cbunidademedida.Location = new System.Drawing.Point(884, 89);
            this.cbunidademedida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbunidademedida.Name = "cbunidademedida";
            this.cbunidademedida.Size = new System.Drawing.Size(197, 31);
            this.cbunidademedida.TabIndex = 25;
            // 
            // nudestoqueminimo
            // 
            this.nudestoqueminimo.BackColor = System.Drawing.Color.White;
            this.nudestoqueminimo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudestoqueminimo.DecimalPlaces = 2;
            this.nudestoqueminimo.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.nudestoqueminimo.Location = new System.Drawing.Point(1179, 89);
            this.nudestoqueminimo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudestoqueminimo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudestoqueminimo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudestoqueminimo.Name = "nudestoqueminimo";
            this.nudestoqueminimo.Size = new System.Drawing.Size(93, 30);
            this.nudestoqueminimo.TabIndex = 24;
            this.nudestoqueminimo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbcategoria
            // 
            this.cbcategoria.BackColor = System.Drawing.Color.White;
            this.cbcategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcategoria.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbcategoria.FormattingEnabled = true;
            this.cbcategoria.Items.AddRange(new object[] {
            "Salgado",
            "Bebida",
            "Doce",
            "Outro"});
            this.cbcategoria.Location = new System.Drawing.Point(623, 89);
            this.cbcategoria.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbcategoria.Name = "cbcategoria";
            this.cbcategoria.Size = new System.Drawing.Size(183, 31);
            this.cbcategoria.TabIndex = 23;
            // 
            // lestoqueminimo
            // 
            this.lestoqueminimo.AutoSize = true;
            this.lestoqueminimo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lestoqueminimo.Location = new System.Drawing.Point(1153, 64);
            this.lestoqueminimo.Name = "lestoqueminimo";
            this.lestoqueminimo.Size = new System.Drawing.Size(109, 21);
            this.lestoqueminimo.TabIndex = 22;
            this.lestoqueminimo.Text = "Estoque mín:";
            // 
            // bexcluir
            // 
            this.bexcluir.BackColor = System.Drawing.Color.White;
            this.bexcluir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bexcluir.FlatAppearance.BorderSize = 2;
            this.bexcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexcluir.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexcluir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bexcluir.Location = new System.Drawing.Point(2021, 76);
            this.bexcluir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bexcluir.Name = "bexcluir";
            this.bexcluir.Size = new System.Drawing.Size(172, 52);
            this.bexcluir.TabIndex = 21;
            this.bexcluir.Text = "Excluir";
            this.bexcluir.UseVisualStyleBackColor = false;
            this.bexcluir.Click += new System.EventHandler(this.bexcluir_Click);
            // 
            // dgvprodutos
            // 
            this.dgvprodutos.AllowUserToAddRows = false;
            this.dgvprodutos.AllowUserToDeleteRows = false;
            this.dgvprodutos.AllowUserToResizeColumns = false;
            this.dgvprodutos.AllowUserToResizeRows = false;
            this.dgvprodutos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvprodutos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvprodutos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvprodutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvprodutos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvprodutos.Location = new System.Drawing.Point(36, 286);
            this.dgvprodutos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvprodutos.MultiSelect = false;
            this.dgvprodutos.Name = "dgvprodutos";
            this.dgvprodutos.ReadOnly = true;
            this.dgvprodutos.RowHeadersWidth = 51;
            this.dgvprodutos.RowTemplate.Height = 24;
            this.dgvprodutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvprodutos.Size = new System.Drawing.Size(2371, 993);
            this.dgvprodutos.TabIndex = 20;
            // 
            // lcategoria
            // 
            this.lcategoria.AutoSize = true;
            this.lcategoria.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lcategoria.Location = new System.Drawing.Point(619, 64);
            this.lcategoria.Name = "lcategoria";
            this.lcategoria.Size = new System.Drawing.Size(88, 21);
            this.lcategoria.TabIndex = 19;
            this.lcategoria.Text = "Categoria:";
            // 
            // tbpreco
            // 
            this.tbpreco.BackColor = System.Drawing.Color.White;
            this.tbpreco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbpreco.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbpreco.Location = new System.Drawing.Point(444, 89);
            this.tbpreco.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbpreco.Name = "tbpreco";
            this.tbpreco.Size = new System.Drawing.Size(109, 30);
            this.tbpreco.TabIndex = 18;
            // 
            // lpreco
            // 
            this.lpreco.AutoSize = true;
            this.lpreco.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lpreco.Location = new System.Drawing.Point(440, 64);
            this.lpreco.Name = "lpreco";
            this.lpreco.Size = new System.Drawing.Size(57, 21);
            this.lpreco.TabIndex = 17;
            this.lpreco.Text = "Preço:";
            // 
            // bsalvar
            // 
            this.bsalvar.BackColor = System.Drawing.Color.White;
            this.bsalvar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvar.FlatAppearance.BorderSize = 2;
            this.bsalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsalvar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bsalvar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvar.Location = new System.Drawing.Point(1635, 76);
            this.bsalvar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bsalvar.Name = "bsalvar";
            this.bsalvar.Size = new System.Drawing.Size(172, 52);
            this.bsalvar.TabIndex = 16;
            this.bsalvar.Text = "Salvar";
            this.bsalvar.UseVisualStyleBackColor = false;
            this.bsalvar.Click += new System.EventHandler(this.bsalvar_Click);
            // 
            // tbnomeproduto
            // 
            this.tbnomeproduto.BackColor = System.Drawing.Color.White;
            this.tbnomeproduto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbnomeproduto.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnomeproduto.Location = new System.Drawing.Point(111, 89);
            this.tbnomeproduto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbnomeproduto.Name = "tbnomeproduto";
            this.tbnomeproduto.Size = new System.Drawing.Size(263, 30);
            this.tbnomeproduto.TabIndex = 15;
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lproduto.Location = new System.Drawing.Point(107, 64);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(61, 21);
            this.lproduto.TabIndex = 14;
            this.lproduto.Text = "Nome:";
            // 
            // beditar
            // 
            this.beditar.BackColor = System.Drawing.Color.White;
            this.beditar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditar.FlatAppearance.BorderSize = 2;
            this.beditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beditar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.beditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditar.Location = new System.Drawing.Point(1831, 76);
            this.beditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.beditar.Name = "beditar";
            this.beditar.Size = new System.Drawing.Size(172, 52);
            this.beditar.TabIndex = 27;
            this.beditar.Text = "Editar";
            this.beditar.UseVisualStyleBackColor = false;
            this.beditar.Click += new System.EventHandler(this.beditar_Click);
            // 
            // cbativo
            // 
            this.cbativo.AutoSize = true;
            this.cbativo.Checked = true;
            this.cbativo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbativo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cbativo.Location = new System.Drawing.Point(1383, 91);
            this.cbativo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbativo.Name = "cbativo";
            this.cbativo.Size = new System.Drawing.Size(73, 25);
            this.cbativo.TabIndex = 28;
            this.cbativo.Text = "Ativo";
            this.cbativo.UseVisualStyleBackColor = true;
            this.cbativo.Visible = false;
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.BackColor = System.Drawing.Color.White;
            this.cbfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(2245, 251);
            this.cbfiltrostatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(161, 31);
            this.cbfiltrostatus.TabIndex = 29;
            // 
            // pprodutos
            // 
            this.pprodutos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pprodutos.Controls.Add(this.lproduto);
            this.pprodutos.Controls.Add(this.beditar);
            this.pprodutos.Controls.Add(this.tbnomeproduto);
            this.pprodutos.Controls.Add(this.bexcluir);
            this.pprodutos.Controls.Add(this.cbativo);
            this.pprodutos.Controls.Add(this.tbpreco);
            this.pprodutos.Controls.Add(this.bsalvar);
            this.pprodutos.Controls.Add(this.lpreco);
            this.pprodutos.Controls.Add(this.nudestoqueminimo);
            this.pprodutos.Controls.Add(this.lestoqueminimo);
            this.pprodutos.Controls.Add(this.lunidadedemedida);
            this.pprodutos.Controls.Add(this.cbcategoria);
            this.pprodutos.Controls.Add(this.cbunidademedida);
            this.pprodutos.Controls.Add(this.lcategoria);
            this.pprodutos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pprodutos.Location = new System.Drawing.Point(0, 0);
            this.pprodutos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pprodutos.Name = "pprodutos";
            this.pprodutos.Size = new System.Drawing.Size(2445, 178);
            this.pprodutos.TabIndex = 30;
            // 
            // ldicaedicao
            // 
            this.ldicaedicao.AutoSize = true;
            this.ldicaedicao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ldicaedicao.ForeColor = System.Drawing.Color.Firebrick;
            this.ldicaedicao.Location = new System.Drawing.Point(31, 209);
            this.ldicaedicao.Name = "ldicaedicao";
            this.ldicaedicao.Size = new System.Drawing.Size(456, 28);
            this.ldicaedicao.TabIndex = 32;
            this.ldicaedicao.Text = "Editando: pressione Esc para cancelar a edição.";
            this.ldicaedicao.Visible = false;
            // 
            // lregistrodeprodutos
            // 
            this.lregistrodeprodutos.AutoSize = true;
            this.lregistrodeprodutos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lregistrodeprodutos.Location = new System.Drawing.Point(31, 255);
            this.lregistrodeprodutos.Name = "lregistrodeprodutos";
            this.lregistrodeprodutos.Size = new System.Drawing.Size(216, 28);
            this.lregistrodeprodutos.TabIndex = 31;
            this.lregistrodeprodutos.Text = "Registro de produtos:";
            // 
            // ucprodutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ldicaedicao);
            this.Controls.Add(this.lregistrodeprodutos);
            this.Controls.Add(this.pprodutos);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.dgvprodutos);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucprodutos";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).EndInit();
            this.pprodutos.ResumeLayout(false);
            this.pprodutos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lunidadedemedida;
        private System.Windows.Forms.ComboBox cbunidademedida;
        private System.Windows.Forms.NumericUpDown nudestoqueminimo;
        private System.Windows.Forms.ComboBox cbcategoria;
        private System.Windows.Forms.Label lestoqueminimo;
        private System.Windows.Forms.Button bexcluir;
        private System.Windows.Forms.DataGridView dgvprodutos;
        private System.Windows.Forms.Label lcategoria;
        private System.Windows.Forms.TextBox tbpreco;
        private System.Windows.Forms.Label lpreco;
        private System.Windows.Forms.Button bsalvar;
        private System.Windows.Forms.TextBox tbnomeproduto;
        private System.Windows.Forms.Label lproduto;
        private System.Windows.Forms.Button beditar;
        private System.Windows.Forms.CheckBox cbativo;
        private System.Windows.Forms.ComboBox cbfiltrostatus;
        private System.Windows.Forms.Panel pprodutos;
        private System.Windows.Forms.Label lregistrodeprodutos;
        private System.Windows.Forms.Label ldicaedicao;
    }
}

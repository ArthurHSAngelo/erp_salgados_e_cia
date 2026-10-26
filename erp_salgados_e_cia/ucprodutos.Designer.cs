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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lregistrodeprodutos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lunidadedemedida
            // 
            this.lunidadedemedida.AutoSize = true;
            this.lunidadedemedida.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lunidadedemedida.Location = new System.Drawing.Point(828, 66);
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
            this.cbunidademedida.Location = new System.Drawing.Point(832, 90);
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
            this.nudestoqueminimo.Location = new System.Drawing.Point(1127, 91);
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
            this.nudestoqueminimo.Size = new System.Drawing.Size(96, 30);
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
            this.cbcategoria.Location = new System.Drawing.Point(552, 90);
            this.cbcategoria.Name = "cbcategoria";
            this.cbcategoria.Size = new System.Drawing.Size(183, 31);
            this.cbcategoria.TabIndex = 23;
            // 
            // lestoqueminimo
            // 
            this.lestoqueminimo.AutoSize = true;
            this.lestoqueminimo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lestoqueminimo.Location = new System.Drawing.Point(1102, 67);
            this.lestoqueminimo.Name = "lestoqueminimo";
            this.lestoqueminimo.Size = new System.Drawing.Size(109, 21);
            this.lestoqueminimo.TabIndex = 22;
            this.lestoqueminimo.Text = "Estoque mín:";
            // 
            // bexcluir
            // 
            this.bexcluir.BackColor = System.Drawing.Color.Transparent;
            this.bexcluir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bexcluir.FlatAppearance.BorderSize = 2;
            this.bexcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexcluir.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexcluir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bexcluir.Location = new System.Drawing.Point(2021, 76);
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
            this.dgvprodutos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvprodutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvprodutos.Location = new System.Drawing.Point(36, 286);
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
            this.lcategoria.Location = new System.Drawing.Point(548, 66);
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
            this.tbpreco.Location = new System.Drawing.Point(373, 90);
            this.tbpreco.Name = "tbpreco";
            this.tbpreco.Size = new System.Drawing.Size(109, 30);
            this.tbpreco.TabIndex = 18;
            // 
            // lpreco
            // 
            this.lpreco.AutoSize = true;
            this.lpreco.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lpreco.Location = new System.Drawing.Point(369, 66);
            this.lpreco.Name = "lpreco";
            this.lpreco.Size = new System.Drawing.Size(57, 21);
            this.lpreco.TabIndex = 17;
            this.lpreco.Text = "Preço:";
            // 
            // bsalvar
            // 
            this.bsalvar.BackColor = System.Drawing.Color.Transparent;
            this.bsalvar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvar.FlatAppearance.BorderSize = 2;
            this.bsalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsalvar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bsalvar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bsalvar.Location = new System.Drawing.Point(1634, 76);
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
            this.tbnomeproduto.Location = new System.Drawing.Point(111, 90);
            this.tbnomeproduto.Name = "tbnomeproduto";
            this.tbnomeproduto.Size = new System.Drawing.Size(195, 30);
            this.tbnomeproduto.TabIndex = 15;
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lproduto.Location = new System.Drawing.Point(107, 66);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(61, 21);
            this.lproduto.TabIndex = 14;
            this.lproduto.Text = "Nome:";
            // 
            // beditar
            // 
            this.beditar.BackColor = System.Drawing.Color.Transparent;
            this.beditar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditar.FlatAppearance.BorderSize = 2;
            this.beditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beditar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.beditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.beditar.Location = new System.Drawing.Point(1831, 76);
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
            this.cbativo.Location = new System.Drawing.Point(1328, 92);
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
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(162, 31);
            this.cbfiltrostatus.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.tbnomeproduto);
            this.panel1.Controls.Add(this.lproduto);
            this.panel1.Controls.Add(this.beditar);
            this.panel1.Controls.Add(this.bexcluir);
            this.panel1.Controls.Add(this.cbativo);
            this.panel1.Controls.Add(this.tbpreco);
            this.panel1.Controls.Add(this.bsalvar);
            this.panel1.Controls.Add(this.lpreco);
            this.panel1.Controls.Add(this.nudestoqueminimo);
            this.panel1.Controls.Add(this.lestoqueminimo);
            this.panel1.Controls.Add(this.lunidadedemedida);
            this.panel1.Controls.Add(this.cbcategoria);
            this.panel1.Controls.Add(this.cbunidademedida);
            this.panel1.Controls.Add(this.lcategoria);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2445, 178);
            this.panel1.TabIndex = 30;
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
            this.Controls.Add(this.lregistrodeprodutos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.dgvprodutos);
            this.Name = "ucprodutos";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lregistrodeprodutos;
    }
}

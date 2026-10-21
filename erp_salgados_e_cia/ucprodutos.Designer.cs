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
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).BeginInit();
            this.SuspendLayout();
            // 
            // lunidadedemedida
            // 
            this.lunidadedemedida.AutoSize = true;
            this.lunidadedemedida.Location = new System.Drawing.Point(614, 52);
            this.lunidadedemedida.Name = "lunidadedemedida";
            this.lunidadedemedida.Size = new System.Drawing.Size(130, 16);
            this.lunidadedemedida.TabIndex = 26;
            this.lunidadedemedida.Text = "Unidade de medida:";
            // 
            // cbunidademedida
            // 
            this.cbunidademedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbunidademedida.FormattingEnabled = true;
            this.cbunidademedida.Items.AddRange(new object[] {
            "Unidade",
            "Quilograma",
            "Grama",
            "Litro",
            "Mililitro",
            "Pacote",
            "Caixa"});
            this.cbunidademedida.Location = new System.Drawing.Point(750, 47);
            this.cbunidademedida.Name = "cbunidademedida";
            this.cbunidademedida.Size = new System.Drawing.Size(109, 24);
            this.cbunidademedida.TabIndex = 25;
            // 
            // nudestoqueminimo
            // 
            this.nudestoqueminimo.DecimalPlaces = 2;
            this.nudestoqueminimo.Location = new System.Drawing.Point(1004, 48);
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
            this.nudestoqueminimo.Size = new System.Drawing.Size(69, 22);
            this.nudestoqueminimo.TabIndex = 24;
            this.nudestoqueminimo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbcategoria
            // 
            this.cbcategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcategoria.FormattingEnabled = true;
            this.cbcategoria.Items.AddRange(new object[] {
            "Salgado",
            "Bebida",
            "Doce",
            "Outro"});
            this.cbcategoria.Location = new System.Drawing.Point(480, 47);
            this.cbcategoria.Name = "cbcategoria";
            this.cbcategoria.Size = new System.Drawing.Size(109, 24);
            this.cbcategoria.TabIndex = 23;
            // 
            // lestoqueminimo
            // 
            this.lestoqueminimo.AutoSize = true;
            this.lestoqueminimo.Location = new System.Drawing.Point(892, 51);
            this.lestoqueminimo.Name = "lestoqueminimo";
            this.lestoqueminimo.Size = new System.Drawing.Size(106, 16);
            this.lestoqueminimo.TabIndex = 22;
            this.lestoqueminimo.Text = "Estoque mínimo:";
            // 
            // bexcluir
            // 
            this.bexcluir.Location = new System.Drawing.Point(347, 152);
            this.bexcluir.Name = "bexcluir";
            this.bexcluir.Size = new System.Drawing.Size(75, 23);
            this.bexcluir.TabIndex = 21;
            this.bexcluir.Text = "Excluir";
            this.bexcluir.UseVisualStyleBackColor = true;
            this.bexcluir.Click += new System.EventHandler(this.bexcluir_Click);
            // 
            // dgvprodutos
            // 
            this.dgvprodutos.AllowUserToAddRows = false;
            this.dgvprodutos.AllowUserToDeleteRows = false;
            this.dgvprodutos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvprodutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvprodutos.Location = new System.Drawing.Point(127, 190);
            this.dgvprodutos.MultiSelect = false;
            this.dgvprodutos.Name = "dgvprodutos";
            this.dgvprodutos.ReadOnly = true;
            this.dgvprodutos.RowHeadersWidth = 51;
            this.dgvprodutos.RowTemplate.Height = 24;
            this.dgvprodutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvprodutos.Size = new System.Drawing.Size(894, 382);
            this.dgvprodutos.TabIndex = 20;
            this.dgvprodutos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvprodutos_CellValidating);
            // 
            // lcategoria
            // 
            this.lcategoria.AutoSize = true;
            this.lcategoria.Location = new System.Drawing.Point(405, 50);
            this.lcategoria.Name = "lcategoria";
            this.lcategoria.Size = new System.Drawing.Size(69, 16);
            this.lcategoria.TabIndex = 19;
            this.lcategoria.Text = "Categoria:";
            // 
            // tbpreco
            // 
            this.tbpreco.Location = new System.Drawing.Point(266, 47);
            this.tbpreco.Name = "tbpreco";
            this.tbpreco.Size = new System.Drawing.Size(109, 22);
            this.tbpreco.TabIndex = 18;
            // 
            // lpreco
            // 
            this.lpreco.AutoSize = true;
            this.lpreco.Location = new System.Drawing.Point(214, 50);
            this.lpreco.Name = "lpreco";
            this.lpreco.Size = new System.Drawing.Size(46, 16);
            this.lpreco.TabIndex = 17;
            this.lpreco.Text = "Preço:";
            // 
            // bsalvar
            // 
            this.bsalvar.BackColor = System.Drawing.Color.White;
            this.bsalvar.Location = new System.Drawing.Point(185, 152);
            this.bsalvar.Name = "bsalvar";
            this.bsalvar.Size = new System.Drawing.Size(75, 23);
            this.bsalvar.TabIndex = 16;
            this.bsalvar.Text = "Salvar";
            this.bsalvar.UseVisualStyleBackColor = false;
            this.bsalvar.Click += new System.EventHandler(this.bsalvar_Click);
            // 
            // tbnomeproduto
            // 
            this.tbnomeproduto.Location = new System.Drawing.Point(83, 47);
            this.tbnomeproduto.Name = "tbnomeproduto";
            this.tbnomeproduto.Size = new System.Drawing.Size(109, 22);
            this.tbnomeproduto.TabIndex = 15;
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Location = new System.Drawing.Point(30, 50);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(47, 16);
            this.lproduto.TabIndex = 14;
            this.lproduto.Text = "Nome:";
            // 
            // beditar
            // 
            this.beditar.BackColor = System.Drawing.Color.White;
            this.beditar.Location = new System.Drawing.Point(266, 152);
            this.beditar.Name = "beditar";
            this.beditar.Size = new System.Drawing.Size(75, 23);
            this.beditar.TabIndex = 27;
            this.beditar.Text = "Editar";
            this.beditar.UseVisualStyleBackColor = false;
            this.beditar.Click += new System.EventHandler(this.beditar_Click);
            // 
            // ucprodutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.beditar);
            this.Controls.Add(this.lunidadedemedida);
            this.Controls.Add(this.cbunidademedida);
            this.Controls.Add(this.nudestoqueminimo);
            this.Controls.Add(this.cbcategoria);
            this.Controls.Add(this.lestoqueminimo);
            this.Controls.Add(this.bexcluir);
            this.Controls.Add(this.dgvprodutos);
            this.Controls.Add(this.lcategoria);
            this.Controls.Add(this.tbpreco);
            this.Controls.Add(this.lpreco);
            this.Controls.Add(this.bsalvar);
            this.Controls.Add(this.tbnomeproduto);
            this.Controls.Add(this.lproduto);
            this.Name = "ucprodutos";
            this.Size = new System.Drawing.Size(2320, 1156);
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).EndInit();
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
    }
}

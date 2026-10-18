namespace erp_salgados_e_cia
{
    partial class CadastroProdutos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lproduto = new System.Windows.Forms.Label();
            this.tbnomeproduto = new System.Windows.Forms.TextBox();
            this.bsalvar = new System.Windows.Forms.Button();
            this.lpreco = new System.Windows.Forms.Label();
            this.tbpreco = new System.Windows.Forms.TextBox();
            this.lcategoria = new System.Windows.Forms.Label();
            this.dgvprodutos = new System.Windows.Forms.DataGridView();
            this.bexcluir = new System.Windows.Forms.Button();
            this.lestoqueminimo = new System.Windows.Forms.Label();
            this.cbcategoria = new System.Windows.Forms.ComboBox();
            this.nudestoqueminimo = new System.Windows.Forms.NumericUpDown();
            this.cbunidademedida = new System.Windows.Forms.ComboBox();
            this.lunidadedemedida = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).BeginInit();
            this.SuspendLayout();
            // 
            // lproduto
            // 
            this.lproduto.AutoSize = true;
            this.lproduto.Location = new System.Drawing.Point(33, 41);
            this.lproduto.Name = "lproduto";
            this.lproduto.Size = new System.Drawing.Size(47, 16);
            this.lproduto.TabIndex = 0;
            this.lproduto.Text = "Nome:";
            // 
            // tbnomeproduto
            // 
            this.tbnomeproduto.Location = new System.Drawing.Point(86, 38);
            this.tbnomeproduto.Name = "tbnomeproduto";
            this.tbnomeproduto.Size = new System.Drawing.Size(109, 22);
            this.tbnomeproduto.TabIndex = 1;
            // 
            // bsalvar
            // 
            this.bsalvar.BackColor = System.Drawing.Color.White;
            this.bsalvar.Location = new System.Drawing.Point(375, 143);
            this.bsalvar.Name = "bsalvar";
            this.bsalvar.Size = new System.Drawing.Size(75, 23);
            this.bsalvar.TabIndex = 2;
            this.bsalvar.Text = "Salvar";
            this.bsalvar.UseVisualStyleBackColor = false;
            this.bsalvar.Click += new System.EventHandler(this.button1_Click);
            // 
            // lpreco
            // 
            this.lpreco.AutoSize = true;
            this.lpreco.Location = new System.Drawing.Point(217, 41);
            this.lpreco.Name = "lpreco";
            this.lpreco.Size = new System.Drawing.Size(46, 16);
            this.lpreco.TabIndex = 3;
            this.lpreco.Text = "Preço:";
            // 
            // tbpreco
            // 
            this.tbpreco.Location = new System.Drawing.Point(269, 38);
            this.tbpreco.Name = "tbpreco";
            this.tbpreco.Size = new System.Drawing.Size(109, 22);
            this.tbpreco.TabIndex = 4;
            // 
            // lcategoria
            // 
            this.lcategoria.AutoSize = true;
            this.lcategoria.Location = new System.Drawing.Point(408, 41);
            this.lcategoria.Name = "lcategoria";
            this.lcategoria.Size = new System.Drawing.Size(69, 16);
            this.lcategoria.TabIndex = 5;
            this.lcategoria.Text = "Categoria:";
            // 
            // dgvprodutos
            // 
            this.dgvprodutos.AllowUserToAddRows = false;
            this.dgvprodutos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvprodutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvprodutos.Location = new System.Drawing.Point(130, 181);
            this.dgvprodutos.Name = "dgvprodutos";
            this.dgvprodutos.RowHeadersWidth = 51;
            this.dgvprodutos.RowTemplate.Height = 24;
            this.dgvprodutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvprodutos.Size = new System.Drawing.Size(894, 382);
            this.dgvprodutos.TabIndex = 7;
            this.dgvprodutos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvprodutos_CellValidating);
            // 
            // bexcluir
            // 
            this.bexcluir.Location = new System.Drawing.Point(176, 143);
            this.bexcluir.Name = "bexcluir";
            this.bexcluir.Size = new System.Drawing.Size(75, 23);
            this.bexcluir.TabIndex = 8;
            this.bexcluir.Text = "Excluir";
            this.bexcluir.UseVisualStyleBackColor = true;
            this.bexcluir.Click += new System.EventHandler(this.bexcluir_Click);
            // 
            // lestoqueminimo
            // 
            this.lestoqueminimo.AutoSize = true;
            this.lestoqueminimo.Location = new System.Drawing.Point(895, 42);
            this.lestoqueminimo.Name = "lestoqueminimo";
            this.lestoqueminimo.Size = new System.Drawing.Size(106, 16);
            this.lestoqueminimo.TabIndex = 9;
            this.lestoqueminimo.Text = "Estoque mínimo:";
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
            this.cbcategoria.Location = new System.Drawing.Point(483, 38);
            this.cbcategoria.Name = "cbcategoria";
            this.cbcategoria.Size = new System.Drawing.Size(109, 24);
            this.cbcategoria.TabIndex = 10;
            // 
            // nudestoqueminimo
            // 
            this.nudestoqueminimo.DecimalPlaces = 2;
            this.nudestoqueminimo.Location = new System.Drawing.Point(1007, 39);
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
            this.nudestoqueminimo.TabIndex = 11;
            this.nudestoqueminimo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.cbunidademedida.Location = new System.Drawing.Point(753, 38);
            this.cbunidademedida.Name = "cbunidademedida";
            this.cbunidademedida.Size = new System.Drawing.Size(109, 24);
            this.cbunidademedida.TabIndex = 12;
            // 
            // lunidadedemedida
            // 
            this.lunidadedemedida.AutoSize = true;
            this.lunidadedemedida.Location = new System.Drawing.Point(617, 43);
            this.lunidadedemedida.Name = "lunidadedemedida";
            this.lunidadedemedida.Size = new System.Drawing.Size(130, 16);
            this.lunidadedemedida.TabIndex = 13;
            this.lunidadedemedida.Text = "Unidade de medida:";
            // 
            // CadastroProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1731, 857);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CadastroProdutos";
            this.Text = "Cadastro de Produtos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudestoqueminimo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lproduto;
        private System.Windows.Forms.TextBox tbnomeproduto;
        private System.Windows.Forms.Button bsalvar;
        private System.Windows.Forms.Label lpreco;
        private System.Windows.Forms.TextBox tbpreco;
        private System.Windows.Forms.Label lcategoria;
        private System.Windows.Forms.DataGridView dgvprodutos;
        private System.Windows.Forms.Button bexcluir;
        private System.Windows.Forms.Label lestoqueminimo;
        private System.Windows.Forms.ComboBox cbcategoria;
        private System.Windows.Forms.NumericUpDown nudestoqueminimo;
        private System.Windows.Forms.ComboBox cbunidademedida;
        private System.Windows.Forms.Label lunidadedemedida;
    }
}
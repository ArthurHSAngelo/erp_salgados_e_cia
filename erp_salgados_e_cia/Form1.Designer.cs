namespace erp_salgados_e_cia
{
    partial class Form1
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
            this.tbcategoria = new System.Windows.Forms.TextBox();
            this.dgvprodutos = new System.Windows.Forms.DataGridView();
            this.bexcluir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).BeginInit();
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
            this.bsalvar.Location = new System.Drawing.Point(647, 38);
            this.bsalvar.Name = "bsalvar";
            this.bsalvar.Size = new System.Drawing.Size(75, 23);
            this.bsalvar.TabIndex = 2;
            this.bsalvar.Text = "Salvar";
            this.bsalvar.UseVisualStyleBackColor = true;
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
            // tbcategoria
            // 
            this.tbcategoria.Location = new System.Drawing.Point(483, 38);
            this.tbcategoria.Name = "tbcategoria";
            this.tbcategoria.Size = new System.Drawing.Size(109, 22);
            this.tbcategoria.TabIndex = 6;
            // 
            // dgvprodutos
            // 
            this.dgvprodutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvprodutos.Location = new System.Drawing.Point(36, 102);
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
            this.bexcluir.Location = new System.Drawing.Point(765, 37);
            this.bexcluir.Name = "bexcluir";
            this.bexcluir.Size = new System.Drawing.Size(75, 23);
            this.bexcluir.TabIndex = 8;
            this.bexcluir.Text = "Excluir";
            this.bexcluir.UseVisualStyleBackColor = true;
            this.bexcluir.Click += new System.EventHandler(this.bexcluir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.bexcluir);
            this.Controls.Add(this.dgvprodutos);
            this.Controls.Add(this.tbcategoria);
            this.Controls.Add(this.lcategoria);
            this.Controls.Add(this.tbpreco);
            this.Controls.Add(this.lpreco);
            this.Controls.Add(this.bsalvar);
            this.Controls.Add(this.tbnomeproduto);
            this.Controls.Add(this.lproduto);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvprodutos)).EndInit();
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
        private System.Windows.Forms.TextBox tbcategoria;
        private System.Windows.Forms.DataGridView dgvprodutos;
        private System.Windows.Forms.Button bexcluir;
    }
}
namespace erp_salgados_e_cia
{
    partial class ucunidades
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
            this.bexcluirunidade = new System.Windows.Forms.Button();
            this.dgvunidades = new System.Windows.Forms.DataGridView();
            this.bsalvarunidade = new System.Windows.Forms.Button();
            this.tbnumerou = new System.Windows.Forms.TextBox();
            this.tbenderecou = new System.Windows.Forms.TextBox();
            this.tbnomeu = new System.Windows.Forms.TextBox();
            this.lnumerou = new System.Windows.Forms.Label();
            this.lenderecou = new System.Windows.Forms.Label();
            this.lnomeu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvunidades)).BeginInit();
            this.SuspendLayout();
            // 
            // bexcluirunidade
            // 
            this.bexcluirunidade.Location = new System.Drawing.Point(211, 170);
            this.bexcluirunidade.Name = "bexcluirunidade";
            this.bexcluirunidade.Size = new System.Drawing.Size(75, 23);
            this.bexcluirunidade.TabIndex = 17;
            this.bexcluirunidade.Text = "Excluir";
            this.bexcluirunidade.UseVisualStyleBackColor = true;
            this.bexcluirunidade.Click += new System.EventHandler(this.bexcluirunidade_Click);
            // 
            // dgvunidades
            // 
            this.dgvunidades.AllowUserToAddRows = false;
            this.dgvunidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvunidades.Location = new System.Drawing.Point(391, 208);
            this.dgvunidades.MultiSelect = false;
            this.dgvunidades.Name = "dgvunidades";
            this.dgvunidades.RowHeadersWidth = 51;
            this.dgvunidades.RowTemplate.Height = 24;
            this.dgvunidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvunidades.Size = new System.Drawing.Size(240, 150);
            this.dgvunidades.TabIndex = 16;
            this.dgvunidades.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvunidades_CellValidating);
            // 
            // bsalvarunidade
            // 
            this.bsalvarunidade.Location = new System.Drawing.Point(97, 170);
            this.bsalvarunidade.Name = "bsalvarunidade";
            this.bsalvarunidade.Size = new System.Drawing.Size(75, 23);
            this.bsalvarunidade.TabIndex = 15;
            this.bsalvarunidade.Text = "Salvar";
            this.bsalvarunidade.UseVisualStyleBackColor = true;
            this.bsalvarunidade.Click += new System.EventHandler(this.bsalvarunidade_Click);
            // 
            // tbnumerou
            // 
            this.tbnumerou.Location = new System.Drawing.Point(177, 120);
            this.tbnumerou.Name = "tbnumerou";
            this.tbnumerou.Size = new System.Drawing.Size(100, 22);
            this.tbnumerou.TabIndex = 14;
            // 
            // tbenderecou
            // 
            this.tbenderecou.Location = new System.Drawing.Point(177, 80);
            this.tbenderecou.Name = "tbenderecou";
            this.tbenderecou.Size = new System.Drawing.Size(100, 22);
            this.tbenderecou.TabIndex = 13;
            // 
            // tbnomeu
            // 
            this.tbnomeu.Location = new System.Drawing.Point(177, 44);
            this.tbnomeu.Name = "tbnomeu";
            this.tbnomeu.Size = new System.Drawing.Size(100, 22);
            this.tbnomeu.TabIndex = 12;
            // 
            // lnumerou
            // 
            this.lnumerou.AutoSize = true;
            this.lnumerou.Location = new System.Drawing.Point(94, 120);
            this.lnumerou.Name = "lnumerou";
            this.lnumerou.Size = new System.Drawing.Size(58, 16);
            this.lnumerou.TabIndex = 11;
            this.lnumerou.Text = "Número:";
            // 
            // lenderecou
            // 
            this.lenderecou.AutoSize = true;
            this.lenderecou.Location = new System.Drawing.Point(94, 80);
            this.lenderecou.Name = "lenderecou";
            this.lenderecou.Size = new System.Drawing.Size(69, 16);
            this.lenderecou.TabIndex = 10;
            this.lenderecou.Text = "Endereço:";
            // 
            // lnomeu
            // 
            this.lnomeu.AutoSize = true;
            this.lnomeu.Location = new System.Drawing.Point(94, 44);
            this.lnomeu.Name = "lnomeu";
            this.lnomeu.Size = new System.Drawing.Size(47, 16);
            this.lnomeu.TabIndex = 9;
            this.lnomeu.Text = "Nome:";
            // 
            // ucunidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bexcluirunidade);
            this.Controls.Add(this.dgvunidades);
            this.Controls.Add(this.bsalvarunidade);
            this.Controls.Add(this.tbnumerou);
            this.Controls.Add(this.tbenderecou);
            this.Controls.Add(this.tbnomeu);
            this.Controls.Add(this.lnumerou);
            this.Controls.Add(this.lenderecou);
            this.Controls.Add(this.lnomeu);
            this.Name = "ucunidades";
            this.Size = new System.Drawing.Size(2397, 1195);
            ((System.ComponentModel.ISupportInitialize)(this.dgvunidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bexcluirunidade;
        private System.Windows.Forms.DataGridView dgvunidades;
        private System.Windows.Forms.Button bsalvarunidade;
        private System.Windows.Forms.TextBox tbnumerou;
        private System.Windows.Forms.TextBox tbenderecou;
        private System.Windows.Forms.TextBox tbnomeu;
        private System.Windows.Forms.Label lnumerou;
        private System.Windows.Forms.Label lenderecou;
        private System.Windows.Forms.Label lnomeu;
    }
}

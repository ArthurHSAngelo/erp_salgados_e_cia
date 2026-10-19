namespace erp_salgados_e_cia
{
    partial class CadastroUnidades
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
            this.lnomeu = new System.Windows.Forms.Label();
            this.lenderecou = new System.Windows.Forms.Label();
            this.lnumerou = new System.Windows.Forms.Label();
            this.tbnomeu = new System.Windows.Forms.TextBox();
            this.tbenderecou = new System.Windows.Forms.TextBox();
            this.tbnumerou = new System.Windows.Forms.TextBox();
            this.bsalvarunidade = new System.Windows.Forms.Button();
            this.dgvunidades = new System.Windows.Forms.DataGridView();
            this.bexcluirunidade = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvunidades)).BeginInit();
            this.SuspendLayout();
            // 
            // lnomeu
            // 
            this.lnomeu.AutoSize = true;
            this.lnomeu.Location = new System.Drawing.Point(59, 57);
            this.lnomeu.Name = "lnomeu";
            this.lnomeu.Size = new System.Drawing.Size(47, 16);
            this.lnomeu.TabIndex = 0;
            this.lnomeu.Text = "Nome:";
            // 
            // lenderecou
            // 
            this.lenderecou.AutoSize = true;
            this.lenderecou.Location = new System.Drawing.Point(59, 93);
            this.lenderecou.Name = "lenderecou";
            this.lenderecou.Size = new System.Drawing.Size(69, 16);
            this.lenderecou.TabIndex = 1;
            this.lenderecou.Text = "Endereço:";
            // 
            // lnumerou
            // 
            this.lnumerou.AutoSize = true;
            this.lnumerou.Location = new System.Drawing.Point(59, 133);
            this.lnumerou.Name = "lnumerou";
            this.lnumerou.Size = new System.Drawing.Size(58, 16);
            this.lnumerou.TabIndex = 2;
            this.lnumerou.Text = "Número:";
            // 
            // tbnomeu
            // 
            this.tbnomeu.Location = new System.Drawing.Point(142, 57);
            this.tbnomeu.Name = "tbnomeu";
            this.tbnomeu.Size = new System.Drawing.Size(100, 22);
            this.tbnomeu.TabIndex = 3;
            // 
            // tbenderecou
            // 
            this.tbenderecou.Location = new System.Drawing.Point(142, 93);
            this.tbenderecou.Name = "tbenderecou";
            this.tbenderecou.Size = new System.Drawing.Size(100, 22);
            this.tbenderecou.TabIndex = 4;
            // 
            // tbnumerou
            // 
            this.tbnumerou.Location = new System.Drawing.Point(142, 133);
            this.tbnumerou.Name = "tbnumerou";
            this.tbnumerou.Size = new System.Drawing.Size(100, 22);
            this.tbnumerou.TabIndex = 5;
            // 
            // bsalvarunidade
            // 
            this.bsalvarunidade.Location = new System.Drawing.Point(62, 183);
            this.bsalvarunidade.Name = "bsalvarunidade";
            this.bsalvarunidade.Size = new System.Drawing.Size(75, 23);
            this.bsalvarunidade.TabIndex = 6;
            this.bsalvarunidade.Text = "Salvar";
            this.bsalvarunidade.UseVisualStyleBackColor = true;
            this.bsalvarunidade.Click += new System.EventHandler(this.bsalvarunidade_Click);
            // 
            // dgvunidades
            // 
            this.dgvunidades.AllowUserToAddRows = false;
            this.dgvunidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvunidades.Location = new System.Drawing.Point(356, 221);
            this.dgvunidades.MultiSelect = false;
            this.dgvunidades.Name = "dgvunidades";
            this.dgvunidades.RowHeadersWidth = 51;
            this.dgvunidades.RowTemplate.Height = 24;
            this.dgvunidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvunidades.Size = new System.Drawing.Size(240, 150);
            this.dgvunidades.TabIndex = 7;
            this.dgvunidades.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvunidades_CellValidating);
            // 
            // bexcluirunidade
            // 
            this.bexcluirunidade.Location = new System.Drawing.Point(176, 183);
            this.bexcluirunidade.Name = "bexcluirunidade";
            this.bexcluirunidade.Size = new System.Drawing.Size(75, 23);
            this.bexcluirunidade.TabIndex = 8;
            this.bexcluirunidade.Text = "Excluir";
            this.bexcluirunidade.UseVisualStyleBackColor = true;
            this.bexcluirunidade.Click += new System.EventHandler(this.bexcluirunidade_Click);
            // 
            // CadastroUnidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bexcluirunidade);
            this.Controls.Add(this.dgvunidades);
            this.Controls.Add(this.bsalvarunidade);
            this.Controls.Add(this.tbnumerou);
            this.Controls.Add(this.tbenderecou);
            this.Controls.Add(this.tbnomeu);
            this.Controls.Add(this.lnumerou);
            this.Controls.Add(this.lenderecou);
            this.Controls.Add(this.lnomeu);
            this.Name = "CadastroUnidades";
            this.Text = "CadastroUnidades";
            ((System.ComponentModel.ISupportInitialize)(this.dgvunidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lnomeu;
        private System.Windows.Forms.Label lenderecou;
        private System.Windows.Forms.Label lnumerou;
        private System.Windows.Forms.TextBox tbnomeu;
        private System.Windows.Forms.TextBox tbenderecou;
        private System.Windows.Forms.TextBox tbnumerou;
        private System.Windows.Forms.Button bsalvarunidade;
        private System.Windows.Forms.DataGridView dgvunidades;
        private System.Windows.Forms.Button bexcluirunidade;
    }
}
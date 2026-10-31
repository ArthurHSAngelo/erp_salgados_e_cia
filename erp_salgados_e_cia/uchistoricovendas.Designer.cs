namespace erp_salgados_e_cia
{
    partial class uchistoricovendas
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
            this.dgvhistoricovendas = new System.Windows.Forms.DataGridView();
            this.lhistoricovendas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvhistoricovendas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvhistoricovendas
            // 
            this.dgvhistoricovendas.AllowUserToAddRows = false;
            this.dgvhistoricovendas.AllowUserToDeleteRows = false;
            this.dgvhistoricovendas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvhistoricovendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvhistoricovendas.Location = new System.Drawing.Point(60, 179);
            this.dgvhistoricovendas.Name = "dgvhistoricovendas";
            this.dgvhistoricovendas.ReadOnly = true;
            this.dgvhistoricovendas.RowHeadersWidth = 51;
            this.dgvhistoricovendas.RowTemplate.Height = 24;
            this.dgvhistoricovendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvhistoricovendas.Size = new System.Drawing.Size(2313, 1012);
            this.dgvhistoricovendas.TabIndex = 0;
            // 
            // lhistoricovendas
            // 
            this.lhistoricovendas.AutoSize = true;
            this.lhistoricovendas.Location = new System.Drawing.Point(67, 146);
            this.lhistoricovendas.Name = "lhistoricovendas";
            this.lhistoricovendas.Size = new System.Drawing.Size(130, 16);
            this.lhistoricovendas.TabIndex = 1;
            this.lhistoricovendas.Text = "Histórico de vendas:";
            // 
            // uchistoricovendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lhistoricovendas);
            this.Controls.Add(this.dgvhistoricovendas);
            this.Name = "uchistoricovendas";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvhistoricovendas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvhistoricovendas;
        private System.Windows.Forms.Label lhistoricovendas;
    }
}

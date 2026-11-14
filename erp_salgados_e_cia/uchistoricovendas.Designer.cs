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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvhistoricovendas = new System.Windows.Forms.DataGridView();
            this.lhistoricovendas = new System.Windows.Forms.Label();
            this.cbfiltrostatus = new System.Windows.Forms.ComboBox();
            this.bcancelarvenda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvhistoricovendas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvhistoricovendas
            // 
            this.dgvhistoricovendas.AllowUserToAddRows = false;
            this.dgvhistoricovendas.AllowUserToDeleteRows = false;
            this.dgvhistoricovendas.AllowUserToResizeColumns = false;
            this.dgvhistoricovendas.AllowUserToResizeRows = false;
            this.dgvhistoricovendas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvhistoricovendas.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvhistoricovendas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvhistoricovendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvhistoricovendas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvhistoricovendas.Location = new System.Drawing.Point(68, 112);
            this.dgvhistoricovendas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvhistoricovendas.Name = "dgvhistoricovendas";
            this.dgvhistoricovendas.ReadOnly = true;
            this.dgvhistoricovendas.RowHeadersWidth = 51;
            this.dgvhistoricovendas.RowTemplate.Height = 24;
            this.dgvhistoricovendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvhistoricovendas.Size = new System.Drawing.Size(2313, 1119);
            this.dgvhistoricovendas.TabIndex = 0;
            // 
            // lhistoricovendas
            // 
            this.lhistoricovendas.AutoSize = true;
            this.lhistoricovendas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lhistoricovendas.Location = new System.Drawing.Point(63, 81);
            this.lhistoricovendas.Name = "lhistoricovendas";
            this.lhistoricovendas.Size = new System.Drawing.Size(204, 28);
            this.lhistoricovendas.TabIndex = 1;
            this.lhistoricovendas.Text = "Histórico de vendas:";
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.BackColor = System.Drawing.Color.White;
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Location = new System.Drawing.Point(2219, 80);
            this.cbfiltrostatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(161, 31);
            this.cbfiltrostatus.TabIndex = 2;
            // 
            // bcancelarvenda
            // 
            this.bcancelarvenda.BackColor = System.Drawing.Color.White;
            this.bcancelarvenda.FlatAppearance.BorderSize = 2;
            this.bcancelarvenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bcancelarvenda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bcancelarvenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bcancelarvenda.Location = new System.Drawing.Point(83, 1247);
            this.bcancelarvenda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bcancelarvenda.Name = "bcancelarvenda";
            this.bcancelarvenda.Size = new System.Drawing.Size(169, 52);
            this.bcancelarvenda.TabIndex = 3;
            this.bcancelarvenda.Text = "Cancelar venda";
            this.bcancelarvenda.UseVisualStyleBackColor = false;
            // 
            // uchistoricovendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.bcancelarvenda);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.lhistoricovendas);
            this.Controls.Add(this.dgvhistoricovendas);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "uchistoricovendas";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvhistoricovendas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvhistoricovendas;
        private System.Windows.Forms.Label lhistoricovendas;
        private System.Windows.Forms.ComboBox cbfiltrostatus;
        private System.Windows.Forms.Button bcancelarvenda;
    }
}

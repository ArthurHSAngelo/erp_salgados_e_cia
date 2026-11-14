namespace erp_salgados_e_cia
{
    partial class ucdre
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
            this.dgvdre = new System.Windows.Forms.DataGridView();
            this.pfiltrosdre = new System.Windows.Forms.Panel();
            this.lperiododre = new System.Windows.Forms.Label();
            this.ldreaviso = new System.Windows.Forms.Label();
            this.cbdreunidade = new System.Windows.Forms.ComboBox();
            this.ldreunidade = new System.Windows.Forms.Label();
            this.dtpdrefim = new System.Windows.Forms.DateTimePicker();
            this.dtpdreinicio = new System.Windows.Forms.DateTimePicker();
            this.ldrefim = new System.Windows.Forms.Label();
            this.ldreinicio = new System.Windows.Forms.Label();
            this.bexportardre = new System.Windows.Forms.Button();
            this.ldrenota = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdre)).BeginInit();
            this.pfiltrosdre.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvdre
            // 
            this.dgvdre.AllowUserToAddRows = false;
            this.dgvdre.AllowUserToDeleteRows = false;
            this.dgvdre.AllowUserToResizeColumns = false;
            this.dgvdre.AllowUserToResizeRows = false;
            this.dgvdre.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvdre.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvdre.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvdre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvdre.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvdre.Location = new System.Drawing.Point(36, 286);
            this.dgvdre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvdre.MultiSelect = false;
            this.dgvdre.Name = "dgvdre";
            this.dgvdre.ReadOnly = true;
            this.dgvdre.RowHeadersWidth = 51;
            this.dgvdre.RowTemplate.Height = 24;
            this.dgvdre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvdre.Size = new System.Drawing.Size(2371, 993);
            this.dgvdre.TabIndex = 36;
            // 
            // pfiltrosdre
            // 
            this.pfiltrosdre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pfiltrosdre.Controls.Add(this.lperiododre);
            this.pfiltrosdre.Controls.Add(this.ldreaviso);
            this.pfiltrosdre.Controls.Add(this.cbdreunidade);
            this.pfiltrosdre.Controls.Add(this.ldreunidade);
            this.pfiltrosdre.Controls.Add(this.dtpdrefim);
            this.pfiltrosdre.Controls.Add(this.dtpdreinicio);
            this.pfiltrosdre.Controls.Add(this.ldrefim);
            this.pfiltrosdre.Controls.Add(this.ldreinicio);
            this.pfiltrosdre.Controls.Add(this.bexportardre);
            this.pfiltrosdre.Dock = System.Windows.Forms.DockStyle.Top;
            this.pfiltrosdre.Location = new System.Drawing.Point(0, 0);
            this.pfiltrosdre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pfiltrosdre.Name = "pfiltrosdre";
            this.pfiltrosdre.Size = new System.Drawing.Size(2445, 176);
            this.pfiltrosdre.TabIndex = 42;
            // 
            // lperiododre
            // 
            this.lperiododre.AutoSize = true;
            this.lperiododre.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.lperiododre.Location = new System.Drawing.Point(838, 88);
            this.lperiododre.Name = "lperiododre";
            this.lperiododre.Size = new System.Drawing.Size(94, 25);
            this.lperiododre.TabIndex = 46;
            this.lperiododre.Text = "Período: -";
            // 
            // ldreaviso
            // 
            this.ldreaviso.AutoSize = true;
            this.ldreaviso.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.ldreaviso.Location = new System.Drawing.Point(1201, 92);
            this.ldreaviso.Name = "ldreaviso";
            this.ldreaviso.Size = new System.Drawing.Size(0, 21);
            this.ldreaviso.TabIndex = 45;
            // 
            // cbdreunidade
            // 
            this.cbdreunidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdreunidade.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbdreunidade.FormattingEnabled = true;
            this.cbdreunidade.Location = new System.Drawing.Point(573, 88);
            this.cbdreunidade.Name = "cbdreunidade";
            this.cbdreunidade.Size = new System.Drawing.Size(182, 31);
            this.cbdreunidade.TabIndex = 44;
            // 
            // ldreunidade
            // 
            this.ldreunidade.AutoSize = true;
            this.ldreunidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.ldreunidade.Location = new System.Drawing.Point(569, 64);
            this.ldreunidade.Name = "ldreunidade";
            this.ldreunidade.Size = new System.Drawing.Size(79, 21);
            this.ldreunidade.TabIndex = 43;
            this.ldreunidade.Text = "Unidade:";
            // 
            // dtpdrefim
            // 
            this.dtpdrefim.Checked = false;
            this.dtpdrefim.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpdrefim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdrefim.Location = new System.Drawing.Point(345, 88);
            this.dtpdrefim.Name = "dtpdrefim";
            this.dtpdrefim.ShowCheckBox = true;
            this.dtpdrefim.Size = new System.Drawing.Size(136, 27);
            this.dtpdrefim.TabIndex = 42;
            // 
            // dtpdreinicio
            // 
            this.dtpdreinicio.Checked = false;
            this.dtpdreinicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpdreinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdreinicio.Location = new System.Drawing.Point(111, 88);
            this.dtpdreinicio.Name = "dtpdreinicio";
            this.dtpdreinicio.ShowCheckBox = true;
            this.dtpdreinicio.Size = new System.Drawing.Size(130, 27);
            this.dtpdreinicio.TabIndex = 41;
            // 
            // ldrefim
            // 
            this.ldrefim.AutoSize = true;
            this.ldrefim.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.ldrefim.Location = new System.Drawing.Point(341, 64);
            this.ldrefim.Name = "ldrefim";
            this.ldrefim.Size = new System.Drawing.Size(113, 21);
            this.ldrefim.TabIndex = 40;
            this.ldrefim.Text = "Período final:";
            // 
            // ldreinicio
            // 
            this.ldreinicio.AutoSize = true;
            this.ldreinicio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.ldreinicio.Location = new System.Drawing.Point(107, 64);
            this.ldreinicio.Name = "ldreinicio";
            this.ldreinicio.Size = new System.Drawing.Size(125, 21);
            this.ldreinicio.TabIndex = 39;
            this.ldreinicio.Text = "Período inicial:";
            // 
            // bexportardre
            // 
            this.bexportardre.BackColor = System.Drawing.Color.White;
            this.bexportardre.FlatAppearance.BorderSize = 2;
            this.bexportardre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexportardre.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexportardre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bexportardre.Location = new System.Drawing.Point(1639, 76);
            this.bexportardre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bexportardre.Name = "bexportardre";
            this.bexportardre.Size = new System.Drawing.Size(172, 52);
            this.bexportardre.TabIndex = 35;
            this.bexportardre.Text = "Exportar";
            this.bexportardre.UseVisualStyleBackColor = false;
            // 
            // ldrenota
            // 
            this.ldrenota.AutoSize = true;
            this.ldrenota.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ldrenota.Location = new System.Drawing.Point(51, 226);
            this.ldrenota.Name = "ldrenota";
            this.ldrenota.Size = new System.Drawing.Size(1542, 25);
            this.ldrenota.TabIndex = 47;
            this.ldrenota.Text = "Receita = vendas concluídas (a Notinha conta na data da venda). Despesas = entrad" +
    "as de estoque do período. As perdas já estão incluídas nas compras e não são som" +
    "adas de novo.";
            // 
            // ucdre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ldrenota);
            this.Controls.Add(this.dgvdre);
            this.Controls.Add(this.pfiltrosdre);
            this.Name = "ucdre";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdre)).EndInit();
            this.pfiltrosdre.ResumeLayout(false);
            this.pfiltrosdre.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvdre;
        private System.Windows.Forms.Panel pfiltrosdre;
        private System.Windows.Forms.Button bexportardre;
        private System.Windows.Forms.DateTimePicker dtpdrefim;
        private System.Windows.Forms.DateTimePicker dtpdreinicio;
        private System.Windows.Forms.Label ldrefim;
        private System.Windows.Forms.Label ldreinicio;
        private System.Windows.Forms.ComboBox cbdreunidade;
        private System.Windows.Forms.Label ldreunidade;
        private System.Windows.Forms.Label ldrenota;
        private System.Windows.Forms.Label lperiododre;
        private System.Windows.Forms.Label ldreaviso;
    }
}

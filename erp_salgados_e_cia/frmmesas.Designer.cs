namespace erp_salgados_e_cia
{
    partial class frmmesas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lmesasabertas = new System.Windows.Forms.Label();
            this.dgvmesas = new System.Windows.Forms.DataGridView();
            this.lsemmesas = new System.Windows.Forms.Label();
            this.babrirmesa = new System.Windows.Forms.Button();
            this.bfechar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvmesas)).BeginInit();
            this.SuspendLayout();
            // 
            // lmesasabertas
            // 
            this.lmesasabertas.AutoSize = true;
            this.lmesasabertas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lmesasabertas.Location = new System.Drawing.Point(20, 15);
            this.lmesasabertas.Name = "lmesasabertas";
            this.lmesasabertas.Size = new System.Drawing.Size(191, 28);
            this.lmesasabertas.TabIndex = 0;
            this.lmesasabertas.Text = "Comandas abertas:";
            // 
            // dgvmesas
            // 
            this.dgvmesas.AllowUserToAddRows = false;
            this.dgvmesas.AllowUserToDeleteRows = false;
            this.dgvmesas.AllowUserToResizeColumns = false;
            this.dgvmesas.AllowUserToResizeRows = false;
            this.dgvmesas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvmesas.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvmesas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvmesas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvmesas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvmesas.Location = new System.Drawing.Point(15, 55);
            this.dgvmesas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvmesas.MultiSelect = false;
            this.dgvmesas.Name = "dgvmesas";
            this.dgvmesas.ReadOnly = true;
            this.dgvmesas.RowHeadersWidth = 51;
            this.dgvmesas.RowTemplate.Height = 24;
            this.dgvmesas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmesas.Size = new System.Drawing.Size(975, 321);
            this.dgvmesas.TabIndex = 1;
            this.dgvmesas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvmesas_CellDoubleClick);
            // 
            // lsemmesas
            // 
            this.lsemmesas.BackColor = System.Drawing.Color.White;
            this.lsemmesas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsemmesas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lsemmesas.Location = new System.Drawing.Point(15, 55);
            this.lsemmesas.Name = "lsemmesas";
            this.lsemmesas.Size = new System.Drawing.Size(974, 321);
            this.lsemmesas.TabIndex = 2;
            this.lsemmesas.Text = "Nenhuma mesa aberta.";
            this.lsemmesas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // babrirmesa
            // 
            this.babrirmesa.BackColor = System.Drawing.Color.White;
            this.babrirmesa.FlatAppearance.BorderSize = 2;
            this.babrirmesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.babrirmesa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.babrirmesa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.babrirmesa.Location = new System.Drawing.Point(817, 431);
            this.babrirmesa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.babrirmesa.Name = "babrirmesa";
            this.babrirmesa.Size = new System.Drawing.Size(172, 52);
            this.babrirmesa.TabIndex = 3;
            this.babrirmesa.Text = "Abrir mesa";
            this.babrirmesa.UseVisualStyleBackColor = false;
            this.babrirmesa.Click += new System.EventHandler(this.babrirmesa_Click);
            // 
            // bfechar
            // 
            this.bfechar.BackColor = System.Drawing.Color.White;
            this.bfechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bfechar.FlatAppearance.BorderSize = 2;
            this.bfechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bfechar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bfechar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bfechar.Location = new System.Drawing.Point(620, 431);
            this.bfechar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bfechar.Name = "bfechar";
            this.bfechar.Size = new System.Drawing.Size(172, 52);
            this.bfechar.TabIndex = 4;
            this.bfechar.Text = "Fechar";
            this.bfechar.UseVisualStyleBackColor = false;
            // 
            // frmmesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.CancelButton = this.bfechar;
            this.ClientSize = new System.Drawing.Size(1004, 496);
            this.Controls.Add(this.bfechar);
            this.Controls.Add(this.babrirmesa);
            this.Controls.Add(this.lsemmesas);
            this.Controls.Add(this.dgvmesas);
            this.Controls.Add(this.lmesasabertas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmmesas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mesas abertas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvmesas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lmesasabertas;
        private System.Windows.Forms.DataGridView dgvmesas;
        private System.Windows.Forms.Label lsemmesas;
        private System.Windows.Forms.Button babrirmesa;
        private System.Windows.Forms.Button bfechar;
    }
}
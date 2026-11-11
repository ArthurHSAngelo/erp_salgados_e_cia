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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.lmesasabertas.Location = new System.Drawing.Point(15, 12);
            this.lmesasabertas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lmesasabertas.Name = "lmesasabertas";
            this.lmesasabertas.Size = new System.Drawing.Size(154, 21);
            this.lmesasabertas.TabIndex = 0;
            this.lmesasabertas.Text = "Comandas abertas:";
            // 
            // dgvmesas
            // 
            this.dgvmesas.AllowUserToAddRows = false;
            this.dgvmesas.AllowUserToDeleteRows = false;
            this.dgvmesas.AllowUserToResizeColumns = false;
            this.dgvmesas.AllowUserToResizeRows = false;
            this.dgvmesas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvmesas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvmesas.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvmesas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvmesas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvmesas.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvmesas.Location = new System.Drawing.Point(11, 45);
            this.dgvmesas.Margin = new System.Windows.Forms.Padding(2);
            this.dgvmesas.MultiSelect = false;
            this.dgvmesas.Name = "dgvmesas";
            this.dgvmesas.ReadOnly = true;
            this.dgvmesas.RowHeadersWidth = 51;
            this.dgvmesas.RowTemplate.Height = 24;
            this.dgvmesas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmesas.Size = new System.Drawing.Size(731, 261);
            this.dgvmesas.TabIndex = 1;
            this.dgvmesas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvmesas_CellDoubleClick);
            // 
            // lsemmesas
            // 
            this.lsemmesas.BackColor = System.Drawing.Color.White;
            this.lsemmesas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsemmesas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lsemmesas.Location = new System.Drawing.Point(11, 45);
            this.lsemmesas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lsemmesas.Name = "lsemmesas";
            this.lsemmesas.Size = new System.Drawing.Size(731, 261);
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
            this.babrirmesa.Location = new System.Drawing.Point(613, 350);
            this.babrirmesa.Margin = new System.Windows.Forms.Padding(2);
            this.babrirmesa.Name = "babrirmesa";
            this.babrirmesa.Size = new System.Drawing.Size(129, 42);
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
            this.bfechar.Location = new System.Drawing.Point(465, 350);
            this.bfechar.Margin = new System.Windows.Forms.Padding(2);
            this.bfechar.Name = "bfechar";
            this.bfechar.Size = new System.Drawing.Size(129, 42);
            this.bfechar.TabIndex = 4;
            this.bfechar.Text = "Fechar";
            this.bfechar.UseVisualStyleBackColor = false;
            // 
            // frmmesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.CancelButton = this.bfechar;
            this.ClientSize = new System.Drawing.Size(753, 403);
            this.Controls.Add(this.bfechar);
            this.Controls.Add(this.babrirmesa);
            this.Controls.Add(this.lsemmesas);
            this.Controls.Add(this.dgvmesas);
            this.Controls.Add(this.lmesasabertas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
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
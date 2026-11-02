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
            this.lmesasabertas.Location = new System.Drawing.Point(20, 15);
            this.lmesasabertas.Name = "lmesasabertas";
            this.lmesasabertas.Size = new System.Drawing.Size(125, 16);
            this.lmesasabertas.TabIndex = 0;
            this.lmesasabertas.Text = "Comandas abertas:";
            // 
            // dgvmesas
            // 
            this.dgvmesas.AllowUserToAddRows = false;
            this.dgvmesas.AllowUserToDeleteRows = false;
            this.dgvmesas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvmesas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvmesas.Location = new System.Drawing.Point(20, 55);
            this.dgvmesas.MultiSelect = false;
            this.dgvmesas.Name = "dgvmesas";
            this.dgvmesas.ReadOnly = true;
            this.dgvmesas.RowHeadersWidth = 51;
            this.dgvmesas.RowTemplate.Height = 24;
            this.dgvmesas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvmesas.Size = new System.Drawing.Size(720, 300);
            this.dgvmesas.TabIndex = 1;
            this.dgvmesas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvmesas_CellDoubleClick);
            // 
            // lsemmesas
            // 
            this.lsemmesas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lsemmesas.Location = new System.Drawing.Point(20, 55);
            this.lsemmesas.Name = "lsemmesas";
            this.lsemmesas.Size = new System.Drawing.Size(720, 300);
            this.lsemmesas.TabIndex = 2;
            this.lsemmesas.Text = "Nenhuma mesa aberta.";
            this.lsemmesas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // babrirmesa
            // 
            this.babrirmesa.Location = new System.Drawing.Point(420, 375);
            this.babrirmesa.Name = "babrirmesa";
            this.babrirmesa.Size = new System.Drawing.Size(101, 23);
            this.babrirmesa.TabIndex = 3;
            this.babrirmesa.Text = "Abrir mesa";
            this.babrirmesa.UseVisualStyleBackColor = true;
            this.babrirmesa.Click += new System.EventHandler(this.babrirmesa_Click);
            // 
            // bfechar
            // 
            this.bfechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bfechar.Location = new System.Drawing.Point(590, 375);
            this.bfechar.Name = "bfechar";
            this.bfechar.Size = new System.Drawing.Size(75, 23);
            this.bfechar.TabIndex = 4;
            this.bfechar.Text = "Fechar";
            this.bfechar.UseVisualStyleBackColor = true;
            // 
            // frmmesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bfechar;
            this.ClientSize = new System.Drawing.Size(762, 423);
            this.Controls.Add(this.bfechar);
            this.Controls.Add(this.babrirmesa);
            this.Controls.Add(this.lsemmesas);
            this.Controls.Add(this.dgvmesas);
            this.Controls.Add(this.lmesasabertas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
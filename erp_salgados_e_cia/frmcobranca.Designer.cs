namespace erp_salgados_e_cia
{
    partial class frmcobranca
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
            this.lfalta = new System.Windows.Forms.Label();
            this.lpago = new System.Windows.Forms.Label();
            this.bremoverpagamento = new System.Windows.Forms.Button();
            this.badicionarpagamento = new System.Windows.Forms.Button();
            this.lpagamentosvenda = new System.Windows.Forms.Label();
            this.dgvpagamentosvenda = new System.Windows.Forms.DataGridView();
            this.lvalorrecebido = new System.Windows.Forms.Label();
            this.tbvalorrecebido = new System.Windows.Forms.TextBox();
            this.ltroco = new System.Windows.Forms.Label();
            this.cbformapagamento = new System.Windows.Forms.ComboBox();
            this.lpagamento = new System.Windows.Forms.Label();
            this.ltotalvenda = new System.Windows.Forms.Label();
            this.bconfirmarvenda = new System.Windows.Forms.Button();
            this.bcancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpagamentosvenda)).BeginInit();
            this.SuspendLayout();
            // 
            // lfalta
            // 
            this.lfalta.AutoSize = true;
            this.lfalta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lfalta.Location = new System.Drawing.Point(455, 337);
            this.lfalta.Name = "lfalta";
            this.lfalta.Size = new System.Drawing.Size(140, 28);
            this.lfalta.TabIndex = 38;
            this.lfalta.Text = "Falta: R$ 0,00";
            // 
            // lpago
            // 
            this.lpago.AutoSize = true;
            this.lpago.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lpago.Location = new System.Drawing.Point(452, 298);
            this.lpago.Name = "lpago";
            this.lpago.Size = new System.Drawing.Size(142, 28);
            this.lpago.TabIndex = 37;
            this.lpago.Text = "Pago: R$ 0,00";
            // 
            // bremoverpagamento
            // 
            this.bremoverpagamento.BackColor = System.Drawing.Color.White;
            this.bremoverpagamento.FlatAppearance.BorderSize = 2;
            this.bremoverpagamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bremoverpagamento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bremoverpagamento.ForeColor = System.Drawing.Color.Black;
            this.bremoverpagamento.Location = new System.Drawing.Point(651, 220);
            this.bremoverpagamento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bremoverpagamento.Name = "bremoverpagamento";
            this.bremoverpagamento.Size = new System.Drawing.Size(191, 37);
            this.bremoverpagamento.TabIndex = 36;
            this.bremoverpagamento.Text = "Remover pagamento";
            this.bremoverpagamento.UseVisualStyleBackColor = false;
            this.bremoverpagamento.Click += new System.EventHandler(this.bremoverpagamento_Click);
            // 
            // badicionarpagamento
            // 
            this.badicionarpagamento.BackColor = System.Drawing.Color.White;
            this.badicionarpagamento.FlatAppearance.BorderSize = 2;
            this.badicionarpagamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.badicionarpagamento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.badicionarpagamento.ForeColor = System.Drawing.Color.Black;
            this.badicionarpagamento.Location = new System.Drawing.Point(455, 220);
            this.badicionarpagamento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.badicionarpagamento.Name = "badicionarpagamento";
            this.badicionarpagamento.Size = new System.Drawing.Size(191, 37);
            this.badicionarpagamento.TabIndex = 35;
            this.badicionarpagamento.Text = "Adicionar pagamento";
            this.badicionarpagamento.UseVisualStyleBackColor = false;
            this.badicionarpagamento.Click += new System.EventHandler(this.badicionarpagamento_Click);
            // 
            // lpagamentosvenda
            // 
            this.lpagamentosvenda.AutoSize = true;
            this.lpagamentosvenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lpagamentosvenda.Location = new System.Drawing.Point(14, 184);
            this.lpagamentosvenda.Name = "lpagamentosvenda";
            this.lpagamentosvenda.Size = new System.Drawing.Size(225, 28);
            this.lpagamentosvenda.TabIndex = 34;
            this.lpagamentosvenda.Text = "Pagamentos da venda:";
            // 
            // dgvpagamentosvenda
            // 
            this.dgvpagamentosvenda.AllowUserToAddRows = false;
            this.dgvpagamentosvenda.AllowUserToDeleteRows = false;
            this.dgvpagamentosvenda.AllowUserToResizeColumns = false;
            this.dgvpagamentosvenda.AllowUserToResizeRows = false;
            this.dgvpagamentosvenda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvpagamentosvenda.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvpagamentosvenda.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvpagamentosvenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvpagamentosvenda.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvpagamentosvenda.Location = new System.Drawing.Point(15, 214);
            this.dgvpagamentosvenda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvpagamentosvenda.Name = "dgvpagamentosvenda";
            this.dgvpagamentosvenda.ReadOnly = true;
            this.dgvpagamentosvenda.RowHeadersWidth = 51;
            this.dgvpagamentosvenda.RowTemplate.Height = 24;
            this.dgvpagamentosvenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvpagamentosvenda.Size = new System.Drawing.Size(412, 199);
            this.dgvpagamentosvenda.TabIndex = 33;
            // 
            // lvalorrecebido
            // 
            this.lvalorrecebido.AutoSize = true;
            this.lvalorrecebido.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lvalorrecebido.Location = new System.Drawing.Point(547, 103);
            this.lvalorrecebido.Name = "lvalorrecebido";
            this.lvalorrecebido.Size = new System.Drawing.Size(153, 28);
            this.lvalorrecebido.TabIndex = 32;
            this.lvalorrecebido.Text = "Valor recebido:";
            // 
            // tbvalorrecebido
            // 
            this.tbvalorrecebido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbvalorrecebido.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbvalorrecebido.Location = new System.Drawing.Point(719, 100);
            this.tbvalorrecebido.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbvalorrecebido.Name = "tbvalorrecebido";
            this.tbvalorrecebido.Size = new System.Drawing.Size(134, 30);
            this.tbvalorrecebido.TabIndex = 31;
            this.tbvalorrecebido.TextChanged += new System.EventHandler(this.tbvalorrecebido_TextChanged);
            // 
            // ltroco
            // 
            this.ltroco.AutoSize = true;
            this.ltroco.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ltroco.Location = new System.Drawing.Point(455, 377);
            this.ltroco.Name = "ltroco";
            this.ltroco.Size = new System.Drawing.Size(147, 28);
            this.ltroco.TabIndex = 30;
            this.ltroco.Text = "Troco: R$ 0,00";
            // 
            // cbformapagamento
            // 
            this.cbformapagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbformapagamento.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbformapagamento.FormattingEnabled = true;
            this.cbformapagamento.Items.AddRange(new object[] {
            "Pix",
            "Dinheiro",
            "Débito",
            "Crédito",
            "Notinha"});
            this.cbformapagamento.Location = new System.Drawing.Point(263, 98);
            this.cbformapagamento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbformapagamento.Name = "cbformapagamento";
            this.cbformapagamento.Size = new System.Drawing.Size(240, 31);
            this.cbformapagamento.TabIndex = 29;
            this.cbformapagamento.SelectedIndexChanged += new System.EventHandler(this.cbformapagamento_SelectedIndexChanged);
            // 
            // lpagamento
            // 
            this.lpagamento.AutoSize = true;
            this.lpagamento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lpagamento.Location = new System.Drawing.Point(21, 100);
            this.lpagamento.Name = "lpagamento";
            this.lpagamento.Size = new System.Drawing.Size(218, 28);
            this.lpagamento.TabIndex = 28;
            this.lpagamento.Text = "Forma de pagamento:";
            // 
            // ltotalvenda
            // 
            this.ltotalvenda.AutoSize = true;
            this.ltotalvenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltotalvenda.Location = new System.Drawing.Point(9, 11);
            this.ltotalvenda.Name = "ltotalvenda";
            this.ltotalvenda.Size = new System.Drawing.Size(228, 28);
            this.ltotalvenda.TabIndex = 27;
            this.ltotalvenda.Text = "Total da venda: R$0,00";
            // 
            // bconfirmarvenda
            // 
            this.bconfirmarvenda.BackColor = System.Drawing.Color.White;
            this.bconfirmarvenda.FlatAppearance.BorderSize = 2;
            this.bconfirmarvenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bconfirmarvenda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bconfirmarvenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.bconfirmarvenda.Location = new System.Drawing.Point(681, 491);
            this.bconfirmarvenda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bconfirmarvenda.Name = "bconfirmarvenda";
            this.bconfirmarvenda.Size = new System.Drawing.Size(172, 52);
            this.bconfirmarvenda.TabIndex = 39;
            this.bconfirmarvenda.Text = "Confirmar venda";
            this.bconfirmarvenda.UseVisualStyleBackColor = false;
            this.bconfirmarvenda.Click += new System.EventHandler(this.bconfirmarvenda_Click);
            // 
            // bcancelar
            // 
            this.bcancelar.BackColor = System.Drawing.Color.White;
            this.bcancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bcancelar.FlatAppearance.BorderSize = 2;
            this.bcancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bcancelar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bcancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.bcancelar.Location = new System.Drawing.Point(491, 491);
            this.bcancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bcancelar.Name = "bcancelar";
            this.bcancelar.Size = new System.Drawing.Size(172, 52);
            this.bcancelar.TabIndex = 40;
            this.bcancelar.Text = "Cancelar";
            this.bcancelar.UseVisualStyleBackColor = false;
            // 
            // frmcobranca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.CancelButton = this.bcancelar;
            this.ClientSize = new System.Drawing.Size(868, 556);
            this.Controls.Add(this.bcancelar);
            this.Controls.Add(this.bconfirmarvenda);
            this.Controls.Add(this.lfalta);
            this.Controls.Add(this.lpago);
            this.Controls.Add(this.bremoverpagamento);
            this.Controls.Add(this.badicionarpagamento);
            this.Controls.Add(this.lpagamentosvenda);
            this.Controls.Add(this.dgvpagamentosvenda);
            this.Controls.Add(this.lvalorrecebido);
            this.Controls.Add(this.tbvalorrecebido);
            this.Controls.Add(this.ltroco);
            this.Controls.Add(this.cbformapagamento);
            this.Controls.Add(this.lpagamento);
            this.Controls.Add(this.ltotalvenda);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmcobranca";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cobrança";
            ((System.ComponentModel.ISupportInitialize)(this.dgvpagamentosvenda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lfalta;
        private System.Windows.Forms.Label lpago;
        private System.Windows.Forms.Button bremoverpagamento;
        private System.Windows.Forms.Button badicionarpagamento;
        private System.Windows.Forms.Label lpagamentosvenda;
        private System.Windows.Forms.DataGridView dgvpagamentosvenda;
        private System.Windows.Forms.Label lvalorrecebido;
        private System.Windows.Forms.TextBox tbvalorrecebido;
        private System.Windows.Forms.Label ltroco;
        private System.Windows.Forms.ComboBox cbformapagamento;
        private System.Windows.Forms.Label lpagamento;
        private System.Windows.Forms.Label ltotalvenda;
        private System.Windows.Forms.Button bconfirmarvenda;
        private System.Windows.Forms.Button bcancelar;
    }
}
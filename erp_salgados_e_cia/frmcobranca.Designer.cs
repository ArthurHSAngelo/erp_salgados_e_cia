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
            this.lfalta.Location = new System.Drawing.Point(24, 420);
            this.lfalta.Name = "lfalta";
            this.lfalta.Size = new System.Drawing.Size(87, 16);
            this.lfalta.TabIndex = 38;
            this.lfalta.Text = "Falta: R$ 0,00";
            // 
            // lpago
            // 
            this.lpago.AutoSize = true;
            this.lpago.Location = new System.Drawing.Point(24, 395);
            this.lpago.Name = "lpago";
            this.lpago.Size = new System.Drawing.Size(90, 16);
            this.lpago.TabIndex = 37;
            this.lpago.Text = "Pago: R$ 0,00";
            // 
            // bremoverpagamento
            // 
            this.bremoverpagamento.Location = new System.Drawing.Point(200, 106);
            this.bremoverpagamento.Name = "bremoverpagamento";
            this.bremoverpagamento.Size = new System.Drawing.Size(174, 23);
            this.bremoverpagamento.TabIndex = 36;
            this.bremoverpagamento.Text = "Remover pagamento";
            this.bremoverpagamento.UseVisualStyleBackColor = true;
            this.bremoverpagamento.Click += new System.EventHandler(this.bremoverpagamento_Click);
            // 
            // badicionarpagamento
            // 
            this.badicionarpagamento.Location = new System.Drawing.Point(24, 106);
            this.badicionarpagamento.Name = "badicionarpagamento";
            this.badicionarpagamento.Size = new System.Drawing.Size(154, 23);
            this.badicionarpagamento.TabIndex = 35;
            this.badicionarpagamento.Text = "Adicionar pagamento";
            this.badicionarpagamento.UseVisualStyleBackColor = true;
            this.badicionarpagamento.Click += new System.EventHandler(this.badicionarpagamento_Click);
            // 
            // lpagamentosvenda
            // 
            this.lpagamentosvenda.AutoSize = true;
            this.lpagamentosvenda.Location = new System.Drawing.Point(24, 150);
            this.lpagamentosvenda.Name = "lpagamentosvenda";
            this.lpagamentosvenda.Size = new System.Drawing.Size(147, 16);
            this.lpagamentosvenda.TabIndex = 34;
            this.lpagamentosvenda.Text = "Pagamentos da venda:";
            // 
            // dgvpagamentosvenda
            // 
            this.dgvpagamentosvenda.AllowUserToAddRows = false;
            this.dgvpagamentosvenda.AllowUserToDeleteRows = false;
            this.dgvpagamentosvenda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvpagamentosvenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpagamentosvenda.Location = new System.Drawing.Point(24, 175);
            this.dgvpagamentosvenda.Name = "dgvpagamentosvenda";
            this.dgvpagamentosvenda.ReadOnly = true;
            this.dgvpagamentosvenda.RowHeadersWidth = 51;
            this.dgvpagamentosvenda.RowTemplate.Height = 24;
            this.dgvpagamentosvenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvpagamentosvenda.Size = new System.Drawing.Size(700, 200);
            this.dgvpagamentosvenda.TabIndex = 33;
            // 
            // lvalorrecebido
            // 
            this.lvalorrecebido.AutoSize = true;
            this.lvalorrecebido.Location = new System.Drawing.Point(340, 66);
            this.lvalorrecebido.Name = "lvalorrecebido";
            this.lvalorrecebido.Size = new System.Drawing.Size(99, 16);
            this.lvalorrecebido.TabIndex = 32;
            this.lvalorrecebido.Text = "Valor recebido:";
            // 
            // tbvalorrecebido
            // 
            this.tbvalorrecebido.Location = new System.Drawing.Point(440, 63);
            this.tbvalorrecebido.Name = "tbvalorrecebido";
            this.tbvalorrecebido.Size = new System.Drawing.Size(100, 22);
            this.tbvalorrecebido.TabIndex = 31;
            this.tbvalorrecebido.TextChanged += new System.EventHandler(this.tbvalorrecebido_TextChanged);
            // 
            // ltroco
            // 
            this.ltroco.AutoSize = true;
            this.ltroco.Location = new System.Drawing.Point(24, 445);
            this.ltroco.Name = "ltroco";
            this.ltroco.Size = new System.Drawing.Size(93, 16);
            this.ltroco.TabIndex = 30;
            this.ltroco.Text = "Troco: R$ 0,00";
            // 
            // cbformapagamento
            // 
            this.cbformapagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbformapagamento.FormattingEnabled = true;
            this.cbformapagamento.Items.AddRange(new object[] {
            "Dinheiro",
            "Pix",
            "Cartão",
            "Notinha"});
            this.cbformapagamento.Location = new System.Drawing.Point(170, 63);
            this.cbformapagamento.Name = "cbformapagamento";
            this.cbformapagamento.Size = new System.Drawing.Size(120, 24);
            this.cbformapagamento.TabIndex = 29;
            this.cbformapagamento.SelectedIndexChanged += new System.EventHandler(this.cbformapagamento_SelectedIndexChanged);
            // 
            // lpagamento
            // 
            this.lpagamento.AutoSize = true;
            this.lpagamento.Location = new System.Drawing.Point(24, 66);
            this.lpagamento.Name = "lpagamento";
            this.lpagamento.Size = new System.Drawing.Size(140, 16);
            this.lpagamento.TabIndex = 28;
            this.lpagamento.Text = "Forma de pagamento:";
            // 
            // ltotalvenda
            // 
            this.ltotalvenda.AutoSize = true;
            this.ltotalvenda.Location = new System.Drawing.Point(24, 20);
            this.ltotalvenda.Name = "ltotalvenda";
            this.ltotalvenda.Size = new System.Drawing.Size(145, 16);
            this.ltotalvenda.TabIndex = 27;
            this.ltotalvenda.Text = "Total da venda: R$0,00";
            // 
            // bconfirmarvenda
            // 
            this.bconfirmarvenda.Location = new System.Drawing.Point(590, 500);
            this.bconfirmarvenda.Name = "bconfirmarvenda";
            this.bconfirmarvenda.Size = new System.Drawing.Size(154, 23);
            this.bconfirmarvenda.TabIndex = 39;
            this.bconfirmarvenda.Text = "Confirmar venda";
            this.bconfirmarvenda.UseVisualStyleBackColor = true;
            this.bconfirmarvenda.Click += new System.EventHandler(this.bconfirmarvenda_Click);
            // 
            // bcancelar
            // 
            this.bcancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bcancelar.Location = new System.Drawing.Point(425, 500);
            this.bcancelar.Name = "bcancelar";
            this.bcancelar.Size = new System.Drawing.Size(154, 23);
            this.bcancelar.TabIndex = 40;
            this.bcancelar.Text = "Cancelar";
            this.bcancelar.UseVisualStyleBackColor = true;
            // 
            // frmcobranca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bcancelar;
            this.ClientSize = new System.Drawing.Size(762, 543);
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
namespace erp_salgados_e_cia
{
    partial class uccaixa
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
            this.ltitulocaixa = new System.Windows.Forms.Label();
            this.labrircaixa = new System.Windows.Forms.Label();
            this.lvalorinicial = new System.Windows.Forms.Label();
            this.lresponsavel = new System.Windows.Forms.Label();
            this.pabertura = new System.Windows.Forms.Panel();
            this.babrircaixa = new System.Windows.Forms.Button();
            this.cbresponsavel = new System.Windows.Forms.ComboBox();
            this.tbvalorinicial = new System.Windows.Forms.TextBox();
            this.presumo = new System.Windows.Forms.Panel();
            this.ldebito = new System.Windows.Forms.Label();
            this.bfecharcaixa = new System.Windows.Forms.Button();
            this.ldiferenca = new System.Windows.Forms.Label();
            this.tbvalorcontado = new System.Windows.Forms.TextBox();
            this.labatimentos = new System.Windows.Forms.Label();
            this.ldinheiroesperado = new System.Windows.Forms.Label();
            this.lvalorcontado = new System.Windows.Forms.Label();
            this.lnotinha = new System.Windows.Forms.Label();
            this.ltotalvendas = new System.Windows.Forms.Label();
            this.lcredito = new System.Windows.Forms.Label();
            this.lpix = new System.Windows.Forms.Label();
            this.ldinheiro = new System.Windows.Forms.Label();
            this.ltituloformas = new System.Windows.Forms.Label();
            this.lvalorinicialresumo = new System.Windows.Forms.Label();
            this.linfocaixa = new System.Windows.Forms.Label();
            this.lcaixaaberto = new System.Windows.Forms.Label();
            this.lhistoricocaixas = new System.Windows.Forms.Label();
            this.dgvcaixas = new System.Windows.Forms.DataGridView();
            this.pabertura.SuspendLayout();
            this.presumo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcaixas)).BeginInit();
            this.SuspendLayout();
            // 
            // ltitulocaixa
            // 
            this.ltitulocaixa.AutoSize = true;
            this.ltitulocaixa.Location = new System.Drawing.Point(40, 30);
            this.ltitulocaixa.Name = "ltitulocaixa";
            this.ltitulocaixa.Size = new System.Drawing.Size(41, 16);
            this.ltitulocaixa.TabIndex = 0;
            this.ltitulocaixa.Text = "Caixa";
            // 
            // labrircaixa
            // 
            this.labrircaixa.AutoSize = true;
            this.labrircaixa.Location = new System.Drawing.Point(20, 15);
            this.labrircaixa.Name = "labrircaixa";
            this.labrircaixa.Size = new System.Drawing.Size(70, 16);
            this.labrircaixa.TabIndex = 1;
            this.labrircaixa.Text = "Abrir caixa";
            // 
            // lvalorinicial
            // 
            this.lvalorinicial.AutoSize = true;
            this.lvalorinicial.Location = new System.Drawing.Point(20, 110);
            this.lvalorinicial.Name = "lvalorinicial";
            this.lvalorinicial.Size = new System.Drawing.Size(123, 16);
            this.lvalorinicial.TabIndex = 2;
            this.lvalorinicial.Text = "Valor inicial: R$0,00";
            // 
            // lresponsavel
            // 
            this.lresponsavel.AutoSize = true;
            this.lresponsavel.Location = new System.Drawing.Point(20, 65);
            this.lresponsavel.Name = "lresponsavel";
            this.lresponsavel.Size = new System.Drawing.Size(91, 16);
            this.lresponsavel.TabIndex = 3;
            this.lresponsavel.Text = "Responsável:";
            // 
            // pabertura
            // 
            this.pabertura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pabertura.Controls.Add(this.babrircaixa);
            this.pabertura.Controls.Add(this.cbresponsavel);
            this.pabertura.Controls.Add(this.tbvalorinicial);
            this.pabertura.Controls.Add(this.lvalorinicial);
            this.pabertura.Controls.Add(this.labrircaixa);
            this.pabertura.Controls.Add(this.lresponsavel);
            this.pabertura.Location = new System.Drawing.Point(40, 90);
            this.pabertura.Name = "pabertura";
            this.pabertura.Size = new System.Drawing.Size(520, 220);
            this.pabertura.TabIndex = 4;
            // 
            // babrircaixa
            // 
            this.babrircaixa.Location = new System.Drawing.Point(20, 160);
            this.babrircaixa.Name = "babrircaixa";
            this.babrircaixa.Size = new System.Drawing.Size(107, 23);
            this.babrircaixa.TabIndex = 6;
            this.babrircaixa.Text = "Abrir caixa";
            this.babrircaixa.UseVisualStyleBackColor = true;
            this.babrircaixa.Click += new System.EventHandler(this.babrircaixa_Click);
            // 
            // cbresponsavel
            // 
            this.cbresponsavel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbresponsavel.FormattingEnabled = true;
            this.cbresponsavel.Location = new System.Drawing.Point(150, 62);
            this.cbresponsavel.Name = "cbresponsavel";
            this.cbresponsavel.Size = new System.Drawing.Size(121, 24);
            this.cbresponsavel.TabIndex = 5;
            // 
            // tbvalorinicial
            // 
            this.tbvalorinicial.Location = new System.Drawing.Point(170, 107);
            this.tbvalorinicial.Name = "tbvalorinicial";
            this.tbvalorinicial.Size = new System.Drawing.Size(100, 22);
            this.tbvalorinicial.TabIndex = 5;
            // 
            // presumo
            // 
            this.presumo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.presumo.Controls.Add(this.ldebito);
            this.presumo.Controls.Add(this.bfecharcaixa);
            this.presumo.Controls.Add(this.ldiferenca);
            this.presumo.Controls.Add(this.tbvalorcontado);
            this.presumo.Controls.Add(this.labatimentos);
            this.presumo.Controls.Add(this.ldinheiroesperado);
            this.presumo.Controls.Add(this.lvalorcontado);
            this.presumo.Controls.Add(this.lnotinha);
            this.presumo.Controls.Add(this.ltotalvendas);
            this.presumo.Controls.Add(this.lcredito);
            this.presumo.Controls.Add(this.lpix);
            this.presumo.Controls.Add(this.ldinheiro);
            this.presumo.Controls.Add(this.ltituloformas);
            this.presumo.Controls.Add(this.lvalorinicialresumo);
            this.presumo.Controls.Add(this.linfocaixa);
            this.presumo.Controls.Add(this.lcaixaaberto);
            this.presumo.Location = new System.Drawing.Point(40, 90);
            this.presumo.Name = "presumo";
            this.presumo.Size = new System.Drawing.Size(520, 480);
            this.presumo.TabIndex = 5;
            // 
            // ldebito
            // 
            this.ldebito.AutoSize = true;
            this.ldebito.Location = new System.Drawing.Point(138, 203);
            this.ldebito.Name = "ldebito";
            this.ldebito.Size = new System.Drawing.Size(97, 16);
            this.ldebito.TabIndex = 22;
            this.ldebito.Text = "\tDébito: R$ 0,00";
            // 
            // bfecharcaixa
            // 
            this.bfecharcaixa.Location = new System.Drawing.Point(20, 430);
            this.bfecharcaixa.Name = "bfecharcaixa";
            this.bfecharcaixa.Size = new System.Drawing.Size(75, 23);
            this.bfecharcaixa.TabIndex = 21;
            this.bfecharcaixa.Text = "Fechar caixa";
            this.bfecharcaixa.UseVisualStyleBackColor = true;
            this.bfecharcaixa.Click += new System.EventHandler(this.bfecharcaixa_Click);
            // 
            // ldiferenca
            // 
            this.ldiferenca.AutoSize = true;
            this.ldiferenca.Location = new System.Drawing.Point(20, 395);
            this.ldiferenca.Name = "ldiferenca";
            this.ldiferenca.Size = new System.Drawing.Size(115, 16);
            this.ldiferenca.TabIndex = 20;
            this.ldiferenca.Text = "Diferença: R$ 0,00";
            // 
            // tbvalorcontado
            // 
            this.tbvalorcontado.Location = new System.Drawing.Point(120, 359);
            this.tbvalorcontado.Name = "tbvalorcontado";
            this.tbvalorcontado.Size = new System.Drawing.Size(100, 22);
            this.tbvalorcontado.TabIndex = 19;
            this.tbvalorcontado.TextChanged += new System.EventHandler(this.tbvalorcontado_TextChanged);
            // 
            // labatimentos
            // 
            this.labatimentos.AutoSize = true;
            this.labatimentos.Location = new System.Drawing.Point(20, 280);
            this.labatimentos.Name = "labatimentos";
            this.labatimentos.Size = new System.Drawing.Size(264, 16);
            this.labatimentos.TabIndex = 18;
            this.labatimentos.Text = "Abatimentos de Notinha recebidos: R$ 0,00";
            // 
            // ldinheiroesperado
            // 
            this.ldinheiroesperado.AutoSize = true;
            this.ldinheiroesperado.Location = new System.Drawing.Point(20, 320);
            this.ldinheiroesperado.Name = "ldinheiroesperado";
            this.ldinheiroesperado.Size = new System.Drawing.Size(169, 16);
            this.ldinheiroesperado.TabIndex = 17;
            this.ldinheiroesperado.Text = "Dinheiro esperado: R$ 0,00";
            // 
            // lvalorcontado
            // 
            this.lvalorcontado.AutoSize = true;
            this.lvalorcontado.Location = new System.Drawing.Point(20, 362);
            this.lvalorcontado.Name = "lvalorcontado";
            this.lvalorcontado.Size = new System.Drawing.Size(94, 16);
            this.lvalorcontado.TabIndex = 16;
            this.lvalorcontado.Text = "Valor contado:";
            // 
            // lnotinha
            // 
            this.lnotinha.AutoSize = true;
            this.lnotinha.Location = new System.Drawing.Point(20, 255);
            this.lnotinha.Name = "lnotinha";
            this.lnotinha.Size = new System.Drawing.Size(185, 16);
            this.lnotinha.TabIndex = 7;
            this.lnotinha.Text = "Notinha do dia (fiado): R$ 0,00";
            // 
            // ltotalvendas
            // 
            this.ltotalvendas.AutoSize = true;
            this.ltotalvendas.Location = new System.Drawing.Point(20, 230);
            this.ltotalvendas.Name = "ltotalvendas";
            this.ltotalvendas.Size = new System.Drawing.Size(241, 16);
            this.ltotalvendas.TabIndex = 8;
            this.ltotalvendas.Text = "Total de vendas (sem Notinha): R$ 0,00\t";
            // 
            // lcredito
            // 
            this.lcredito.AutoSize = true;
            this.lcredito.Location = new System.Drawing.Point(20, 202);
            this.lcredito.Name = "lcredito";
            this.lcredito.Size = new System.Drawing.Size(100, 16);
            this.lcredito.TabIndex = 9;
            this.lcredito.Text = "Crédito: R$ 0,00";
            // 
            // lpix
            // 
            this.lpix.AutoSize = true;
            this.lpix.Location = new System.Drawing.Point(20, 167);
            this.lpix.Name = "lpix";
            this.lpix.Size = new System.Drawing.Size(75, 16);
            this.lpix.TabIndex = 10;
            this.lpix.Text = "Pix: R$ 0,00";
            // 
            // ldinheiro
            // 
            this.ldinheiro.AutoSize = true;
            this.ldinheiro.Location = new System.Drawing.Point(20, 140);
            this.ldinheiro.Name = "ldinheiro";
            this.ldinheiro.Size = new System.Drawing.Size(107, 16);
            this.ldinheiro.TabIndex = 11;
            this.ldinheiro.Text = "Dinheiro: R$ 0,00";
            // 
            // ltituloformas
            // 
            this.ltituloformas.AutoSize = true;
            this.ltituloformas.Location = new System.Drawing.Point(20, 110);
            this.ltituloformas.Name = "ltituloformas";
            this.ltituloformas.Size = new System.Drawing.Size(364, 16);
            this.ltituloformas.TabIndex = 12;
            this.ltituloformas.Text = "Recebido por forma de pagamento (vendas + abatimentos):";
            // 
            // lvalorinicialresumo
            // 
            this.lvalorinicialresumo.AutoSize = true;
            this.lvalorinicialresumo.Location = new System.Drawing.Point(20, 80);
            this.lvalorinicialresumo.Name = "lvalorinicialresumo";
            this.lvalorinicialresumo.Size = new System.Drawing.Size(126, 16);
            this.lvalorinicialresumo.TabIndex = 13;
            this.lvalorinicialresumo.Text = "Valor inicial: R$ 0,00";
            // 
            // linfocaixa
            // 
            this.linfocaixa.AutoSize = true;
            this.linfocaixa.Location = new System.Drawing.Point(20, 45);
            this.linfocaixa.Name = "linfocaixa";
            this.linfocaixa.Size = new System.Drawing.Size(277, 16);
            this.linfocaixa.TabIndex = 14;
            this.linfocaixa.Text = "Aberto em 00/00/0000 00:00 por Responsável";
            // 
            // lcaixaaberto
            // 
            this.lcaixaaberto.AutoSize = true;
            this.lcaixaaberto.Location = new System.Drawing.Point(20, 15);
            this.lcaixaaberto.Name = "lcaixaaberto";
            this.lcaixaaberto.Size = new System.Drawing.Size(83, 16);
            this.lcaixaaberto.TabIndex = 6;
            this.lcaixaaberto.Text = "Caixa aberto";
            // 
            // lhistoricocaixas
            // 
            this.lhistoricocaixas.AutoSize = true;
            this.lhistoricocaixas.Location = new System.Drawing.Point(40, 590);
            this.lhistoricocaixas.Name = "lhistoricocaixas";
            this.lhistoricocaixas.Size = new System.Drawing.Size(121, 16);
            this.lhistoricocaixas.TabIndex = 22;
            this.lhistoricocaixas.Text = "Histórico de caixas";
            // 
            // dgvcaixas
            // 
            this.dgvcaixas.AllowUserToAddRows = false;
            this.dgvcaixas.AllowUserToDeleteRows = false;
            this.dgvcaixas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvcaixas.Location = new System.Drawing.Point(40, 620);
            this.dgvcaixas.MultiSelect = false;
            this.dgvcaixas.Name = "dgvcaixas";
            this.dgvcaixas.ReadOnly = true;
            this.dgvcaixas.RowHeadersWidth = 51;
            this.dgvcaixas.RowTemplate.Height = 24;
            this.dgvcaixas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvcaixas.Size = new System.Drawing.Size(1100, 250);
            this.dgvcaixas.TabIndex = 23;
            // 
            // uccaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvcaixas);
            this.Controls.Add(this.lhistoricocaixas);
            this.Controls.Add(this.presumo);
            this.Controls.Add(this.pabertura);
            this.Controls.Add(this.ltitulocaixa);
            this.Name = "uccaixa";
            this.Size = new System.Drawing.Size(2445, 1332);
            this.pabertura.ResumeLayout(false);
            this.pabertura.PerformLayout();
            this.presumo.ResumeLayout(false);
            this.presumo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcaixas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ltitulocaixa;
        private System.Windows.Forms.Label labrircaixa;
        private System.Windows.Forms.Label lvalorinicial;
        private System.Windows.Forms.Label lresponsavel;
        private System.Windows.Forms.Panel pabertura;
        private System.Windows.Forms.ComboBox cbresponsavel;
        private System.Windows.Forms.TextBox tbvalorinicial;
        private System.Windows.Forms.Button babrircaixa;
        private System.Windows.Forms.Panel presumo;
        private System.Windows.Forms.Label lcredito;
        private System.Windows.Forms.Label lpix;
        private System.Windows.Forms.Label ldinheiro;
        private System.Windows.Forms.Label ltituloformas;
        private System.Windows.Forms.Label lvalorinicialresumo;
        private System.Windows.Forms.Label linfocaixa;
        private System.Windows.Forms.Label lcaixaaberto;
        private System.Windows.Forms.Label lnotinha;
        private System.Windows.Forms.Label ltotalvendas;
        private System.Windows.Forms.Label ldinheiroesperado;
        private System.Windows.Forms.Label lvalorcontado;
        private System.Windows.Forms.Label labatimentos;
        private System.Windows.Forms.Button bfecharcaixa;
        private System.Windows.Forms.Label ldiferenca;
        private System.Windows.Forms.TextBox tbvalorcontado;
        private System.Windows.Forms.Label lhistoricocaixas;
        private System.Windows.Forms.DataGridView dgvcaixas;
        private System.Windows.Forms.Label ldebito;
    }
}

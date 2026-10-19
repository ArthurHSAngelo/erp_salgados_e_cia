namespace erp_salgados_e_cia
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.bprodutos = new System.Windows.Forms.Button();
            this.bfornecedores = new System.Windows.Forms.Button();
            this.bunidades = new System.Windows.Forms.Button();
            this.pnltelas = new System.Windows.Forms.Panel();
            this.pnlmenu = new System.Windows.Forms.Panel();
            this.bausuarios = new erp_salgados_e_cia.botaoarredondado();
            this.bacadastros = new erp_salgados_e_cia.botaoarredondado();
            this.barelatorios = new erp_salgados_e_cia.botaoarredondado();
            this.baestoque = new erp_salgados_e_cia.botaoarredondado();
            this.bacaixa = new erp_salgados_e_cia.botaoarredondado();
            this.bavendas = new erp_salgados_e_cia.botaoarredondado();
            this.bapainel = new erp_salgados_e_cia.botaoarredondado();
            this.ltitulo = new System.Windows.Forms.Label();
            this.bmovimentoestoque = new System.Windows.Forms.Button();
            this.pnlmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bprodutos
            // 
            this.bprodutos.Location = new System.Drawing.Point(12, 601);
            this.bprodutos.Name = "bprodutos";
            this.bprodutos.Size = new System.Drawing.Size(113, 23);
            this.bprodutos.TabIndex = 0;
            this.bprodutos.Text = "Produtos";
            this.bprodutos.UseVisualStyleBackColor = true;
            this.bprodutos.Click += new System.EventHandler(this.bprodutos_Click);
            // 
            // bfornecedores
            // 
            this.bfornecedores.Location = new System.Drawing.Point(12, 655);
            this.bfornecedores.Name = "bfornecedores";
            this.bfornecedores.Size = new System.Drawing.Size(113, 23);
            this.bfornecedores.TabIndex = 1;
            this.bfornecedores.Text = "Fornecedores";
            this.bfornecedores.UseVisualStyleBackColor = true;
            this.bfornecedores.Click += new System.EventHandler(this.bfornecedores_Click);
            // 
            // bunidades
            // 
            this.bunidades.Location = new System.Drawing.Point(12, 708);
            this.bunidades.Name = "bunidades";
            this.bunidades.Size = new System.Drawing.Size(113, 23);
            this.bunidades.TabIndex = 2;
            this.bunidades.Text = "Unidades";
            this.bunidades.UseVisualStyleBackColor = true;
            this.bunidades.Click += new System.EventHandler(this.bunidades_Click);
            // 
            // pnltelas
            // 
            this.pnltelas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltelas.Location = new System.Drawing.Point(290, 0);
            this.pnltelas.Name = "pnltelas";
            this.pnltelas.Size = new System.Drawing.Size(2125, 1119);
            this.pnltelas.TabIndex = 3;
            // 
            // pnlmenu
            // 
            this.pnlmenu.BackColor = System.Drawing.Color.DarkOrange;
            this.pnlmenu.Controls.Add(this.bausuarios);
            this.pnlmenu.Controls.Add(this.bacadastros);
            this.pnlmenu.Controls.Add(this.barelatorios);
            this.pnlmenu.Controls.Add(this.baestoque);
            this.pnlmenu.Controls.Add(this.bacaixa);
            this.pnlmenu.Controls.Add(this.bavendas);
            this.pnlmenu.Controls.Add(this.bapainel);
            this.pnlmenu.Controls.Add(this.ltitulo);
            this.pnlmenu.Controls.Add(this.bmovimentoestoque);
            this.pnlmenu.Controls.Add(this.bprodutos);
            this.pnlmenu.Controls.Add(this.bfornecedores);
            this.pnlmenu.Controls.Add(this.bunidades);
            this.pnlmenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlmenu.Location = new System.Drawing.Point(0, 0);
            this.pnlmenu.Name = "pnlmenu";
            this.pnlmenu.Size = new System.Drawing.Size(290, 1119);
            this.pnlmenu.TabIndex = 4;
            // 
            // bausuarios
            // 
            this.bausuarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bausuarios.BackColor = System.Drawing.Color.DarkOrange;
            this.bausuarios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bausuarios.FlatAppearance.BorderSize = 0;
            this.bausuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bausuarios.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.bausuarios.ForeColor = System.Drawing.Color.White;
            this.bausuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bausuarios.Location = new System.Drawing.Point(20, 415);
            this.bausuarios.Name = "bausuarios";
            this.bausuarios.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.bausuarios.raio = 25;
            this.bausuarios.Size = new System.Drawing.Size(250, 45);
            this.bausuarios.TabIndex = 17;
            this.bausuarios.Text = "Usuários";
            this.bausuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bausuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bausuarios.UseVisualStyleBackColor = false;
            this.bausuarios.Click += new System.EventHandler(this.bausuarios_Click);
            // 
            // bacadastros
            // 
            this.bacadastros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bacadastros.BackColor = System.Drawing.Color.DarkOrange;
            this.bacadastros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bacadastros.FlatAppearance.BorderSize = 0;
            this.bacadastros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bacadastros.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.bacadastros.ForeColor = System.Drawing.Color.White;
            this.bacadastros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bacadastros.Location = new System.Drawing.Point(20, 305);
            this.bacadastros.Name = "bacadastros";
            this.bacadastros.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.bacadastros.raio = 25;
            this.bacadastros.Size = new System.Drawing.Size(250, 45);
            this.bacadastros.TabIndex = 16;
            this.bacadastros.Text = "Cadastros";
            this.bacadastros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bacadastros.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bacadastros.UseVisualStyleBackColor = false;
            this.bacadastros.Click += new System.EventHandler(this.bacadastros_Click);
            // 
            // barelatorios
            // 
            this.barelatorios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barelatorios.BackColor = System.Drawing.Color.DarkOrange;
            this.barelatorios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.barelatorios.FlatAppearance.BorderSize = 0;
            this.barelatorios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.barelatorios.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.barelatorios.ForeColor = System.Drawing.Color.White;
            this.barelatorios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.barelatorios.Location = new System.Drawing.Point(20, 360);
            this.barelatorios.Name = "barelatorios";
            this.barelatorios.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.barelatorios.raio = 25;
            this.barelatorios.Size = new System.Drawing.Size(250, 45);
            this.barelatorios.TabIndex = 15;
            this.barelatorios.Text = "Relatórios";
            this.barelatorios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.barelatorios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.barelatorios.UseVisualStyleBackColor = false;
            this.barelatorios.Click += new System.EventHandler(this.barelatorios_Click);
            // 
            // baestoque
            // 
            this.baestoque.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.baestoque.BackColor = System.Drawing.Color.DarkOrange;
            this.baestoque.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.baestoque.FlatAppearance.BorderSize = 0;
            this.baestoque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.baestoque.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.baestoque.ForeColor = System.Drawing.Color.White;
            this.baestoque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.baestoque.Location = new System.Drawing.Point(20, 250);
            this.baestoque.Name = "baestoque";
            this.baestoque.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.baestoque.raio = 25;
            this.baestoque.Size = new System.Drawing.Size(250, 45);
            this.baestoque.TabIndex = 14;
            this.baestoque.Text = "Estoque";
            this.baestoque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.baestoque.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.baestoque.UseVisualStyleBackColor = false;
            this.baestoque.Click += new System.EventHandler(this.baestoque_Click);
            // 
            // bacaixa
            // 
            this.bacaixa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bacaixa.BackColor = System.Drawing.Color.DarkOrange;
            this.bacaixa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bacaixa.FlatAppearance.BorderSize = 0;
            this.bacaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bacaixa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.bacaixa.ForeColor = System.Drawing.Color.White;
            this.bacaixa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bacaixa.Location = new System.Drawing.Point(20, 195);
            this.bacaixa.Name = "bacaixa";
            this.bacaixa.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.bacaixa.raio = 25;
            this.bacaixa.Size = new System.Drawing.Size(250, 45);
            this.bacaixa.TabIndex = 13;
            this.bacaixa.Text = "Caixa";
            this.bacaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bacaixa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bacaixa.UseVisualStyleBackColor = false;
            this.bacaixa.Click += new System.EventHandler(this.bacaixa_Click);
            // 
            // bavendas
            // 
            this.bavendas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bavendas.BackColor = System.Drawing.Color.DarkOrange;
            this.bavendas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bavendas.FlatAppearance.BorderSize = 0;
            this.bavendas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bavendas.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.bavendas.ForeColor = System.Drawing.Color.White;
            this.bavendas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bavendas.Location = new System.Drawing.Point(20, 140);
            this.bavendas.Name = "bavendas";
            this.bavendas.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.bavendas.raio = 25;
            this.bavendas.Size = new System.Drawing.Size(250, 45);
            this.bavendas.TabIndex = 12;
            this.bavendas.Text = "Vendas";
            this.bavendas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bavendas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bavendas.UseVisualStyleBackColor = false;
            this.bavendas.Click += new System.EventHandler(this.botaoarredondado2_Click);
            // 
            // bapainel
            // 
            this.bapainel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bapainel.BackColor = System.Drawing.Color.White;
            this.bapainel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bapainel.FlatAppearance.BorderSize = 0;
            this.bapainel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bapainel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.bapainel.ForeColor = System.Drawing.Color.DarkOrange;
            this.bapainel.Image = ((System.Drawing.Image)(resources.GetObject("bapainel.Image")));
            this.bapainel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bapainel.Location = new System.Drawing.Point(20, 85);
            this.bapainel.Name = "bapainel";
            this.bapainel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.bapainel.raio = 25;
            this.bapainel.Size = new System.Drawing.Size(250, 45);
            this.bapainel.TabIndex = 11;
            this.bapainel.Text = "Painel";
            this.bapainel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bapainel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bapainel.UseVisualStyleBackColor = false;
            this.bapainel.Click += new System.EventHandler(this.bapainel_Click);
            // 
            // ltitulo
            // 
            this.ltitulo.AutoSize = true;
            this.ltitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltitulo.ForeColor = System.Drawing.Color.White;
            this.ltitulo.Location = new System.Drawing.Point(20, 30);
            this.ltitulo.Name = "ltitulo";
            this.ltitulo.Size = new System.Drawing.Size(196, 25);
            this.ltitulo.TabIndex = 0;
            this.ltitulo.Text = "ERP Salgados & Cia";
            this.ltitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bmovimentoestoque
            // 
            this.bmovimentoestoque.Location = new System.Drawing.Point(12, 752);
            this.bmovimentoestoque.Name = "bmovimentoestoque";
            this.bmovimentoestoque.Size = new System.Drawing.Size(113, 49);
            this.bmovimentoestoque.TabIndex = 3;
            this.bmovimentoestoque.Text = "Entrada de estoque";
            this.bmovimentoestoque.UseVisualStyleBackColor = true;
            this.bmovimentoestoque.Click += new System.EventHandler(this.bmovimentoestoque_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2415, 1119);
            this.Controls.Add(this.pnltelas);
            this.Controls.Add(this.pnlmenu);
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlmenu.ResumeLayout(false);
            this.pnlmenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bprodutos;
        private System.Windows.Forms.Button bfornecedores;
        private System.Windows.Forms.Button bunidades;
        private System.Windows.Forms.Panel pnltelas;
        private System.Windows.Forms.Panel pnlmenu;
        private System.Windows.Forms.Button bmovimentoestoque;
        private System.Windows.Forms.Label ltitulo;
        private botaoarredondado bapainel;
        private botaoarredondado bacadastros;
        private botaoarredondado barelatorios;
        private botaoarredondado baestoque;
        private botaoarredondado bacaixa;
        private botaoarredondado bavendas;
        private botaoarredondado bausuarios;
    }
}
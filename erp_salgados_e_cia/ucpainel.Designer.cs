namespace erp_salgados_e_cia
{
    partial class ucpainel
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
            this.ltitulopainel = new System.Windows.Forms.Label();
            this.ppaineltopo = new System.Windows.Forms.Panel();
            this.lunidadepainel = new System.Windows.Forms.Label();
            this.latualizacaopainel = new System.Windows.Forms.Label();
            this.tlppainel = new System.Windows.Forms.TableLayoutPanel();
            this.pcartaoatividade = new System.Windows.Forms.Panel();
            this.dgvatividade = new System.Windows.Forms.DataGridView();
            this.llinkatividade = new System.Windows.Forms.LinkLabel();
            this.ltituloatividade = new System.Windows.Forms.Label();
            this.pcartaoultimas = new System.Windows.Forms.Panel();
            this.dgvultimasvendas = new System.Windows.Forms.DataGridView();
            this.llinkultimas = new System.Windows.Forms.LinkLabel();
            this.ltituloultimas = new System.Windows.Forms.Label();
            this.pcartaonotinha = new System.Windows.Forms.Panel();
            this.llinknotinha = new System.Windows.Forms.LinkLabel();
            this.lnotinhaclientes = new System.Windows.Forms.Label();
            this.ltitulonotinha = new System.Windows.Forms.Label();
            this.lnotinhatotal = new System.Windows.Forms.Label();
            this.pcartaoestoque = new System.Windows.Forms.Panel();
            this.dgvcriticos = new System.Windows.Forms.DataGridView();
            this.llinkestoque = new System.Windows.Forms.LinkLabel();
            this.ltituloestoque = new System.Windows.Forms.Label();
            this.ltotalcriticos = new System.Windows.Forms.Label();
            this.pcartaovendas = new System.Windows.Forms.Panel();
            this.llinkvendas = new System.Windows.Forms.LinkLabel();
            this.lnotinhahoje = new System.Windows.Forms.Label();
            this.lvendashoje = new System.Windows.Forms.Label();
            this.lfaturamentohoje = new System.Windows.Forms.Label();
            this.lticketmediohoje = new System.Windows.Forms.Label();
            this.ltitulovendas = new System.Windows.Forms.Label();
            this.pcartaocaixa = new System.Windows.Forms.Panel();
            this.laberturacaixa = new System.Windows.Forms.Label();
            this.llinkcaixa = new System.Windows.Forms.LinkLabel();
            this.lesperadocaixa = new System.Windows.Forms.Label();
            this.lstatuscaixa = new System.Windows.Forms.Label();
            this.lresponsavelcaixa = new System.Windows.Forms.Label();
            this.linicialcaixa = new System.Windows.Forms.Label();
            this.ltitulocaixa = new System.Windows.Forms.Label();
            this.ppaineltopo.SuspendLayout();
            this.tlppainel.SuspendLayout();
            this.pcartaoatividade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvatividade)).BeginInit();
            this.pcartaoultimas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvultimasvendas)).BeginInit();
            this.pcartaonotinha.SuspendLayout();
            this.pcartaoestoque.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcriticos)).BeginInit();
            this.pcartaovendas.SuspendLayout();
            this.pcartaocaixa.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltitulopainel
            // 
            this.ltitulopainel.AutoSize = true;
            this.ltitulopainel.Location = new System.Drawing.Point(15, 10);
            this.ltitulopainel.Name = "ltitulopainel";
            this.ltitulopainel.Size = new System.Drawing.Size(45, 16);
            this.ltitulopainel.TabIndex = 11;
            this.ltitulopainel.Text = "Painel";
            // 
            // ppaineltopo
            // 
            this.ppaineltopo.Controls.Add(this.lunidadepainel);
            this.ppaineltopo.Controls.Add(this.latualizacaopainel);
            this.ppaineltopo.Controls.Add(this.ltitulopainel);
            this.ppaineltopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ppaineltopo.Location = new System.Drawing.Point(0, 0);
            this.ppaineltopo.Name = "ppaineltopo";
            this.ppaineltopo.Size = new System.Drawing.Size(2445, 195);
            this.ppaineltopo.TabIndex = 12;
            // 
            // lunidadepainel
            // 
            this.lunidadepainel.AutoSize = true;
            this.lunidadepainel.Location = new System.Drawing.Point(5, 42);
            this.lunidadepainel.Name = "lunidadepainel";
            this.lunidadepainel.Size = new System.Drawing.Size(69, 16);
            this.lunidadepainel.TabIndex = 13;
            this.lunidadepainel.Text = "Unidade: -";
            // 
            // latualizacaopainel
            // 
            this.latualizacaopainel.AutoSize = true;
            this.latualizacaopainel.Location = new System.Drawing.Point(300, 42);
            this.latualizacaopainel.Name = "latualizacaopainel";
            this.latualizacaopainel.Size = new System.Drawing.Size(121, 16);
            this.latualizacaopainel.TabIndex = 12;
            this.latualizacaopainel.Text = "Atualizado às --:--:--";
            // 
            // tlppainel
            // 
            this.tlppainel.ColumnCount = 3;
            this.tlppainel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlppainel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlppainel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tlppainel.Controls.Add(this.pcartaoatividade, 2, 1);
            this.tlppainel.Controls.Add(this.pcartaoultimas, 1, 1);
            this.tlppainel.Controls.Add(this.pcartaonotinha, 0, 1);
            this.tlppainel.Controls.Add(this.pcartaoestoque, 2, 0);
            this.tlppainel.Controls.Add(this.pcartaovendas, 1, 0);
            this.tlppainel.Controls.Add(this.pcartaocaixa, 0, 0);
            this.tlppainel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlppainel.Location = new System.Drawing.Point(0, 195);
            this.tlppainel.Name = "tlppainel";
            this.tlppainel.Padding = new System.Windows.Forms.Padding(8);
            this.tlppainel.RowCount = 2;
            this.tlppainel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlppainel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlppainel.Size = new System.Drawing.Size(2445, 1137);
            this.tlppainel.TabIndex = 13;
            // 
            // pcartaoatividade
            // 
            this.pcartaoatividade.Controls.Add(this.dgvatividade);
            this.pcartaoatividade.Controls.Add(this.llinkatividade);
            this.pcartaoatividade.Controls.Add(this.ltituloatividade);
            this.pcartaoatividade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcartaoatividade.Location = new System.Drawing.Point(1613, 571);
            this.pcartaoatividade.Name = "pcartaoatividade";
            this.pcartaoatividade.Size = new System.Drawing.Size(821, 555);
            this.pcartaoatividade.TabIndex = 5;
            // 
            // dgvatividade
            // 
            this.dgvatividade.AllowUserToAddRows = false;
            this.dgvatividade.AllowUserToDeleteRows = false;
            this.dgvatividade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvatividade.Location = new System.Drawing.Point(192, 147);
            this.dgvatividade.MultiSelect = false;
            this.dgvatividade.Name = "dgvatividade";
            this.dgvatividade.ReadOnly = true;
            this.dgvatividade.RowHeadersWidth = 51;
            this.dgvatividade.RowTemplate.Height = 24;
            this.dgvatividade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvatividade.Size = new System.Drawing.Size(416, 138);
            this.dgvatividade.TabIndex = 34;
            // 
            // llinkatividade
            // 
            this.llinkatividade.AutoSize = true;
            this.llinkatividade.Location = new System.Drawing.Point(189, 323);
            this.llinkatividade.Name = "llinkatividade";
            this.llinkatividade.Size = new System.Drawing.Size(50, 16);
            this.llinkatividade.TabIndex = 33;
            this.llinkatividade.TabStop = true;
            this.llinkatividade.Text = "Ver log";
            // 
            // ltituloatividade
            // 
            this.ltituloatividade.AutoSize = true;
            this.ltituloatividade.Location = new System.Drawing.Point(189, 114);
            this.ltituloatividade.Name = "ltituloatividade";
            this.ltituloatividade.Size = new System.Drawing.Size(112, 16);
            this.ltituloatividade.TabIndex = 32;
            this.ltituloatividade.Text = "Atividade recente";
            // 
            // pcartaoultimas
            // 
            this.pcartaoultimas.Controls.Add(this.dgvultimasvendas);
            this.pcartaoultimas.Controls.Add(this.llinkultimas);
            this.pcartaoultimas.Controls.Add(this.ltituloultimas);
            this.pcartaoultimas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcartaoultimas.Location = new System.Drawing.Point(812, 571);
            this.pcartaoultimas.Name = "pcartaoultimas";
            this.pcartaoultimas.Size = new System.Drawing.Size(795, 555);
            this.pcartaoultimas.TabIndex = 4;
            // 
            // dgvultimasvendas
            // 
            this.dgvultimasvendas.AllowUserToAddRows = false;
            this.dgvultimasvendas.AllowUserToDeleteRows = false;
            this.dgvultimasvendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvultimasvendas.Location = new System.Drawing.Point(175, 147);
            this.dgvultimasvendas.MultiSelect = false;
            this.dgvultimasvendas.Name = "dgvultimasvendas";
            this.dgvultimasvendas.ReadOnly = true;
            this.dgvultimasvendas.RowHeadersWidth = 51;
            this.dgvultimasvendas.RowTemplate.Height = 24;
            this.dgvultimasvendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvultimasvendas.Size = new System.Drawing.Size(416, 138);
            this.dgvultimasvendas.TabIndex = 31;
            // 
            // llinkultimas
            // 
            this.llinkultimas.AutoSize = true;
            this.llinkultimas.Location = new System.Drawing.Point(172, 323);
            this.llinkultimas.Name = "llinkultimas";
            this.llinkultimas.Size = new System.Drawing.Size(81, 16);
            this.llinkultimas.TabIndex = 30;
            this.llinkultimas.TabStop = true;
            this.llinkultimas.Text = "Ver histórico";
            // 
            // ltituloultimas
            // 
            this.ltituloultimas.AutoSize = true;
            this.ltituloultimas.Location = new System.Drawing.Point(172, 114);
            this.ltituloultimas.Name = "ltituloultimas";
            this.ltituloultimas.Size = new System.Drawing.Size(100, 16);
            this.ltituloultimas.TabIndex = 29;
            this.ltituloultimas.Text = "Últimas vendas";
            // 
            // pcartaonotinha
            // 
            this.pcartaonotinha.Controls.Add(this.llinknotinha);
            this.pcartaonotinha.Controls.Add(this.lnotinhaclientes);
            this.pcartaonotinha.Controls.Add(this.ltitulonotinha);
            this.pcartaonotinha.Controls.Add(this.lnotinhatotal);
            this.pcartaonotinha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcartaonotinha.Location = new System.Drawing.Point(11, 571);
            this.pcartaonotinha.Name = "pcartaonotinha";
            this.pcartaonotinha.Size = new System.Drawing.Size(795, 555);
            this.pcartaonotinha.TabIndex = 3;
            // 
            // llinknotinha
            // 
            this.llinknotinha.AutoSize = true;
            this.llinknotinha.Location = new System.Drawing.Point(47, 176);
            this.llinknotinha.Name = "llinknotinha";
            this.llinknotinha.Size = new System.Drawing.Size(77, 16);
            this.llinknotinha.TabIndex = 23;
            this.llinknotinha.TabStop = true;
            this.llinknotinha.Text = "Ver clientes";
            // 
            // lnotinhaclientes
            // 
            this.lnotinhaclientes.AutoSize = true;
            this.lnotinhaclientes.Location = new System.Drawing.Point(47, 133);
            this.lnotinhaclientes.Name = "lnotinhaclientes";
            this.lnotinhaclientes.Size = new System.Drawing.Size(137, 16);
            this.lnotinhaclientes.TabIndex = 22;
            this.lnotinhaclientes.Text = "Clientes devedores: 0";
            // 
            // ltitulonotinha
            // 
            this.ltitulonotinha.AutoSize = true;
            this.ltitulonotinha.Location = new System.Drawing.Point(47, 47);
            this.ltitulonotinha.Name = "ltitulonotinha";
            this.ltitulonotinha.Size = new System.Drawing.Size(156, 16);
            this.ltitulonotinha.TabIndex = 21;
            this.ltitulonotinha.Text = "Notinha em aberto (rede)";
            // 
            // lnotinhatotal
            // 
            this.lnotinhatotal.AutoSize = true;
            this.lnotinhatotal.Location = new System.Drawing.Point(47, 89);
            this.lnotinhatotal.Name = "lnotinhatotal";
            this.lnotinhatotal.Size = new System.Drawing.Size(133, 16);
            this.lnotinhatotal.TabIndex = 20;
            this.lnotinhatotal.Text = "Total devido: R$ 0,00";
            // 
            // pcartaoestoque
            // 
            this.pcartaoestoque.Controls.Add(this.dgvcriticos);
            this.pcartaoestoque.Controls.Add(this.llinkestoque);
            this.pcartaoestoque.Controls.Add(this.ltituloestoque);
            this.pcartaoestoque.Controls.Add(this.ltotalcriticos);
            this.pcartaoestoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcartaoestoque.Location = new System.Drawing.Point(1613, 11);
            this.pcartaoestoque.Name = "pcartaoestoque";
            this.pcartaoestoque.Size = new System.Drawing.Size(821, 554);
            this.pcartaoestoque.TabIndex = 2;
            // 
            // dgvcriticos
            // 
            this.dgvcriticos.AllowUserToAddRows = false;
            this.dgvcriticos.AllowUserToDeleteRows = false;
            this.dgvcriticos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvcriticos.Location = new System.Drawing.Point(166, 156);
            this.dgvcriticos.MultiSelect = false;
            this.dgvcriticos.Name = "dgvcriticos";
            this.dgvcriticos.ReadOnly = true;
            this.dgvcriticos.RowHeadersWidth = 51;
            this.dgvcriticos.RowTemplate.Height = 24;
            this.dgvcriticos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvcriticos.Size = new System.Drawing.Size(416, 138);
            this.dgvcriticos.TabIndex = 28;
            // 
            // llinkestoque
            // 
            this.llinkestoque.AutoSize = true;
            this.llinkestoque.Location = new System.Drawing.Point(163, 332);
            this.llinkestoque.Name = "llinkestoque";
            this.llinkestoque.Size = new System.Drawing.Size(80, 16);
            this.llinkestoque.TabIndex = 26;
            this.llinkestoque.TabStop = true;
            this.llinkestoque.Text = "Ver estoque";
            // 
            // ltituloestoque
            // 
            this.ltituloestoque.AutoSize = true;
            this.ltituloestoque.Location = new System.Drawing.Point(163, 85);
            this.ltituloestoque.Name = "ltituloestoque";
            this.ltituloestoque.Size = new System.Drawing.Size(134, 16);
            this.ltituloestoque.TabIndex = 22;
            this.ltituloestoque.Text = "Estoque crítico (rede)";
            // 
            // ltotalcriticos
            // 
            this.ltotalcriticos.AutoSize = true;
            this.ltotalcriticos.Location = new System.Drawing.Point(163, 123);
            this.ltotalcriticos.Name = "ltotalcriticos";
            this.ltotalcriticos.Size = new System.Drawing.Size(130, 16);
            this.ltotalcriticos.TabIndex = 21;
            this.ltotalcriticos.Text = "Em estoque crítico: 0";
            // 
            // pcartaovendas
            // 
            this.pcartaovendas.Controls.Add(this.llinkvendas);
            this.pcartaovendas.Controls.Add(this.lnotinhahoje);
            this.pcartaovendas.Controls.Add(this.lvendashoje);
            this.pcartaovendas.Controls.Add(this.lfaturamentohoje);
            this.pcartaovendas.Controls.Add(this.lticketmediohoje);
            this.pcartaovendas.Controls.Add(this.ltitulovendas);
            this.pcartaovendas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcartaovendas.Location = new System.Drawing.Point(812, 11);
            this.pcartaovendas.Name = "pcartaovendas";
            this.pcartaovendas.Size = new System.Drawing.Size(795, 554);
            this.pcartaovendas.TabIndex = 1;
            // 
            // llinkvendas
            // 
            this.llinkvendas.AutoSize = true;
            this.llinkvendas.Location = new System.Drawing.Point(82, 263);
            this.llinkvendas.Name = "llinkvendas";
            this.llinkvendas.Size = new System.Drawing.Size(76, 16);
            this.llinkvendas.TabIndex = 25;
            this.llinkvendas.TabStop = true;
            this.llinkvendas.Text = "Ver vendas";
            // 
            // lnotinhahoje
            // 
            this.lnotinhahoje.AutoSize = true;
            this.lnotinhahoje.Location = new System.Drawing.Point(82, 220);
            this.lnotinhahoje.Name = "lnotinhahoje";
            this.lnotinhahoje.Size = new System.Drawing.Size(126, 16);
            this.lnotinhahoje.TabIndex = 24;
            this.lnotinhahoje.Text = "Em Notinha: R$ 0,00";
            // 
            // lvendashoje
            // 
            this.lvendashoje.AutoSize = true;
            this.lvendashoje.Location = new System.Drawing.Point(82, 85);
            this.lvendashoje.Name = "lvendashoje";
            this.lvendashoje.Size = new System.Drawing.Size(67, 16);
            this.lvendashoje.TabIndex = 23;
            this.lvendashoje.Text = "Vendas: 0";
            // 
            // lfaturamentohoje
            // 
            this.lfaturamentohoje.AutoSize = true;
            this.lfaturamentohoje.Location = new System.Drawing.Point(82, 134);
            this.lfaturamentohoje.Name = "lfaturamentohoje";
            this.lfaturamentohoje.Size = new System.Drawing.Size(132, 16);
            this.lfaturamentohoje.TabIndex = 22;
            this.lfaturamentohoje.Text = "Faturamento: R$ 0,00";
            // 
            // lticketmediohoje
            // 
            this.lticketmediohoje.AutoSize = true;
            this.lticketmediohoje.Location = new System.Drawing.Point(82, 176);
            this.lticketmediohoje.Name = "lticketmediohoje";
            this.lticketmediohoje.Size = new System.Drawing.Size(135, 16);
            this.lticketmediohoje.TabIndex = 21;
            this.lticketmediohoje.Text = "Ticket médio: R$ 0,00";
            // 
            // ltitulovendas
            // 
            this.ltitulovendas.AutoSize = true;
            this.ltitulovendas.Location = new System.Drawing.Point(82, 43);
            this.ltitulovendas.Name = "ltitulovendas";
            this.ltitulovendas.Size = new System.Drawing.Size(102, 16);
            this.ltitulovendas.TabIndex = 20;
            this.ltitulovendas.Text = "Vendas de hoje";
            // 
            // pcartaocaixa
            // 
            this.pcartaocaixa.Controls.Add(this.laberturacaixa);
            this.pcartaocaixa.Controls.Add(this.llinkcaixa);
            this.pcartaocaixa.Controls.Add(this.lesperadocaixa);
            this.pcartaocaixa.Controls.Add(this.lstatuscaixa);
            this.pcartaocaixa.Controls.Add(this.lresponsavelcaixa);
            this.pcartaocaixa.Controls.Add(this.linicialcaixa);
            this.pcartaocaixa.Controls.Add(this.ltitulocaixa);
            this.pcartaocaixa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcartaocaixa.Location = new System.Drawing.Point(11, 11);
            this.pcartaocaixa.Name = "pcartaocaixa";
            this.pcartaocaixa.Size = new System.Drawing.Size(795, 554);
            this.pcartaocaixa.TabIndex = 0;
            // 
            // laberturacaixa
            // 
            this.laberturacaixa.AutoSize = true;
            this.laberturacaixa.Location = new System.Drawing.Point(47, 144);
            this.laberturacaixa.Name = "laberturacaixa";
            this.laberturacaixa.Size = new System.Drawing.Size(79, 16);
            this.laberturacaixa.TabIndex = 20;
            this.laberturacaixa.Text = "Aberto em: -";
            // 
            // llinkcaixa
            // 
            this.llinkcaixa.AutoSize = true;
            this.llinkcaixa.Location = new System.Drawing.Point(47, 278);
            this.llinkcaixa.Name = "llinkcaixa";
            this.llinkcaixa.Size = new System.Drawing.Size(93, 16);
            this.llinkcaixa.TabIndex = 19;
            this.llinkcaixa.TabStop = true;
            this.llinkcaixa.Text = "Ir para o Caixa";
            // 
            // lesperadocaixa
            // 
            this.lesperadocaixa.AutoSize = true;
            this.lesperadocaixa.Location = new System.Drawing.Point(47, 220);
            this.lesperadocaixa.Name = "lesperadocaixa";
            this.lesperadocaixa.Size = new System.Drawing.Size(169, 16);
            this.lesperadocaixa.TabIndex = 18;
            this.lesperadocaixa.Text = "Dinheiro esperado: R$ 0,00";
            // 
            // lstatuscaixa
            // 
            this.lstatuscaixa.AutoSize = true;
            this.lstatuscaixa.Location = new System.Drawing.Point(47, 65);
            this.lstatuscaixa.Name = "lstatuscaixa";
            this.lstatuscaixa.Size = new System.Drawing.Size(104, 16);
            this.lstatuscaixa.TabIndex = 17;
            this.lstatuscaixa.Text = "Não aberto hoje";
            // 
            // lresponsavelcaixa
            // 
            this.lresponsavelcaixa.AutoSize = true;
            this.lresponsavelcaixa.Location = new System.Drawing.Point(47, 108);
            this.lresponsavelcaixa.Name = "lresponsavelcaixa";
            this.lresponsavelcaixa.Size = new System.Drawing.Size(98, 16);
            this.lresponsavelcaixa.TabIndex = 16;
            this.lresponsavelcaixa.Text = "Responsável: -";
            // 
            // linicialcaixa
            // 
            this.linicialcaixa.AutoSize = true;
            this.linicialcaixa.Location = new System.Drawing.Point(47, 176);
            this.linicialcaixa.Name = "linicialcaixa";
            this.linicialcaixa.Size = new System.Drawing.Size(126, 16);
            this.linicialcaixa.TabIndex = 15;
            this.linicialcaixa.Text = "Valor inicial: R$ 0,00";
            // 
            // ltitulocaixa
            // 
            this.ltitulocaixa.AutoSize = true;
            this.ltitulocaixa.Location = new System.Drawing.Point(47, 23);
            this.ltitulocaixa.Name = "ltitulocaixa";
            this.ltitulocaixa.Size = new System.Drawing.Size(89, 16);
            this.ltitulocaixa.TabIndex = 14;
            this.ltitulocaixa.Text = "Caixa de hoje";
            // 
            // ucpainel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlppainel);
            this.Controls.Add(this.ppaineltopo);
            this.Name = "ucpainel";
            this.Size = new System.Drawing.Size(2445, 1332);
            this.ppaineltopo.ResumeLayout(false);
            this.ppaineltopo.PerformLayout();
            this.tlppainel.ResumeLayout(false);
            this.pcartaoatividade.ResumeLayout(false);
            this.pcartaoatividade.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvatividade)).EndInit();
            this.pcartaoultimas.ResumeLayout(false);
            this.pcartaoultimas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvultimasvendas)).EndInit();
            this.pcartaonotinha.ResumeLayout(false);
            this.pcartaonotinha.PerformLayout();
            this.pcartaoestoque.ResumeLayout(false);
            this.pcartaoestoque.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcriticos)).EndInit();
            this.pcartaovendas.ResumeLayout(false);
            this.pcartaovendas.PerformLayout();
            this.pcartaocaixa.ResumeLayout(false);
            this.pcartaocaixa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ltitulopainel;
        private System.Windows.Forms.Panel ppaineltopo;
        private System.Windows.Forms.Label lunidadepainel;
        private System.Windows.Forms.Label latualizacaopainel;
        private System.Windows.Forms.TableLayoutPanel tlppainel;
        private System.Windows.Forms.Panel pcartaoatividade;
        private System.Windows.Forms.Panel pcartaoultimas;
        private System.Windows.Forms.Panel pcartaonotinha;
        private System.Windows.Forms.Panel pcartaoestoque;
        private System.Windows.Forms.Panel pcartaovendas;
        private System.Windows.Forms.Panel pcartaocaixa;
        private System.Windows.Forms.LinkLabel llinkcaixa;
        private System.Windows.Forms.Label lesperadocaixa;
        private System.Windows.Forms.Label lstatuscaixa;
        private System.Windows.Forms.Label lresponsavelcaixa;
        private System.Windows.Forms.Label linicialcaixa;
        private System.Windows.Forms.Label ltitulocaixa;
        private System.Windows.Forms.LinkLabel llinkestoque;
        private System.Windows.Forms.Label ltituloestoque;
        private System.Windows.Forms.Label ltotalcriticos;
        private System.Windows.Forms.LinkLabel llinkvendas;
        private System.Windows.Forms.Label lnotinhahoje;
        private System.Windows.Forms.Label lvendashoje;
        private System.Windows.Forms.Label lfaturamentohoje;
        private System.Windows.Forms.Label lticketmediohoje;
        private System.Windows.Forms.Label ltitulovendas;
        private System.Windows.Forms.DataGridView dgvatividade;
        private System.Windows.Forms.LinkLabel llinkatividade;
        private System.Windows.Forms.Label ltituloatividade;
        private System.Windows.Forms.DataGridView dgvultimasvendas;
        private System.Windows.Forms.LinkLabel llinkultimas;
        private System.Windows.Forms.Label ltituloultimas;
        private System.Windows.Forms.LinkLabel llinknotinha;
        private System.Windows.Forms.Label lnotinhaclientes;
        private System.Windows.Forms.Label ltitulonotinha;
        private System.Windows.Forms.Label lnotinhatotal;
        private System.Windows.Forms.DataGridView dgvcriticos;
        private System.Windows.Forms.Label laberturacaixa;
    }
}

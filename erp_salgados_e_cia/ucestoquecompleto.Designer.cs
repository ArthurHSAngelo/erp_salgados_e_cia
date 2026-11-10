namespace erp_salgados_e_cia
{
    partial class ucestoquecompleto
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
            this.lfiltroproduto = new System.Windows.Forms.Label();
            this.lfiltrocategoria = new System.Windows.Forms.Label();
            this.tbfiltroproduto = new System.Windows.Forms.TextBox();
            this.cbfiltrocategoria = new System.Windows.Forms.ComboBox();
            this.lfiltrostatus = new System.Windows.Forms.Label();
            this.cbfiltrostatus = new System.Windows.Forms.ComboBox();
            this.cbfiltrocriticos = new System.Windows.Forms.CheckBox();
            this.bexportarestoque = new System.Windows.Forms.Button();
            this.dgvestoquecompleto = new System.Windows.Forms.DataGridView();
            this.ltotalvalorvenda = new System.Windows.Forms.Label();
            this.ltotalvalorcusto = new System.Windows.Forms.Label();
            this.ltotalcriticos = new System.Windows.Forms.Label();
            this.ltotalprodutos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvestoquecompleto)).BeginInit();
            this.SuspendLayout();
            // 
            // lfiltroproduto
            // 
            this.lfiltroproduto.AutoSize = true;
            this.lfiltroproduto.Location = new System.Drawing.Point(56, 57);
            this.lfiltroproduto.Name = "lfiltroproduto";
            this.lfiltroproduto.Size = new System.Drawing.Size(57, 16);
            this.lfiltroproduto.TabIndex = 0;
            this.lfiltroproduto.Text = "Produto:";
            // 
            // lfiltrocategoria
            // 
            this.lfiltrocategoria.AutoSize = true;
            this.lfiltrocategoria.Location = new System.Drawing.Point(299, 63);
            this.lfiltrocategoria.Name = "lfiltrocategoria";
            this.lfiltrocategoria.Size = new System.Drawing.Size(69, 16);
            this.lfiltrocategoria.TabIndex = 1;
            this.lfiltrocategoria.Text = "Categoria:";
            // 
            // tbfiltroproduto
            // 
            this.tbfiltroproduto.Location = new System.Drawing.Point(119, 57);
            this.tbfiltroproduto.Name = "tbfiltroproduto";
            this.tbfiltroproduto.Size = new System.Drawing.Size(100, 22);
            this.tbfiltroproduto.TabIndex = 2;
            // 
            // cbfiltrocategoria
            // 
            this.cbfiltrocategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrocategoria.FormattingEnabled = true;
            this.cbfiltrocategoria.Location = new System.Drawing.Point(399, 57);
            this.cbfiltrocategoria.Name = "cbfiltrocategoria";
            this.cbfiltrocategoria.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrocategoria.TabIndex = 3;
            // 
            // lfiltrostatus
            // 
            this.lfiltrostatus.AutoSize = true;
            this.lfiltrostatus.Location = new System.Drawing.Point(580, 60);
            this.lfiltrostatus.Name = "lfiltrostatus";
            this.lfiltrostatus.Size = new System.Drawing.Size(47, 16);
            this.lfiltrostatus.TabIndex = 4;
            this.lfiltrostatus.Text = "Status:";
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Items.AddRange(new object[] {
            "Ativos",
            "Inativos",
            "Todos"});
            this.cbfiltrostatus.Location = new System.Drawing.Point(653, 57);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(121, 24);
            this.cbfiltrostatus.TabIndex = 5;
            // 
            // cbfiltrocriticos
            // 
            this.cbfiltrocriticos.AutoSize = true;
            this.cbfiltrocriticos.Location = new System.Drawing.Point(842, 59);
            this.cbfiltrocriticos.Name = "cbfiltrocriticos";
            this.cbfiltrocriticos.Size = new System.Drawing.Size(173, 20);
            this.cbfiltrocriticos.TabIndex = 6;
            this.cbfiltrocriticos.Text = "Somente estoque crítico";
            this.cbfiltrocriticos.UseVisualStyleBackColor = true;
            // 
            // bexportarestoque
            // 
            this.bexportarestoque.Location = new System.Drawing.Point(1050, 57);
            this.bexportarestoque.Name = "bexportarestoque";
            this.bexportarestoque.Size = new System.Drawing.Size(92, 23);
            this.bexportarestoque.TabIndex = 7;
            this.bexportarestoque.Text = "Exportar";
            this.bexportarestoque.UseVisualStyleBackColor = true;
            // 
            // dgvestoquecompleto
            // 
            this.dgvestoquecompleto.AllowUserToAddRows = false;
            this.dgvestoquecompleto.AllowUserToDeleteRows = false;
            this.dgvestoquecompleto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvestoquecompleto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvestoquecompleto.Location = new System.Drawing.Point(45, 141);
            this.dgvestoquecompleto.MultiSelect = false;
            this.dgvestoquecompleto.Name = "dgvestoquecompleto";
            this.dgvestoquecompleto.ReadOnly = true;
            this.dgvestoquecompleto.RowHeadersVisible = false;
            this.dgvestoquecompleto.RowHeadersWidth = 51;
            this.dgvestoquecompleto.RowTemplate.Height = 24;
            this.dgvestoquecompleto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvestoquecompleto.Size = new System.Drawing.Size(1020, 421);
            this.dgvestoquecompleto.TabIndex = 8;
            // 
            // ltotalvalorvenda
            // 
            this.ltotalvalorvenda.AutoSize = true;
            this.ltotalvalorvenda.Location = new System.Drawing.Point(1025, 608);
            this.ltotalvalorvenda.Name = "ltotalvalorvenda";
            this.ltotalvalorvenda.Size = new System.Drawing.Size(198, 16);
            this.ltotalvalorvenda.TabIndex = 9;
            this.ltotalvalorvenda.Text = "Valor a preço de venda: R$ 0,00";
            // 
            // ltotalvalorcusto
            // 
            this.ltotalvalorcusto.AutoSize = true;
            this.ltotalvalorcusto.Location = new System.Drawing.Point(827, 608);
            this.ltotalvalorcusto.Name = "ltotalvalorcusto";
            this.ltotalvalorcusto.Size = new System.Drawing.Size(135, 16);
            this.ltotalvalorcusto.TabIndex = 10;
            this.ltotalvalorcusto.Text = "Valor a custo: R$ 0,00";
            // 
            // ltotalcriticos
            // 
            this.ltotalcriticos.AutoSize = true;
            this.ltotalcriticos.Location = new System.Drawing.Point(635, 608);
            this.ltotalcriticos.Name = "ltotalcriticos";
            this.ltotalcriticos.Size = new System.Drawing.Size(130, 16);
            this.ltotalcriticos.TabIndex = 11;
            this.ltotalcriticos.Text = "Em estoque crítico: 0";
            // 
            // ltotalprodutos
            // 
            this.ltotalprodutos.AutoSize = true;
            this.ltotalprodutos.Location = new System.Drawing.Point(485, 608);
            this.ltotalprodutos.Name = "ltotalprodutos";
            this.ltotalprodutos.Size = new System.Drawing.Size(74, 16);
            this.ltotalprodutos.TabIndex = 12;
            this.ltotalprodutos.Text = "Produtos: 0";
            // 
            // ucestoquecompleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ltotalprodutos);
            this.Controls.Add(this.ltotalcriticos);
            this.Controls.Add(this.ltotalvalorcusto);
            this.Controls.Add(this.ltotalvalorvenda);
            this.Controls.Add(this.dgvestoquecompleto);
            this.Controls.Add(this.bexportarestoque);
            this.Controls.Add(this.cbfiltrocriticos);
            this.Controls.Add(this.cbfiltrostatus);
            this.Controls.Add(this.lfiltrostatus);
            this.Controls.Add(this.cbfiltrocategoria);
            this.Controls.Add(this.tbfiltroproduto);
            this.Controls.Add(this.lfiltrocategoria);
            this.Controls.Add(this.lfiltroproduto);
            this.Name = "ucestoquecompleto";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvestoquecompleto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lfiltroproduto;
        private System.Windows.Forms.Label lfiltrocategoria;
        private System.Windows.Forms.TextBox tbfiltroproduto;
        private System.Windows.Forms.ComboBox cbfiltrocategoria;
        private System.Windows.Forms.Label lfiltrostatus;
        private System.Windows.Forms.ComboBox cbfiltrostatus;
        private System.Windows.Forms.CheckBox cbfiltrocriticos;
        private System.Windows.Forms.Button bexportarestoque;
        private System.Windows.Forms.DataGridView dgvestoquecompleto;
        private System.Windows.Forms.Label ltotalvalorvenda;
        private System.Windows.Forms.Label ltotalvalorcusto;
        private System.Windows.Forms.Label ltotalcriticos;
        private System.Windows.Forms.Label ltotalprodutos;
    }
}

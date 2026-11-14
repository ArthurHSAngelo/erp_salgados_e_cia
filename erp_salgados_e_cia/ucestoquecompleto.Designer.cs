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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.cbfiltrounidade = new System.Windows.Forms.ComboBox();
            this.lfiltrounidade = new System.Windows.Forms.Label();
            this.pestoquecompleto = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvestoquecompleto)).BeginInit();
            this.pestoquecompleto.SuspendLayout();
            this.SuspendLayout();
            // 
            // lfiltroproduto
            // 
            this.lfiltroproduto.AutoSize = true;
            this.lfiltroproduto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lfiltroproduto.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltroproduto.Location = new System.Drawing.Point(107, 64);
            this.lfiltroproduto.Name = "lfiltroproduto";
            this.lfiltroproduto.Size = new System.Drawing.Size(76, 21);
            this.lfiltroproduto.TabIndex = 0;
            this.lfiltroproduto.Text = "Produto:";
            // 
            // lfiltrocategoria
            // 
            this.lfiltrocategoria.AutoSize = true;
            this.lfiltrocategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.lfiltrocategoria.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrocategoria.Location = new System.Drawing.Point(440, 64);
            this.lfiltrocategoria.Name = "lfiltrocategoria";
            this.lfiltrocategoria.Size = new System.Drawing.Size(88, 21);
            this.lfiltrocategoria.TabIndex = 1;
            this.lfiltrocategoria.Text = "Categoria:";
            // 
            // tbfiltroproduto
            // 
            this.tbfiltroproduto.BackColor = System.Drawing.Color.White;
            this.tbfiltroproduto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbfiltroproduto.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tbfiltroproduto.Location = new System.Drawing.Point(111, 89);
            this.tbfiltroproduto.Name = "tbfiltroproduto";
            this.tbfiltroproduto.Size = new System.Drawing.Size(263, 30);
            this.tbfiltroproduto.TabIndex = 2;
            // 
            // cbfiltrocategoria
            // 
            this.cbfiltrocategoria.BackColor = System.Drawing.Color.White;
            this.cbfiltrocategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrocategoria.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrocategoria.FormattingEnabled = true;
            this.cbfiltrocategoria.Location = new System.Drawing.Point(444, 89);
            this.cbfiltrocategoria.Name = "cbfiltrocategoria";
            this.cbfiltrocategoria.Size = new System.Drawing.Size(183, 31);
            this.cbfiltrocategoria.TabIndex = 3;
            // 
            // lfiltrostatus
            // 
            this.lfiltrostatus.AutoSize = true;
            this.lfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrostatus.Location = new System.Drawing.Point(693, 64);
            this.lfiltrostatus.Name = "lfiltrostatus";
            this.lfiltrostatus.Size = new System.Drawing.Size(61, 21);
            this.lfiltrostatus.TabIndex = 4;
            this.lfiltrostatus.Text = "Status:";
            // 
            // cbfiltrostatus
            // 
            this.cbfiltrostatus.BackColor = System.Drawing.Color.White;
            this.cbfiltrostatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrostatus.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrostatus.FormattingEnabled = true;
            this.cbfiltrostatus.Items.AddRange(new object[] {
            "Ativos",
            "Inativos",
            "Todos"});
            this.cbfiltrostatus.Location = new System.Drawing.Point(697, 89);
            this.cbfiltrostatus.Name = "cbfiltrostatus";
            this.cbfiltrostatus.Size = new System.Drawing.Size(128, 31);
            this.cbfiltrostatus.TabIndex = 5;
            // 
            // cbfiltrocriticos
            // 
            this.cbfiltrocriticos.AutoSize = true;
            this.cbfiltrocriticos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cbfiltrocriticos.Location = new System.Drawing.Point(1159, 89);
            this.cbfiltrocriticos.Name = "cbfiltrocriticos";
            this.cbfiltrocriticos.Size = new System.Drawing.Size(217, 25);
            this.cbfiltrocriticos.TabIndex = 6;
            this.cbfiltrocriticos.Text = "Somente estoque crítico";
            this.cbfiltrocriticos.UseVisualStyleBackColor = true;
            // 
            // bexportarestoque
            // 
            this.bexportarestoque.FlatAppearance.BorderSize = 2;
            this.bexportarestoque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bexportarestoque.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.bexportarestoque.Location = new System.Drawing.Point(1635, 76);
            this.bexportarestoque.Name = "bexportarestoque";
            this.bexportarestoque.Size = new System.Drawing.Size(172, 52);
            this.bexportarestoque.TabIndex = 7;
            this.bexportarestoque.Text = "Exportar";
            this.bexportarestoque.UseVisualStyleBackColor = true;
            // 
            // dgvestoquecompleto
            // 
            this.dgvestoquecompleto.AllowUserToAddRows = false;
            this.dgvestoquecompleto.AllowUserToDeleteRows = false;
            this.dgvestoquecompleto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvestoquecompleto.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvestoquecompleto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvestoquecompleto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvestoquecompleto.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvestoquecompleto.Location = new System.Drawing.Point(36, 286);
            this.dgvestoquecompleto.MultiSelect = false;
            this.dgvestoquecompleto.Name = "dgvestoquecompleto";
            this.dgvestoquecompleto.ReadOnly = true;
            this.dgvestoquecompleto.RowHeadersVisible = false;
            this.dgvestoquecompleto.RowHeadersWidth = 51;
            this.dgvestoquecompleto.RowTemplate.Height = 24;
            this.dgvestoquecompleto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvestoquecompleto.Size = new System.Drawing.Size(2371, 993);
            this.dgvestoquecompleto.TabIndex = 8;
            // 
            // ltotalvalorvenda
            // 
            this.ltotalvalorvenda.AutoSize = true;
            this.ltotalvalorvenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ltotalvalorvenda.Location = new System.Drawing.Point(753, 246);
            this.ltotalvalorvenda.Name = "ltotalvalorvenda";
            this.ltotalvalorvenda.Size = new System.Drawing.Size(311, 28);
            this.ltotalvalorvenda.TabIndex = 9;
            this.ltotalvalorvenda.Text = "Valor a preço de venda: R$ 0,00";
            // 
            // ltotalvalorcusto
            // 
            this.ltotalvalorcusto.AutoSize = true;
            this.ltotalvalorcusto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ltotalvalorcusto.Location = new System.Drawing.Point(479, 246);
            this.ltotalvalorcusto.Name = "ltotalvalorcusto";
            this.ltotalvalorcusto.Size = new System.Drawing.Size(217, 28);
            this.ltotalvalorcusto.TabIndex = 10;
            this.ltotalvalorcusto.Text = "Valor a custo: R$ 0,00";
            // 
            // ltotalcriticos
            // 
            this.ltotalcriticos.AutoSize = true;
            this.ltotalcriticos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ltotalcriticos.Location = new System.Drawing.Point(210, 246);
            this.ltotalcriticos.Name = "ltotalcriticos";
            this.ltotalcriticos.Size = new System.Drawing.Size(211, 28);
            this.ltotalcriticos.TabIndex = 11;
            this.ltotalcriticos.Text = "Em estoque crítico: 0";
            // 
            // ltotalprodutos
            // 
            this.ltotalprodutos.AutoSize = true;
            this.ltotalprodutos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.ltotalprodutos.Location = new System.Drawing.Point(43, 246);
            this.ltotalprodutos.Name = "ltotalprodutos";
            this.ltotalprodutos.Size = new System.Drawing.Size(120, 28);
            this.ltotalprodutos.TabIndex = 12;
            this.ltotalprodutos.Text = "Produtos: 0";
            // 
            // cbfiltrounidade
            // 
            this.cbfiltrounidade.BackColor = System.Drawing.Color.White;
            this.cbfiltrounidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfiltrounidade.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cbfiltrounidade.FormattingEnabled = true;
            this.cbfiltrounidade.Location = new System.Drawing.Point(891, 89);
            this.cbfiltrounidade.Name = "cbfiltrounidade";
            this.cbfiltrounidade.Size = new System.Drawing.Size(182, 31);
            this.cbfiltrounidade.TabIndex = 14;
            // 
            // lfiltrounidade
            // 
            this.lfiltrounidade.AutoSize = true;
            this.lfiltrounidade.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lfiltrounidade.Location = new System.Drawing.Point(887, 64);
            this.lfiltrounidade.Name = "lfiltrounidade";
            this.lfiltrounidade.Size = new System.Drawing.Size(79, 21);
            this.lfiltrounidade.TabIndex = 13;
            this.lfiltrounidade.Text = "Unidade:";
            // 
            // pestoquecompleto
            // 
            this.pestoquecompleto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pestoquecompleto.Controls.Add(this.cbfiltrounidade);
            this.pestoquecompleto.Controls.Add(this.tbfiltroproduto);
            this.pestoquecompleto.Controls.Add(this.lfiltrounidade);
            this.pestoquecompleto.Controls.Add(this.lfiltrostatus);
            this.pestoquecompleto.Controls.Add(this.cbfiltrostatus);
            this.pestoquecompleto.Controls.Add(this.cbfiltrocriticos);
            this.pestoquecompleto.Dock = System.Windows.Forms.DockStyle.Top;
            this.pestoquecompleto.Location = new System.Drawing.Point(0, 0);
            this.pestoquecompleto.Name = "pestoquecompleto";
            this.pestoquecompleto.Size = new System.Drawing.Size(2445, 178);
            this.pestoquecompleto.TabIndex = 15;
            // 
            // ucestoquecompleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ltotalprodutos);
            this.Controls.Add(this.ltotalcriticos);
            this.Controls.Add(this.ltotalvalorcusto);
            this.Controls.Add(this.ltotalvalorvenda);
            this.Controls.Add(this.dgvestoquecompleto);
            this.Controls.Add(this.bexportarestoque);
            this.Controls.Add(this.cbfiltrocategoria);
            this.Controls.Add(this.lfiltrocategoria);
            this.Controls.Add(this.lfiltroproduto);
            this.Controls.Add(this.pestoquecompleto);
            this.Name = "ucestoquecompleto";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvestoquecompleto)).EndInit();
            this.pestoquecompleto.ResumeLayout(false);
            this.pestoquecompleto.PerformLayout();
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
        private System.Windows.Forms.ComboBox cbfiltrounidade;
        private System.Windows.Forms.Label lfiltrounidade;
        private System.Windows.Forms.Panel pestoquecompleto;
    }
}

namespace erp_salgados_e_cia
{
    partial class ucinventario
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
            this.linfoinventario = new System.Windows.Forms.Label();
            this.lavisoinventario = new System.Windows.Forms.Label();
            this.dgvinventario = new System.Windows.Forms.DataGridView();
            this.lobservacaoinventario = new System.Windows.Forms.Label();
            this.tbobservacaoinventario = new System.Windows.Forms.TextBox();
            this.blimparinventario = new System.Windows.Forms.Button();
            this.befetivarinventario = new System.Windows.Forms.Button();
            this.ldivergenciasinventario = new System.Windows.Forms.Label();
            this.lcontadosinventario = new System.Windows.Forms.Label();
            this.lresponsavelinventario = new System.Windows.Forms.Label();
            this.cbresponsavelinventario = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvinventario)).BeginInit();
            this.SuspendLayout();
            // 
            // linfoinventario
            // 
            this.linfoinventario.AutoSize = true;
            this.linfoinventario.Location = new System.Drawing.Point(735, 41);
            this.linfoinventario.Name = "linfoinventario";
            this.linfoinventario.Size = new System.Drawing.Size(465, 16);
            this.linfoinventario.TabIndex = 2;
            this.linfoinventario.Text = "Informe a quantidade contada. Deixe em branco os produtos que não contou.";
            // 
            // lavisoinventario
            // 
            this.lavisoinventario.AutoSize = true;
            this.lavisoinventario.Location = new System.Drawing.Point(735, 87);
            this.lavisoinventario.Name = "lavisoinventario";
            this.lavisoinventario.Size = new System.Drawing.Size(0, 16);
            this.lavisoinventario.TabIndex = 3;
            // 
            // dgvinventario
            // 
            this.dgvinventario.AllowUserToAddRows = false;
            this.dgvinventario.AllowUserToDeleteRows = false;
            this.dgvinventario.AllowUserToOrderColumns = true;
            this.dgvinventario.AllowUserToResizeColumns = false;
            this.dgvinventario.AllowUserToResizeRows = false;
            this.dgvinventario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvinventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvinventario.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvinventario.Location = new System.Drawing.Point(804, 323);
            this.dgvinventario.MultiSelect = false;
            this.dgvinventario.Name = "dgvinventario";
            this.dgvinventario.RowHeadersVisible = false;
            this.dgvinventario.RowHeadersWidth = 51;
            this.dgvinventario.RowTemplate.Height = 24;
            this.dgvinventario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvinventario.Size = new System.Drawing.Size(836, 686);
            this.dgvinventario.TabIndex = 20;
            // 
            // lobservacaoinventario
            // 
            this.lobservacaoinventario.AutoSize = true;
            this.lobservacaoinventario.Location = new System.Drawing.Point(801, 1025);
            this.lobservacaoinventario.Name = "lobservacaoinventario";
            this.lobservacaoinventario.Size = new System.Drawing.Size(85, 16);
            this.lobservacaoinventario.TabIndex = 21;
            this.lobservacaoinventario.Text = "Observação:";
            // 
            // tbobservacaoinventario
            // 
            this.tbobservacaoinventario.Location = new System.Drawing.Point(892, 1022);
            this.tbobservacaoinventario.Name = "tbobservacaoinventario";
            this.tbobservacaoinventario.Size = new System.Drawing.Size(100, 22);
            this.tbobservacaoinventario.TabIndex = 22;
            // 
            // blimparinventario
            // 
            this.blimparinventario.Location = new System.Drawing.Point(1316, 1038);
            this.blimparinventario.Name = "blimparinventario";
            this.blimparinventario.Size = new System.Drawing.Size(144, 23);
            this.blimparinventario.TabIndex = 23;
            this.blimparinventario.Text = "Limpar contagem";
            this.blimparinventario.UseVisualStyleBackColor = true;
            // 
            // befetivarinventario
            // 
            this.befetivarinventario.Location = new System.Drawing.Point(1188, 1038);
            this.befetivarinventario.Name = "befetivarinventario";
            this.befetivarinventario.Size = new System.Drawing.Size(122, 23);
            this.befetivarinventario.TabIndex = 24;
            this.befetivarinventario.Text = "Efetivar inventário";
            this.befetivarinventario.UseVisualStyleBackColor = true;
            // 
            // ldivergenciasinventario
            // 
            this.ldivergenciasinventario.AutoSize = true;
            this.ldivergenciasinventario.Location = new System.Drawing.Point(1145, 224);
            this.ldivergenciasinventario.Name = "ldivergenciasinventario";
            this.ldivergenciasinventario.Size = new System.Drawing.Size(100, 16);
            this.ldivergenciasinventario.TabIndex = 25;
            this.ldivergenciasinventario.Text = "Divergências: 0";
            // 
            // lcontadosinventario
            // 
            this.lcontadosinventario.AutoSize = true;
            this.lcontadosinventario.Location = new System.Drawing.Point(1028, 224);
            this.lcontadosinventario.Name = "lcontadosinventario";
            this.lcontadosinventario.Size = new System.Drawing.Size(78, 16);
            this.lcontadosinventario.TabIndex = 26;
            this.lcontadosinventario.Text = "Contados: 0";
            // 
            // lresponsavelinventario
            // 
            this.lresponsavelinventario.AutoSize = true;
            this.lresponsavelinventario.Location = new System.Drawing.Point(1218, 41);
            this.lresponsavelinventario.Name = "lresponsavelinventario";
            this.lresponsavelinventario.Size = new System.Drawing.Size(91, 16);
            this.lresponsavelinventario.TabIndex = 27;
            this.lresponsavelinventario.Text = "Responsável:";
            // 
            // cbresponsavelinventario
            // 
            this.cbresponsavelinventario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbresponsavelinventario.FormattingEnabled = true;
            this.cbresponsavelinventario.Location = new System.Drawing.Point(1325, 38);
            this.cbresponsavelinventario.Name = "cbresponsavelinventario";
            this.cbresponsavelinventario.Size = new System.Drawing.Size(121, 24);
            this.cbresponsavelinventario.TabIndex = 19;
            // 
            // ucinventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbresponsavelinventario);
            this.Controls.Add(this.lresponsavelinventario);
            this.Controls.Add(this.lcontadosinventario);
            this.Controls.Add(this.ldivergenciasinventario);
            this.Controls.Add(this.befetivarinventario);
            this.Controls.Add(this.blimparinventario);
            this.Controls.Add(this.tbobservacaoinventario);
            this.Controls.Add(this.lobservacaoinventario);
            this.Controls.Add(this.dgvinventario);
            this.Controls.Add(this.lavisoinventario);
            this.Controls.Add(this.linfoinventario);
            this.Name = "ucinventario";
            this.Size = new System.Drawing.Size(2445, 1332);
            ((System.ComponentModel.ISupportInitialize)(this.dgvinventario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label linfoinventario;
        private System.Windows.Forms.Label lavisoinventario;
        private System.Windows.Forms.DataGridView dgvinventario;
        private System.Windows.Forms.Label lobservacaoinventario;
        private System.Windows.Forms.TextBox tbobservacaoinventario;
        private System.Windows.Forms.Button blimparinventario;
        private System.Windows.Forms.Button befetivarinventario;
        private System.Windows.Forms.Label ldivergenciasinventario;
        private System.Windows.Forms.Label lcontadosinventario;
        private System.Windows.Forms.Label lresponsavelinventario;
        private System.Windows.Forms.ComboBox cbresponsavelinventario;
    }
}

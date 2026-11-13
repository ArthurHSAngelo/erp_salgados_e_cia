using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace erp_salgados_e_cia
{
    public partial class uccaixa : UserControl
    {
        private BindingSource fonteCaixas =
            new BindingSource();

        private bool editandoCaixa;

        public uccaixa()
        {
            InitializeComponent();

            dgvcaixas.DataSource =
                fonteCaixas;

            beditarcaixa.Click += beditarcaixa_Click;
            tbvalormaquininha.TextChanged += tbvalormaquininha_TextChanged;

            AtualizarTela();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && editandoCaixa)
            {
                SairModoEdicao();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private Color CorDaDiferenca(decimal diferenca)
        {
            return diferenca == 0
                ? Color.SeaGreen
                : diferenca > 0
                ? Color.RoyalBlue
                : Color.Firebrick;
        }

        private void EntrarModoEdicao()
        {
            Caixa caixa =
                dadossistema.CaixaAberto();

            if (caixa == null)
            {
                return;
            }

            editandoCaixa = true;

            CarregarResponsaveis();

            cbresponsavel.SelectedItem = caixa.Responsavel;
            tbvalorinicial.Text = caixa.ValorInicial.ToString("N2");
            tbmotivoedicao.Clear();

            labrircaixa.Text = "Editando abertura (ESC para cancelar)";
            babrircaixa.Text = "Salvar edição";

            AtualizarTela();

            tbvalorinicial.Focus();
        }

        private void SairModoEdicao()
        {
            editandoCaixa = false;

            tbmotivoedicao.Clear();
            tbvalorinicial.Clear();
            cbresponsavel.SelectedIndex = -1;

            labrircaixa.Text = "Abrir caixa";
            babrircaixa.Text = "Abrir caixa";

            AtualizarTela();
        }

        private void beditarcaixa_Click(object sender, EventArgs e)
        {
            EntrarModoEdicao();
        }

        private void SalvarEdicaoCaixa()
        {
            Caixa caixa =
                dadossistema.CaixaAberto();

            if (caixa == null)
            {
                SairModoEdicao();
                return;
            }

            if (cbresponsavel.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o responsável.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbresponsavel.Focus();
                return;
            }

            if (!decimal.TryParse(
                    tbvalorinicial.Text,
                    out decimal novoValorInicial)
                || novoValorInicial < 0)
            {
                MessageBox.Show(
                    "Informe um valor inicial válido (zero ou maior).",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbvalorinicial.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbmotivoedicao.Text))
            {
                MessageBox.Show(
                    "Informe o motivo da edição.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbmotivoedicao.Focus();
                return;
            }

            if (novoValorInicial == caixa.ValorInicial &&
                cbresponsavel.Text == caixa.Responsavel)
            {
                MessageBox.Show(
                    "Nenhuma alteração foi feita.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            string autorizadoPor;

            if (dadossistema.LoginEhDeAtendente)
            {
                using (frmlogin autorizacao = new frmlogin(true))
                {
                    if (autorizacao.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    autorizadoPor = autorizacao.LoginAutorizador;
                }
            }
            else
            {
                autorizadoPor = dadossistema.UsuarioLogado.Login;
            }

            string mensagem;

            if (!dadossistema.EditarCaixa(
                    caixa,
                    novoValorInicial,
                    cbresponsavel.Text,
                    tbmotivoedicao.Text,
                    autorizadoPor,
                    out mensagem))
            {
                MessageBox.Show(
                    mensagem,
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            SairModoEdicao();

            MessageBox.Show(
                "Caixa editado com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void CarregarResponsaveis()
        {
            string responsavelSelecionado =
                cbresponsavel.SelectedItem as string;

            cbresponsavel.Items.Clear();

            foreach (string nome in
                dadossistema.NomesFuncionariosAtivos(
                    dadossistema.UnidadeAtual))
            {
                cbresponsavel.Items.Add(nome);
            }

            if (responsavelSelecionado != null &&
                cbresponsavel.Items.Contains(responsavelSelecionado))
            {
                cbresponsavel.SelectedItem = responsavelSelecionado;
            }
            else
            {
                cbresponsavel.SelectedIndex = -1;
            }
        }

        public void AtualizarTela()
        {
            CarregarResponsaveis();

            Caixa caixa =
                dadossistema.CaixaAberto();

            if (caixa == null)
            {
                editandoCaixa = false;
            }

            pabertura.Visible = caixa == null || editandoCaixa;
            presumo.Visible = caixa != null && !editandoCaixa;

            lmotivoedicao.Visible = editandoCaixa;
            tbmotivoedicao.Visible = editandoCaixa;

            if (caixa != null)
            {
                linfocaixa.Text =
                    $"Aberto em {caixa.DataAbertura:dd/MM/yyyy HH:mm} por {caixa.Responsavel}";

                lvalorinicialresumo.Text =
                    $"Valor inicial: R$ {caixa.ValorInicial:N2}";

                ldinheiro.Text =
                    $"Dinheiro: R$ {dadossistema.TotalRecebidoPorForma(caixa.IdCaixa, "Dinheiro"):N2}";

                lpix.Text =
                    $"Pix: R$ {dadossistema.TotalRecebidoPorForma(caixa.IdCaixa, "Pix"):N2}";

                lcredito.Text =
                    $"Crédito: R$ {dadossistema.TotalRecebidoPorForma(caixa.IdCaixa, "Crédito"):N2}";

                ldebito.Text =
                    $"Débito: R$ {dadossistema.TotalRecebidoPorForma(caixa.IdCaixa, "Débito"):N2}";

                ltotalvendas.Text =
                    $"Total de vendas (sem Notinha): R$ {caixa.TotalVendas:N2}";

                lnotinha.Text =
                    $"Notinha do dia (fiado): R$ {caixa.TotalNotinha:N2}";

                labatimentos.Text =
                    $"Abatimentos de Notinha recebidos: R$ {caixa.TotalAbatimentos:N2}";

                ldinheiroesperado.Text =
                    $"Dinheiro esperado: R$ {caixa.DinheiroEsperado:N2}";

                lmaquininhaesperada.Text =
                    $"Maquininha esperada: R$ {caixa.MaquininhaEsperada:N2}";

                AtualizarDiferenca();
                AtualizarDiferencaMaquininha();
            }

            AtualizarHistorico();
        }

        private void AtualizarHistorico()
        {
            fonteCaixas.DataSource =
                dadossistema.caixas
                .Where(c => c.Unidade == dadossistema.UnidadeAtual)
                .OrderByDescending(c => c.DataAbertura)
                .ToList();

            if (dgvcaixas.Columns.Contains("DataAbertura"))
            {
                dgvcaixas.Columns["DataAbertura"].HeaderText = "Abertura";
                dgvcaixas.Columns["ValorInicial"].HeaderText = "Valor inicial";
                dgvcaixas.Columns["DataFechamento"].HeaderText = "Fechamento";
                dgvcaixas.Columns["TotalVendas"].HeaderText = "Vendas";
                dgvcaixas.Columns["TotalNotinha"].HeaderText = "Notinha";
                dgvcaixas.Columns["TotalAbatimentos"].HeaderText = "Abatimentos";
                dgvcaixas.Columns["DinheiroEsperado"].HeaderText = "Dinheiro esperado";
                dgvcaixas.Columns["ValorContado"].HeaderText = "Contado";
                dgvcaixas.Columns["Diferenca"].HeaderText = "Diferença";
                dgvcaixas.Columns["MaquininhaEsperada"].HeaderText = "Maquininha esperada";
                dgvcaixas.Columns["ValorMaquininha"].HeaderText = "Maquininha informada";
                dgvcaixas.Columns["DiferencaMaquininha"].HeaderText = "Diferença maquininha";

                dgvcaixas.Columns["DataAbertura"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dgvcaixas.Columns["DataFechamento"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                foreach (string coluna in new[]
                {
                    "ValorInicial",
                    "TotalVendas",
                    "TotalNotinha",
                    "TotalAbatimentos",
                    "DinheiroEsperado",
                    "ValorContado",
                    "Diferenca",
                    "MaquininhaEsperada",
                    "ValorMaquininha",
                    "DiferencaMaquininha"
                })
                {
                    dgvcaixas.Columns[coluna]
                        .DefaultCellStyle.Format = "C2";
                }
            }
        }

        private void AtualizarDiferenca()
        {
            Caixa caixa =
                dadossistema.CaixaAberto();

            if (caixa == null)
            {
                return;
            }

            if (!decimal.TryParse(
                    tbvalorcontado.Text,
                    out decimal valorContado))
            {
                ldiferenca.Text = "Diferença: R$ 0,00";
                ldiferenca.ForeColor = SystemColors.ControlText;
                return;
            }

            decimal diferenca =
                valorContado - caixa.DinheiroEsperado;

            ldiferenca.Text =
                $"Diferença: R$ {diferenca:N2}";

            ldiferenca.ForeColor =
                CorDaDiferenca(diferenca);
        }

        private void AtualizarDiferencaMaquininha()
        {
            Caixa caixa =
                dadossistema.CaixaAberto();

            if (caixa == null)
            {
                return;
            }

            if (!decimal.TryParse(
                    tbvalormaquininha.Text,
                    out decimal valorMaquininha))
            {
                ldiferencamaquininha.Text =
                    "Diferença da maquininha: R$ 0,00";

                ldiferencamaquininha.ForeColor =
                    SystemColors.ControlText;

                return;
            }

            decimal diferenca =
                valorMaquininha - caixa.MaquininhaEsperada;

            ldiferencamaquininha.Text =
                $"Diferença da maquininha: R$ {diferenca:N2}";

            ldiferencamaquininha.ForeColor =
                CorDaDiferenca(diferenca);
        }

        private void tbvalorcontado_TextChanged(object sender, EventArgs e)
        {
            AtualizarDiferenca();
        }

        private void tbvalormaquininha_TextChanged(object sender, EventArgs e)
        {
            AtualizarDiferencaMaquininha();
        }

        private void babrircaixa_Click(object sender, EventArgs e)
        {
            if (editandoCaixa)
            {
                SalvarEdicaoCaixa();
                return;
            }

            if (dadossistema.CaixaAberto() != null)
            {
                AtualizarTela();
                return;
            }

            if (dadossistema.CaixaDeHoje() != null)
            {
                MessageBox.Show(
                    "O caixa de hoje já foi aberto e fechado. Um novo caixa só pode ser aberto amanhã.",
                    "Abertura não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cbresponsavel.Items.Count == 0)
            {
                MessageBox.Show(
                    "Esta unidade não tem funcionário ativo. Cadastre um em Cadastros, Funcionários.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cbresponsavel.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o responsável.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbresponsavel.Focus();
                return;
            }

            if (!decimal.TryParse(
                    tbvalorinicial.Text,
                    out decimal valorInicial)
                || valorInicial < 0)
            {
                MessageBox.Show(
                    "Informe um valor inicial válido (zero ou maior).",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbvalorinicial.Focus();
                return;
            }

            Caixa novoCaixa = new Caixa
            {
                IdCaixa = dadossistema.ProximoIdCaixa(),
                Unidade = dadossistema.UnidadeAtual,
                Responsavel = cbresponsavel.Text,
                DataAbertura = DateTime.Now,
                ValorInicial = valorInicial,
                Status = "Aberto"
            };

            dadossistema.caixas.Add(novoCaixa);

            dadossistema.RegistrarLog(
                TipoOperacaoLog.Inclusao,
                "Caixa",
                $"Caixa {novoCaixa.IdCaixa}",
                $"Responsável: {novoCaixa.Responsavel}; Valor inicial: {novoCaixa.ValorInicial:N2}; Unidade: {novoCaixa.Unidade}");

            cbresponsavel.SelectedIndex = -1;
            tbvalorinicial.Clear();

            AtualizarTela();

            MessageBox.Show(
                "Caixa aberto com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void bfecharcaixa_Click(object sender, EventArgs e)
        {
            Caixa caixa =
                dadossistema.CaixaAberto();

            if (caixa == null)
            {
                return;
            }

            if (dadossistema.ComandasDaUnidade().Count > 0)
            {
                MessageBox.Show(
                    "Fechamento bloqueado: existem comandas abertas.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!decimal.TryParse(
                    tbvalorcontado.Text,
                    out decimal valorContado)
                || valorContado < 0)
            {
                MessageBox.Show(
                    "Informe o valor contado em dinheiro.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbvalorcontado.Focus();
                return;
            }

            decimal valorMaquininha;

            if (string.IsNullOrWhiteSpace(tbvalormaquininha.Text))
            {
                if (caixa.MaquininhaEsperada != 0)
                {
                    MessageBox.Show(
                        "Informe o valor da maquininha (Crédito + Débito).",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    tbvalormaquininha.Focus();
                    return;
                }

                valorMaquininha = 0m;
            }
            else if (!decimal.TryParse(
                         tbvalormaquininha.Text,
                         out valorMaquininha)
                     || valorMaquininha < 0)
            {
                MessageBox.Show(
                    "Informe um valor válido para a maquininha.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbvalormaquininha.Focus();
                return;
            }

            decimal diferenca =
                valorContado - caixa.DinheiroEsperado;

            decimal diferencaMaquininha =
                valorMaquininha - caixa.MaquininhaEsperada;

            string aviso = "";

            if (diferenca != 0)
            {
                aviso += $"Diferença no dinheiro: R$ {diferenca:N2}. ";
            }

            if (diferencaMaquininha != 0)
            {
                aviso += $"Diferença na maquininha: R$ {diferencaMaquininha:N2}. ";
            }

            DialogResult resposta = MessageBox.Show(
                aviso == ""
                ? "Deseja realmente fechar o caixa?"
                : aviso + "(informado menos esperado). Fechar o caixa mesmo assim?",
                "Confirmar fechamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            caixa.ValorContado = valorContado;
            caixa.ValorMaquininha = valorMaquininha;
            caixa.DataFechamento = DateTime.Now;
            caixa.Status = "Fechado";

            dadossistema.RegistrarLog(
                TipoOperacaoLog.Alteracao,
                "Caixa",
                $"Caixa {caixa.IdCaixa}",
                $"Status: Aberto → Fechado; Valor contado: {valorContado:N2}; Dinheiro esperado: {caixa.DinheiroEsperado:N2}; Diferença: {diferenca:N2}; Maquininha informada: {valorMaquininha:N2}; Maquininha esperada: {caixa.MaquininhaEsperada:N2}; Diferença maquininha: {diferencaMaquininha:N2}");

            tbvalorcontado.Clear();
            tbvalormaquininha.Clear();

            AtualizarTela();

            MessageBox.Show(
                "Caixa fechado com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
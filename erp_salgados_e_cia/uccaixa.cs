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

        public uccaixa()
        {
            InitializeComponent();

            dgvcaixas.DataSource =
                fonteCaixas;

            AtualizarTela();
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

            pabertura.Visible = caixa == null;
            presumo.Visible = caixa != null;

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

                AtualizarDiferenca();
            }

            AtualizarHistorico();
        }

        private void AtualizarHistorico()
        {
            fonteCaixas.DataSource =
                dadossistema.caixas
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
                    "Diferenca"
                })
                {
                    dgvcaixas.Columns[coluna]
                        .DefaultCellStyle.Format = "N2";
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
                diferenca == 0
                ? SystemColors.ControlText
                : Color.Firebrick;
        }

        private void tbvalorcontado_TextChanged(object sender, EventArgs e)
        {
            AtualizarDiferenca();
        }

        private void babrircaixa_Click(object sender, EventArgs e)
        {
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

            if (dadossistema.comandas.Count > 0)
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

            decimal diferenca =
                valorContado - caixa.DinheiroEsperado;

            DialogResult resposta = MessageBox.Show(
                diferenca == 0
                ? "Deseja realmente fechar o caixa?"
                : $"Há uma diferença de R$ {diferenca:N2} (contado menos esperado). Fechar o caixa mesmo assim?",
                "Confirmar fechamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            caixa.ValorContado = valorContado;
            caixa.DataFechamento = DateTime.Now;
            caixa.Status = "Fechado";

            dadossistema.RegistrarLog(
                TipoOperacaoLog.Alteracao,
                "Caixa",
                $"Caixa {caixa.IdCaixa}",
                $"Status: Aberto → Fechado; Valor contado: {valorContado:N2}; Dinheiro esperado: {caixa.DinheiroEsperado:N2}; Diferença: {diferenca:N2}");

            tbvalorcontado.Clear();

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
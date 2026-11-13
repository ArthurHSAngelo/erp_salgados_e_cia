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
    public partial class ucpainel : UserControl
    {
        public event Action<string> Navegar;

        private Timer temporizador =
            new Timer();

        private void ConfigurarGrade(DataGridView grade)
        {
            grade.AllowUserToAddRows = false;
            grade.AllowUserToDeleteRows = false;
            grade.ReadOnly = true;
            grade.MultiSelect = false;
            grade.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        public void AtualizarTela()
        {
            bool atendente =
                dadossistema.LoginEhDeAtendente;

            DadosPainel dados =
                dadossistema.MontarPainel(
                    dadossistema.UnidadeAtual,
                    !atendente);

            lunidadepainel.Text =
                $"Unidade: {dados.Unidade}";

            latualizacaopainel.Text =
                $"Atualizado às {DateTime.Now:HH:mm:ss}";

            AtualizarCaixa(dados);
            AtualizarVendas(dados, atendente);
            AtualizarEstoque(dados);
            AtualizarNotinha(dados);
            AtualizarUltimasVendas(dados);
            AtualizarAtividade(dados, atendente);
        }

        private void AtualizarCaixa(DadosPainel dados)
        {
            lstatuscaixa.Text = dados.StatusCaixa;

            if (dados.AberturaCaixa == null)
            {
                lresponsavelcaixa.Text = "Responsável: -";
                laberturacaixa.Text = "Aberto em: -";
                linicialcaixa.Text = "Valor inicial: -";
                lesperadocaixa.Text = "Dinheiro esperado: -";
                return;
            }

            lresponsavelcaixa.Text =
                $"Responsável: {dados.ResponsavelCaixa}";

            laberturacaixa.Text =
                $"Aberto em: {dados.AberturaCaixa:dd/MM/yyyy HH:mm}";

            linicialcaixa.Text =
                $"Valor inicial: R$ {dados.ValorInicialCaixa:N2}";

            lesperadocaixa.Text =
                $"Dinheiro esperado: R$ {dados.DinheiroEsperado:N2}";
        }

        private void AtualizarVendas(DadosPainel dados, bool atendente)
        {
            lvendashoje.Text =
                dados.VendasHoje == 0
                ? "Nenhuma venda hoje"
                : $"Vendas: {dados.VendasHoje}";

            lfaturamentohoje.Text =
                $"Faturamento: R$ {dados.FaturamentoHoje:N2}";

            lticketmediohoje.Text =
                $"Ticket médio: R$ {dados.TicketMedioHoje:N2}";

            lnotinhahoje.Text =
                $"Em Notinha: R$ {dados.EmNotinhaHoje:N2}";

            llinkvendas.Text =
                atendente
                ? "Ver histórico"
                : "Ver relatório";
        }

        private void AtualizarEstoque(DadosPainel dados)
        {
            ltotalcriticos.Text =
                dados.TotalCriticos == 0
                ? "Nenhum produto em estoque crítico."
                : $"Em estoque crítico: {dados.TotalCriticos}";

            dgvcriticos.DataSource = null;
            dgvcriticos.DataSource = dados.Criticos;

            if (dgvcriticos.Columns.Count > 0)
            {
                dgvcriticos.Columns["Produto"].HeaderText = "Produto";
                dgvcriticos.Columns["Saldo"].HeaderText = "Saldo";
                dgvcriticos.Columns["Minimo"].HeaderText = "Mínimo";

                dgvcriticos.Columns["Saldo"]
                    .DefaultCellStyle.Format = "N2";

                dgvcriticos.Columns["Minimo"]
                    .DefaultCellStyle.Format = "N2";
            }
        }

        private void AtualizarNotinha(DadosPainel dados)
        {
            lnotinhatotal.Text =
                $"Total devido: R$ {dados.NotinhaTotal:N2}";

            lnotinhaclientes.Text =
                $"Clientes devedores: {dados.NotinhaClientes}";
        }

        private void AtualizarUltimasVendas(DadosPainel dados)
        {
            dgvultimasvendas.DataSource = null;
            dgvultimasvendas.DataSource = dados.UltimasVendas;

            if (dgvultimasvendas.Columns.Count > 0)
            {
                dgvultimasvendas.Columns["Venda"].HeaderText = "Venda";
                dgvultimasvendas.Columns["DataHora"].HeaderText = "Data e hora";
                dgvultimasvendas.Columns["Total"].HeaderText = "Total";
                dgvultimasvendas.Columns["Status"].HeaderText = "Status";

                dgvultimasvendas.Columns["DataHora"]
                    .DefaultCellStyle.Format = "dd/MM HH:mm";

                dgvultimasvendas.Columns["Total"]
                    .DefaultCellStyle.Format = "C2";
            }
        }

        private void AtualizarAtividade(DadosPainel dados, bool atendente)
        {
            pcartaoatividade.Visible = !atendente;

            if (atendente)
            {
                return;
            }

            dgvatividade.DataSource = null;
            dgvatividade.DataSource = dados.Atividade;

            if (dgvatividade.Columns.Count > 0)
            {
                dgvatividade.Columns["DataHora"].HeaderText = "Data e hora";
                dgvatividade.Columns["Usuario"].HeaderText = "Usuário";
                dgvatividade.Columns["Operacao"].HeaderText = "Operação";
                dgvatividade.Columns["Registro"].HeaderText = "Registro";

                dgvatividade.Columns["DataHora"]
                    .DefaultCellStyle.Format = "dd/MM HH:mm:ss";
            }
        }

        public ucpainel()
        {
            InitializeComponent();

            ConfigurarGrade(dgvcriticos);
            ConfigurarGrade(dgvultimasvendas);
            ConfigurarGrade(dgvatividade);

            temporizador.Interval = 5000;

            temporizador.Tick += temporizador_Tick;

            ParentChanged += ucpainel_ParentChanged;

            Disposed += ucpainel_Disposed;

            llinkcaixa.LinkClicked +=
                (origem, argumentos) => Navegar?.Invoke("caixa");

            llinkvendas.LinkClicked +=
                (origem, argumentos) => Navegar?.Invoke("vendas");

            llinkestoque.LinkClicked +=
                (origem, argumentos) => Navegar?.Invoke("estoquecritico");

            llinknotinha.LinkClicked +=
                (origem, argumentos) => Navegar?.Invoke("notinha");

            llinkultimas.LinkClicked +=
                (origem, argumentos) => Navegar?.Invoke("ultimasvendas");

            llinkatividade.LinkClicked +=
                (origem, argumentos) => Navegar?.Invoke("log");
        }

        private void ucpainel_ParentChanged(object sender, EventArgs e)
        {
            if (Parent == null)
            {
                temporizador.Stop();
                return;
            }

            AtualizarTela();

            temporizador.Start();
        }

        private void temporizador_Tick(object sender, EventArgs e)
        {
            if (Parent == null)
            {
                temporizador.Stop();
                return;
            }

            AtualizarTela();
        }

        private void ucpainel_Disposed(object sender, EventArgs e)
        {
            temporizador.Stop();
            temporizador.Dispose();
        }
    }
}

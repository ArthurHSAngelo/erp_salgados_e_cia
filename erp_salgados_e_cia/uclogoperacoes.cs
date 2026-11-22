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
    public partial class uclogoperacoes : UserControl
    {
        private readonly TipoOperacaoLog[] operacoes =
        {
            TipoOperacaoLog.Inclusao,
            TipoOperacaoLog.Alteracao,
            TipoOperacaoLog.Exclusao,
            TipoOperacaoLog.Login,
            TipoOperacaoLog.Logout
        };

        private BindingSource fonteLog =
            new BindingSource();

        private bool carregandoFiltros = false;

        private void CarregarFiltros()
        {
            carregandoFiltros = true;

            string usuarioSelecionado =
                cbfiltrousuario.SelectedItem as string;

            string tabelaSelecionada =
                cbfiltrotabela.SelectedItem as string;

            cbfiltrousuario.Items.Clear();
            cbfiltrousuario.Items.Add("Todos");

            foreach (string usuario in dadossistema.UsuariosDoLog())
            {
                cbfiltrousuario.Items.Add(usuario);
            }

            if (usuarioSelecionado != null &&
                cbfiltrousuario.Items.Contains(usuarioSelecionado))
            {
                cbfiltrousuario.SelectedItem = usuarioSelecionado;
            }
            else
            {
                cbfiltrousuario.SelectedIndex = 0;
            }

            cbfiltrotabela.Items.Clear();
            cbfiltrotabela.Items.Add("Todas");

            foreach (string tabela in dadossistema.TabelasDoLog())
            {
                cbfiltrotabela.Items.Add(tabela);
            }

            if (tabelaSelecionada != null &&
                cbfiltrotabela.Items.Contains(tabelaSelecionada))
            {
                cbfiltrotabela.SelectedItem = tabelaSelecionada;
            }
            else
            {
                cbfiltrotabela.SelectedIndex = 0;
            }

            carregandoFiltros = false;
        }

        private void ConfigurarColunas()
        {
            if (dgvlog.Columns.Count == 0)
            {
                return;
            }

            dgvlog.Columns["DataHora"].HeaderText =
                "Data e hora";

            dgvlog.Columns["Usuario"].HeaderText =
                "Usuário";

            dgvlog.Columns["OperacaoTexto"].HeaderText =
                "Operação";

            dgvlog.Columns["Tabela"].HeaderText =
                "Tabela";

            dgvlog.Columns["Registro"].HeaderText =
                "Registro afetado";

            dgvlog.Columns["Detalhes"].HeaderText =
                "Detalhes";

            dgvlog.Columns["DataHora"]
                .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
        }

        private void GerarConsulta()
        {
            DateTime? inicio =
                dtpfiltroinicio.Checked
                ? (DateTime?)dtpfiltroinicio.Value.Date
                : null;

            DateTime? fim =
                dtpfiltrofim.Checked
                ? (DateTime?)dtpfiltrofim.Value.Date
                : null;

            DateTime dataInicial;
            DateTime dataFinal;

            dadossistema.DefinirPeriodoPadrao(
                inicio,
                fim,
                out dataInicial,
                out dataFinal
            );

            if (dataInicial > dataFinal)
            {
                fonteLog.DataSource =
                    new List<LogOperacao>();

                ConfigurarColunas();

                lperiodoaplicado.Text =
                    "Período: -";

                lresultadolog.Text =
                    "A data final não pode ser anterior à data inicial.";

                return;
            }

            string usuarioFiltro =
                cbfiltrousuario.SelectedItem == null ||
                cbfiltrousuario.SelectedIndex == 0
                ? null
                : cbfiltrousuario.SelectedItem.ToString();

            string tabelaFiltro =
                cbfiltrotabela.SelectedItem == null ||
                cbfiltrotabela.SelectedIndex == 0
                ? null
                : cbfiltrotabela.SelectedItem.ToString();

            TipoOperacaoLog? operacaoFiltro =
                cbfiltrooperacao.SelectedIndex > 0
                ? (TipoOperacaoLog?)operacoes[
                    cbfiltrooperacao.SelectedIndex - 1]
                : null;

            List<LogOperacao> resultado =
                dadossistema.ConsultarLog(
                    inicio,
                    fim,
                    usuarioFiltro,
                    operacaoFiltro,
                    tabelaFiltro
                );

            fonteLog.DataSource =
                resultado;

            ConfigurarColunas();

            string periodoAplicado =
                $"{dataInicial:dd/MM/yyyy} a {dataFinal:dd/MM/yyyy}";

            if (inicio == null && fim == null)
            {
                periodoAplicado +=
                    " (padrão: últimos 30 dias)";
            }

            lperiodoaplicado.Text =
                $"Período: {periodoAplicado}";

            lresultadolog.Text =
                resultado.Count == 0
                ? "Nenhum registro encontrado para os filtros informados."
                : $"{resultado.Count} registro(s) encontrado(s).";
        }

        public void AtualizarTela()
        {
            CarregarFiltros();

            GerarConsulta();
        }

        public uclogoperacoes()
        {
            InitializeComponent();

            fonteLog.DataSource =
                new List<LogOperacao>();

            dgvlog.DataSource =
                fonteLog;

            cbfiltrooperacao.Items.AddRange(
                new object[]
                {
                    "Todas",
                    "Inclusão",
                    "Alteração",
                    "Exclusão",
                    "Login",
                    "Logout"
                }
            );

            cbfiltrooperacao.SelectedIndex =
                0;

            dtpfiltroinicio.Value =
                DateTime.Today.AddDays(-30);

            dtpfiltroinicio.Checked =
                true;

            CarregarFiltros();

            dtpfiltroinicio.ValueChanged +=
                FiltroAlterado;

            dtpfiltrofim.ValueChanged +=
                FiltroAlterado;

            cbfiltrousuario.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltrooperacao.SelectedIndexChanged +=
                FiltroAlterado;

            cbfiltrotabela.SelectedIndexChanged +=
                FiltroAlterado;

            GerarConsulta();
        }

        private void FiltroAlterado(object sender, EventArgs e)
        {
            if (carregandoFiltros)
            {
                return;
            }

            GerarConsulta();
        }
    }
}

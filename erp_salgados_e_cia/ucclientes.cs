using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static erp_salgados_e_cia.dadossistema;

namespace erp_salgados_e_cia
{
    public partial class ucclientes : UserControl
    {
        private bool modoEdicao = false;
        private int indiceClienteEditando = -1;

        private BindingList<Cliente> clientes =
            dadossistema.clientes;

        private BindingSource fonteClientes =
            new BindingSource();

        private string ApenasDigitos(string texto)
        {
            return new string(
                (texto ?? "")
                    .Where(char.IsDigit)
                    .ToArray()
            );
        }

        private void AtualizarTabela()
        {
            string filtro =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Cliente> clientesFiltrados =
                clientes;

            if (filtro == "Ativos")
            {
                clientesFiltrados =
                    clientes.Where(c =>
                        c.Status == StatusCadastro.Ativo);
            }
            else if (filtro == "Inativos")
            {
                clientesFiltrados =
                    clientes.Where(c =>
                        c.Status == StatusCadastro.Inativo);
            }

            fonteClientes.DataSource =
                clientesFiltrados.ToList();

            if (dgvclientes.Columns.Count > 0)
            {
                dgvclientes.Columns["IdCliente"].HeaderText =
                    "ID";

                dgvclientes.Columns["CpfCnpj"].HeaderText =
                    "CPF/CNPJ";
            }
        }

        public ucclientes()
        {
            InitializeComponent();

            cbfiltrostatus.Items.AddRange(
                new object[]
                {
                    "Ativos",
                    "Inativos",
                    "Todos"
                }
            );

            cbfiltrostatus.SelectedIndexChanged +=
                cbfiltrostatus_SelectedIndexChanged;

            fonteClientes.DataSource =
                clientes;

            dgvclientes.DataSource =
                fonteClientes;

            cbfiltrostatus.SelectedIndex =
                0;

            AtualizarTabela();
        }

        private void cbfiltrostatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AtualizarTabela();
        }

        private void LimparCampos()
        {
            tbnomec.Clear();
            tbcpfcnpjc.Clear();
            tbtelefonec.Clear();
            cbativo.Checked = true;
            cbativo.Visible = false;

            tbnomec.Focus();
        }

        private void bsalvarcliente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbnomec.Text))
            {
                MessageBox.Show(
                    "Informe o nome do cliente.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbnomec.Focus();
                return;
            }

            string cpfCnpjDigitado =
                ApenasDigitos(tbcpfcnpjc.Text);

            if (cpfCnpjDigitado.Length > 0 &&
                cpfCnpjDigitado.Length != 11 &&
                cpfCnpjDigitado.Length != 14)
            {
                MessageBox.Show(
                    "Informe um CPF (11 dígitos) ou CNPJ (14 dígitos) válido.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcpfcnpjc.Focus();
                return;
            }

            Cliente clienteAtual = null;

            if (modoEdicao && indiceClienteEditando >= 0)
            {
                clienteAtual = clientes[indiceClienteEditando];
            }

            bool cpfCnpjExiste =
                cpfCnpjDigitado.Length > 0 &&
                clientes.Any(c =>
                    c != clienteAtual &&
                    ApenasDigitos(c.CpfCnpj) == cpfCnpjDigitado
                );

            if (cpfCnpjExiste)
            {
                MessageBox.Show(
                    "Esse CPF/CNPJ já está cadastrado.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tbcpfcnpjc.Focus();
                return;
            }

            if (modoEdicao)
            {
                if (!cbativo.Checked &&
                    clienteAtual.Status == StatusCadastro.Ativo)
                {
                    DialogResult resposta = MessageBox.Show(
                        "Deseja realmente inativar este cliente?",
                        "Confirmar inativação",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (resposta != DialogResult.Yes)
                    {
                        cbativo.Checked = true;
                        return;
                    }
                }

                clienteAtual.Nome = tbnomec.Text.Trim();
                clienteAtual.CpfCnpj = tbcpfcnpjc.Text.Trim();
                clienteAtual.Telefone = tbtelefonec.Text.Trim();

                clienteAtual.Status =
                    cbativo.Checked
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo;

                MessageBox.Show(
                    "Cliente atualizado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Cliente cliente = new Cliente
                {
                    IdCliente = dadossistema.ProximoIdCliente(),
                    Status = StatusCadastro.Ativo,
                    Nome = tbnomec.Text.Trim(),
                    CpfCnpj = tbcpfcnpjc.Text.Trim(),
                    Telefone = tbtelefonec.Text.Trim()
                };

                clientes.Add(cliente);

                MessageBox.Show(
                    "Cliente salvo com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            AtualizarTabela();
            dgvclientes.Refresh();

            modoEdicao = false;
            indiceClienteEditando = -1;

            bsalvarcliente.Text = "Salvar cliente";
            beditarc.Enabled = true;

            LimparCampos();
        }

        private void bexcluirc_Click(object sender, EventArgs e)
        {
            if (dgvclientes.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um cliente para excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Cliente clienteSelecionado =
                dgvclientes.SelectedRows[0]
                .DataBoundItem as Cliente;

            if (clienteSelecionado == null)
            {
                return;
            }

            if (dadossistema.ClientePossuiVinculos(
                    clienteSelecionado))
            {
                MessageBox.Show(
                    "Exclusão bloqueada: este cliente possui vendas.",
                    "Exclusão não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult resposta = MessageBox.Show(
                "Deseja excluir o cliente selecionado?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            clientes.Remove(clienteSelecionado);

            AtualizarTabela();
            dgvclientes.Refresh();

            MessageBox.Show(
                "Cliente excluído com sucesso.",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void beditarc_Click(object sender, EventArgs e)
        {
            if (dgvclientes.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um cliente para editar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Cliente clienteSelecionado =
                dgvclientes.SelectedRows[0]
                .DataBoundItem as Cliente;

            if (clienteSelecionado == null)
            {
                return;
            }

            indiceClienteEditando =
                clientes.IndexOf(clienteSelecionado);

            if (indiceClienteEditando == -1)
            {
                return;
            }

            tbnomec.Text =
                clienteSelecionado.Nome;

            tbcpfcnpjc.Text =
                clienteSelecionado.CpfCnpj;

            tbtelefonec.Text =
                clienteSelecionado.Telefone;

            cbativo.Checked =
                clienteSelecionado.Status == StatusCadastro.Ativo;

            cbativo.Visible = true;

            modoEdicao = true;

            bsalvarcliente.Text = "Atualizar cliente";
            beditarc.Enabled = false;

            tbnomec.Focus();
        }
    }
}
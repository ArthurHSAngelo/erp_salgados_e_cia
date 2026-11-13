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
    public partial class ucusuarios : UserControl
    {
        private const string AcessoGerente =
            "Gerente (todas as unidades)";

        private const string PrefixoLoginBase =
            "Login base: ";

        private bool modoEdicao = false;
        private int indiceUsuarioEditando = -1;

        private BindingList<Usuario> usuarios =
            dadossistema.usuarios;

        private BindingSource fonteUsuarios =
            new BindingSource();

        public void AtualizarTabela()
        {
            string filtro =
                cbfiltrostatus.SelectedItem == null
                ? "Ativos"
                : cbfiltrostatus.SelectedItem.ToString();

            IEnumerable<Usuario> usuariosFiltrados =
                usuarios.Where(u =>
                    u.Unidade == null ||
                    u.Unidade == dadossistema.UnidadeAtual);

            if (filtro == "Ativos")
            {
                usuariosFiltrados =
                    usuariosFiltrados.Where(u =>
                        u.Status == StatusCadastro.Ativo);
            }
            else if (filtro == "Inativos")
            {
                usuariosFiltrados =
                    usuariosFiltrados.Where(u =>
                        u.Status == StatusCadastro.Inativo);
            }

            fonteUsuarios.DataSource =
                usuariosFiltrados.ToList();

            if (dgvusuarios.Columns.Count > 0)
            {
                dgvusuarios.Columns["IdUsuario"].Visible = false;

                dgvusuarios.Columns["Login"].HeaderText = "Login";
                dgvusuarios.Columns["Perfil"].HeaderText = "Perfil";
                dgvusuarios.Columns["Unidade"].HeaderText = "Unidade";
                dgvusuarios.Columns["Status"].HeaderText = "Status";
            }
        }

        private string TextoAcesso(Usuario usuario)
        {
            return usuario.Perfil == PerfilUsuario.Atendente
                ? PrefixoLoginBase + usuario.Unidade
                : AcessoGerente;
        }

        public void CarregarAcessos()
        {
            string acessoSelecionado =
                cbacesso.SelectedItem as string;

            cbacesso.Items.Clear();

            cbacesso.Items.Add(AcessoGerente);

            foreach (Unidade unidade in
                dadossistema.unidades
                .Where(u => u.Status == StatusCadastro.Ativo)
                .OrderBy(u => u.Nome))
            {
                cbacesso.Items.Add(PrefixoLoginBase + unidade.Nome);
            }

            if (acessoSelecionado != null &&
                cbacesso.Items.Contains(acessoSelecionado))
            {
                cbacesso.SelectedItem = acessoSelecionado;
            }
            else
            {
                cbacesso.SelectedIndex = -1;
            }
        }

        public void AtualizarTela()
        {
            CarregarAcessos();

            AtualizarTabela();
        }

        public ucusuarios()
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

            fonteUsuarios.DataSource =
                usuarios;

            dgvusuarios.DataSource =
                fonteUsuarios;

            cbfiltrostatus.SelectedIndexChanged +=
                cbfiltrostatus_SelectedIndexChanged;

            bsalvarusuario.Click +=
                bsalvarusuario_Click;

            beditarusuario.Click +=
                beditarusuario_Click;

            cbfiltrostatus.SelectedIndex =
                0;

            CarregarAcessos();

            LimparCampos();

            AtualizarTabela();
        }

        protected override bool ProcessCmdKey(
            ref Message msg,
            Keys keyData)
        {
            if (keyData == Keys.Escape && modoEdicao)
            {
                CancelarEdicao();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cbfiltrostatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            AtualizarTabela();
        }

        private void LimparCampos()
        {
            tblogin.Clear();
            tbsenha.Clear();
            tbconfirmarsenha.Clear();

            cbacesso.SelectedIndex = -1;

            cbativo.Checked = true;
            cbativo.Visible = false;

            ldicasenha.Visible = false;

            tblogin.Focus();
        }

        private void CancelarEdicao()
        {
            modoEdicao = false;
            indiceUsuarioEditando = -1;

            bsalvarusuario.Text = "Salvar";
            beditarusuario.Enabled = true;

            CarregarAcessos();

            LimparCampos();
        }

        private bool PodeRedefinirSenha(Usuario alvo)
        {
            Usuario logado = dadossistema.UsuarioLogado;

            if (logado == null)
            {
                return false;
            }

            if (alvo == logado)
            {
                return true;
            }

            if (logado.Perfil == PerfilUsuario.Administrador)
            {
                return true;
            }

            return alvo.Perfil == PerfilUsuario.Atendente;
        }

        private void bsalvarusuario_Click(object sender, EventArgs e)
        {
            string loginDigitado =
                tblogin.Text.Trim();

            if (loginDigitado.Length < 3 ||
                loginDigitado.Contains(" "))
            {
                MessageBox.Show(
                    "Informe um login com pelo menos 3 caracteres, sem espaços.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tblogin.Focus();
                return;
            }

            if (cbacesso.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecione o acesso.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbacesso.Focus();
                return;
            }

            Usuario usuarioAtual = null;

            if (modoEdicao && indiceUsuarioEditando >= 0)
            {
                usuarioAtual = usuarios[indiceUsuarioEditando];
            }

            if (usuarioAtual != null &&
                usuarioAtual.Perfil == PerfilUsuario.Administrador)
            {
                MessageBox.Show(
                    "A conta do Administrador não pode ser alterada.",
                    "Alteração não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            bool loginExiste = usuarios.Any(u =>
                u != usuarioAtual &&
                string.Equals(
                    u.Login,
                    loginDigitado,
                    StringComparison.OrdinalIgnoreCase)
            );

            if (loginExiste)
            {
                MessageBox.Show(
                    "Esse login já está em uso.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                tblogin.Focus();
                return;
            }

            PerfilUsuario perfilEscolhido =
                cbacesso.SelectedItem.ToString() == AcessoGerente
                ? PerfilUsuario.Gerente
                : PerfilUsuario.Atendente;

            string unidadeEscolhida =
                perfilEscolhido == PerfilUsuario.Atendente
                ? cbacesso.SelectedItem.ToString()
                    .Substring(PrefixoLoginBase.Length)
                : null;

            if (perfilEscolhido == PerfilUsuario.Atendente &&
                usuarios.Any(u =>
                    u != usuarioAtual &&
                    u.Perfil == PerfilUsuario.Atendente &&
                    u.Unidade == unidadeEscolhida))
            {
                MessageBox.Show(
                    "Esta unidade já tem um login base.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                cbacesso.Focus();
                return;
            }

            bool alterarSenha =
                !modoEdicao ||
                tbsenha.Text.Length > 0 ||
                tbconfirmarsenha.Text.Length > 0;

            if (alterarSenha)
            {
                if (tbsenha.Text.Length < 6)
                {
                    MessageBox.Show(
                        "A senha deve ter pelo menos 6 caracteres.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    tbsenha.Focus();
                    return;
                }

                if (tbsenha.Text != tbconfirmarsenha.Text)
                {
                    MessageBox.Show(
                        "A confirmação da senha não confere.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    tbconfirmarsenha.Focus();
                    return;
                }
            }

            if (modoEdicao)
            {
                if (usuarioAtual == dadossistema.UsuarioLogado &&
                    (usuarioAtual.Perfil != perfilEscolhido ||
                     usuarioAtual.Unidade != unidadeEscolhida))
                {
                    MessageBox.Show(
                        "Você não pode alterar o próprio acesso.",
                        "Alteração não permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (alterarSenha &&
                    !PodeRedefinirSenha(usuarioAtual))
                {
                    MessageBox.Show(
                        "Somente o Administrador redefine a senha de outro Gerente.",
                        "Alteração não permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                if (!cbativo.Checked &&
                    usuarioAtual.Status == StatusCadastro.Ativo)
                {
                    if (usuarioAtual == dadossistema.UsuarioLogado)
                    {
                        MessageBox.Show(
                            "Você não pode inativar a própria conta.",
                            "Alteração não permitida",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        cbativo.Checked = true;
                        return;
                    }

                    DialogResult resposta = MessageBox.Show(
                        "Deseja realmente inativar este usuário?",
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

                string registroLog = $"Usuário: {usuarioAtual.Login}";

                string detalhesLog = dadossistema.JuntarDetalhes(
                    dadossistema.Alteracao(
                        "Login", usuarioAtual.Login, loginDigitado),
                    dadossistema.Alteracao(
                        "Acesso",
                        TextoAcesso(usuarioAtual),
                        cbacesso.SelectedItem.ToString()),
                    dadossistema.Alteracao(
                        "Status",
                        usuarioAtual.Status.ToString(),
                        (cbativo.Checked
                            ? StatusCadastro.Ativo
                            : StatusCadastro.Inativo).ToString()),
                    alterarSenha ? "Senha redefinida" : null
                );

                usuarioAtual.Login = loginDigitado;
                usuarioAtual.Perfil = perfilEscolhido;
                usuarioAtual.Unidade = unidadeEscolhida;

                usuarioAtual.Status =
                    cbativo.Checked
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo;

                if (alterarSenha)
                {
                    usuarioAtual.SenhaHash =
                        dadossistema.GerarHashSenha(tbsenha.Text);
                }

                if (detalhesLog != null)
                {
                    dadossistema.RegistrarLog(
                        TipoOperacaoLog.Alteracao,
                        "Usuario",
                        registroLog,
                        detalhesLog);
                }

                MessageBox.Show(
                    "Usuário atualizado com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                Usuario usuario = new Usuario
                {
                    IdUsuario = dadossistema.ProximoIdUsuario(),
                    Login = loginDigitado,
                    SenhaHash = dadossistema.GerarHashSenha(tbsenha.Text),
                    Perfil = perfilEscolhido,
                    Unidade = unidadeEscolhida,
                    Status = StatusCadastro.Ativo
                };

                usuarios.Add(usuario);

                dadossistema.RegistrarLog(
                    TipoOperacaoLog.Inclusao,
                    "Usuario",
                    $"Usuário: {usuario.Login}",
                    $"Acesso: {cbacesso.SelectedItem}");

                MessageBox.Show(
                    "Usuário salvo com sucesso.",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            AtualizarTabela();
            dgvusuarios.Refresh();

            modoEdicao = false;
            indiceUsuarioEditando = -1;

            bsalvarusuario.Text = "Salvar";
            beditarusuario.Enabled = true;

            CarregarAcessos();

            LimparCampos();
        }

        private void beditarusuario_Click(object sender, EventArgs e)
        {
            if (dgvusuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Selecione um usuário para editar.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            Usuario usuarioSelecionado =
                dgvusuarios.SelectedRows[0]
                .DataBoundItem as Usuario;

            if (usuarioSelecionado == null)
            {
                return;
            }

            if (usuarioSelecionado.Perfil == PerfilUsuario.Administrador)
            {
                MessageBox.Show(
                    "A conta do Administrador não pode ser alterada.",
                    "Alteração não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            indiceUsuarioEditando =
                usuarios.IndexOf(usuarioSelecionado);

            if (indiceUsuarioEditando == -1)
            {
                return;
            }

            tblogin.Text =
                usuarioSelecionado.Login;

            tbsenha.Clear();
            tbconfirmarsenha.Clear();

            string textoAcesso =
                TextoAcesso(usuarioSelecionado);

            if (!cbacesso.Items.Contains(textoAcesso))
            {
                cbacesso.Items.Add(textoAcesso);
            }

            cbacesso.SelectedItem = textoAcesso;

            cbativo.Checked =
                usuarioSelecionado.Status == StatusCadastro.Ativo;

            cbativo.Visible = true;
            ldicasenha.Visible = true;

            modoEdicao = true;

            bsalvarusuario.Text = "Atualizar";
            beditarusuario.Enabled = false;

            tblogin.Focus();
        }
    }
}

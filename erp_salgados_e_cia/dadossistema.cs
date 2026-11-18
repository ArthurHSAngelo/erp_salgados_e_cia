using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using erp_salgados_e_cia.Data.Connection;
using erp_salgados_e_cia.Data.Repositories;
using erp_salgados_e_cia.Domain.Entities;
using erp_salgados_e_cia.Domain.Exceptions;
using erp_salgados_e_cia.Domain.Services;

namespace erp_salgados_e_cia
{
    public static class dadossistema
    {
        // Os vínculos são conferidos no banco. As listas em memória (movimentações,
        // vendas, comandas...) ainda existem até os próximos blocos da migração,
        // por isso também entram na conta.
        public static bool ProdutoPossuiVinculos(Produto produto)
        {
            return produto != null &&
                   (ProdutoRepositorio.PossuiVinculos(produto.Id) ||
                    movimentacoes.Any(m =>
                       m.Produto == produto.Nome) ||
                    vendas.Any(v =>
                       v.Itens.Any(item =>
                           item.Produto == produto.Nome)) ||
                    comandas.Any(c =>
                       c.Itens.Any(item =>
                           item.Produto == produto.Nome)));
        }

        public static bool FornecedorPossuiVinculos(
            Fornecedor fornecedor)
        {
            return fornecedor != null &&
                   (FornecedorRepositorio.PossuiVinculos(fornecedor.Id) ||
                    movimentacoes.Any(m =>
                       m.Fornecedor == fornecedor.Nome));
        }

        public static bool UnidadePossuiVinculos(
            Unidade unidade)
        {
            return unidade != null &&
                   (UnidadeRepositorio.PossuiVinculos(unidade.Id) ||
                    movimentacoes.Any(m =>
                       m.Unidade == unidade.Nome));
        }

        public static bool ClientePossuiVinculos(
            Cliente cliente)
        {
            return cliente != null &&
                   (ClienteRepositorio.PossuiVinculos((uint)cliente.IdCliente) ||
                    vendas.Any(v =>
                       v.IdCliente == cliente.IdCliente) ||
                    comandas.Any(c =>
                       c.IdCliente == cliente.IdCliente) ||
                    notinhas.Any(n =>
                       n.IdCliente == cliente.IdCliente) ||
                    abatimentos.Any(a =>
                       a.IdCliente == cliente.IdCliente));
        }

        // ---- Cadastros lidos do banco ----
        // As listas abaixo (produtos, unidades, fornecedores, clientes,
        // funcionarios, usuarios) são apenas um espelho do que está no banco:
        // as telas gravam pelos repositórios e depois chamam CarregarXxx().
        // O objeto de cada linha é reaproveitado (pelo id), para que as telas e as
        // listas que ainda estão em memória continuem apontando para ele.

        private static bool saldosCarregados = false;

        private static StatusCadastro ParaStatus(object valor)
        {
            return System.Convert.ToString(valor) == "Ativo"
                ? StatusCadastro.Ativo
                : StatusCadastro.Inativo;
        }

        private static string TextoOuNulo(object valor)
        {
            return valor == System.DBNull.Value
                ? null
                : System.Convert.ToString(valor);
        }

        private static void Reconciliar<T>(
            BindingList<T> lista,
            List<T> doBanco)
        {
            foreach (T antigo in
                lista.Where(item => !doBanco.Contains(item)).ToList())
            {
                lista.Remove(antigo);
            }

            foreach (T novo in doBanco)
            {
                if (!lista.Contains(novo))
                {
                    lista.Add(novo);
                }
            }
        }

        public static void CarregarCadastros()
        {
            CarregarUnidades();
            CarregarProdutos();
            CarregarFornecedores();
            CarregarClientes();
            CarregarFuncionarios();
            CarregarUsuarios();
            CarregarSaldos();
            CarregarEntradas();
            CarregarPerdas();
            CarregarCaixas();
        }

        public static void CarregarUnidades()
        {
            List<Unidade> doBanco = new List<Unidade>();

            foreach (System.Data.DataRow linha in
                UnidadeRepositorio.Listar().Rows)
            {
                uint id = System.Convert.ToUInt32(linha["id"]);

                Unidade unidade =
                    unidades.FirstOrDefault(u => u.Id == id) ??
                    new Unidade { Id = id };

                unidade.Nome = System.Convert.ToString(linha["nome"]);
                unidade.Cnpj = TextoOuNulo(linha["cnpj"]);
                unidade.Endereco = System.Convert.ToString(linha["endereco"]);
                unidade.Numero = TextoOuNulo(linha["numero"]);
                unidade.Status = ParaStatus(linha["status"]);

                doBanco.Add(unidade);
            }

            Reconciliar(unidades, doBanco);
        }

        public static void CarregarProdutos()
        {
            List<Produto> doBanco = new List<Produto>();

            foreach (System.Data.DataRow linha in
                ProdutoRepositorio.Listar().Rows)
            {
                uint id = System.Convert.ToUInt32(linha["id"]);

                Produto produto =
                    produtos.FirstOrDefault(p => p.Id == id) ??
                    new Produto { Id = id };

                produto.Nome = System.Convert.ToString(linha["nome"]);
                produto.Categoria = System.Convert.ToString(linha["categoria"]);
                produto.Preco = System.Convert.ToDecimal(linha["preco_venda"]);
                produto.UnidadeMedida = System.Convert.ToString(linha["unidade_medida"]);
                produto.EstoqueMinimo = System.Convert.ToDecimal(linha["estoque_minimo"]);
                produto.Status = ParaStatus(linha["status"]);

                doBanco.Add(produto);
            }

            Reconciliar(produtos, doBanco);
        }

        public static void CarregarFornecedores()
        {
            List<Fornecedor> doBanco = new List<Fornecedor>();

            foreach (System.Data.DataRow linha in
                FornecedorRepositorio.Listar().Rows)
            {
                uint id = System.Convert.ToUInt32(linha["id"]);

                Fornecedor fornecedor =
                    fornecedores.FirstOrDefault(f => f.Id == id) ??
                    new Fornecedor { Id = id };

                fornecedor.Nome = System.Convert.ToString(linha["nome"]);
                fornecedor.Cnpj = TextoOuNulo(linha["cnpj"]);
                fornecedor.Telefone = TextoOuNulo(linha["telefone"]);
                fornecedor.Status = ParaStatus(linha["status"]);

                doBanco.Add(fornecedor);
            }

            Reconciliar(fornecedores, doBanco);
        }

        public static void CarregarClientes()
        {
            List<Cliente> doBanco = new List<Cliente>();

            foreach (System.Data.DataRow linha in
                ClienteRepositorio.Listar().Rows)
            {
                int id = System.Convert.ToInt32(linha["id"]);

                Cliente cliente =
                    clientes.FirstOrDefault(c => c.IdCliente == id) ??
                    new Cliente { IdCliente = id };

                cliente.Nome = System.Convert.ToString(linha["nome"]);
                cliente.CpfCnpj = TextoOuNulo(linha["cpf_cnpj"]);
                cliente.Telefone = TextoOuNulo(linha["telefone"]);
                cliente.Status = ParaStatus(linha["status"]);

                doBanco.Add(cliente);
            }

            Reconciliar(clientes, doBanco);
        }

        public static void CarregarFuncionarios()
        {
            List<Funcionario> doBanco = new List<Funcionario>();

            foreach (System.Data.DataRow linha in
                FuncionarioRepositorio.Listar().Rows)
            {
                int id = System.Convert.ToInt32(linha["id"]);

                Funcionario funcionario =
                    funcionarios.FirstOrDefault(f => f.IdFuncionario == id) ??
                    new Funcionario { IdFuncionario = id };

                funcionario.Nome = System.Convert.ToString(linha["nome"]);
                funcionario.Unidade = System.Convert.ToString(linha["unidade_nome"]);
                funcionario.Status = ParaStatus(linha["status"]);

                doBanco.Add(funcionario);
            }

            Reconciliar(funcionarios, doBanco);
        }

        public static void CarregarUsuarios()
        {
            List<Usuario> doBanco = new List<Usuario>();

            foreach (System.Data.DataRow linha in
                UsuarioRepositorio.Listar().Rows)
            {
                int id = System.Convert.ToInt32(linha["id"]);

                Usuario usuario =
                    usuarios.FirstOrDefault(u => u.IdUsuario == id) ??
                    new Usuario { IdUsuario = id };

                usuario.Login = System.Convert.ToString(linha["login"]);
                usuario.SenhaHash = System.Convert.ToString(linha["senha_hash"]);
                usuario.Perfil = ParaPerfil(System.Convert.ToString(linha["perfil"]));
                usuario.Unidade = TextoOuNulo(linha["unidade_nome"]);
                usuario.Status = ParaStatus(linha["status"]);

                doBanco.Add(usuario);
            }

            Reconciliar(usuarios, doBanco);

            // O login da sessão passa a ser o mesmo objeto da lista (comparações por referência nas telas).
            if (UsuarioLogado != null)
            {
                Usuario daLista = usuarios.FirstOrDefault(u =>
                    u.IdUsuario == UsuarioLogado.IdUsuario);

                if (daLista != null)
                {
                    UsuarioLogado = daLista;
                }
            }
        }

        // PROVISÓRIO até o Bloco 5 (estoque): os saldos ainda são mantidos em memória
        // pelas telas de venda e estoque; aqui só entra o saldo gravado no banco,
        // uma vez por execução do programa.
        private static void CarregarSaldos()
        {
            if (saldosCarregados)
            {
                return;
            }

            foreach (System.Data.DataRow linha in
                EstoqueRepositorio.Listar().Rows)
            {
                uint produtoId = System.Convert.ToUInt32(linha["produto_id"]);
                uint unidadeId = System.Convert.ToUInt32(linha["unidade_id"]);

                Produto produto = produtos.FirstOrDefault(p => p.Id == produtoId);
                Unidade unidade = unidades.FirstOrDefault(u => u.Id == unidadeId);

                if (produto != null && unidade != null)
                {
                    DefinirSaldoDoProduto(
                        produto,
                        unidade.Nome,
                        System.Convert.ToDecimal(linha["quantidade"]));
                }
            }

            saldosCarregados = true;
        }

        private static string unidadeAtual =
            "Lanchonete Centro";

        // A unidade de trabalho também precisa chegar ao banco (SET @unidade_id),
        // para o log e as regras de unidade; por isso o setter atualiza a sessão.
        public static string UnidadeAtual
        {
            get { return unidadeAtual; }
            set
            {
                unidadeAtual = value;

                if (SessaoBanco.UsuarioId.HasValue)
                {
                    UnidadeResumo unidade =
                        UnidadeRepositorio.PorNome(value);

                    SessaoBanco.UnidadeId =
                        unidade == null
                        ? (uint?)null
                        : unidade.Id;
                }
            }
        }

        public static Usuario UsuarioLogado { get; private set; }

        public static bool LoginEhDeAtendente
        {
            get
            {
                return UsuarioLogado == null ||
                       UsuarioLogado.Perfil == PerfilUsuario.Atendente;
            }
        }

        public static string GerarHashSenha(string senha)
        {
            return ServicoSenha.GerarHash(senha);
        }

        public static bool VerificarSenha(string senha, string senhaHash)
        {
            return ServicoSenha.Verificar(senha, senhaHash);
        }

        private static PerfilUsuario ParaPerfil(string perfil)
        {
            return (PerfilUsuario)System.Enum.Parse(
                typeof(PerfilUsuario),
                perfil);
        }

        // Converte o login lido do banco no Usuario que as telas já usam.
        private static Usuario ParaUsuario(UsuarioAcesso acesso)
        {
            return new Usuario
            {
                IdUsuario = (int)acesso.Id,
                Login = acesso.Login,
                SenhaHash = acesso.SenhaHash,
                Perfil = ParaPerfil(acesso.Perfil),
                Unidade = acesso.UnidadeNome,
                Status = acesso.Ativo
                    ? StatusCadastro.Ativo
                    : StatusCadastro.Inativo
            };
        }

        public static bool Autenticar(
            string login,
            string senha,
            out Usuario usuario,
            out string mensagem)
        {
            usuario = null;
            mensagem = "";

            try
            {
                UsuarioAcesso acesso =
                    UsuarioRepositorio.BuscarPorLogin(login);

                if (acesso == null ||
                    !VerificarSenha(senha, acesso.SenhaHash))
                {
                    mensagem = "Login ou senha inválidos.";
                    return false;
                }

                if (!acesso.Ativo)
                {
                    mensagem = "Usuário inativo. Procure um gerente.";
                    return false;
                }

                // Unidade de trabalho: a do login base (Atendente) ou a
                // primeira unidade ativa (Gerente e Administrador).
                UnidadeResumo unidade =
                    acesso.UnidadeId.HasValue
                    ? new UnidadeResumo
                    {
                        Id = acesso.UnidadeId.Value,
                        Nome = acesso.UnidadeNome
                    }
                    : UnidadeRepositorio.PrimeiraAtiva();

                if (unidade == null)
                {
                    mensagem = "Nenhuma unidade ativa cadastrada.";
                    return false;
                }

                Usuario encontrado = ParaUsuario(acesso);

                usuario = encontrado;
                UsuarioLogado = encontrado;

                SessaoBanco.UsuarioId = acesso.Id;
                SessaoBanco.UnidadeId = unidade.Id;
                unidadeAtual = unidade.Nome;

                LogRepositorio.RegistrarLogin(
                    acesso.Id,
                    acesso.Login);

                // Cadastros (unidades, produtos, clientes...) vêm do banco a cada login.
                CarregarCadastros();

                usuario = UsuarioLogado;
            }
            catch (RegraNegocioException ex)
            {
                usuario = null;
                UsuarioLogado = null;
                SessaoBanco.Limpar();
                mensagem = ex.Message;
                return false;
            }

            RegistrarLog(
                TipoOperacaoLog.Login,
                "Usuario",
                usuario.Login,
                $"Unidade de trabalho: {UnidadeAtual}");

            return true;
        }

        public static void Sair()
        {
            if (UsuarioLogado != null)
            {
                RegistrarLog(
                    TipoOperacaoLog.Logout,
                    "Usuario",
                    UsuarioLogado.Login,
                    null);

                try
                {
                    LogRepositorio.RegistrarLogout(
                        (uint)UsuarioLogado.IdUsuario,
                        UsuarioLogado.Login);
                }
                catch (RegraNegocioException)
                {
                    // sem banco, o programa ainda precisa conseguir sair
                }
            }

            UsuarioLogado = null;
            SessaoBanco.Limpar();
        }

        private static List<LogOperacao> registrosLog =
            new List<LogOperacao>();

        public static void RegistrarLog(
            TipoOperacaoLog operacao,
            string tabela,
            string registro,
            string detalhes)
        {
            registrosLog.Add(
                new LogOperacao(
                    registrosLog.Count + 1,
                    System.DateTime.Now,
                    UsuarioLogado == null
                        ? "sistema"
                        : UsuarioLogado.Login,
                    operacao,
                    tabela,
                    registro,
                    detalhes,
                    UnidadeAtual
                )
            );
        }

        public static void RegistrarLogVenda(Venda venda)
        {
            Caixa caixaDaVenda =
                caixas.FirstOrDefault(c =>
                    c.IdCaixa == venda.IdCaixa);

            RegistrarLog(
                TipoOperacaoLog.Inclusao,
                "Venda",
                $"Venda {venda.IdVenda}",
                JuntarDetalhes(
                    $"Total: {venda.ValorTotal:N2}",
                    $"Pagamento: {venda.FormaPagamento}",
                    venda.NumeroMesa == 0
                        ? "Balcão"
                        : $"Mesa {venda.NumeroMesa}",
                    venda.IdCliente == null
                        ? null
                        : $"Cliente {venda.IdCliente}",
                    caixaDaVenda == null
                        ? null
                        : $"Caixa {caixaDaVenda.IdCaixa} (responsável: {caixaDaVenda.Responsavel})"
                )
            );
        }

        // ---- Estoque (entradas, perdas, inventário) ----
        // Entradas e perdas ficam no banco; estas listas são só um espelho, recarregado
        // do banco no login e sempre que uma tela de estoque consulta. O DRE (Bloco 8)
        // ainda lê daqui.

        public static BindingList<Perda> perdas =
            new BindingList<Perda>();

        private static Unidade UnidadeDeTrabalho()
        {
            Unidade unidade =
                unidades.FirstOrDefault(u => u.Nome == UnidadeAtual);

            if (unidade == null)
            {
                throw new RegraNegocioException(
                    "Unidade de trabalho não encontrada. Entre no sistema novamente.");
            }

            return unidade;
        }

        private static uint IdDoUsuarioLogado()
        {
            if (UsuarioLogado == null)
            {
                throw new RegraNegocioException(
                    "Entre no sistema para continuar.");
            }

            return (uint)UsuarioLogado.IdUsuario;
        }

        private static uint IdDoFuncionario(string nome, string nomeUnidade)
        {
            Funcionario funcionario =
                funcionarios.FirstOrDefault(f =>
                    f.Nome == nome &&
                    f.Unidade == nomeUnidade &&
                    f.Status == StatusCadastro.Ativo);

            if (funcionario == null)
            {
                throw new RegraNegocioException(
                    "Funcionário não encontrado ou inativo nesta unidade.");
            }

            return (uint)funcionario.IdFuncionario;
        }

        /// <summary>Saldo do produto na unidade de trabalho, lido do banco.</summary>
        public static decimal SaldoNoBanco(Produto produto)
        {
            return EstoqueRepositorio.SaldoDe(
                UnidadeDeTrabalho().Id,
                produto.Id);
        }

        /// <summary>
        /// Saldo de todos os produtos da unidade de trabalho, lido do banco
        /// (chave = id do produto; produto sem linha tem saldo 0).
        /// </summary>
        public static Dictionary<uint, decimal> SaldosNoBanco()
        {
            return EstoqueRepositorio.SaldosDaUnidade(
                UnidadeDeTrabalho().Id);
        }

        // PROVISÓRIO até o Bloco 7: a venda ainda usa o saldo em memória; depois de gravar
        // no banco, o saldo daquele produto na memória volta a ser o do banco.
        private static void SincronizarSaldoDaMemoria(
            Produto produto,
            Unidade unidade)
        {
            DefinirSaldoDoProduto(
                produto,
                unidade.Nome,
                EstoqueRepositorio.SaldoDe(unidade.Id, produto.Id));
        }

        public static void CarregarEntradas()
        {
            movimentacoes.Clear();

            foreach (System.Data.DataRow linha in
                EstoqueRepositorio.ListarEntradas().Rows)
            {
                movimentacoes.Add(
                    new movimentoestoque
                    {
                        Produto = System.Convert.ToString(linha["produto"]),
                        Quantidade = System.Convert.ToDecimal(linha["quantidade"]),
                        CustoUnitario = System.Convert.ToDecimal(linha["custo_unitario"]),
                        Fornecedor = TextoOuNulo(linha["fornecedor"]),
                        Responsavel = System.Convert.ToString(linha["responsavel"]),
                        Unidade = System.Convert.ToString(linha["unidade"]),
                        DataEntrada = System.Convert.ToDateTime(linha["data_hora"])
                    });
            }
        }

        public static List<movimentoestoque> EntradasDaUnidade()
        {
            CarregarEntradas();

            return movimentacoes
                .Where(m => m.Unidade == UnidadeAtual)
                .OrderByDescending(m => m.DataEntrada)
                .ToList();
        }

        public static bool RegistrarEntradaEstoque(
            List<movimentoestoque> itens,
            out string mensagem)
        {
            mensagem = "";

            if (itens == null || itens.Count == 0)
            {
                mensagem = "Adicione pelo menos um item à entrada.";
                return false;
            }

            try
            {
                Unidade unidade = UnidadeDeTrabalho();

                string nomeFornecedor = itens[0].Fornecedor;
                string nomeResponsavel = itens[0].Responsavel;

                Fornecedor fornecedor =
                    fornecedores.FirstOrDefault(f =>
                        f.Nome == nomeFornecedor);

                if (fornecedor == null)
                {
                    mensagem = "Fornecedor não encontrado.";
                    return false;
                }

                List<ItemEntradaEstoque> linhas =
                    new List<ItemEntradaEstoque>();

                List<Produto> afetados = new List<Produto>();

                foreach (movimentoestoque item in itens)
                {
                    Produto produto =
                        produtos.FirstOrDefault(p =>
                            p.Nome == item.Produto);

                    if (produto == null)
                    {
                        mensagem = $"Produto não encontrado: {item.Produto}.";
                        return false;
                    }

                    afetados.Add(produto);

                    linhas.Add(
                        new ItemEntradaEstoque
                        {
                            ProdutoId = produto.Id,
                            Quantidade = item.Quantidade,
                            CustoUnitario = item.CustoUnitario
                        });
                }

                EstoqueRepositorio.InserirEntrada(
                    unidade.Id,
                    IdDoUsuarioLogado(),
                    fornecedor.Id,
                    IdDoFuncionario(nomeResponsavel, unidade.Nome),
                    System.DateTime.Now,
                    linhas);

                foreach (Produto produto in afetados.Distinct())
                {
                    SincronizarSaldoDaMemoria(produto, unidade);
                }

                CarregarEntradas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            foreach (movimentoestoque item in itens)
            {
                RegistrarLog(
                    TipoOperacaoLog.Inclusao,
                    "MovimentoEstoque",
                    $"Entrada: {item.Produto}",
                    $"Quantidade: {item.Quantidade:N2}; Custo unitário: {item.CustoUnitario:N2}; Fornecedor: {item.Fornecedor}; Responsável: {item.Responsavel}; Unidade: {item.Unidade}");
            }

            return true;
        }

        public static void CarregarPerdas()
        {
            perdas.Clear();

            foreach (System.Data.DataRow linha in
                EstoqueRepositorio.ListarPerdas().Rows)
            {
                perdas.Add(
                    new Perda
                    {
                        IdPerda = System.Convert.ToInt32(linha["id"]),
                        Data = System.Convert.ToDateTime(linha["data"]),
                        Unidade = System.Convert.ToString(linha["unidade"]),
                        Produto = System.Convert.ToString(linha["produto"]),
                        Quantidade = System.Convert.ToDecimal(linha["quantidade"]),
                        Motivo = (MotivoPerda)System.Enum.Parse(
                            typeof(MotivoPerda),
                            System.Convert.ToString(linha["motivo"])),
                        Responsavel = System.Convert.ToString(linha["responsavel"]),
                        Usuario = System.Convert.ToString(linha["usuario"]),
                        DataHoraRegistro = System.Convert.ToDateTime(linha["data_hora"])
                    });
            }
        }

        public static List<Perda> PerdasDaUnidade(
            string unidade,
            int dias)
        {
            CarregarPerdas();

            System.DateTime inicio =
                System.DateTime.Today.AddDays(-dias);

            return perdas
                .Where(p =>
                    p.Unidade == unidade &&
                    p.Data >= inicio)
                .OrderByDescending(p => p.Data)
                .ThenByDescending(p => p.IdPerda)
                .ToList();
        }

        public static bool RegistrarPerda(
            string nomeProduto,
            decimal quantidade,
            MotivoPerda motivo,
            System.DateTime data,
            string responsavel,
            out string mensagem)
        {
            mensagem = "";

            Produto produto =
                produtos.FirstOrDefault(p =>
                    p.Nome == nomeProduto &&
                    p.Status == StatusCadastro.Ativo);

            if (produto == null)
            {
                mensagem = "Selecione um produto.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(responsavel))
            {
                mensagem = "Selecione o responsável.";
                return false;
            }

            if (quantidade <= 0)
            {
                mensagem = "Informe uma quantidade maior que zero.";
                return false;
            }

            if (data.Date > System.DateTime.Today)
            {
                mensagem = "A data da perda não pode ser futura.";
                return false;
            }

            decimal saldoAnterior;
            decimal saldoAtual;

            try
            {
                Unidade unidade = UnidadeDeTrabalho();

                saldoAnterior = EstoqueRepositorio.SaldoDe(unidade.Id, produto.Id);

                if (quantidade > saldoAnterior)
                {
                    mensagem =
                        $"Quantidade acima do saldo disponível ({saldoAnterior:N2}).";
                    return false;
                }

                EstoqueRepositorio.InserirPerda(
                    unidade.Id,
                    produto.Id,
                    quantidade,
                    motivo.ToString(),
                    data.Date,
                    IdDoUsuarioLogado(),
                    IdDoFuncionario(responsavel, unidade.Nome));

                SincronizarSaldoDaMemoria(produto, unidade);

                saldoAtual = produto.EstoqueAtual;

                CarregarPerdas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            Perda perda =
                perdas
                .OrderByDescending(p => p.IdPerda)
                .FirstOrDefault(p =>
                    p.Produto == produto.Nome &&
                    p.Unidade == UnidadeAtual);

            string textoMotivo =
                perda == null
                ? motivo.ToString()
                : perda.MotivoTexto;

            RegistrarLog(
                TipoOperacaoLog.Inclusao,
                "Perda",
                perda == null
                    ? $"Perda: {produto.Nome}"
                    : $"Perda {perda.IdPerda}: {produto.Nome}",
                $"Quantidade: {quantidade:N2}; Motivo: {textoMotivo}; Data: {data.Date:dd/MM/yyyy}; Responsável: {responsavel}; Unidade: {UnidadeAtual}; Saldo: {saldoAnterior:N2} → {saldoAtual:N2}");

            return true;
        }

        public static List<ItemInventario> MontarItensInventario()
        {
            Dictionary<uint, decimal> saldos = SaldosNoBanco();

            return produtos
                .Where(p => p.Status == StatusCadastro.Ativo)
                .OrderBy(p => p.Nome)
                .Select(p => new ItemInventario
                {
                    Produto = p.Nome,
                    SaldoSistema =
                        saldos.ContainsKey(p.Id)
                        ? saldos[p.Id]
                        : 0m,
                    QuantidadeContada = null,
                    Motivo = null
                })
                .ToList();
        }

        public static bool EfetivarInventario(
            List<ItemInventario> itens,
            string observacao,
            out int quantidadeAjustes,
            out string mensagem,
            string responsavel = null)
        {
            quantidadeAjustes = 0;
            mensagem = "";

            if (UsuarioLogado == null)
            {
                mensagem = "Entre no sistema para efetivar o inventário.";
                return false;
            }

            List<ItemInventario> contados =
                itens == null
                ? new List<ItemInventario>()
                : itens
                    .Where(i => i.QuantidadeContada.HasValue)
                    .ToList();

            if (contados.Count == 0)
            {
                mensagem = "Informe a contagem de pelo menos um produto.";
                return false;
            }

            string responsavelEfetivo =
                string.IsNullOrWhiteSpace(responsavel)
                ? UsuarioLogado.Login
                : responsavel.Trim();

            List<string> linhasLog = new List<string>();
            List<Produto> afetados = new List<Produto>();
            uint inventarioId;
            Unidade unidade;

            try
            {
                unidade = UnidadeDeTrabalho();

                Dictionary<uint, decimal> saldos = SaldosNoBanco();

                List<ItemContagemInventario> linhas =
                    new List<ItemContagemInventario>();

                foreach (ItemInventario item in contados)
                {
                    Produto produto =
                        produtos.FirstOrDefault(p =>
                            p.Nome == item.Produto &&
                            p.Status == StatusCadastro.Ativo);

                    if (produto == null)
                    {
                        mensagem = $"Produto não encontrado: {item.Produto}.";
                        return false;
                    }

                    if (item.QuantidadeContada.Value < 0)
                    {
                        mensagem =
                            $"A contagem de {item.Produto} não pode ser negativa.";
                        return false;
                    }

                    decimal saldoAtual =
                        saldos.ContainsKey(produto.Id)
                        ? saldos[produto.Id]
                        : 0m;

                    decimal diferenca =
                        item.QuantidadeContada.Value - saldoAtual;

                    if (diferenca != 0 &&
                        string.IsNullOrWhiteSpace(item.Motivo))
                    {
                        mensagem =
                            $"Informe o motivo da divergência de {item.Produto}.";
                        return false;
                    }

                    if (diferenca != 0)
                    {
                        quantidadeAjustes++;

                        linhasLog.Add(
                            $"{produto.Nome}: {saldoAtual:N2} → {item.QuantidadeContada.Value:N2} ({item.Motivo.Trim()})");
                    }

                    afetados.Add(produto);

                    linhas.Add(
                        new ItemContagemInventario
                        {
                            ProdutoId = produto.Id,
                            SaldoSistema = saldoAtual,
                            QuantidadeContada = item.QuantidadeContada.Value,
                            Motivo = diferenca == 0
                                ? null
                                : item.Motivo.Trim()
                        });
                }

                inventarioId = EstoqueRepositorio.EfetivarInventario(
                    unidade.Id,
                    IdDoFuncionario(responsavelEfetivo, unidade.Nome),
                    IdDoUsuarioLogado(),
                    observacao,
                    linhas);

                foreach (Produto produto in afetados)
                {
                    SincronizarSaldoDaMemoria(produto, unidade);
                }
            }
            catch (RegraNegocioException ex)
            {
                quantidadeAjustes = 0;
                mensagem = ex.Message;
                return false;
            }

            string observacaoLimpa =
                string.IsNullOrWhiteSpace(observacao)
                ? null
                : observacao.Trim();

            RegistrarLog(
                TipoOperacaoLog.Inclusao,
                "Inventario",
                $"Inventário {inventarioId}",
                JuntarDetalhes(
                    $"Unidade: {unidade.Nome}",
                    $"Responsável: {responsavelEfetivo}",
                    $"Produtos contados: {contados.Count}",
                    $"Ajustes: {quantidadeAjustes}",
                    observacaoLimpa == null
                        ? null
                        : $"Observação: {observacaoLimpa}",
                    linhasLog.Count == 0
                        ? null
                        : string.Join(" | ", linhasLog)
                )
            );

            return true;
        }

        public static DadosPainel MontarPainel(
            string unidade,
            bool incluirAtividade)
        {
            DadosPainel painel = new DadosPainel
            {
                Unidade = unidade
            };

            Caixa caixa =
                caixas.FirstOrDefault(c =>
                    c.Unidade == unidade &&
                    c.Status == "Aberto") ??
                caixas.FirstOrDefault(c =>
                    c.Unidade == unidade &&
                    c.DataAbertura.Date == System.DateTime.Today);

            if (caixa != null)
            {
                painel.CaixaAberto = caixa.Status == "Aberto";

                painel.StatusCaixa =
                    painel.CaixaAberto
                    ? "Aberto"
                    : "Fechado";

                painel.ResponsavelCaixa = caixa.Responsavel;
                painel.AberturaCaixa = caixa.DataAbertura;
                painel.ValorInicialCaixa = caixa.ValorInicial;
                painel.DinheiroEsperado = caixa.DinheiroEsperado;
            }

            ResumoVendas resumo =
                GerarRelatorioVendas(
                    System.DateTime.Today,
                    System.DateTime.Today,
                    unidade,
                    null,
                    null).Resumo;

            painel.VendasHoje = resumo.QuantidadeVendas;
            painel.FaturamentoHoje = resumo.Faturamento;
            painel.TicketMedioHoje = resumo.TicketMedio;
            painel.EmNotinhaHoje = resumo.EmNotinha;

            List<LinhaEstoque> criticos =
                MontarRelatorioEstoque()
                .Where(l =>
                    l.Situacao == "Crítico" &&
                    l.Status == StatusCadastro.Ativo.ToString())
                .OrderBy(l => l.EstoqueAtual - l.EstoqueMinimo)
                .ThenBy(l => l.Produto)
                .ToList();

            painel.TotalCriticos = criticos.Count;

            painel.Criticos =
                criticos
                .Take(5)
                .Select(l => new LinhaCriticoPainel
                {
                    Produto = l.Produto,
                    Saldo = l.EstoqueAtual,
                    Minimo = l.EstoqueMinimo
                })
                .ToList();

            foreach (Cliente cliente in clientes)
            {
                decimal saldo = SaldoNotinha(cliente.IdCliente);

                if (saldo > 0)
                {
                    painel.NotinhaTotal += saldo;
                    painel.NotinhaClientes++;
                }
            }

            painel.UltimasVendas =
                vendas
                .Where(v =>
                    caixas.Any(c =>
                        c.IdCaixa == v.IdCaixa &&
                        c.Unidade == unidade))
                .OrderByDescending(v => v.DataHora)
                .ThenByDescending(v => v.IdVenda)
                .Take(5)
                .Select(v => new LinhaVendaPainel
                {
                    Venda = v.IdVenda,
                    DataHora = v.DataHora,
                    Total = v.ValorTotal,
                    Status = v.Status
                })
                .ToList();

            if (incluirAtividade)
            {
                painel.Atividade =
                    ConsultarLog(null, null, null, null, null)
                    .Take(5)
                    .Select(l => new LinhaAtividadePainel
                    {
                        DataHora = l.DataHora,
                        Usuario = l.Usuario,
                        Operacao = l.OperacaoTexto,
                        Registro = l.Registro
                    })
                    .ToList();
            }

            return painel;
        }

        public static List<string> UsuariosDoLog()
        {
            return registrosLog
                .Where(l => l.Unidade == UnidadeAtual)
                .Select(l => l.Usuario)
                .Concat(usuarios
                    .Where(u =>
                        u.Unidade == null ||
                        u.Unidade == UnidadeAtual)
                    .Select(u => u.Login))
                .Distinct()
                .OrderBy(nome => nome)
                .ToList();
        }

        public static List<string> TabelasDoLog()
        {
            return registrosLog
                .Where(l => l.Unidade == UnidadeAtual)
                .Select(l => l.Tabela)
                .Distinct()
                .OrderBy(nome => nome)
                .ToList();
        }

        public static string Alteracao(
            string campo,
            string antes,
            string depois)
        {
            return antes == depois
                ? null
                : $"{campo}: {antes} → {depois}";
        }

        public static string JuntarDetalhes(params string[] partes)
        {
            string[] preenchidas =
                partes
                .Where(parte => !string.IsNullOrWhiteSpace(parte))
                .ToArray();

            return preenchidas.Length == 0
                ? null
                : string.Join("; ", preenchidas);
        }

        public static List<LogOperacao> ConsultarLog(
            System.DateTime? inicio,
            System.DateTime? fim,
            string usuario,
            TipoOperacaoLog? operacao,
            string tabela)
        {
            System.DateTime dataInicial;
            System.DateTime dataFinal;

            DefinirPeriodoPadrao(
                inicio,
                fim,
                out dataInicial,
                out dataFinal
            );

            return registrosLog
                .Where(l =>
                    l.Unidade == UnidadeAtual &&
                    l.DataHora.Date >= dataInicial &&
                    l.DataHora.Date <= dataFinal &&
                    (string.IsNullOrEmpty(usuario) ||
                        l.Usuario == usuario) &&
                    (operacao == null || l.Operacao == operacao) &&
                    (string.IsNullOrEmpty(tabela) ||
                        l.Tabela == tabela))
                .OrderByDescending(l => l.DataHora)
                .ThenByDescending(l => l.IdLog)
                .ToList();
        }

        public static List<string> NomesFuncionariosAtivos(
            string nomeUnidade)
        {
            return funcionarios
                .Where(f =>
                    f.Status == StatusCadastro.Ativo &&
                    f.Unidade == nomeUnidade)
                .Select(f => f.Nome)
                .OrderBy(nome => nome)
                .ToList();
        }

        public static bool FuncionarioPossuiVinculos(
            Funcionario funcionario)
        {
            return funcionario != null &&
                   (FuncionarioRepositorio.PossuiVinculos(
                        (uint)funcionario.IdFuncionario) ||
                    caixas.Any(c =>
                       c.Responsavel == funcionario.Nome &&
                       c.Unidade == funcionario.Unidade) ||
                    movimentacoes.Any(m =>
                       m.Responsavel == funcionario.Nome &&
                       m.Unidade == funcionario.Unidade));
        }

        public static Caixa CaixaAberto()
        {
            return caixas.FirstOrDefault(c =>
                c.Unidade == UnidadeAtual &&
                c.Status == "Aberto");
        }

        public static Caixa CaixaDeHoje()
        {
            return caixas.FirstOrDefault(c =>
                c.Unidade == UnidadeAtual &&
                c.DataAbertura.Date == System.DateTime.Today);
        }

        // O caixa fica no banco; a lista "caixas" é um espelho recarregado do banco no login,
        // ao abrir a tela do caixa e depois de cada gravação (as vendas e a DRE ainda leem daqui).
        public static void CarregarCaixas()
        {
            caixas.Clear();

            foreach (System.Data.DataRow linha in
                CaixaRepositorio.Listar().Rows)
            {
                caixas.Add(
                    new Caixa
                    {
                        IdCaixa = System.Convert.ToInt32(linha["id"]),
                        Unidade = System.Convert.ToString(linha["unidade"]),
                        Responsavel = System.Convert.ToString(linha["responsavel"]),
                        DataAbertura = System.Convert.ToDateTime(linha["data_abertura"]),
                        DataFechamento =
                            linha["data_fechamento"] == System.DBNull.Value
                            ? (System.DateTime?)null
                            : System.Convert.ToDateTime(linha["data_fechamento"]),
                        ValorInicial = System.Convert.ToDecimal(linha["valor_inicial"]),
                        ValorContado =
                            linha["valor_contado_dinheiro"] == System.DBNull.Value
                            ? (decimal?)null
                            : System.Convert.ToDecimal(linha["valor_contado_dinheiro"]),
                        ValorMaquininha =
                            linha["valor_contado_maquininha"] == System.DBNull.Value
                            ? (decimal?)null
                            : System.Convert.ToDecimal(linha["valor_contado_maquininha"]),
                        Status = System.Convert.ToString(linha["status"])
                    });
            }
        }

        public static bool AbrirCaixa(
            string responsavel,
            decimal valorInicial,
            out string mensagem)
        {
            mensagem = "";

            try
            {
                Unidade unidade = UnidadeDeTrabalho();

                uint idCaixa = CaixaRepositorio.Abrir(
                    unidade.Id,
                    IdDoFuncionario(responsavel, unidade.Nome),
                    valorInicial);

                CarregarCaixas();

                RegistrarLog(
                    TipoOperacaoLog.Inclusao,
                    "Caixa",
                    $"Caixa {idCaixa}",
                    $"Responsável: {responsavel}; Valor inicial: {valorInicial:N2}; Unidade: {unidade.Nome}");
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            return true;
        }

        public static bool FecharCaixa(
            Caixa caixa,
            decimal valorContado,
            decimal valorMaquininha,
            out string mensagem)
        {
            mensagem = "";

            if (caixa == null)
            {
                mensagem = "Não há caixa aberto para fechar.";
                return false;
            }

            decimal dinheiroEsperado = caixa.DinheiroEsperado;
            decimal maquininhaEsperada = caixa.MaquininhaEsperada;

            try
            {
                CaixaRepositorio.Fechar(
                    (uint)caixa.IdCaixa,
                    valorContado,
                    valorMaquininha);

                CarregarCaixas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            RegistrarLog(
                TipoOperacaoLog.Alteracao,
                "Caixa",
                $"Caixa {caixa.IdCaixa}",
                $"Status: Aberto → Fechado; Valor contado: {valorContado:N2}; Dinheiro esperado: {dinheiroEsperado:N2}; Diferença: {valorContado - dinheiroEsperado:N2}; Maquininha informada: {valorMaquininha:N2}; Maquininha esperada: {maquininhaEsperada:N2}; Diferença maquininha: {valorMaquininha - maquininhaEsperada:N2}");

            return true;
        }

        public static bool EditarCaixa(
            Caixa caixa,
            decimal novoValorInicial,
            string novoResponsavel,
            string motivo,
            string autorizadoPor,
            out string mensagem)
        {
            mensagem = "";

            if (caixa == null)
            {
                mensagem = "Não há caixa aberto para editar.";
                return false;
            }

            if (caixa.Status != "Aberto")
            {
                mensagem = "Somente um caixa aberto pode ser editado.";
                return false;
            }

            if (caixa.Unidade != UnidadeAtual)
            {
                mensagem = "Este caixa pertence a outra unidade.";
                return false;
            }

            if (novoValorInicial < 0)
            {
                mensagem = "O valor inicial não pode ser negativo.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(novoResponsavel) ||
                !NomesFuncionariosAtivos(caixa.Unidade)
                    .Contains(novoResponsavel))
            {
                mensagem = "Selecione um responsável ativo desta unidade.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(motivo))
            {
                mensagem = "Informe o motivo da edição.";
                return false;
            }

            if (novoValorInicial == caixa.ValorInicial &&
                novoResponsavel == caixa.Responsavel)
            {
                mensagem = "Nenhuma alteração foi feita.";
                return false;
            }

            string detalhes =
                JuntarDetalhes(
                    Alteracao(
                        "Valor inicial",
                        caixa.ValorInicial.ToString("N2"),
                        novoValorInicial.ToString("N2")),
                    Alteracao(
                        "Responsável",
                        caixa.Responsavel,
                        novoResponsavel),
                    $"Motivo: {motivo.Trim()}",
                    $"Autorizado por: {autorizadoPor}");

            try
            {
                UsuarioAcesso autorizador =
                    UsuarioRepositorio.BuscarPorLogin(autorizadoPor);

                if (autorizador == null)
                {
                    mensagem = "Autorizador não encontrado.";
                    return false;
                }

                CaixaRepositorio.Editar(
                    (uint)caixa.IdCaixa,
                    novoValorInicial,
                    IdDoFuncionario(novoResponsavel, caixa.Unidade),
                    motivo.Trim(),
                    autorizador.Id);

                CarregarCaixas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            RegistrarLog(
                TipoOperacaoLog.Alteracao,
                "Caixa",
                $"Caixa {caixa.IdCaixa}",
                detalhes);

            return true;
        }

        public static decimal TotalRecebidoPorForma(
            int idCaixa,
            string formaPagamento)
        {
            IEnumerable<Venda> vendasDoCaixa =
                vendas.Where(v =>
                    v.IdCaixa == idCaixa &&
                    v.Status == "Concluída");

            decimal total =
                vendasDoCaixa
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento == formaPagamento)
                .Sum(p => p.Valor);

            if (formaPagamento == "Dinheiro")
            {
                total -= vendasDoCaixa.Sum(v => v.Troco);
            }

            total += abatimentos
                .Where(a =>
                    a.IdCaixa == idCaixa &&
                    a.FormaPagamento == formaPagamento)
                .Sum(a => a.Valor);

            return total;
        }

        public static decimal TotalVendasDoCaixa(int idCaixa)
        {
            IEnumerable<Venda> vendasDoCaixa =
                vendas.Where(v =>
                    v.IdCaixa == idCaixa &&
                    v.Status == "Concluída");

            return vendasDoCaixa
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento != "Notinha")
                .Sum(p => p.Valor)
                - vendasDoCaixa.Sum(v => v.Troco);
        }

        public static decimal TotalNotinhaDoCaixa(int idCaixa)
        {
            return vendas
                .Where(v =>
                    v.IdCaixa == idCaixa &&
                    v.Status == "Concluída")
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento == "Notinha")
                .Sum(p => p.Valor);
        }

        public static decimal TotalAbatimentosDoCaixa(int idCaixa)
        {
            return abatimentos
                .Where(a => a.IdCaixa == idCaixa)
                .Sum(a => a.Valor);
        }

        public static decimal SaldoDoProduto(
            Produto produto,
            string unidade)
        {
            SaldoEstoque saldo =
                saldosestoque.FirstOrDefault(s =>
                    s.Produto == produto &&
                    s.Unidade == unidade);

            return saldo == null
                ? 0m
                : saldo.Quantidade;
        }

        public static void DefinirSaldoDoProduto(
            Produto produto,
            string unidade,
            decimal quantidade)
        {
            SaldoEstoque saldo =
                saldosestoque.FirstOrDefault(s =>
                    s.Produto == produto &&
                    s.Unidade == unidade);

            if (saldo == null)
            {
                saldosestoque.Add(
                    new SaldoEstoque
                    {
                        Produto = produto,
                        Unidade = unidade,
                        Quantidade = quantidade
                    });

                return;
            }

            saldo.Quantidade = quantidade;
        }

        public static void RemoverSaldosDoProduto(Produto produto)
        {
            foreach (SaldoEstoque saldo in
                saldosestoque
                .Where(s => s.Produto == produto)
                .ToList())
            {
                saldosestoque.Remove(saldo);
            }
        }

        public static List<Comanda> ComandasDaUnidade()
        {
            return comandas
                .Where(c => c.Unidade == UnidadeAtual)
                .ToList();
        }

        public static Comanda ComandaDaMesa(int numeroMesa)
        {
            return comandas.FirstOrDefault(c =>
                c.Unidade == UnidadeAtual &&
                c.NumeroMesa == numeroMesa);
        }

        public static int ProximoIdNotinha()
        {
            return notinhas.Any()
                ? notinhas.Max(n => n.IdNotinha) + 1
                : 1;
        }

        public static int ProximoIdAbatimento()
        {
            return abatimentos.Any()
                ? abatimentos.Max(a => a.IdAbatimento) + 1
                : 1;
        }

        public static decimal SaldoNotinha(int idCliente)
        {
            return notinhas
                       .Where(n => n.IdCliente == idCliente && n.Unidade == UnidadeAtual)
                       .Sum(n => n.Valor)
                   - abatimentos
                       .Where(a => a.IdCliente == idCliente && a.Unidade == UnidadeAtual)
                       .Sum(a => a.Valor);
        }

        public static void LancarNotinhaDaVenda(Venda venda)
        {
            if (venda == null || venda.IdCliente == null)
            {
                return;
            }

            decimal valorNotinha =
                venda.Pagamentos
                .Where(pagamento =>
                    pagamento.FormaPagamento == "Notinha")
                .Sum(pagamento => pagamento.Valor);

            if (valorNotinha <= 0)
            {
                return;
            }

            notinhas.Add(
                new Notinha
                {
                    IdNotinha = ProximoIdNotinha(),
                    IdCliente = venda.IdCliente.Value,
                    IdVenda = venda.IdVenda,
                    DataHora = venda.DataHora,
                    Valor = valorNotinha,
                    Unidade = UnidadeAtual
                }
            );
        }

        public static bool AutorizarGerente(
            string login,
            string senha,
            out Usuario autorizador,
            out string mensagem)
        {
            autorizador = null;
            mensagem = "";

            UsuarioAcesso acesso;

            try
            {
                acesso = UsuarioRepositorio.BuscarPorLogin(login);
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            if (acesso == null ||
                !VerificarSenha(senha, acesso.SenhaHash))
            {
                mensagem = "Login ou senha inválidos.";
                return false;
            }

            if (!acesso.Ativo)
            {
                mensagem = "Usuário inativo.";
                return false;
            }

            Usuario encontrado = ParaUsuario(acesso);

            if (encontrado.Perfil == PerfilUsuario.Atendente)
            {
                mensagem = "Este login não tem permissão para autorizar.";
                return false;
            }

            autorizador = encontrado;

            return true;
        }

        public static bool CancelarVenda(
            Venda venda,
            string motivo,
            string autorizadoPor,
            out string mensagem)
        {
            mensagem = "";

            if (venda == null)
            {
                mensagem = "Selecione uma venda.";
                return false;
            }

            if (venda.Status != "Concluída")
            {
                mensagem = "Somente vendas concluídas podem ser canceladas.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(motivo))
            {
                mensagem = "Informe o motivo do cancelamento.";
                return false;
            }

            Caixa caixaDaVenda =
                caixas.FirstOrDefault(c =>
                    c.IdCaixa == venda.IdCaixa);

            if (caixaDaVenda == null ||
                caixaDaVenda.Status != "Aberto")
            {
                mensagem =
                    "Cancelamento bloqueado: o caixa desta venda já foi fechado.";
                return false;
            }

            if (caixaDaVenda.Unidade != UnidadeAtual)
            {
                mensagem =
                    "Cancelamento bloqueado: esta venda pertence a outra unidade.";
                return false;
            }

            Notinha notinhaDaVenda =
                notinhas.FirstOrDefault(n =>
                    n.IdVenda == venda.IdVenda);

            if (notinhaDaVenda != null &&
                SaldoNotinha(notinhaDaVenda.IdCliente) -
                notinhaDaVenda.Valor < 0)
            {
                mensagem =
                    "Cancelamento bloqueado: o cliente já abateu parte da Notinha desta venda.";
                return false;
            }

            foreach (ItemVenda item in venda.Itens)
            {
                Produto produto =
                    produtos.FirstOrDefault(p =>
                        p.Nome == item.Produto);

                if (produto == null)
                {
                    continue;
                }

                produto.EstoqueAtual += item.Quantidade;

                RegistrarHistoricoEstoque(
                    produto,
                    TipoMovimentoEstoque.Estorno,
                    item.Quantidade,
                    autorizadoPor,
                    $"Venda {venda.IdVenda} cancelada"
                );
            }

            if (notinhaDaVenda != null)
            {
                notinhas.Remove(notinhaDaVenda);
            }

            venda.Status = "Cancelada";
            venda.MotivoCancelamento = motivo.Trim();
            venda.CanceladaPor = autorizadoPor;
            venda.DataCancelamento = System.DateTime.Now;

            RegistrarLog(
                TipoOperacaoLog.Alteracao,
                "Venda",
                $"Venda {venda.IdVenda}",
                $"Status: Concluída → Cancelada; Motivo: {venda.MotivoCancelamento}; Autorizado por: {autorizadoPor}; Total: {venda.ValorTotal:N2}");

            return true;
        }

        public static List<LinhaEstoque> MontarRelatorioEstoque()
        {
            return MontarRelatorioEstoque(UnidadeAtual);
        }

        // Saldos e custos vêm do banco. Com unidade vazia, soma todas as unidades.
        public static List<LinhaEstoque> MontarRelatorioEstoque(
            string unidade)
        {
            List<LinhaEstoque> linhas = new List<LinhaEstoque>();

            bool todas = string.IsNullOrEmpty(unidade);

            int quantidadeUnidades =
                System.Math.Max(1, unidades.Count);

            Unidade unidadeEscolhida =
                todas
                ? null
                : unidades.FirstOrDefault(u => u.Nome == unidade);

            Dictionary<string, decimal> saldos =
                new Dictionary<string, decimal>();

            foreach (System.Data.DataRow linha in
                EstoqueRepositorio.Listar().Rows)
            {
                saldos[
                    System.Convert.ToString(linha["unidade_id"]) + ":" +
                    System.Convert.ToString(linha["produto_id"])] =
                    System.Convert.ToDecimal(linha["quantidade"]);
            }

            Dictionary<uint, decimal> custosGerais =
                EstoqueRepositorio.UltimosCustos(null);

            Dictionary<uint, decimal> custosDaUnidade =
                unidadeEscolhida == null
                ? new Dictionary<uint, decimal>()
                : EstoqueRepositorio.UltimosCustos(unidadeEscolhida.Id);

            foreach (Produto produto in produtos)
            {
                decimal saldo = 0m;

                foreach (Unidade u in unidades)
                {
                    if (!todas && u != unidadeEscolhida)
                    {
                        continue;
                    }

                    decimal saldoDaUnidade;

                    if (saldos.TryGetValue(u.Id + ":" + produto.Id, out saldoDaUnidade))
                    {
                        saldo += saldoDaUnidade;
                    }
                }

                decimal minimo =
                    todas
                    ? produto.EstoqueMinimo * quantidadeUnidades
                    : produto.EstoqueMinimo;

                decimal custo;

                decimal? ultimoCusto =
                    custosDaUnidade.TryGetValue(produto.Id, out custo)
                        ? custo
                        : (custosGerais.TryGetValue(produto.Id, out custo)
                            ? custo
                            : (decimal?)null);

                linhas.Add(
                    new LinhaEstoque
                    {
                        Produto = produto.Nome,
                        Categoria = produto.Categoria,
                        UnidadeMedida = produto.UnidadeMedida,
                        EstoqueAtual = saldo,
                        EstoqueMinimo = minimo,
                        Situacao =
                            saldo <= minimo
                                ? "Crítico"
                                : "Normal",
                        PrecoVenda = produto.Preco,
                        UltimoCusto = ultimoCusto,
                        ValorACusto =
                            saldo * (ultimoCusto ?? 0m),
                        ValorAPrecoVenda =
                            saldo * produto.Preco,
                        Status = produto.Status.ToString()
                    }
                );
            }

            return linhas;
        }
        public static void RegistrarHistoricoEstoque(
            Produto produto,
            TipoMovimentoEstoque tipo,
            decimal quantidade,
            string responsavel,
            string origem
        )
        {
            historicoestoque.Add(
                new HistoricoEstoque
                {
                    Data = System.DateTime.Now,
                    Produto = produto.Nome,
                    Tipo = tipo,
                    Quantidade = quantidade,
                    SaldoApos = produto.EstoqueAtual,
                    Unidade = UnidadeAtual,
                    Responsavel = responsavel,
                    Origem = origem
                }
            );
        }
        private static decimal? CustoDaPerda(Perda perda)
        {
            System.DateTime dia = perda.Data.Date;

            var candidatas = movimentacoes
                .Select((m, posicao) => new { m, posicao })
                .Where(x =>
                    x.m.Produto == perda.Produto &&
                    x.m.DataEntrada.Date <= dia)
                .ToList();

            var daUnidade = candidatas
                .Where(x => x.m.Unidade == perda.Unidade)
                .OrderByDescending(x => x.m.DataEntrada)
                .ThenByDescending(x => x.posicao)
                .FirstOrDefault();

            var escolhida = daUnidade ?? candidatas
                .OrderByDescending(x => x.m.DataEntrada)
                .ThenByDescending(x => x.posicao)
                .FirstOrDefault();

            return escolhida == null
                ? (decimal?)null
                : escolhida.m.CustoUnitario;
        }

        private static DreUnidade CalcularDreDaUnidade(
            string unidade,
            System.DateTime dataInicial,
            System.DateTime dataFinal)
        {
            ResumoVendas resumo =
                GerarRelatorioVendas(
                    dataInicial,
                    dataFinal,
                    unidade,
                    null,
                    null).Resumo;

            DreUnidade dre = new DreUnidade
            {
                Unidade = unidade,
                Receita = resumo.Faturamento,
                Recebido = resumo.Recebido,
                VendidoEmNotinha = resumo.EmNotinha
            };

            dre.Abatimentos =
                abatimentos
                .Where(a =>
                    a.Unidade == unidade &&
                    a.DataHora.Date >= dataInicial &&
                    a.DataHora.Date <= dataFinal)
                .Sum(a => a.Valor);

            dre.Despesas =
                movimentacoes
                .Where(m =>
                    m.Unidade == unidade &&
                    m.DataEntrada.Date >= dataInicial &&
                    m.DataEntrada.Date <= dataFinal)
                .Sum(m => m.Quantidade * m.CustoUnitario);

            foreach (Perda perda in perdas.Where(p =>
                p.Unidade == unidade &&
                p.Data.Date >= dataInicial &&
                p.Data.Date <= dataFinal))
            {
                decimal? custo = CustoDaPerda(perda);

                if (custo == null)
                {
                    dre.PerdasSemCusto++;
                    continue;
                }

                dre.Perdas += perda.Quantidade * custo.Value;
            }

            return dre;
        }

        public static RelatorioDre GerarDre(
            System.DateTime? inicio,
            System.DateTime? fim,
            string unidade,
            out string mensagem)
        {
            mensagem = "";

            System.DateTime dataInicial;
            System.DateTime dataFinal;

            DefinirPeriodoPadrao(
                inicio,
                fim,
                out dataInicial,
                out dataFinal
            );

            if (dataInicial > dataFinal)
            {
                mensagem =
                    "A data inicial não pode ser maior que a data final.";

                return null;
            }

            RelatorioDre relatorio = new RelatorioDre
            {
                DataInicial = dataInicial,
                DataFinal = dataFinal,
                Consolidado = string.IsNullOrEmpty(unidade)
            };

            List<string> nomes =
                relatorio.Consolidado
                ? unidades.Select(u => u.Nome).OrderBy(n => n).ToList()
                : new List<string> { unidade };

            foreach (string nome in nomes)
            {
                relatorio.Unidades.Add(
                    CalcularDreDaUnidade(nome, dataInicial, dataFinal));
            }

            DreUnidade total = relatorio.Total;

            total.Receita = relatorio.Unidades.Sum(u => u.Receita);
            total.Recebido = relatorio.Unidades.Sum(u => u.Recebido);
            total.VendidoEmNotinha = relatorio.Unidades.Sum(u => u.VendidoEmNotinha);
            total.Abatimentos = relatorio.Unidades.Sum(u => u.Abatimentos);
            total.Despesas = relatorio.Unidades.Sum(u => u.Despesas);
            total.Perdas = relatorio.Unidades.Sum(u => u.Perdas);
            total.PerdasSemCusto = relatorio.Unidades.Sum(u => u.PerdasSemCusto);

            return relatorio;
        }

        public static void DefinirPeriodoPadrao(
            System.DateTime? inicio,
            System.DateTime? fim,
            out System.DateTime dataInicial,
            out System.DateTime dataFinal
        )
        {
            dataFinal = (fim ?? System.DateTime.Today).Date;
            dataInicial = (inicio ?? dataFinal.AddDays(-30)).Date;
        }

        public static List<HistoricoEstoque> ConsultarHistoricoEstoque(
            System.DateTime? inicio,
            System.DateTime? fim,
            string unidade,
            string produto,
            TipoMovimentoEstoque? tipo
        )
        {
            System.DateTime dataInicial;
            System.DateTime dataFinal;

            DefinirPeriodoPadrao(
                inicio,
                fim,
                out dataInicial,
                out dataFinal
            );

            uint? unidadeId = null;
            uint? produtoId = null;

            if (!string.IsNullOrEmpty(unidade))
            {
                Unidade encontrada =
                    unidades.FirstOrDefault(u => u.Nome == unidade);

                if (encontrada == null)
                {
                    return new List<HistoricoEstoque>();
                }

                unidadeId = encontrada.Id;
            }

            if (!string.IsNullOrEmpty(produto))
            {
                Produto encontrado =
                    produtos.FirstOrDefault(p => p.Nome == produto);

                if (encontrado == null)
                {
                    return new List<HistoricoEstoque>();
                }

                produtoId = encontrado.Id;
            }

            List<HistoricoEstoque> resultado = new List<HistoricoEstoque>();

            foreach (System.Data.DataRow linha in
                EstoqueRepositorio.ConsultarMovimentacoes(
                    dataInicial,
                    dataFinal,
                    unidadeId,
                    produtoId,
                    tipo == null ? null : tipo.ToString()).Rows)
            {
                TipoMovimentoEstoque tipoLinha =
                    (TipoMovimentoEstoque)System.Enum.Parse(
                        typeof(TipoMovimentoEstoque),
                        System.Convert.ToString(linha["tipo"]));

                decimal quantidade =
                    System.Convert.ToDecimal(linha["quantidade"]);

                resultado.Add(
                    new HistoricoEstoque
                    {
                        Data = System.Convert.ToDateTime(linha["data_hora"]),
                        Produto = System.Convert.ToString(linha["produto"]),
                        Tipo = tipoLinha,
                        // No extrato do banco a saída vem negativa; aqui só o ajuste mostra o sinal.
                        Quantidade =
                            tipoLinha == TipoMovimentoEstoque.Ajuste
                            ? quantidade
                            : System.Math.Abs(quantidade),
                        SaldoApos = System.Convert.ToDecimal(linha["saldo_apos"]),
                        Unidade = System.Convert.ToString(linha["unidade"]),
                        Responsavel = TextoOuNulo(linha["login"]),
                        Origem = OrigemDoMovimento(tipoLinha)
                    });
            }

            // PROVISÓRIO até o Bloco 7: vendas e estornos ainda são anotados em memória.
            resultado.AddRange(
                historicoestoque
                .Where(h =>
                    (h.Tipo == TipoMovimentoEstoque.Venda ||
                     h.Tipo == TipoMovimentoEstoque.Estorno) &&
                    h.Data.Date >= dataInicial &&
                    h.Data.Date <= dataFinal &&
                    (string.IsNullOrEmpty(unidade) ||
                        h.Unidade == unidade) &&
                    (string.IsNullOrEmpty(produto) ||
                        h.Produto == produto) &&
                    (tipo == null || h.Tipo == tipo)));

            return resultado
                .OrderByDescending(h => h.Data)
                .ToList();
        }

        private static string OrigemDoMovimento(TipoMovimentoEstoque tipo)
        {
            switch (tipo)
            {
                case TipoMovimentoEstoque.Entrada:
                    return "Entrada de estoque";

                case TipoMovimentoEstoque.Perda:
                    return "Perda";

                case TipoMovimentoEstoque.Ajuste:
                    return "Inventário";

                case TipoMovimentoEstoque.Estorno:
                    return "Cancelamento de venda";

                default:
                    return "Venda";
            }
        }
        public static string SomenteDigitos(string texto)
        {
            return new string(
                (texto ?? "")
                    .Where(char.IsDigit)
                    .ToArray());
        }

        private static string AplicarPadrao(string digitos, string padrao)
        {
            System.Text.StringBuilder resultado =
                new System.Text.StringBuilder();

            int posicao = 0;

            foreach (char simbolo in padrao)
            {
                if (posicao >= digitos.Length)
                {
                    break;
                }

                if (simbolo == '0')
                {
                    resultado.Append(digitos[posicao]);
                    posicao++;
                }
                else
                {
                    resultado.Append(simbolo);
                }
            }

            return resultado.ToString();
        }

        public static string FormatarCnpj(string digitos)
        {
            return AplicarPadrao(digitos, "00.000.000/0000-00");
        }

        public static string FormatarCpfCnpj(string digitos)
        {
            return digitos.Length > 11
                ? FormatarCnpj(digitos)
                : AplicarPadrao(digitos, "000.000.000-00");
        }

        public static string FormatarTelefone(string digitos)
        {
            return digitos.Length > 10
                ? AplicarPadrao(digitos, "(00) 00000-0000")
                : AplicarPadrao(digitos, "(00) 0000-0000");
        }

        public static void AplicarMascara(
            System.Windows.Forms.TextBox caixa,
            System.Func<string, string> formatar,
            int maximoDigitos)
        {
            string anterior = caixa.Text;
            bool aplicando = false;

            caixa.TextChanged += (origem, evento) =>
            {
                if (aplicando)
                {
                    return;
                }

                string digitos = SomenteDigitos(caixa.Text);

                if (caixa.Text.Length < anterior.Length &&
                    digitos == SomenteDigitos(anterior) &&
                    digitos.Length > 0)
                {
                    digitos = digitos.Substring(0, digitos.Length - 1);
                }

                if (digitos.Length > maximoDigitos)
                {
                    digitos = digitos.Substring(0, maximoDigitos);
                }

                string novo = formatar(digitos);

                if (novo != caixa.Text)
                {
                    aplicando = true;
                    caixa.Text = novo;
                    caixa.SelectionStart = novo.Length;
                    aplicando = false;
                }

                anterior = caixa.Text;
            };
        }

        public static string CampoCsv(string valor)
        {
            if (valor == null)
            {
                return "";
            }

            if (valor.Contains(";") ||
                valor.Contains("\"") ||
                valor.Contains("\n"))
            {
                return "\"" + valor.Replace("\"", "\"\"") + "\"";
            }

            return valor;
        }
        public static readonly string[] FormasPagamento =
        {
            "Dinheiro",
            "Pix",
            "Crédito",
            "Débito",
            "Notinha"
        };

        public static RelatorioVendas GerarRelatorioVendas(
            System.DateTime? inicio,
            System.DateTime? fim,
            string unidade,
            string formaPagamento,
            string produto
        )
        {
            System.DateTime dataInicial;
            System.DateTime dataFinal;

            DefinirPeriodoPadrao(
                inicio,
                fim,
                out dataInicial,
                out dataFinal
            );

            IEnumerable<Venda> vendasFiltradas =
                vendas.Where(v =>
                    v.Status == "Concluída" &&
                    v.DataHora.Date >= dataInicial &&
                    v.DataHora.Date <= dataFinal);

            if (!string.IsNullOrEmpty(unidade))
            {
                vendasFiltradas =
                    vendasFiltradas.Where(v =>
                        caixas.Any(c =>
                            c.IdCaixa == v.IdCaixa &&
                            c.Unidade == unidade));
            }

            if (!string.IsNullOrEmpty(formaPagamento))
            {
                vendasFiltradas =
                    vendasFiltradas.Where(v =>
                        v.Pagamentos.Any(p =>
                            p.FormaPagamento == formaPagamento));
            }

            if (!string.IsNullOrEmpty(produto))
            {
                vendasFiltradas =
                    vendasFiltradas.Where(v =>
                        v.Itens.Any(i => i.Produto == produto));
            }

            List<Venda> vendasDoRelatorio =
                vendasFiltradas.ToList();

            RelatorioVendas relatorio = new RelatorioVendas
            {
                DataInicial = dataInicial,
                DataFinal = dataFinal
            };

            decimal faturamento =
                vendasDoRelatorio.Sum(v => v.ValorTotal);

            decimal emNotinha =
                vendasDoRelatorio
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento == "Notinha")
                .Sum(p => p.Valor);

            decimal recebido =
                vendasDoRelatorio
                .SelectMany(v => v.Pagamentos)
                .Where(p => p.FormaPagamento != "Notinha")
                .Sum(p => p.Valor)
                - vendasDoRelatorio.Sum(v => v.Troco);

            relatorio.Resumo = new ResumoVendas
            {
                QuantidadeVendas = vendasDoRelatorio.Count,
                Faturamento = faturamento,
                Recebido = recebido,
                EmNotinha = emNotinha,
                TicketMedio =
                    vendasDoRelatorio.Count > 0
                    ? faturamento / vendasDoRelatorio.Count
                    : 0m
            };

            for (System.DateTime dia = dataInicial;
                dia <= dataFinal;
                dia = dia.AddDays(1))
            {
                System.DateTime diaAtual = dia;

                List<Venda> vendasDoDia =
                    vendasDoRelatorio
                    .Where(v => v.DataHora.Date == diaAtual)
                    .ToList();

                relatorio.PorDia.Add(
                    new LinhaFaturamentoDia
                    {
                        Dia = diaAtual,
                        QuantidadeVendas = vendasDoDia.Count,
                        Faturamento = vendasDoDia.Sum(v => v.ValorTotal)
                    }
                );
            }

            IEnumerable<ItemVenda> itensVendidos =
                vendasDoRelatorio.SelectMany(v => v.Itens);

            if (!string.IsNullOrEmpty(produto))
            {
                itensVendidos =
                    itensVendidos.Where(i => i.Produto == produto);
            }

            relatorio.Produtos =
                itensVendidos
                .GroupBy(i => i.Produto)
                .Select(g => new LinhaProdutoVendido
                {
                    Produto = g.Key,
                    Quantidade = g.Sum(i => i.Quantidade),
                    Receita = g.Sum(i => i.Total)
                })
                .OrderByDescending(l => l.Quantidade)
                .ThenByDescending(l => l.Receita)
                .ThenBy(l => l.Produto)
                .ToList();

            foreach (string forma in FormasPagamento)
            {
                decimal valorForma =
                    vendasDoRelatorio
                    .SelectMany(v => v.Pagamentos)
                    .Where(p => p.FormaPagamento == forma)
                    .Sum(p => p.Valor);

                if (forma == "Dinheiro")
                {
                    valorForma -=
                        vendasDoRelatorio.Sum(v => v.Troco);
                }

                relatorio.Formas.Add(
                    new LinhaFormaPagamento
                    {
                        Forma = forma,
                        Valor = valorForma
                    }
                );
            }

            return relatorio;
        }
        public static BindingList<movimentoestoque> movimentacoes =
            new BindingList<movimentoestoque>();        public static BindingList<HistoricoEstoque> historicoestoque =
            new BindingList<HistoricoEstoque>();
        public static BindingList<Venda> vendas =
            new BindingList<Venda>();
        public static BindingList<Produto> produtos =
            new BindingList<Produto>();

        public static BindingList<Fornecedor> fornecedores =
            new BindingList<Fornecedor>();

        public static BindingList<Unidade> unidades =
            new BindingList<Unidade>();

        public static BindingList<Cliente> clientes =
            new BindingList<Cliente>();

        public static BindingList<Comanda> comandas =
            new BindingList<Comanda>();

        public static BindingList<Notinha> notinhas =
            new BindingList<Notinha>();

        public static BindingList<AbatimentoNotinha> abatimentos =
            new BindingList<AbatimentoNotinha>();

        public static BindingList<Caixa> caixas =
            new BindingList<Caixa>();

        public static BindingList<SaldoEstoque> saldosestoque =
            new BindingList<SaldoEstoque>();

        public static BindingList<Usuario> usuarios =
            new BindingList<Usuario>();

        public static BindingList<Funcionario> funcionarios =
            new BindingList<Funcionario>();

        public enum StatusCadastro
        {
            Inativo = 0,
            Ativo = 1
        }
    }
}
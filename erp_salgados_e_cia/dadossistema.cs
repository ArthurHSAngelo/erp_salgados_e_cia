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
            CarregarEntradas();
            CarregarPerdas();
            CarregarCaixas();
            CarregarVendas();
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

            // Saldo da unidade de trabalho (coluna "Estoque atual" e telas de venda).
            Unidade unidadeTrabalho =
                unidades.FirstOrDefault(u => u.Nome == UnidadeAtual);

            Dictionary<uint, decimal> saldos =
                unidadeTrabalho == null
                ? new Dictionary<uint, decimal>()
                : EstoqueRepositorio.SaldosDaUnidade(unidadeTrabalho.Id);

            foreach (Produto produto in doBanco)
            {
                decimal saldo;

                produto.EstoqueAtual =
                    saldos.TryGetValue(produto.Id, out saldo)
                    ? saldo
                    : 0m;
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

            return true;
        }

        public static void Sair()
        {
            if (UsuarioLogado != null)
            {
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

                CarregarProdutos();

                CarregarEntradas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
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

                saldoAtual = EstoqueRepositorio.SaldoDe(unidade.Id, produto.Id);

                CarregarProdutos();

                CarregarPerdas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

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

                CarregarProdutos();
            }
            catch (RegraNegocioException ex)
            {
                quantidadeAjustes = 0;
                mensagem = ex.Message;
                return false;
            }

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

            // Caixa e vendas do dia vêm do banco.
            CarregarCaixas();
            CarregarVendas();

            Unidade unidadeDoPainel =
                unidades.FirstOrDefault(u => u.Nome == unidade);

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

            if (unidadeDoPainel == null)
            {
                return painel;
            }

            System.Data.DataRow notinha =
                RelatorioRepositorio.ResumoNotinha(unidadeDoPainel.Id).Rows[0];

            painel.NotinhaClientes = System.Convert.ToInt32(notinha["clientes"]);
            painel.NotinhaTotal = System.Convert.ToDecimal(notinha["total"]);

            painel.UltimasVendas =
                RelatorioRepositorio.UltimasVendas(unidadeDoPainel.Id, 5)
                .Rows
                .Cast<System.Data.DataRow>()
                .Select(v => new LinhaVendaPainel
                {
                    Venda = System.Convert.ToInt32(v["id"]),
                    DataHora = System.Convert.ToDateTime(v["data_hora"]),
                    Total = System.Convert.ToDecimal(v["valor_total"]),
                    Status =
                        System.Convert.ToString(v["status"]) == "Concluida"
                        ? "Concluída"
                        : "Cancelada"
                })
                .ToList();

            if (incluirAtividade)
            {
                painel.Atividade =
                    LogRepositorio.Ultimos(unidadeDoPainel.Id, 5)
                    .Rows
                    .Cast<System.Data.DataRow>()
                    .Select(ParaLogOperacao)
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

        // ---- Log de operações (lido do banco) ----
        // As triggers e procedures do banco gravam o log; aqui só se consulta.

        private static TipoOperacaoLog OperacaoDoBanco(string operacao)
        {
            switch (operacao)
            {
                case "INSERT":
                    return TipoOperacaoLog.Inclusao;

                case "UPDATE":
                    return TipoOperacaoLog.Alteracao;

                case "DELETE":
                    return TipoOperacaoLog.Exclusao;

                case "LOGIN":
                    return TipoOperacaoLog.Login;

                default:
                    return TipoOperacaoLog.Logout;
            }
        }

        private static string OperacaoParaBanco(TipoOperacaoLog operacao)
        {
            switch (operacao)
            {
                case TipoOperacaoLog.Inclusao:
                    return "INSERT";

                case TipoOperacaoLog.Alteracao:
                    return "UPDATE";

                case TipoOperacaoLog.Exclusao:
                    return "DELETE";

                case TipoOperacaoLog.Login:
                    return "LOGIN";

                default:
                    return "LOGOUT";
            }
        }

        private static LogOperacao ParaLogOperacao(System.Data.DataRow linha)
        {
            return new LogOperacao(
                System.Convert.ToInt32(linha["id"]),
                System.Convert.ToDateTime(linha["data_hora"]),
                TextoOuNulo(linha["usuario"]) ?? "sistema",
                OperacaoDoBanco(System.Convert.ToString(linha["operacao"])),
                System.Convert.ToString(linha["tabela"]),
                TextoOuNulo(linha["registro"]),
                TextoOuNulo(linha["detalhes"]),
                TextoOuNulo(linha["unidade"]));
        }

        public static List<string> UsuariosDoLog()
        {
            return LogRepositorio.Usuarios(UnidadeDeTrabalho().Id);
        }

        public static List<string> TabelasDoLog()
        {
            return LogRepositorio.Tabelas(UnidadeDeTrabalho().Id);
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

            return LogRepositorio.Consultar(
                    UnidadeDeTrabalho().Id,
                    dataInicial,
                    dataFinal,
                    string.IsNullOrEmpty(usuario) ? null : usuario,
                    operacao == null ? null : OperacaoParaBanco(operacao.Value),
                    string.IsNullOrEmpty(tabela) ? null : tabela)
                .Rows
                .Cast<System.Data.DataRow>()
                .Select(ParaLogOperacao)
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

                CarregarCaixas();            }
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

        // ---- Comandas, vendas e Notinha (lidas do banco) ----
        // As listas vendas, notinhas e abatimentos são um espelho recarregado do banco
        // (login, depois de cada gravação e ao abrir as telas). Os relatórios do Bloco 8
        // ainda leem delas. As comandas abertas são lidas direto do banco a cada consulta.

        private static string FormaParaBanco(string forma)
        {
            switch (forma)
            {
                case "Crédito":
                    return "Credito";

                case "Débito":
                    return "Debito";

                default:
                    return forma;
            }
        }

        private static string FormaDoBanco(string forma)
        {
            switch (forma)
            {
                case "Credito":
                    return "Crédito";

                case "Debito":
                    return "Débito";

                default:
                    return forma;
            }
        }

        private static List<PagamentoNovo> ParaPagamentosNovos(
            IEnumerable<PagamentoVenda> pagamentos)
        {
            return pagamentos
                .Select(p => new PagamentoNovo
                {
                    Forma = FormaParaBanco(p.FormaPagamento),
                    Valor = p.Valor
                })
                .ToList();
        }

        private static List<ItemPedido> ParaItensPedido(
            IEnumerable<ItemVenda> itens)
        {
            List<ItemPedido> pedido = new List<ItemPedido>();

            foreach (ItemVenda item in itens)
            {
                Produto produto =
                    produtos.FirstOrDefault(p => p.Nome == item.Produto);

                if (produto == null)
                {
                    throw new RegraNegocioException(
                        $"Produto não encontrado: {item.Produto}.");
                }

                pedido.Add(
                    new ItemPedido
                    {
                        ProdutoId = produto.Id,
                        Quantidade = item.Quantidade,
                        PrecoUnitario = item.PrecoUnitario
                    });
            }

            return pedido;
        }

        public static void CarregarVendas()
        {
            Dictionary<int, Venda> porId = new Dictionary<int, Venda>();
            List<Venda> doBanco = new List<Venda>();

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarVendas().Rows)
            {
                Venda venda = new Venda
                {
                    IdVenda = System.Convert.ToInt32(linha["id"]),
                    NumeroMesa = System.Convert.ToInt32(linha["numero_mesa"]),
                    IdCaixa = System.Convert.ToInt32(linha["caixa_id"]),
                    DataHora = System.Convert.ToDateTime(linha["data_hora"]),
                    ValorTotal = System.Convert.ToDecimal(linha["valor_total"]),
                    Troco = System.Convert.ToDecimal(linha["troco"]),
                    Status =
                        System.Convert.ToString(linha["status"]) == "Concluida"
                        ? "Concluída"
                        : "Cancelada",
                    IdCliente =
                        linha["cliente_id"] == System.DBNull.Value
                        ? (int?)null
                        : System.Convert.ToInt32(linha["cliente_id"]),
                    MotivoCancelamento = TextoOuNulo(linha["motivo_cancelamento"]),
                    CanceladaPor = TextoOuNulo(linha["cancelado_por"]),
                    DataCancelamento =
                        linha["data_cancelamento"] == System.DBNull.Value
                        ? (System.DateTime?)null
                        : System.Convert.ToDateTime(linha["data_cancelamento"])
                };

                porId[venda.IdVenda] = venda;
                doBanco.Add(venda);
            }

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarItensDasVendas().Rows)
            {
                Venda venda;

                if (porId.TryGetValue(System.Convert.ToInt32(linha["venda_id"]), out venda))
                {
                    venda.Itens.Add(
                        new ItemVenda
                        {
                            Produto = System.Convert.ToString(linha["produto"]),
                            Quantidade = System.Convert.ToDecimal(linha["quantidade"]),
                            PrecoUnitario = System.Convert.ToDecimal(linha["preco_unitario"])
                        });
                }
            }

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarPagamentosDasVendas().Rows)
            {
                Venda venda;

                if (!porId.TryGetValue(System.Convert.ToInt32(linha["venda_id"]), out venda))
                {
                    continue;
                }

                string forma = FormaDoBanco(System.Convert.ToString(linha["forma_pagamento"]));
                decimal valor = System.Convert.ToDecimal(linha["valor"]);

                PagamentoVenda existente =
                    venda.Pagamentos.FirstOrDefault(p => p.FormaPagamento == forma);

                if (existente != null)
                {
                    existente.Valor += valor;
                }
                else
                {
                    venda.Pagamentos.Add(
                        new PagamentoVenda { FormaPagamento = forma, Valor = valor });
                }
            }

            foreach (Venda venda in doBanco)
            {
                venda.ValorRecebido =
                    venda.Pagamentos
                    .Where(p => p.FormaPagamento != "Notinha")
                    .Sum(p => p.Valor);

                venda.FormaPagamento = string.Join(
                    " + ",
                    venda.Pagamentos.Select(p =>
                        $"{p.FormaPagamento} R$ {p.Valor:N2}"));
            }

            vendas.Clear();

            foreach (Venda venda in doBanco)
            {
                vendas.Add(venda);
            }

            notinhas.Clear();

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarNotinhas().Rows)
            {
                notinhas.Add(
                    new Notinha
                    {
                        IdNotinha = System.Convert.ToInt32(linha["id"]),
                        IdCliente = System.Convert.ToInt32(linha["cliente_id"]),
                        IdVenda = System.Convert.ToInt32(linha["venda_id"]),
                        DataHora = System.Convert.ToDateTime(linha["data_hora"]),
                        Valor = System.Convert.ToDecimal(linha["valor"]),
                        Unidade = System.Convert.ToString(linha["unidade"])
                    });
            }

            abatimentos.Clear();

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarAbatimentos().Rows)
            {
                abatimentos.Add(
                    new AbatimentoNotinha
                    {
                        IdAbatimento = System.Convert.ToInt32(linha["id"]),
                        IdCliente = System.Convert.ToInt32(linha["cliente_id"]),
                        IdCaixa = System.Convert.ToInt32(linha["caixa_id"]),
                        Unidade = System.Convert.ToString(linha["unidade"]),
                        DataHora = System.Convert.ToDateTime(linha["data_hora"]),
                        Valor = System.Convert.ToDecimal(linha["valor"]),
                        FormaPagamento = FormaDoBanco(System.Convert.ToString(linha["forma_pagamento"]))
                    });
            }
        }

        // Comandas de mesa abertas na unidade de trabalho (itens e pagamentos incluídos).
        public static List<Comanda> ComandasDaUnidade()
        {
            uint unidadeId = UnidadeDeTrabalho().Id;

            List<Comanda> lista = new List<Comanda>();
            Dictionary<uint, Comanda> porId = new Dictionary<uint, Comanda>();

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarComandasAbertas(unidadeId).Rows)
            {
                Comanda comanda = new Comanda
                {
                    Id = System.Convert.ToUInt32(linha["id"]),
                    NumeroMesa = System.Convert.ToInt32(linha["numero_mesa"]),
                    Unidade = UnidadeAtual,
                    IdCliente =
                        linha["cliente_id"] == System.DBNull.Value
                        ? (int?)null
                        : System.Convert.ToInt32(linha["cliente_id"]),
                    Abertura = System.Convert.ToDateTime(linha["data_abertura"])
                };

                porId[comanda.Id] = comanda;
                lista.Add(comanda);
            }

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarItensDasComandasAbertas(unidadeId).Rows)
            {
                Comanda comanda;

                if (!porId.TryGetValue(System.Convert.ToUInt32(linha["comanda_id"]), out comanda))
                {
                    continue;
                }

                string nomeProduto = System.Convert.ToString(linha["produto"]);
                decimal quantidade = System.Convert.ToDecimal(linha["quantidade"]);
                decimal preco = System.Convert.ToDecimal(linha["preco_unitario"]);

                ItemVenda existente =
                    comanda.Itens.FirstOrDefault(i =>
                        i.Produto == nomeProduto &&
                        i.PrecoUnitario == preco);

                if (existente != null)
                {
                    existente.Quantidade += quantidade;
                }
                else
                {
                    comanda.Itens.Add(
                        new ItemVenda
                        {
                            Produto = nomeProduto,
                            Quantidade = quantidade,
                            PrecoUnitario = preco
                        });
                }
            }

            foreach (System.Data.DataRow linha in
                VendaRepositorio.ListarPagamentosDasComandasAbertas(unidadeId).Rows)
            {
                Comanda comanda;

                if (!porId.TryGetValue(System.Convert.ToUInt32(linha["comanda_id"]), out comanda))
                {
                    continue;
                }

                string forma = FormaDoBanco(System.Convert.ToString(linha["forma_pagamento"]));
                decimal valor = System.Convert.ToDecimal(linha["valor"]);

                PagamentoVenda existente =
                    comanda.Pagamentos.FirstOrDefault(p => p.FormaPagamento == forma);

                if (existente != null)
                {
                    existente.Valor += valor;
                }
                else
                {
                    comanda.Pagamentos.Add(
                        new PagamentoVenda { FormaPagamento = forma, Valor = valor });
                }
            }

            return lista;
        }

        public static Comanda ComandaDaMesa(int numeroMesa)
        {
            return ComandasDaUnidade()
                .FirstOrDefault(c => c.NumeroMesa == numeroMesa);
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

        // Confere os pagamentos antes de gravar, para não deixar uma venda pela metade.
        private static string ConferirPagamentos(
            decimal total,
            IEnumerable<PagamentoVenda> pagamentos,
            bool temCliente)
        {
            decimal pago = pagamentos.Sum(p => p.Valor);

            decimal dinheiro =
                pagamentos
                .Where(p => p.FormaPagamento == "Dinheiro")
                .Sum(p => p.Valor);

            if (total <= 0)
            {
                return "A venda não tem itens.";
            }

            if (pagamentos.Any(p => p.FormaPagamento == "Notinha") && !temCliente)
            {
                return "Para usar Notinha, selecione um cliente na venda.";
            }

            if (pago < total)
            {
                return $"Ainda faltam R$ {total - pago:N2} para cobrir o total da venda.";
            }

            if (pago - total > dinheiro)
            {
                return "O troco não pode ser maior que o valor pago em dinheiro.";
            }

            return null;
        }

        private static uint? ClienteParaBanco(int? idCliente)
        {
            return idCliente.HasValue
                ? (uint?)idCliente.Value
                : null;
        }

        private static Caixa CaixaParaVender()
        {
            CarregarCaixas();

            return CaixaAberto();
        }

        public static bool EnviarPedidoParaComanda(
            int numeroMesa,
            int? idCliente,
            List<ItemVenda> itens,
            out string mensagem)
        {
            mensagem = "";

            try
            {
                VendaRepositorio.EnviarPedido(
                    UnidadeDeTrabalho().Id,
                    numeroMesa,
                    ClienteParaBanco(idCliente),
                    ParaItensPedido(itens));

                CarregarProdutos();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            return true;
        }

        public static bool TrocarClienteDaComanda(
            Comanda comanda,
            int? novoCliente,
            out string mensagem)
        {
            mensagem = "";

            try
            {
                VendaRepositorio.TrocarCliente(
                    comanda.Id,
                    ClienteParaBanco(novoCliente));
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            return true;
        }

        public static bool CancelarComanda(
            Comanda comanda,
            out string mensagem)
        {
            mensagem = "";

            try
            {
                VendaRepositorio.CancelarComanda(
                    comanda.Id,
                    IdDoUsuarioLogado());

                CarregarProdutos();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            return true;
        }

        // Pagamento de uma comanda: grava os pagamentos e, se cobrirem tudo, cobra e gera a venda.
        public static bool CobrarComanda(
            Comanda comanda,
            IEnumerable<PagamentoVenda> pagamentosNovos,
            bool pagamentoParcial,
            out string mensagem)
        {
            mensagem = "";

            try
            {
                List<PagamentoVenda> novos = pagamentosNovos.ToList();

                if (!pagamentoParcial)
                {
                    List<PagamentoVenda> todos =
                        comanda.Pagamentos
                        .Select(p => new PagamentoVenda { FormaPagamento = p.FormaPagamento, Valor = p.Valor })
                        .Concat(novos)
                        .ToList();

                    string problema = ConferirPagamentos(
                        comanda.Total,
                        todos,
                        comanda.IdCliente != null);

                    if (problema != null)
                    {
                        mensagem = problema;
                        return false;
                    }
                }

                Caixa caixa = CaixaParaVender();

                if (caixa == null)
                {
                    mensagem = "Abra o caixa antes de registrar vendas.";
                    return false;
                }

                VendaRepositorio.RegistrarPagamentos(
                    comanda.Id,
                    ParaPagamentosNovos(novos));

                if (pagamentoParcial)
                {
                    return true;
                }

                uint idVenda = VendaRepositorio.Cobrar(
                    comanda.Id,
                    (uint)caixa.IdCaixa,
                    IdDoUsuarioLogado());

                CarregarVendas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            return true;
        }

        public static bool VenderNoBalcao(
            int? idCliente,
            List<ItemVenda> itens,
            IEnumerable<PagamentoVenda> pagamentos,
            out string mensagem)
        {
            mensagem = "";

            try
            {
                List<PagamentoVenda> lista = pagamentos.ToList();

                string problema = ConferirPagamentos(
                    itens.Sum(i => i.Total),
                    lista,
                    idCliente != null);

                if (problema != null)
                {
                    mensagem = problema;
                    return false;
                }

                Caixa caixa = CaixaParaVender();

                if (caixa == null)
                {
                    mensagem = "Abra o caixa antes de registrar vendas.";
                    return false;
                }

                uint idVenda = VendaRepositorio.VenderNoBalcao(
                    UnidadeDeTrabalho().Id,
                    ClienteParaBanco(idCliente),
                    ParaItensPedido(itens),
                    ParaPagamentosNovos(lista),
                    (uint)caixa.IdCaixa,
                    IdDoUsuarioLogado());

                CarregarProdutos();
                CarregarVendas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            return true;
        }

        // Abatimento da Notinha de um cliente na unidade de trabalho (o troco sai do dinheiro).
        public static bool RegistrarAbatimentoNotinha(
            Cliente cliente,
            IEnumerable<PagamentoVenda> pagamentosCobrados,
            out string mensagem)
        {
            mensagem = "";

            try
            {
                Caixa caixa = CaixaParaVender();

                if (caixa == null)
                {
                    mensagem = "Abra o caixa antes de registrar pagamentos.";
                    return false;
                }

                decimal restante = SaldoNotinha(cliente.IdCliente);

                List<PagamentoNovo> abatimentosNovos = new List<PagamentoNovo>();

                foreach (PagamentoVenda pagamento in pagamentosCobrados
                    .OrderBy(p => p.FormaPagamento == "Dinheiro"))
                {
                    decimal valorAbatido =
                        System.Math.Min(pagamento.Valor, restante);

                    if (valorAbatido <= 0)
                    {
                        break;
                    }

                    abatimentosNovos.Add(
                        new PagamentoNovo
                        {
                            Forma = FormaParaBanco(pagamento.FormaPagamento),
                            Valor = valorAbatido
                        });

                    restante -= valorAbatido;
                }

                VendaRepositorio.RegistrarAbatimentos(
                    (uint)cliente.IdCliente,
                    (uint)caixa.IdCaixa,
                    UnidadeDeTrabalho().Id,
                    abatimentosNovos);

                CarregarVendas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

            return true;
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

            try
            {
                UsuarioAcesso autorizador =
                    UsuarioRepositorio.BuscarPorLogin(autorizadoPor);

                if (autorizador == null)
                {
                    mensagem = "Autorizador não encontrado.";
                    return false;
                }

                // As regras (caixa aberto, unidade, Notinha já abatida, perfil) são do banco.
                VendaRepositorio.CancelarVenda(
                    (uint)venda.IdVenda,
                    autorizador.Id,
                    motivo.Trim());

                CarregarVendas();
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return false;
            }

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
        // DRE calculado no banco (procedure sp_dre).
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

            uint? unidadeId = null;

            if (!relatorio.Consolidado)
            {
                Unidade escolhida =
                    unidades.FirstOrDefault(u => u.Nome == unidade);

                if (escolhida == null)
                {
                    mensagem = "Unidade não encontrada.";
                    return null;
                }

                unidadeId = escolhida.Id;
            }

            try
            {
                foreach (System.Data.DataRow linha in
                    RelatorioRepositorio.Dre(unidadeId, dataInicial, dataFinal)
                    .Rows
                    .Cast<System.Data.DataRow>()
                    .OrderBy(l => System.Convert.ToString(l["unidade"])))
                {
                    relatorio.Unidades.Add(
                        new DreUnidade
                        {
                            Unidade = System.Convert.ToString(linha["unidade"]),
                            Receita = System.Convert.ToDecimal(linha["receita"]),
                            Recebido = System.Convert.ToDecimal(linha["recebido"]),
                            VendidoEmNotinha = System.Convert.ToDecimal(linha["vendido_notinha"]),
                            Abatimentos = System.Convert.ToDecimal(linha["abatimentos"]),
                            Despesas = System.Convert.ToDecimal(linha["despesa"]),
                            Perdas = System.Convert.ToDecimal(linha["perdas"]),
                            PerdasSemCusto = System.Convert.ToInt32(linha["perdas_sem_custo"])
                        });
                }
            }
            catch (RegraNegocioException ex)
            {
                mensagem = ex.Message;
                return null;
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

        // Relatório de vendas calculado no banco. Com filtro de forma de pagamento ou de
        // produto, entram as vendas que têm aquela forma / aquele produto.
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

            RelatorioVendas relatorio = new RelatorioVendas
            {
                DataInicial = dataInicial,
                DataFinal = dataFinal
            };

            uint? unidadeId = null;
            uint? produtoId = null;

            if (!string.IsNullOrEmpty(unidade))
            {
                Unidade escolhida =
                    unidades.FirstOrDefault(u => u.Nome == unidade);

                if (escolhida == null)
                {
                    return relatorio;
                }

                unidadeId = escolhida.Id;
            }

            if (!string.IsNullOrEmpty(produto))
            {
                Produto escolhido =
                    produtos.FirstOrDefault(p => p.Nome == produto);

                if (escolhido == null)
                {
                    return relatorio;
                }

                produtoId = escolhido.Id;
            }

            string formaBanco =
                string.IsNullOrEmpty(formaPagamento)
                ? null
                : FormaParaBanco(formaPagamento);

            Dictionary<System.DateTime, System.Data.DataRow> porDia =
                RelatorioRepositorio
                .VendasPorDia(dataInicial, dataFinal, unidadeId, formaBanco, produtoId)
                .Rows
                .Cast<System.Data.DataRow>()
                .ToDictionary(l => System.Convert.ToDateTime(l["dia"]).Date);

            int quantidadeVendas = porDia.Values.Sum(l => System.Convert.ToInt32(l["quantidade"]));
            decimal faturamento = porDia.Values.Sum(l => System.Convert.ToDecimal(l["faturamento"]));
            decimal troco = porDia.Values.Sum(l => System.Convert.ToDecimal(l["troco"]));

            Dictionary<string, decimal> pagamentos =
                RelatorioRepositorio
                .PagamentosDasVendas(dataInicial, dataFinal, unidadeId, formaBanco, produtoId)
                .Rows
                .Cast<System.Data.DataRow>()
                .ToDictionary(
                    l => FormaDoBanco(System.Convert.ToString(l["forma_pagamento"])),
                    l => System.Convert.ToDecimal(l["valor"]));

            System.Func<string, decimal> valorDaForma = forma =>
            {
                decimal valor;
                return pagamentos.TryGetValue(forma, out valor) ? valor : 0m;
            };

            decimal emNotinha = valorDaForma("Notinha");

            decimal recebido =
                pagamentos
                .Where(p => p.Key != "Notinha")
                .Sum(p => p.Value)
                - troco;

            relatorio.Resumo = new ResumoVendas
            {
                QuantidadeVendas = quantidadeVendas,
                Faturamento = faturamento,
                Recebido = recebido,
                EmNotinha = emNotinha,
                TicketMedio =
                    quantidadeVendas > 0
                    ? faturamento / quantidadeVendas
                    : 0m
            };

            for (System.DateTime dia = dataInicial;
                dia <= dataFinal;
                dia = dia.AddDays(1))
            {
                System.Data.DataRow linhaDoDia;

                bool temVendas = porDia.TryGetValue(dia, out linhaDoDia);

                relatorio.PorDia.Add(
                    new LinhaFaturamentoDia
                    {
                        Dia = dia,
                        QuantidadeVendas =
                            temVendas
                            ? System.Convert.ToInt32(linhaDoDia["quantidade"])
                            : 0,
                        Faturamento =
                            temVendas
                            ? System.Convert.ToDecimal(linhaDoDia["faturamento"])
                            : 0m
                    }
                );
            }

            relatorio.Produtos =
                RelatorioRepositorio
                .ProdutosVendidos(dataInicial, dataFinal, unidadeId, formaBanco, produtoId)
                .Rows
                .Cast<System.Data.DataRow>()
                .Select(l => new LinhaProdutoVendido
                {
                    Produto = System.Convert.ToString(l["produto"]),
                    Quantidade = System.Convert.ToDecimal(l["quantidade"]),
                    Receita = System.Convert.ToDecimal(l["receita"])
                })
                .OrderByDescending(l => l.Quantidade)
                .ThenByDescending(l => l.Receita)
                .ThenBy(l => l.Produto)
                .ToList();

            foreach (string forma in FormasPagamento)
            {
                decimal valorForma = valorDaForma(forma);

                if (forma == "Dinheiro")
                {
                    valorForma -= troco;
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
            new BindingList<movimentoestoque>();
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

        public static BindingList<Notinha> notinhas =
            new BindingList<Notinha>();

        public static BindingList<AbatimentoNotinha> abatimentos =
            new BindingList<AbatimentoNotinha>();

        public static BindingList<Caixa> caixas =
            new BindingList<Caixa>();

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
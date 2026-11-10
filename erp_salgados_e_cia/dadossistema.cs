using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace erp_salgados_e_cia
{
    public static class dadossistema
    {
        public static bool ProdutoPossuiVinculos(Produto produto)
        {
            return produto != null &&
                   (movimentacoes.Any(m =>
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
                   movimentacoes.Any(m =>
                       m.Fornecedor == fornecedor.Nome);
        }

        public static bool UnidadePossuiVinculos(
            Unidade unidade)
        {
            return unidade != null &&
                   (movimentacoes.Any(m =>
                       m.Unidade == unidade.Nome) ||
                    usuarios.Any(u =>
                       u.Unidade == unidade.Nome) ||
                    funcionarios.Any(f =>
                       f.Unidade == unidade.Nome));
        }

        public static bool ClientePossuiVinculos(
            Cliente cliente)
        {
            return cliente != null &&
                   (vendas.Any(v =>
                       v.IdCliente == cliente.IdCliente) ||
                    comandas.Any(c =>
                       c.IdCliente == cliente.IdCliente) ||
                    notinhas.Any(n =>
                       n.IdCliente == cliente.IdCliente) ||
                    abatimentos.Any(a =>
                       a.IdCliente == cliente.IdCliente));
        }

        public static string UnidadeAtual { get; set; } =
            "Lanchonete Centro";

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
            byte[] sal = new byte[16];

            using (System.Security.Cryptography.RNGCryptoServiceProvider gerador =
                new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                gerador.GetBytes(sal);
            }

            byte[] hash;

            using (System.Security.Cryptography.Rfc2898DeriveBytes derivador =
                new System.Security.Cryptography.Rfc2898DeriveBytes(
                    senha,
                    sal,
                    10000,
                    System.Security.Cryptography.HashAlgorithmName.SHA256))
            {
                hash = derivador.GetBytes(32);
            }

            return System.Convert.ToBase64String(sal) +
                   ":" +
                   System.Convert.ToBase64String(hash);
        }

        public static bool VerificarSenha(string senha, string senhaHash)
        {
            if (string.IsNullOrEmpty(senha) ||
                string.IsNullOrEmpty(senhaHash))
            {
                return false;
            }

            string[] partes = senhaHash.Split(':');

            if (partes.Length != 2)
            {
                return false;
            }

            byte[] sal = System.Convert.FromBase64String(partes[0]);
            byte[] esperado = System.Convert.FromBase64String(partes[1]);
            byte[] calculado;

            using (System.Security.Cryptography.Rfc2898DeriveBytes derivador =
                new System.Security.Cryptography.Rfc2898DeriveBytes(
                    senha,
                    sal,
                    10000,
                    System.Security.Cryptography.HashAlgorithmName.SHA256))
            {
                calculado = derivador.GetBytes(esperado.Length);
            }

            int diferenca = 0;

            for (int i = 0; i < esperado.Length; i++)
            {
                diferenca |= esperado[i] ^ calculado[i];
            }

            return diferenca == 0;
        }

        public static bool Autenticar(
            string login,
            string senha,
            out Usuario usuario,
            out string mensagem)
        {
            usuario = null;
            mensagem = "";

            string loginInformado =
                (login ?? "").Trim();

            Usuario encontrado =
                usuarios.FirstOrDefault(u =>
                    string.Equals(
                        u.Login,
                        loginInformado,
                        System.StringComparison.OrdinalIgnoreCase));

            if (encontrado == null ||
                !VerificarSenha(senha, encontrado.SenhaHash))
            {
                mensagem = "Login ou senha inválidos.";
                return false;
            }

            if (encontrado.Status != StatusCadastro.Ativo)
            {
                mensagem = "Usuário inativo. Procure um gerente.";
                return false;
            }

            usuario = encontrado;
            UsuarioLogado = encontrado;

            UnidadeAtual =
                encontrado.Unidade ??
                unidades
                    .Where(u => u.Status == StatusCadastro.Ativo)
                    .OrderBy(u => u.Nome)
                    .Select(u => u.Nome)
                    .FirstOrDefault() ??
                UnidadeAtual;

            RegistrarLog(
                TipoOperacaoLog.Login,
                "Usuario",
                encontrado.Login,
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
            }

            UsuarioLogado = null;
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
                    detalhes
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

        public static BindingList<Perda> perdas =
            new BindingList<Perda>();

        public static int ProximoIdPerda()
        {
            return perdas.Any()
                ? perdas.Max(p => p.IdPerda) + 1
                : 1;
        }

        public static List<Perda> PerdasDaUnidade(
            string unidade,
            int dias)
        {
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

            if (quantidade > produto.EstoqueAtual)
            {
                mensagem =
                    $"Quantidade acima do saldo disponível ({produto.EstoqueAtual:N2}).";
                return false;
            }

            if (data.Date > System.DateTime.Today)
            {
                mensagem = "A data da perda não pode ser futura.";
                return false;
            }

            decimal saldoAnterior = produto.EstoqueAtual;

            produto.EstoqueAtual -= quantidade;

            Perda perda = new Perda
            {
                IdPerda = ProximoIdPerda(),
                Data = data.Date,
                Unidade = UnidadeAtual,
                Produto = produto.Nome,
                Quantidade = quantidade,
                Motivo = motivo,
                Responsavel = responsavel,
                Usuario = UsuarioLogado == null
                    ? "sistema"
                    : UsuarioLogado.Login,
                DataHoraRegistro = System.DateTime.Now
            };

            perdas.Add(perda);

            RegistrarHistoricoEstoque(
                produto,
                TipoMovimentoEstoque.Perda,
                quantidade,
                responsavel,
                $"Perda ({perda.MotivoTexto})"
            );

            RegistrarLog(
                TipoOperacaoLog.Inclusao,
                "Perda",
                $"Perda {perda.IdPerda}: {produto.Nome}",
                $"Quantidade: {quantidade:N2}; Motivo: {perda.MotivoTexto}; Data: {perda.Data:dd/MM/yyyy}; Responsável: {responsavel}; Unidade: {perda.Unidade}; Saldo: {saldoAnterior:N2} → {produto.EstoqueAtual:N2}");

            return true;
        }

        public static BindingList<Inventario> inventarios =
            new BindingList<Inventario>();

        public static int ProximoIdInventario()
        {
            return inventarios.Any()
                ? inventarios.Max(i => i.IdInventario) + 1
                : 1;
        }

        public static List<ItemInventario> MontarItensInventario()
        {
            return produtos
                .Where(p => p.Status == StatusCadastro.Ativo)
                .OrderBy(p => p.Nome)
                .Select(p => new ItemInventario
                {
                    Produto = p.Nome,
                    SaldoSistema = p.EstoqueAtual,
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

                decimal diferencaItem =
                    item.QuantidadeContada.Value - produto.EstoqueAtual;

                if (diferencaItem != 0 &&
                    string.IsNullOrWhiteSpace(item.Motivo))
                {
                    mensagem =
                        $"Informe o motivo da divergência de {item.Produto}.";
                    return false;
                }
            }

            string responsavelEfetivo =
                string.IsNullOrWhiteSpace(responsavel)
                ? UsuarioLogado.Login
                : responsavel.Trim();

            Inventario inventario = new Inventario
            {
                IdInventario = ProximoIdInventario(),
                DataHora = System.DateTime.Now,
                Unidade = UnidadeAtual,
                Usuario = UsuarioLogado.Login,
                Responsavel = responsavelEfetivo,
                Observacao =
                    string.IsNullOrWhiteSpace(observacao)
                    ? null
                    : observacao.Trim()
            };

            List<string> linhasLog = new List<string>();

            foreach (ItemInventario item in contados)
            {
                Produto produto =
                    produtos.First(p =>
                        p.Nome == item.Produto &&
                        p.Status == StatusCadastro.Ativo);

                decimal saldoAnterior = produto.EstoqueAtual;
                decimal contada = item.QuantidadeContada.Value;
                decimal diferenca = contada - saldoAnterior;

                string motivo =
                    diferenca == 0
                    ? null
                    : item.Motivo.Trim();

                inventario.Itens.Add(
                    new ItemInventario
                    {
                        Produto = produto.Nome,
                        SaldoSistema = saldoAnterior,
                        QuantidadeContada = contada,
                        Motivo = motivo
                    }
                );

                if (diferenca != 0)
                {
                    produto.EstoqueAtual = contada;

                    RegistrarHistoricoEstoque(
                        produto,
                        TipoMovimentoEstoque.Ajuste,
                        diferenca,
                        responsavelEfetivo,
                        $"Inventário {inventario.IdInventario}: {motivo}"
                    );

                    quantidadeAjustes++;

                    linhasLog.Add(
                        $"{produto.Nome}: {saldoAnterior:N2} → {contada:N2} ({motivo})");
                }
            }

            inventarios.Add(inventario);

            RegistrarLog(
                TipoOperacaoLog.Inclusao,
                "Inventario",
                $"Inventário {inventario.IdInventario}",
                JuntarDetalhes(
                    $"Unidade: {inventario.Unidade}",
                    $"Responsável: {responsavelEfetivo}",
                    $"Produtos contados: {contados.Count}",
                    $"Ajustes: {quantidadeAjustes}",
                    inventario.Observacao == null
                        ? null
                        : $"Observação: {inventario.Observacao}",
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
                .Select(l => l.Usuario)
                .Concat(usuarios.Select(u => u.Login))
                .Distinct()
                .OrderBy(nome => nome)
                .ToList();
        }

        public static List<string> TabelasDoLog()
        {
            return registrosLog
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

        public static int ProximoIdUsuario()
        {
            return usuarios.Any()
                ? usuarios.Max(u => u.IdUsuario) + 1
                : 1;
        }

        public static int ProximoIdFuncionario()
        {
            return funcionarios.Any()
                ? funcionarios.Max(f => f.IdFuncionario) + 1
                : 1;
        }

        private static void CriarUsuariosIniciais()
        {
            // SENHAS PROVISÓRIAS DE TESTE: troque pelas suas.
            // Elas só existem aqui; o sistema guarda apenas o hash.
            string senhaAdmin = "123";
            string senhaGerente1 = "123";
            string senhaGerente2 = "123";
            string senhaCentro = "123";

            usuarios.Add(new Usuario
            {
                IdUsuario = 1,
                Login = "admin",
                SenhaHash = GerarHashSenha(senhaAdmin),
                Perfil = PerfilUsuario.Administrador,
                Unidade = null,
                Status = StatusCadastro.Ativo
            });

            usuarios.Add(new Usuario
            {
                IdUsuario = 2,
                Login = "gerente1",
                SenhaHash = GerarHashSenha(senhaGerente1),
                Perfil = PerfilUsuario.Gerente,
                Unidade = null,
                Status = StatusCadastro.Ativo
            });

            usuarios.Add(new Usuario
            {
                IdUsuario = 3,
                Login = "gerente2",
                SenhaHash = GerarHashSenha(senhaGerente2),
                Perfil = PerfilUsuario.Gerente,
                Unidade = null,
                Status = StatusCadastro.Ativo
            });

            usuarios.Add(new Usuario
            {
                IdUsuario = 4,
                Login = "centro",
                SenhaHash = GerarHashSenha(senhaCentro),
                Perfil = PerfilUsuario.Atendente,
                Unidade = "Lanchonete Centro",
                Status = StatusCadastro.Ativo
            });

            funcionarios.Add(new Funcionario
            {
                IdFuncionario = 1,
                Nome = "Ana Lima",
                Unidade = "Lanchonete Centro",
                Status = StatusCadastro.Ativo
            });

            funcionarios.Add(new Funcionario
            {
                IdFuncionario = 2,
                Nome = "Carlos Souza",
                Unidade = "Lanchonete Centro",
                Status = StatusCadastro.Ativo
            });
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
                   (caixas.Any(c =>
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

        public static int ProximoIdCaixa()
        {
            return caixas.Any()
                ? caixas.Max(c => c.IdCaixa) + 1
                : 1;
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

        public static Comanda ComandaDaMesa(int numeroMesa)
        {
            return comandas.FirstOrDefault(c =>
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
                       .Where(n => n.IdCliente == idCliente)
                       .Sum(n => n.Valor)
                   - abatimentos
                       .Where(a => a.IdCliente == idCliente)
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
                    Valor = valorNotinha
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

            string loginInformado =
                (login ?? "").Trim();

            Usuario encontrado =
                usuarios.FirstOrDefault(u =>
                    string.Equals(
                        u.Login,
                        loginInformado,
                        System.StringComparison.OrdinalIgnoreCase));

            if (encontrado == null ||
                !VerificarSenha(senha, encontrado.SenhaHash))
            {
                mensagem = "Login ou senha inválidos.";
                return false;
            }

            if (encontrado.Status != StatusCadastro.Ativo)
            {
                mensagem = "Usuário inativo.";
                return false;
            }

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

        public static decimal? UltimoCustoDoProduto(string nomeProduto)
        {
            var ultima = movimentacoes
                .Select((m, posicao) => new { m, posicao })
                .Where(x => x.m.Produto == nomeProduto)
                .OrderByDescending(x => x.m.DataEntrada)
                .ThenByDescending(x => x.posicao)
                .FirstOrDefault();

            if (ultima == null)
            {
                return null;
            }

            return ultima.m.CustoUnitario;
        }

        public static List<LinhaEstoque> MontarRelatorioEstoque()
        {
            List<LinhaEstoque> linhas = new List<LinhaEstoque>();

            foreach (Produto produto in produtos)
            {
                decimal? ultimoCusto = UltimoCustoDoProduto(produto.Nome);

                linhas.Add(
                    new LinhaEstoque
                    {
                        Produto = produto.Nome,
                        Categoria = produto.Categoria,
                        UnidadeMedida = produto.UnidadeMedida,
                        EstoqueAtual = produto.EstoqueAtual,
                        EstoqueMinimo = produto.EstoqueMinimo,
                        Situacao =
                            produto.EstoqueAtual < produto.EstoqueMinimo
                                ? "Crítico"
                                : "Normal",
                        PrecoVenda = produto.Preco,
                        UltimoCusto = ultimoCusto,
                        ValorACusto =
                            produto.EstoqueAtual * (ultimoCusto ?? 0m),
                        ValorAPrecoVenda =
                            produto.EstoqueAtual * produto.Preco,
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

            return historicoestoque
                .Where(h =>
                    h.Data.Date >= dataInicial &&
                    h.Data.Date <= dataFinal &&
                    (string.IsNullOrEmpty(unidade) ||
                        h.Unidade == unidade) &&
                    (string.IsNullOrEmpty(produto) ||
                        h.Produto == produto) &&
                    (tipo == null || h.Tipo == tipo))
                .OrderByDescending(h => h.Data)
                .ToList();
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
        public static int ProximoIdCliente()
        {
            return clientes.Any()
                ? clientes.Max(c => c.IdCliente) + 1
                : 1;
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

        public static BindingList<Usuario> usuarios =
            new BindingList<Usuario>();

        public static BindingList<Funcionario> funcionarios =
            new BindingList<Funcionario>();

        static dadossistema()
        {
            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Coxinha",
                Preco = 5.00m,
                Categoria = "Salgados",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 10,
                EstoqueAtual = 50
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Bolinha de queijo",
                Preco = 4.50m,
                Categoria = "Salgados",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 10,
                EstoqueAtual = 50
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Refrigerante",
                Preco = 6.00m,
                Categoria = "Bebidas",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 5,
                EstoqueAtual = 50
            });
            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Pastel de carne",
                Preco = 7.00m,
                Categoria = "Salgados",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 10,
                EstoqueAtual = 30
            });

            produtos.Add(new Produto
            {
                Status = StatusCadastro.Ativo,
                Nome = "Suco de laranja",
                Preco = 5.50m,
                Categoria = "Bebidas",
                UnidadeMedida = "Unidade",
                EstoqueMinimo = 5,
                EstoqueAtual = 20
            });

            fornecedores.Add(new Fornecedor
            {
                Status = StatusCadastro.Ativo,
                Nome = "Distribuidora Central",
                Cnpj = "12.345.678/0001-90",
                Telefone = "(11) 3333-4444"
            });

            fornecedores.Add(new Fornecedor
            {
                Status = StatusCadastro.Ativo,
                Nome = "Fornecedor Bom Sabor",
                Cnpj = "98.765.432/0001-10",
                Telefone = "(11) 99999-8888"
            });
            unidades.Add(new Unidade
            {
                Status = StatusCadastro.Ativo,
                Nome = "Lanchonete Centro",
                Cnpj = "11.222.333/0001-44",
                Endereco = "Rua Principal",
                Numero = "100"
            });

            clientes.Add(new Cliente
            {
                IdCliente = 1,
                Status = StatusCadastro.Ativo,
                Nome = "Maria Souza",
                CpfCnpj = "123.456.789-09",
                Telefone = "(11) 98888-1111"
            });

            clientes.Add(new Cliente
            {
                IdCliente = 2,
                Status = StatusCadastro.Ativo,
                Nome = "João Pereira",
                Telefone = "(11) 97777-2222"
            });

            CriarUsuariosIniciais();
        }

        public enum StatusCadastro
        {
            Inativo = 0,
            Ativo = 1
        }
    }
}
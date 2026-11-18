using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using erp_salgados_e_cia.Domain.Exceptions;

namespace erp_salgados_e_cia.Data.Connection
{
    /// <summary>
    /// Ponto único de acesso ao MySQL/MariaDB. Todas as telas e repositórios
    /// passam por aqui: abre a conexão, informa ao banco quem é o usuário e a
    /// unidade (SET @usuario_id / @unidade_id) e traduz os erros do banco em
    /// mensagens para o usuário (RegraNegocioException).
    /// </summary>
    public static class BancoDados
    {
        private const string NomeConexao = "salgados_cia";

        private static string _stringConexao;

        /// <summary>
        /// String de conexão. Por padrão vem da seção connectionStrings do App.config;
        /// pode ser trocada em tempo de execução (usado nos testes).
        /// </summary>
        public static string StringConexao
        {
            get
            {
                if (_stringConexao == null)
                {
                    ConnectionStringSettings cfg = ConfigurationManager.ConnectionStrings[NomeConexao];
                    if (cfg == null || string.IsNullOrWhiteSpace(cfg.ConnectionString))
                        throw new RegraNegocioException("A string de conexão '" + NomeConexao + "' não foi encontrada no App.config.");
                    _stringConexao = cfg.ConnectionString;
                }
                return _stringConexao;
            }
            set { _stringConexao = value; }
        }

        /// <summary>
        /// Abre uma conexão já com o contexto da sessão. Quem chama deve fechar (using).
        /// </summary>
        public static MySqlConnection AbrirConexao()
        {
            // AllowUserVariables: sem isso o conector trata @usuario_id como parâmetro não definido.
            MySqlConnectionStringBuilder cb;
            try
            {
                cb = new MySqlConnectionStringBuilder(StringConexao);
            }
            catch (ArgumentException)
            {
                throw new RegraNegocioException("A string de conexão do App.config está inválida.");
            }
            cb.AllowUserVariables = true;
            MySqlConnection cn = new MySqlConnection(cb.ConnectionString);
            try
            {
                cn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SET @usuario_id = @u, @unidade_id = @un", cn))
                {
                    cmd.Parameters.AddWithValue("@u", SessaoBanco.UsuarioId.HasValue ? (object)SessaoBanco.UsuarioId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@un", SessaoBanco.UnidadeId.HasValue ? (object)SessaoBanco.UnidadeId.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                return cn;
            }
            catch (MySqlException ex)
            {
                cn.Dispose();
                throw Traduzir(ex);
            }
        }

        /// <summary>Executa INSERT/UPDATE/DELETE e devolve as linhas afetadas.</summary>
        public static int Executar(string sql, params MySqlParameter[] parametros)
        {
            return Rodar(cmd => cmd.ExecuteNonQuery(), sql, parametros);
        }

        /// <summary>Executa um SELECT que devolve um único valor (ou null).</summary>
        public static object Escalar(string sql, params MySqlParameter[] parametros)
        {
            object r = Rodar(cmd => cmd.ExecuteScalar(), sql, parametros);
            return r == DBNull.Value ? null : r;
        }

        /// <summary>Executa um SELECT (ou CALL que devolve linhas) e devolve uma DataTable.</summary>
        public static DataTable Consultar(string sql, params MySqlParameter[] parametros)
        {
            return Rodar(cmd =>
            {
                DataTable tabela = new DataTable();
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    da.Fill(tabela);
                return tabela;
            }, sql, parametros);
        }

        /// <summary>
        /// Executa várias operações na MESMA conexão e transação: se qualquer uma
        /// falhar, nada é gravado (ROLLBACK).
        /// </summary>
        public static void EmTransacao(Action<MySqlConnection, MySqlTransaction> trabalho)
        {
            using (MySqlConnection cn = AbrirConexao())
            using (MySqlTransaction tx = cn.BeginTransaction())
            {
                try
                {
                    trabalho(cn, tx);
                    tx.Commit();
                }
                catch (MySqlException ex)
                {
                    TentarRollback(tx);
                    throw Traduzir(ex);
                }
                catch
                {
                    TentarRollback(tx);
                    throw;
                }
            }
        }

        /// <summary>Cria um parâmetro tratando null como NULL do banco.</summary>
        public static MySqlParameter Parametro(string nome, object valor)
        {
            return new MySqlParameter(nome, valor ?? DBNull.Value);
        }

        /// <summary>Verifica se o banco responde e devolve a versão do servidor.</summary>
        public static string TestarConexao()
        {
            return Convert.ToString(Escalar("SELECT VERSION()"));
        }

        private static T Rodar<T>(Func<MySqlCommand, T> acao, string sql, MySqlParameter[] parametros)
        {
            try
            {
                using (MySqlConnection cn = AbrirConexao())
                using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                {
                    if (parametros != null)
                        cmd.Parameters.AddRange(parametros);
                    return acao(cmd);
                }
            }
            catch (MySqlException ex)
            {
                throw Traduzir(ex);
            }
        }

        private static void TentarRollback(MySqlTransaction tx)
        {
            try { tx.Rollback(); }
            catch { /* conexão já perdida: o servidor desfaz sozinho */ }
        }

        /// <summary>
        /// Converte um erro do MySQL em RegraNegocioException com texto para o usuário.
        /// 1644 = mensagem escrita nas procedures/triggers (SIGNAL SQLSTATE '45000').
        /// </summary>
        public static Exception Traduzir(MySqlException ex)
        {
            switch (ex.Number)
            {
                case 1644:
                    return new RegraNegocioException(ex.Message, ex);
                case 1062:
                    return new RegraNegocioException("Já existe um registro com esses dados.", ex);
                case 1451:
                    return new RegraNegocioException("Este registro possui vínculos (histórico) e não pode ser excluído. Inative-o.", ex);
                case 1452:
                    return new RegraNegocioException("O registro informado não existe ou foi removido.", ex);
                case 3819:
                case 4025:
                    return new RegraNegocioException("Algum valor informado não é permitido.", ex);
                case 1142:
                case 1143:
                case 1370:
                    return new RegraNegocioException("O usuário do banco não tem permissão para esta operação.", ex);
                case 1045:
                    return new RegraNegocioException("Usuário ou senha do banco de dados incorretos (verifique o App.config).", ex);
                case 1049:
                    return new RegraNegocioException("O banco 'salgados_cia' não existe. Execute o 01_schema.sql.", ex);
                case 0:
                case 1042:
                    return new RegraNegocioException("Não foi possível conectar ao banco de dados. Verifique se o MySQL está ligado.", ex);
                default:
                    return new RegraNegocioException("Erro no banco de dados (" + ex.Number + "): " + ex.Message, ex);
            }
        }
    }
}

using System;
using System.ComponentModel;

namespace erp_salgados_e_cia
{
    public enum TipoOperacaoLog
    {
        Inclusao,
        Alteracao,
        Exclusao,
        Login,
        Logout
    }

    public class LogOperacao
    {
        public LogOperacao(
            int idLog,
            DateTime dataHora,
            string usuario,
            TipoOperacaoLog operacao,
            string tabela,
            string registro,
            string detalhes)
        {
            IdLog = idLog;
            DataHora = dataHora;
            Usuario = usuario;
            Operacao = operacao;
            Tabela = tabela;
            Registro = registro;
            Detalhes = detalhes;
        }

        [Browsable(false)]
        public int IdLog { get; private set; }

        public DateTime DataHora { get; private set; }

        public string Usuario { get; private set; }

        [Browsable(false)]
        public TipoOperacaoLog Operacao { get; private set; }

        public string OperacaoTexto
        {
            get
            {
                switch (Operacao)
                {
                    case TipoOperacaoLog.Inclusao:
                        return "Inclusão";

                    case TipoOperacaoLog.Alteracao:
                        return "Alteração";

                    case TipoOperacaoLog.Exclusao:
                        return "Exclusão";

                    case TipoOperacaoLog.Login:
                        return "Login";

                    default:
                        return "Logout";
                }
            }
        }

        public string Tabela { get; private set; }

        public string Registro { get; private set; }

        public string Detalhes { get; private set; }
    }
}

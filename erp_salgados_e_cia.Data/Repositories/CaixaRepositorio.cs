using System;
using System.Data;
using erp_salgados_e_cia.Data.Connection;

namespace erp_salgados_e_cia.Data.Repositories
{
    /// <summary>
    /// Caixa (abrir, editar e fechar). As regras (um caixa aberto por unidade, um por dia,
    /// caixa fechado não muda, autorização do gerente na edição) ficam nas procedures do banco.
    /// </summary>
    public static class CaixaRepositorio
    {
        /// <summary>Caixas da unidade (ou de todas, se unidadeId for nulo), do mais antigo para o mais novo.</summary>
        public static DataTable Listar(uint? unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT c.id, un.nome AS unidade, fu.nome AS responsavel, c.data_abertura, c.data_fechamento, " +
                "c.valor_inicial, c.valor_contado_dinheiro, c.valor_contado_maquininha, c.diferenca, c.status " +
                "FROM caixa c " +
                "JOIN unidade un ON un.id = c.unidade_id " +
                "JOIN funcionario fu ON fu.id = c.responsavel_funcionario_id " +
                "WHERE (@un IS NULL OR c.unidade_id = @un) " +
                "ORDER BY c.data_abertura, c.id",
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null));
        }

        /// <summary>
        /// O que entrou em cada caixa por forma de pagamento (vw_caixa_totais): vendas já sem o troco
        /// e abatimentos de Notinha. Formas com os nomes do banco.
        /// </summary>
        public static DataTable ListarTotais(uint? unidadeId)
        {
            return BancoDados.Consultar(
                "SELECT t.caixa_id, t.forma_pagamento, t.total_vendas, t.total_abatimentos " +
                "FROM vw_caixa_totais t JOIN caixa c ON c.id = t.caixa_id " +
                "WHERE (@un IS NULL OR c.unidade_id = @un)",
                BancoDados.Parametro("@un", unidadeId.HasValue ? (object)unidadeId.Value : null));
        }

        /// <summary>Abre o caixa da unidade e devolve o id dele.</summary>
        public static uint Abrir(uint unidadeId, uint funcionarioId, decimal valorInicial)
        {
            object id = BancoDados.Escalar(
                "CALL sp_abrir_caixa(@un, @func, @valor)",
                BancoDados.Parametro("@un", unidadeId),
                BancoDados.Parametro("@func", funcionarioId),
                BancoDados.Parametro("@valor", valorInicial));

            return Convert.ToUInt32(id);
        }

        /// <summary>Corrige valor inicial e responsável de um caixa aberto (motivo e autorizador obrigatórios).</summary>
        public static void Editar(
            uint caixaId,
            decimal valorInicial,
            uint funcionarioId,
            string motivo,
            uint autorizadorId)
        {
            BancoDados.Executar(
                "CALL sp_editar_caixa(@caixa, @valor, @func, @motivo, @autorizador)",
                BancoDados.Parametro("@caixa", caixaId),
                BancoDados.Parametro("@valor", valorInicial),
                BancoDados.Parametro("@func", funcionarioId),
                BancoDados.Parametro("@motivo", motivo),
                BancoDados.Parametro("@autorizador", autorizadorId));
        }

        /// <summary>
        /// Corrige o dinheiro contado e a maquininha de um caixa já fechado (motivo e autorizador
        /// obrigatórios). O banco recalcula a diferença e anota tudo no log.
        /// </summary>
        public static void EditarFechado(
            uint caixaId,
            decimal dinheiroContado,
            decimal? maquininha,
            string motivo,
            uint autorizadorId)
        {
            BancoDados.Executar(
                "CALL sp_editar_caixa_fechado(@caixa, @contado, @maquininha, @motivo, @autorizador)",
                BancoDados.Parametro("@caixa", caixaId),
                BancoDados.Parametro("@contado", dinheiroContado),
                BancoDados.Parametro("@maquininha", maquininha.HasValue ? (object)maquininha.Value : null),
                BancoDados.Parametro("@motivo", motivo),
                BancoDados.Parametro("@autorizador", autorizadorId));
        }

        /// <summary>Fecha o caixa com o dinheiro contado e o valor da maquininha.</summary>
        public static void Fechar(uint caixaId, decimal dinheiroContado, decimal? maquininha)
        {
            BancoDados.Executar(
                "CALL sp_fechar_caixa(@caixa, @contado, @maquininha)",
                BancoDados.Parametro("@caixa", caixaId),
                BancoDados.Parametro("@contado", dinheiroContado),
                BancoDados.Parametro("@maquininha", maquininha.HasValue ? (object)maquininha.Value : null));
        }
    }
}

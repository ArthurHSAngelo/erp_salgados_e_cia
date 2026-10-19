using erp_salgados_e_cia.Domain;

namespace erp_salgados_e_cia.Data
{
    /// <summary>
    /// Camada Data: acesso ao banco de dados MySQL (conexão e repositórios).
    /// Nesta etapa (D0) a camada está vazia; esta classe existe apenas para
    /// comprovar que a camada compila e referencia a camada Domain ("Hello World").
    /// </summary>
    public static class InfoDados
    {
        public static string Mensagem()
        {
            return "Camada Data carregada (referencia a camada Domain: " + InfoDominio.NomeSistema + ").";
        }
    }
}

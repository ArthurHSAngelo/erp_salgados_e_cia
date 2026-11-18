using System;
using System.Security.Cryptography;

namespace erp_salgados_e_cia.Domain.Services
{
    /// <summary>
    /// Hash de senha no formato "sal:hash" (ambos em Base64), guardado em
    /// usuario.senha_hash. PBKDF2 com SHA-256, 10.000 iterações, sal de 16 bytes
    /// e hash de 32 bytes. O banco nunca vê a senha, só este texto.
    /// </summary>
    public static class ServicoSenha
    {
        private const int Iteracoes = 10000;
        private const int TamanhoSal = 16;
        private const int TamanhoHash = 32;

        public static string GerarHash(string senha)
        {
            byte[] sal = new byte[TamanhoSal];

            using (RNGCryptoServiceProvider gerador = new RNGCryptoServiceProvider())
            {
                gerador.GetBytes(sal);
            }

            byte[] hash;

            using (Rfc2898DeriveBytes derivador = new Rfc2898DeriveBytes(senha, sal, Iteracoes, HashAlgorithmName.SHA256))
            {
                hash = derivador.GetBytes(TamanhoHash);
            }

            return Convert.ToBase64String(sal) + ":" + Convert.ToBase64String(hash);
        }

        public static bool Verificar(string senha, string senhaHash)
        {
            if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(senhaHash))
            {
                return false;
            }

            string[] partes = senhaHash.Split(':');

            if (partes.Length != 2)
            {
                return false;
            }

            byte[] sal;
            byte[] esperado;

            try
            {
                sal = Convert.FromBase64String(partes[0]);
                esperado = Convert.FromBase64String(partes[1]);
            }
            catch (FormatException)
            {
                return false;
            }

            byte[] calculado;

            using (Rfc2898DeriveBytes derivador = new Rfc2898DeriveBytes(senha, sal, Iteracoes, HashAlgorithmName.SHA256))
            {
                calculado = derivador.GetBytes(esperado.Length);
            }

            // comparação sem parar no primeiro byte diferente
            int diferenca = 0;

            for (int i = 0; i < esperado.Length; i++)
            {
                diferenca |= esperado[i] ^ calculado[i];
            }

            return diferenca == 0;
        }
    }
}

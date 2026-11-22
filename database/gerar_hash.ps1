# Gera o hash de senha (formato "sal:hash") para a coluna usuario.senha_hash.
# Usa o mesmo algoritmo do sistema (PBKDF2, SHA-256, 10.000 iteracoes).
#
# Uso (PowerShell):
#   .\gerar_hash.ps1 -Senha "minhaSenha"
# Depois:
#   UPDATE usuario SET senha_hash = '<hash gerado>' WHERE login = 'admin';
param(
    [Parameter(Mandatory = $true)]
    [string]$Senha
)

$sal = New-Object byte[] 16
$rng = New-Object System.Security.Cryptography.RNGCryptoServiceProvider
$rng.GetBytes($sal)
$rng.Dispose()

$pbkdf2 = New-Object System.Security.Cryptography.Rfc2898DeriveBytes($Senha, $sal, 10000, [System.Security.Cryptography.HashAlgorithmName]::SHA256)
$hash = $pbkdf2.GetBytes(32)
$pbkdf2.Dispose()

[Convert]::ToBase64String($sal) + ":" + [Convert]::ToBase64String($hash)

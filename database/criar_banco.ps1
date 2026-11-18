# Cria o banco de dados do Salgados & Cia - ERP num servidor MySQL/MariaDB:
# roda o 01_schema.sql e o 02_dados_teste.sql, define a senha do usuario do programa
# (app_salgados) e confere se o programa consegue entrar.
#
# E o mesmo script usado pelo instalador. Tambem pode ser rodado a mao:
#   .\criar_banco.ps1 -Interativo
#   .\criar_banco.ps1 -Servidor localhost -Porta 3306 -UsuarioAdmin root -SenhaAdmin "..." -SenhaApp "..."
#
# ATENCAO: o 01_schema.sql APAGA e recria o banco "salgados_cia". Se ele ja existir com
# dados, o script para (codigo 3) a menos que -Forcar seja usado.
#
# Codigos de saida: 0 ok | 2 nao conectou no servidor | 3 banco ja existe com dados
#                   4 erro ao rodar os scripts | 5 o programa nao conseguiu entrar | 6 falta arquivo
param(
    [string]$Servidor = "localhost",
    [int]$Porta = 3306,
    [string]$UsuarioAdmin = "root",
    [string]$SenhaAdmin = "",
    [string]$SenhaApp = "TROQUE_ESTA_SENHA_FORTE",
    [string]$PastaScripts = "",
    [string]$DllMySql = "",
    [switch]$IncluirRede,
    [switch]$Forcar,
    [switch]$Interativo,
    [int]$EsperarSegundos = 0,
    [string]$ArquivoParametros = "",
    [string]$Log = ""
)

$ErrorActionPreference = "Stop"

function Escrever($texto) {
    Write-Output $texto
    if ($script:Log -ne "") { Add-Content -Path $script:Log -Value $texto -Encoding UTF8 }
}

function Sair($codigo, $mensagem) {
    Escrever $mensagem
    exit $codigo
}

# --- parametros vindos de um arquivo (o instalador usa isto para nao expor senhas na linha de comando) ---
if ($ArquivoParametros -ne "" -and (Test-Path $ArquivoParametros)) {
    $p = Get-Content $ArquivoParametros -Raw -Encoding UTF8 | ConvertFrom-Json
    if ($p.Servidor)      { $Servidor = [string]$p.Servidor }
    if ($p.Porta)         { $Porta = [int]$p.Porta }
    if ($p.UsuarioAdmin)  { $UsuarioAdmin = [string]$p.UsuarioAdmin }
    if ($null -ne $p.SenhaAdmin) { $SenhaAdmin = [string]$p.SenhaAdmin }
    if ($p.SenhaApp)      { $SenhaApp = [string]$p.SenhaApp }
    if ($p.IncluirRede)   { $IncluirRede = [switch]::new($true) }
    if ($p.Forcar)        { $Forcar = [switch]::new($true) }
    if ($p.EsperarSegundos) { $EsperarSegundos = [int]$p.EsperarSegundos }
    if ($p.DllMySql)      { $DllMySql = [string]$p.DllMySql }
    if ($p.PastaScripts)  { $PastaScripts = [string]$p.PastaScripts }
}

if ($Interativo) {
    $r = Read-Host "Servidor [$Servidor]"; if ($r) { $Servidor = $r }
    $r = Read-Host "Porta [$Porta]"; if ($r) { $Porta = [int]$r }
    $r = Read-Host "Usuario administrador do servidor [$UsuarioAdmin]"; if ($r) { $UsuarioAdmin = $r }
    $seg = Read-Host "Senha do administrador (pode ficar vazia)" -AsSecureString
    $SenhaAdmin = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($seg))
    $seg = Read-Host "Senha para o usuario do programa (app_salgados)" -AsSecureString
    $digitada = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($seg))
    if ($digitada) { $SenhaApp = $digitada }
    $r = Read-Host "Liberar acesso pela rede para as outras unidades? (s/N)"; if ($r -match '^[sS]') { $IncluirRede = [switch]::new($true) }
    $r = Read-Host "Se o banco ja existir, apagar e recriar? (s/N)"; if ($r -match '^[sS]') { $Forcar = [switch]::new($true) }
}

$aqui = Split-Path -Parent $MyInvocation.MyCommand.Path
if ($PastaScripts -eq "") { $PastaScripts = $aqui }

if ($SenhaApp -match "[']" -or $SenhaApp -match "\\" -or $SenhaApp -match '"') {
    Sair 6 "A senha do usuario do programa nao pode ter aspas nem barra invertida."
}

# --- arquivos necessarios ---
if ($DllMySql -eq "") {
    foreach ($c in @((Join-Path $aqui "MySql.Data.dll"), (Join-Path $aqui "..\MySql.Data.dll"))) {
        if (Test-Path $c) { $DllMySql = $c; break }
    }
}
if ($DllMySql -eq "" -or -not (Test-Path $DllMySql)) { Sair 6 "Nao encontrei o MySql.Data.dll (a biblioteca do programa). Informe com -DllMySql." }
foreach ($nome in "01_schema.sql", "02_dados_teste.sql") {
    if (-not (Test-Path (Join-Path $PastaScripts $nome))) { Sair 6 "Nao encontrei o arquivo $nome em $PastaScripts." }
}
if ($IncluirRede -and -not (Test-Path (Join-Path $PastaScripts "03_acesso_rede.sql"))) { Sair 6 "Nao encontrei o arquivo 03_acesso_rede.sql em $PastaScripts." }

[void][Reflection.Assembly]::LoadFrom((Resolve-Path $DllMySql).Path)

function Nova-Conexao($usuario, $senha, $banco) {
    $b = New-Object MySql.Data.MySqlClient.MySqlConnectionStringBuilder
    $b.Server = $Servidor
    $b.Port = [uint32]$Porta
    $b.UserID = $usuario
    $b.Password = $senha
    $b.CharacterSet = "utf8mb4"
    $b.DefaultCommandTimeout = 600
    $b.ConnectionTimeout = 15
    if ($banco) { $b.Database = $banco }
    return New-Object MySql.Data.MySqlClient.MySqlConnection($b.ConnectionString)
}

function Rodar-Sql($conn, $sql) {
    $cmd = $conn.CreateCommand(); $cmd.CommandText = $sql; return $cmd.ExecuteScalar()
}

function Rodar-Arquivo($conn, $caminho) {
    $texto = [IO.File]::ReadAllText($caminho, [Text.Encoding]::UTF8)
    $s = New-Object MySql.Data.MySqlClient.MySqlScript($conn, $texto)
    [void]$s.Execute()
}

# --- 1. conectar como administrador ---
# Com -EsperarSegundos, tenta de novo enquanto o servidor ainda esta subindo (logo apos instalar).
$limite = (Get-Date).AddSeconds($EsperarSegundos)
$erroConexao = $null
do {
    $admin = Nova-Conexao $UsuarioAdmin $SenhaAdmin $null
    try {
        $admin.Open()
        $versao = Rodar-Sql $admin "SELECT VERSION()"
        Escrever "Conectado ao servidor $Servidor`:$Porta (versao $versao)."
        $erroConexao = $null
        break
    } catch {
        $erroConexao = $_.Exception.Message
        $admin.Dispose()
        # senha errada nao melhora esperando: so repete quando o servidor nao respondeu
        if ($erroConexao -match "Access denied|using password|auth_gssapi_client|Authentication") { break }
        if ((Get-Date) -lt $limite) { Start-Sleep -Seconds 3 }
    }
} while ((Get-Date) -lt $limite)

if ($erroConexao) {
    Sair 2 ("Nao foi possivel conectar ao servidor " + $Servidor + ":" + $Porta + " com o usuario '" + $UsuarioAdmin + "'. Confira se o MySQL esta ligado, o endereco, a porta e a senha. Detalhe: " + $erroConexao)
}

try {
    # --- 2. banco ja existe? ---
    $existe = [int](Rodar-Sql $admin "SELECT COUNT(*) FROM information_schema.schemata WHERE schema_name = 'salgados_cia'")
    if ($existe -gt 0 -and -not $Forcar) {
        $temDados = 0
        try { $temDados = [int](Rodar-Sql $admin "SELECT (SELECT COUNT(*) FROM salgados_cia.usuario) + (SELECT COUNT(*) FROM salgados_cia.venda)") } catch { $temDados = 0 }
        if ($temDados -gt 0) {
            Sair 3 "O banco 'salgados_cia' ja existe e tem dados. Para apagar e recriar, confirme (opcao -Forcar)."
        }
    }

    # --- 3. scripts ---
    Escrever "Criando as tabelas e regras (01_schema.sql)..."
    Rodar-Arquivo $admin (Join-Path $PastaScripts "01_schema.sql")
    Escrever "Carregando os dados de teste (02_dados_teste.sql)..."
    Rodar-Arquivo $admin (Join-Path $PastaScripts "02_dados_teste.sql")

    # --- 4. senha do usuario do programa ---
    Rodar-Sql $admin ("ALTER USER 'app_salgados'@'localhost' IDENTIFIED BY '" + $SenhaApp + "'") | Out-Null
    if ($IncluirRede) {
        Escrever "Liberando o usuario do programa para a rede (03_acesso_rede.sql)..."
        $rede = [IO.File]::ReadAllText((Join-Path $PastaScripts "03_acesso_rede.sql"), [Text.Encoding]::UTF8)
        $rede = $rede.Replace("TROQUE_ESTA_SENHA_FORTE", $SenhaApp)
        $sr = New-Object MySql.Data.MySqlClient.MySqlScript($admin, $rede); [void]$sr.Execute()
        Rodar-Sql $admin ("ALTER USER 'app_salgados'@'%' IDENTIFIED BY '" + $SenhaApp + "'") | Out-Null
    }
    Rodar-Sql $admin "FLUSH PRIVILEGES" | Out-Null
} catch {
    Sair 4 ("Erro ao criar o banco: " + $_.Exception.Message)
} finally {
    $admin.Close()
}

# --- 5. conferir como o programa vai entrar ---
try {
    $app = Nova-Conexao "app_salgados" $SenhaApp "salgados_cia"
    $app.Open()
    $logins = [int](Rodar-Sql $app "SELECT COUNT(*) FROM usuario")
    $app.Close()
    Escrever "Banco criado. O programa consegue entrar como app_salgados ($logins logins cadastrados)."
} catch {
    Sair 5 ("O banco foi criado, mas o usuario do programa nao conseguiu entrar: " + $_.Exception.Message + " (se o servidor e outro computador, use a opcao de acesso pela rede).")
}

exit 0

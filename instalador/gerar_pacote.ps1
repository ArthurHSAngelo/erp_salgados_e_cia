# Gera o pacote RC (Release Candidate) do Salgados & Cia - ERP:
#   - compila o programa em Release;
#   - cria o instalador (Inno Setup) e a versao portatil (ZIP);
#   - reune os scripts do banco e o LEIA-ME.
#
# Uso (PowerShell, na pasta instalador):
#   .\gerar_pacote.ps1
#   .\gerar_pacote.ps1 -Versao "1.0.1" -Saida "D:\Entrega\Pacote_RC"
#
# Precisa do Visual Studio (MSBuild) e do Inno Setup 6
# (instale com:  winget install JRSoftware.InnoSetup).
# O pacote e gerado FORA do repositorio, para nao versionar arquivos compilados.
param(
    [string]$Versao = "1.0.0",
    [string]$Saida = "",
    [switch]$SemServidor   # gera um instalador menor, sem o servidor MariaDB embutido
)

$ErrorActionPreference = "Stop"

$pastaScript = Split-Path -Parent $MyInvocation.MyCommand.Path
$repo = Split-Path -Parent $pastaScript
if ([string]::IsNullOrWhiteSpace($Saida)) { $Saida = Join-Path (Split-Path -Parent $repo) "Pacote_RC" }

# --- ferramentas ---
function Achar-MSBuild {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vswhere) {
        $achado = & $vswhere -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
        if ($achado) { return $achado }
    }
    throw "MSBuild nao encontrado. Instale o Visual Studio com a carga de trabalho de desktop .NET."
}

function Achar-Iscc {
    $comando = Get-Command ISCC.exe -ErrorAction SilentlyContinue
    if ($comando) { return $comando.Source }
    foreach ($pasta in @("$env:LOCALAPPDATA\Programs\Inno Setup 6", "${env:ProgramFiles(x86)}\Inno Setup 6", "$env:ProgramFiles\Inno Setup 6")) {
        $iscc = Join-Path $pasta "ISCC.exe"
        if (Test-Path $iscc) { return $iscc }
    }
    throw "Inno Setup 6 nao encontrado. Instale com: winget install JRSoftware.InnoSetup"
}

$msbuild = Achar-MSBuild
$iscc = Achar-Iscc

# --- servidor MariaDB embutido no instalador (baixado uma vez e guardado em cache) ---
$msiNome = "mariadb-10.11.19-winx64.msi"
$msiUrl = "https://archive.mariadb.org/mariadb-10.11.19/winx64-packages/$msiNome"
$msiSha256 = "83AF9D523BF7BA989EA0031D4EBCA7E4DC21E49F435122570DDC87F0620B3A5C"
$msiLocal = $null
if (-not $SemServidor) {
    $cache = Join-Path $env:LOCALAPPDATA "SalgadosECia_cache"
    New-Item -ItemType Directory -Force $cache | Out-Null
    $msiLocal = Join-Path $cache $msiNome
    if (-not (Test-Path $msiLocal) -or (Get-FileHash $msiLocal -Algorithm SHA256).Hash -ne $msiSha256) {
        Write-Host "Baixando o servidor MariaDB 10.11 LTS (cerca de 82 MB, so na primeira vez)..."
        Invoke-WebRequest -Uri $msiUrl -OutFile $msiLocal -UseBasicParsing
    }
    if ((Get-FileHash $msiLocal -Algorithm SHA256).Hash -ne $msiSha256) {
        Remove-Item $msiLocal -Force
        throw "O arquivo baixado do MariaDB nao confere com o esperado (SHA256). Tente de novo."
    }
}

Write-Host "MSBuild : $msbuild"
Write-Host "Inno    : $iscc"
Write-Host "Saida   : $Saida"
Write-Host ""

# --- 1. compilar em Release ---
$temp = Join-Path $env:TEMP ("pacote_salgados_" + [Guid]::NewGuid().ToString("N").Substring(0, 8))
$pastaApp = Join-Path $temp "app"
New-Item -ItemType Directory -Force $pastaApp | Out-Null

Write-Host "1/4 Compilando em Release..."
& $msbuild (Join-Path $repo "erp_salgados_e_cia.sln") -restore -v:m -nologo "-p:Configuration=Release" "-p:OutDir=$pastaApp\" "-p:IntermediateOutputPath=obj_pacote\"
if ($LASTEXITCODE -ne 0) { throw "A compilacao falhou." }
Get-ChildItem $repo -Recurse -Directory -Filter obj_pacote -ErrorAction SilentlyContinue | ForEach-Object { Remove-Item $_.FullName -Recurse -Force }

if (-not (Test-Path (Join-Path $pastaApp "erp_salgados_e_cia.exe"))) { throw "O executavel nao foi gerado." }

# --- 2. pasta de saida ---
foreach ($sub in "Instalador", "Portatil", "Banco de Dados") {
    $alvo = Join-Path $Saida $sub
    if (Test-Path $alvo) { Remove-Item $alvo -Recurse -Force }
    New-Item -ItemType Directory -Force $alvo | Out-Null
}

$pastaBancoOrigem = Join-Path $repo "database"
$arquivosBanco = "01_schema.sql", "02_dados_teste.sql", "03_acesso_rede.sql", "criar_banco.ps1", "README.md", "gerar_hash.ps1"
foreach ($a in $arquivosBanco) { Copy-Item (Join-Path $pastaBancoOrigem $a) (Join-Path $Saida "Banco de Dados") -Force }

$leiame = Join-Path $pastaScript "LEIA-ME.txt"
Copy-Item $leiame (Join-Path $Saida "LEIA-ME.txt") -Force

# --- 3. instalador ---
Write-Host "2/4 Criando o instalador..."
$extraIscc = @()
if ($msiLocal) { $extraIscc += "/DArquivoMariaDB=$msiLocal" }
& $iscc "/Qp" @extraIscc "/DVersao=$Versao" "/DPastaApp=$pastaApp" "/DPastaBanco=$pastaBancoOrigem" "/DPastaSaida=$(Join-Path $Saida 'Instalador')" "/DArquivoIcone=$(Join-Path $repo 'erp_salgados_e_cia\Recursos\salgados.ico')" "/DArquivoLeiame=$leiame" (Join-Path $pastaScript "SalgadosECia.iss")
if ($LASTEXITCODE -ne 0) { throw "A criacao do instalador falhou." }

# --- 4. versao portatil (ZIP) ---
Write-Host "3/4 Criando a versao portatil..."
$nomePasta = "SalgadosECia_ERP"
$montagem = Join-Path $temp $nomePasta
New-Item -ItemType Directory -Force $montagem | Out-Null
Get-ChildItem $pastaApp -Recurse -File | Where-Object { $_.Extension -notin ".pdb", ".xml" } | ForEach-Object {
    $relativo = $_.FullName.Substring($pastaApp.Length).TrimStart('\')
    $destino = Join-Path $montagem $relativo
    New-Item -ItemType Directory -Force (Split-Path -Parent $destino) | Out-Null
    Copy-Item $_.FullName $destino -Force
}
$bancoPortatil = Join-Path $montagem "Banco de Dados"
New-Item -ItemType Directory -Force $bancoPortatil | Out-Null
foreach ($a in $arquivosBanco) { Copy-Item (Join-Path $pastaBancoOrigem $a) $bancoPortatil -Force }
Copy-Item $leiame (Join-Path $montagem "LEIA-ME.txt") -Force
# atalho de duplo clique para criar o banco num servidor MySQL/MariaDB ja existente
$assistente = "@echo off`r`nchcp 65001 >nul`r`npowershell -NoProfile -ExecutionPolicy Bypass -NoExit -File ""%~dp0Banco de Dados\criar_banco.ps1"" -Interativo`r`n"
[IO.File]::WriteAllText((Join-Path $montagem "Criar banco de dados.cmd"), $assistente, (New-Object Text.UTF8Encoding($false)))

$zip = Join-Path $Saida "Portatil\SalgadosECia_ERP_Portatil_$Versao.zip"
if (Test-Path $zip) { Remove-Item $zip -Force }
Compress-Archive -Path $montagem -DestinationPath $zip -CompressionLevel Optimal

# --- resumo ---
Remove-Item $temp -Recurse -Force
Write-Host "4/4 Pronto."
Write-Host ""
Get-ChildItem $Saida -Recurse -File | ForEach-Object {
    "{0,-70} {1,8:N0} KB" -f $_.FullName.Substring($Saida.Length + 1), ($_.Length / 1KB)
}

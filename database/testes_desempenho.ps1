# Testes de desempenho do ERP Salgados & Cia contra o banco de dados.
#
# Confere os requisitos de tempo do sistema chamando o proprio programa compilado:
#   - cadastro, consulta e registro (RNF Geral 04, RNF08.1): ate 2 segundos;
#   - listagens e relatorios (RNF Geral 04, RNF17.1, RNF18.1, RNF19.1): ate 3 segundos.
# Primeiro o script enche o banco com um volume realista (por padrao, 1.000 vendas no balcao) e,
# a cada venda, mede o tempo de registro. Depois mede as consultas e os relatorios com esse volume.
# O tempo medido e o da chamada completa do C# ao MySQL (sem desenhar as telas).
#
# ATENCAO: use um banco de TESTE. Antes de rodar, execute 01_schema.sql e 02_dados_teste.sql
# (eles apagam e recriam o banco) e deixe o caixa do dia sem abrir. O script grava vendas, entradas e
# caixa, e so pode ser repetido depois de recriar o banco.
#
# Uso (PowerShell), com a solucao ja compilada:
#   .\testes_desempenho.ps1 -Pasta "..\erp_salgados_e_cia\bin\Debug" -Porta 3306
#   (se trocou a senha do usuario app_salgados, informe -Senha "sua_senha")
#   Opcoes: -Vendas 1000  -Repeticoes 5  -Saida resultado.json
# Termina com "DESEMPENHO: N de N operacoes dentro do limite" quando tudo esta certo.
param(
    [int]$Porta = 3306,
    [Parameter(Mandatory = $true)][string]$Pasta,
    [string]$Senha = "TROQUE_ESTA_SENHA_FORTE",
    [int]$Vendas = 1000,
    [int]$Repeticoes = 5,
    [string]$Saida = ""
)
$Port = $Porta
$d = (Resolve-Path $Pasta).Path
[AppDomain]::CurrentDomain.add_AssemblyResolve({param($s,$e) $n=($e.Name -split ',')[0]; foreach($x in "$n.dll","$n.exe"){ $p=Join-Path $d $x; if(Test-Path $p){return [Reflection.Assembly]::LoadFrom($p)} } })
[void][Reflection.Assembly]::LoadFrom("$d\MySql.Data.dll")
$data=[Reflection.Assembly]::LoadFrom("$d\erp_salgados_e_cia.Data.dll")
$app=[Reflection.Assembly]::LoadFrom("$d\erp_salgados_e_cia.exe")
$bd=$data.GetType("erp_salgados_e_cia.Data.Connection.BancoDados")
$bd.GetProperty("StringConexao").SetValue($null,"Server=127.0.0.1;Port=$Port;Database=salgados_cia;Uid=app_salgados;Pwd=$Senha;CharSet=utf8mb4;")
$ds=$app.GetType("erp_salgados_e_cia.dadossistema")
$R="erp_salgados_e_cia.Data.Repositories."
$prod=$data.GetType($R+"ProdutoRepositorio")
$tItem=$app.GetType("erp_salgados_e_cia.ItemVenda"); $tPag=$app.GetType("erp_salgados_e_cia.PagamentoVenda")
$tmov=$app.GetType("erp_salgados_e_cia.movimentoestoque")

function Itens($lista){ $l=New-Object "System.Collections.Generic.List[$($tItem.FullName)]"; foreach($x in $lista){ $i=[Activator]::CreateInstance($tItem); $i.Produto=$x[0]; $i.Quantidade=[decimal]$x[1]; $i.PrecoUnitario=[decimal]$x[2]; $l.Add($i) }; ,$l }
function Pags($lista){ $l=New-Object "System.Collections.Generic.List[$($tPag.FullName)]"; foreach($x in $lista){ $p=[Activator]::CreateInstance($tPag); $p.FormaPagamento=$x[0]; $p.Valor=[decimal]$x[1]; $l.Add($p) }; ,$l }
function NovoMov($p,$q,$c,$fo,$re){ $m=[Activator]::CreateInstance($tmov); $m.Produto=$p; $m.Quantidade=[decimal]$q; $m.CustoUnitario=[decimal]$c; $m.Fornecedor=$fo; $m.Responsavel=$re; $m.Unidade="Lanchonete Centro"; $m.DataEntrada=[DateTime]::Now; $m }
function Comanda($m){ $ds.GetMethod("ComandaDaMesa").Invoke($null,@([int]$m)) }

$resultados = New-Object System.Collections.ArrayList
$fora = 0
function Registrar($nome,$limiteMs,$tempos){
    $o = $tempos | Sort-Object
    $media = ($tempos | Measure-Object -Average).Average
    $max = $o[-1]
    $p95 = $o[[math]::Min($o.Count-1,[math]::Ceiling($o.Count*0.95)-1)]
    $ok = ($max -le $limiteMs)
    if(-not $ok){ $script:fora++ }
    [void]$script:resultados.Add([pscustomobject]@{ Operacao=$nome; LimiteMs=$limiteMs; Repeticoes=$tempos.Count; MediaMs=[math]::Round($media,1); P95Ms=[math]::Round($p95,1); MaxMs=[math]::Round($max,1); Dentro=$ok })
    "{0,-56} limite {1,5} ms | media {2,8:N1} | max {3,8:N1} | x{4,-5} {5}" -f $nome,$limiteMs,$media,$max,$tempos.Count,$(if($ok){"OK"}else{"ACIMA DO LIMITE"})
}
function Medir($nome,$limiteMs,$repeticoes,[scriptblock]$acao){
    $tempos = @()
    for($i=0;$i -lt $repeticoes;$i++){
        $sw=[Diagnostics.Stopwatch]::StartNew(); [void](& $acao); $sw.Stop(); $tempos += $sw.Elapsed.TotalMilliseconds
    }
    Registrar $nome $limiteMs $tempos
}

"Ambiente: $([Environment]::OSVersion.VersionString) | $env:PROCESSOR_IDENTIFIER | $([math]::Round((Get-CimInstance Win32_ComputerSystem).TotalPhysicalMemory/1GB,1)) GB de RAM"
"Volume: $Vendas vendas no balcao; $Repeticoes repeticoes por consulta"
""
# ---------- preparo: login, caixa e estoque ----------
$a=[object[]]@("centro","123",$null,$null)
$t=@(); for($i=0;$i -lt $Repeticoes;$i++){ $sw=[Diagnostics.Stopwatch]::StartNew(); $a=[object[]]@("centro","123",$null,$null); [void]$ds.GetMethod("Autenticar").Invoke($null,$a); $sw.Stop(); $t += $sw.Elapsed.TotalMilliseconds }
Registrar "Login (autenticar e carregar cadastros)" 2000 $t
$a=[object[]]@("Ana Lima",[decimal]100,$null); $r=$ds.GetMethod("AbrirCaixa").Invoke($null,$a); if(-not $r){ "Falha ao abrir o caixa: $($a[2]). Recrie o banco (01 e 02) e rode de novo."; exit 2 }
$l=New-Object "System.Collections.Generic.List[$($tmov.FullName)]"
foreach($p in "Bolinha de queijo","Coxinha","Pastel de carne","Refrigerante","Suco de laranja"){ $l.Add((NovoMov $p 10000 2 "Distribuidora Central" "Ana Lima")) }
$a=[object[]]@($l.PSObject.BaseObject,$null)
$t=@(); $sw=[Diagnostics.Stopwatch]::StartNew(); $r=$ds.GetMethod("RegistrarEntradaEstoque").Invoke($null,$a); $sw.Stop(); $t += $sw.Elapsed.TotalMilliseconds
if(-not $r){ "Falha na entrada de estoque: $($a[1])"; exit 2 }
Registrar "Entrada de estoque com 5 itens" 2000 $t
$nome="Teste Desempenho"
$t=@(); for($i=0;$i -lt $Repeticoes;$i++){ $nm="$nome $i"; $sw=[Diagnostics.Stopwatch]::StartNew(); $id=$prod.GetMethod("Inserir").Invoke($null,[object[]]@($nm,"Salgado",[decimal]5,"Unidade",[decimal]1)); $sw.Stop(); $t += $sw.Elapsed.TotalMilliseconds; [void]$prod.GetMethod("Excluir").Invoke($null,[object[]]@([uint32]$id)) }
Registrar "Cadastro de produto" 2000 $t

# ---------- vendas: enche o banco e mede o registro de cada uma ----------
$tVenda=@(); $falhas=0
for($i=0;$i -lt $Vendas;$i++){
    $it=Itens @(@("Coxinha",2,5.00),@("Bolinha de queijo",1,4.50)).PSObject.BaseObject
    $pg=Pags @(,@("Pix",14.5))
    $a=[object[]]@([Nullable[int]]$null,$it.PSObject.BaseObject,$pg.PSObject.BaseObject,$null)
    $sw=[Diagnostics.Stopwatch]::StartNew(); $r=$ds.GetMethod("VenderNoBalcao").Invoke($null,$a); $sw.Stop()
    if($r){ $tVenda += $sw.Elapsed.TotalMilliseconds } else { $falhas++ }
}
if($falhas -gt 0){ "AVISO: $falhas vendas falharam durante o preparo." }
Registrar "Registro de venda no balcao (cada uma das $Vendas)" 2000 $tVenda

# comandas por mesa: pedido e cobranca
$tCom=@()
for($m=11;$m -lt 41;$m++){
    $it=Itens @(,@("Refrigerante",2,6.00))
    $a=[object[]]@([int]$m,$null,$it.PSObject.BaseObject,$null)
    $sw=[Diagnostics.Stopwatch]::StartNew(); $r1=$ds.GetMethod("EnviarPedidoParaComanda").Invoke($null,$a)
    $c=Comanda $m; $pg=Pags @(,@("Pix",12))
    $a=[object[]]@($c.PSObject.BaseObject,$pg.PSObject.BaseObject,$false,$null); $r2=$ds.GetMethod("CobrarComanda").Invoke($null,$a); $sw.Stop()
    if($r1 -and $r2){ $tCom += $sw.Elapsed.TotalMilliseconds }
}
Registrar "Pedido na comanda e cobranca (30 mesas)" 2000 $tCom

# ---------- consultas e relatorios com o volume ----------
Medir "Historico de vendas da unidade" 3000 $Repeticoes { $ds.GetMethod("VendasDaUnidade").Invoke($null,@()) }
Medir "Historico de caixas da unidade" 3000 $Repeticoes { $ds.GetMethod("CaixasDaUnidade").Invoke($null,@()) }
Medir "Relatorio de vendas (sem filtros)" 3000 $Repeticoes { $x=[object[]]@($null,$null,$null,$null,$null); $ds.GetMethod("GerarRelatorioVendas").Invoke($null,$x) }
Medir "Relatorio de vendas (filtro Pix)" 3000 $Repeticoes { $x=[object[]]@($null,$null,$null,"Pix",$null); $ds.GetMethod("GerarRelatorioVendas").Invoke($null,$x) }
Medir "DRE (todas as unidades)" 3000 $Repeticoes { $x=[object[]]@($null,$null,$null,$null); $ds.GetMethod("GerarDre").Invoke($null,$x) }
Medir "Painel inicial" 3000 $Repeticoes { $ds.GetMethod("MontarPainel").Invoke($null,@("Lanchonete Centro",$true)) }
Medir "Estoque completo" 3000 $Repeticoes { $ds.GetMethod("MontarRelatorioEstoque",[Type[]]@([string])).Invoke($null,@("Lanchonete Centro")) }
Medir "Movimentacoes de estoque (periodo padrao, todos os produtos)" 3000 $Repeticoes { $ds.GetMethod("ConsultarHistoricoEstoque").Invoke($null,@($null,$null,$null,$null,$null)) }
Medir "Movimentacoes de estoque de um produto" 3000 $Repeticoes { $ds.GetMethod("ConsultarHistoricoEstoque").Invoke($null,@($null,$null,$null,"Coxinha",$null)) }
Medir "Entradas de estoque da unidade" 3000 $Repeticoes { $ds.GetMethod("EntradasDaUnidade").Invoke($null,@()) }
Medir "Log de operacoes" 3000 $Repeticoes { $x=[object[]]@($null,$null,$null,$null,$null); $ds.GetMethod("ConsultarLog").Invoke($null,$x) }

""
$dentro = ($resultados | Where-Object { $_.Dentro }).Count
"DESEMPENHO: $dentro de $($resultados.Count) operacoes dentro do limite"
if($Saida -ne ""){
    [pscustomobject]@{
        Data = (Get-Date).ToString("yyyy-MM-dd HH:mm")
        Ambiente = "$([Environment]::OSVersion.VersionString) | $env:PROCESSOR_IDENTIFIER | $([math]::Round((Get-CimInstance Win32_ComputerSystem).TotalPhysicalMemory/1GB,1)) GB de RAM"
        Vendas = $Vendas; Repeticoes = $Repeticoes; Resultados = $resultados
    } | ConvertTo-Json -Depth 4 | Set-Content -Path $Saida -Encoding UTF8
}
if($fora -gt 0){ exit 1 }

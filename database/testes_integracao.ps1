# Testes de integracao do ERP Salgados & Cia contra o banco de dados.
#
# Exercita as regras de verdade (cadastros, caixa, estoque, comandas, vendas, Notinha,
# cancelamentos, relatorios, DRE, painel e log) chamando o proprio programa compilado.
#
# ATENCAO: use um banco de TESTE. Antes de rodar, execute 01_schema.sql e 02_dados_teste.sql
# (eles apagam e recriam o banco) e deixe o caixa do dia fechado/sem abrir. Os testes gravam
# dados (vendas, entradas, perdas, caixa) e so podem ser repetidos depois de recriar o banco.
#
# Uso (PowerShell), com a solucao ja compilada:
#   .\testes_integracao.ps1 -Pasta "..\erp_salgados_e_cia\bin\Debug" -Porta 3306
#   (se trocou a senha do usuario app_salgados, informe -Senha "sua_senha")
# Termina com "PASSOU: N   FALHOU: 0" quando tudo esta certo.
param(
    [int]$Porta = 3306,
    [Parameter(Mandatory = $true)][string]$Pasta,
    [string]$Senha = "TROQUE_ESTA_SENHA_FORTE"
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
$prod=$data.GetType($R+"ProdutoRepositorio"); $forn=$data.GetType($R+"FornecedorRepositorio"); $cli=$data.GetType($R+"ClienteRepositorio")
$fun=$data.GetType($R+"FuncionarioRepositorio"); $uni=$data.GetType($R+"UnidadeRepositorio"); $usu=$data.GetType($R+"UsuarioRepositorio")
$tItem=$app.GetType("erp_salgados_e_cia.ItemVenda"); $tPag=$app.GetType("erp_salgados_e_cia.PagamentoVenda")
$tmov=$app.GetType("erp_salgados_e_cia.movimentoestoque"); $tmot=$app.GetType("erp_salgados_e_cia.MotivoPerda"); $tipoOp=$app.GetType("erp_salgados_e_cia.TipoOperacaoLog")
$ok=0; $falha=0
function Ver($nome,$obtido,$esperado){ $o="$obtido".Trim(); $e="$esperado".Trim(); if($o -eq $e){ $script:ok++ } else { $script:falha++; "FALHOU  {0}: esperado '{1}' obtido '{2}'" -f $nome,$e,$o } }
function Msg($r,$a,$i){ "$r | " + $a[$i] }
function Itens($lista){ $l=New-Object "System.Collections.Generic.List[$($tItem.FullName)]"; foreach($x in $lista){ $i=[Activator]::CreateInstance($tItem); $i.Produto=$x[0]; $i.Quantidade=[decimal]$x[1]; $i.PrecoUnitario=[decimal]$x[2]; $l.Add($i) }; ,$l }
function Pags($lista){ $l=New-Object "System.Collections.Generic.List[$($tPag.FullName)]"; foreach($x in $lista){ $p=[Activator]::CreateInstance($tPag); $p.FormaPagamento=$x[0]; $p.Valor=[decimal]$x[1]; $l.Add($p) }; ,$l }
function Prod($n){ (($ds.GetField("produtos").GetValue($null)) | Where-Object { $_.Nome -eq $n }) }
function Saldo($n){ [decimal]($ds.GetMethod("SaldoNoBanco").Invoke($null,[object[]]@((Prod $n).PSObject.BaseObject))) }
function Comanda($m){ $ds.GetMethod("ComandaDaMesa").Invoke($null,@([int]$m)) }
function Cliente($id){ (($ds.GetField("clientes").GetValue($null)) | Where-Object { $_.IdCliente -eq $id }) }
function Chama($tipo,$metodo,$args1){ try { return ,$tipo.GetMethod($metodo).Invoke($null,[object[]]$args1) } catch { $m=$_.Exception.InnerException; if($m -eq $null){$m=$_.Exception}; return "ERRO: " + $m.Message } }

# ---------- 1. login e cadastros ----------
$a=[object[]]@("centro","123",$null,$null); Ver "login centro" ($ds.GetMethod("Autenticar").Invoke($null,$a)) "True"
$a=[object[]]@("admin","errada",$null,$null); Ver "login senha errada" ($ds.GetMethod("Autenticar").Invoke($null,$a)) "False"
$a=[object[]]@("centro","123",$null,$null); [void]$ds.GetMethod("Autenticar").Invoke($null,$a)
Ver "cadastros carregados (un/prod/forn/cli/func/usu)" ((("unidades","produtos","fornecedores","clientes","funcionarios","usuarios" | ForEach-Object { ($ds.GetField($_).GetValue($null)).Count }) -join "/")) "2/5/2/2/3/5"
$id=Chama $prod "Inserir" @("Empada","Salgado",[decimal]6.5,"Unidade",[decimal]4); Ver "produto novo" ([int]$id -gt 5) "True"
Ver "produto duplicado" (Chama $prod "Inserir" @("Empada","Salgado",[decimal]6.5,"Unidade",[decimal]4)) "ERRO: Já existe um registro com esses dados."
Ver "produto preco zero" (Chama $prod "Inserir" @("Zero","Salgado",[decimal]0,"Unidade",[decimal]1)) "ERRO: Algum valor informado não é permitido."
[void](Chama $prod "Excluir" @([uint32]$id[0]))
$f=Chama $forn "Inserir" @("Forn Teste","33.444.555/0001-66",""); Ver "fornecedor CNPJ duplicado" (Chama $forn "Inserir" @("Outro","33.444.555/0001-66","")) "ERRO: Já existe um registro com esses dados."; [void](Chama $forn "Excluir" @([uint32]$f[0]))
$c1=Chama $cli "Inserir" @("Sem Cpf A","",""); $c2=Chama $cli "Inserir" @("Sem Cpf B","",""); Ver "dois clientes sem CPF" (($c1 -isnot [string]) -and ($c2 -isnot [string])) "True"; [void](Chama $cli "Excluir" @([uint32]$c1[0])); [void](Chama $cli "Excluir" @([uint32]$c2[0]))
Ver "unidade Centro nao pode ser excluida" (Chama $uni "Excluir" @([uint32]1)) "ERRO: Este registro possui vínculos (histórico) e não pode ser excluído. Inative-o."
Ver "2o login base na unidade" (Chama $usu "Inserir" @("centro2","x:y","Atendente",[uint32]1)) "ERRO: Já existe um registro com esses dados."

# ---------- 2. caixa ----------
Ver "sem caixa aberto" ($ds.GetMethod("CaixaAberto").Invoke($null,@()) -eq $null) "True"
$a=[object[]]@("Ana Lima",[decimal]100,$null); Ver "abrir caixa" (Msg ($ds.GetMethod("AbrirCaixa").Invoke($null,$a)) $a 2) "True | "
$a=[object[]]@("Ana Lima",[decimal]10,$null); Ver "abrir 2o caixa" (Msg ($ds.GetMethod("AbrirCaixa").Invoke($null,$a)) $a 2) "False | Ja existe um caixa aberto nesta unidade."
$cx=$ds.GetMethod("CaixaAberto").Invoke($null,@())
$a=[object[]]@($cx.PSObject.BaseObject,[decimal]120,"Carlos Souza","Troco errado","centro",$null); Ver "editar caixa com atendente" (Msg ($ds.GetMethod("EditarCaixa").Invoke($null,$a)) $a 5) "False | Apenas Administrador ou Gerente pode autorizar."
$a=[object[]]@($cx.PSObject.BaseObject,[decimal]120,"Carlos Souza","Troco errado","gerente1",$null); Ver "editar caixa com gerente" (Msg ($ds.GetMethod("EditarCaixa").Invoke($null,$a)) $a 5) "True | "
$cx=$ds.GetMethod("CaixaAberto").Invoke($null,@()); Ver "caixa editado" ("$($cx.Responsavel) $($cx.ValorInicial)") "Carlos Souza 120.00"

# ---------- 3. estoque ----------
Ver "saldo inicial coxinha/refri" ("$(Saldo 'Coxinha')/$(Saldo 'Refrigerante')") "50.00/50.00"
function NovoMov($p,$q,$c,$fo,$re){ $m=[Activator]::CreateInstance($tmov); $m.Produto=$p; $m.Quantidade=[decimal]$q; $m.CustoUnitario=[decimal]$c; $m.Fornecedor=$fo; $m.Responsavel=$re; $m.Unidade="Lanchonete Centro"; $m.DataEntrada=[DateTime]::Now; $m }
$l=New-Object "System.Collections.Generic.List[$($tmov.FullName)]"; $l.Add((NovoMov "Coxinha" 10 2.5 "Distribuidora Central" "Ana Lima")); $l.Add((NovoMov "Refrigerante" 5.5 3.1 "Distribuidora Central" "Ana Lima"))
$a=[object[]]@($l.PSObject.BaseObject,$null); Ver "entrada 2 itens" (Msg ($ds.GetMethod("RegistrarEntradaEstoque").Invoke($null,$a)) $a 1) "True | "
Ver "saldos apos entrada" ("$(Saldo 'Coxinha')/$(Saldo 'Refrigerante')") "60.00/55.50"
Ver "entradas da unidade" ($ds.GetMethod("EntradasDaUnidade").Invoke($null,@())).Count "2"
$l2=New-Object "System.Collections.Generic.List[$($tmov.FullName)]"; $l2.Add((NovoMov "Coxinha" 1 1 "Distribuidora Central" "Bia Rocha"))
$a=[object[]]@($l2.PSObject.BaseObject,$null); Ver "entrada com funcionario de outra unidade" (Msg ($ds.GetMethod("RegistrarEntradaEstoque").Invoke($null,$a)) $a 1) "False | Funcionário não encontrado ou inativo nesta unidade."
$a=[object[]]@("Coxinha",[decimal]4,[Enum]::Parse($tmot,"Vencido"),[DateTime]::Today,"Carlos Souza",$null); Ver "perda 4" (Msg ($ds.GetMethod("RegistrarPerda").Invoke($null,$a)) $a 5) "True | "
$a=[object[]]@("Coxinha",[decimal]999,[Enum]::Parse($tmot,"Danificado"),[DateTime]::Today,"Carlos Souza",$null); Ver "perda maior que saldo" (Msg ($ds.GetMethod("RegistrarPerda").Invoke($null,$a)) $a 5) "False | Quantidade acima do saldo disponível (56,00)."
Ver "perdas da unidade" ($ds.GetMethod("PerdasDaUnidade").Invoke($null,@("Lanchonete Centro",30))).Count "1"
$it=$ds.GetMethod("MontarItensInventario").Invoke($null,@()); foreach($i in $it){ if($i.Produto -eq "Coxinha"){ $i.QuantidadeContada=[decimal]50; $i.Motivo="Contagem" } }
$a=[object[]]@($it,"obs",0,$null,"Ana Lima"); Ver "inventario" ("$($ds.GetMethod('EfetivarInventario').Invoke($null,$a)) ajustes=$($a[2])") "True ajustes=1"
Ver "saldo coxinha apos inventario" (Saldo 'Coxinha') "50.00"
$h=$ds.GetMethod("ConsultarHistoricoEstoque").Invoke($null,@($null,$null,$null,"Coxinha",$null)); Ver "extrato coxinha (entrada, perda, ajuste)" (($h | ForEach-Object { "$($_.Tipo):$($_.SaldoApos)" }) -join " ") "Ajuste:50.00 Perda:56.00 Entrada:60.00"

# ---------- 4. comandas, vendas, notinha ----------
$a=[object[]]@([int]3,$null,(Itens @(@("Coxinha",5,5.00),@("Refrigerante",2,6.00))).PSObject.BaseObject,$null); Ver "pedido mesa 3" (Msg ($ds.GetMethod("EnviarPedidoParaComanda").Invoke($null,$a)) $a 3) "True | "
$a=[object[]]@([int]3,$null,(Itens @(,@("Coxinha",2,5.00))).PSObject.BaseObject,$null); [void]$ds.GetMethod("EnviarPedidoParaComanda").Invoke($null,$a)
$c=Comanda 3; Ver "comanda mesa 3 (itens/total)" ("$($c.Itens.Count) / $($c.Total)") "2 / 47.0000"
Ver "saldos apos pedido" ("$(Saldo 'Coxinha')/$(Saldo 'Refrigerante')") "43.00/53.50"
$a=[object[]]@([int]3,$null,(Itens @(,@("Coxinha",999,5.00))).PSObject.BaseObject,$null); Ver "pedido sem estoque" (Msg ($ds.GetMethod("EnviarPedidoParaComanda").Invoke($null,$a)) $a 3) "False | Estoque insuficiente para o produto."
$a=[object[]]@($c.PSObject.BaseObject,[Nullable[int]]1,$null); Ver "cliente na comanda" (Msg ($ds.GetMethod("TrocarClienteDaComanda").Invoke($null,$a)) $a 2) "True | "
$c=Comanda 3
$a=[object[]]@($c.PSObject.BaseObject,(Pags @(,@("Dinheiro",20))).PSObject.BaseObject,$true,$null); Ver "pagamento parcial" (Msg ($ds.GetMethod("CobrarComanda").Invoke($null,$a)) $a 3) "True | "
$c=Comanda 3; Ver "pago/falta" ("$($c.TotalPago)/$($c.Falta)") "20.00/27.0000"
$a=[object[]]@($c.PSObject.BaseObject,$null); Ver "cancelar comanda com pagamento" (Msg ($ds.GetMethod("CancelarComanda").Invoke($null,$a)) $a 1) "False | Cancelamento bloqueado: esta comanda ja possui pagamentos."
$a=[object[]]@($c.PSObject.BaseObject,(Pags @(,@("Pix",5))).PSObject.BaseObject,$false,$null); Ver "cobrar valor insuficiente" (Msg ($ds.GetMethod("CobrarComanda").Invoke($null,$a)) $a 3) "False | Ainda faltam R$ 22,00 para cobrir o total da venda."
$a=[object[]]@($c.PSObject.BaseObject,(Pags @(@("Notinha",10),@("Dinheiro",20))).PSObject.BaseObject,$false,$null); Ver "cobrar com Notinha e troco" (Msg ($ds.GetMethod("CobrarComanda").Invoke($null,$a)) $a 3) "True | "
Ver "mesa 3 fechada" ((Comanda 3) -eq $null) "True"
Ver "saldo notinha Maria" ([decimal]($ds.GetMethod("SaldoNotinha").Invoke($null,@([int]1)))) "10.00"
$a=[object[]]@([Nullable[int]]$null,(Itens @(,@("Bolinha de queijo",2,4.50))).PSObject.BaseObject,(Pags @(,@("Pix",9))).PSObject.BaseObject,$null); Ver "balcao pix" (Msg ($ds.GetMethod("VenderNoBalcao").Invoke($null,$a)) $a 3) "True | "
$a=[object[]]@([Nullable[int]]$null,(Itens @(,@("Bolinha de queijo",1,4.50))).PSObject.BaseObject,(Pags @(,@("Notinha",4.5))).PSObject.BaseObject,$null); Ver "balcao notinha sem cliente" (Msg ($ds.GetMethod("VenderNoBalcao").Invoke($null,$a)) $a 3) "False | Para usar Notinha, selecione um cliente na venda."
$a=[object[]]@([Nullable[int]]$null,(Itens @(,@("Bolinha de queijo",100,4.50))).PSObject.BaseObject,(Pags @(,@("Pix",450))).PSObject.BaseObject,$null); Ver "balcao sem estoque" (Msg ($ds.GetMethod("VenderNoBalcao").Invoke($null,$a)) $a 3) "False | Estoque insuficiente para o produto."
$maria=Cliente 1
$a=[object[]]@($maria.PSObject.BaseObject,(Pags @(,@("Dinheiro",4))).PSObject.BaseObject,$null); Ver "abatimento 4" (Msg ($ds.GetMethod("RegistrarAbatimentoNotinha").Invoke($null,$a)) $a 2) "True | "
$a=[object[]]@($maria.PSObject.BaseObject,(Pags @(,@("Dinheiro",10))).PSObject.BaseObject,$null); [void]$ds.GetMethod("RegistrarAbatimentoNotinha").Invoke($null,$a)
Ver "saldo notinha zerado" ([decimal]($ds.GetMethod("SaldoNotinha").Invoke($null,@([int]1)))) "0.00"
[void]$ds.GetMethod("CarregarClientes").Invoke($null,@()); Ver "Cliente.SaldoNotinha (cache)" ((Cliente 1).SaldoNotinha) "0.00"
$mov=$ds.GetMethod("MovimentosNotinhaDoCliente").Invoke($null,@([int]1)); Ver "historico notinha (tipos)" (($mov | ForEach-Object { $_.Tipo }) -join ",") "Abatimento,Abatimento,Notinha"
Ver "detalhe da notinha" (($mov | Where-Object { $_.Tipo -eq 'Notinha' }).Detalhe) "7x Coxinha, 2x Refrigerante"
$vs=$ds.GetMethod("VendasDaUnidade").Invoke($null,@()); Ver "vendas da unidade" $vs.Count "2"
$vN=$vs | Where-Object { $_.NumeroMesa -eq 3 }; $vB=$vs | Where-Object { $_.NumeroMesa -eq 0 }
Ver "venda 1 (total/troco/pagto)" ("$($vN.ValorTotal) $($vN.Troco) $($vN.FormaPagamento)") "47.00 3.00 Dinheiro R$ 40,00 + Notinha R$ 10,00"
$a=[object[]]@($vN.PSObject.BaseObject,"erro","gerente1",$null); Ver "cancelar venda com notinha abatida" (Msg ($ds.GetMethod("CancelarVenda").Invoke($null,$a)) $a 3) "False | Venda com notinha ja abatida: nao pode ser cancelada."
$a=[object[]]@($vB.PSObject.BaseObject,"desistiu","centro",$null); Ver "cancelar com atendente" (Msg ($ds.GetMethod("CancelarVenda").Invoke($null,$a)) $a 3) "False | Apenas Administrador ou Gerente pode cancelar vendas."
$a=[object[]]@($vB.PSObject.BaseObject,"desistiu","gerente1",$null); Ver "cancelar balcao" (Msg ($ds.GetMethod("CancelarVenda").Invoke($null,$a)) $a 3) "True | "
Ver "estoque bolinha apos cancelar" (Saldo 'Bolinha de queijo') "50.00"
$a=[object[]]@([int]5,$null,(Itens @(,@("Coxinha",2,5.00))).PSObject.BaseObject,$null); [void]$ds.GetMethod("EnviarPedidoParaComanda").Invoke($null,$a)
$a=[object[]]@((Comanda 5).PSObject.BaseObject,$null); Ver "cancelar comanda sem pagamento" (Msg ($ds.GetMethod("CancelarComanda").Invoke($null,$a)) $a 1) "True | "
Ver "estoque coxinha apos cancelar comanda" (Saldo 'Coxinha') "43.00"

# ---------- 5. fechamento do caixa ----------
$cx=$ds.GetMethod("CaixaAberto").Invoke($null,@())
Ver "caixa: esperado dinheiro / vendas / notinha / abatimentos" ("$($cx.DinheiroEsperado) / $($cx.TotalVendas) / $($cx.TotalNotinha) / $($cx.TotalAbatimentos)") "167.00 / 37.00 / 10.00 / 10.00"
$a=[object[]]@($cx.PSObject.BaseObject,[decimal]167,[decimal]0,$null); Ver "fechar caixa" (Msg ($ds.GetMethod("FecharCaixa").Invoke($null,$a)) $a 3) "True | "
$cxs=$ds.GetMethod("CaixasDaUnidade").Invoke($null,@()); Ver "caixa fechado (status/contado/diferenca)" ("$($cxs[0].Status) $($cxs[0].ValorContado) $($cxs[0].Diferenca)") "Fechado 167.00 0.00"
$a=[object[]]@("Ana Lima",[decimal]10,$null); Ver "reabrir no mesmo dia" (Msg ($ds.GetMethod("AbrirCaixa").Invoke($null,$a)) $a 2) "False | Esta unidade ja teve um caixa hoje (um caixa por dia)."

# ---------- 6. relatorios, DRE, painel, log ----------
function Rel($un,$forma,$pr){ $a=[object[]]@($null,$null,$un,$forma,$pr); $ds.GetMethod("GerarRelatorioVendas").Invoke($null,$a) }
$r=Rel $null $null $null; Ver "relatorio (qtd/fat/recebido/notinha)" ("$($r.Resumo.QuantidadeVendas) $($r.Resumo.Faturamento) $($r.Resumo.Recebido) $($r.Resumo.EmNotinha)") "1 47.00 37.00 10.00"
Ver "relatorio formas" (($r.Formas | ForEach-Object { "$($_.Forma)=$($_.Valor)" }) -join " ") "Dinheiro=37.00 Pix=0 Crédito=0 Débito=0 Vale Alimentação=0 Notinha=10.00"
Ver "relatorio filtro Pix" (Rel $null "Pix" $null).Resumo.QuantidadeVendas "0"
Ver "relatorio unidade Norte" (Rel "Lanchonete Norte" $null $null).Resumo.QuantidadeVendas "0"
$a=[object[]]@($null,$null,$null,$null); $dre=$ds.GetMethod("GerarDre").Invoke($null,$a)
$u=$dre.Unidades | Where-Object { $_.Unidade -eq "Lanchonete Centro" }
Ver "DRE Centro (receita/despesa/perdas/resultado)" ("$([decimal]$u.Receita) $([decimal]$u.Despesas) $([decimal]$u.Perdas) $([decimal]$u.Resultado)") "47.00 42.0500 10.0000 4.9500"
$p=$ds.GetMethod("MontarPainel").Invoke($null,@("Lanchonete Centro",$true))
Ver "painel (caixa/vendas/faturamento)" ("$($p.StatusCaixa) $($p.VendasHoje) $($p.FaturamentoHoje)") "Fechado 1 47.00"
Ver "painel ultimas vendas" (($p.UltimasVendas | ForEach-Object { "#$($_.Venda) $($_.Status)" }) -join " ") "#2 Cancelada #1 Concluída"
$a=[object[]]@($null,$null,"gerente1",$null,$null); $lg=$ds.GetMethod("ConsultarLog").Invoke($null,$a); Ver "log do gerente1" (($lg | ForEach-Object { "$($_.OperacaoTexto) $($_.Registro)" }) -join "|") "Alteração Venda 2"
Ver "log: tabelas" (($ds.GetMethod("TabelasDoLog").Invoke($null,@())) -join ",") "abatimento_notinha,caixa,cliente,comanda,entrada_estoque,fornecedor,inventario,perda,produto,usuario,venda"

# ---------- 7. bloqueio de login por tentativas ----------
$cnTeste = New-Object MySql.Data.MySqlClient.MySqlConnection("Server=127.0.0.1;Port=$Port;Database=salgados_cia;Uid=app_salgados;Pwd=$Senha;CharSet=utf8mb4;"); $cnTeste.Open()
function SqlEscalar($q){ $c=$cnTeste.CreateCommand(); $c.CommandText=$q; "$($c.ExecuteScalar())" }
function SqlExec($q){ $c=$cnTeste.CreateCommand(); $c.CommandText=$q; [void]$c.ExecuteNonQuery() }
function Tentar($senha){ $a=[object[]]@("teste_bloqueio",$senha,$null,$null); $r=$ds.GetMethod("Autenticar").Invoke($null,$a); "$r | " + $a[3] }
$hashBloq=$ds.GetMethod("GerarHashSenha").Invoke($null,@("abc123"))
[void](Chama $usu "Inserir" @("teste_bloqueio",$hashBloq,"Gerente",$null))
$estado="SELECT CONCAT(tentativas_falhas,'/',IF(bloqueado_ate IS NULL,'livre','bloqueado')) FROM usuario WHERE login='teste_bloqueio'"
Ver "bloqueio: 1a senha errada" (Tentar "errada") "False | Login ou senha inválidos."
1..3 | ForEach-Object { [void](Tentar "errada") }
Ver "bloqueio: 4 senhas erradas, ainda sem bloqueio" (SqlEscalar $estado) "4/livre"
Ver "bloqueio: 5a senha errada bloqueia o login" (Tentar "errada") "False | Acesso bloqueado por muitas tentativas incorretas. Tente novamente em 15 minutos."
Ver "bloqueio: senha certa durante o bloqueio" (Tentar "abc123") "False | Acesso bloqueado por muitas tentativas incorretas. Tente novamente em 15 minutos."
Ver "bloqueio: anotado no log" (SqlEscalar "SELECT COUNT(*) FROM log_operacao WHERE tabela='usuario' AND registro='teste_bloqueio' AND detalhes LIKE 'Acesso bloqueado%'") "1"
SqlExec "UPDATE usuario SET bloqueado_ate = NOW() - INTERVAL 1 MINUTE WHERE login='teste_bloqueio'"
Ver "bloqueio: depois do prazo, a senha certa entra" (Tentar "abc123") "True |"
Ver "bloqueio: login certo zera a contagem" (SqlEscalar $estado) "0/livre"
$cnTeste.Close()

# ---------- 8. vale alimentacao por unidade ----------
$cnTeste = New-Object MySql.Data.MySqlClient.MySqlConnection("Server=127.0.0.1;Port=$Port;Database=salgados_cia;Uid=app_salgados;Pwd=$Senha;CharSet=utf8mb4;"); $cnTeste.Open()
function SqlTenta($q){ try { SqlExec $q; "ok" } catch { $m=$_.Exception.InnerException; if($m -eq $null){$m=$_.Exception}; $m.Message } }
Ver "vale: Centro aceita e Norte nao" (SqlEscalar "SELECT GROUP_CONCAT(aceita_vale_alimentacao ORDER BY id SEPARATOR '/') FROM unidade") "1/0"
SqlExec "INSERT INTO comanda (unidade_id, numero_mesa) VALUES (2, 0)"
$cNorte = SqlEscalar "SELECT MAX(id) FROM comanda WHERE unidade_id = 2"
Ver "vale: unidade que nao aceita recusa" (SqlTenta "INSERT INTO pagamento_comanda (comanda_id, forma_pagamento, valor) VALUES ($cNorte, 'ValeAlimentacao', 5)") "Esta unidade nao aceita vale alimentacao."
SqlExec "INSERT INTO comanda (unidade_id, numero_mesa) VALUES (1, 0)"
$cCentro = SqlEscalar "SELECT MAX(id) FROM comanda WHERE unidade_id = 1"
Ver "vale: unidade que aceita grava" (SqlTenta "INSERT INTO pagamento_comanda (comanda_id, forma_pagamento, valor) VALUES ($cCentro, 'ValeAlimentacao', 5)") "ok"
[void](Chama $uni "Atualizar" @([uint32]2,"Lanchonete Norte","22.333.444/0001-55","Avenida Norte","200",$true,$true))
Ver "vale: Norte passa a aceitar e grava" (SqlTenta "INSERT INTO pagamento_comanda (comanda_id, forma_pagamento, valor) VALUES ($cNorte, 'ValeAlimentacao', 5)") "ok"
Ver "vale: mudanca da unidade anotada no log" (SqlEscalar "SELECT COUNT(*) FROM log_operacao WHERE tabela='unidade' AND registro_id=2 AND detalhes LIKE '%vale alimentacao: nao aceita -> aceita%'") "1"
$tCaixa=$app.GetType("erp_salgados_e_cia.Caixa"); $cx=[Activator]::CreateInstance($tCaixa)
$cx.TotaisPorForma.Add("Crédito",[decimal]10); $cx.TotaisPorForma.Add("Débito",[decimal]5); $cx.TotaisPorForma.Add("Vale Alimentação",[decimal]7)
Ver "vale: maquininha esperada soma o vale" $cx.MaquininhaEsperada "22"
$cnTeste.Close()

# ---------- 9. correcao de caixa fechado ----------
$cnTeste = New-Object MySql.Data.MySqlClient.MySqlConnection("Server=127.0.0.1;Port=$Port;Database=salgados_cia;Uid=app_salgados;Pwd=$Senha;CharSet=utf8mb4;"); $cnTeste.Open()
function EditaFechado($contado,$maq,$motivo,$login){ $c=($ds.GetMethod("CaixasDaUnidade").Invoke($null,@()))[0]; $a=[object[]]@($c.PSObject.BaseObject,[decimal]$contado,[decimal]$maq,$motivo,$login,$null); Msg ($ds.GetMethod("EditarCaixaFechado").Invoke($null,$a)) $a 5 }
function CaixaFechado(){ $c=($ds.GetMethod("CaixasDaUnidade").Invoke($null,@()))[0]; "$($c.Status) $($c.ValorContado) $($c.ValorMaquininha) $($c.Diferenca)" }
Ver "fechado: estado inicial (status/contado/maquininha/diferenca)" (CaixaFechado) "Fechado 167.00 0.00 0.00"
Ver "fechado: atendente nao autoriza" (EditaFechado 150 5 "Contagem errada" "centro") "False | Apenas Administrador ou Gerente pode autorizar."
Ver "fechado: autorizador inexistente" (EditaFechado 150 5 "Contagem errada" "fantasma") "False | Autorizador não encontrado."
Ver "fechado: motivo obrigatorio" (EditaFechado 150 5 "  " "gerente1") "False | Informe o motivo da edição."
Ver "fechado: sem alteracao" (EditaFechado 167 0 "Conferi de novo" "gerente1") "False | Nenhuma alteração foi feita."
Ver "fechado: dinheiro negativo" (EditaFechado -1 0 "Erro" "gerente1") "False | Informe o valor contado em dinheiro (zero ou maior)."
Ver "fechado: maquininha negativa" (EditaFechado 150 -1 "Erro" "gerente1") "False | O valor da maquininha não pode ser negativo."
Ver "fechado: nada mudou com as recusas" (CaixaFechado) "Fechado 167.00 0.00 0.00"
Ver "fechado: gerente corrige" (EditaFechado 150 5 "Contagem errada" "gerente1") "True | "
Ver "fechado: diferenca recalculada" (CaixaFechado) "Fechado 150.00 5.00 -17.00"
Ver "fechado: administrador corrige" (EditaFechado 160 5 "Segunda conferencia" "admin") "True | "
Ver "fechado: diferenca apos 2a correcao" (CaixaFechado) "Fechado 160.00 5.00 -7.00"
Ver "fechado: correcoes anotadas no log" (SqlEscalar "SELECT COUNT(*) FROM log_operacao WHERE tabela='caixa' AND detalhes LIKE 'correcao de caixa fechado%autorizado por: gerente1' OR (tabela='caixa' AND detalhes LIKE 'correcao de caixa fechado%autorizado por: admin')") "2"
Ver "fechado: log guarda antes e depois" (SqlEscalar "SELECT COUNT(*) FROM log_operacao WHERE tabela='caixa' AND detalhes LIKE 'correcao de caixa fechado; dinheiro contado: 167.00 -> 150.00; maquininha: 0.00 -> 5.00; diferenca: 0.00 -> -17.00; motivo: Contagem errada; autorizado por: gerente1'") "1"
Ver "fechado: usuario do programa nao altera a tabela direto" ((SqlTenta "UPDATE caixa SET valor_contado_dinheiro = 1 WHERE status = 'Fechado'") -like "UPDATE command denied*") "True"
$cnTeste.Close()

"PASSOU: $ok   FALHOU: $falha"



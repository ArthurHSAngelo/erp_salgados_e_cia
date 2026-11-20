; Instalador do Salgados & Cia - ERP (Inno Setup 6)
; Não compile direto: use instalador\gerar_pacote.ps1, que compila o programa e passa
; as pastas por parâmetro (/DPastaApp, /DPastaBanco, /DPastaSaida, /DArquivoIcone,
; /DArquivoLeiame e, opcionalmente, /DArquivoMariaDB com o instalador do servidor).
;
; O instalador cuida do banco de dados de três jeitos (escolha na tela "Banco de dados"):
;   1) instala um servidor MariaDB junto, cria o banco e configura o programa;
;   2) usa um servidor que já existe, cria o banco nele e configura o programa;
;   3) o banco já está pronto: só configura a conexão do programa.
; Para instalação silenciosa (testes): /Modo=novo|existente|configurar /Servidor= /Porta=
; /UsuarioAdmin= /SenhaAdmin= /SenhaApp= /Rede=1 /Forcar=1 /Banco= /Usuario= /Senha=

#define NomeApp "Salgados & Cia - ERP"
#ifndef Versao
  #define Versao "1.1.0"
#endif
#define NomeServico "MariaDBSalgados"

[Setup]
AppId={{6E2B4C8A-3D17-4F5B-9A60-C1D2E3F40B57}
AppName={#NomeApp}
AppVersion={#Versao}
AppVerName={#NomeApp} {#Versao}
AppPublisher=Projeto Extensionista IV - UEMG
VersionInfoVersion={#Versao}.0
DefaultDirName={autopf}\SalgadosECia-ERP
DefaultGroupName={#NomeApp}
DisableProgramGroupPage=yes
OutputDir={#PastaSaida}
OutputBaseFilename=SalgadosECia_ERP_Setup_{#Versao}
SetupIconFile={#ArquivoIcone}
UninstallDisplayIcon={app}\erp_salgados_e_cia.exe
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=dialog commandline
InfoBeforeFile={#ArquivoLeiame}
ShowLanguageDialog=no
DiskSpanning=no

[Languages]
Name: "pt"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"

[Tasks]
Name: "iconedesktop"; Description: "Criar um atalho na área de trabalho"; GroupDescription: "Atalhos:"

[Files]
Source: "{#PastaApp}\*"; DestDir: "{app}"; Excludes: "*.pdb,*.xml"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#PastaBanco}\01_schema.sql"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#PastaBanco}\02_dados_teste.sql"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#PastaBanco}\03_acesso_rede.sql"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#PastaBanco}\04_atualizar_bloqueio_login.sql"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#PastaBanco}\05_atualizar_vale_alimentacao.sql"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#PastaBanco}\criar_banco.ps1"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#PastaBanco}\README.md"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#PastaBanco}\gerar_hash.ps1"; DestDir: "{app}\Banco de Dados"; Flags: ignoreversion
Source: "{#ArquivoLeiame}"; DestDir: "{app}"; Flags: ignoreversion
#ifdef ArquivoMariaDB
Source: "{#ArquivoMariaDB}"; DestDir: "{tmp}"; DestName: "mariadb.msi"; Flags: deleteafterinstall nocompression; Check: ModoNovoServidor
#endif

[Icons]
Name: "{group}\{#NomeApp}"; Filename: "{app}\erp_salgados_e_cia.exe"
Name: "{group}\Configurar conexão com o banco"; Filename: "{sys}\WindowsPowerShell\v1.0\powershell.exe"; Parameters: "-NoProfile -WindowStyle Hidden -Command ""Start-Process notepad.exe -ArgumentList '\""{app}\erp_salgados_e_cia.exe.config\""' -Verb RunAs"""; Check: IsAdminInstallMode
Name: "{group}\Configurar conexão com o banco"; Filename: "{win}\notepad.exe"; Parameters: """{app}\erp_salgados_e_cia.exe.config"""; Check: not IsAdminInstallMode
Name: "{group}\Criar o banco de dados (assistente)"; Filename: "{sys}\WindowsPowerShell\v1.0\powershell.exe"; Parameters: "-NoProfile -ExecutionPolicy Bypass -NoExit -File ""{app}\Banco de Dados\criar_banco.ps1"" -Interativo"
Name: "{group}\Scripts do banco de dados"; Filename: "{app}\Banco de Dados"
Name: "{group}\Leia-me"; Filename: "{app}\LEIA-ME.txt"
Name: "{autodesktop}\{#NomeApp}"; Filename: "{app}\erp_salgados_e_cia.exe"; Tasks: iconedesktop

[Run]
Filename: "{app}\erp_salgados_e_cia.exe"; Description: "Abrir o {#NomeApp} agora"; Flags: nowait postinstall skipifsilent

[Code]
var
  PaginaModo: TInputOptionWizardPage;
  PaginaNovo: TInputQueryWizardPage;
  PaginaRede: TInputOptionWizardPage;
  PaginaExistente: TInputQueryWizardPage;
  PaginaConexao: TInputQueryWizardPage;

  // valores finais da conexão (preenchidos conforme a opção escolhida)
  CxServidor, CxPorta, CxBanco, CxUsuario, CxSenha: String;
  UltimoLogBanco: String;
  BancoCriado: Boolean;
  ConexaoLida: Boolean;

const
  ServicoMariaDB = '{#NomeServico}';

function MariaDBEmbutido: Boolean;
begin
#ifdef ArquivoMariaDB
  Result := True;
#else
  Result := False;
#endif
end;

// 0 = instalar servidor novo, 1 = servidor existente (criar o banco), 2 = só configurar
function ModoEscolhido: Integer;
begin
  Result := PaginaModo.SelectedValueIndex;
end;

function ModoNovoServidor: Boolean;
begin
  Result := (PaginaModo <> nil) and (ModoEscolhido = 0);
end;

// ---------- verificações do computador ----------

// O .NET Framework 4.8 (release 528040 ou maior) é necessário. Já vem com o Windows 10 (1903+) e 11.
function PrecisaInstalarDotNet: Boolean;
var
  Release: Cardinal;
begin
  Result := True;
  if RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release) then
    Result := Release < 528040;
end;

function InitializeSetup: Boolean;
begin
  Result := True;
  if PrecisaInstalarDotNet then
  begin
    MsgBox('Este programa precisa do .NET Framework 4.8, que não foi encontrado neste computador.' + #13#10 +
           'Instale-o pelo Windows Update (ou pelo site da Microsoft) e execute o instalador de novo.',
           mbError, MB_OK);
    Result := False;
  end;
end;

function PortaLivre(Porta: Integer): Boolean;
var
  Codigo: Integer;
begin
  Result := True;
  if Exec(ExpandConstant('{sys}\WindowsPowerShell\v1.0\powershell.exe'),
          '-NoProfile -Command "try { $l = New-Object Net.Sockets.TcpListener([Net.IPAddress]::Any, ' + IntToStr(Porta) +
          '); $l.Start(); $l.Stop(); exit 0 } catch { exit 1 }"',
          '', SW_HIDE, ewWaitUntilTerminated, Codigo) then
    Result := (Codigo = 0);
end;

function ServicoJaExiste(const Nome: String): Boolean;
var
  Codigo: Integer;
begin
  Result := False;
  if Exec(ExpandConstant('{sys}\sc.exe'), 'query "' + Nome + '"', '', SW_HIDE, ewWaitUntilTerminated, Codigo) then
    Result := (Codigo = 0);
end;

function ServidorEhLocal(const Servidor: String): Boolean;
var
  S: String;
begin
  S := Lowercase(Trim(Servidor));
  Result := (S = 'localhost') or (S = '127.0.0.1') or (S = '::1') or (S = Lowercase(GetComputerNameString));
end;

function SenhaAleatoria: String;
var
  I: Integer;
  Letras: String;
begin
  Letras := 'ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789';
  Result := '';
  for I := 1 to 14 do
    Result := Result + Letras[((Random(1000) + StrToIntDef(GetDateTimeString('nnss', #0, #0), 0) * I) mod Length(Letras)) + 1];
end;

// ---------- páginas ----------

procedure InitializeWizard;
var
  Modo: String;
  PodeServidorNovo: Boolean;
begin
  PodeServidorNovo := MariaDBEmbutido and IsAdminInstallMode;

  PaginaModo := CreateInputOptionPage(wpSelectTasks,
    'Banco de dados',
    'Como o banco de dados do sistema será configurado?',
    'Escolha uma das opções:',
    True, False);
  PaginaModo.Add('Instalar um servidor de banco de dados (MariaDB) neste computador e criar o banco agora. ' +
                 'Recomendado para quem não tem servidor.');
  PaginaModo.Add('Usar um servidor que já existe (neste computador ou na rede) e criar o banco nele agora.');
  PaginaModo.Add('O banco de dados já está criado. Apenas configurar a conexão do programa.');

  Modo := Lowercase(ExpandConstant('{param:Modo|}'));
  if (Modo = 'configurar') then
    PaginaModo.SelectedValueIndex := 2
  else if (Modo = 'existente') or (not PodeServidorNovo) then
    PaginaModo.SelectedValueIndex := 1
  else
    PaginaModo.SelectedValueIndex := 0;

  if not PodeServidorNovo then
    PaginaModo.CheckListBox.ItemEnabled[0] := False;

  // opção 1: servidor novo
  PaginaNovo := CreateInputQueryPage(PaginaModo.ID,
    'Novo servidor de banco de dados',
    'Defina as senhas do servidor MariaDB que será instalado.',
    'A senha do administrador (root) serve para você mexer no servidor depois; guarde-a. ' +
    'A senha do usuário do programa já vem sugerida e fica gravada na configuração do programa. ' +
    'Use letras, números e símbolos simples (sem aspas, sem acentos, sem espaços).');
  PaginaNovo.Add('Porta do servidor:', False);
  PaginaNovo.Add('Senha do administrador do servidor (root):', True);
  PaginaNovo.Add('Repita a senha do administrador:', True);
  PaginaNovo.Add('Senha do usuário do programa (app_salgados):', False);
  PaginaNovo.Values[0] := ExpandConstant('{param:Porta|3306}');
  PaginaNovo.Values[1] := ExpandConstant('{param:SenhaAdmin|}');
  PaginaNovo.Values[2] := ExpandConstant('{param:SenhaAdmin|}');
  PaginaNovo.Values[3] := ExpandConstant('{param:SenhaApp|}');
  if PaginaNovo.Values[3] = '' then
    PaginaNovo.Values[3] := SenhaAleatoria;

  PaginaRede := CreateInputOptionPage(PaginaNovo.ID,
    'Acesso pela rede',
    'Outras unidades vão usar este banco de dados?',
    'Se este computador será o servidor de várias unidades, marque a opção abaixo. ' +
    'Depois, no firewall do Windows deste computador, libere a porta do banco (3306 por padrão). ' +
    'Se o banco é só deste computador, deixe desmarcado.',
    False, False);
  PaginaRede.Add('Permitir que outras unidades acessem este banco pela rede');
  PaginaRede.Values[0] := (ExpandConstant('{param:Rede|0}') = '1');

  // opção 2: servidor existente
  PaginaExistente := CreateInputQueryPage(PaginaRede.ID,
    'Servidor de banco de dados existente',
    'Informe o servidor onde o banco do sistema será criado.',
    'O instalador usa o usuário administrador do servidor apenas agora, para criar o banco "salgados_cia" ' +
    '(se ele já existir, você será avisado antes de apagar). Se o servidor é outro computador, o acesso pela rede ' +
    'será liberado para o usuário do programa.');
  PaginaExistente.Add('Servidor (nome ou IP):', False);
  PaginaExistente.Add('Porta:', False);
  PaginaExistente.Add('Usuário administrador do servidor:', False);
  PaginaExistente.Add('Senha do administrador (pode ficar vazia no XAMPP):', True);
  PaginaExistente.Add('Senha para o usuário do programa (app_salgados):', False);
  PaginaExistente.Values[0] := ExpandConstant('{param:Servidor|localhost}');
  PaginaExistente.Values[1] := ExpandConstant('{param:Porta|3306}');
  PaginaExistente.Values[2] := ExpandConstant('{param:UsuarioAdmin|root}');
  PaginaExistente.Values[3] := ExpandConstant('{param:SenhaAdmin|}');
  PaginaExistente.Values[4] := ExpandConstant('{param:SenhaApp|}');
  if PaginaExistente.Values[4] = '' then
    PaginaExistente.Values[4] := SenhaAleatoria;

  // opção 3: só configurar
  PaginaConexao := CreateInputQueryPage(PaginaExistente.ID,
    'Conexão com o banco de dados',
    'Informe como o programa vai se conectar ao banco já criado.',
    'Use o usuário e a senha do usuário do programa (por padrão, app_salgados).');
  PaginaConexao.Add('Servidor (nome ou IP do computador do banco):', False);
  PaginaConexao.Add('Porta:', False);
  PaginaConexao.Add('Banco de dados:', False);
  PaginaConexao.Add('Usuário do banco:', False);
  PaginaConexao.Add('Senha do usuário do banco:', True);
  PaginaConexao.Values[0] := ExpandConstant('{param:Servidor|localhost}');
  PaginaConexao.Values[1] := ExpandConstant('{param:Porta|3306}');
  PaginaConexao.Values[2] := ExpandConstant('{param:Banco|salgados_cia}');
  PaginaConexao.Values[3] := ExpandConstant('{param:Usuario|app_salgados}');
  PaginaConexao.Values[4] := ExpandConstant('{param:Senha|TROQUE_ESTA_SENHA_FORTE}');
end;

// ---------- conexão da instalação anterior (atualização) ----------

function DecodificarXml(const Texto: String): String;
begin
  Result := Texto;
  StringChangeEx(Result, '&lt;', '<', True);
  StringChangeEx(Result, '&gt;', '>', True);
  StringChangeEx(Result, '&quot;', '"', True);
  StringChangeEx(Result, '&apos;', '''', True);
  StringChangeEx(Result, '&amp;', '&', True);
end;

procedure AplicarPar(const Par: String; var Servidor, Porta, Banco, Usuario, Senha: String);
var
  P: Integer;
  Chave, Valor: String;
begin
  P := Pos('=', Par);
  if P = 0 then Exit;
  Chave := Lowercase(Trim(Copy(Par, 1, P - 1)));
  Valor := Trim(Copy(Par, P + 1, Length(Par)));
  if (Chave = 'server') or (Chave = 'host') then Servidor := Valor
  else if Chave = 'port' then Porta := Valor
  else if (Chave = 'database') or (Chave = 'initial catalog') then Banco := Valor
  else if (Chave = 'uid') or (Chave = 'user id') or (Chave = 'user') then Usuario := Valor
  else if (Chave = 'pwd') or (Chave = 'password') then Senha := Valor;
end;

// Lê a string de conexão do arquivo .config da instalação anterior (se existir).
function LerConexaoAnterior(var Servidor, Porta, Banco, Usuario, Senha: String): Boolean;
var
  Caminho, Texto, Cadeia, Atual: String;
  Conteudo: AnsiString;
  Inicio, Fim, I: Integer;
  EntreAspas: Boolean;
  C: Char;
begin
  Result := False;
  Servidor := ''; Porta := ''; Banco := ''; Usuario := ''; Senha := '';
  Caminho := AddBackslash(WizardDirValue) + 'erp_salgados_e_cia.exe.config';
  if not FileExists(Caminho) then Exit;
  if not LoadStringFromFile(Caminho, Conteudo) then Exit;
  Texto := String(Conteudo);

  Inicio := Pos('connectionString="', Texto);
  if Inicio = 0 then Exit;
  Inicio := Inicio + Length('connectionString="');
  Fim := Inicio;
  while (Fim <= Length(Texto)) and (Texto[Fim] <> '"') do
    Fim := Fim + 1;
  Cadeia := DecodificarXml(Copy(Texto, Inicio, Fim - Inicio));

  // separa os pares chave=valor por ";" (a senha pode vir entre aspas simples)
  Atual := '';
  EntreAspas := False;
  for I := 1 to Length(Cadeia) do
  begin
    C := Cadeia[I];
    if C = '''' then
      EntreAspas := not EntreAspas
    else if (C = ';') and (not EntreAspas) then
    begin
      AplicarPar(Atual, Servidor, Porta, Banco, Usuario, Senha);
      Atual := '';
    end
    else
      Atual := Atual + C;
  end;
  if Atual <> '' then
    AplicarPar(Atual, Servidor, Porta, Banco, Usuario, Senha);

  Result := (Servidor <> '') and (Banco <> '') and (Usuario <> '');
end;

// Se o programa já estava instalado, usa a conexão dele como ponto de partida e sugere "só configurar".
procedure CarregarConexaoAnterior;
var
  S, P, B, U, K: String;
begin
  if ConexaoLida then Exit;
  ConexaoLida := True;
  if not LerConexaoAnterior(S, P, B, U, K) then Exit;

  if ExpandConstant('{param:Servidor|}') = '' then
  begin
    PaginaConexao.Values[0] := S;
    PaginaExistente.Values[0] := S;
  end;
  if (ExpandConstant('{param:Porta|}') = '') and (P <> '') then
  begin
    PaginaConexao.Values[1] := P;
    PaginaExistente.Values[1] := P;
  end;
  if ExpandConstant('{param:Banco|}') = '' then PaginaConexao.Values[2] := B;
  if ExpandConstant('{param:Usuario|}') = '' then PaginaConexao.Values[3] := U;
  if (ExpandConstant('{param:Senha|}') = '') and (K <> '') then PaginaConexao.Values[4] := K;
  if ExpandConstant('{param:Modo|}') = '' then PaginaModo.SelectedValueIndex := 2;
end;

procedure CurPageChanged(CurPageID: Integer);
begin
  if (CurPageID = PaginaModo.ID) and (not ConexaoLida) then
  begin
    CarregarConexaoAnterior;
    if PaginaModo.SelectedValueIndex = 2 then
      WizardForm.PageDescriptionLabel.Caption :=
        'O programa já está instalado: a conexão atual foi lida e a opção "só configurar" foi marcada.';
  end;
end;

// Instalação silenciosa: lê a conexão anterior antes de os arquivos serem substituídos.
function PrepareToInstall(var NeedsRestart: Boolean): String;
begin
  Result := '';
  CarregarConexaoAnterior;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := False;
  if PageID = PaginaNovo.ID then Result := ModoEscolhido <> 0;
  if PageID = PaginaRede.ID then Result := ModoEscolhido <> 0;
  if PageID = PaginaExistente.ID then Result := ModoEscolhido <> 1;
  if PageID = PaginaConexao.ID then Result := ModoEscolhido <> 2;
end;

// ---------- validação ----------

// Só caracteres ASCII visíveis, sem aspas nem barra invertida (quebram o XML, o SQL e a linha de comando).
function TextoValido(const Texto: String; PermitePontoVirgula: Boolean; PermiteVazio: Boolean): Boolean;
var
  I: Integer;
  C: Char;
begin
  Result := PermiteVazio or (Texto <> '');
  for I := 1 to Length(Texto) do
  begin
    C := Texto[I];
    if (Ord(C) < 32) or (Ord(C) > 126) or (C = '''') or (C = '"') or (C = '\') then
      Result := False;
    if (not PermitePontoVirgula) and ((C = ';') or (C = '=') or (C = ' ')) then
      Result := False;
  end;
end;

// Senhas: sem espaços (valem para o instalador do MariaDB e para a linha de comando).
function SenhaValida(const Texto: String; PermiteVazio: Boolean): Boolean;
begin
  Result := TextoValido(Texto, True, PermiteVazio) and (Pos(' ', Texto) = 0);
end;

function PortaValida(const Texto: String): Boolean;
var
  Porta: Integer;
begin
  Porta := StrToIntDef(Texto, 0);
  Result := (Porta >= 1) and (Porta <= 65535);
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;

  if CurPageID = PaginaNovo.ID then
  begin
    if not PortaValida(PaginaNovo.Values[0]) then
    begin
      MsgBox('Informe uma porta válida (de 1 a 65535). A padrão do MySQL/MariaDB é 3306.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if (not SenhaValida(PaginaNovo.Values[1], False)) or (Length(PaginaNovo.Values[1]) < 6) then
    begin
      MsgBox('A senha do administrador deve ter pelo menos 6 caracteres, sem espaços, aspas, barra invertida ou acentos.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if PaginaNovo.Values[1] <> PaginaNovo.Values[2] then
    begin
      MsgBox('As duas senhas do administrador não são iguais.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not SenhaValida(PaginaNovo.Values[3], False) then
    begin
      MsgBox('A senha do usuário do programa não pode ficar vazia nem ter espaços, aspas, barra invertida ou acentos.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if ServicoJaExiste(ServicoMariaDB) then
    begin
      MsgBox('Já existe um serviço chamado "' + ServicoMariaDB + '" neste computador (uma instalação anterior deste sistema).' + #13#10 +
             'Use a opção "Usar um servidor que já existe" para criar o banco nele.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not PortaLivre(StrToInt(PaginaNovo.Values[0])) then
    begin
      MsgBox('A porta ' + PaginaNovo.Values[0] + ' já está em uso neste computador (talvez por outro MySQL ou pelo XAMPP).' + #13#10 +
             'Escolha outra porta, como 3307, ou use a opção "Usar um servidor que já existe".', mbError, MB_OK);
      Result := False; Exit;
    end;
  end;

  if CurPageID = PaginaExistente.ID then
  begin
    if not TextoValido(PaginaExistente.Values[0], False, False) then
    begin
      MsgBox('Informe o servidor (sem espaços, aspas, "=" ou ";").', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not PortaValida(PaginaExistente.Values[1]) then
    begin
      MsgBox('Informe uma porta válida (de 1 a 65535). A padrão é 3306.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not TextoValido(PaginaExistente.Values[2], False, False) then
    begin
      MsgBox('Informe o usuário administrador do servidor (por exemplo, root).', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not SenhaValida(PaginaExistente.Values[3], True) then
    begin
      MsgBox('A senha do administrador não pode ter espaços, aspas, barra invertida ou acentos.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not SenhaValida(PaginaExistente.Values[4], False) then
    begin
      MsgBox('A senha do usuário do programa não pode ficar vazia nem ter espaços, aspas, barra invertida ou acentos.', mbError, MB_OK);
      Result := False; Exit;
    end;
  end;

  if CurPageID = PaginaConexao.ID then
  begin
    if not TextoValido(PaginaConexao.Values[0], False, False) then
    begin
      MsgBox('Informe o servidor (sem espaços, aspas, "=" ou ";").', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not PortaValida(PaginaConexao.Values[1]) then
    begin
      MsgBox('Informe uma porta válida (de 1 a 65535). A padrão do MySQL é 3306.', mbError, MB_OK);
      Result := False; Exit;
    end;
    if (not TextoValido(PaginaConexao.Values[2], False, False)) or (not TextoValido(PaginaConexao.Values[3], False, False)) then
    begin
      MsgBox('Informe o banco e o usuário (sem espaços, aspas, "=" ou ";").', mbError, MB_OK);
      Result := False; Exit;
    end;
    if not SenhaValida(PaginaConexao.Values[4], False) then
    begin
      MsgBox('Informe a senha do usuário do banco. Ela não pode ter espaços, aspas, barra invertida nem acentos.', mbError, MB_OK);
      Result := False;
    end;
  end;
end;

// ---------- execução ----------

function EscaparXml(const Texto: String): String;
begin
  Result := Texto;
  StringChangeEx(Result, '&', '&amp;', True);
  StringChangeEx(Result, '<', '&lt;', True);
  StringChangeEx(Result, '>', '&gt;', True);
end;

// Troca a string de conexão do arquivo .config pelos valores escolhidos na instalação.
procedure GravarConexao;
var
  Caminho: String;
  Conteudo: AnsiString;
  Texto, Senha, Conexao: String;
  Inicio, Fim: Integer;
begin
  Caminho := ExpandConstant('{app}\erp_salgados_e_cia.exe.config');

  if not LoadStringFromFile(Caminho, Conteudo) then
  begin
    MsgBox('Não foi possível ler o arquivo de configuração. Edite a conexão depois pelo atalho ' +
           '"Configurar conexão com o banco".', mbError, MB_OK);
    Exit;
  end;

  Texto := String(Conteudo);

  Senha := CxSenha;
  if (Pos(';', Senha) > 0) or (Pos('=', Senha) > 0) or (Pos(' ', Senha) > 0) then
    Senha := '''' + Senha + '''';

  Conexao :=
    'Server=' + CxServidor +
    ';Port=' + CxPorta +
    ';Database=' + CxBanco +
    ';Uid=' + CxUsuario +
    ';Pwd=' + Senha +
    ';CharSet=utf8mb4;';

  Inicio := Pos('connectionString="', Texto);
  if Inicio = 0 then
  begin
    MsgBox('O arquivo de configuração não tem a conexão esperada. Edite-o depois pelo atalho ' +
           '"Configurar conexão com o banco".', mbError, MB_OK);
    Exit;
  end;

  Inicio := Inicio + Length('connectionString="');
  Fim := Inicio;
  while (Fim <= Length(Texto)) and (Texto[Fim] <> '"') do
    Fim := Fim + 1;

  Texto := Copy(Texto, 1, Inicio - 1) + EscaparXml(Conexao) + Copy(Texto, Fim, Length(Texto));

  if not SaveStringToFile(Caminho, AnsiString(Texto), False) then
    MsgBox('Não foi possível gravar a conexão. Edite o arquivo depois pelo atalho ' +
           '"Configurar conexão com o banco".', mbError, MB_OK);
end;

function JsonBool(Valor: Boolean): String;
begin
  if Valor then Result := 'true' else Result := 'false';
end;

// Roda o criar_banco.ps1. Os dados vão num arquivo temporário (apagado em seguida), não na linha de comando.
function RodarCriarBanco(const Servidor, Porta, UsuarioAdmin, SenhaAdmin, SenhaApp: String;
  Rede, Forcar: Boolean; EsperarSegundos: Integer): Integer;
var
  ArqJson, ArqLog, Json: String;
  Codigo: Integer;
  LogTexto: AnsiString;
begin
  ArqJson := ExpandConstant('{tmp}\salgados_banco.json');
  ArqLog := ExpandConstant('{tmp}\salgados_banco.log');
  DeleteFile(ArqLog);

  Json := '{"Servidor":"' + Servidor + '","Porta":' + Porta +
          ',"UsuarioAdmin":"' + UsuarioAdmin + '","SenhaAdmin":"' + SenhaAdmin +
          '","SenhaApp":"' + SenhaApp + '","IncluirRede":' + JsonBool(Rede) +
          ',"Forcar":' + JsonBool(Forcar) + ',"EsperarSegundos":' + IntToStr(EsperarSegundos) + '}';
  SaveStringToFile(ArqJson, AnsiString(Json), False);

  Codigo := 6;
  if not Exec(ExpandConstant('{sys}\WindowsPowerShell\v1.0\powershell.exe'),
              '-NoProfile -ExecutionPolicy Bypass -File "' + ExpandConstant('{app}\Banco de Dados\criar_banco.ps1') +
              '" -ArquivoParametros "' + ArqJson + '" -DllMySql "' + ExpandConstant('{app}\MySql.Data.dll') +
              '" -Log "' + ArqLog + '"',
              '', SW_HIDE, ewWaitUntilTerminated, Codigo) then
    Codigo := 6;

  DeleteFile(ArqJson);

  UltimoLogBanco := '';
  if LoadStringFromFile(ArqLog, LogTexto) then
    UltimoLogBanco := Trim(String(LogTexto));

  Result := Codigo;
end;

// Instala o MariaDB (msiexec silencioso). Devolve True se deu certo.
function InstalarMariaDB(const Porta, SenhaAdmin: String): Boolean;
var
  Codigo: Integer;
  Parametros: String;
begin
  WizardForm.StatusLabel.Caption := 'Instalando o servidor de banco de dados (pode levar alguns minutos)...';
  WizardForm.FileNameLabel.Caption := '';

  Parametros :=
    '/i "' + ExpandConstant('{tmp}\mariadb.msi') + '" /qn /norestart' +
    ' SERVICENAME=' + ServicoMariaDB +
    ' PORT=' + Porta +
    ' PASSWORD="' + SenhaAdmin + '"' +
    ' UTF8=1' +
    ' INSTALLDIR="' + ExpandConstant('{autopf}\MariaDB Salgados') + '"' +
    ' DATADIR="' + ExpandConstant('{commonappdata}\SalgadosECia\dados') + '"' +
    ' /L*v "' + ExpandConstant('{tmp}\mariadb_instalacao.log') + '"';

  Codigo := -1;
  Result := Exec(ExpandConstant('{sys}\msiexec.exe'), Parametros, '', SW_HIDE, ewWaitUntilTerminated, Codigo)
            and ((Codigo = 0) or (Codigo = 3010));

  if not Result then
  begin
    FileCopy(ExpandConstant('{tmp}\mariadb_instalacao.log'), ExpandConstant('{app}\Banco de Dados\mariadb_instalacao.log'), False);
    MsgBox('Não foi possível instalar o servidor de banco de dados (código ' + IntToStr(Codigo) + ').' + #13#10 +
           'O programa foi instalado, mas o banco não foi criado. O registro da instalação do servidor está em:' + #13#10 +
           ExpandConstant('{app}\Banco de Dados\mariadb_instalacao.log') + #13#10 +
           'Você pode instalar um MySQL/MariaDB por conta própria e depois usar o atalho ' +
           '"Criar o banco de dados (assistente)" do menu Iniciar.', mbError, MB_OK);
  end;
end;

procedure MostrarFalhaDoBanco;
begin
  MsgBox('O programa foi instalado, mas o banco de dados NÃO foi criado.' + #13#10 + #13#10 +
         UltimoLogBanco + #13#10 + #13#10 +
         'Depois de resolver, use o atalho "Criar o banco de dados (assistente)" do menu Iniciar, ' +
         'ou rode os scripts da pasta "Banco de Dados" (veja o LEIA-ME).', mbError, MB_OK);
end;

procedure CriarBancoSeNecessario;
var
  Codigo, Esperar: Integer;
  Rede, Forcar: Boolean;
  Servidor, Porta, UsuarioAdmin, SenhaAdmin, SenhaApp: String;
begin
  Rede := False;
  Esperar := 0;
  BancoCriado := False;

  if ModoEscolhido = 0 then
  begin
    Servidor := '127.0.0.1';
    Porta := PaginaNovo.Values[0];
    UsuarioAdmin := 'root';
    SenhaAdmin := PaginaNovo.Values[1];
    SenhaApp := PaginaNovo.Values[3];
    Rede := PaginaRede.Values[0];
    Esperar := 90;
  end
  else
  begin
    Servidor := PaginaExistente.Values[0];
    Porta := PaginaExistente.Values[1];
    UsuarioAdmin := PaginaExistente.Values[2];
    SenhaAdmin := PaginaExistente.Values[3];
    SenhaApp := PaginaExistente.Values[4];
    Rede := not ServidorEhLocal(Servidor);
  end;

  CxServidor := Servidor;
  if ModoEscolhido = 0 then CxServidor := 'localhost';
  CxPorta := Porta;
  CxBanco := 'salgados_cia';
  CxUsuario := 'app_salgados';
  CxSenha := SenhaApp;

  if ModoEscolhido = 0 then
  begin
    if not InstalarMariaDB(Porta, SenhaAdmin) then Exit;
  end;

  WizardForm.StatusLabel.Caption := 'Criando o banco de dados do sistema...';
  WizardForm.FileNameLabel.Caption := '';

  Forcar := (ExpandConstant('{param:Forcar|0}') = '1');
  Codigo := RodarCriarBanco(Servidor, Porta, UsuarioAdmin, SenhaAdmin, SenhaApp, Rede, Forcar, Esperar);

  if Codigo = 3 then
  begin
    // o banco já existe e tem dados: só apaga se o usuário confirmar
    if MsgBox('O banco de dados "salgados_cia" já existe nesse servidor e tem dados.' + #13#10 + #13#10 +
              'Apagar tudo e criar o banco de novo? Os dados atuais serão PERDIDOS.' + #13#10 +
              '(Se escolher "Não", o banco atual é mantido e o programa usará a senha que você informou.)',
              mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES then
      Codigo := RodarCriarBanco(Servidor, Porta, UsuarioAdmin, SenhaAdmin, SenhaApp, Rede, True, Esperar)
    else
    begin
      MsgBox('O banco existente foi mantido. Se o programa não conseguir entrar, a senha do usuário "app_salgados" ' +
             'no banco é diferente da informada: ajuste no atalho "Configurar conexão com o banco".', mbInformation, MB_OK);
      Exit;
    end;
  end;

  if Codigo = 0 then
    BancoCriado := True
  else
    MostrarFalhaDoBanco;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    if ModoEscolhido = 2 then
    begin
      CxServidor := PaginaConexao.Values[0];
      CxPorta := PaginaConexao.Values[1];
      CxBanco := PaginaConexao.Values[2];
      CxUsuario := PaginaConexao.Values[3];
      CxSenha := PaginaConexao.Values[4];
    end
    else
      CriarBancoSeNecessario;

    GravarConexao;

    if BancoCriado and (not WizardSilent) then
      MsgBox('O banco de dados foi criado e o programa já está configurado.' + #13#10 + #13#10 +
             'Para entrar no sistema, use o login "admin" com a senha "123" (dados de teste; ' +
             'troque as senhas na tela Usuários antes de usar de verdade).', mbInformation, MB_OK);
  end;
end;

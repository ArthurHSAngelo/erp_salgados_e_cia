# Guia de Build – ERP Salgados & Cia

Este guia explica como obter o código, preparar o banco de dados, compilar e executar o ERP Salgados & Cia, e como gerar o pacote de instalação.

## 1. Visão geral da solução

A solução `erp_salgados_e_cia.sln` tem três projetos, um para cada camada:

```
erp_salgados_e_cia.sln
├── erp_salgados_e_cia          → UI      (Windows Forms, gera o executável)
│     ├── usa → erp_salgados_e_cia.Domain
│     └── usa → erp_salgados_e_cia.Data
├── erp_salgados_e_cia.Data     → Data    (conexão MySQL e repositórios)
│     └── usa → erp_salgados_e_cia.Domain
└── erp_salgados_e_cia.Domain   → Domain  (entidades, exceção de negócio e serviço de senha)
```

Regras de dependência entre as camadas:

- A **Domain** não referencia nenhuma outra camada.
- A **Data** referencia somente a Domain.
- A **UI** referencia a Domain e a Data.

O programa só conversa com o banco pela camada Data. As regras de negócio mais importantes (baixa de estoque, caixa, vendas, bloqueio de login, log) ficam no banco, em triggers e procedures.

## 2. Pré-requisitos

| Item | Observação |
|---|---|
| Windows 10 ou 11 | Windows Forms em .NET Framework só roda no Windows |
| Visual Studio 2022 ou superior (Community) | Instalar a carga de trabalho **Desenvolvimento para desktop com .NET** |
| .NET Framework 4.8 (Developer Pack) | Já vem com a carga de trabalho acima; se faltar, instale pelo Visual Studio Installer |
| Git | Para clonar o repositório |
| MySQL 8.0 ou MariaDB 10.4 (ou mais novo) | O XAMPP serve. O banco foi testado no MariaDB 10.4.32 e 10.11.19 e no MySQL 8.0 |
| Inno Setup 6 | Só para gerar o instalador (`winget install JRSoftware.InnoSetup`) |

## 3. Obter o código

```bash
git clone https://github.com/ArthurHSAngelo/erp_salgados_e_cia.git
cd erp_salgados_e_cia
```

Dica: clone em uma pasta de caminho curto, como `C:\dev`, para evitar erros de caminho muito longo.

## 4. Preparar o banco de dados

Os scripts ficam na pasta `database/`. O `database/README.md` explica cada um.

1. Com o servidor ligado, conecte como root (MySQL Workbench, phpMyAdmin ou linha de comando).
2. Execute, inteiros e nesta ordem, `01_schema.sql` (apaga e recria o banco `salgados_cia`) e `02_dados_teste.sql`.
3. Troque a senha do usuário do programa, se quiser: `ALTER USER 'app_salgados'@'localhost' IDENTIFIED BY 'sua_senha_forte';`

Se você já tem um banco criado com uma versão anterior, não apague: rode apenas o `04_atualizar_banco_existente.sql`, que serve para qualquer versão antiga e não apaga dados.

A conexão do programa fica no arquivo `erp_salgados_e_cia.exe.config` (servidor, porta, banco, usuário e senha).

## 5. Compilar

### Pelo Visual Studio (recomendado)

1. Abra o arquivo **`erp_salgados_e_cia.sln`**, na raiz do repositório.
2. Confira no **Gerenciador de Soluções** os três projetos.
3. Escolha a configuração **Debug** (ou **Release**) e **Any CPU**.
4. Use **Compilar > Recompilar Solução**.
5. A última linha da janela **Saída** deve mostrar 3 com êxito e 0 com falha.

As bibliotecas (MySql.Data e PDFsharp-MigraDoc) vêm pelo NuGet e são baixadas na compilação.

### Pela linha de comando

Abra o **Developer Command Prompt for VS** na pasta do repositório:

```bat
msbuild erp_salgados_e_cia.sln /t:Rebuild /p:Configuration=Debug /restore
```

O resultado deve terminar com `0 Error(s)`. Use o `msbuild`, e não o `dotnet build`, porque o projeto de interface é Windows Forms em .NET Framework.

## 6. Executar

- No Visual Studio, pressione **F5**. O projeto de inicialização é `erp_salgados_e_cia`.
- Ou execute `erp_salgados_e_cia\bin\Debug\erp_salgados_e_cia.exe`.

Abre a tela de login. Com os dados de teste, os logins são `centro` e `norte` (Atendente de cada unidade), `gerente1` e `gerente2` (Gerente) e `admin` (Administrador). Veja a senha dos logins de teste no `instalador/LEIA-ME.txt`.

## 7. Testes automáticos

Na pasta `database/`:

- `testes_integracao.ps1`: 96 verificações das regras de negócio, chamando o programa compilado contra o banco.
- `testes_desempenho.ps1`: 16 medições de tempo, com 1.000 vendas no banco.

O `database/README.md` explica como rodar cada um. Os testes gravam dados: use um banco de teste.

## 8. Gerar o pacote de instalação

Na pasta `instalador/`:

```powershell
.\gerar_pacote.ps1
```

O script compila em Release, cria o instalador e a versão portátil (ZIP) e reúne os scripts do banco e o `LEIA-ME.txt`. O pacote sai fora do repositório, em uma pasta `Pacote_RC` ao lado dele. O número da versão padrão está no script (`-Versao`).

## 9. Estrutura de pastas

```
erp_salgados_e_cia/                    (raiz do repositório)
├── erp_salgados_e_cia.sln
├── README.md
├── docs/                              este guia
├── database/                          scripts SQL e testes automáticos
├── instalador/                        instalador, versão portátil e gerar_pacote.ps1
├── erp_salgados_e_cia/                UI (telas uc* e frm*, MenuPrincipal, dadossistema)
├── erp_salgados_e_cia.Domain/         Entities, Exceptions e Services
└── erp_salgados_e_cia.Data/           Connection e Repositories
```

Convenções:

- As telas ficam no projeto de UI e não contêm comandos SQL: tudo que vai ao banco passa pelos repositórios da camada Data.
- A classe `dadossistema` guarda as listas em memória e as regras de aplicação que chamam os repositórios.
- Os controles seguem nomes com prefixo (`tb` caixa de texto, `cb` combo ou check, `b` botão, `l` rótulo, `dgv` tabela).

## 10. Problemas comuns

| Sintoma | Causa provável | Solução |
|---|---|---|
| Erro `MSB3644` ou "referência do .NET Framework 4.8 não encontrada" | .NET Framework 4.8 Developer Pack ausente | Instalar pelo Visual Studio Installer |
| Erro de pacote NuGet ausente | Pacotes não restaurados | Compilar com `/restore` ou usar **Restaurar Pacotes NuGet** no Visual Studio |
| "Não foi possível conectar ao banco de dados" | Servidor desligado, ou servidor/porta errados no `.config` | Ligar o servidor e conferir o arquivo `erp_salgados_e_cia.exe.config` |
| "Usuário ou senha do banco de dados incorretos" | Senha do `.config` diferente da do usuário `app_salgados` | Acertar a senha no `.config` ou no banco |
| Erro ao carregar unidades ou ao entrar | Banco criado antes das últimas atualizações | Rodar o `04_atualizar_banco_existente.sql` |
| Erro de caminho muito longo ao compilar | Repositório em pasta com caminho extenso | Clonar em `C:\dev` |

## 11. Fluxo de trabalho com Git

```bash
git pull                              # atualizar antes de começar
git add .
git commit -m "descrição curta da mudança"
git push
```

Antes de cada `push`, compile a solução e confirme que o resultado é `0 com falha`.

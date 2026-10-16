# Guia de Build – ERP Salgados & Cia

Este guia explica como obter o código, compilar e executar a solução do ERP Salgados & Cia, e como gerar as evidências da etapa D0.

## 1. Visão geral da solução

A solução `erp_salgados_e_cia.sln` tem três projetos, um para cada camada:

```
erp_salgados_e_cia.sln
├── erp_salgados_e_cia          → UI      (Windows Forms, gera o executável)
│     ├── usa → erp_salgados_e_cia.Domain
│     └── usa → erp_salgados_e_cia.Data
├── erp_salgados_e_cia.Data     → Data    (conexão MySQL e repositórios)
│     └── usa → erp_salgados_e_cia.Domain
└── erp_salgados_e_cia.Domain   → Domain  (entidades, interfaces e regras de negócio)
```

Regras de dependência entre as camadas:

- A **Domain** não referencia nenhuma outra camada.
- A **Data** referencia somente a Domain.
- A **UI** referencia a Domain e a Data.
- A Domain define as interfaces dos repositórios, a Data as implementa e a UI conecta as duas.

Nesta etapa as camadas estão vazias. Cada uma tem apenas uma classe de verificação ("Hello World") que comprova a compilação e as referências entre os projetos.

## 2. Pré-requisitos

| Item | Observação |
|---|---|
| Windows 10 ou 11 | Windows Forms em .NET Framework só roda no Windows |
| Visual Studio 2022 ou superior (Community) | Instalar a carga de trabalho **Desenvolvimento para desktop com .NET** |
| .NET Framework 4.8 (Developer Pack) | Já vem com a carga de trabalho acima; se faltar, instale pelo Visual Studio Installer, em Componentes individuais |
| Git | Para clonar o repositório |
| MySQL Server 8.x e MySQL Workbench | Necessários apenas a partir da etapa de banco de dados |

## 3. Obter o código

```bash
git clone https://github.com/ArthurHSAngelo/erp_salgados_e_cia.git
cd erp_salgados_e_cia
```

Dica: clone em uma pasta de caminho curto, como `C:\dev`, para evitar erros de caminho muito longo.

## 4. Compilar

### Pelo Visual Studio (recomendado)

1. Abra o arquivo **`erp_salgados_e_cia.sln`**, na raiz do repositório.
2. No **Gerenciador de Soluções**, confira que aparecem os três projetos: `erp_salgados_e_cia`, `erp_salgados_e_cia.Data` e `erp_salgados_e_cia.Domain`.
3. Escolha a configuração **Debug** e **Any CPU**.
4. Use o menu **Compilar > Recompilar Solução** (Build > Rebuild Solution).
5. Na janela **Saída** (Output), a última linha deve ser parecida com:

```
========== Rebuild All: 3 succeeded, 0 failed, 0 skipped ==========
```

Se o Visual Studio estiver em português, a linha mostra "3 com êxito, 0 com falha".

### Pela linha de comando

Abra o **Developer Command Prompt for VS** na pasta do repositório e execute:

```bat
msbuild erp_salgados_e_cia.sln /t:Rebuild /p:Configuration=Debug
```

O resultado esperado termina com `Build succeeded.` e `0 Error(s)`.

Observação: use o `msbuild`, e não o `dotnet build`, porque o projeto de interface é um projeto Windows Forms em .NET Framework.

## 5. Executar

- No Visual Studio, pressione **F5** (ou **Ctrl+F5** para executar sem depurar). O projeto de inicialização é `erp_salgados_e_cia`.
- Ou execute o arquivo gerado: `erp_salgados_e_cia\bin\Debug\erp_salgados_e_cia.exe`.

Resultado esperado: abre a janela **"ERP Salgados & Cia - Hello World"** com o texto "Hello World!" e três linhas, uma de cada camada:

```
UI: camada de apresentação carregada.
Camada Domain carregada.
Camada Data carregada (referencia a camada Domain: ERP Salgados & Cia).
```

Na pasta `bin\Debug` do projeto de interface devem aparecer também `erp_salgados_e_cia.Domain.dll` e `erp_salgados_e_cia.Data.dll`.

## 6. Evidências da etapa D0

Para a entrega do D0, gere os seguintes prints (Windows + Shift + S):

1. **Gerenciador de Soluções** mostrando os três projetos.
2. **Janela Saída** com a linha final da compilação (3 com êxito, 0 com falha).
3. **Janela Hello World** em execução.

Junte os prints ao PDF dos requisitos e do DER e ao link do repositório.

## 7. Estrutura de pastas

```
erp_salgados_e_cia/                    (raiz do repositório)
├── erp_salgados_e_cia.sln
├── README.md
├── docs/                              documentação e este guia
├── database/                          scripts SQL (etapas futuras)
├── erp_salgados_e_cia/                UI
│   ├── Program.cs                     ponto de entrada
│   └── FrmHelloWorld.cs               tela provisória da etapa D0
├── erp_salgados_e_cia.Domain/         Domain
│   ├── Entities/                      classes das entidades do DER
│   ├── Enums/                         enumerações (Perfil, StatusVenda etc.)
│   ├── Interfaces/                    interfaces dos repositórios
│   └── Services/                      regras de negócio
└── erp_salgados_e_cia.Data/           Data
    ├── Connection/                    conexão com o MySQL
    └── Repositories/                  implementação dos repositórios
```

Convenções:

- Cada entidade do DER vira uma classe em `Domain/Entities`, com o nome em maiúsculas iniciais (por exemplo, `Venda` e `ItemVenda`).
- Os namespaces seguem as pastas, como `erp_salgados_e_cia.Domain.Entities`.
- As telas ficam no projeto de UI e não devem conter regras de negócio nem comandos SQL.

## 8. Adicionar classes e telas

- Para criar uma classe, clique com o botão direito na pasta desejada no Gerenciador de Soluções e escolha **Adicionar > Classe**. O Visual Studio registra o arquivo no projeto automaticamente.
- Se um arquivo for criado fora do Visual Studio, ative **Mostrar Todos os Arquivos**, clique com o botão direito no arquivo e escolha **Incluir no Projeto**.
- Para usar uma camada em outra, confirme que existe a referência em **Adicionar > Referência de Projeto**, respeitando as regras de dependência da seção 1.

## 9. Problemas comuns

| Sintoma | Causa provável | Solução |
|---|---|---|
| Erro `MSB3644` ou "referência do .NET Framework 4.8 não encontrada" | .NET Framework 4.8 Developer Pack ausente | Instalar pelo Visual Studio Installer, em Componentes individuais |
| `O tipo ou nome do namespace 'Domain' não existe` | Referência de projeto ausente | Adicionar a referência em **Adicionar > Referência de Projeto** |
| Aviso de projeto "não carregado" ou solução sem os três projetos | Solução aberta de outro lugar | Abrir o `.sln` da raiz do repositório |
| Erro de caminho muito longo ao compilar | Repositório em pasta com caminho extenso | Clonar em `C:\dev` |
| Arquivos `desktop.ini` aparecendo no Git | Configuração de pastas do Windows | Não versionar esses arquivos (por exemplo, com `git rm --cached` e entrada no `.gitignore`) |

## 10. Fluxo de trabalho com Git

```bash
git pull                              # atualizar antes de começar
git add .
git commit -m "descrição curta da mudança"
git push
```

Antes de cada `push`, compile a solução e confirme que o resultado é `0 com falha`.

## 11. Próximos passos

- Adicionar na pasta `database/` os scripts de criação do banco MySQL, as triggers, as procedures e os dados de teste.
- Adicionar na camada Data o conector do MySQL pelo NuGet e a classe de conexão em `Connection/`.
- Criar em `Domain/Entities` as classes correspondentes às entidades do DER.

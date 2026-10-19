# ERP Salgados & Cia

Sistema de gestão (ERP) desktop para a rede de salgaderias **Salgados & Cia**, de Frutal-MG, desenvolvido no Projeto Interdisciplinar IV do curso de Sistemas de Informação da UEMG – Unidade Frutal.

O sistema substitui o caderno e a calculadora usados no balcão e controla vendas, caixa, estoque por unidade e relatórios gerenciais.

## Equipe

- Arthur Henrique Silva Angelo
- Cauã Soares Aguiar
- Gabriel Tosta Moura
- Marco Antonio da Silva Menezes

Docente: Geraldo Nunes Corrêa

## Tecnologias

- C# com .NET Framework 4.8
- Windows Forms (interface desktop)
- MySQL (banco de dados, a partir da etapa de banco de dados)

## Estrutura da solução

A solução `erp_salgados_e_cia.sln` é dividida em três camadas:

| Projeto | Camada | Responsabilidade |
|---|---|---|
| `erp_salgados_e_cia` | UI | Telas (Windows Forms) e ponto de entrada da aplicação |
| `erp_salgados_e_cia.Domain` | Domain | Entidades, interfaces e regras de negócio |
| `erp_salgados_e_cia.Data` | Data | Conexão com o MySQL e repositórios |

Dependências: a UI usa Domain e Data, a Data usa Domain, e a Domain não depende de ninguém.

Outras pastas:

- `docs/`: documentação do projeto (guia de build e demais documentos).
- `database/`: scripts SQL do banco de dados (a partir da etapa de banco de dados).

## Como compilar e executar

Veja o passo a passo completo no [Guia de Build](docs/GUIA_DE_BUILD.md). Em resumo:

1. Clone o repositório e abra `erp_salgados_e_cia.sln` no Visual Studio.
2. Compile com **Build > Rebuild Solution**.
3. Execute com **F5**. Deve abrir a janela "Hello World" exibindo uma mensagem de cada camada.

## Status

Etapa D0: estrutura da solução em camadas (UI, Domain e Data) compilando com "Hello World".

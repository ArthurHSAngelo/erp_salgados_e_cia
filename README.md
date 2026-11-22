# ERP Salgados & Cia

Sistema de gestão (ERP) desktop para a rede de salgaderias **Salgados & Cia**, de Frutal-MG, desenvolvido no Projeto Interdisciplinar IV do curso de Sistemas de Informação da UEMG – Unidade Frutal.

O sistema substitui o caderno e a calculadora usados no balcão e controla vendas por comanda, caixa, estoque por unidade, Notinha (venda a prazo), relatórios gerenciais e o DRE, com registro de tudo o que é feito (log de operações).

## Equipe

- Arthur Henrique Silva Angelo
- Cauã Soares Aguiar
- Gabriel Tosta Moura
- Marco Antonio da Silva Menezes

Docente: Geraldo Nunes Corrêa

## O que o sistema faz

- **Vendas e comandas:** pedidos por mesa ou balcão, cobrança com várias formas de pagamento (Dinheiro, Pix, Crédito, Débito, Vale Alimentação nas unidades que aceitam, e Notinha) e cálculo do troco.
- **Caixa:** abertura, conferência de dinheiro e maquininha e fechamento.
- **Estoque por unidade:** entradas, perdas, inventário, estoque crítico e extrato de movimentações.
- **Relatórios:** vendas (com gráficos), estoque, movimentações e DRE, com exportação em PDF e CSV.
- **Segurança:** três perfis (Atendente, Gerente e Administrador), senhas protegidas, bloqueio do login por 5 tentativas erradas e log de operações que não pode ser alterado.

## Tecnologias

- C# com .NET Framework 4.8 e Windows Forms
- MySQL / MariaDB, com as regras de negócio em triggers e procedures
- MySql.Data (conexão) e PDFsharp-MigraDoc (relatórios em PDF)

## Estrutura da solução

A solução `erp_salgados_e_cia.sln` é dividida em três camadas:

| Projeto | Camada | Responsabilidade |
|---|---|---|
| `erp_salgados_e_cia` | UI | Telas (Windows Forms) e ponto de entrada da aplicação |
| `erp_salgados_e_cia.Domain` | Domain | Entidades, exceção de regra de negócio e o serviço de senha (PBKDF2) |
| `erp_salgados_e_cia.Data` | Data | Conexão com o MySQL e repositórios |

Dependências: a UI usa Domain e Data, a Data usa Domain, e a Domain não depende de ninguém.

Outras pastas:

- `database/`: scripts SQL (criação do banco, dados de teste, atualizações) e os testes automáticos. Veja o `database/README.md`.
- `instalador/`: instalador (Inno Setup), versão portátil e script que gera o pacote. Veja o `instalador/LEIA-ME.txt`.
- `docs/`: guia de build.

## Como compilar e executar

Veja o passo a passo no [Guia de Build](docs/GUIA_DE_BUILD.md). Em resumo:

1. Clone o repositório e abra `erp_salgados_e_cia.sln` no Visual Studio.
2. Crie o banco de dados: rode `database/01_schema.sql` e `database/02_dados_teste.sql` num servidor MySQL ou MariaDB (ou use o instalador).
3. Compile com **Build > Rebuild Solution** e execute com **F5**.

## Pacote de entrega

O instalador e a versão portátil são gerados por `instalador/gerar_pacote.ps1`. A versão atual do pacote é a **1.1.1**.

## Status

Versão 1.1.1, pronta para a entrega final do Projeto Interdisciplinar IV (novembro de 2026). Os testes automáticos têm 96 verificações, todas passando.

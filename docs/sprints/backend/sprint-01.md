# Sprint 01: Setup da Solução e Infraestrutura Local

## Atividades da Sprint
- Inicialização da solução .NET 8 e organização de projetos no diretório `src`.
- Configuração do ambiente de desenvolvimento conteinerizado.

## Ferramentas e Pacotes
- **Docker & Docker Compose:** Orquestração de serviços.
- **PostgreSQL 16:** Banco de dados relacional principal.
- **Redis 7:** Cache e gerenciamento de filas.
- **pgAdmin / Redis Insight:** Ferramentas de gerenciamento visual.

## Estrutura de Pastas e Arquivos
- `docker-compose.yml`: Definição dos serviços Postgres e Redis.
- `backend/src/web_ifood.sln`: Arquivo de solução global.
- `backend/src/Api/`: Projeto Web API.
- `backend/src/Data/`, `backend/src/Domains/`, `backend/src/Repositories/`, `backend/src/Services/`, `backend/src/Dtos/`: Bibliotecas de classes segregadas.

## Classes e Configurações
- `Dockerfile`: Configuração de multistage build para a API.
- `appsettings.json`: Strings de conexão iniciais e configurações de ambiente.

## Testes e Validação
- **Teste de Conectividade:** Scripts Bash para validar se o Postgres/Redis estão aceitando conexões nos ports 5432 e 6379.
- **HealthChecks:** Implementação do middleware de HealthCheck na API (.NET Diagnostics).

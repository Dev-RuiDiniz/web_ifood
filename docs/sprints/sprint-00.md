# Sprint 00 - Fundacao Tecnica e Baseline

## Objetivo

Estabelecer a fundacao do monorepo e os blocos tecnicos minimos para iniciar o MVP com seguranca, padrao e produtividade.

## Escopo funcional

- Estrutura inicial do monorepo com separacao `web`, `aplicativo`, `backend`, `packages`.
- Setup base de lint, format, convencoes de commit e CI.
- Ambiente local com Docker Compose para API + PostgreSQL + Redis.
- Bootstrap da API NestJS com Prisma e modulos iniciais.
- Definicao de contratos compartilhados (`shared-types` e `shared-validators`).

## Criterios de aceite

- Repositorio sobe localmente com comando unico documentado.
- Pipeline CI valida lint + teste basico sem falhas.
- API responde endpoint de healthcheck com status 200.
- Conexao com PostgreSQL e Redis validada por teste de integracao.
- Estrutura de pastas aprovada e publicada no README.

## Entregaveis

- Monorepo inicial funcional.
- API NestJS inicial com Prisma configurado.
- Primeiras migrations do banco.
- Documento de convencoes tecnicas.

## Tarefas por trilha

### Back-end

- Inicializar `backend/api` com NestJS e TypeScript.
- Configurar Prisma com entidades iniciais: `users`, `stores`, `products`, `orders`, `payments`.
- Implementar `/health` e `/ready`.
- Configurar Redis client para cache/rate-limit.

### Front-end Web

- Inicializar `web/admin` e `web/loja` com Next.js + TypeScript.
- Configurar Tailwind, TanStack Query e estrutura base de layout.
- Criar pagina de status conectada ao healthcheck da API.

### Mobile

- Inicializar `aplicativo/cliente` com React Native + TypeScript.
- Configurar navegacao base e client HTTP.
- Criar tela inicial de conectividade com API.

### QA

- Definir estrategia de testes (unit, integracao, e2e).
- Criar suite minima de smoke test para API.
- Configurar validacao automatica no CI.

### DevOps

- Criar `docker-compose` com API, Postgres e Redis.
- Configurar GitHub Actions para lint, teste e build.
- Padronizar `.env.example` por app.

## Riscos e dependencias

- Decisao entre Expo e Bare no mobile pode impactar cronograma.
- Falta de padrao de versoes Node/pnpm pode quebrar ambiente local.

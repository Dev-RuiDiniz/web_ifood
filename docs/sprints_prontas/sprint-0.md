# Sprint 0 Pronta - Fundacao Tecnica do MVP

## Contexto

Documento de sprint pronta baseado na proposta tecnica do projeto de marketplace (cliente app, loja web, admin web e backend Node/NestJS).

## Periodo sugerido

- Duracao: 2 semanas
- Timeboxes:
  - Semana 1: bootstrap de repositorio, backend e infraestrutura local
  - Semana 2: bootstrap de web/mobile, CI e qualidade minima

## Objetivo da sprint

Estabelecer base de desenvolvimento e operacao para o MVP com monorepo organizado, API inicial funcional e pipeline de qualidade.

## Definition of Ready (DoR)

- Decisao de stack validada: Next.js, React Native, NestJS, PostgreSQL, Redis.
- Definicao de papeis e responsaveis por trilha.
- Ambientes locais e segredos previstos em `.env.example`.

## Itens de backlog da sprint

1. Estruturar monorepo com `web`, `aplicativo`, `backend`, `packages`.
2. Subir API NestJS com Prisma, Postgres e Redis.
3. Criar apps `web/admin`, `web/loja` e `aplicativo/cliente` com bootstrap tecnico.
4. Configurar CI inicial (lint, testes de smoke e build).
5. Documentar convencoes, setup e comandos no README/docs.

## Criterios de aceite (DoD da sprint)

- `docker compose up` sobe servicos essenciais sem erro.
- Endpoint `/health` e `/ready` retornam 200.
- CI executa lint + teste smoke + build sem falhas.
- Estrutura de pastas e padroes documentados.
- Primeiro pacote compartilhado de tipos/validacoes criado.

## Plano por trilha

### Back-end

- [ ] Bootstrap `backend/api` com NestJS e TypeScript.
- [ ] Configurar Prisma + migration inicial.
- [ ] Criar entidades base (`users`, `stores`, `products`, `orders`, `payments`).
- [ ] Implementar healthcheck e conexao Redis.

### Front-end Web

- [ ] Bootstrap `web/admin` com Next.js.
- [ ] Bootstrap `web/loja` com Next.js.
- [ ] Configurar Tailwind + TanStack Query.
- [ ] Criar shell de layout e pagina de status.

### Mobile

- [ ] Bootstrap `aplicativo/cliente` com React Native + TypeScript.
- [ ] Configurar navegacao e camada HTTP base.
- [ ] Criar tela inicial de conectividade com API.

### QA

- [ ] Definir estrategia minima de testes por camada.
- [ ] Criar smoke tests de API.
- [ ] Integrar execucao de testes ao CI.

### DevOps

- [ ] Criar `docker-compose` com API + Postgres + Redis.
- [ ] Configurar GitHub Actions para CI inicial.
- [ ] Definir padrao de variaveis de ambiente.

## Dependencias externas

- Conta/projeto do provedor de notificacao OTP (para sprint 1).
- Registro de dominios/ambiente de homologacao (nao bloqueia sprint 0).

## Riscos da sprint

- Divergencia de versao Node/pnpm entre maquinas.
- Atraso na padronizacao de convencoes pode gerar retrabalho.

## Mitigacoes

- Publicar versoes obrigatorias em `README` e arquivos de configuracao.
- Revisao tecnica rapida no fim de cada semana.

## Saidas esperadas

- Base tecnica pronta para iniciar fluxos de produto na sprint 1.
- Time alinhado em qualidade minima, padrao de codigo e processo.

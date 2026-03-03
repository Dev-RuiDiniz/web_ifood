# web_ifood

Plataforma de marketplace de delivery inspirada no iFood, com foco em MVP vendavel e evolucao modular.

## Visao geral

O sistema possui 3 frentes principais:

1. Cliente (aplicativo iOS/Android): catalogo, busca, carrinho, checkout, rastreio, historico e suporte.
2. Loja/Restaurante (web): cardapio, precos, horarios, pedidos em tempo real, producao e cupons.
3. Admin (web): gestao de usuarios/lojas, taxas/comissoes, auditoria e relatorios.

Fluxo principal:
Cliente seleciona itens -> checkout -> pagamento -> loja recebe pedido em tempo real -> preparo/entrega -> rastreio -> finalizacao + avaliacao.

## Stack tecnica (proposta)

- Front-end Web: Next.js (App Router) + TypeScript + Tailwind + shadcn/ui + TanStack Query + WebSockets.
- Aplicativo: React Native + TypeScript (Expo ou Bare) + React Navigation + TanStack Query + Push (FCM/APNs).
- Backend: Node.js + TypeScript + NestJS + PostgreSQL + Redis + Prisma + BullMQ + Socket.IO.
- Infra: Docker/Compose, CI/CD com GitHub Actions, deploy web em Vercel e API em cloud (AWS/GCP/Render/Fly), CDN para imagens.

## Arquitetura

- Abordagem inicial: monolito modular (backend unico) com possibilidade de extracao para microservicos no crescimento.
- Comunicacao:
  - API principal REST (ou GraphQL no futuro).
  - WebSockets para status de pedidos em tempo real.
  - Filas para eventos assincronos (notificacoes, recibos, conciliacao).

## Modulos principais

- Auth & Accounts
- Catalog
- Cart & Checkout
- Orders
- Payments
- Delivery/Dispatch
- Promotions
- Notifications
- Admin
- Reporting/BI

## Estrutura de pastas

```txt
web_ifood/
  web/
    admin/
    loja/
  aplicativo/
    cliente/
    entregador/
  backend/
    api/
  packages/
    shared-types/
    shared-validators/
    shared-ui/
    shared-config/
  docs/
    sprints/
  PROPOSTA TECNICA – SISTEMA IFOOD #.pdf
  README.md
```

## Sprints (MVP)

As sprints devem ser registradas em `docs/sprints/`.

Sugestao inicial de fases:

1. Fundacao do monorepo e autenticacao base.
2. Catalogo + busca + carrinho no app cliente.
3. Checkout + pagamentos + webhooks.
4. Painel loja com pedidos em tempo real.
5. Admin basico (lojas, pedidos, taxas) + relatorios iniciais.
6. Hardening (seguranca, testes e observabilidade).

## Itens essenciais de qualidade

- JWT + refresh token + OTP.
- Rate limiting com Redis.
- LGPD (consentimento e ciclo de dados).
- Logs estruturados e trilha de auditoria.
- Testes unitarios, integracao e e2e.

## Proximos passos

1. Definir ferramenta de monorepo (Turborepo ou Nx).
2. Inicializar `backend/api` com NestJS + Prisma.
3. Inicializar `web/admin` e `web/loja` em Next.js.
4. Inicializar `aplicativo/cliente` em React Native.
5. Criar o backlog detalhado por sprint em `docs/sprints/`.

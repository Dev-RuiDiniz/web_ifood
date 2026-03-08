# Plano de Implementação: web_ifood

Este documento detalha a arquitetura, infraestrutura e o cronograma de desenvolvimento para a plataforma **web_ifood**, um marketplace de delivery modular e escalável.

## 1. Visão Geral da Arquitetura

O sistema será construído seguindo o padrão de **Monolito Modular** inicial, facilitando o desenvolvimento e deploy precoce, com uma estrutura preparada para transição para Microserviços se necessário.

### Tecnologia Stack
- **Backend:** NestJS (Node.js/TypeScript)
- **Banco de Dados:** PostgreSQL (Persistência) + Redis (Cache/Filas)
- **ORM:** Prisma
- **Mensageria/Filas:** BullMQ
- **Real-time:** Socket.io (WebSockets)
- **Web:** Next.js (App Router) + Tailwind CSS + shadcn/ui
- **Mobile:** React Native (Expo)
- **Monorepo:** Turborepo

---

## 2. Estrutura de Pastas (Monorepo)

```txt
web_ifood/
├── apps/
│   ├── web-admin/         # Painel administrativo geral
│   ├── web-loja/          # Painel do restaurante
│   ├── api/               # Backend NestJS
│   └── mobile-cliente/    # App React Native (Expo)
├── packages/
│   ├── shared-types/      # Interfaces e DTOs compartilhados
│   ├── shared-validators/ # Schemas Zod/class-validator
│   ├── shared-ui/         # Componentes React compartilhados
│   └── shared-config/     # Configurações de ESLint, TS, Tailwind
├── docs/
│   ├── sprints/           # Detalhamento de cada fase
│   └── architecture/      # Diagramas e especificações
└── docker/
    └── docker-compose.yml # Infraestrutura local (Postgres, Redis)
```

---

## 3. Modelo de Dados (EER) - Principais Entidades

### Núcleo de Usuários (Auth & Accounts)
- **User:** `id, email, password, role (ADMIN, STORE, CUSTOMER, COURIER), status`
- **Profile:** `user_id, name, phone, document (CPF/CNPJ)`
- **Address:** `user_id, street, number, neighborhood, city, lat, lng`

### Catálogo & Restaurantes
- **Restaurant:** `id, owner_id, name, category, rating, opening_hours, status`
- **Category:** `id, name, icon`
- **Product:** `id, restaurant_id, name, description, price, discount_price, image, status`

### Pedidos & Pagamentos
- **Order:** `id, customer_id, restaurant_id, courier_id, status, total, payment_method, createdAt`
- **OrderItem:** `id, order_id, product_id, quantity, unit_price`
- **Payment:** `id, order_id, status, provider_id, amount, method (PIX, CARD)`

---

## 4. Detalhamento de Módulos e Funcionalidades

### 4.1. Módulo Loja (Web Restaurante)
- **Gestão de Pedidos:** Tela com som de alerta para novos pedidos, timeline de status (Aceitar -> Preparar -> Despachar).
- **Gestão de Cardápio:** Categorias (Pizzas, Bebidas), pausar itens (esgotado), fotos e preços diferenciados.
- **Configuração da Loja:** Horários de funcionamento, raio de entrega, tempo médio de preparo.
- **Financeiro:** Extrato de repasses, configuração de conta bancária.

### 4.2. Módulo Admin (Gestão da Plataforma)
- **Gestão de Parceiros:** Onboarding de novos restaurantes, aprovação de documentos.
- **Configurações Globais:** Taxas de comissão por categoria, taxas de entrega base.
- **Promoções & Cupons:** Criação de campanhas (ex: "Primeiro Pedido", "Frete Grátis acima de R$50").
- **Auditoria & Logs:** Rastreio de todas as ações sensíveis no sistema (LGPD compliant).

### 4.3. aplicativo Cliente (Mobile)
- **Geolocalização:** Listagem de lojas próximas, endereços favoritados.
- **Busca & Filtros:** Busca por prato ou restaurante, filtros por preço, avaliação e tempo de entrega.
- **Fluxo de Pagamento:** Checkout simplificado com Apple Pay/Google Pay e PIX.
- **Notificações:** Push notifications para cada alteração de status do pedido.

---

## 5. Infraestrutura e Serviços

### Cache e Performance
- **Redis:** Utilizado para cache de catálogos populares, sessões de usuário (Refresh Tokens) e Rate Limiting.
- **CDN:** AWS S3 + CloudFront (ou similar) para armazenamento e entrega de imagens.

### Eventos Assíncronos (BullMQ)
- Processamento de webhooks de pagamento.
- Envio de notificações Push/E-mail.
- Geração de relatórios pesados.
- Conciliação financeira.

### Real-time (Socket.IO)
- **Status do Pedido:** Atualização instantânea para o cliente.
- **Gestão de Pedidos:** Restaurante recebe novos pedidos sem refresh.
- **Rastreio:** Coordenadas do entregador enviadas para o cliente em tempo real.

---

## 5. Cronograma de Sprints (MVP)

| Sprint | Foco | Entregáveis Principais |
| :--- | :--- | :--- |
| **1** | Fundação | Setup Turborepo, NestJS + Prisma, Auth JWT/OTP, Docker Compose. |
| **2** | Catálogo | CRUD de Restaurantes/Produtos, Busca Geográfica, Upload de Imagens. |
| **3** | Checkout | Fluxo de Carrinho, Integração Pix/Cartão, Cálculo de Frete. |
| **4** | Loja | Dashboard de pedidos em tempo real (Socket.io), Gestão de Estoque. |
| **5** | Admin & BI | Relatórios de Vendas, Comissões, Auditoria de Logs. |
| **6** | Polimento | Segurança (Rate Limit), Testes E2E, Preparação para Deploy. |

---

## 6. Itens de Qualidade e Segurança
- **Autenticação:** JWT com Refresh Token em Cookie HTTP-only + Verificação OTP via e-mail/SMS.
- **LGPD:** Soft delete de dados sensíveis, logs de acesso e termo de consentimento.
- **Observabilidade:** Structured Logging (Winston) e rastreamento de erros (Sentry).
- **Testes:** Unitários (Jest) e Integração (Prisma + Testcontainers).

---

## 7. Próximos Passos Imediatos
1. Executar `npx create-turbo@latest` para estruturar o monorepo.
2. Configurar `apps/api` (NestJS) com Prisma e conexão Postgres/Redis.
3. Definir Schemas fundamentais no `schema.prisma`.
4. Implementar Auth Module (Login/Register/Me).

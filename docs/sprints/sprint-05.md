# Sprint 05 - Admin Basico, Relatorios e Governanca

## Objetivo

Disponibilizar painel admin MVP com operacao minima de lojas, pedidos, taxas, auditoria e relatorios iniciais.

## Escopo funcional

- Gestao de lojas (aprovacao, bloqueio, consulta).
- Gestao de usuarios por perfil.
- Parametrizacao basica de taxas/comissoes.
- Relatorios iniciais: vendas, ticket medio, cancelamentos.
- Exportacao simples de dados.

## Criterios de aceite

- Admin aplica aprovacao de loja com rastreabilidade.
- Alteracao de taxa gera registro de auditoria.
- Relatorios exibem dados consistentes com base transacional.
- Exportacao funcional com filtro de periodo.

## Entregaveis

- Modulo `Admin` funcional (MVP).
- Modulo `Reporting/BI` inicial.
- Trilha de auditoria para acoes administrativas.

## Tarefas por trilha

### Back-end

- Endpoints admin protegidos por RBAC.
- Servicos de agregacao para relatorios.
- Entidade `audit_logs` e consulta paginada.

### Front-end Web

- `web/admin`: telas de lojas, usuarios, taxas e relatorios.
- Filtros por data, status e loja.

### Mobile

- Ajustes de exibicao de historico e suporte basico.
- Coleta de eventos para analytics.

### QA

- Validar permissao por role e cenarios de abuso.
- Testar consistencia dos relatorios com dados de fixtures.

### DevOps

- Retencao de logs e auditoria.
- Dashboard operacional para pedidos e receita.

## Riscos e dependencias

- Regras de negocio de comissionamento podem variar por cidade.

# Sprint 06 - Hardening, Qualidade e Go-live Controlado

## Objetivo

Fortalecer seguranca, qualidade e observabilidade para liberar MVP em rollout controlado por cidade/loja.

## Escopo funcional

- Rate limiting e protecao de abuso.
- Revisao de LGPD (consentimento e ciclo de dados).
- Observabilidade (metricas, tracing, alertas).
- Testes de regressao e performance basica.
- Feature flags para rollout gradual.

## Criterios de aceite

- Limites de taxa aplicados e testados em endpoints criticos.
- Logs estruturados e trilha de auditoria cobrindo fluxo principal.
- Cobertura minima acordada para modulos core.
- Rollout controlado habilitado por flag sem novo deploy.

## Entregaveis

- Baseline de seguranca e operacao.
- Checklists de liberacao de producao.
- Plano de contingencia e rollback.

## Tarefas por trilha

### Back-end

- Rate limiting com Redis em auth/checkout/pagamento.
- Endpoints LGPD para exportacao/remocao quando aplicavel.
- Ajustes de performance em queries de `orders`.

### Front-end Web

- Tratamento de erros e estados de indisponibilidade.
- Melhorias de usabilidade para operacao de loja/admin.

### Mobile

- Refino de UX no fluxo de pedido e rastreio.
- Instrumentacao de eventos de crash e conversao.

### QA

- Regressao completa de fluxo pedido/pagamento.
- Testes de carga basicos em checkout e pedidos.
- Testes de seguranca (auth, permissao, abuso).

### DevOps

- Alertas de SLO/SLI e runbook de incidentes.
- Feature flags por cidade/loja.
- Plano de backup e restauracao do banco.

## Riscos e dependencias

- Ausencia de observabilidade adequada aumenta risco de go-live.

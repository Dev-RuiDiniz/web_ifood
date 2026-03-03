# Sprint 03 - Pagamentos e Webhooks

## Objetivo

Concluir pagamento online (cartao/PIX) com idempotencia e conciliacao basica via webhooks.

## Escopo funcional

- Integracao com gateway de pagamento (MVP: 1 provedor).
- Fluxo de pagamento no checkout.
- Recebimento e processamento de webhooks.
- Atualizacao de status de `payments` e `orders`.

## Criterios de aceite

- Pagamento aprovado muda pedido para estado correto.
- Pagamento recusado mantem trilha de motivo e permite nova tentativa.
- Webhook duplicado nao gera duplicidade de pagamento/pedido.
- Auditoria de transicoes de status registrada.

## Entregaveis

- Modulo `Payments` funcional.
- Fluxo robusto de idempotencia.
- Logs e trilha de auditoria para eventos financeiros.

## Tarefas por trilha

### Back-end

- Adapter do provedor de pagamento.
- Tabela `payments` com `provider`, `externalId`, `status`.
- Handler de webhook com verificacao de assinatura.

### Front-end Web

- `web/admin`: visao basica de status financeiro por pedido.

### Mobile

- Integracao do checkout com intents de pagamento.
- Tratamento UX de sucesso, falha e pendencia.

### QA

- Simulacoes de webhook, timeout e retry.
- Casos de idempotencia com repeticao de eventos.

### DevOps

- Endpoint de webhook exposto com seguranca.
- Alertas para alta taxa de falha em pagamento.

## Riscos e dependencias

- SLA/instabilidade do provedor afeta conversao.
- Regras antifraude avancadas ainda estarao fora do MVP.

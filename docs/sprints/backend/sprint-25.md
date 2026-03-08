# Sprint 25: Webhooks e Confirmação de Pagamento

## Atividades da Sprint
- Recebimento de notificações assíncronas do gateway para atualizar o status do pedido.
- Segurança de endpoints de webhook.

## Estrutura e Classes
- **Controllers/Webhooks/PaymentsWebhookController.cs**.
- **Services/Commands/UpdateOrderStatus/UpdateOrderPayStatusHandler.cs**.

## Segurança
- Validação de assinaturas (Signatures) enviadas pelo gateway no Header para evitar ataques de injeção.
- Idempotência: Garantir que processar o mesmo webhook duas vezes não duplique créditos.

## Testes e Validação
- **Security Test:** Enviar um payload falso sem assinatura e validar o retorno 401/403.
- **Idempotency Test:** Disparar o mesmo webhook de sucesso 3 vezes e ver se o status do pedido continua como `Paid` sem erros.

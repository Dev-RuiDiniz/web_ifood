# Sprint 29: Cancelamentos e Estornos Automáticos

## Atividades da Sprint
- Lógica complexa para cancelamento por parte do cliente ou restaurante.
- Devolução de valores via API do Gateway.

## Lógica de Negócio
- Regra de Tempo: Cliente só cancela sem taxas se o restaurante não tiver "aceitado" o pedido.
- Estorno PIX: Chamada para a API do banco/gateway para devolver o valor instantaneamente.

## Classes
- **Services/Commands/CancelOrder/CancelOrderHandler.cs**.
- **Services/Payments/IRefundService.cs**.

## Testes e Validação
- **Refund Integration Test:** Validar em sandbox que a chamada de estorno retorna sucesso e o status do pedido muda para `Refunded`.
- **Tax Test:** Calcular multa de cancelamento se o tempo limite for excedido.

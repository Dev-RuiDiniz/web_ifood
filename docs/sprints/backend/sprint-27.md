# Sprint 27: Fluxo de Pedido Real-time (Loja)

## Atividades da Sprint
- Notificação instantânea para o painel do restaurante ao receber um novo pedido pago.
- Ações de Aceitar/Recusar.

## Lógica de Negócio
- Ao confirmar o pagamento, disparar evento SignalR para o `RestaurantGroup`.
- Som de alerta no front-end disparado pelo backend via `Context.Clients.Group(...)`.

## Classes
- **Services/Events/OrderCreatedEvent.cs**.
- **Services/Events/Handlers/NotifyRestaurantHandler.cs**.

## Testes e Validação
- **Latência Test:** Medir o tempo entre o webhook de pagamento e a notificação aparecer no "painel da loja".
- **Reliability:** Validar se mensagens são reenviadas caso o socket sofra uma micro-queda.

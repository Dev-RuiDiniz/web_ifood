# Sprint 28: Status do Pedido (Timeline)

## Atividades da Sprint
- Implementação de todos os estados do ciclo de vida: `Prep`, `Ready`, `Dispatch`, `Delivered`.
- Notificação push para o app cliente em cada etapa.

## Estrutura e Classes
- **Enums/OrderStatus.cs**.
- **Services/Ordering/OrderStateService.cs**: Máquina de estados para garantir transições válidas (ex: não pode ir de `Created` direto para `Delivered`).

## Integrações
- Integração com Firebase Cloud Messaging (FCM) para notificações fora do app.

## Testes e Validação
- **State Machine Test:** Tentar forçar uma transição de status inválida e validar a exceção.
- **Push Test:** Validar se o payload do FCM está formatado corretamente com os dados do pedido.

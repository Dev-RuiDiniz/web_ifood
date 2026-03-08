# Sprint 23: Checkout e Criação de Pedido (Status: Pending)

## Atividades da Sprint
- Transformação do Carrinho em um Pedido persistente no banco relacional.
- Reserva de estoque (se aplicável).

## Estrutura e Classes
- **Domains/Entities/Order.cs**: OrderNumber, Total, Status (Created, PendingPayment).
- **Domains/Entities/OrderItem.cs**: Snapshot do preço no momento da compra.
- **Services/Commands/CreateOrder/CreateOrderHandler.cs**.

## Lógica de Negócio
- Geração de código curto e legível para o pedido (ex: #5492).
- Bloqueio de checkout se o restaurante estiver `Offline`.

## Testes e Validação
- **Atomic Test:** Garantir que se a criação do `OrderItem` falhar, o `Order` não seja salvo (UoW).
- **Snapshot Test:** Validar que se o preço do produto mudar após a compra, o valor no `OrderItem` permaneça o original (Imutabilidade).

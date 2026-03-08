# Sprint 34: Rastreamento em Tempo Real (Cliente)

## Atividades da Sprint
- Streaming das coordenadas do entregador para o app do cliente via WebSockets.

## Lógica de Negócio
- Quando o pedido muda para `OutForDelivery`, o backend começa a retransmitir as atualizações de `Lat/Lng` do entregador X para o `ClientGroup` do pedido Y.

## Tecnologias
- SignalR (usado na Sprint 26).

## Testes e Validação
- **End-to-End Test:** Simular movimento do entregador e validar se o cliente recebeu os eventos `UpdateLocation` sem precisar fazer polling na API.

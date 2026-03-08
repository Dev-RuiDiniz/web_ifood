# Sprint 33: Algoritmo de Despacho (Matching)

## Atividades da Sprint
- Lógica para atribuir um pedido pronto ao entregador mais próximo e disponível.

## Estrutura e Classes
- **Services/Logistics/IDispatchService.cs**.
- **Services/Logistics/NearbyDispatchService.cs**: Algoritmo que busca entregadores num raio crescente (1km -> 3km -> 5km) até encontrar um aceite.

## Lógica de Negócio
- Sistema de "Convite de Entrega": O entregador tem 30 segundos para aceitar antes de passar para o próximo.
- Prevenção de "Cherry Picking": Bloqueio temporário se o entregador recusar 3 pedidos seguidos.

## Testes e Validação
- **Match Simulation:** Criar um pedido e 3 entregadores fake em posições diferentes e validar se o convite foi enviado para o mais próximo primeiro.

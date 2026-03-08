# Sprint 32: Geofencing e Status do Entregador

## Atividades da Sprint
- Controle de disponibilidade dos entregadores no mapa.
- Transição entre estados `Online` (Disponível) e `Busy` (Em Entrega).

## Bancos de Dados
- **Redis (Geospatial Indexes)**: Utilizar comandos `GEOADD` e `GEORADIUS` para encontrar entregadores em tempo real de forma ultra-rápida.

## Estrutura e Classes
- **Data/RedisGeoRepository.cs**: Métodos `SetCourierLocation`, `GetNearbyCouriers`.
- **Controllers/CourierController.cs**: Endpoint `PUT /location` disparado pelo app a cada 30-60 segundos.

## Testes e Validação
- **Performance Test:** Validar que a busca por 100 entregadores num raio de 5km via Redis ocorre em menos de 10ms.
- **Accuracy:** Testar atualização de coordenadas e persistência temporária (TTL).

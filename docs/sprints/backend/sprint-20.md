# Sprint 20: Busca Geográfica (Restaurantes Próximos)

## Atividades da Sprint
- Implementação de inteligência para retornar apenas lojas que entregam no endereço do cliente.
- Cálculo de distância via coordenadas.

## Estrutura e Classes
- **Repositories/Extensions/RestaurantGeoExtensions.cs**: Uso de funções espaciais ou Haversine Formula no SQL.
- **Services/Queries/SearchRestaurants/SearchRestaurantsQuery.cs**: Parâmetros: `Lat`, `Lng`, `MaxDistance`.

## Bancos de Dados
- **PostGIS**: Extensão do Postgres para queries espaciais otimizadas.

## Testes e Validação
- **Geofence Test:** Validar que uma loja com raio de 5km não aparece para um cliente a 10km de distância.
- **Accuracy Test:** Comparar distância calculada com Google Maps para precisão básica.

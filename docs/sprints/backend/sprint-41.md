# Sprint 41: Caching Agressivo (Result Caching)

## Atividades da Sprint
- Redução drástica da latência e carga no banco de dados através de cache de queries.

## Ferramentas e Configurações
- **Redis**: Armazenamento dos resultados de busca e detalhe de restaurante.
- **Cache-Aside Pattern**: Lógica na camada de Query Handler para buscar no Redis antes do DB.

## Lógica de Negócio
- Invalidação Inteligente: Se o restaurante mudar o preço de um prato, o cache daquele restaurante deve ser limpo imediatamente.

## Testes e Validação
- **Benchmark:** Comparar tempo de resposta da home (`GET /restaurants/nearby`) com e sem cache (Esperado: redução de >80% no tempo).
- **Stale Data Test:** Garantir que após alteração no DB, o cache reflita a nova informação em menos de 1 segundo.

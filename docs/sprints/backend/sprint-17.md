# Sprint 17: Categorias e Tags Globais

## Atividades da Sprint
- Gerenciamento de categorias de culinária (Brasileira, Pizza, Japonesa) e filtros.
- Associação N:N entre Restaurantes e Categorias.

## Estrutura e Classes
- **Domains/Entities/Category.cs**: Nome e IconPath (URL CDN).
- **Domains/Entities/RestaurantCategory.cs**: Tabela associativa.
- **Services/Queries/GetCategories/GetCategoriesHandler.cs**.

## Testes e Validação
- **Search Test:** Buscar restaurantes através de uma categoria específica e validar os resultados.
- **Integrity Test:** Tentar deletar uma categoria que possui restaurantes associados (Regra: Bloquear ou desassociar).

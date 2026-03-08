# Sprint 18: Gestão de Cardápio (Produtos)

## Atividades da Sprint
- Implementação dos itens à venda em cada restaurante.
- Suporte a variações e preços promocionais.

## Estrutura e Classes
- **Domains/Entities/Product.cs**: Nome, Descrição, Preço, Custo, Imagem, IsActive.
- **Controllers/StoreMenuController.cs**: Endpoints para o dono do restaurante gerir seus itens.

## Lógica de Negócio
- Regra de Preço: `DiscountPrice` não pode ser maior que o `OriginalPrice`.
- Pausa de Item: Endpoint rápido para mudar o status para `Out of Stock`.

## Testes e Validação
- **Business Rule Test:** Tentar salvar um desconto de 50% sobre um preço que resultaria em valor negativo.
- **Availability Test:** Validar que produtos inativos não aparecem na listagem para o cliente.

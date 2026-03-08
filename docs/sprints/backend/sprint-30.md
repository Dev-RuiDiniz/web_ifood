# Sprint 30: Avaliações e Ratings

## Atividades da Sprint
- Sistema de feedback para restaurantes e entregadores.
- Cálculo de média aritmética de ratings.

## Estrutura e Classes
- **Domains/Entities/Review.cs**: Stars (1-5), Comment, OrderId, UserId.
- **Services/Commands/AddReview/AddReviewHandler.cs**.

## Lógica de Negócio
- Um usuário só pode avaliar um pedido que esteja com status `Delivered`.
- Uma avaliação por pedido.

## Testes e Validação
- **Math Test:** Adicionar 5 avaliações com notas diferentes e validar se a média no perfil do restaurante foi atualizada corretamente.
- **Duplicate Review Test:** Tentar avaliar o mesmo pedido duas vezes e validar o erro 400.

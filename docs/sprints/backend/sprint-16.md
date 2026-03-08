# Sprint 16: Onboarding de Restaurantes

## Atividades da Sprint
- Início do ecossistema de marketplace com o cadastro de parceiros.
- Validação de documentos empresariais (CNPJ).

## Estrutura e Classes
- **Domains/Entities/Restaurant.cs**: Propriedades como `CommercialName`, `LegalName`, `CNPJ`, `Status` (Pending/Active/Paused).
- **Repositories/IRestaurantRepository.cs**.
- **Controllers/RestaurantAdminController.cs**: CRUD exclusivo para administradores da plataforma.

## Testes e Validação
- **Duplicate Test:** Impedir cadastro de dois restaurantes com o mesmo CNPJ.
- **Status Test:** Garantir que um restaurante novo comece com status `Pending` por padrão.

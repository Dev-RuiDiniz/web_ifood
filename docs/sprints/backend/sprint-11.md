# Sprint 11: Gestão de Perfis e Endereços

## Atividades da Sprint
- Implementação completa do CRUD de múltiplos endereços por usuário.
- Atualização de dados cadastrais sensíveis.

## Estrutura e Classes
- **Domains/Entities/Address.cs**: Entidade com validações geográficas.
- **Services/Commands/UpdateProfile/UpdateProfileHandler.cs**.
- **Controllers/ProfileController.cs**: Endpoints `GET /me`, `PUT /me`, `POST /addresses`.

## Lógica de Negócio
- Regra de "Endereço Principal": apenas um endereço pode ser marcado como principal por vez.
- Limite de 10 endereços salvos por usuário.

## Testes e Validação
- **Unit Test:** Garantir que ao marcar um novo endereço como principal, o antigo perca essa flag.
- **Integration Test:** Validar persistência de múltiplos endereços associados ao ID do usuário autenticado.

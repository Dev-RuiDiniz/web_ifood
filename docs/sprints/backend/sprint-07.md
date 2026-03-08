# Sprint 07: Autenticação Avançada (Refresh Tokens)

## Atividades da Sprint
- Manutenção da sessão do usuário sem necessidade de re-login constante.
- Invalidação de sessões comprometidas.

## Estrutura e Classes
- **Domains/Entities/RefreshToken.cs**: Entidade para armazenar tokens no banco/Cache.
- **Services/Commands/RefreshToken/RefreshTokenCommandHandler.cs**.
- **Data/RedisService.cs**: Implementação de persistência rápida para tokens temporários.

## Lógica de Negócio
- Rota para renovar `AccessToken` enviando um `RefreshToken` válido.
- Revogação de tokens em caso de Logout ou alteração de senha.

## Testes e Validação
- **Persistence Test:** Verificar se o Refresh Token está sendo salvo e recuperado corretamente do Redis.
- **Expiry Test:** Validar que um Refresh Token expirado não gera novos Access Tokens.

# Sprint 06: Autenticação Segura (JWT + Identity)

## Atividades da Sprint
- Implementação do sistema de autenticação e geração de tokens de acesso.
- Proteção de rotas e definição de Claims.

## Ferramentas e Pacotes
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `BCrypt.Net-Next`: Para hashing de senhas.
- `System.IdentityModel.Tokens.Jwt`

## Estrutura e Classes
- **Services/Auth/TokenService.cs**: Gerador de JWT com SecretKey em ambiente seguro.
- **Dtos/Auth/LoginDto.cs** e **LoginResponseDto.cs**.
- **Controllers/AuthController.cs**: Endpoint de Login e validação inicial.

## Segurança
- Configuração de políticas de expiração de token (15-60 min).
- Armazenamento seguro de secrets via Environment Variables.

## Testes e Validação
- **Integration Test:** Chamar endpoint `/api/auth/login` e validar se o Bearer Token retornado é válido.
- **Protection Test:** Tentar acessar rota `[Authorize]` sem token e garantir retorno 401.

# Sprint 05: CQRS com MediatR e Handlers

## Atividades da Sprint
- Implementação da segregação de responsabilidades entre escrita (Commands) e leitura (Queries).
- Desacoplamento entre a Controller e a lógica de serviço.

## Ferramentas e Pacotes
- `MediatR` (v12.0+)
- `MediatR.Extensions.Microsoft.DependencyInjection`

## Estrutura de Pastas (dentro de Services/Handlers)
- **Services/Commands/CreateUser/CreateUserCommand.cs**: DTO de entrada para criação.
- **Services/Commands/CreateUser/CreateUserHandler.cs**: Lógica de execução que orquestra Repositórios e Domínio.
- **Services/Queries/GetUser/GetUserQuery.cs**: Parâmetros de consulta.

## Testes e Validação
- **Handler Unit Tests:** Simular o envio de um comando e verificar se o handler interage com o repositório esperado.
- **Pipeline Behaviors:** (Opcional) Testar logs automáticos antes de cada handler.

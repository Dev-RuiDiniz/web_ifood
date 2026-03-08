# Sprint 03: Pattern Repository e Unit of Work

## Atividades da Sprint
- Implementação de abstrações para desacoplamento da camada de dados.
- Gerenciamento atômico de transações.

## Estrutura e Classes
- **Repositories/Interfaces/IBaseRepository.cs**: Interface genérica para operações CRUD (Add, Update, Delete, GetById, GetAll).
- **Repositories/BaseRepository.cs**: Implementação base utilizando Generics e DbSet.
- **Data/Interfaces/IUnitOfWork.cs**: Contrato para o comando `CommitAsync`.
- **Data/UnitOfWork.cs**: Implementação que garante que múltiplas operações de repositório usem a mesma transação.

## Injeção de Dependência
- Configuração no `Program.cs` para registrar os repositórios via Scoped.

## Testes e Validação
- **Unit Testing (xUnit + Moq):** Validar se o Repositório chama corretamente os métodos do DbSet.
- **Unit of Work Test:** Verificar se o `SaveChanges` é chamado apenas uma vez em operações compostas.

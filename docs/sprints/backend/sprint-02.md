# Sprint 02: Camada de Dados e EF Core (Code First)

## Atividades da Sprint
- Mapeamento das entidades base para o banco de dados.
- Configuração do contexto do Entity Framework e políticas de migração.

## Ferramentas e Pacotes
- `Microsoft.EntityFrameworkCore` (v8.0+)
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `Microsoft.EntityFrameworkCore.Design`

## Estrutura e Classes
- **Data/Context/AppDbContext.cs**: Classe principal de contexto gerenciando o ciclo de vida das transações.
- **Data/Mappings/**: Classes de configuração Fluent API (IEntityTypeConfiguration).
- **Domains/Base/BaseEntity.cs**: Entidade abstrata com ID (Guid), CreatedAt e UpdatedAt.

## Mapeamentos Iniciais
- `UserMapping`: Definição de índices únicos para Email e CPF/CNPJ.
- `AddressMapping`: Relacionamento 1:N com usuários.

## Testes e Validação
- **Migration Test:** Execução de `dotnet ef migrations add InitialCreate` e validação do schema gerado no Postgres.
- **Integration Test:** Teste de persistência de um usuário fake usando `InMemoryDatabase` ou banco de dev.

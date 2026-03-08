# Sprint 04: Domain-Driven Design (DDD) e Value Objects

## Atividades da Sprint
- Enriquecimento das entidades de domínio com lógica de negócio e validações.
- Criação de Objetos de Valor para tipos complexos.

## Conceitos e Classes
- **Domains/ValueObjects/Email.cs**: Lógica de validação de formato de e-mail.
- **Domains/ValueObjects/Document.cs**: Validação de algoritmos CNPJ/CPF.
- **Domains/ValueObjects/AddressCoords.cs**: Estrutura para Latitude e Longitude.
- **Domains/Entities/User.cs**: Implementação de métodos de domínio (ex: `ChangePassword`, `ActivateUser`).

## Ferramentas
- `FluentValidation`: Para regras de validação consistentes nos Domain Objects.

## Testes e Validação
- **Value Object Tests:** Testar falhas de validação para documentos inválidos e e-mails mal formatados.
- **Domain Logic Tests:** Garantir que o estado da entidade mude corretamente através de seus métodos públicos.

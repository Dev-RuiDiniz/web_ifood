# Sprint 08: AutoMapper e Padronização de DTOs

## Atividades da Sprint
- Mapeamento automático entre Entidades de Domínio e DTOs de saída.
- Redução de boilerplate code nas Controllers e Handlers.

## Ferramentas e Pacotes
- `AutoMapper`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`

## Estrutura e Classes
- **Services/Mappings/MappingProfile.cs**: Configuração global de mapeamentos.
- **Dtos/Users/UserDetailDto.cs**: DTO otimizado para exibição de perfil.

## Regras de Mapeamento
- Conversão de `Value Objects` para strings simples em DTOs.
- Ocultação de campos sensíveis (Passwords, Internal IDs) na saída.

## Testes e Validação
- **Mapping Assertions:** Testar se o AutoMapper consegue converter `User` -> `UserDetailDto` sem campos nulos ou erros de cast.

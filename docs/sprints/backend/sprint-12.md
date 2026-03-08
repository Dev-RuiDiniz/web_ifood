# Sprint 12: RBAC (Role-Based Access Control)

## Atividades da Sprint
- Definição de níveis de acesso para Clientes, Donos de Loja e Administradores.
- Implementação de autorização por Claims.

## Ferramentas e Configurações
- **Enums/UserRole.cs**: `Admin`, `StoreOwner`, `Customer`, `Courier`.
- **Policy Configuration**: Configuração de `AddAuthorization(options => ...)` no `Program.cs`.

## Segurança
- Uso do atributo `[Authorize(Roles = "Admin")]` em rotas críticas.
- Custom Authorization Handlers para lógica complexa (ex: "Apenas o dono da loja pode editar o menu da sua loja").

## Testes e Validação
- **Security Check:** Tentar deletar um usuário logado como `Customer` (esperado: 403 Forbidden).
- **Admin Check:** Validar acesso total às rotas de auditoria como `Admin`.

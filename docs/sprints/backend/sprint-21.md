# Sprint 21: Lógica de Carrinho Persistentemente (Redis)

## Atividades da Sprint
- Gestão de itens temporários antes do fechamento do pedido.
- Persistência rápida para evitar perda de dados em caso de refresh no app/web.

## Ferramentas e Configurações
- **StackExchange.Redis**: Cliente para interação com Redis.
- **Serialization**: Uso de `System.Text.Json` para converter objetos do carrinho em strings JSON.

## Estrutura e Classes
- **Dtos/Cart/CartDto.cs**: Itens, quantidades, observações.
- **Services/Cart/ICartService.cs**: Interface com métodos `AddItem`, `RemoveItem`, `ClearCart`.

## Lógica de Negócio
- Regra de "Loja Única": O cliente não pode adicionar itens de dois restaurantes diferentes no mesmo carrinho (limpeza automática ou erro).

## Testes e Validação
- **Concurrency Test:** Garantir que se o usuário abrir o app em dois celulares, o carrinho seja sincronizado via Redis.
- **Validation Test:** Validar rejeição de adição de item de loja B se já existir item da loja A.

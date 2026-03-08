# Sprint 22: Motor de Cálculo de Frete

## Atividades da Sprint
- Sistema dinâmico para calcular taxas de entrega baseadas em distância e faixas de preço.

## Estrutura e Classes
- **Domains/Entities/DeliveryFeeConfig.cs**: Tabela com faixas (0-2km: R$ 5,00, etc).
- **Services/Delivery/IDeliveryCalculator.cs**: Lógica que recebe `RestaurantId` e `CustomerAddressId`.

## Lógica de Negócio
- Suporte a "Frete Grátis" acima de um valor X configurado por loja.
- Adicional de urgência ou horários de pico (configurável globalmente).

## Testes e Validação
- **Unit Test:** Configurar faixas e validar se a distância calculada cai no preço correto.
- **Promo Test:** Garantir que se o pedido for de R$ 100 e a regra for frete grátis a partir de R$ 80, a taxa retorne zero.

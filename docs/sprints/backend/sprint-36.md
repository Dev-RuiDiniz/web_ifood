# Sprint 36: Sistema de Cupons de Desconto

## Atividades da Sprint
- Criação de códigos promocionais com regras complexas de validação.

## Estrutura e Classes
- **Domains/Entities/Coupon.cs**: Code, DiscountType (Fixed/Percent), MaxUsage, MinOrderValue, ExpiryDate.
- **Services/Promotions/ICouponValidator.cs**.

## Lógica de Negócio
- "Cupom de Primeira Compra": Verificar histórico de pedidos do usuário antes de aplicar.
- Cupons vinculados a categorias específicas (ex: "PIZZA10" só funciona em pizzarias).

## Testes e Validação
- **Validation Test:** Tentar aplicar um cupom expirado ou com valor de pedido abaixo do mínimo.
- **Usage Limit Test:** Garantir que o contador de uso do cupom seja incrementado apenas após a confirmação do pagamento.

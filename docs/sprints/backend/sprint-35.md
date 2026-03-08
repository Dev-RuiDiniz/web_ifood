# Sprint 35: Gestão de Repasses Financeiros (Payouts)

## Atividades da Sprint
- Cálculo de quanto a plataforma deve ao restaurante e ao entregador após cada venda.

## Estrutura e Classes
- **Domains/Entities/Statement.cs**: Registro de débito/crédito por transação.
- **Services/Finance/IBalanceService.cs**.

## Lógica de Negócio
- Retenção da comissão da plataforma (ex: 15% do restaurante, 2% do gateway).
- Agendamento de saque (Payout) semanal ou quinzenal.

## Testes e Validação
- **Calculation Test:** Validar que num pedido de R$ 100, os valores líquidos para Loja, Entregador e Plataforma somem exatamente R$ 100 (Conciliação).

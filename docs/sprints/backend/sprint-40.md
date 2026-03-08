# Sprint 40: Auditoria Crítica e Segurança Avançada

## Atividades da Sprint
- Proteção contra fraudes sofisticadas e auditoria de transações financeiras.

## Estrutura e Classes
- **Middlewares/RateLimitingMiddleware.cs**: Limitar chamadas de API por IP/Usuário no Redis.
- **Services/Security/IFraudDetectionService.cs**: Análise de padrão de compra (ex: 5 pedidos de contas diferentes usando o mesmo cartão em 10 minutos).

## Ferramentas
- `AspNetCoreRateLimit` ou implementação customizada no Redis.

## Testes e Validação
- **Stress Test:** Tentar bombardear o endpoint de Checkout com 100 requisições/seg e validar o bloqueio (429 Too Many Requests).

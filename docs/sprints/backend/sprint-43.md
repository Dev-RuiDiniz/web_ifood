# Sprint 43: Resiliência e Circuit Breaker

## Atividades da Sprint
- Proteção da API contra falhas de serviços externos (Gateway de Pagamento, Google Maps, FCM).

## Ferramentas e Pacotes
- `Polly`: Biblioteca rica em resiliência para .NET.

## Lógica de Negócio
- **Retry Policy:** Tentar enviar notificação 3 vezes em caso de timeout.
- **Circuit Breaker:** Parar de chamar o Gateway de Pagamento se ele estiver fora do ar, retornando erro imediato ao usuário (fail-fast) ao invés de travar a thread.

## Testes e Validação
- **Chaos Testing:** Simular falha manual no serviço de e-mail e verificar se o Polly está executando as retentativas conforme configurado.

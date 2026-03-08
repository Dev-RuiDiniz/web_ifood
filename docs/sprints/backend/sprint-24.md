# Sprint 24: Integração com Gateway de Pagamento (PIX)

## Atividades da Sprint
- Geração de código Copia e Cola e QR Code para pagamentos instantâneos.

## Ferramentas e Pacotes
- `Gateway SDK` (Stripe, Mercado Pago ou Pagar.me).
- `QRCoder`: Biblioteca para gerar imagens de QR Code localmente.

## Estrutura e Classes
- **Services/Payments/IPaymentProvider.cs**.
- **Services/Payments/PixPaymentService.cs**.
- **Dtos/Payments/PixResponseDto.cs**: Copia e Cola, QR Code Base64 e expiração.

## Testes e Validação
- **Simulation:** Usar ambiente de Sandbox do gateway para gerar um PIX real e validar a resposta.
- **Handling Failure:** Tratar erros de comunicação com o provedor (Timeout, Invalid Data).

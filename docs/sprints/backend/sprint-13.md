# Sprint 13: Verificação OTP (One-Time Password)

## Atividades da Sprint
- Implementação de segurança extra (2FA) para logins e alterações de dados.
- Integração com serviço de envio de e-mail/SMS (Mock inicial).

## Estrutura e Classes
- **Domains/Entities/OtpCode.cs**: Código de 6 dígitos, expiração e flag de uso.
- **Services/Auth/IOtpService.cs**: Interface para envio e validação.
- **Services/Auth/EmailOtpService.cs**: Implementação enviando via SMTP.

## Ferramentas
- `MailKit` ou `FluentEmail`.

## Testes e Validação
- **Validation Test:** Testar código correto antes da expiração (Sucesso).
- **Expiração Test:** Testar código correto após 5 minutos (Erro: Código Expirado).
- **Reuso Test:** Tentar usar o mesmo código duas vezes (Erro: Código já utilizado).

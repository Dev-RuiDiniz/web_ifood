# Sprint 14: LGPD (Consentimento e Delegação)

## Atividades da Sprint
- Conformidade com a Lei Geral de Proteção de Dados.
- Gestão de termos de uso e exclusão definitiva (ou anonimização).

## Estrutura e Classes
- **Domains/Entities/TermConsent.cs**: Registro de qual versão do termo o usuário aceitou e quando.
- **Services/Privacy/PrivacyService.cs**: Métodos para anonimização de dados (GDPR/LGPD).

## Lógica de Negócio
- Implementação de "Soft Delete" para fins de auditoria, mas com remoção de PII (Personally Identifiable Information).
- Endpoint para o usuário solicitar o download de todos os seus dados em JSON.

## Testes e Validação
- **Anonimização Test:** Validar que após exclusão, campos como Nome e Telefone foram substituídos por hashes irreconhecíveis.

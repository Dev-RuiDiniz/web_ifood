# Sprint 45: Hardening de Segurança Final e Teste de Carga

## Atividades da Sprint
- Última camada de segurança e validação de escala antes do lançamento.

## Ferramentas
- **k6** ou **JMeter**: Para testes de carga e estresse.
- **OWASP ZAP**: Scan dinâmico de segurança (DAST).

## Lógica de Negócio
- Configuração de Headers de Segurança (HSTS, XSS Protection, CSP).
- Encriptação de dados sensíveis em repouso (Data-at-rest encryption).

## Testes e Validação
- **Stress Test:** Simular 5.000 usuários simultâneos fazendo busca e adição ao carrinho. Validar se a API escalou horizontalmente e o banco suportou a carga.
- **Pentest Básico:** Identificar e corrigir vulnerabilidades comuns (SQL Injection, IDOR) nos principais endpoints.

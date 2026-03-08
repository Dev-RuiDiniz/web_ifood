# Sprint 15: Auditoria de Logins e Acessos

## Atividades da Sprint
- Registro de histórico de dispositivos e IPs que acessaram a conta.
- Alerta por e-mail para logins em dispositivos desconhecidos.

## Estrutura e Classes
- **Domains/Entities/AuditLog.cs**: Registro de ação, IP, UserAgent e Timestamp.
- **Data/Interceptors/AuditInterceptor.cs**: Interceptor do EF Core para salvar logs de alteração automaticamente.

## Testes e Validação
- **Tracking Test:** Abrir a API no Chrome e depois no Postman e verificar se dois UserAgents diferentes foram registrados.
- **Interceptor Test:** Alterar um User e checar se uma linha foi criada na tabela `AuditLogs` descrevendo o que mudou.

# Sprint 26: SignalR Setup (WebSockets)

## Atividades da Sprint
- Configuração da infraestrutura para comunicação bidirecional em tempo real.
- Notificação push-like interna.

## Ferramentas e Pacotes
- `Microsoft.AspNetCore.SignalR`
- **SignalR Redis Backplane**: Para escala horizontal (múltiplas instâncias da API).

## Estrutura e Classes
- **Api/Hubs/OrderHub.cs**: Classe que gerencia conexões e grupos (ex: Grupo Restaurante X, Grupo Cliente Y).
- **Services/RealTime/INotificationService.cs**.

## Testes e Validação
- **Connection Test:** Validar se o cliente consegue estabelecer conexão via WebSockets/Long Polling.
- **Broadcasting Test:** Enviar mensagem para um ID de usuário específico e garantir que apenas ele receba.

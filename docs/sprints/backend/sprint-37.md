# Sprint 37: Campanhas de Marketing e Notificações Segmentadas

## Atividades da Sprint
- Envio massivo de notificações Push baseadas no comportamento do usuário.

## Ferramentas e Pacotes
- **Hangfire**: Para agendamento de tarefas em background (Job Scheduling).
- **FCM Batch Sending**.

## Lógica de Negócio
- Exemplo: "Notificar todos os usuários que não pedem há 7 dias com um cupom de 20% OFF".

## Testes e Validação
- **Scheduler Test:** Verificar se o Job do Hangfire disparou no horário correto.
- **Segmentation Test:** Validar se a query de filtro de usuários retornou os perfis corretos para a campanha.

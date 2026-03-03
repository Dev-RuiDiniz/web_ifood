# Sprint 04 - Pedidos em Tempo Real (Loja)

## Objetivo

Disponibilizar operacao de pedidos ao vivo para loja com atualizacao de status em tempo real para cliente.

## Escopo funcional

- Painel da loja com fila de pedidos em tempo real.
- Acoes de status: aceitar, preparar, pronto, saiu para entrega, finalizado.
- Canal em tempo real para cliente acompanhar status.
- Notificacoes push de mudanca de status.

## Criterios de aceite

- Loja recebe novos pedidos em ate poucos segundos apos pagamento.
- Mudanca de status na loja aparece no cliente em tempo real.
- Fluxo de status nao permite transicoes invalidas.
- Cliente recebe push nos eventos criticos.

## Entregaveis

- Modulo `Orders` com eventos em tempo real.
- Modulo `Notifications` com push basico.
- `web/loja` operacional para processamento de pedidos.

## Tarefas por trilha

### Back-end

- WebSockets/Socket.IO para canais loja e cliente.
- Maquina de estado de pedido com auditoria.
- Publicacao de eventos em fila para push.

### Front-end Web

- Dashboard de pedidos ao vivo com filtros por status.
- Acoes de status com confirmacao e feedback visual.

### Mobile

- Tela de rastreio de pedido em tempo real.
- Atualizacao de timeline de status no historico.

### QA

- Testes e2e de fluxo fim a fim (pagamento -> pedido -> finalizacao).
- Testes de resiliencia de reconexao websocket.

### DevOps

- Redis adapter para escalar WebSockets.
- Metricas de latencia de eventos em tempo real.

## Riscos e dependencias

- Queda de conexao websocket exige fallback de polling.

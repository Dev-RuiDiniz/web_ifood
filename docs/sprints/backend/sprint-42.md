# Sprint 42: Observabilidade e Monitoramento (OpenTelemetry)

## Atividades da Sprint
- Implementação de rastreamento de requisições ponta a ponta (Tracing) e métricas de sistema.

## Ferramentas e Pacotes
- `OpenTelemetry.Extensions.Hosting`
- **Jaeger** ou **Zipkin**: Para visualização de traces.
- **Prometheus & Grafana**: Para dashboards de CPU, Memória e Request/sec.

## Estrutura e Classes
- **Middlewares/MetricsMiddleware.cs**: Coleta de métricas por endpoint.
- **Logging/Enrichers/TraceIdEnricher.cs**: Adiciona o ID do trace nos logs do Serilog.

## Testes e Validação
- **Trace Test:** Seguir uma requisição desde a Controller -> Handler -> DB -> Redis no painel do Jaeger.
- **Metric Test:** Validar se o contador de erros 500 está sendo incrementado corretamente no Prometheus.

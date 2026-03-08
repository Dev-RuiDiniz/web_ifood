# Sprint 38: BI - Dashboard do Administrador

## Atividades da Sprint
- Agregação de dados para visão macro da plataforma (GMV, Churn, Conversão).

## Estrutura e Classes
- **Services/Analytics/IAdminAnalyticsService.cs**.
- **Dtos/Analytics/AdminDashboardDto.cs**.

## Queries Otimizadas
- Uso de `Dapper` para queries SQL puras e performáticas em relatórios que envolvem milhões de linhas.

## Testes e Validação
- **Data Integrity Test:** Comparar o relatório de vendas do dia com a soma real das transações no banco.
- **Performance Check:** Garantir que a query do dashboard não trave o banco de produção (uso de `NOLOCK` ou réplicas de leitura).

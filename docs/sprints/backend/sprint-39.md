# Sprint 39: BI - Relatórios da Loja (Vendas e Produtos)

## Atividades da Sprint
- Ferramentas para o parceiro entender seus horários de maior venda e pratos mais populares.

## Estrutura e Classes
- **Dtos/Analytics/StoreSalesDto.cs**: Top 5 Produtos, Vendas por Hora, Ticket Médio.

## Lógica de Negócio
- Exportação de dados para Excel/CSV para conciliação contábil do restaurante.

## Ferramentas
- `ClosedXML`: Para geração de arquivos Excel no backend.

## Testes e Validação
- **File Integrity Test:** Gerar um Excel e validar se as colunas e somas batem com os filtros selecionados.

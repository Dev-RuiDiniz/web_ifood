# Sprint 09: Global Exception Handling e Logging

## Atividades da Sprint
- Centralização do tratamento de erros para respostas amigáveis.
- Implementação de rastreabilidade de requisições.

## Ferramentas e Pacotes
- `Serilog`
- `Serilog.Sinks.File` / `Serilog.Sinks.Console`
- `Serilog.AspNetCore`

## Estrutura e Classes
- **Api/Middleware/ExceptionMiddleware.cs**: Captura de exceções não tratadas e retorno de JSON estruturado.
- **Dtos/Common/ErrorResponseDto.cs**: Modelo padrão de erro (Timestamp, Message, TraceId).

## Configurações
- Registro de logs em arquivos `.log` diários para auditoria.

## Testes e Validação
- **Error Simulation:** Forçar uma `DivideByZeroException` em uma rota de teste e verificar se o middleware retorna um erro 500 formatado.
- **Logging Test:** Verificar se a mensagem de erro foi gravada no arquivo físico de logs.

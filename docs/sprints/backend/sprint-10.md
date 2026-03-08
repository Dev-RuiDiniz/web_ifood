# Sprint 10: Documentação com Swagger / OpenAPI

## Atividades da Sprint
- Documentação técnica interativa para facilitar o consumo da API pelo Front-end e Apps.
- Configuração de exemplos e definições de segurança no Swagger.

## Ferramentas e Pacotes
- `Swashbuckle.AspNetCore`
- `Swashbuckle.AspNetCore.Annotations`

## Configurações
- Inclusão do botão "Authorize" no Swagger para testar rotas protegidas por JWT.
- XML Comments: Configuração para que comentários triple-slash `///` apareçam como descrições no Swagger.

## Testes e Validação
- **UI Check:** Acessar `/swagger/index.html` e verificar se todos os endpoints mapeados estão visíveis.
- **Integration Check:** Criar um usuário via interface do Swagger e validar a persistência.

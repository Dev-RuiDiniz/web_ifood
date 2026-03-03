# Sprint 01 - Auth, Contas e Catalogo Base

## Objetivo

Entregar autenticacao inicial e catalogo navegavel no app cliente e painel loja.

## Escopo funcional

- Cadastro/login por email ou telefone com OTP.
- Estrutura de perfis (cliente, loja, admin).
- Cadastro e consulta de lojas, categorias e produtos.
- Home do cliente com destaque e listagem de lojas.

## Criterios de aceite

- Usuario consegue criar conta e autenticar com token valido.
- Cliente lista lojas e visualiza produtos por loja.
- Loja consegue cadastrar e editar item de cardapio.
- Validacoes de payload aplicadas com schemas compartilhados.

## Entregaveis

- Modulo `Auth & Accounts` funcional (MVP).
- Modulo `Catalog` funcional para leitura e manutencao basica.
- Telas base de login e catalogo no mobile e web.

## Tarefas por trilha

### Back-end

- Implementar JWT + refresh token + OTP.
- Criar endpoints de loja/produto/categoria.
- Incluir RBAC inicial por role.

### Front-end Web

- `web/loja`: telas de login e gestao de cardapio.
- `web/admin`: listagem de lojas e status de aprovacao inicial.

### Mobile

- Telas de onboarding, login, home e vitrine da loja.
- Integrar busca simples por nome de loja/produto.

### QA

- Testes de integracao de auth e catalogo.
- Casos de erro: token expirado, payload invalido, permissao insuficiente.

### DevOps

- Segredos de auth e banco em ambientes dev/hml.
- Monitoramento basico de erro de login.

## Riscos e dependencias

- Integracao OTP depende de provedor externo.
- Definicao de regras de aprovacao de loja pode mudar com produto.

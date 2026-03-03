# Sprint 02 - Carrinho e Checkout

## Objetivo

Permitir montagem de pedido completo no app cliente, com regras de carrinho e fechamento de checkout.

## Escopo funcional

- Carrinho com itens, adicionais, observacoes e regras de minimo.
- Calculo de taxa de entrega e subtotal/total.
- Selecao de endereco salvo e previsao de entrega.
- Aplicacao de cupom simples.

## Criterios de aceite

- Cliente adiciona/remove itens com recalc correto.
- Checkout bloqueia pedido invalido (minimo, endereco, item indisponivel).
- Cupom valido aplica desconto e cupom invalido retorna erro claro.
- Ordem de compra e payload de pagamento sao persistidos.

## Entregaveis

- Modulo `Cart & Checkout` operacional.
- Modulo `Promotions` com cupom basico.
- Fluxo cliente do carrinho ao checkout pronto para pagamento.

## Tarefas por trilha

### Back-end

- Criar `carts`, `cart_items` e regras de precificacao.
- Criar endpoint de checkout com snapshot do pedido.
- Implementar validacao de cupom e taxa de entrega.

### Front-end Web

- `web/loja`: marcacao de item indisponivel e horario de funcionamento.
- `web/admin`: parametrizacao basica de taxa/comissao.

### Mobile

- Telas de carrinho, endereco, cupom e revisao de pedido.
- Estados de loading/erro no checkout.

### QA

- Testes de regra de negocio de carrinho.
- Testes e2e do fluxo completo sem pagamento externo.

### DevOps

- Habilitar logs estruturados para checkout.
- Dashboard de erros de conversao de checkout.

## Riscos e dependencias

- Regras de frete por area podem exigir refinamento antecipado.

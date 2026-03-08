# Sprint 31: Onboarding de Entregadores

## Atividades da Sprint
- Fluxo de cadastro para prestadores de serviço de entrega.
- Validação de veículo e CNH (Carteira Nacional de Habilitação).

## Estrutura e Classes
- **Domains/Entities/Courier.cs**: VehicleType (Moto, Carro, Bike), LicenseNumber, Status (AwaitingApproval, Active, Banned).
- **Controllers/CourierAdminController.cs**.

## Lógica de Negócio
- Regra de idade mínima (18+).
- Upload de documentos de identificação (reutilizando `FileService`).

## Testes e Validação
- **Validation Test:** Validar rejeição de cadastro se a CNH estiver vencida.
- **Status Test:** Garantir que o entregador não receba pedidos até que seu status seja alterado para `Active` por um admin.

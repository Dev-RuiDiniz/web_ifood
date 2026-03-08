# Sprint 44: Preparação para Deployment (CI/CD e Docker Optimization)

## Atividades da Sprint
- Automação do ciclo de vida de release e otimização de imagens de container.

## Ferramentas
- **GitHub Actions** ou **Azure DevOps Pipelines**.
- **SonarQube**: Análise estática de código (Code Quality).

## Configurações
- Docker Multistage Build: Imagem de runtime baseada em `.NET Alpine` para menor superfície de ataque e tamanho reduzido (<200MB).
- Pipeline de Build -> Teste -> Lint -> Push para Registry.

## Testes e Validação
- **Pipeline Check:** Validar que um commit com falha nos testes unitários trava o deploy automático.
- **Vulnerability Scan:** Executar scan na imagem Docker (Trivy ou Snyk) em busca de pacotes vulneráveis.

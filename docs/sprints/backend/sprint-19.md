# Sprint 19: Upload de Imagens com CDN

## Atividades da Sprint
- Integração com serviços de armazenamento de arquivos para fotos de pratos e logos.
- Redimensionamento automático de imagens para performance.

## Ferramentas e Pacotes
- `AWS SDK` (para S3) ou Azure Storage SDK.
- `SixLabors.ImageSharp`: Redimensionamento em memória.

## Estrutura e Classes
- **Services/Storage/IFileService.cs**.
- **Services/Storage/S3FileService.cs**: Implementação real.
- **Services/Storage/LocalFileService.cs**: Implementação para desenvolvimento local.

## Testes e Validação
- **Format Test:** Rejeitar arquivos que não sejam `.jpg`, `.png` ou `.webp`.
- **Performance Test:** Validar que uma imagem de 5MB foi processada e salva com menos de 200KB.

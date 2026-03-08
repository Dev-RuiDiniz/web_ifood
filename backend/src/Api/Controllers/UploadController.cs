using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Storage;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IFileService _fileService;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public UploadController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromQuery] string folder = "general")
        {
            if (file == null || file.Length == 0) return BadRequest("Nenhum arquivo enviado.");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
            {
                return BadRequest("Formato de imagem não suportado. Use JPG, PNG ou WEBP.");
            }

            if (file.Length > 5 * 1024 * 1024) // 5MB limit
            {
                return BadRequest("O arquivo é muito grande. O limite é 5MB.");
            }

            var url = await _fileService.UploadFileAsync(file, folder);
            return Ok(new { url });
        }
    }
}

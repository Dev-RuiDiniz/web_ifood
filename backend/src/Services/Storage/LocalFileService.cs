using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.Storage
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        Task DeleteFileAsync(string fileUrl);
    }

    public class LocalFileService : IFileService
    {
        private readonly string _basePath;
        private readonly string _baseUrl;

        public LocalFileService(string basePath, string baseUrl)
        {
            _basePath = basePath;
            _baseUrl = baseUrl;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0) return string.Empty;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var directoryPath = Path.Combine(_basePath, folder);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"{_baseUrl}/{folder}/{fileName}";
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            var fileName = Path.GetFileName(fileUrl);
            var folder = Path.GetDirectoryName(fileUrl.Replace(_baseUrl, ""))?.TrimStart(Path.DirectorySeparatorChar);
            
            if (string.IsNullOrEmpty(folder)) return Task.CompletedTask;

            var filePath = Path.Combine(_basePath, folder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
    }
}

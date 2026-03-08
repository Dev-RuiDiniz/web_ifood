using Microsoft.AspNetCore.Mvc;
using Domains.Entities;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public CategoryController(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.FindAsync(c => c.IsActive);
            return Ok(categories);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogProduct.Api.DTOs;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Pagination;
using CatalogProduct.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CatalogProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<CategoriesController> _logger;
        
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, ILogger<CategoriesController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get([FromQuery] CategoryParameters parameters)
        {
            var categoriesPaged = _unitOfWork.CategoryRepository
                .GetCategories(parameters);

            var metadata = new 
            {
                categoriesPaged.TotalCount,
                categoriesPaged.PageSize,
                categoriesPaged.CurrentPage,
                categoriesPaged.TotalPages,
                categoriesPaged.HasNext,
                categoriesPaged.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return _mapper.Map<CategoryDto[]>(categoriesPaged);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoryProducts()
        {
            _logger.LogInformation("========== GET: api/categories/products ==========");

            var categories = await _unitOfWork.CategoryRepository
                .GetProducts();

            return _mapper.Map<CategoryDto[]>(categories);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            _logger.LogInformation($"========== GET: api/categories/{id} ==========");

            var category = await _unitOfWork.CategoryRepository
                .GetById(p => p.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoryDto>(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto model)
        {
            var category = _mapper.Map<Category>(model);

            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.Commit();
            
            return CreatedAtRoute("GetCategory", new { id = category.CategoryId }, _mapper.Map<CategoryDto>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto model)
        {
            if (id != model.CategoryId)
            {
                return BadRequest("Invalid category.");
            }

            var category = _mapper.Map<Category>(model);

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Commit();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.Commit();

            return new NoContentResult();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CatalogProduct.Api.DTOs;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            _logger.LogInformation("========== GET: api/categories ==========");

            var categories = _unitOfWork.CategoryRepository
                .Get()
                .ToList();

            return _mapper.Map<CategoryDto[]>(categories);
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategoryProducts()
        {
            _logger.LogInformation("========== GET: api/categories/products ==========");

            var categories = _unitOfWork.CategoryRepository
                .GetProducts()
                .ToArray();

            return _mapper.Map<CategoryDto[]>(categories);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<CategoryDto> Get(int id)
        {
            _logger.LogInformation($"========== GET: api/categories/{id} ==========");

            var category = _unitOfWork.CategoryRepository
                .GetById(p => p.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoryDto>(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto model)
        {
            var category = _mapper.Map<Category>(model);

            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Commit();
            
            return CreatedAtRoute("GetCategory", new { id = category.CategoryId }, _mapper.Map<CategoryDto>(category));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryDto model)
        {
            if (id != model.CategoryId)
            {
                return BadRequest("Invalid category.");
            }

            var category = _mapper.Map<Category>(model);

            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();

            return new NoContentResult();
        }
    }
}
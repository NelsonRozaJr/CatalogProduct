using System.Collections.Generic;
using System.Linq;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CatalogProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IUnitOfWork unitOfWork, ILogger<CategoriesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            _logger.LogInformation("========== GET: api/categories ==========");

            return _unitOfWork.CategoryRepository
                .Get()
                .ToList();
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoryProducts()
        {
            _logger.LogInformation("========== GET: api/categories/products ==========");

            return _unitOfWork.CategoryRepository
                .GetProducts()
                .ToArray();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            _logger.LogInformation($"========== GET: api/categories/{id} ==========");

            var category = _unitOfWork.CategoryRepository
                .GetById(p => p.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Commit();
            
            return CreatedAtRoute("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Invalid category.");
            }

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
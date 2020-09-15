using System.Collections.Generic;
using System.Linq;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CatalogProductContext _catalogProductContext;

        public CategoriesController(CatalogProductContext catalogProductContext)
        {
            _catalogProductContext = catalogProductContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return _catalogProductContext.Categories
                .AsNoTracking()
                .ToList();
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoryProducts()
        {
            return _catalogProductContext.Categories
                .Include(c => c.Products)
                .ToList();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            var category = _catalogProductContext.Categories
                .AsNoTracking()
                .FirstOrDefault(p => p.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            _catalogProductContext.Categories.Add(category);

            var inserted = _catalogProductContext.SaveChanges();
            if (inserted == 1)
            {
                return CreatedAtRoute("GetCategory", new { id = category.CategoryId }, category);
            }

            return BadRequest("An error occurred while creating category.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Invalid category.");
            }

            _catalogProductContext.Categories.Update(category);

            var updated = _catalogProductContext.SaveChanges();
            if (updated == 1)
            {
                return new NoContentResult();
            }

            return BadRequest("An error occurred while updating category.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _catalogProductContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _catalogProductContext.Categories.Remove(category);

            var deleted = _catalogProductContext.SaveChanges();
            if (deleted == 1)
            {
                return new NoContentResult();
            }

            return BadRequest("An error occurred while removing category.");
        }
    }
}
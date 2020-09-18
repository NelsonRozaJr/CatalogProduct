using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Filters;
using CatalogProduct.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly CatalogProductContext _catalogProductContext;

        public ProductsController(CatalogProductContext catalogProductContext)
        {
            _catalogProductContext = catalogProductContext;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
        {
            var products = await _catalogProductContext.Products
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            var product = await _catalogProductContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("{name:alpha:maxlength(80)}", Name = "GetProductByName")]
        public async Task<ActionResult<Product>> GetAsync(string name)
        {
            var product = await _catalogProductContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _catalogProductContext.Products.Add(product);

            var inserted = _catalogProductContext.SaveChanges();
            if (inserted == 1)
            {
                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
            }

            return BadRequest("An error occurred while creating product.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Invalid product.");
            }

            _catalogProductContext.Products.Update(product);

            var updated = _catalogProductContext.SaveChanges();
            if (updated == 1)
            {
                return new NoContentResult();
            }

            return BadRequest("An error occurred while updating product.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _catalogProductContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _catalogProductContext.Products.Remove(product);

            var deleted = _catalogProductContext.SaveChanges();
            if (deleted == 1)
            {
                return new NoContentResult();
            }

            return BadRequest("An error occurred while removing product.");
        }
    }
}
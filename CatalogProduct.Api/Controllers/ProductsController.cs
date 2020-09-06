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
    public class ProductsController : ControllerBase
    {
        private readonly CatalogProductContext _catalogProductContext;

        public ProductsController(CatalogProductContext catalogProductContext)
        {
            _catalogProductContext = catalogProductContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _catalogProductContext.Products
                .AsNoTracking()
                .ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _catalogProductContext.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.ProductId == id);

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
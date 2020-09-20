using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Filters;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _unitOfWork.ProductRepository
                .Get()
                .ToArray();

            return products;
        }

        [HttpGet("{id:int:min(1)}", Name = "GetById")]
        public ActionResult<Product> Get(int id)
        {
            var product = _unitOfWork.ProductRepository
                .GetById(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("{name:alpha:maxlength(80)}", Name = "GetByName")]
        public ActionResult<Product> Get(string name)
        {
            var product = _unitOfWork.ProductRepository
                .GetByName(p => p.Name.ToLower() == name.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("highest-price")]
        public ActionResult<IEnumerable<Product>> GetByPrice()
        {
            var products = _unitOfWork.ProductRepository
                .GetByPrice()
                .ToArray();

            return products;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Commit();

            return CreatedAtRoute("GetById", new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Invalid product.");
            }

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();

            return new NoContentResult();
        }
    }
}
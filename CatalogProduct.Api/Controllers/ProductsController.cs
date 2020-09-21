using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.DTOs;
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

        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var products = _unitOfWork.ProductRepository
                .Get()
                .ToArray();

            return _mapper.Map<ProductDto[]>(products);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetById")]
        public ActionResult<ProductDto> Get(int id)
        {
            var product = _unitOfWork.ProductRepository
                .GetById(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductDto>(product);
        }

        [HttpGet("{name:alpha:maxlength(80)}", Name = "GetByName")]
        public ActionResult<ProductDto> Get(string name)
        {
            var product = _unitOfWork.ProductRepository
                .GetByName(p => p.Name.ToLower() == name.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return  _mapper.Map<ProductDto>(product);
        }

        [HttpGet("highest-price")]
        public ActionResult<IEnumerable<ProductDto>> GetByPrice()
        {
            var products = _unitOfWork.ProductRepository
                .GetByPrice()
                .ToArray();

            return _mapper.Map<ProductDto[]>(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDto model)
        {
            var product = _mapper.Map<Product>(model);

            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Commit();

            return CreatedAtRoute("GetById", new { id = product.ProductId }, _mapper.Map<ProductDto>(product));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDto model)
        {
            if (id != model.ProductId)
            {
                return BadRequest("Invalid product.");
            }

            var product  = _mapper.Map<Product>(model);
            
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
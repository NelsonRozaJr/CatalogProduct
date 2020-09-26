using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.DTOs;
using CatalogProduct.Api.Filters;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Pagination;
using CatalogProduct.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        public ActionResult<IEnumerable<ProductDto>> Get([FromQuery] ProductParameters parameters)
        {
            var productsPaged = _unitOfWork.ProductRepository
                .GetProducts(parameters);

            var metadata = new 
            {
                productsPaged.TotalCount,
                productsPaged.PageSize,
                productsPaged.CurrentPage,
                productsPaged.TotalPages,
                productsPaged.HasNext,
                productsPaged.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return _mapper.Map<ProductDto[]>(productsPaged);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetById")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .GetById(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductDto>(product);
        }

        [HttpGet("{name:alpha:maxlength(80)}", Name = "GetByName")]
        public async Task<ActionResult<ProductDto>> Get(string name)
        {
            var product = await _unitOfWork.ProductRepository
                .GetByName(p => p.Name.ToLower() == name.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return  _mapper.Map<ProductDto>(product);
        }

        [HttpGet("highest-price")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByPrice()
        {
            var products = await _unitOfWork.ProductRepository
                .GetByPrice();

            return _mapper.Map<ProductDto[]>(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto model)
        {
            var product = _mapper.Map<Product>(model);

            _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.Commit();

            return CreatedAtRoute("GetById", new { id = product.ProductId }, _mapper.Map<ProductDto>(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto model)
        {
            if (id != model.ProductId)
            {
                return BadRequest("Invalid product.");
            }

            var product  = _mapper.Map<Product>(model);
            
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Commit();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.Commit();

            return new NoContentResult();
        }
    }
}
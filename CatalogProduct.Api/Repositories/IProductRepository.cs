using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Pagination;

namespace CatalogProduct.Api.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByPrice();

        PagedList<Product> GetProducts(ProductParameters parameters);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogProduct.Api.Models;

namespace CatalogProduct.Api.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByPrice();
    }
}
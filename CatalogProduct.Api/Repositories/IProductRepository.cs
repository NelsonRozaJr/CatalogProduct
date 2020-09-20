using System.Collections.Generic;
using CatalogProduct.Api.Models;

namespace CatalogProduct.Api.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByPrice();
    }
}
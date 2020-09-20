using System.Collections.Generic;
using CatalogProduct.Api.Models;

namespace CatalogProduct.Api.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
         IEnumerable<Category> GetProducts();
    }
}
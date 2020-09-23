using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogProduct.Api.Models;

namespace CatalogProduct.Api.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetProducts();
    }
}
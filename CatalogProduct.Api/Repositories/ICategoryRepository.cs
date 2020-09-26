using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Pagination;

namespace CatalogProduct.Api.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetProducts();

        PagedList<Category> GetCategories(CategoryParameters parameters);
    }
}
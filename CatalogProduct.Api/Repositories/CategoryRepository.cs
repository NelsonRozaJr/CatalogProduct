using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Models;
using CatalogProduct.Api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogProductContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetProducts()
        {
            return await Get().Include(c => c.Products)
                .ToArrayAsync();
        }

        public PagedList<Category> GetCategories(CategoryParameters parameters)
        {
            var source = Get().OrderBy(c => c.Name);
            return PagedList<Category>.ToPagedList(source, parameters.PageNumber, parameters.PageSize);
        }
    }
}
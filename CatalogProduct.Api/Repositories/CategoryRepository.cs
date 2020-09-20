using System.Collections.Generic;
using System.Linq;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogProductContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetProducts()
        {
            return Get().Include(c => c.Products)
                .ToArray();
        }
    }
}
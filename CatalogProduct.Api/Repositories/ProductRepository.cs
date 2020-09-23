using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CatalogProductContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetByPrice()
        {
            return await Get().OrderByDescending(p => p.UnitPrice)
                .ToArrayAsync();
        }
    }
}
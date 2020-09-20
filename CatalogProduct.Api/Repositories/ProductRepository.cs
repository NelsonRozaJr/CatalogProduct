using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CatalogProduct.Api.Context;
using CatalogProduct.Api.Models;

namespace CatalogProduct.Api.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CatalogProductContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetByPrice()
        {
            return Get().OrderByDescending(p => p.UnitPrice)
                .ToArray();
        }
    }
}
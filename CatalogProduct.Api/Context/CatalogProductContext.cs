using CatalogProduct.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Context
{
    public class CatalogProductContext : DbContext
    {
        public CatalogProductContext(DbContextOptions<CatalogProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
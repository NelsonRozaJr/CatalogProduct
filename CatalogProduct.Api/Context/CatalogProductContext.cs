using CatalogProduct.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Context
{
    public class CatalogProductContext : IdentityDbContext
    {
        public CatalogProductContext(DbContextOptions<CatalogProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
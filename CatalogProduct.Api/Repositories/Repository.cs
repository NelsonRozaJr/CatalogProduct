using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CatalogProduct.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogProduct.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CatalogProductContext _context;

        public Repository(CatalogProductContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByName(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
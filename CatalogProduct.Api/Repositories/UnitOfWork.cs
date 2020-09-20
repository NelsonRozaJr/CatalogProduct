using CatalogProduct.Api.Context;

namespace CatalogProduct.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogProductContext _context;

        public UnitOfWork(CatalogProductContext context)
        {
            _context = context;
        }

        private ProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository = _productRepository ?? new ProductRepository(_context);
            }
        }

        private CategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
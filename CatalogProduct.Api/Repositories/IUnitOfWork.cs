namespace CatalogProduct.Api.Repositories
{
    public interface IUnitOfWork
    {
         IProductRepository ProductRepository { get; }

         ICategoryRepository CategoryRepository { get; }

         void Commit();
    }
}
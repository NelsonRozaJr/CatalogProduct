using System.Threading.Tasks;

namespace CatalogProduct.Api.Repositories
{
    public interface IUnitOfWork
    {
         IProductRepository ProductRepository { get; }

         ICategoryRepository CategoryRepository { get; }

         Task Commit();
    }
}
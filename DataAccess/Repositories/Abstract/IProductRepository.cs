using Entities.Concrete;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(int categoryId);
    }
}

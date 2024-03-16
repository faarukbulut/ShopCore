using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(int categoryId);
    }
}

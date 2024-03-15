using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : GenericRepository<Product,Context>, IProductRepository
    {
    }
}

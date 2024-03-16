using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : GenericRepository<Product, Context>, IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context) : base(context)
        {
            _context = context;
        }

        public Product GetProductDetails(int id)
        {
            return _context.Products.Where(x => x.ProductID == id).Include(x => x.Category).FirstOrDefault();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(x => x.CategoryID == categoryId).ToList();
        }
    }
}

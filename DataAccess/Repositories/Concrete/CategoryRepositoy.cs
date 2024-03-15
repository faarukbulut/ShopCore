using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class CategoryRepositoy : GenericRepository<Category, Context>, ICategoryRepository
    {
        public CategoryRepositoy(Context context) : base(context)
        {
        }
    }
}

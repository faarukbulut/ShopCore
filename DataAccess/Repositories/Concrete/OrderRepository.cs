using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class OrderRepository : GenericRepository<Order, Context>, IOrderRepository
    {
        public OrderRepository(Context context) : base(context)
        {
        }
    }
}

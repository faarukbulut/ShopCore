using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class OrderLineRepository : GenericRepository<OrderLine, Context>, IOrderLineRepository
    {
    }
}

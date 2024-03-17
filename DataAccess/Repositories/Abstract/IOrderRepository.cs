using Entities.Concrete;

namespace DataAccess.Repositories.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        List<Order> GetOrders(string? userId);
    }
}

using Entities.Concrete;

namespace Business.Abstract
{
    public interface IOrderService
    {
        void Create(Order order);
        List<Order> GetOrders(string? userId);
    }
}

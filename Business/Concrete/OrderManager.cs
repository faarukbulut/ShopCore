using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Create(Order order)
        {
            _orderRepository.Create(order);
        }

        public List<Order> GetOrders(string? userId)
        {
            return _orderRepository.GetOrders(userId);
        }
    }
}

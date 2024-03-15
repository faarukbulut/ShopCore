using Business.Abstract;
using DataAccess.Repositories.Concrete;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderManager(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Create(Order entity)
        {
            _orderRepository.Create(entity);
        }

        public void Delete(Order entity)
        {
            _orderRepository.Delete(entity);
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll().ToList();
        }

        public Order GetByID(int id)
        {
            return _orderRepository.GetByID(id);
        }

        public void Update(Order entity)
        {
            _orderRepository.Update(entity);
        }
    }
}

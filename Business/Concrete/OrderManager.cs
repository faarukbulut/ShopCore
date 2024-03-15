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

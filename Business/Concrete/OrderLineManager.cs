using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OrderLineManager : IOrderLineService
    {
        private readonly IOrderLineRepository _orderLineRepository;

        public OrderLineManager(IOrderLineRepository orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        public void Create(OrderLine entity)
        {
            _orderLineRepository.Create(entity);
        }

        public void Delete(OrderLine entity)
        {
            _orderLineRepository.Delete(entity);
        }

        public List<OrderLine> GetAll()
        {
            return _orderLineRepository.GetAll().ToList();
        }

        public OrderLine GetByID(int id)
        {
            return _orderLineRepository.GetByID(id);
        }

        public void Update(OrderLine entity)
        {
            _orderLineRepository.Update(entity);
        }
    }
}

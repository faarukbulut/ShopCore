using Business.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OrderLineManager : IOrderLineService
    {
        private readonly OrderLineManager _orderLineManager;

        public OrderLineManager(OrderLineManager orderLineManager)
        {
            _orderLineManager = orderLineManager;
        }

        public void Create(OrderLine entity)
        {
            _orderLineManager.Create(entity);
        }

        public void Delete(OrderLine entity)
        {
            _orderLineManager.Delete(entity);
        }

        public List<OrderLine> GetAll()
        {
            return _orderLineManager.GetAll().ToList();
        }

        public OrderLine GetByID(int id)
        {
            return _orderLineManager.GetByID(id);
        }

        public void Update(OrderLine entity)
        {
            _orderLineManager.Update(entity);
        }
    }
}

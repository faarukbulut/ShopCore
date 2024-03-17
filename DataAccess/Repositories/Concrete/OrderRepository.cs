using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class OrderRepository : GenericRepository<Order, Context>, IOrderRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context) : base(context)
        {
            _context = context;
        }

        public List<Order> GetOrders(string? userId)
        {
            var orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(x => x.UserId == userId);
            }

            return orders.ToList();
        }
    }
}

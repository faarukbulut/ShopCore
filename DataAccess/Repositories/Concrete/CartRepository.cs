using DataAccess.Concrete;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class CartRepository : GenericRepository<Cart, Context>, ICartRepository
    {
        private readonly Context _context;

        public CartRepository(Context context) : base(context)
        {
            _context = context;
        }

        public override void Update(Cart entity)
        {
            _context.Carts.Update(entity);
            _context.SaveChanges();
        }

        public Cart GetByUserID(string userId)
        {
            return _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);
        }

        public void DeleteFromCart(int cartID, int productID)
        {
            var value = _context.CartItems.Where(x => x.CartID == cartID && x.ProductID == productID).FirstOrDefault();
            _context.CartItems.Remove(value);
            _context.SaveChanges();
        }

        public void ClearCart(int cartID)
        {
            var itemsToRemove = _context.CartItems.Where(x => x.CartID == cartID).ToList();
            _context.CartItems.RemoveRange(itemsToRemove);
            _context.SaveChanges();
        }
    }
}

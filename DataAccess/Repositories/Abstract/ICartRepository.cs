using Entities.Concrete;

namespace DataAccess.Repositories.Abstract
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Cart GetByUserID(string userId);
        void DeleteFromCart(int cartID, int productID);
        void ClearCart(int cartID);
    }
}

using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICartService
    {
        void Initialize(string userID);
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId, int productId, int quantity);
        void DeleteFromCart(string userId, int productID);
        void ClearCart(int cartID);
    }
}

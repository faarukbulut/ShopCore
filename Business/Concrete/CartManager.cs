using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void Initialize(string userID)
        {
            _cartRepository.Create(new Cart()
            {
                UserId = userID,
            });
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetByUserID(userId);
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if(cart != null)
            {
                var index = cart.CartItems.FindIndex(x => x.ProductID == productId);

                if(index < 0)
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductID = productId,
                        Quantity = quantity,
                        CartID = cart.CartID
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                _cartRepository.Update(cart);
            }
        }

        public void DeleteFromCart(string userId, int productID)
        {
            var cart = GetCartByUserId(userId);

            if(cart != null)
            {
                _cartRepository.DeleteFromCart(cart.CartID, productID);
            }
        }

        public void ClearCart(int cartID)
        {
            _cartRepository.ClearCart(cartID);
        }
    }
}

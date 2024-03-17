namespace Entities.Concrete
{
    public class Cart
    {
        public int CartID { get; set; }
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}

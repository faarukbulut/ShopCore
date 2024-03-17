namespace Entities.Concrete
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string? OrderNote { get; set; }
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string? SenderName { get; set; }
        public string? SendDate { get; set; }
    }

    public enum EnumOrderState
    {
        Waiting = 0,
        Unpaid = 1,
        Completed = 2
    }

    public enum EnumPaymentTypes
    {
        CreditCart = 0,
        Eft = 1
    }

}

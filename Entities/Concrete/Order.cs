namespace Entities.Concrete
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string OrderNote { get; set; }
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }
        public string? PaymentID { get; set; }
        public string? PaymentToken { get; set; }
        public string? ConversationID { get; set; }
        public List<OrderItem> OrderItems { get; set; }
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

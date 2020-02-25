using Shop.Utils.Messaging;

namespace Shop.Order.Contract.Orders.Messages.CreateOrder
{
    public class OrderCreatedMessage : Message
    {
        public int OrderId { get; set; }
        public string UserEmail { get; set; }
    }
}

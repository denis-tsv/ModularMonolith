using Shop.Utils.ServiceBus;

namespace Shop.Order.Contract.Orders.Messages
{
    public class OrderCreatedMessage : Message
    {
        public int OrderId { get; set; }
        public string UserEmail { get; set; }
    }
}

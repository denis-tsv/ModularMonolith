using Shop.Utils.ServiceBus;

namespace Shop.Order.Contract.Orders.Messages
{
    public class CancelOrderCreationMessage : Message
    {
        public int OrderId { get; set; }
    }
}

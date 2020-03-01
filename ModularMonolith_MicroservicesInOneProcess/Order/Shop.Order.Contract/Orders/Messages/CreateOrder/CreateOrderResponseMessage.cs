using Shop.Framework.Interfaces.Messaging;

namespace Shop.Order.Contract.Orders.Messages.CreateOrder
{
    public class CreateOrderResponseMessage : Message
    {
        public int OrderId { get; set; }
    }
}

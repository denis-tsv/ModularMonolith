using Shop.Framework.Interfaces.Messaging;

namespace Shop.Order.Contract.Orders.Messages.GetOrder
{
    public class GetOrderMessage : Message
    {
        public int Id { get; set; }
    }
}

using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Dto;

namespace Shop.Order.Contract.Orders.Messages.GetOrder
{
    public class OrderDetailsMessage : Message
    {
        public OrderDto Order { get; set; }
    }
}

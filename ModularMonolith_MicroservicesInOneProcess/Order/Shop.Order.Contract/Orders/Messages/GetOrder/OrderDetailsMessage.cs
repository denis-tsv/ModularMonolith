using Shop.Order.Contract.Orders.Dto;
using Shop.Utils.Messaging;

namespace Shop.Order.Contract.Orders.Messages.GetOrder
{
    public class OrderDetailsMessage : Message
    {
        public OrderDto Order { get; set; }
    }
}

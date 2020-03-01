using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Dto;

namespace Shop.Order.Contract.Orders.Messages.CreateOrder
{
    public class CreateOrderRequestMessage : Message
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}

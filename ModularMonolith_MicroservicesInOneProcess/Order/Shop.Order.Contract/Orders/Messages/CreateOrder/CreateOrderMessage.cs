using Shop.Order.Contract.Orders.Dto;
using Shop.Utils.Messaging;

namespace Shop.Order.Contract.Orders.Messages.CreateOrder
{
    public class CreateOrderMessage : Message
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}

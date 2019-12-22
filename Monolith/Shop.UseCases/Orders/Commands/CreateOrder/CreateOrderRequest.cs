using MediatR;
using Shop.UseCases.Orders.Dto;

namespace Shop.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderRequest : IRequest
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}

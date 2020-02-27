using MediatR;
using Shop.Framework.Interfaces.Transactions;
using Shop.Order.UseCases.Orders.Dto;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequest : IRequest, ITransactionalRequest
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}

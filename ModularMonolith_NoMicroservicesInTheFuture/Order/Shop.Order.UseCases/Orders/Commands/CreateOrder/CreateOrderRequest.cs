using MediatR;
using Shop.Framework.UseCases.Interfaces.Transactions;
using Shop.Order.UseCases.Orders.Dto;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequest : IRequest<int>, ITransactionalRequest, IOrderRequest
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}

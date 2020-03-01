using MediatR;
using Shop.Framework.Interfaces.Transactions;
using Shop.Order.Contract.Dto;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequest : IRequest<int>, ITransactionalRequest
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}

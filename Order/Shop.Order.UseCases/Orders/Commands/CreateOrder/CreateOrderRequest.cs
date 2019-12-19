using MediatR;
using Shop.Order.UseCases.Orders.Dto;
using Shop.Utils.Transactions;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequest : IRequest, ITransactionalRequest
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}

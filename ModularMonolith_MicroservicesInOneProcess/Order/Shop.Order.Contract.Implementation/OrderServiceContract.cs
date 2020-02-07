using System;
using Shop.Order.Contract.Orders;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Contract.Orders.Dto;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Queries.GetOrder;

namespace Shop.Order.Contract.Implementation
{
    internal class OrderServiceContract : IOrderServiceContract
    {
        private readonly IMediator _mediator;

        public OrderServiceContract(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<OrderDto> GetOrderAsync(int id)
        {
            return await _mediator.Send(new GetOrderRequest { Id = id });
        }

        public Task CreateOrderAsync(string correlationId, CreateOrderDto createOrderDto)
        {
            return _mediator.Send(new CreateOrderRequest { CreateOrderDto = createOrderDto, CorrelationId = correlationId });
        }        
    }
}

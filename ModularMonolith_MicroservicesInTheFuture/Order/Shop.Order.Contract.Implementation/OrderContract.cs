using System.Threading.Tasks;
using MediatR;
using Shop.Order.Contract.Dto;
using Shop.Order.UseCases.Orders.Commands.CancelCreateOrder;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Queries.GetOrder;

namespace Shop.Order.Contract.Implementation
{
    internal class OrderContract : IOrderContract
    {
        private readonly IMediator _mediator;

        public OrderContract(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<OrderDto> GetOrderAsync(int id)
        {
            return await _mediator.Send(new GetOrderRequest { Id = id });
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            await _mediator.Send(new CreateOrderRequest { CreateOrderDto = createOrderDto });
        }

        public async Task CancelCreateOrderAsync(int orderId)
        {
            await _mediator.Send(new CancelCreateOrderRequest {Id = orderId});
        }
    }
}

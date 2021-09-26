using System.Threading.Tasks;
using MediatR;
using Shop.Order.Contract.Dto;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Commands.DeleteOrder;
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
        public async Task<int> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            return await _mediator.Send(new CreateOrderRequest { CreateOrderDto = createOrderDto });
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _mediator.Send(new DeleteOrderRequest { Id = orderId });
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            return await _mediator.Send(new GetOrderRequest { Id = orderId });
        }
    }
}

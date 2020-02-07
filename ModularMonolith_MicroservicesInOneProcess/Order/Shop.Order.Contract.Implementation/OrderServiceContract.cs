using System;
using Shop.Order.Contract.Orders;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Contract.Orders.Dto;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Queries.GetOrder;
using Shop.Utils.WaitingTasksStore;

namespace Shop.Order.Contract.Implementation
{
    internal class OrderServiceContract : IOrderServiceContract
    {
        private readonly IMediator _mediator;
        private readonly IWaitingTasksStore _waitingTasksStore;

        public OrderServiceContract(IMediator mediator, IWaitingTasksStore waitingTasksStore)
        {
            _mediator = mediator;
            _waitingTasksStore = waitingTasksStore;
        }
        public async Task<OrderDto> GetOrderAsync(int id)
        {
            return await _mediator.Send(new GetOrderRequest { Id = id });
        }

        public Task<int> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var correlationId = Guid.NewGuid().ToString();
            
            _mediator.Send(new CreateOrderRequest { CreateOrderDto = createOrderDto, CorrelationId = correlationId });

            return _waitingTasksStore.Add<int>(correlationId); 
        }        
    }
}

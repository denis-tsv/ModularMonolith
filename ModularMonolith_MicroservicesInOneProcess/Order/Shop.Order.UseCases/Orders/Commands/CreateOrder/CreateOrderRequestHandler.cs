using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Common.Contract.Services;
using Shop.Order.Contract.Orders.Messages;
using Shop.Order.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.ServiceBus;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequestHandler : AsyncRequestHandler<CreateOrderRequest>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IServiceBus _serviceBus;

        public CreateOrderRequestHandler(
            IOrderDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            IServiceBus serviceBus)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _serviceBus = serviceBus;
        }

        protected override async Task Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Entities.Order>(request.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await _serviceBus.PublishAsync(new OrderCreatedMessage
            {
                OrderId = order.Id,
                UserEmail = _currentUserService.Email,
                CorrelationId = request.CorrelationId
            });
        }
    }
}

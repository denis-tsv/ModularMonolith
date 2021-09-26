using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract.Orders.Messages.CreateOrder;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderMessageHandler : INotificationHandler<CreateOrderRequestMessage>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageBroker _messageBroker;

        public CreateOrderMessageHandler(
            IOrderDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            IMessageBroker messageBroker)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _messageBroker = messageBroker;
        }

        public async Task Handle(CreateOrderRequestMessage message, CancellationToken token)
        {            
            var order = _mapper.Map<Entities.Order>(message.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(token);

            await _messageBroker.PublishAsync(new CreateOrderResponseMessage
            {
                OrderId = order.Id,
                CorrelationId = message.CorrelationId
            });
        }
    }
}

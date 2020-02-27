using System;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Framework.Interfaces.CancelUseCase;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract.Orders.Messages.CreateOrder;
using Shop.Order.Infrastructure.Interfaces.DataAccess;
using Shop.Order.UseCases.Orders.Commands.CancelCreateOrder;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderMessageHandler : MessageHandler<CreateOrderMessage>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICancelUseCaseService _cancelService;

        public CreateOrderMessageHandler(
            IOrderDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            IMessageBroker messageBroker,
            ICancelUseCaseService cancelService) 
            : base(messageBroker)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _cancelService = cancelService;
        }

        protected override async Task Handle(CreateOrderMessage message)
        {            
            var order = _mapper.Map<Entities.Order>(message.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();

            _cancelService.Add<CancelOrderCreationContext, ICancelUseCase<CancelOrderCreationContext>>(message.CorrelationId, new CancelOrderCreationContext { OrderId = order.Id });

            await MessageBroker.PublishAsync(new OrderCreatedMessage
            {
                OrderId = order.Id,
                UserEmail = _currentUserService.Email,
                CorrelationId = message.CorrelationId
            });
        }
    }
}

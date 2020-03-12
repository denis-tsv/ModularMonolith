using System;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract.Orders.Messages.CreateOrder;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderMessageHandler : MessageHandler<CreateOrderRequestMessage>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrderMessageHandler(
            IOrderDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService,
            IMessageBroker messageBroker) 
            : base(messageBroker)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        protected override async Task Handle(CreateOrderRequestMessage message)
        {            
            var order = _mapper.Map<Entities.Order>(message.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();

            //_cancelService.AddCancel(new CreateOrderCancel { OrderId = order.Id });

            await MessageBroker.PublishAsync(new CreateOrderResponseMessage
            {
                OrderId = order.Id
            });
        }
    }
}

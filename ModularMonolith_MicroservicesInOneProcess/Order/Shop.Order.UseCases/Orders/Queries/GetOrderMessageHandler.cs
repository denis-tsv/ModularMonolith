using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Framework.Interfaces.Exceptions;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Dto;
using Shop.Order.Contract.Orders.Messages.GetOrder;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Queries
{
    internal class GetOrderMessageHandler : INotificationHandler<GetOrderRequestMessage>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMessageBroker _messageBroker;

        public GetOrderMessageHandler(IOrderDbContext dbContext, 
            IMapper mapper, 
            IMessageBroker messageBroker)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _messageBroker = messageBroker;
        }

        public async Task Handle(GetOrderRequestMessage message, CancellationToken token)
        {
            var order = await _dbContext.Orders.AsNoTracking()
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == message.Id, token);

            if (order == null) throw new EntityNotFoundException();

            var result = _mapper.Map<OrderDto>(order);
            result.Price = order.GetPrice();

            var resultMessage = new GetOrderResponseMessage {Order = result};
            await _messageBroker.PublishAsync(resultMessage);            
        }
    }
}

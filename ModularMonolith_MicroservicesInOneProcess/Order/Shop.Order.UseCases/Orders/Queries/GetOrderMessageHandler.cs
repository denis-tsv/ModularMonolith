using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Framework.Interfaces.Exceptions;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Dto;
using Shop.Order.Contract.Orders.Messages.GetOrder;
using Shop.Order.DomainServices.Interfaces;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Queries
{
    internal class GetOrderMessageHandler : MessageHandler<GetOrderMessage>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOrdersService _orderService;
        
        public GetOrderMessageHandler(IOrderDbContext dbContext, IMapper mapper, IOrdersService orderService, IMessageBroker messageBroker)
            : base(messageBroker)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _orderService = orderService;
        }

        protected async override Task Handle(GetOrderMessage message)
        {
            var order = await _dbContext.Orders.AsNoTracking()
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == message.Id);

            if (order == null) throw new EntityNotFoundException();

            var result = _mapper.Map<OrderDto>(order);
            result.Price = _orderService.GetPrice(order);

            var resultMessage = new OrderDetailsMessage {CorrelationId = message.CorrelationId, Order = result};
            await MessageBroker.PublishAsync(resultMessage);            
        }
    }
}

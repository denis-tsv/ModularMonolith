﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Framework.Interfaces.Exceptions;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Dto;
using Shop.Order.Contract.Orders.Messages.GetOrder;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Queries
{
    internal class GetOrderMessageHandler : MessageHandler<GetOrderRequestMessage>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderMessageHandler(IOrderDbContext dbContext, 
            IMapper mapper, 
            IMessageBroker messageBroker)
            : base(messageBroker)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        protected override async Task Handle(GetOrderRequestMessage message)
        {
            var order = await _dbContext.Orders.AsNoTracking()
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == message.Id);

            if (order == null) throw new EntityNotFoundException();

            var result = _mapper.Map<OrderDto>(order);
            result.Price = order.GetPrice();

            var resultMessage = new GetOrderResponseMessage {Order = result};
            await MessageBroker.PublishAsync(resultMessage);            
        }
    }
}

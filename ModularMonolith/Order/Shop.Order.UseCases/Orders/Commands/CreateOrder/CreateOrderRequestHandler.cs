using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Order.DataAccess.Interfaces;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, int>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrderRequestHandler(
            IOrderDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Entities.Order>(request.CreateOrderDto);
            order.CreationDate = DateTime.UtcNow;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}

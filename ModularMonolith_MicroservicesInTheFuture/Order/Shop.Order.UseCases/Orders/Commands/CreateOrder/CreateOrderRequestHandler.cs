using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Communication.Contract;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequestHandler : AsyncRequestHandler<CreateOrderRequest>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICommunicationContract _communicationContract;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRequestContext _requestContext;

        public CreateOrderRequestHandler(
            IOrderDbContext dbContext, 
            IMapper mapper,
            ICommunicationContract communicationContract,
            ICurrentUserService currentUserService,
            IRequestContext requestContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _communicationContract = communicationContract;
            _currentUserService = currentUserService;
            _requestContext = requestContext;
        }

        protected override async Task Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Entities.Order>(request.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            //with request context
            _requestContext.AddValue(OrderRequestContextKeys.OrderId, order.Id);
            await _communicationContract.SendOrderEmailRequestContextAsync(_currentUserService.Email, "Order created", $"Your order {order.Id} created successfully");

            //without request context
            //await _communicationContract.SendOrderEmailAsync(order.Id, _currentUserService.Email, "Order created", $"Your order {order.Id} created successfully");
        }
    }
}

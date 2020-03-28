using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Communication.Contract;
using Shop.Framework.Interfaces.Compensation;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Infrastructure.Interfaces.DataAccess;
using Shop.Order.UseCases.Orders.Commands.CancelOrder;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequestHandler : AsyncRequestHandler<CreateOrderRequest>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICommunicationContract _communicationContract;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRequestContext _requestContext;
        private readonly ICompensationService _compensationService;

        public CreateOrderRequestHandler(
            IOrderDbContext dbContext, 
            IMapper mapper,
            ICommunicationContract communicationContract,
            ICurrentUserService currentUserService,
            IRequestContext requestContext,
            ICompensationService compensationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _communicationContract = communicationContract;
            _currentUserService = currentUserService;
            _requestContext = requestContext;
            _compensationService = compensationService;
        }

        protected override async Task Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Entities.Order>(request.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            //with compensation
            _compensationService.AddRequest(new CancelOrderRequest {Id = order.Id});
            await _communicationContract.SendEmailAsync(_currentUserService.Email, "Order created", $"Your order {order.Id} created successfully", order.Id);

            //with request context
            //_requestContext.AddValue(OrderRequestContextKeys.OrderId, order.Id);
            //await _communicationContract.SendOrderEmailRequestContextAsync(_currentUserService.Email, "Order created", $"Your order {order.Id} created successfully");

            //without request context
            //await _communicationContract.SendOrderEmailAsync(order.Id, _currentUserService.Email, "Order created", $"Your order {order.Id} created successfully");
        }
    }
}

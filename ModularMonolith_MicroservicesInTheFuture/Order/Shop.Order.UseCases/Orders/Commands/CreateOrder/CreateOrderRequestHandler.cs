using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Common.Contract.Services;
using Shop.Order.Contract;
using Shop.Order.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Sagas;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequestHandler : AsyncRequestHandler<CreateOrderRequest>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEmailServiceContract _emailServiceContract;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISaga _saga;

        public CreateOrderRequestHandler(
            IOrderDbContext dbContext, 
            IMapper mapper,
            IEmailServiceContract emailServiceContract,
            ICurrentUserService currentUserService,
            ISaga saga)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _emailServiceContract = emailServiceContract;
            _currentUserService = currentUserService;
            _saga = saga;
        }

        protected override async Task Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Entities.Order>(request.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            //with saga
            _saga.AddValue(OrderSagaKeys.OrderId, order.Id);
            await _emailServiceContract.SendOrderEmailSagaAsync(_currentUserService.Email, "Order created", $"Your order {order.Id} created successfully");

            //without saga
            //await _emailServiceContract.SendOrderEmailAsync(order.Id, _currentUserService.Email, "Order created", $"Your order {order.Id} created successfully");
        }
    }
}

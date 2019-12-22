using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Entities;
using Shop.Infrastructure.Interfaces.DataAccess;
using Shop.Infrastructure.Interfaces.Services;

namespace Shop.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderRequestHandler : AsyncRequestHandler<CreateOrderRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public CreateOrderRequestHandler(
            IDbContext dbContext, 
            IMapper mapper, 
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;            
        }

        protected override async Task Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Entities.Order>(request.CreateOrderDto);
            order.CreationDate = DateTime.Now;
            order.UserId = _currentUserService.Id;
            _dbContext.Orders.Add(order);

            var newMail = new Email
            {
                Address = _currentUserService.Email,
                Subject = "Order created",
                Body = $"Your order {order.Id} created successfully"
            };
            _dbContext.Emails.Add(newMail);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

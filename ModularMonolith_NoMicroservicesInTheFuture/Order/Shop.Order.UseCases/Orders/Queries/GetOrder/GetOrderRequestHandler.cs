using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Framework.Interfaces.Exceptions;
using Shop.Order.Contract.Dto;
using Shop.Order.DomainServices.Interfaces;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Queries.GetOrder
{
    internal class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDto>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOrdersService _orderService;
        
        public GetOrderRequestHandler(IOrderDbContext dbContext, IMapper mapper, IOrdersService orderService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _orderService = orderService;            
        }

        public async Task<OrderDto> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.AsNoTracking()
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (order == null) throw new EntityNotFoundException();

            var result = _mapper.Map<OrderDto>(order);
            result.Price = _orderService.GetPrice(order);            

            return result;
        }
    }
}

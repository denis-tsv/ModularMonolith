using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.DomainServices.Interfaces;
using Shop.Infrastructure.Interfaces.DataAccess;
using Shop.UseCases.Orders.Dto;
using Shop.Utils.Exceptions;

namespace Shop.UseCases.Orders.Queries.GetOrder
{
    public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IOrdersService _orderService;
        
        public GetOrderRequestHandler(IDbContext dbContext, IMapper mapper, IOrdersService orderService)
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

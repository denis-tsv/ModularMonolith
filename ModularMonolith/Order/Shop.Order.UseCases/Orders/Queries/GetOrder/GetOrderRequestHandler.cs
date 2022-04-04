using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Communication.Contract;
using Shop.Framework.UseCases.Interfaces.Exceptions;
using Shop.Order.DataAccess.Interfaces;
using Shop.Order.UseCases.Orders.Dto;

namespace Shop.Order.UseCases.Orders.Queries.GetOrder
{
    internal class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDto>
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICommunicationContract _communicationContract;

        public GetOrderRequestHandler(IOrderDbContext dbContext, IMapper mapper, ICommunicationContract communicationContract)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _communicationContract = communicationContract;
        }

        public async Task<OrderDto> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.AsNoTracking()
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (order == null) throw new EntityNotFoundException();

            var result = _mapper.Map<OrderDto>(order);
            result.Price = order.GetPrice();
            result.EmailsCount = await _communicationContract.GetOrderEmailsCountAsync(request.Id, cancellationToken);

            return result;
        }
    }
}

﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CancelCreateOrder
{
    internal class CancelCreateOrderRequestHandler : AsyncRequestHandler<CancelCreateOrderRequest>
    {
        private readonly IOrderDbContext _dbContext;

        public CancelCreateOrderRequestHandler(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(CancelCreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _dbContext.Orders.FindAsync(request.Id); //order already tracked by context
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                //log
                //schedule task to remove order
            }
        }
    }
}

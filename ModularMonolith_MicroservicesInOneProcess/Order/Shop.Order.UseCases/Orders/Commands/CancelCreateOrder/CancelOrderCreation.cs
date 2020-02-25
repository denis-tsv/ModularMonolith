using System.Threading.Tasks;
using Shop.Order.Infrastructure.Interfaces.DataAccess;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Utils.CancelUseCase;

namespace Shop.Order.UseCases.Orders.MessageHandlers
{
    internal class CancelOrderCreation : ICancelUseCase<CancelOrderCreationContext>
    {
        private readonly IOrderDbContext _dbContext;

        public CancelOrderCreation(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task CancelAsync(CancelOrderCreationContext context)
        {
            try
            {
                var order = await _dbContext.Orders.FindAsync(context.OrderId); //order already tracked by context
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                //log using correlation id
                //schedule task to remove order
            }
        }
    }
}

using System.Threading.Tasks;
using Shop.Framework.Interfaces.Cancel;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CancelCreateOrder
{
    internal class CreateOrderCancelHandler : ICancelHandler<CreateOrderCancel>
    {
        private readonly IOrderDbContext _dbContext;

        public CreateOrderCancelHandler(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task HandleAsync(CreateOrderCancel cancel)
        {
            try
            {
                var order = await _dbContext.Orders.FindAsync(cancel.OrderId); //order already tracked by context
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

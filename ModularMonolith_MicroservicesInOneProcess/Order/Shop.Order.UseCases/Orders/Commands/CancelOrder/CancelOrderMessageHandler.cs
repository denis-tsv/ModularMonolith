using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.UseCases.Orders.Commands.CancelOrder
{
    internal class CancelOrderMessageHandler : MessageHandler<CancelOrderMesage>
    {
        private readonly IOrderDbContext _dbContext;

        public CancelOrderMessageHandler(IMessageBroker messageBroker, IOrderDbContext dbContext) : base(messageBroker)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(CancelOrderMesage message)
        {
            var order = await _dbContext.Orders.FindAsync(message.OrderId); //order already tracked by context
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}

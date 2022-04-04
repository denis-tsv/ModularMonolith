using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Communication.Entities;
using Shop.Order.Contract.Notifications;

namespace Shop.Communication.UseCases.Emails.NotificationHandlers
{
    internal class OrderCreatedNotificationHandler : INotificationHandler<OrderCreatedNotification>
    {
        private readonly ICommunicationDbContext _dbContext;

        public OrderCreatedNotificationHandler(ICommunicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
        {
            var mail = new Email
            {
                Address = notification.UserEmail,
                Subject = "Order created",
                Body = $"Your order {notification.OrderId} created successfully",
                OrderId = notification.OrderId,
                UserId = notification.UserId
            };
            _dbContext.Emails.Add(mail);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
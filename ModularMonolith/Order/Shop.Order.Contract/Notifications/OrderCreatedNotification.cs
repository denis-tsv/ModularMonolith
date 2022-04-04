using MediatR;

namespace Shop.Order.Contract.Notifications
{
    public record OrderCreatedNotification(int OrderId, int UserId, string UserEmail) : INotification
    {
    }
}
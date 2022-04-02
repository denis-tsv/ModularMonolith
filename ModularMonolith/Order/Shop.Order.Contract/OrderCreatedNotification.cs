using MediatR;

namespace Shop.Order.Contract
{
    public record OrderCreatedNotification(int OrderId, string Email, int UserId) : INotification
    {
    }
}
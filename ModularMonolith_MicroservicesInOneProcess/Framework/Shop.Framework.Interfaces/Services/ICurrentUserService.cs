namespace Shop.Framework.Interfaces.Services
{
    public interface ICurrentUserService
    {
        int Id { get; }
        bool IsAuthenticated { get; }
        string Email { get; }
        string CorrelationId { get; }
    }
}

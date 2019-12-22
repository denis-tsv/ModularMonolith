namespace Shop.DomainServices.Interfaces
{
    public interface IOrdersService
    {
        decimal GetPrice(Entities.Order order);
    }
}

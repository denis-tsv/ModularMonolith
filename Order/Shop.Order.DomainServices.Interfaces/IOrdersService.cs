namespace Shop.Order.DomainServices.Interfaces
{
    internal interface IOrdersService
    {
        decimal GetPrice(Entities.Order order);
    }
}

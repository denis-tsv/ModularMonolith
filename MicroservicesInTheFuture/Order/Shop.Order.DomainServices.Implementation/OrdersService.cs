using System.Linq;
using Shop.Order.DomainServices.Interfaces;

namespace Shop.Order.DomainServices.Implementation
{
    internal class OrdersService : IOrdersService
    {
        public decimal GetPrice(Entities.Order order)
        {
            return order.Items.Sum(x => x.Count * x.Product.Price);
        }
    }
}

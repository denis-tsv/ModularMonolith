using System.Linq;
using Shop.DomainServices.Interfaces;

namespace Shop.DomainServices.Implementation
{
    public class OrdersService : IOrdersService
    {
        public decimal GetPrice(Entities.Order order)
        {
            return order.Items.Sum(x => x.Count * x.Product.Price);
        }
    }
}

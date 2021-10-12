using Shop.Framework.Entities;

namespace Shop.Order.Entities
{
    internal class Product : Aggregate
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}

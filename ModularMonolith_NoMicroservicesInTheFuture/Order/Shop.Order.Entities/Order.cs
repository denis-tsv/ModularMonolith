using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Framework.Entities;

namespace Shop.Order.Entities
{
    internal class Order : Aggregate
    {
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        public ICollection<OrderItem> Items { get; private set; } = new HashSet<OrderItem>();

        public decimal GetPrice()
        {
            return Items.Sum(x => x.Count * x.Product.Price);
        }
    }
}

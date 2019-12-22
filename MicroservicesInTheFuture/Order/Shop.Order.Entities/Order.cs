using System;
using System.Collections.Generic;

namespace Shop.Order.Entities
{
    internal class Order
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        public ICollection<OrderItem> Items { get; private set; } = new HashSet<OrderItem>();
    }
}

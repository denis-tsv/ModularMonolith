using System.ComponentModel.DataAnnotations;
using Shop.Framework.Entities;

namespace Shop.Order.Entities
{
    internal class OrderItem : Entity
    {
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; }


        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Order.UseCases.Orders.Dto
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Count { get; set; }
    }
}

namespace Shop.Order.UseCases.Orders.Dto
{
    internal class OrderDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int EmailsCount { get; set; }
    }
}

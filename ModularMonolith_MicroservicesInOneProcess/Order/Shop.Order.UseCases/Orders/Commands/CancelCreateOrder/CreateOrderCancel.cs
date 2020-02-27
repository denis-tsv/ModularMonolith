using Shop.Framework.Interfaces.Cancel;

namespace Shop.Order.UseCases.Orders.Commands.CancelCreateOrder
{
    internal class CreateOrderCancel : ICancel
    {
        public int OrderId { get; set; }
    }
}

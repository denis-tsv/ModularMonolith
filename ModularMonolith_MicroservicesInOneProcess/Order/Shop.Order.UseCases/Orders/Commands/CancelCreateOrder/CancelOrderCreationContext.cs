using Shop.Framework.Interfaces.CancelUseCase;

namespace Shop.Order.UseCases.Orders.Commands.CancelCreateOrder
{
    internal class CancelOrderCreationContext : ICancelContext
    {
        public int OrderId { get; set; }
    }
}

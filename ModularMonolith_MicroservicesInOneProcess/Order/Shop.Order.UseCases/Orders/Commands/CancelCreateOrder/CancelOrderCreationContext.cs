using Shop.Utils.CancelUseCase;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CancelOrderCreationContext : ICancelContext
    {
        public int OrderId { get; set; }
    }
}

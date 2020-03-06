using Shop.Framework.Interfaces.Messaging;

namespace Shop.Order.UseCases.Orders.Commands.CancelOrder
{
    internal class CancelOrderMesage : Message
    {
        public int OrderId { get; set; }
    }
}

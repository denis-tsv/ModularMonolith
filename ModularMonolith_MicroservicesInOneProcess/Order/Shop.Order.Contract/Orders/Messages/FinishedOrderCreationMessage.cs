using System;
using Shop.Utils.ServiceBus;

namespace Shop.Order.Contract.Orders.Messages
{
    public class FinishedOrderCreationMessage : Message
    {
        public int OrderId { get; set; }
    }
}

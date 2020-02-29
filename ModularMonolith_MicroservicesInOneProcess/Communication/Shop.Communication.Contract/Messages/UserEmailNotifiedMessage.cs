using Shop.Framework.Interfaces.Messaging;

namespace Shop.Communication.Contract.Messages
{
    public class UserEmailNotifiedMessage : Message
    {
        public int OrderId { get; set; }
    }
}

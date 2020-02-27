using Shop.Framework.Interfaces.Messaging;

namespace Shop.Communication.Contract.Messages
{
    public class EntityEmailMessage : Message
    {
        public int Id { get; set; }
    }
}

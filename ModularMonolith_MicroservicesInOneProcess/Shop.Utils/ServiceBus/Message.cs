namespace Shop.Utils.ServiceBus
{
    public abstract class Message : IMessage
    {
        public string CorrelationId { get; set; }
    }
}

using System.Threading.Tasks;
using Shop.Utils.Messaging;

namespace Shop.Web.Utils.Dispatcher
{
    public interface IMessageDispatcher
    {
        Task<TResultMessage> SendMessageAsync<TResultMessage>(Message message) where TResultMessage : Message;
    }
}

using System.Threading.Tasks;
using Shop.Framework.Interfaces.Messaging;

namespace Shop.Web.Utils.Dispatcher
{
    public interface IMessageDispatcher
    {
        Task<TResultMessage> SendMessageAsync<TResultMessage>(Message message) where TResultMessage : Message;
    }
}

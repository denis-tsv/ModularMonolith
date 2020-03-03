using System.Threading.Tasks;

namespace Shop.Communication.Contract
{
    public interface ICommunicationContract
    {
        // we need to know that we send email about order to make a cancel in case of error
        Task SendOrderEmailRequestContextAsync(string email, string subject, string body);
        Task SendOrderEmailAsync(int orderId, string email, string subject, string body);
    }
}

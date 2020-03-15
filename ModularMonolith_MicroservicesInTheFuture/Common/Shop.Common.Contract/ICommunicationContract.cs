using System.Threading.Tasks;

namespace Shop.Communication.Contract
{
    public interface ICommunicationContract
    {
        Task SendEmailAsync(string email, string subject, string body, int orderId);

        // we need to know that we send email about order to make a cancel in case of error
        Task SendOrderEmailRequestContextAsync(string email, string subject, string body);
        Task SendOrderEmailAsync(int orderId, string email, string subject, string body);
    }
}

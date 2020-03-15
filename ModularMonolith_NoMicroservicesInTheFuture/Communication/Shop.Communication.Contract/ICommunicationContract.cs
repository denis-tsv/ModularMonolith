using System.Threading.Tasks;

namespace Shop.Communication.Contract
{
    public interface ICommunicationContract
    {
        Task SendEmailAsync(string email, string subject, string body, int orderId);
    }
}

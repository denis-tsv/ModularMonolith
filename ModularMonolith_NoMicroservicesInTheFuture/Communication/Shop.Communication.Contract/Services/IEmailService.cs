using System.Threading.Tasks;

namespace Shop.Communication.Contract.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}

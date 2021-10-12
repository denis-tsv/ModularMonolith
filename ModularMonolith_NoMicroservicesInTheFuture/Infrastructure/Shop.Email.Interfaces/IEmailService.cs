using System.Threading.Tasks;

namespace Shop.Emails.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
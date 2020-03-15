using System.Threading.Tasks;

namespace Shop.Communication.Infrastructure.Interfaces.Services
{
    internal interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
using System.Threading.Tasks;

namespace Shop.Common.Infrastructure.Interfaces.Services
{
    internal interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}

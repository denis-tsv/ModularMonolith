using System.Threading.Tasks;

namespace Shop.Common.Contract.Services
{
    public interface IEmailServiceContract
    {
        // we need to know that we send email about order to make a cancel in case of error
        Task SendOrderEmailSagaAsync(string email, string subject, string body);
        Task SendOrderEmailAsync(int orderId, string email, string subject, string body);
    }
}

using Microsoft.Extensions.DependencyInjection;
using Shop.Emails.Interfaces;
using Shop.Utils.Modules;

namespace Shop.Emails.Implementation
{
    public class EmailModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();

            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
        }
    }
}

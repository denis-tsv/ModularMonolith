using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.Infrastructure.Implementation.BackgroundJobs;
using Shop.Communication.Infrastructure.Interfaces.Options;
using Shop.Utils.Modules;

namespace Shop.Communication.Infrastructure.Implementation
{
    public class CommunicationInfrastructureModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<SendEmailsJob>();

            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
        }
    }
}

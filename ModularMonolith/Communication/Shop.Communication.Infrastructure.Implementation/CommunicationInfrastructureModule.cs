using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.BackgroundJobs.BackgroundJobs;
using Shop.Utils.Modules;

namespace Shop.Communication.BackgroundJobs
{
    public class CommunicationInfrastructureModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<SendEmailsJob>();
        }
    }
}

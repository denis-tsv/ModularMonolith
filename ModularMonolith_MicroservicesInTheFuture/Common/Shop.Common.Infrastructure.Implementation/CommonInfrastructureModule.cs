using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Infrastructure.Implementation.BackgroundJobs;
using Shop.Common.Infrastructure.Implementation.Services;
using Shop.Common.Infrastructure.Interfaces.Options;
using Shop.Common.Infrastructure.Interfaces.Services;
using Shop.Utils.Modules;

namespace Shop.Common.Infrastructure.Implementation
{
    public class CommonInfrastructureModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<SendEmailsJob>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
        }
    }
}

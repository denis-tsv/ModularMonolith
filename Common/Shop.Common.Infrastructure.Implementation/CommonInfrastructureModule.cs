using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Contract.Services;
using Shop.Common.Infrastructure.Implementation.BackgroundJobs;
using Shop.Common.Infrastructure.Implementation.Services;
using Shop.Common.Infrastructure.Interfaces.Options;
using Shop.Utils.Modules;

namespace Shop.Common.Infrastructure.Implementation
{
    public class CommonInfrastructureModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddTransient<SendEmailsJob>();

            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
        }

        //protected override void Load(ContainerBuilder builder)
        //{
        //    builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
        //    builder.RegisterType<CurrentUserService>().As<ICurrentUserService>().InstancePerLifetimeScope();

        //    builder.RegisterType<SendEmailsJob>();

        //    builder.Register(p => _configuration.GetSection("EmailOptions").Get<EmailOptions>()).SingleInstance();
        //}
    }
}

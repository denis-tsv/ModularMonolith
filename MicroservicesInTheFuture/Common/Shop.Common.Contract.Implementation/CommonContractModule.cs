using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Contract.Implementation.Services;
using Shop.Common.Contract.Services;
using Shop.Utils.Modules;

namespace Shop.Common.Contract.Implementation
{
    public class CommonContractModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}

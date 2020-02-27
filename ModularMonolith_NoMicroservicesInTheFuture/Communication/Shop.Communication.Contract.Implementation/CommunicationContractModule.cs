using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.Contract.Implementation.Services;
using Shop.Communication.Contract.Services;
using Shop.Utils.Modules;

namespace Shop.Communication.Contract.Implementation
{
    public class CommunicationContractModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}

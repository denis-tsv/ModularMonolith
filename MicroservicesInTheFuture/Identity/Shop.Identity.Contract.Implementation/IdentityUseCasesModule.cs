using Microsoft.Extensions.DependencyInjection;
using Shop.Identity.Contract.Identity;
using Shop.Utils.Modules;

namespace Shop.Identity.Contract.Implementation
{
    public class IdentityContractModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}

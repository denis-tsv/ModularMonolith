using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.UseCases.Implementation.Services;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Utils.Modules;

namespace Shop.Framework.UseCases.Implementation
{
    public class FrameworkModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}

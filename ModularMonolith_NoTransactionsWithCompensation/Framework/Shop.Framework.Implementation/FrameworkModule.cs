using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.Implementation.Services;
using Shop.Framework.Interfaces.Services;
using Shop.Utils.Modules;
using IMvcUrlHelper = Microsoft.AspNetCore.Mvc.IUrlHelper;

namespace Shop.Framework.Implementation
{
    public class FrameworkModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUrlHelper, McvUrlHelper>();
            
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IMvcUrlHelper>(serviceProvider =>
            {
                var actionContext = serviceProvider.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = serviceProvider.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
        }
    }
}

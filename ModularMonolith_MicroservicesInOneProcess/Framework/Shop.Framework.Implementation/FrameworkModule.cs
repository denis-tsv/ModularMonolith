using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.Implementation.CancelUseCase;
using Shop.Framework.Implementation.Messaging;
using Shop.Framework.Implementation.Services;
using Shop.Framework.Interfaces.Cancel;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Utils.Modules;

namespace Shop.Framework.Implementation
{
    public class FrameworkModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IMessageBroker, MediatrMessageBroker>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<ICancelService, CancelService>();
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.Implementation.CancelUseCase;
using Shop.Framework.Implementation.Messaging;
using Shop.Framework.Implementation.Services;
using Shop.Framework.Interfaces.Cancel;
using Shop.Framework.Interfaces.Messaging;
using Shop.Framework.Interfaces.Services;
using Shop.Utils.Modules;
using IMvcUrlHelper = Microsoft.AspNetCore.Mvc.IUrlHelper;

namespace Shop.Framework.Implementation
{
    public class FrameworkModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IMessageBroker, MediatrMessageBroker>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<ICancelService, CancelService>();
            services.AddScoped<IUrlHelper, MvcUrlHelper>();

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

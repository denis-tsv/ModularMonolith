using Microsoft.Extensions.DependencyInjection;
using Shop.Order.DomainServices.Interfaces;
using Shop.Utils.Modules;

namespace Shop.Order.DomainServices.Implementation
{
    public class OrderDomainServicesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IOrdersService, OrdersService>();
        }
    }
}

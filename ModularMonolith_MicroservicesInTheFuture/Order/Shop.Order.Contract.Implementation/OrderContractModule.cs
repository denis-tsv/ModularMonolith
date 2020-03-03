using Microsoft.Extensions.DependencyInjection;
using Shop.Utils.Modules;

namespace Shop.Order.Contract.Implementation
{
    public class OrderContractModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IOrderContract, OrderContract>();
        }
    }
}

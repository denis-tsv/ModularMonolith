using Microsoft.Extensions.DependencyInjection;
using Shop.Order.Contract.Orders;
using Shop.Utils.Modules;

namespace Shop.Order.Contract.Implementation
{
    public class OrderContractModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IOrderServiceContract, OrderServiceContract>();
        }
    }
}

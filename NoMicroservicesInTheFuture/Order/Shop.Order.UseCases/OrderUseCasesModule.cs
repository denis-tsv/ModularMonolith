using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Utils.Modules;

namespace Shop.Order.UseCases
{
    public class OrderUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateOrderRequest));
        }
    }
}

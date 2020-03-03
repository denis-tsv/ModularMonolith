using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.Interfaces.Cancel;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Commands.CreateOrderCancel;
using Shop.Utils.Modules;

namespace Shop.Order.UseCases
{
    public class OrderUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateOrderRequest));

            services.AddTransient<ICancelHandler<CreateOrderCancel>, CreateOrderCancelHandler>();
        }
    }
}

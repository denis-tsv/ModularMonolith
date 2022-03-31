using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Mappings;
using Shop.Order.UseCases.Orders.Sagas;
using Shop.Utils.Modules;

namespace Shop.Order.UseCases
{
    public class OrderUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<Profile, OrdersAutoMapperProfile>();

            services.AddTransient<IBaseRequest, CreateOrderRequest>();

            services.AddScoped<CreateOrderSaga>();
        }
    }
}

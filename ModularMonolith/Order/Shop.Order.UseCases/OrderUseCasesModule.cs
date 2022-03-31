using Autofac;
using AutoMapper;
using MediatR;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Mappings;

namespace Shop.Order.UseCases
{
    public class OrderUseCasesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrdersAutoMapperProfile>().As<Profile>().InstancePerDependency();
            builder.RegisterType<CreateOrderRequest>().As<IBaseRequest>().InstancePerDependency();
        }
    }
}

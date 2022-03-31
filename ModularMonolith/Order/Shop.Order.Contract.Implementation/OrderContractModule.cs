using Autofac;

namespace Shop.Order.Contract.Implementation
{
    public class OrderContractModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderContract>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}

using Autofac;

namespace Shop.Communication.Contract.Implementation
{
    public class CommunicationContractModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommunicationContract>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}

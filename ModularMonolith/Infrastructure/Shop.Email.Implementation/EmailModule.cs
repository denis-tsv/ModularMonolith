using Autofac;

namespace Shop.Emails.Implementation
{
    public class EmailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}

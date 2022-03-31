using Autofac;
using FluentScheduler;
using Shop.Communication.BackgroundJobs.BackgroundJobs;

namespace Shop.Communication.BackgroundJobs
{
    public class CommunicationInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SendEmailsJob>().InstancePerDependency();

            builder.RegisterType<CommunicationJobRegistry>()
                .As<Registry>()
                .InstancePerDependency();
        }
    }
}

using Autofac;
using AutoMapper;
using MediatR;
using Shop.Communication.UseCases.Emails.Mappings;
using Shop.Communication.UseCases.Emails.Queries.GetEmails;

namespace Shop.Communication.UseCases
{
    public class CommunicationUseCasesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailsAutoMapperProfile>().As<Profile>().InstancePerDependency();
            builder.RegisterType<GetEmailsRequest>().As<IBaseRequest>().InstancePerDependency();
        }
    }
}

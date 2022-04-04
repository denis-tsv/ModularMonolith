using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.UseCases.Emails.Mappings;
using Shop.Communication.UseCases.Emails.Queries.GetEmails;
using Shop.Utils.Modules;

namespace Shop.Communication.UseCases
{
    public class CommunicationUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<Profile, EmailsAutoMapperProfile>();

            services.AddTransient<IBaseRequest, GetEmailsRequest>();
        }
    }
}

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.UseCases.Emails.Queries.GetEmails;
using Shop.Utils.Modules;

namespace Shop.Communication.UseCases
{
    public class CommunicationUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            //works fine when registered in other modules
            services.AddMediatR(typeof(SendEmailRequestHandler));
        }
    }
}

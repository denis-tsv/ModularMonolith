using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.UseCases.Emails.Commands.SendEmail;
using Shop.Utils.Modules;

namespace Shop.Communication.UseCases
{
    public class CommunicationUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(SendEmailRequestHandler));
        }
    }
}

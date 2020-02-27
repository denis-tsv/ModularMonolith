using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.UseCases.Handlers;
using Shop.Utils.Modules;

namespace Shop.Communication.UseCases
{
    public class CommunicationUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(OrderCreatedMessageHandler));
        }
    }
}

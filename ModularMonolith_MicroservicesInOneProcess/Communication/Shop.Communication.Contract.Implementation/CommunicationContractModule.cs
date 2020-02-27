using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Utils.Modules;

namespace Shop.Communication.Contract.Implementation
{
    public class CommunicationContractModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(OrderCreatedMessageHandler));
        }
    }
}

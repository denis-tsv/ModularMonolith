using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Utils.Modules;

namespace Shop.Common.Contract.Implementation
{
    public class CommonContractModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(OrderCreatedMessageHandler));
        }
    }
}

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Identity.UseCases.Identity.Commands.Login;
using Shop.Utils.Modules;

namespace Shop.Identity.UseCases
{
    public class IdentityUseCasesModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginRequest));
        }
    }
}

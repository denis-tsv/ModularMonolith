using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Utils.Modules;

namespace Shop.Communication.DataAccess.MsSql
{
    public class CommunicationDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<ICommunicationDbContext, CommunicationDbContext>((sp, bld) =>
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                bld.UseSqlServer(factory.GetConnection());
            });
        }
    }
}

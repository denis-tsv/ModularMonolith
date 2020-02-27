using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.Interfaces.Services;
using Shop.Identity.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;

namespace Shop.Identity.DataAccess.MsSql
{
    public class IdentityDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<IIdentityDbContext, IdentityDbContext>((sp, bld) => 
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                bld.UseSqlServer(factory.GetConnection());
            });
        }
    }
}

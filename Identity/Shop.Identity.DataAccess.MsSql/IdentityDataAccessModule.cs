using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Identity.Entities;
using Shop.Identity.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;

namespace Shop.Identity.DataAccess.MsSql
{
    public class IdentityDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Identity"/*"MsSqlConnection"*/)));
            services.AddScoped<IIdentityDbContext>(fact => fact.GetService<IdentityDbContext>());

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentityDbContext>();
        }
    }
}

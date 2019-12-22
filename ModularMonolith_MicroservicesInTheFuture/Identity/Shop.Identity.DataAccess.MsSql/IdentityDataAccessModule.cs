using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Identity.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;
using Microsoft.Extensions.Configuration;

namespace Shop.Identity.DataAccess.MsSql
{
    public class IdentityDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<IIdentityDbContext, IdentityDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Identity")));           
            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;

namespace Shop.Common.DataAccess.MsSql
{
    public class CommonDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<CommonDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));
            services.AddScoped<ICommonDbContext>(fact => fact.GetService<CommonDbContext>());
        }
    }
}

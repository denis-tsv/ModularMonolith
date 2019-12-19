using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;
using Shop.Utils.Connections;
using Microsoft.Extensions.Configuration;

namespace Shop.Common.DataAccess.MsSql
{
    public class CommonDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IConnectionFactory, ConnectionFactory>(factory => new ConnectionFactory(Configuration.GetConnectionString("MsSqlConnection")));

            services.AddDbContext<ICommonDbContext, CommonDbContext>((sp, bld) => 
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                bld.UseSqlServer(factory.GetConnection());
            });
        }
    }
}

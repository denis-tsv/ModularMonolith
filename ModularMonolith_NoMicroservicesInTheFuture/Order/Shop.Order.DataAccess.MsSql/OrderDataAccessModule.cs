using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;

namespace Shop.Order.DataAccess.MsSql
{
    public class OrderDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<IOrderDbContext, OrderDbContext>((sp, bld) => 
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                bld.UseSqlServer(factory.GetConnection());
            });            
        }
    }
}

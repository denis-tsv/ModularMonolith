using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Order.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;
using Microsoft.Extensions.Configuration;

namespace Shop.Order.DataAccess.MsSql
{
    public class OrderDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<IOrderDbContext, OrderDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Order")));            
        }
    }
}

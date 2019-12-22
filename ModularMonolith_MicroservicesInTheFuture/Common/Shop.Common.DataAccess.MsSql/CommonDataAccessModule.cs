using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;
using Microsoft.Extensions.Configuration;

namespace Shop.Common.DataAccess.MsSql
{
    public class CommonDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<ICommonDbContext, CommonDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Common")));
        }
    }
}

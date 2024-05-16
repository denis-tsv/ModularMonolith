using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Utils.Modules;

namespace Shop.Communication.DataAccess.MsSql
{
    public class CommunicationDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<ICommunicationDbContext, CommunicationDbContext>((sp, bld) =>
            {
                bld.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection"));
            });
        }
    }
}

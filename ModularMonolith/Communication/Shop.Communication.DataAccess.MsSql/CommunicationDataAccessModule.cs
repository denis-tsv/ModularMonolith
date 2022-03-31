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
            services.AddDbContext<CommunicationDbContext>((sp, bld) =>
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                bld.UseSqlServer(factory.GetConnection());
            });

            services.AddScoped<ICommunicationDbContext>(sp =>
            {
                var context = sp.GetRequiredService<CommunicationDbContext>();
                var connectionFactory = sp.GetRequiredService<IConnectionFactory>();

                context.Database.UseTransaction(connectionFactory.GetTransaction());

                return context;
            });
        }
    }
}

using Autofac;
using Microsoft.EntityFrameworkCore;
using Shop.Utils.Connections;

namespace Shop.Common.DataAccess.MsSql
{
    public static class ContainerBuilderExtensions
    {
        public static void AddDbContext<TInterface, TImplementation>(this ContainerBuilder containerBuilder)
            where TImplementation : DbContext, TInterface
        {
            containerBuilder.Register(
                    componentContext =>
                    {
                        var optionsBuilder = new DbContextOptionsBuilder<TImplementation>();
                        var connectionFactory = componentContext.Resolve<IConnectionFactory>();
                        optionsBuilder.UseSqlServer(connectionFactory.GetConnection());
                        return optionsBuilder.Options;
                    })
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<TImplementation>()
                .OnActivated(args =>
                {
                    var t = args.Context.Resolve<IConnectionFactory>().GetTransaction();
                    args.Instance.Database.UseTransaction(t);
                })
                .InstancePerLifetimeScope()
                .As<TInterface>();
        }
    }
}

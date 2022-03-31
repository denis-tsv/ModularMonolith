using Autofac;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Shop.Framework.UseCases.Implementation.Services;

namespace Shop.Framework.UseCases.Implementation
{
    public class FrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CurrentUserService>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ActionContextAccessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c =>
            {
                var configuration = c.Resolve<IConfiguration>();
                var connectionString = configuration.GetConnectionString("MsSqlConnection");
                return new ConnectionFactory(connectionString);
            }).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}

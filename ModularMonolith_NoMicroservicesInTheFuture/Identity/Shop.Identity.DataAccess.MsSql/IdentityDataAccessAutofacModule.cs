using Autofac;
using Shop.Framework.Implementation;
using Shop.Identity.Infrastructure.Interfaces.DataAccess;

namespace Shop.Identity.DataAccess.MsSql
{
    public class IdentityDataAccessAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddDbContext<IIdentityDbContext, IdentityDbContext>();
        }
    }
}

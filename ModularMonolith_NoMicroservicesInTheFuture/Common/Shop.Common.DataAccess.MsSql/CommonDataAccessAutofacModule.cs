using Autofac;
using Shop.Common.Infrastructure.Interfaces.DataAccess;

namespace Shop.Common.DataAccess.MsSql
{
    public class CommonDataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddDbContext<ICommonDbContext, CommonDbContext>();
        }
    }
}

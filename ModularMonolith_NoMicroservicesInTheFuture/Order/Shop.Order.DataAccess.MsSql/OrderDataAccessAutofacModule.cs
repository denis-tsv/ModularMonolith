using Autofac;
using Shop.Common.DataAccess.MsSql;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.DataAccess.MsSql
{
    public class OrderDataAccessAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddDbContext<IOrderDbContext, OrderDbContext>();
        }
    }
}

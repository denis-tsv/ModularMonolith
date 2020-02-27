using Autofac;
using Shop.Framework.Implementation;
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

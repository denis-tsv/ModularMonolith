using Autofac;
using Shop.Framework.UseCases.Implementation;
using Shop.Order.DataAccess.Interfaces;

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

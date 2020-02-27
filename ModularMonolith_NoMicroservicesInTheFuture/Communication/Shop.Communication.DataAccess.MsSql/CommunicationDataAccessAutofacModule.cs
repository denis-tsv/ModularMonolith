using Autofac;
using Shop.Communication.Infrastructure.Interfaces.DataAccess;
using Shop.Framework.Implementation;

namespace Shop.Communication.DataAccess.MsSql
{
    public class CommunicationDataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddDbContext<ICommunicationDbContext, CommunicationDbContext>();
        }
    }
}

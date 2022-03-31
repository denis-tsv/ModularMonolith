using Autofac;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Framework.UseCases.Implementation;

namespace Shop.Communication.DataAccess.MsSql
{
    public class CommunicationDataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddDbContext<ICommunicationDbContext, CommunicationDbContext>();
        }
    }
}

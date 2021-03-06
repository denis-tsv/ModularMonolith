﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Communication.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Modules;

namespace Shop.Communication.DataAccess.MsSql
{
    public class CommunicationDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<ICommunicationDbContext, CommunicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Communication")));
        }
    }
}

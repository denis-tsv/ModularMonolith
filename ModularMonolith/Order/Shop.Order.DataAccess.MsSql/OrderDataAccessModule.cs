﻿using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Order.DataAccess.Interfaces;
using Shop.Utils.Modules;

namespace Shop.Order.DataAccess.MsSql
{
    public class OrderDataAccessModule : Module
    {
        public override void Load(IServiceCollection services)
        {
            services.AddDbContext<OrderDbContext>((sp, bld) =>
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();
                bld.UseSqlServer(factory.GetConnection());
            });

            services.AddScoped<IOrderDbContext>(sp =>
            {
                var context = sp.GetRequiredService<OrderDbContext>();
                var connectionFactory = sp.GetRequiredService<IConnectionFactory>();

                context.Database.EnlistTransaction(connectionFactory.GetTransaction());

                return context;
            });
        }
    }
}

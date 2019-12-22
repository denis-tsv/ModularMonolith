﻿using System;
using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Web.BackgroundJobsConfig
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob GetJobInstance<T>() where T : IJob
        {
            var scope = _serviceProvider.CreateScope();
            var res = scope.ServiceProvider.GetService<T>();
            return res;
        }
    }
}

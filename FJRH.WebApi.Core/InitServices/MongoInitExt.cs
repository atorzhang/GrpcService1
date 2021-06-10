using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public static class MongoInitExt
    {
        /// <summary>
        /// 添加对Monodb支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            services.AddScoped(typeof(MongoProvider));
            return services;
        }
    }
}

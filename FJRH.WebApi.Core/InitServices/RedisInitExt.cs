using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public static class RedisInitExt
    {
        /// <summary>
        /// 添加对Redis支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            //初始化Redis服务
            services.AddSingleton(typeof(RedisProvider));
            return services;
        }
    }
}

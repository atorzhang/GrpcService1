using CSRedis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public static class RedisInitExt
    {
        private static CSRedisClient csredis;
        /// <summary>
        /// 添加对Redis支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedis(this IServiceCollection services)
        {
            //初始化Redis服务
            var config = ConfigHelper.DefaultConfiguration;
            var redisConn = config["Connection:Redis"];
            if (!string.IsNullOrEmpty(redisConn))
            {
                //初始化Redis
                csredis = new CSRedis.CSRedisClient(redisConn);
                RedisHelper.Initialization(csredis);

                //添加redis单例辅助帮助类
                services.AddSingleton(typeof(RedisProvider));
            }
            else
            {
                Console.WriteLine("redis配置字符串不存在,请在配置文件位置Connection:Redis配置");
            }
            return services;
        }
    }
}

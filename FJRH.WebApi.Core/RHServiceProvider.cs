using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public class RHServiceProvider
    {
        /// <summary>
        /// 提供Redis缓存服务
        /// </summary>
        public RHRedisHelper Redis;
        /// <summary>
        /// 提供MongoDB服务
        /// </summary>
        public MongoClient Mongo;

        public RHServiceProvider(IConfiguration configuration)
        {
            var mongoUrl = configuration["Connection:MongoDB:Conn"];
            var redisUrl = configuration["Connection:Redis"];
            MongoExt.DefaultDbName = configuration["Connection:MongoDB:DefaultDBName"];
            Mongo = new MongoClient(mongoUrl);

            var csredis = new CSRedis.CSRedisClient(redisUrl);
            RHRedisHelper.Initialization(csredis);
            Redis = new RHRedisHelper();
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="redisUrl"></param>
        /// <param name="mongoUrl"></param>
        public RHServiceProvider(string redisUrl , string mongoUrl)
        {
            //初始化Mongo
            Mongo = new MongoClient(mongoUrl);
            //初始化Redis
            var csredis = new CSRedis.CSRedisClient(redisUrl);
            RHRedisHelper.Initialization(csredis);
            Redis = new RHRedisHelper();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="redisUrl"></param>
        /// <param name="mongoUrl"></param>
        /// <returns></returns>
        public static RHServiceProvider GetInstance(string redisUrl, string mongoUrl)
        {
            return new RHServiceProvider(redisUrl, mongoUrl);
        }
    }
}

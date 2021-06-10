using FJRH.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.WebApi.FIlters;
using CSRedis;
using Test.WebApi.Models;
using System.Threading;

namespace Test.WebApi.Controllers
{
    [Produces("application/x-protobuf", "application/json", "application/xml")]
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly RedisProvider _redisProvider;
        private readonly MongoProvider _mongoProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RedisProvider redisProvider,MongoProvider mongoProvider)
        {
            _logger = logger;
            _redisProvider = redisProvider;
            _mongoProvider = mongoProvider;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(string id = "")
        {
            _logger.LogInformation("测试测试:"+ id);

            //测试redis读写
            RedisHelper.Set("test1", "dfas", 600);
            var ss = RedisHelper.Get("test1");

            //测试mongodb读写
            var operLogColl = _mongoProvider.Mongo.GenMongoCollection<OperLog>();
            var newid = GUIDHelper.CreateGUID().GetGUIDString();
            var data = new OperLog()
            {
                OldData = "asdf",
                NewData = "21012109370757596801402089595806",
                OLID = newid,
                OperID = newid,
                OperName = "18120846228",
                PrimaryKey = "",
                OperType = 1,
                TableName = "test",
                OperTime = DateTime.Now
            };
            //添加1条数据
            operLogColl.InsertOne(data);

            //测试redis分布式事务
            try
            {
                while (true)
                {
                    if (_redisProvider.SetNx("test", 10000))
                    {
                        _logger.LogInformation("开始事务");
                        Thread.Sleep(3000);
                        _logger.LogInformation("结束事务");
                        break;
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {
                _redisProvider.Remove("test");
            }

            //throw new Exception("测试错误异常");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

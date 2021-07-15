using FJRH.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.WebApi.FIlters;
using FreeRedis;
using Test.WebApi.Models;
using System.Threading;

namespace Test.WebApi.Controllers
{
    [Produces("application/x-protobuf", "application/json", "application/xml")]
    //[Route("[controller]")]
    [Route("/FJRH.RTM/AccountService")]
    [ApiController]
    public class AccountServiceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly RedisProvider _redisProvider;
        private readonly MongoProvider _mongoProvider;

        public AccountServiceController(ILogger<WeatherForecastController> logger, RedisProvider redisProvider,MongoProvider mongoProvider)
        {
            _logger = logger;
            _redisProvider = redisProvider;
            _mongoProvider = mongoProvider;
        }

        [HttpPost("Login")]
        public string Post([FromBody] LoginModel loginModel)
        {
            return "yes" + loginModel.name;
        }

        //[HttpPost("Login")]
        //public string Post(dynamic name)
        //{
        //    var name1 = name.name;
        //    return "yes" + name;
        //}

    }
    public class LoginModel
    {
        public string name { get; set; }
        public string pwd { get; set; }
        public string from { get; set; }
        public string code { get; set; }
        public int bindType { get; set; }
        public string bindID { get; set; }


    }
}

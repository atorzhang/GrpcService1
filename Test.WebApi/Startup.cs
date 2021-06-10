using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FJRH.WebApi.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NLog.Extensions.Logging;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Test.WebApi.FIlters;

namespace Test.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 设置跨域
            services.AddRHCors("CorsPolicy");

            //初始化redis
            services.AddRedis();

            //初始化MongoDb
            services.AddMongoDb();

            //允许异步流，不开启Protobuf序列化时候报错
            services.SetAllowSynchronousIO();

            //添加获取ip的中间件
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //添加MVC
            services.AddMvc(options =>
            {
                options.AddProtobuf();//添加对Protobuf返回类型的支持
                options.Filters.Add(typeof(ApiLogFilter));//添加日志过滤
            });

            //添加swaager
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test.WebApi", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // 设置允许部分域名跨域
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test.WebApi v1"));
            }

            app.UseRouting();   //使用默认路由

            app.UseAuthentication();    //认证服务

            app.UseAuthorization();    //使用授权服务

            
            app.UseMiddleware<RealIpMiddleware>();  //强化ip获取中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

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
            // 设置允许所有来源跨域
            #region 设置允许所有来源跨域
            var corLists = new List<string> {
                        "http://stutest.fjrh.cn",
                        "http://thctest.fjrh.cn",
                        "http://ccrtest.fjrh.cn",
                        "http://spacetest.fjrh.cn",
                        "http://rtmtest.fjrh.cn",
                        "http://stu.fjrh.cn",
                        "http://thc.fjrh.cn",
                        "http://ccr.fjrh.cn",
                        "http://space.fjrh.cn",
                        "http://rtm.fjrh.cn",
                        "https://stutest.fjrh.cn",
                        "https://thctest.fjrh.cn",
                        "https://ccrtest.fjrh.cn",
                        "https://spacetest.fjrh.cn",
                        "https://rtmtest.fjrh.cn",
                        "https://stu.fjrh.cn",
                        "https://thc.fjrh.cn",
                        "https://ccr.fjrh.cn",
                        "https://space.fjrh.cn",
                        "https://rtm.fjrh.cn",
                    };
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins(corLists.ToArray())
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    })
            ); 
            #endregion

            //允许异步流，不开启Protobuf序列化时候报错
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //初始化redis和monogo服务
            //var serviceProvider = RHServiceProvider.GetInstance(Configuration["Connection:Redis"], Configuration["Connection:MongoDB:Conn"]);
            //services.AddSingleton(serviceProvider);
            services.AddScoped(typeof(RHServiceProvider));
            
            //添加IdentityServer服务并使用中间件
            services.AddIdentityServer()                             //注册服务
                  .AddDeveloperSigningCredential()
                  .AddInMemoryApiResources(ApiConfig.GetResources()) //配置类定义的授权范围
                  .AddInMemoryClients(ApiConfig.GetClients())        //配置类定义的授权客户端
                  .AddInMemoryApiScopes(ApiConfig.ApiScopes)         //这个ApiScopes需要新加上，否则访问提示invalid_scope
                  .AddInMemoryIdentityResources(ApiConfig.GetIds())
                  .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();//注入自定义登录验证

            //添加获取ip的中间件
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //添加MVC
            services.AddMvc(options =>
            {
                options.AddProtobuf();
                options.Filters.Add(typeof(ApiLogFilter));
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
            // 设置允许所有来源跨域
            app.UseCors("CorsPolicy");

            //添加认证中间件
            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            //强化ip获取中间件
            app.UseMiddleware<RealIpMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

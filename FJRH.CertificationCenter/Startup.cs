using FJRH.CertificationCenter.Ext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter
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
            #region 增加跨域配置
            var corLists = Configuration.GetSection("Cors").Get<List<string>>();
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

            //添加MongoDB支持
            services.AddMongoDB();

            //添加数据库操作SqlSugar支持
            services.AddSqlSugar();

            //添加业务层服务注册
            services.AddBLLService();

            //添加IdentityServer服务并使用中间件
            services.AddRHIdentityServer();

            //添加api控制器
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 设置允许所有来源跨域
            app.UseCors("CorsPolicy");

            //添加认证中间件
            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

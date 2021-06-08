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
            // ��������������Դ����
            #region ��������������Դ����
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

            //�����첽����������Protobuf���л�ʱ�򱨴�
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //��ʼ��redis��monogo����
            //var serviceProvider = RHServiceProvider.GetInstance(Configuration["Connection:Redis"], Configuration["Connection:MongoDB:Conn"]);
            //services.AddSingleton(serviceProvider);
            services.AddScoped(typeof(RHServiceProvider));
            
            //���IdentityServer����ʹ���м��
            services.AddIdentityServer()                             //ע�����
                  .AddDeveloperSigningCredential()
                  .AddInMemoryApiResources(ApiConfig.GetResources()) //�����ඨ�����Ȩ��Χ
                  .AddInMemoryClients(ApiConfig.GetClients())        //�����ඨ�����Ȩ�ͻ���
                  .AddInMemoryApiScopes(ApiConfig.ApiScopes)         //���ApiScopes��Ҫ�¼��ϣ����������ʾinvalid_scope
                  .AddInMemoryIdentityResources(ApiConfig.GetIds())
                  .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();//ע���Զ����¼��֤

            //��ӻ�ȡip���м��
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //���MVC
            services.AddMvc(options =>
            {
                options.AddProtobuf();
                options.Filters.Add(typeof(ApiLogFilter));
            });

            //���swaager
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test.WebApi", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // ��������������Դ����
            app.UseCors("CorsPolicy");

            //�����֤�м��
            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            //ǿ��ip��ȡ�м��
            app.UseMiddleware<RealIpMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

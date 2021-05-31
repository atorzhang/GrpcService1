using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //添加Grpc服务
            services.AddGrpc();

            //添加GrpcHttpApi服务，启动了该服务配置下proto路由就可以http方式访问gRpc
            services.AddGrpcHttpApi();

            //添加gRPC Swagger支持，必须先开启GrpcHttpApi
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddGrpcSwagger();

            //添加AutoMapper注入
            services.AddAutoMapper(typeof(GrpcService1.AutoMapperProfiles).Assembly);

            //添加跨域
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));

        }


        #region 注入AutoFac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //业务逻辑层所在程序集命名空间
            Assembly service = Assembly.Load("FJRH.Service");
            //业务逻辑接口层所在命名空间
            Assembly iService = Assembly.Load("FJRH.IService");
            //自动注入
            builder.RegisterAssemblyTypes(service, iService)
                .Where(t => t.Name.EndsWith("Service") && !t.IsAbstract) ////类名以service结尾，且类型不能是抽象的　
                .InstancePerLifetimeScope() //生命周期，在相同作用域下获取到的服务实例是相同的
                .AsImplementedInterfaces()
                .PropertiesAutowired(); //属性注入
        } 
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //如果是开发和预生产环境，开启Swagger
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();

                #region 添加swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                }); 
                #endregion
            }

       

            app.UseRouting();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true }); // Must be added between UseRouting and UseEndpoints

            //app.UseGrpcWeb();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                //扩展添加gRPC服务注册
                endpoints.MapAllGrpcService();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}

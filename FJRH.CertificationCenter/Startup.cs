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
            #region ���ӿ�������
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

            //���MongoDB֧��
            services.AddMongoDB();

            //������ݿ����SqlSugar֧��
            services.AddSqlSugar();

            //���ҵ������ע��
            services.AddBLLService();

            //���IdentityServer����ʹ���м��
            services.AddRHIdentityServer();

            //���api������
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ��������������Դ����
            app.UseCors("CorsPolicy");

            //�����֤�м��
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

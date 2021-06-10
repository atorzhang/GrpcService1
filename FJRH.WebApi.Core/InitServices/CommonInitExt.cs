using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public static class CommonInitExt
    {
        /// <summary>
        /// 添加睿和相关跨域域名,appsetting.json的Cors下配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public static IServiceCollection AddRHCors(this IServiceCollection services,string policyName)
        {
            var config = ConfigHelper.DefaultConfiguration;
            var corLists = config.GetSection("Cors").Get<List<string>>();
            services.AddCors(options =>
                options.AddPolicy(policyName ,
                    builder =>
                    {
                        builder.WithOrigins(corLists.ToArray())
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    })
            );
            return services;
        }
    }
}

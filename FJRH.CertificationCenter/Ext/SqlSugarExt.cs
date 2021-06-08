using FJRH.CertificationCenter.BLL;
using FJRH.CertificationCenter.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter.Ext
{
    public static class SqlSugarExt
    {
        public static IServiceCollection AddBLLService(this IServiceCollection services)
        {
            //用户服务
            services.AddScoped(typeof(BLLUser));
            return services;
        }
    }
}

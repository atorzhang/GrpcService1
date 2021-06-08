using FJRH.CertificationCenter.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter.Ext
{
    public static class BLLExt
    {
        public static IServiceCollection AddSqlSugar(this IServiceCollection services)
        {
            services.AddScoped(typeof(SqlSugarContext));
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter.Ext
{
    public static class IdentityExt
    {
        public static void AddRHIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer()                                       //注册服务
                 .AddDeveloperSigningCredential()
                 .AddInMemoryApiResources(ApiConfig.GetResources())           //配置类定义的授权范围
                 .AddInMemoryClients(ApiConfig.GetClients())                  //配置类定义的授权客户端
                 .AddInMemoryApiScopes(ApiConfig.ApiScopes)                   //这个ApiScopes需要新加上，否则访问提示invalid_scope
                 .AddInMemoryIdentityResources(ApiConfig.GetIds())
                 .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();//注入自定义登录验证
        }
    }
}

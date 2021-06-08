using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;

namespace Test.WebApi
{
    public class ApiConfig
    {
        /// <summary>
        /// 这个方法是来规范tooken生成的规则和方法的。一般不进行设置，直接采用默认的即可。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIds()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        ///  定义资源范围   这里的资源（Resources）指的就是我们的API
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetResources()
        {
            return new[]
            {
                //配置资源的id,名称,Claim,Scopes
                new ApiResource("api1", "MY API",new List<string>(){ JwtClaimTypes.Role,JwtClaimTypes.Name})
                {
                    Scopes = { "api1" }
                }
            };
        }

        /// <summary>
        /// 定义访问的资源客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",//客户端的标识，要是惟一的
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//授权方式，这里采用的是密码认证模式
                    ClientSecrets = {
                        new Secret("mima123456".Sha256()),//客户端密码，进行了加密
                    },
                    AllowedScopes = {"api1" ,StandardScopes.OfflineAccess},//定义这个客户端可以访问的APi资源数组，上面只有一个api
                    RedirectUris = { "https://localhost:5000/signin-oidc" },//登录地址
                    PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },//退出跳转地址
                    AccessTokenLifetime = 3600,//token有效时间(秒)
                    AllowOfflineAccess = true,// 允许刷新refresh_token,
                }
            };
        }

        //下面是需要新加上方法，否则访问提示invalid_scope
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] { new ApiScope("api1") };




    }
}

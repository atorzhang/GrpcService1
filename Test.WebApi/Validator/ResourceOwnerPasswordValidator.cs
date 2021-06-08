using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Test.WebApi
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            switch (context.Request.ClientId)
            {
                case "client":
                    if (context.UserName == "test" && context.Password == "test")
                    {
                        context.Result = new GrantValidationResult(
                             subject: context.UserName,
                             authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                             claims: new List<Claim>() {
                         new Claim(JwtClaimTypes.Role,"test"), //多个逗号隔开
                         new Claim(JwtClaimTypes.Name, "测试用户"),
                             }
                         );
                    }
                    else
                    {
                        //验证失败
                        context.Result = new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "用户名或密码错误"
                            );
                    }
                    break;
                default:
                    context.Result = new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "客户端不正确"
                            );
                    break;
            }
            return Task.FromResult(0);
        }
    }
}

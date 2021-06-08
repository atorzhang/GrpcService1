using FJRH.CertificationCenter.BLL;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter
{
    /// <summary>
    /// 自定义验证用户名和密码
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly BLLUser _bLLUser;

        public ResourceOwnerPasswordValidator(BLLUser bLLUser)
        {
            _bLLUser = bLLUser;
        }

        /// <summary>
        /// 验证用户名和密码,成功返回token(包含权限id信息)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            switch (context.Request.ClientId)
            {
                case PlatformNames.RTM:
                    var lstRole = new List<string>();
                    var msg = _bLLUser.Login(context.UserName, context.Password, ref lstRole);
                    if (string.IsNullOrEmpty(msg))
                    {
                        context.Result = new GrantValidationResult(
                            subject: context.UserName,
                            authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                            claims: new List<Claim>() {
                                new Claim(JwtClaimTypes.Role, string.Join(",",lstRole)), //多个逗号隔开
                                new Claim(JwtClaimTypes.Name, context.UserName),
                            }
                        );
                    }
                    else
                    {
                        //验证失败
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,msg);
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

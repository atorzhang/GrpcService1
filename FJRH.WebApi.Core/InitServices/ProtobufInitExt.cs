using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public static class ProtobufInitExt
    {
        /// <summary>
        /// 添加对Accept=application/x-protobuf的响应,需要先调用下services.SetAllowSynchronousIO()支持异步流
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MvcOptions AddProtobuf(this MvcOptions options)
        {
            options.InputFormatters.Add(new ProtobufInputFormatter());
            options.OutputFormatters.Add(new ProtobufOutputFormatter());
            options.FormatterMappings.SetMediaTypeMappingForFormat("x-protobuf", MediaTypeHeaderValue.Parse("application/x-protobuf"));
            return options;
        }

        /// <summary>
        /// 允许异步流，不开启Protobuf序列化时候报错,要使用Protobuf需要配合开启
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection SetAllowSynchronousIO(this IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            return services;
        }
    }
}

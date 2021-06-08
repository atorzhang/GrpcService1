using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public static class ProtobufExt
    {
        /// <summary>
        /// 添加对Accept=application/x-protobuf的响应
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
    }
}

using Microsoft.AspNetCore.Mvc.Formatters;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    /// <summary>
    /// protobuf输入支持类
    /// </summary>
    public class ProtobufInputFormatter : InputFormatter
    {
        private static Lazy<RuntimeTypeModel> model = new Lazy<RuntimeTypeModel>(CreateTypeModel);

        public static RuntimeTypeModel Model
        {
            get { return model.Value; }
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var type = context.ModelType;
            var request = context.HttpContext.Request;
            if (request.Headers.ContainsKey("Accept"))
            {
                if(request.Headers["Accept"].FirstOrDefault().ToLower() == "application/x-protobuf")
                {
                    object result = Model.Deserialize(context.HttpContext.Request.Body, null, type);//默认UTF-8编码
                    return InputFormatterResult.SuccessAsync(result);
                }
            }
            //MediaTypeHeaderValue requestContentType = null;
            //MediaTypeHeaderValue.TryParse(request.ContentType, out requestContentType);
            StreamReader sr = new StreamReader(request.Body);
            string body =  sr.ReadToEnd();
            return InputFormatterResult.SuccessAsync(Newtonsoft.Json.JsonConvert.DeserializeObject<object>(body));
        }

        public override bool CanRead(InputFormatterContext context)
        {
            return true;
        }

        private static RuntimeTypeModel CreateTypeModel()
        {
            var typeModel = RuntimeTypeModel.Create();
            typeModel.UseImplicitZeroDefaults = false;
            typeModel.Add(typeof(DateTimeOffset), false).SetSurrogate(typeof(DateTimeOffsetSurrogate));
            return typeModel;
        }
    }
}

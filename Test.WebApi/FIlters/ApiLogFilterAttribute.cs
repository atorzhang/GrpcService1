using FJRH.WebApi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.WebApi.Models;

namespace Test.WebApi.FIlters
{
    public class ApiLogFilter : IActionFilter
    {
        private readonly RHServiceProvider _rHServiceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 注入Mongo
        /// </summary>
        public ApiLogFilter(RHServiceProvider rHServiceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _rHServiceProvider = rHServiceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Action执行之前 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Action执行之前,记录参数信息,写入Mongo数据库
            var actionName = context.ActionDescriptor.DisplayName;
            var requestLogColl = _rHServiceProvider.Mongo.GenMongoCollection<RequestLog>();
            RequestLog requestLog = new()
            {
                RequestLogID = Guid.NewGuid().ToString("n"),
                ActionName = actionName,
                RequestIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                RequestMethod = _httpContextAccessor.HttpContext.Request.Method,
                RequestHeaders = ToDic(_httpContextAccessor.HttpContext.Request.Headers),
                RequestTime = DateTime.Now,
                RequestLogTime = DateTime.Now,
                RequestParameters = context.ActionArguments,
                RequestURL = _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.Path,
                //Certificate = Newtonsoft.Json.JsonConvert.SerializeObject(_httpContextAccessor.HttpContext.User.Claims),
            };
            requestLogColl.InsertOne(requestLog);
            //把当前写入的Mongo的guid写入http管道,后面在根据这id更新Mongo数据
            context.HttpContext.Items.Add("RequestLogID", requestLog.RequestLogID);
        }

        /// <summary>
        /// Action执行之后,回写日志
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var requestLogID = new object();
            context.HttpContext.Items.TryGetValue("RequestLogID", out requestLogID);
            var updateLog = new RequestLog
            {
                RequestLogTime = DateTime.Now,
                ResponseHeaders = ToDic(context.HttpContext.Response.Headers)
            };
            if (context.Result is ObjectResult)
            {
                updateLog.ResponseContent = ((ObjectResult)context.Result).Value;
            }
            var requestLogColl = _rHServiceProvider.Mongo.GenMongoCollection<RequestLog>();
            requestLogColl.UpdateByEntity(it => it.RequestLogID == requestLogID.ToString(), updateLog);
        }

        #region 内部方法
        private static IDictionary<string, string> ToDic(IHeaderDictionary dict)
        {
            IDictionary<string, string> mongoDic = new Dictionary<string, string>();
            foreach (var entry in dict)
            {
                mongoDic.Add(entry.Key, entry.Value);
            }
            return mongoDic;
        } 
        #endregion

    }
}

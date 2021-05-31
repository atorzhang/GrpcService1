using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService1
{
    public static class MapGrpcExt
    {
        /// <summary>
        /// 注册本项目内所有Grpc服务
        /// </summary>
        /// <param name="endpoints"></param>
        public static void MapAllGrpcService(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGrpcService<Greeter1Service>().EnableGrpcWeb().RequireCors("AllowAll"); 
            endpoints.MapGrpcService<HelloworldService>().EnableGrpcWeb().RequireCors("AllowAll"); ;
        }
    }
}

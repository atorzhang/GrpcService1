using AutoMapper;
using FJRH.Entity;
using FJRH.IService;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class Greeter1Service : Greeter1.Greeter1Base
    {
        private readonly ILogger<Greeter1Service> _logger;
        private readonly IMapper _mapper;
        //注入业务层服务
        private readonly IUserService _userService;
        public Greeter1Service(ILogger<Greeter1Service> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        public override Task<HelloReply1> SayHello1(HelloRequest1 request, ServerCallContext context)
        {
            var message = _userService.GetUserName(_mapper.Map<HelloRequest1Dto>(request));
            return Task.FromResult(new HelloReply1
            {
                Message = "Hello1 " + message,
                Status = "正常1"
            });
        }

        public override Task<HelloReply1> SayHelloPost(HelloRequest1 request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply1
            {
                Message = $"HelloPost {request.Name} {request.Code}",
                Status = "Post正常"
            });
        }
    }
}

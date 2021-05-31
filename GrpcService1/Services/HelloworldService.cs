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
    public class HelloworldService : Greeter.GreeterBase
    {
        private readonly ILogger<Greeter1Service> _logger;
        private readonly IMapper _mapper;
        //注入业务层服务
        private readonly IUserService _userService;
        public HelloworldService(ILogger<Greeter1Service> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            //var message = _userService.GetUserName(_mapper.Map<HelloRequest1Dto>(request));
            var message = _userService.GetUserName(new HelloRequest1Dto {Name = request.Name,Code="test" });
            return Task.FromResult(new HelloReply
            {
                Message = "SayHello " + message,
            });
        }

        public override Task SayRepeatHello(RepeatHelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"SayRepeatHello {request.Name} {request.Count}",
            });
        }
    }
}

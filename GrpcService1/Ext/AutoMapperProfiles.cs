using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FJRH.Entity;

namespace GrpcService1
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        //配置AutoMapper映射实体
        public AutoMapperProfiles()
        {
            CreateMap<HelloRequest1, HelloRequest1Dto>().ReverseMap();
        }
    }
}

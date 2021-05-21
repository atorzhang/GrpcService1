using FJRH.Entity;
using FJRH.IService;
using System;

namespace FJRH.Service
{
    public class UserService : IUserService
    {
        public string GetUserName(HelloRequest1Dto hilloRequest1Dto)
        {
            return $"[数据库处理Code:{hilloRequest1Dto.Code},Name:{hilloRequest1Dto.Name}]";
        }
    }
}

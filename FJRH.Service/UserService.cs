using FJRH.Entity;
using FJRH.IService;
using System;

namespace FJRH.Service
{
    public class UserService : IUserService
    {
        public string GetUserName(HelloRequest1Dto hilloRequest1Dto)
        {
            var length = 100000000m;
            var j = 1m;
            for (int i = 1; i < length; i++)
            {
                j += i;
            }
            return $"[{j}数据库处理Code:{hilloRequest1Dto.Code},Name:{hilloRequest1Dto.Name}]";
        }
    }
}

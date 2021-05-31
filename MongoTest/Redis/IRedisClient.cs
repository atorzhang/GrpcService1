using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTest.Redis
{
    public interface IRedisClient
    {
        string Get(string key);
        Task<string> GetAsync(string key);
        void Set(string key, object t, int expiresSec = 0);
        Task SetAsync(string key, object t, int expiresSec = 0);
        T Get<T>(string key) where T : new();
        Task<T> GetAsync<T>(string key) where T : new();
    }
}

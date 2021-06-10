using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public class RedisProvider 
    {
        /// <summary>
        /// 设置分布式锁(请使用try,finally去使用})
        /// </summary>
        /// <param name="key">锁key</param>
        /// <param name="expireMS">过期时间(毫秒)默认时间3秒</param>
        /// <returns></returns>
        public  bool SetNx(string key, double expireMS = 3000)
        {
            var currentTime = DateTime.UtcNow.ToUnixTimestampByMilliseconds();
            if (RedisHelper.Instance.SetNx(key, currentTime + (long)expireMS))
            {
                if (expireMS > 0)
                    RedisHelper.Instance.Expire(key, TimeSpan.FromMilliseconds(expireMS));
                return true;
            }
            else
            {
                //未获取到锁，继续判断，判断时间戳看看是否可以重置并获取锁
                var lockValue = RedisHelper.Instance.Get(key);
                var time = DateTime.Now.ToUnixTimestampByMilliseconds();
                //解决死锁问题,设置了锁但在设置过期时间(Instance.Expire)上没执行导致的死锁
                if (!string.IsNullOrEmpty(lockValue) && time > Convert.ToInt64(lockValue))
                {
                    Console.WriteLine("可能死锁发生了");
                    //再次用当前时间戳getset
                    //返回固定key的旧值，旧值判断是否可以获取锁
                    var getsetResult = RedisHelper.Instance.GetSet("lockkey", time);
                    if (getsetResult == null || (getsetResult != null && getsetResult == lockValue))
                    {
                        Console.WriteLine("重新获取到Redis锁了");
                        //真正获取到锁,添加锁过期时间
                        RedisHelper.Instance.Expire(key, TimeSpan.FromMilliseconds(expireMS));
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除锁key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return RedisHelper.Instance.Del(key) > 0;
        }
    }
}

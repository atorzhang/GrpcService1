using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public class MongoProvider
    {
        private MongoClient _mongo;
        public MongoClient Mongo
        {
            get
            {
                return _mongo;
            }
        }

        public MongoProvider(IConfiguration configuration)
        {
            string connStr = configuration["Connection:MongoDB:Conn"];
            string defualtDbName = configuration["Connection:MongoDB:DefaultDBName"];
            if (!string.IsNullOrEmpty(connStr))
            {
                _mongo = connStr.GenMongoClient(defualtDbName);
            }
            else
            {
                throw new Exception("未配置MongoDB连接字符串,请在appsettings.json文件以下位置配置连接字符串(Connection:MongoDB:Conn)以及默认连接库名(Connection:MongoDB:DefaultDBName)");
            }
        }
    }
}

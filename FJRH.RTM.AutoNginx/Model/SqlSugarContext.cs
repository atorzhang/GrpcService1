using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.RTM.AutoNginx
{
    public class SqlSugarContext
    {
        public IConfiguration Configuration;
        public ISqlSugarClient Db { get; set; }
        //通过构造函数注入Configuration对象
        public SqlSugarContext(IConfiguration configuration)
        {
            Configuration = configuration;
            var lstDbModel = configuration.GetSection("Connection:Sql").Get<List<DBModel>>();
            var lstConnectionConfig = new List<ConnectionConfig>();
            for (int i = 0; i < lstDbModel.Count; i++)
            {
                lstConnectionConfig.Add(new ConnectionConfig()
                {
                    ConnectionString = lstDbModel[i].Conn,//连接符字串
                    DbType = (DbType)Enum.Parse(typeof(DbType), lstDbModel[i].DBType),
                    IsAutoCloseConnection = true,
                    ConfigId = i.ToString(),
                });
            }
            Db = new SqlSugarClient(lstConnectionConfig);
#if DEBUG
            //添加Sql打印事件，开发中可查看生成的sql
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);
            };
#endif
        }
    }
}

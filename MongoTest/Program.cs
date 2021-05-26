using FJRH.Mongo.Ext;
using MongoDB.Driver;
using MongoTest.entity;
using System;

namespace MongoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //与Mongodb建立连接,使用helper帮助类
            #region 操作Sms表
            //var sms = MongodbHelper.GetInstance().GetCollection<Sms>("Sms", "FJRH_RTM_LOG");
            //var id = Guid.NewGuid().ToString("n");
            //var data = new Sms()
            //{
            //    id = id,
            //    SMSID = "21012109370757596801402089595806",
            //    Content = "",
            //    Platform = 2,
            //    Target = "18120846228",
            //    Result = new { pd = "1asdf1" },
            //    SendTime = DateTime.Now
            //};
            ////添加1条数据
            //sms.Add(data);
            ////查询1条数据
            //var filedata = sms.QueryOne(o => o.SMSID.Contains("59580"));

            ////查询数据集合
            //var filedataList = sms.QueryList(o => o.SMSID.Contains("59580"));

            ////分页查询数据
            //var filePageData = sms.QueryPage(o => o.SMSID.Contains("59580"), 1, 1);

            ////更新数据
            //sms.Update(o => o.id == "dfas", new Sms { SMSID = "fadsfewa", SendTime = DateTime.Now });

            ////删除数据
            //sms.Detele(o => o.id == id);
            #endregion

            //从配置中获取Mongodb连接字符串
            string connStr = "mongodb://sa:123qwe!%40#@192.168.0.110";
            //使用初始化方式Mongo扩展单例Client操作
            var cilent = connStr.InitSingleClient("FJRH_RTM_LOG");
            var operLogColl = cilent.GenMongoCollection<OperLog>();

            var id = Guid.NewGuid().ToString("n");
            var data = new OperLog()
            {
                OldData = "asdf",
                NewData = "21012109370757596801402089595806",
                OLID = id,
                OperID = id,
                OperName = "18120846228",
                PrimaryKey = "",
                OperType = 1,
                TableName = "test",
                OperTime = DateTime.Now
            };
            //添加1条数据
            operLogColl.InsertOne(data);
            //查询1条数据
            var filedata = operLogColl.FirstOrDefault(o => o.OldData.Contains("asdf"));

            //查询数据集合
            var filedataList = operLogColl.QueryList(o => o.OldData.Contains("asdf"));

            //分页查询数据
            var filePageData = operLogColl.QueryPage(o => o.OperName.Contains("181"), 1, 1);

            //更新数据
            operLogColl.UpdateByEntity(o => o.OldData == "asdf", new OperLog { NewData = "qaz", OperTime = DateTime.Now });

            //删除数据
            operLogColl.DeleteMany(o => o.OLID == id);

            Console.ReadLine();
        }
    }
}

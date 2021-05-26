using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace MongoTest
{
    /// <summary>
    /// MongoDB操作帮助类
    /// </summary>
    public class MongodbHelper
    {
        public MongoClient Client { get; set; }
        public static string MongodbUrl = "mongodb://sa:123qwe!%40#@192.168.0.110";

        #region 构造方法
        public MongodbHelper()
        {
            Client = new MongoClient(MongodbUrl);
        }
        public MongodbHelper(string url)
        {
            Client = new MongoClient(url);
        }
        public static MongodbHelper GetInstance(string url)
        {
            MongodbUrl = url;
            var instance = new MongodbHelper(MongodbUrl);
            return instance;
        }
        public static MongodbHelper GetInstance()
        {
            var instance = new MongodbHelper(MongodbUrl);
            return instance;
        }
        #endregion

        /// <summary>
        /// 获取 Collection 信息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="collName">Collection 名称</param>
        /// <param name="dbName">DBase名称</param>
        /// <returns></returns>
        public MyMongoCollection<T> GetCollection<T>(string collName, string dbName)
        {
            MyMongoCollection<T> mycollection = new MyMongoCollection<T>();
            IMongoDatabase database = Client.GetDatabase(dbName);
            IMongoCollection<T> collection = database.GetCollection<T>(collName);
            mycollection.MongoCollection = collection;
            return mycollection;
        }
    }

    /// <summary>
    /// 自定义操作集合类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyMongoCollection<T>
    {
        public IMongoCollection<T> MongoCollection { get; set; }

        /// <summary>
        /// 查询数据 延迟加载 后期可以使用Linq  Lambda处理数据
        /// </summary>
        /// <returns></returns>
        public IMongoQueryable<T> AsQueryable()
        {
            return MongoCollection.AsQueryable<T>();
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public List<T> QueryList(Expression<Func<T, bool>> expression)
        {
            var list = MongoCollection.AsQueryable().Where(expression);
            return list.ToList<T>();
        }

        /// <summary>
        /// [异步]查询所有数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryListAsync(Expression<Func<T, bool>> expression)
        {
            var list = MongoCollection.AsQueryable().Where(expression);
            return await list.ToListAsync<T>();
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T QueryOne(Expression<Func<T, bool>> expression)
        {
            var data = MongoCollection.AsQueryable().FirstOrDefault(expression);
            return data;
        }

        /// <summary>
        /// [异步]查询一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T> QueryOneAsync(Expression<Func<T, bool>> expression)
        {
            var data = await MongoCollection.AsQueryable().FirstOrDefaultAsync(expression);
            return data;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expressio">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public PageInfo<T> QueryPage(Expression<Func<T, bool>> expressio, int pageIndex,int pageSize)
        {
            var pageInfo = new PageInfo<T>
            {
                Totoal = MongoCollection.AsQueryable<T>().Count()
            };
            List<T> list;
            if (expressio != null)
            {
                list = MongoCollection.AsQueryable<T>().Where(expressio).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
            else
            {
                list = MongoCollection.AsQueryable<T>().Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
            pageInfo.Data = list;
            return pageInfo;
        }

        /// <summary>
        /// [异步]分页查询
        /// </summary>
        /// <param name="expressio"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public async Task<PageInfo<T>> QueryPageAsync(Expression<Func<T, bool>> expressio, int pageIndex, int pageSize)
        {
            var pageInfo = new PageInfo<T>
            {
                Totoal = await MongoCollection.AsQueryable<T>().CountAsync()
            };
            List<T> list;
            if (expressio != null)
            {
                list = await MongoCollection.AsQueryable<T>().Where(expressio).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            }
            else
            {
                list = await MongoCollection.AsQueryable<T>().Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            }
            pageInfo.Data = list;
            return pageInfo;
        }

        /// <summary>
        /// 新增一条数据(文档)
        /// </summary>
        /// <param name="ts"></param>
        public void Add(T ts)
        {
            MongoCollection.InsertOne(ts);
        }

        /// <summary>
        /// [异步]新增一条数据(文档)
        /// </summary>
        /// <param name="ts"></param>
        public async Task AddAsync(T ts)
        {
            await MongoCollection.InsertOneAsync(ts);
        }

        /// <summary>
        /// 批量新增多个文档
        /// </summary>
        /// <param name="ts"></param>
        public void Add(List<T> ts)
        {
            MongoCollection.InsertMany(ts);
        }

        /// <summary>
        /// [异步]批量新增多个文档
        /// </summary>
        /// <param name="ts"></param>
        public async Task AddAsync(List<T> ts)
        {
            await MongoCollection.InsertManyAsync(ts);
        }

        /// <summary>
        /// 更新文档  不存在就新增
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        public void Update(Expression<Func<T, bool>> filter, T t)
        {
            var newData = BuildQueryOption(t);
            UpdateResult result = MongoCollection.UpdateMany(filter, newData, new UpdateOptions { IsUpsert = true });
        }

        /// <summary>
        /// [异步]更新文档  不存在就新增
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        public async Task UpdateAsync(Expression<Func<T, bool>> filter, T t)
        {
            var newData = BuildQueryOption(t);
            UpdateResult result = await MongoCollection.UpdateManyAsync(filter, newData, new UpdateOptions { IsUpsert = true });
        }

        /// <summary>
        /// 删除文档
        /// </summary>
        /// <param name="predicate"></param>
        public long Detele(Expression<Func<T, bool>> predicate)
        {
            var result = MongoCollection.DeleteMany(predicate);
            return result.DeletedCount;
        }

        /// <summary>
        /// [异步]删除文档
        /// </summary>
        /// <param name="predicate"></param>
        public async Task<long> DeteleAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await MongoCollection.DeleteManyAsync(predicate);
            return result.DeletedCount;
        }

        /// <summary>
        /// 利用反射创建 更新字段 (这里没有处理空)
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private UpdateDefinition<T> BuildQueryOption(T doc)
        {
            var update = Builders<T>.Update;
            var updates = new List<UpdateDefinition<T>>();

            var t = doc.GetType();
            var proper = t.GetProperties();
            foreach (PropertyInfo info in proper)
            {
                var value = info.GetValue(doc);
                if (value != null && info.Name.ToLower() != "id")
                {
                    updates.Add(update.Set(info.Name, info.GetValue(doc)));
                }
            }
            return update.Combine(updates);
        }
    }

    /// <summary>
    /// 分页信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public long Totoal { get; set; }
        /// <summary>
        /// 返回分页数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}
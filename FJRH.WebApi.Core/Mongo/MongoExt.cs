using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    /// <summary>
    /// 分页信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageInfo<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public long Total { get; set; }
        /// <summary>
        /// 返回分页数据
        /// </summary>
        public List<T> Data { get; set; }
    }
    /// <summary>
    /// MongoDb扩展
    /// </summary>
    public static class MongoExt
    {
        /// <summary>
        /// 默认连接数据库名
        /// </summary>
        public static string DefaultDbName { get; set; }

        /// <summary>
        /// 生成MongoClient
        /// </summary>
        /// <param name="connStr">链接字符串，mongodb://用户名:密码@ip:端口</param>
        /// <returns></returns>
        public static MongoClient GenMongoClient(this string connStr,string defaultDbName="")
        {
            var newClient = new MongoClient(connStr);
            if (!string.IsNullOrEmpty(defaultDbName))
            {
                DefaultDbName = defaultDbName;
            }
            return newClient;
        }

        /// <summary>
        /// 生成MongoCollection集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="dbName">库名</param>
        /// <param name="collName">集合名</param>
        /// <returns></returns>
        public static IMongoCollection<T> GenMongoCollection<T>(this MongoClient client,string dbName = "", string collName = "")
        {
            IMongoDatabase database = client.GetDatabase(string.IsNullOrWhiteSpace(dbName) ? DefaultDbName : dbName);
            IMongoCollection<T> collection = database.GetCollection<T>(string.IsNullOrEmpty(collName) ? typeof(T).Name : collName);
            return collection;
        }

        /// <summary>
        /// 根据条件获取实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mongoCollection"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static List<T> QueryList<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return mongoCollection.AsQueryable().ToList();
            }
            return mongoCollection.AsQueryable().Where(expression).ToList();
        }

        /// <summary>
        /// [异步]根据条件获取实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mongoCollection"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static async Task<List<T>> QueryListAsync<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return await mongoCollection.AsQueryable().ToListAsync();
            }
            return await mongoCollection.AsQueryable().Where(expression).ToListAsync();
        }

        /// <summary>
        /// 判断条件数据是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mongoCollection"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool Any<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression)
        {
            return mongoCollection.AsQueryable().Any(expression);
        }

        /// <summary>
        /// 查找第一个满足条件的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mongoCollection"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression)
        {
            return mongoCollection.AsQueryable().FirstOrDefault(expression);
        }

        /// <summary>
        /// 查找第一个满足条件的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mongoCollection"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static async Task<T> FirstOrDefaultAsync<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression)
        {
            return await mongoCollection.AsQueryable().FirstOrDefaultAsync(expression);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static PageInfo<T> QueryPage<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression, int pageIndex, int pageSize)
        {
            var pageInfo = new PageInfo<T>
            {
                Total = mongoCollection.AsQueryable<T>().Count()
            };
            List<T> list;
            if (expression != null)
            {
                list = mongoCollection.AsQueryable<T>().Where(expression).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
            else
            {
                list = mongoCollection.AsQueryable<T>().Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
            pageInfo.Data = list;
            return pageInfo;
        }

        /// <summary>
        /// [异步]分页查询
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static async Task<PageInfo<T>> QueryPageAsync<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression, int pageIndex, int pageSize)
        {
            var pageInfo = new PageInfo<T>
            {
                Total = await mongoCollection.AsQueryable<T>().CountAsync()
            };
            List<T> list;
            if (expression != null)
            {
                list = await mongoCollection.AsQueryable<T>().Where(expression).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            }
            else
            {
                list = await mongoCollection.AsQueryable<T>().Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            }
            pageInfo.Data = list;
            return pageInfo;
        }

        /// <summary>
        /// 更新文档 不存在就新增[默认]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mongoCollection"></param>
        /// <param name="expression"></param>
        /// <param name="t"></param>
        /// <param name="isUpsert">是否不存在就新增</param>
        /// <returns></returns>
        public static UpdateResult UpdateByEntity<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression, T t, bool isUpsert = true)
        {
            var newData = BuildQueryOption(t);
            UpdateResult result = mongoCollection.UpdateMany(expression, newData, new UpdateOptions { IsUpsert = isUpsert });
            return result;
        }

        /// <summary>
        /// [异步]更新文档 不存在就新增[默认]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mongoCollection"></param>
        /// <param name="expression">条件表达式</param>
        /// <param name="t">新数据</param>
        /// <param name="isUpsert">是否不存在就新增</param>
        /// <returns></returns>
        public static async Task<UpdateResult> UpdateByEntityAsync<T>(this IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> expression, T t, bool isUpsert = true)
        {
            var newData = BuildQueryOption(t);
            UpdateResult result = await mongoCollection.UpdateManyAsync(expression, newData, new UpdateOptions { IsUpsert = isUpsert });
            return result;
        }


        /// <summary>
        /// 利用反射创建 更新字段 (这里没有处理空)
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static UpdateDefinition<T> BuildQueryOption<T>(T doc)
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
                    if(info.PropertyType == typeof(DateTime))
                    {
                        if((DateTime)value == DateTime.MinValue)
                        {
                            continue;//时间为最小值不更新
                        }
                    }
                    updates.Add(update.Set(info.Name, info.GetValue(doc)));
                }
            }
            return update.Combine(updates);
        }
    }
}

using FJRH.CertificationCenter.DBEntity;
using FJRH.CertificationCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter.BLL
{
    public class BLLUser
    {
        private SqlSugarContext _sqlSugarContext;
        public BLLUser(SqlSugarContext sqlSugarContext)
        {
            _sqlSugarContext = sqlSugarContext;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lstRole"></param>
        /// <returns></returns>
        public string Login(string username,string password,ref List<string> lstRole)
        {
            var userModel = _sqlSugarContext.Db.Queryable<Account>().First(o => o.AccountName == username && o.AccountPwd == password && o.AccountEstate == 1);
            AccountLoginLog accountLoginLog = new()
            {
                ALLId = GUIDHelper.CreateGUID().GetGUIDString(),
                LoginName = username,
                LoginResult = 1,
                LoginParams = Newtonsoft.Json.JsonConvert.SerializeObject(new { password , username }),
                LoginTime = DateTime.Now,
                LoginIP = ""
            };
            if (userModel == null)
            {
                accountLoginLog.LoginResult = 0;
                accountLoginLog.LoginResultDesc = "用户名或密码错误";
                _sqlSugarContext.Db.Insertable(accountLoginLog).ExecuteCommand();
                return accountLoginLog.LoginResultDesc;
            }
            //写入登录数据处理
            userModel.AccountLoginCount += 1;
            userModel.AccountLastVisist = DateTime.Now;
            accountLoginLog.AccountID = userModel.AccountID;
            _sqlSugarContext.Db.Updateable(userModel).ExecuteCommand();

            //获取用户权限
            lstRole = _sqlSugarContext.Db.Queryable<AccountRole>().Where(o => o.RTMAID == userModel.AccountID).Select(o => o.RoleID).ToList();

            //登录日志写入
            _sqlSugarContext.Db.Insertable(accountLoginLog).ExecuteCommand();
            return "";
        }
    }
}

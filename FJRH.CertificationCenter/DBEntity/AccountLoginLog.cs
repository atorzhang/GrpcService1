using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FJRH.CertificationCenter.DBEntity
{
    ///<summary>
    ///账号登入日志
    ///</summary>
    [SugarTable("AccountLoginLog")]
    public partial class AccountLoginLog
    {
           public AccountLoginLog(){


           }
           /// <summary>
           /// Desc:登录编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ALLId {get;set;}

           /// <summary>
           /// Desc:用户编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccountID {get;set;}

           /// <summary>
           /// Desc:登录名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LoginName {get;set;}

           /// <summary>
           /// Desc:登录IP
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LoginIP {get;set;}

           /// <summary>
           /// Desc:登录方式  Loginway枚举
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int LoginWay {get;set;}

           /// <summary>
           /// Desc:登录时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime LoginTime {get;set;}

           /// <summary>
           /// Desc:登录结果 0:失败,1:成功
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int LoginResult {get;set;}

           /// <summary>
           /// Desc:UserAgent
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserAgent {get;set;}

           /// <summary>
           /// Desc:Authorization
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Authorization {get;set;}

           /// <summary>
           /// Desc:登录结果描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LoginResultDesc {get;set;}

           /// <summary>
           /// Desc:登录参数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public object LoginParams {get;set;}

           /// <summary>
           /// Desc:登录地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LoginLocation {get;set;}

    }
}

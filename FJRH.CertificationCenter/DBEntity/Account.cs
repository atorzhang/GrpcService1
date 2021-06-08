using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FJRH.CertificationCenter.DBEntity
{
    ///<summary>
    ///用户
    ///</summary>
    [SugarTable("Account")]
    public partial class Account
    {
           public Account(){


           }
           /// <summary>
           /// Desc:用户编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string AccountID {get;set;}

           /// <summary>
           /// Desc:用户名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AccountName {get;set;}

           /// <summary>
           /// Desc:Mail邮箱
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccountMail {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccountPwd {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccountPhone {get;set;}

           /// <summary>
           /// Desc:状态（-1：删除，0：已禁用/未激活，1：已启用）	             只做标记删除 不做真实删除
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int AccountEstate {get;set;}

           /// <summary>
           /// Desc:第一次访问时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? AccountFirstVisit {get;set;}

           /// <summary>
           /// Desc:上一次访问时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? AccountPreviousVisit {get;set;}

           /// <summary>
           /// Desc:最后访问时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? AccountLastVisist {get;set;}

           /// <summary>
           /// Desc:登录次数
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? AccountLoginCount {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccountDescription {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:登录地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccountLastLoginLocation {get;set;}

           /// <summary>
           /// Desc:登录IP
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string AccountLastLoginIP {get;set;}

    }
}

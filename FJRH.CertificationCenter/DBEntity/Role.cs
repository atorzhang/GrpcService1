using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FJRH.CertificationCenter.DBEntity
{
    ///<summary>
    ///角色
    ///</summary>
    [SugarTable("Role")]
    public partial class Role
    {
           public Role(){


           }
           /// <summary>
           /// Desc:角色编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string RoleID {get;set;}

           /// <summary>
           /// Desc:角色名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RoleName {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RoleDescription {get;set;}

           /// <summary>
           /// Desc:状态（0：已禁用/未激活，1：已启用）
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int RoleEstate {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime RoleCreateDate {get;set;}

           /// <summary>
           /// Desc:修改日期
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime RoleUpdateDate {get;set;}

    }
}

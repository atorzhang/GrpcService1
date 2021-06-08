using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FJRH.CertificationCenter.DBEntity
{
    ///<summary>
    ///角色模块权限
    ///</summary>
    [SugarTable("RolePermission")]
    public partial class RolePermission
    {
           public RolePermission(){


           }
           /// <summary>
           /// Desc:角色编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string RoleID {get;set;}

           /// <summary>
           /// Desc:模块编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ModuleID {get;set;}

           /// <summary>
           /// Desc:权限值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Permission {get;set;}

    }
}

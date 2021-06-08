using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FJRH.CertificationCenter.DBEntity
{
    ///<summary>
    ///用户角色关联
    ///</summary>
    [SugarTable("AccountRole")]
    public partial class AccountRole
    {
           public AccountRole(){


           }
           /// <summary>
           /// Desc:用户编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string RTMAID {get;set;}

           /// <summary>
           /// Desc:角色编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string RoleID {get;set;}

    }
}

using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FJRH.RTM.AutoNginx
{
    ///<summary>
    ///组织机构
    ///</summary>
    [SugarTable("ORG")]
    public partial class ORG
    {
           public ORG(){


           }
           /// <summary>
           /// Desc:机构编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ORGID {get;set;}

           /// <summary>
           /// Desc:机构名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ORGName {get;set;}

           /// <summary>
           /// Desc:机构代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ORGCode {get;set;}

           /// <summary>
           /// Desc:机构类型	             1: 学校	             2: 培训机构
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int ORGType {get;set;}

           /// <summary>
           /// Desc:访问秘钥
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AccessKey {get;set;}

           /// <summary>
           /// Desc:状态（-1：删除，0：禁用，1：正常）
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int ORGStatus {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:拼音
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ORGPY {get;set;}

           /// <summary>
           /// Desc:拼音首字母
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ORGPYInitial {get;set;}

    }
}

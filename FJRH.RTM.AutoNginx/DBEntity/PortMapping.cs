using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace FJRH.RTM.AutoNginx
{
    ///<summary>
    ///端口映射
    ///</summary>
    [SugarTable("PortMapping")]
    public partial class PortMapping
    {
           public PortMapping(){


           }
           /// <summary>
           /// Desc:映射编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string PMID {get;set;}

           /// <summary>
           /// Desc:机构编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ORGID {get;set;}

           /// <summary>
           /// Desc:端口代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PortCode {get;set;}

           /// <summary>
           /// Desc:端口
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Port {get;set;}

           /// <summary>
           /// Desc:内网IP
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IntranetIP {get;set;}

           /// <summary>
           /// Desc:学校提供的外网映射IP
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SchExMapIP {get;set;}

           /// <summary>
           /// Desc:学校提供的外网映射端口
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SchExMapPort {get;set;}

           /// <summary>
           /// Desc:代理类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AgentType {get;set;}

           /// <summary>
           /// Desc:内网端口
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? IntranetPort {get;set;}

    }
}

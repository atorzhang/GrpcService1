using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Test.WebApi.Models
{
    [ProtoContract]
    [Serializable]
    public class Module 
    {

        #region 私有变量
        /// <summary>
        /// 模块编号
        /// </summary>
        private string _ModuleID;
        /// <summary>
        /// 模块父编号
        /// </summary>
        private string _ModuleParentID;
        /// <summary>
        /// 模块名称
        /// </summary>
        private string _ModuleName;
        /// <summary>
        /// 模块代码
        /// </summary>
        private string _ModuleCode;
        /// <summary>
        /// 模块路径
        /// </summary>
        private string _ModuleURL;
        /// <summary>
        /// 状态
        /// </summary>
        private int _ModuleEstate;
        /// <summary>
        /// 备注
        /// </summary>
        private string _ModuleDescription;
        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _ModuleCreateDate;
        /// <summary>
        /// 修改日期
        /// </summary>
        private DateTime _ModuleUpdateDate;
        /// <summary>
        /// 参数1
        /// </summary>
        private string _ModuleParameter1;
        /// <summary>
        /// 参数2
        /// </summary>
        private string _ModuleParameter2;
        /// <summary>
        /// 参数3
        /// </summary>
        private string _ModuleParameter3;
        /// <summary>
        /// 参数4
        /// </summary>
        private string _ModuleParameter4;
        /// <summary>
        /// 参数5
        /// </summary>
        private string _ModuleParameter5;
        /// <summary>
        /// 模块操作项
        /// </summary>
        private int _ModuleOper;

        #endregion

        #region 公共属性
        /// <summary>
        /// 模块编号
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public string ModuleID
        {
            get => _ModuleID;
            set
            {
                _ModuleID = value;
            }
        }
        /// <summary>
        /// 模块父编号
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public string ModuleParentID
        {
            get => _ModuleParentID;
            set
            {
                _ModuleParentID = value;
                
            }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public string ModuleName
        {
            get => _ModuleName;
            set
            {
                _ModuleName = value;
                
            }
        }
        /// <summary>
        /// 模块代码
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public string ModuleCode
        {
            get => _ModuleCode;
            set
            {
                _ModuleCode = value;
                
            }
        }
        /// <summary>
        /// 模块路径
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public string ModuleURL
        {
            get => _ModuleURL;
            set
            {
                _ModuleURL = value;
                
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public int ModuleEstate
        {
            get => _ModuleEstate;
            set
            {
                _ModuleEstate = value;
                
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        public string ModuleDescription
        {
            get => _ModuleDescription;
            set
            {
                _ModuleDescription = value;
                
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        [ProtoMember(8)]
        public DateTime ModuleCreateDate
        {
            get => _ModuleCreateDate;
            set
            {
                _ModuleCreateDate = value;
                
            }
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public DateTime ModuleUpdateDate
        {
            get => _ModuleUpdateDate;
            set
            {
                _ModuleUpdateDate = value;
                
            }
        }
        /// <summary>
        /// 参数1
        /// </summary>
        [DataMember]
        [ProtoMember(10)]
        public string ModuleParameter1
        {
            get => _ModuleParameter1;
            set
            {
                _ModuleParameter1 = value;
                
            }
        }
        /// <summary>
        /// 参数2
        /// </summary>
        [DataMember]
        [ProtoMember(11)]
        public string ModuleParameter2
        {
            get => _ModuleParameter2;
            set
            {
                _ModuleParameter2 = value;
                
            }
        }
        /// <summary>
        /// 参数3
        /// </summary>
        [DataMember]
        [ProtoMember(12)]
        public string ModuleParameter3
        {
            get => _ModuleParameter3;
            set
            {
                _ModuleParameter3 = value;
                
            }
        }
        /// <summary>
        /// 参数4
        /// </summary>
        [DataMember]
        [ProtoMember(13)]
        public string ModuleParameter4
        {
            get => _ModuleParameter4;
            set
            {
                _ModuleParameter4 = value;
                
            }
        }
        /// <summary>
        /// 参数5
        /// </summary>
        [DataMember]
        [ProtoMember(14)]
        public string ModuleParameter5
        {
            get => _ModuleParameter5;
            set
            {
                _ModuleParameter5 = value;
                
            }
        }
        /// <summary>
        /// 模块操作项
        /// </summary>
        [DataMember]
        [ProtoMember(15)]
        public int ModuleOper
        {
            get => _ModuleOper;
            set
            {
                _ModuleOper = value;
                
            }
        }

        #endregion
    }
}

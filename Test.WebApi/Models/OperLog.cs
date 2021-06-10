/*****************************************************************************
 * Version 		: 2.15
 * Create Date	: 2021/5/26			
 * Create By  	: FireTiger@MFKSoft.com	
 * Description	: OperLog的实体层
 * Sample    	: 
 *					
 *
 *
 *
 ******************************************************************************/

using System;
using System.Data;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Test.WebApi.Models
{
    /// <summary>
    /// OperLog的实体层
    /// </summary>
    [Serializable]
    public class OperLog
    {

        #region 私有变量

        /// <summary>
        ///                    
        /// </summary>
        private string _OLID;

        /// <summary>
        ///                    
        /// </summary>
        private string _TableName;

        /// <summary>
        ///                    
        /// </summary>
        private string _PrimaryKey;

        /// <summary>
        ///                    
        /// </summary>
        private DateTime _OperTime;

        /// <summary>
        ///                    
        /// </summary>
        private string _OperID;

        /// <summary>
        ///                    
        /// </summary>
        private string _OperName;

        /// <summary>
        ///                    
        /// </summary>
        private short? _OperType;

        /// <summary>
        ///                    
        /// </summary>
        private string _OldData;

        /// <summary>
        ///                    
        /// </summary>
        private string _NewData;
        #endregion

        #region 公共属性
        /// <summary>
        /// mongodb主键    		
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        ///                    
        /// </summary>
        public string OLID
        {
            get => _OLID;
            set
            {
                _OLID = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public string TableName
        {
            get => _TableName;
            set
            {
                _TableName = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public string PrimaryKey
        {
            get => _PrimaryKey;
            set
            {
                _PrimaryKey = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public DateTime OperTime
        {
            get => _OperTime;
            set
            {
                _OperTime = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public string OperID
        {
            get => _OperID;
            set
            {
                _OperID = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public string OperName
        {
            get => _OperName;
            set
            {
                _OperName = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public short? OperType
        {
            get => _OperType;
            set
            {
                _OperType = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public string OldData
        {
            get => _OldData;
            set
            {
                _OldData = value;
            }
        }

        /// <summary>
        ///                    
        /// </summary>
        public string NewData
        {
            get => _NewData;
            set
            {
                _NewData = value;
            }
        }

        #endregion
    }
}
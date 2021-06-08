using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.WebApi.Models
{
    /// <summary>
    /// OperLog的实体层
    /// </summary>
    [Serializable]
    public class RequestLog
    {
        /// <summary>
        /// mongodb主键    		
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        ///                    
        /// </summary>
        public string RequestLogID{ get; set;}
        /// <summary>
        ///                    
        /// </summary>
        public string RequestIP { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public DateTime RequestTime { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public DateTime RequestLogTime { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public string RequestURL { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public string RequestMethod { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public IDictionary<string, string> RequestHeaders;
        /// <summary>
        ///                    
        /// </summary>
        public object Certificate { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public IDictionary<string, object> RequestParameters { get; set; }
        /// <summary>
        ///                    
        /// </summary>
        public IDictionary<string, string> ResponseHeaders { get; set; }

        /// <summary>
        ///                    
        /// </summary>
        public object ResponseContent { get; set; }
    }
}

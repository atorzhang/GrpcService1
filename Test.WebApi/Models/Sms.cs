using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Test.WebApi.Models
{
    public class Sms
    {
        public object id { get; set; }
        public string SMSID { get; set; }
        public int Platform { get; set; }
        public string Target { get; set; }
        public string Content { get; set; }
        public object Result { get; set; }
        public DateTime SendTime { get; set; }
    }
}

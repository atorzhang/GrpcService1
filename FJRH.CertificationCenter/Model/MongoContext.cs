using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.CertificationCenter.Model
{
    public class MongoContext
    {
        public MongoClient Mongo { get; set; }

        public MongoContext(IConfiguration configuration)
        {
            var mongoUrl = configuration["Connection:MongoDB:Conn"];
            Ext.MongoExt.DefaultDbName = configuration["Connection:MongoDB:DefaultDBName"];
            Mongo = new MongoClient(mongoUrl);
        }
    }
}

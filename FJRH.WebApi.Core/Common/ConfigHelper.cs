using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FJRH.WebApi.Core
{
    public class ConfigHelper
    {
        /// <summary>
        /// 应用程序默认配置文件获取
        /// </summary>
        public static IConfigurationRoot DefaultConfiguration
        {
            get
            {
                return GetConfiguration("appsettings.json");
            }
        }

        /// <summary>
        /// 初始化应用程序配置文件
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetConfiguration(string fileName)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(fileName, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = configBuilder.Build();
            return configuration;
        }
    }
}

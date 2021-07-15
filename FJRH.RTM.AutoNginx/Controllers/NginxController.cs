using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJRH.RTM.AutoNginx.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NginxController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILogger<NginxController> _logger;
        private SqlSugarContext _sqlSugarContext;
        private string baseConfDir;

        public NginxController(ILogger<NginxController> logger, SqlSugarContext sqlSugarContext, IConfiguration configuration)
        {
            _logger = logger;
            _sqlSugarContext = sqlSugarContext;
            _configuration = configuration;
            baseConfDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conf.d");
        }

        [HttpPost("conf")]
        public async Task<string> Conf([FromForm]string key="")
        {
            if(key == _configuration["Nginx:key"])
            {
                var result = await GenConf();
                Update();
                return result;
            }
            return "请输入正确的密码";
        }

        /// <summary>
        /// 执行bash更新nginx配置
        /// </summary>
        private void Update()
        {
            var bash = $" cd {baseConfDir}/ \n cp * /etc/nginx/conf.f \n service nginx restart";
            var aa = bash.Replace("\r\n", "\n").Bash();
            Console.WriteLine(aa);
        }

        /// <summary>
        /// 生成配置文件
        /// </summary>
        /// <returns></returns>
        private async Task<string> GenConf()
        {
            var result = "";
            try
            {
                //获取所有的机构
                var allOrg = await _sqlSugarContext.Db.Queryable<ORG>().Where(o => o.ORGStatus == 1).OrderBy(o => o.CreateTime).ToListAsync();
                //获取所有机构映射端口信息
                var allPortMapping = await _sqlSugarContext.Db.Queryable<PortMapping>().OrderBy(o => o.ORGID).ToListAsync();
                //获取模板参数
                var server_name = _configuration["Nginx:server_name"];
                var targetIps = _configuration.GetSection("Nginx:target_ips").Get<List<string>>();
                var connect_timeout = _configuration["Nginx:connect_timeout"];
                var send_timeout = _configuration["Nginx:send_timeout"];
                var read_timeout = _configuration["Nginx:read_timeout"];
                var fail_timeout = _configuration["Nginx:fail_timeout"];
                var isSsl = _configuration.GetSection("Nginx:ssl").Get<bool>();
                var ssl_certificate_key = _configuration["Nginx:ssl_certificate_key"];
                var ssl_certificate = _configuration["Nginx:ssl_certificate"];
                //获取模板
                var temp = System.IO.File.ReadAllText("nginx.txt", System.Text.Encoding.UTF8);
                //输出文件夹
                if (!Directory.Exists(baseConfDir))
                {
                    Directory.CreateDirectory(baseConfDir);
                }
                //生成模板
                foreach (var org in allOrg)
                {
                    var orgMappings = allPortMapping.Where(o => o.ORGID == org.ORGID).OrderBy(o => o.Port).ToList();
                    var orgConf = string.Empty;
                    foreach (var orgMap in orgMappings)
                    {
                        var servers = string.Join(";\n\t", targetIps.Select(i => $"server {i}:{orgMap.Port} fail_timeout={fail_timeout}"));
                        if (!string.IsNullOrEmpty(servers))
                        {
                            servers += ";";
                        }
                        var orgTemp = temp.Replace("{servers}", servers)
                            .Replace("{server_name}", server_name)
                            .Replace("{port}", orgMap.Port.ToString())
                            .Replace("{send_timeout}", send_timeout)
                            .Replace("{read_timeout}", read_timeout)
                            .Replace("{connect_timeout}", connect_timeout);
                        if (isSsl)
                        {
                            var sslTemp = $@"ssl on;
    #ssl证书的pem文件路径
    ssl_certificate  {ssl_certificate};
    #ssl证书的key文件路径
    ssl_certificate_key {ssl_certificate_key};";
                            orgTemp = orgTemp.Replace("{ssl}", sslTemp);
                        }
                        else
                        {
                            orgTemp = orgTemp.Replace("{ssl}","");
                        }
                        orgConf += orgTemp;
                    }

                    if (!string.IsNullOrEmpty(orgConf))
                    {
                        //将配置写入文件
                        var configPath = Path.Combine(baseConfDir, $"{org.ORGPY?.ToLower()}_{org.ORGCode?.ToLower()}.conf");
                        try
                        {
                            using FileStream fs = new FileStream(configPath, FileMode.Create);
                            byte[] info = new UTF8Encoding(true).GetBytes(orgConf.Replace("\r\n","\n"));
                            fs.Write(info, 0, info.Length);

                            //System.IO.File.WriteAllText(configPath, orgConf, System.Text.Encoding.UTF8);
                        }
                        catch (Exception e)
                        {
                            result += e.ToString()+"\n\n";
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}

using System;
using System.Reflection;

namespace Proxy.Common
{
    public class ConfigService
    {
        /// <summary>
        /// 基础配置
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 患者绑定时间
        /// </summary>
        public static DateTime bind_time { get; set; } = default(DateTime);
        public ConfigService()
        {
            var path = Assembly.GetCallingAssembly().Location;
            Path = path.Substring(0, path.LastIndexOf("\\"));
        }

        public string GetPath()
        {
            return Path;
        }
    }
}

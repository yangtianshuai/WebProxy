using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Proxy.Models
{
    public class ConfigFile
    {
        public string plugin_id { get; set; }
        /// <summary>
        /// 插件配置
        /// </summary>
        public string configs { get; set; }
    }
}

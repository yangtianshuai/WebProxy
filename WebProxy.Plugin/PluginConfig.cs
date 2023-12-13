using System;
using System.ComponentModel;

namespace WebProxy.Plugin
{
    public class PluginConfig
    {
        public PluginModel Plugin { get; set; }
        /// <summary>
        /// 插件主文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 插件创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 插件更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 插件路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Discription { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public PluginFrom From { get; set; }   
        
    }

    public enum PluginFrom
    {
        /// <summary>
        /// 本地
        /// </summary>
        [Description("本地")]
        Local,
        /// <summary>
        /// 服务器
        /// </summary>
        [Description("服务器")]
        Server
    }
}
using System;

namespace Web_Proxy.Models
{
    public class PluginFileView
    {
        /// <summary>
        /// 插件id
        /// </summary>
        public string plugin_id { get; set; }
        public string key { get; set; }
        /// <summary>
        ///插件名称
        /// </summary>
        public string plugin_name { get; set; }
        /// <summary>
        /// 插件版本
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 版本序号
        /// </summary>
        public int version_no { get; set; }
        /// <summary>
        /// 流水号（GUID-36位）
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// 是否为主文件
        /// </summary>
        public string main_flag { get; set; }
        /// <summary>
        /// Http下载URL
        /// </summary>
        public string upload_url { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 文件描述
        /// </summary>
        public string description { get; set; }
    }
}

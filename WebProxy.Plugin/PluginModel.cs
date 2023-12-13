namespace WebProxy.Plugin
{
    public class PluginModel
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 插件Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }        
        /// <summary>
        /// 版本序号
        /// </summary>
        public int VersionNo { get; set; }
        /// <summary>
        /// 是否自动启动
        /// </summary>
        public bool? AutoStart { get; set; }

    }
}
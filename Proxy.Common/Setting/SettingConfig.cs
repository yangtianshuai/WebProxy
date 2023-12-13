namespace Proxy.Common.Setting
{
    public class SettingConfig
    {
        public string BaseApi { get; set; }
        /// <summary>
        /// 本地端口
        /// </summary>
        public int LocalPort { get; set; }
        public string Token { get; set; }
        public string Version { get; set; }
        public int VersionNo { get; set; }
        /// <summary>
        /// 本地版本号
        /// </summary>
        public int LocalVersionNo { get; set; }
        /// <summary>
        /// web主页面
        /// </summary>
        public int HomeUrl { get; set; }  
        /// <summary>
        /// 客户端ip列表（多网卡有可能是多个ip）
        /// </summary>
        public string IPs { get; set;}
        
    }
}
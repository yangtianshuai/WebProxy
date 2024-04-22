using Proxy.Common.Setting;

namespace Web_Proxy.Models
{
    public class ClientRegisterModel
    { 
        
        public string Token { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 8655;
        /// <summary>
        /// Mac地址
        /// </summary>
        public string Mac { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 版本序号
        /// </summary>
        public int? VersionNo { get; set; }
        /// <summary>
        /// 启动路径
        /// </summary>
        public string StartPath { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        public string OS { get; set; } 
        
        public ClientRegisterModel() {
            SettingManager _manager = new SettingManager();
            //读取配置文件
            SettingConfig _config = _manager.Config.Read();
            if (_config != null)
            {
                if (_config.LocalPort>0)
                {
                    Port = _config.LocalPort;
                }
            }
        }
    }
}
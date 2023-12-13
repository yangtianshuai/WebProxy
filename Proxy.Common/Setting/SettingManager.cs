using System;

namespace Proxy.Common.Setting
{
    public class SettingManager : ProxyManager, IDisposable
    {
        //客户端配置
        private Config<SettingConfig> _config;
       
        public SettingManager()
        {
            string path = this.BaseDictionary + "setting.cfg";
            _config = new Config<SettingConfig>(path);
        }

        public Config<SettingConfig> Config
        {
            get
            {
                return _config;
            }
        }

        public void Dispose()
        {
            
        }
    }
}

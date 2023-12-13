using System;
using System.Collections.Generic;
using System.Text;
using Proxy.Common;

namespace WebProxy.Plugin
{    
    public class ClientManager: ProxyManager
    {
        private static Config<ClientConfig> _config;
        private string path;

        public ClientManager()
        {
            string path = this.BaseDictionary + "client.wp";
            _config = new Config<ClientConfig>(path);
        }

        public Config<ClientConfig> Config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
            }
        }



    }
}

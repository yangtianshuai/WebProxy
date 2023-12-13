using Proxy.Common;
using System;
using System.Collections.Generic;

namespace WebProxy.Plugin
{
    public class PluginSetting
    {        
        /// <summary>
        /// Key-Value配置
        /// </summary>
        private Dictionary<string, string> settings { get; set; }
        private string path;

        public PluginSetting():this(Environment.CurrentDirectory)
        {            
        }
        public PluginSetting(string path)
        {
            if (path[path.Length - 1] != '\\')
            {
                path += "\\";
            }
            //配置
            this.path = path + "setting.cfg";
            settings = new Config<Dictionary<string, string>>(this.path).Read();
        }

        public bool Save()
        {
            return new Config<Dictionary<string, string>>(path).Write(settings);
        }

        public bool Set(string key, string value)
        {
            if (settings == null)
            {
                settings = new Dictionary<string, string>();
            }
            if (settings.ContainsKey(key))
            {
                settings[key] = value;
                return true;
            }
            settings.Add(key, value);
            return true;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> Get()
        {
            return settings;
        }

        public string Get(string key)
        {
            if (settings != null && settings.ContainsKey(key))
            {
                return settings[key];
            }
            return "";
        }
        
    }
}

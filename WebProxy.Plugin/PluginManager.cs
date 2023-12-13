using Proxy.Common;
using System;
using System.Collections.Generic;

namespace WebProxy.Plugin
{
    public class PluginManager : ProxyManager
    {
        private static Config<List<PluginConfig>> _config;
        public PluginManager()
        {
            string path = this.BaseDictionary + "plugin.wp";
            _config = new Config<List<PluginConfig>>(path);
        }
        public PluginManager(string update_path)
        {
            string path = this.BaseDictionary + update_path;
            _config = new Config<List<PluginConfig>>(path);
        }
        public Config<List<PluginConfig>> Config
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

        private static PluginManager current;
        /// <summary>
        /// 当前管理者
        /// </summary>
        public static PluginManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new PluginManager();
                }
                return current;
            }
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="action"></param>
        public void Load(Action<PluginConfig> action = null)
        {
            var configs = _config.Read();
            if (configs != null)
            {
                PluginCollection.Clear();
                foreach (var config in configs)
                {
                    PluginCollection.Add(config.Plugin);
                    action?.Invoke(config);
                }
            }
        }        

        /// <summary>
        /// 反馈动作
        /// </summary>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void Action(Type[] list,Action<IPlugin> action)
        {
            var _type = typeof(IPlugin);
            foreach (var item in list)
            {
                var types = item.GetInterfaces();
                foreach (var t in types)
                {
                    if (t.FullName == _type.FullName)
                    {
                        //Activator.CreateComInstanceFrom(t.FullName, t.Name);
                        var plugin = Activator.CreateInstance(item) as IPlugin;
                        if (plugin == null)
                        {
                            continue;
                        }
                        action?.Invoke(plugin);
                        break;
                    }
                }
            }
        }

        public PluginSetting GetSetting(string plugin_id)
        {
            string path = "";
            var plugins = _config.Read();
            foreach (var plugin in plugins)
            {
                if (plugin_id == plugin.Plugin.ID)
                {
                    int index = plugin.Path.LastIndexOf('\\');
                    path = plugin.Path.Substring(0, index);
                    break;
                }
            }
            if (path.Length > 0)
            {
                return new PluginSetting(path);
            }
            return null;
        }

        public bool SetSetting(string plugin_id, Dictionary<string, string> configs)
        {
            string path = "";
            var plugins = _config.Read();
            foreach (var plugin in plugins)
            {
                if (plugin_id == plugin.Plugin.ID)
                {
                    int index = plugin.Path.LastIndexOf('\\');
                    path = plugin.Path.Substring(0, index);
                    break;
                }
            }
            if (path.Length > 0)
            {                      
                var setting =  new PluginSetting(path);
                foreach(var config in configs)
                {
                    setting.Set(config.Key, config.Value);
                }
                return setting.Save();
            }
            return false;
        }
    }
}
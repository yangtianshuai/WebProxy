using System.Collections.Generic;

namespace WebProxy.Plugin
{
    public class PluginCollection
    {
        private static Dictionary<string,PluginModel> _plugins;
        private readonly static object _lock = new object();

        static PluginCollection()
        {
            _plugins = new Dictionary<string, PluginModel>();
        }

        public static void Add(PluginModel plugin)
        {
            if (plugin.ID == null)
            {
                return;
            }
            lock (_lock)
            {
                if (!_plugins.ContainsKey(plugin.ID))
                {
                    _plugins.Add(plugin.ID, plugin);
                }
            }
        }

        public static void Clear()
        {
            lock (_lock)
            {
                _plugins.Clear();
            }
        }

        public static void Update(PluginModel plugin)
        {
            lock (_lock)
            {
                if (_plugins.ContainsKey(plugin.ID))
                {
                    _plugins[plugin.ID] = plugin;
                }
            }
        }

        public static void Remove(PluginModel plugin)
        {
            lock (_lock)
            {
                if (_plugins.ContainsKey(plugin.ID))
                {
                    _plugins.Remove(plugin.ID);
                }
            }
        }
    }
}
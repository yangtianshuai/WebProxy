//using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proxy.Common;
using Proxy.Common.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Web_Proxy.Service;
using WebProxy.Plugin;

namespace Web_Proxy
{
    public partial class FormMain : Form
    {
        private SettingConfig _config;
        private SettingManager _manager;
        private List<PluginConfig> _plugins;

        FormHome form = null;
        public FormMain()
        {
            InitializeComponent();
            _manager = new SettingManager();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _config = _manager.Config.Read();
            if (_config == null)
            {
                _config = new SettingConfig();
            }

            ApplicationUnit.Client.Token = _config?.token;
            ApplicationUnit.IsCheckPlugins = _config.IsCheckPlugins;

            //检测客户端
            // if (FireWallHelper.GetRule("Web-Proxy", "TCP") == null)
            //{
            //加入防火墙入栈规则
            // FireWallHelper.AddRule("Web-Proxy", ApplicationUnit.Client.Port, "TCP");

            //检测客户端是否注册
            if (RegistryUtility.ExistConfig())
            {
                //保存本地端口配置
                RegistryUtility.RegistryPort(ApplicationUnit.Client.Port);

                // if (_config?.Ip != ApplicationUnit.Client.IP)
                //{
                //不同计算机拷贝
                //  ApplicationUnit.Client.Token = null;
                //客户端插件是否需要自动注册

                //本地配置里的ip
                string[] config_ip = { };
                if (!string.IsNullOrEmpty(_config.Ip))
                {
                    config_ip = _config.Ip.Split(',');
                }

                Logger.WriteTrace("客户端正在加入防火墙入栈规则");

                //不同计算机拷贝(或本机重装系统且更换ip)
                if (commonElements(config_ip, ApplicationUnit.Client.IP))
                {
                    ApplicationUnit.Client.Token = null;

                    //需要服务器注册插件
                    ApplicationUnit.IsCheckPlugins = true;
                }
            }
            else
            {
                Logger.WriteTrace("客户端已加入防火墙入栈规则");
                var res_client = new ClientService().GetClient(ApplicationUnit.Client.Token);
                if (res_client.IsSuccess())
                {
                    var res_data = JsonConvert.DeserializeObject<JObject>(res_client.Data.ToString());

                    //服务器注册的客户端ip
                    string[] res_ips = { };
                    var ipArray = res_data["ip"] as JArray;
                    if (ipArray != null)
                    {
                        res_ips = ipArray.ToObject<string[]>();
                    }

                    //IP、端口、启动地址都没有发生变化
                    if (commonElements(res_ips, ApplicationUnit.Client.IP) && int.Parse(res_data["port"].ToString()) == ApplicationUnit.Client.Port && res_data["start_path"].ToString() == ApplicationUnit.Client.StartPath)
                    {
                        return;
                    }
                }
            }

            // 注册或修改客户端（如果已经注册，从服务器获取客户端配置）
            var result = new ClientService().Register(ApplicationUnit.Client);
            if (result.IsSuccess())
            {
                //注册成功                   
                _config.token = result.Data.ToString();
                ApplicationUnit.Client.Token = _config.token;
                _config.BaseApi = new ClientService().Config;
                _config.LocalPort = ApplicationUnit.Client.Port;
                _config.Version = ApplicationUnit.Client.Version;
                _config.VersionNo = ApplicationUnit.Client.VersionNo == null ? 0 : ApplicationUnit.Client.VersionNo.Value;
                _config.LocalVersionNo = ApplicationUnit.Client.VersionNo == null ? 0 : ApplicationUnit.Client.VersionNo.Value;
                _config.Ip = ApplicationUnit.Client.IP;

                if (_manager.Config.Write(_config))
                {
                    Logger.WriteTrace("自动注册成功");
                }
            }

            //插件注册检测            
            if (ApplicationUnit.IsCheckPlugins)
            {
                _plugins = new PluginManager("update_plugin.wp").Config.Read();
                if (_plugins == null)
                {
                    return;
                }

                var plugin_id = _plugins.Select(t => t.Plugin.ID).ToList();

                //调用客户端插件注册检测api （插件id，客户端Token）
                var res_plugin = new ClientService().ModifyPluginsRegister(ApplicationUnit.Client.Token, plugin_id);
                if (res_plugin.IsSuccess())
                {
                    var _config = _manager.Config.Read();
                    _config.IsCheckPlugins = true;
                    if (_manager.Config.Write(_config))
                    {
                        Logger.WriteTrace("插件服务器注册更新成功");
                    }
                }
                else
                {
                    Logger.WriteTrace(res_plugin.Message);
                }
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            if (form == null)
            {
                form = new FormHome();
                form.FormClosedEvent += new FormHome.Form_Closed(SetClose);
            }
            form.Show();
        }

        private void SetClose()
        {
            //form = null;
        }

        /// <summary>
        /// 判断两个数组是否有交集
        /// </summary>
        /// <param name="arr_str1"></param>
        /// <param name="str2">客户端的真实ip</param>
        /// <returns></returns>
        private static bool commonElements(string[] arr_str1, string str2)
        {
            string[] arr_str2 = str2.Split(',');

            //获取两个数组的交集
            var commonElements = arr_str1.Intersect(arr_str2);
            return commonElements.Any();
        }
    }
}
using Newtonsoft.Json;
using Proxy.Common;
using Proxy.Common.Setting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Web_Proxy.UC;
using WebProxy.Plugin;

namespace Web_Proxy
{
    public partial class FormHome : Form
    {
        public List<PluginConfig> plugins;
        public UCPlugin selectPlugin;
        public delegate void Form_Closed();
        public Form_Closed FormClosedEvent;
        private SettingConfig _config;

        public FormHome()
        {
            InitializeComponent();
            this.LoadPlugin();
            this.LoadConfig();
        }
        private void LoadConfig()
        {
            var _manager = new SettingManager();
            //读取配置文件
            _config = _manager.Config.Read();
        }

        /// <summary>
        /// 重新加载插件
        /// </summary>
        /// <param name="flag"></param>
        private void LoadPlugin(bool flag = false)
        {
            if (flag)
            {
                //PluginManager.Current.Load(path =>
                //{
                ////加载API路由
                //System.HttpProxy.ApiRoute.Load(System.Reflection.Assembly.LoadFrom(path).GetTypes());
                //});
            }

            this.labelRemove.ForeColor = Color.FromArgb(180, 180, 180);
            this.labelRemove.Cursor = Cursors.Default;

            this.panelPlugin.Controls.Clear();

            //读取插件config
            plugins = new PluginManager().Config.Read();
            if (plugins == null)
            {
                return;
            }

            foreach (var plugin in plugins)
            {
                var uc = new UCPlugin(plugin);
                uc.Dock = DockStyle.Top;
                uc.SelectedChange += new UCPlugin.SelectedChanged((UCPlugin _uc) =>
                {
                    foreach (Control control in this.panelPlugin.Controls)
                    {
                        control.BackColor = Color.Transparent;
                    }
                    uc.BackColor = Color.FromArgb(246, 248, 188);
                    this.selectPlugin = uc;
                    this.labelRemove.ForeColor = Color.FromArgb(220, 0, 0);
                    this.labelRemove.Cursor = Cursors.Hand;
                });
                this.panelPlugin.Controls.Add(uc);
            }
        }

        #region --设置操作
        private void pbSetting_Click(object sender, EventArgs e)
        {
            FormSetting setting = new FormSetting();
            setting.Show(this);
        }

        private void SettingClose()
        {
            //this.Visible = true;
        }

        private void control_MouseEnter(object sender, EventArgs e)
        {
            Control control = sender as Control;
            control.BackColor = Color.FromArgb(81,91,108);
        }

        private void control_MouseLeave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            control.BackColor = Color.Transparent;
        }
        #endregion

        #region --关闭操作
        private void imgClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (FormClosedEvent != null)
            {
                FormClosedEvent();
            }
        }

        private void imgClose_MouseEnter(object sender, EventArgs e)
        {
            this.imgClose.BackColor = Color.Red;
        }

        private void imgClose_MouseLeave(object sender, EventArgs e)
        {
            this.imgClose.BackColor = Color.Transparent;
        }
        #endregion       
        
        private void FormSet_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = Screen.GetWorkingArea(this).Size;            
            //this.ucView.Selected = true;
        }

        //本地导入
        private void labelLocalImp_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "dll|*.dll";
            ofd.RestoreDirectory = false;
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {                          
                if (plugins == null)
                {
                    plugins = new List<PluginConfig>();
                }
                var plugin = new PluginConfig();
                plugin.Path = Path.GetFullPath(ofd.FileName);
                plugin.CreateTime = DateTime.Now;

                var file = FileVersionInfo.GetVersionInfo(plugin.Path);
                plugin.From = PluginFrom.Local;
                plugin.Name = file.ProductName;
                plugin.Discription = file.FileDescription;

                plugin.Plugin = new PluginModel
                {
                    //获取插件版本
                    Version = (Path.GetFileName(Path.GetDirectoryName(plugin.Path))).Split('_')[1]
                };

                //根据GuidAttribute获取一个guid字符串
                var assembly = Assembly.LoadFrom(plugin.Path);
                var guid_attr = Attribute.GetCustomAttribute(Assembly.LoadFile(plugin.Path), typeof(GuidAttribute));
                string key = ((GuidAttribute)guid_attr).Value;             

                //插件需要增加Key扩展
                plugin.Plugin.Key = key;

                //服务器注册,注册后返回plutin_id
                if (_config != null)
                {
                    try
                    {
                        string url = _config.BaseApi + "/api/client/GetClientId";
                        var res = JsonConvert.DeserializeObject<ResponseResult2>(new HttpHelper().Get(url + "?plugin_key=" + key + "&client_token=" + _config.Token + "&version=" + plugin.Plugin.Version));
                        if (res.IsSuccess())
                        {
                            plugin.Plugin.ID = res.Data.ToString();
                            plugins.Add(plugin);
                            //保存配置
                            if (new PluginManager().Config.Write(plugins))
                            {
                                this.LoadPlugin(true);
                            }
                        }
                        else
                        {
                            Logger.WriteError($"远程注册插件：plugin_key={key}：{JsonConvert.SerializeObject(res)}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError("远程注册插件失败：" + ex.Message);
                    }                   
                }else
                {
                    Logger.WriteError("请先注册客户端");
                    return;
                }
            }
        }

        private void labelRemove_Click(object sender, EventArgs e)
        {
            if(this.selectPlugin==null)
            {
                return;
            }
            var config = plugins.Find(t => t.Path == this.selectPlugin.GetPath());
            
            plugins.Remove(config);          
            //保存配置
            if (new PluginManager().Config.Write(plugins))
            {
                try
                {
                    string url = _config.BaseApi + "/api/client/DeleteClientPlugin";
                    var res = JsonConvert.DeserializeObject<ResponseResult2>(new HttpHelper().Get(url + "?plugin_id=" + config.Plugin.ID + "&client_token=" + _config.Token));
                }
                catch (Exception ex)
                {
                    
                }
                this.LoadPlugin(true);
            }
        }
    }
}
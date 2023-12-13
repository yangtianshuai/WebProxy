using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Proxy.Common;
using Proxy.Common.Setting;
using Web_Proxy.Service;
using WebProxy.Plugin;

namespace Web_Proxy
{
    public partial class FormMain : Form
    {
        //public ClientConfig client;
        private SettingConfig _config;
        private SettingManager _manager;
        FormHome form = null;
        public FormMain()
        {
            InitializeComponent();
            _manager = new SettingManager();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _config = _manager.Config.Read();
            ApplicationUnit.Client.Token = _config?.Token;

            //检测客户端是否注册
            if (FireWallHelper.GetRule("Web-Proxy", "TCP") == null)
            {
                //加入防火墙入栈规则
                FireWallHelper.AddRule("Web-Proxy", ApplicationUnit.Client.Port, "TCP");
               
                if(_config.Ip != ApplicationUnit.Client.IP)
                {
                    //不同计算机拷贝
                    ApplicationUnit.Client.Token = null;
                    //客户端插件是否需要自动注册

                }                
            }
            else
            {
                //检测客户端
                //IP、端口、启动地址都没有发生变化
                return;               
            }
            var result = new ClientService().Register(ApplicationUnit.Client);
            if (result.IsSuccess())
            {               
                //注册成功                   
                _config.Token = result.Data.ToString();
                _config.BaseApi = new ClientService().Config;
                _config.LocalPort = ApplicationUnit.Client.Port;
                _config.Token = result.Data.ToString();
                _config.Version = ApplicationUnit.Client.Version;
                _config.VersionNo = ApplicationUnit.Client.VersionNo.Value;
                _config.LocalVersionNo = ApplicationUnit.Client.VersionNo.Value;
                _config.Ip = ApplicationUnit.Client.IP;

                if (_manager.Config.Write(_config))
                {
                    Logger.WriteTrace("自动注册成功");
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

        private void OperaterGuide()
        {
            // 创建一个自定义对话框  
            DialogResult result = MessageBox.Show(this, "请重新注册客户端", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            // 根据用户选择执行不同的操作  
            switch (result)
            {
                case DialogResult.Yes:
                    // 用户点击了"Yes"按钮，执行相应的操作

                    break;
                case DialogResult.No:
                    // 用户点击了"No"按钮，执行相应的操作 

                    Application.Exit();

                    break;
                case DialogResult.Cancel:
                    // 用户点击了"Cancel"按钮，执行相应的操作

                    Application.Exit();

                    break;
                default:
                    // 用户关闭了对话框，不进行任何操作

                    Application.Exit();
                    break;
            }
        }
    }
}
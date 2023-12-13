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
            //监测客户端电脑

            #region  重新注册

            //读取配置文件
            _config = _manager.Config.Read();
            if (_config == null)
            {
                //自动注册
                var result = new ClientService().AutoRegister(ApplicationUnit.Client);
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
                    _config.IPs = ApplicationUnit.Client.IP;

                    if (_manager.Config.Write(_config))
                    {
                        MessageBox.Show("注册成功");
                        //重启Web服务
                        ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                    }
                }
                else
                {
                    MessageBox.Show(result.Message);
                    ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                }
            }
            else
            {
                if (ApplicationUnit.Client.IP != _config.IPs)
                {
                    // 1，修改数据库ip
                    var result = new ClientService().ModifyClientIPs(ApplicationUnit.Client);                    
                    if(result.IsSuccess())
                    {
                        //2，修改本地配置
                        if (result.Data==null)
                        {
                            _config.IPs = ApplicationUnit.Client.IP;
                            if (_manager.Config.Write(_config))
                            {
                                MessageBox.Show("注册成功");
                                //重启Web服务
                                ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                            }
                        }else
                        {
                            _config.Token = result.Data.ToString();
                            _config.BaseApi = new ClientService().Config;
                            _config.LocalPort = ApplicationUnit.Client.Port;
                            _config.Token = result.Data.ToString();
                            _config.Version = ApplicationUnit.Client.Version;
                            _config.VersionNo = ApplicationUnit.Client.VersionNo.Value;
                            _config.LocalVersionNo = ApplicationUnit.Client.VersionNo.Value;
                            _config.IPs = ApplicationUnit.Client.IP;

                            if (_manager.Config.Write(_config))
                            {
                                MessageBox.Show("注册成功");
                                //重启Web服务
                                ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                        ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                    }

                    ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                }

            }

            #endregion
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
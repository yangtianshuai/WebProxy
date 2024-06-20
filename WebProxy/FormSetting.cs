using Proxy.Common.Setting;
using System;
using System.Windows.Forms;
using Web_Proxy.Service;

namespace Web_Proxy
{
    public partial class FormSetting : PopForm
    {
        private SettingConfig _config;
        private SettingManager _manager;
        public FormSetting()
        {
            InitializeComponent();
            _manager = new SettingManager();
        }       

        private void FormSetting_Load(object sender, EventArgs e)
        {
            //this.tbPort.Text = ApplicationUnit.Client.Port.ToString();
            //加载配置
            LoadConfig();
        }

        private void LoadConfig()
        {
            //读取配置文件
            _config = _manager.Config.Read();
            if (_config != null)
            {
                //this.btRegister.Enabled = _config.Token == null || _config.Token.Length == 0;
                this.labelToken.Text = _config.Token;
                this.tbApi.Text = _config.BaseApi;
                this.labelVersion.Text = ApplicationUnit.Client.Version;
                this.labelVersionNo.Text = ApplicationUnit.Client.VersionNo.ToString();
                this.tbPort.Text = _config.LocalPort.ToString();
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (_config == null)
        //    {
        //        _config = new SettingConfig();
        //    }
        //    _config.BaseApi = this.tbApi.Text.Trim();

        //    int port = ApplicationUnit.Client.Port;
        //    if (int.TryParse(this.tbPort.Text.Trim(), out port))
        //    {
        //        ApplicationUnit.Client.Port = port;
        //    }
        //    _config.LocalPort = port;

        //    if (_manager.Config.Write(_config))
        //    {
        //        MessageBox.Show("保存成功");
        //        //重启Web服务
        //        ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
        //    }
        //}

        /// <summary>
        /// 注册按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRegister_Click(object sender, EventArgs e)
        {
           //1，参数验证
            if (_config == null)
            {
                _config = new SettingConfig();
            }
            _config.BaseApi = this.tbApi.Text.Trim();

            int port = ApplicationUnit.Client.Port;
            if (int.TryParse(this.tbPort.Text.Trim(), out port))
            {
                ApplicationUnit.Client.Port = port;
            }
            _config.LocalPort = port;

            //注册客户端或修改客户端注册
            var result = new ClientService(this.tbApi.Text.Trim()).Register(ApplicationUnit.Client);           
            if (result.IsSuccess())
            {
                //注册成功
                _config.Token = result.Data.ToString();
                this.labelToken.Text = _config.Token;
                if (_manager.Config.Write(_config))
                {
                    //重启Web服务
                    ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                }
                MessageBox.Show("注册成功");
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}
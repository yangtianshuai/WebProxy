using Proxy.Common;
using Proxy.Common.Setting;
using System;
using System.Diagnostics;
using System.HttpProxy;
using Web_Proxy.Models;
using Web_Proxy.Service;

namespace Web_Proxy
{
    public class ApplicationUnit
    {
        static ApplicationUnit()
        {
            Client = new ClientRegisterModel();
            Client.IP = SystemHelper.GetAllIP();
            Client.Mac = SystemHelper.GetComputerMac();
            Client.OS = Environment.OSVersion.VersionString;
            Client.Port = 8655;         
            Client.StartPath = Process.GetCurrentProcess().MainModule.FileName;
            Client.Version = Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion;
            var _manager = new SettingManager();
            var _config = _manager.Config.Read();
            if (_config != null)
            {
                if (_config.Token == null)
                {
                    //自动注册
                    //var result = new ClientService().AutoRegister(Client);
                    //if (result.IsSuccess())
                    //{
                    //    //注册成功                   
                    //    _config.Token = result.Data.ToString();
                    //    _config.BaseApi = new ClientService().Config;
                    //    _config.LocalPort = ApplicationUnit.Client.Port;
                    //    _config.Token = result.Data.ToString();
                    //    _config.Version = ApplicationUnit.Client.Version;
                    //    _config.VersionNo = ApplicationUnit.Client.VersionNo.Value;
                    //    _config.LocalVersionNo = ApplicationUnit.Client.VersionNo.Value;
                    //    _config.IPs = ApplicationUnit.Client.IP;

                    //    if (_manager.Config.Write(_config))
                    //    {
                    //       // MessageBox.Show("注册成功");
                    //        //重启Web服务
                    //        ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                    //    }
                    //}
                    //else
                    //{
                    //    //MessageBox.Show(result.Message);
                        
                    //    ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);
                    //}
                }
                if (_config.Version != null)
                {
                    Client.Version = _config.Version;
                }
                Client.VersionNo = _config.VersionNo;

                //if(_config.IPs!=Client.IP)
                //{
                //    //修改数据库
                //    //修改配置
                //}
            }
        }
        public static HttpServer Server { get; set; }

        /// <summary>
        /// 是否已经认证
        /// </summary>
        public static bool Auth { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public static ClientRegisterModel Client { get; private set; } 

    }
}
using Proxy.Common;
using System;
using System.Diagnostics;
using System.HttpProxy;
using Web_Proxy.Models;

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

        /// <summary>
        /// 是否要检测插件（服务器是否注册）
        /// </summary>
        public static bool IsCheckPlugins { get; set; }

    }
}
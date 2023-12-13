using Microsoft.Win32;
using Proxy.Common;
using System;
using System.Diagnostics;
using System.HttpProxy;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Web_Proxy.WS;
using WebProxy.Plugin;

namespace Web_Proxy
{
    static class Program
    {
        private static string appName = "Web-Proxy";

        #region --检查是否只运行一个实例 
        /// <summary>
        /// 创建互斥体
        /// </summary>
        /// <param name="lpMutexAttributes"></param>
        /// <param name="bInitialOwner"></param>
        /// <param name="lpName"></param>
        /// <returns></returns>
        [DllImport("kernel32.Dll", SetLastError = true)]
        private static extern IntPtr CreateMutex(SECURITY_ATTRIBUTES lpMutexAttributes, bool bInitialOwner, string lpName);
        [StructLayout(LayoutKind.Sequential)]
        private class SECURITY_ATTRIBUTES
        {
            public int nLength;
            public int lpSecurityDescriptor;
            public int bInheritHandle;
        }
        /// <summary>
        /// 监测进程是否存在
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <returns></returns>
        public static bool ProcessExist(string processName)
        {
            IntPtr hMutex = CreateMutex(null, false, processName);
            int ERROR_ALREADY_EXISTS = 0183;
            if (Marshal.GetLastWin32Error() == ERROR_ALREADY_EXISTS)
            {
                return true;//互斥体已经存在的逻辑处理
            }
            return false;
        }
        #endregion
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {            
            if (ProcessExist(appName))
            {
                Logger.WriteError("应用已启动");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //RegistMedical();//注册
            //开机启动
            AutoStart();

            //添加过滤器
            FilterCollection.Add(new ApiFilter());

            try
            {
                ApplicationUnit.Server = new HttpServer();

                //开启Web服务
                ApplicationUnit.Server.Run(ApplicationUnit.Client.Port);

                //开启WebSocket
                WebSocket.Current.Start(ApplicationUnit.Client.Port + 1);

                //加载本地API --（参数：获取当前运行的程序集中,定义的所有的类型)          
                ApiRoute.Load(Assembly.GetExecutingAssembly().GetTypes());

                if (FireWallHelper.GetRule("Web-Proxy", "TCP") == null)
                {
                    //加入防火墙入栈规则
                    FireWallHelper.AddRule("Web-Proxy", ApplicationUnit.Client.Port, "TCP");
                }                
            }
            catch (Exception ex)
            {
                Logger.WriteError("应用启动时发生错误：" + ex.StackTrace);
                return;
            }    

            PluginManager.Current.Load(config =>
            {
                try
                {
                    var types = Assembly.LoadFrom(config.Path).GetTypes();
                    //加载API路由
                    ApiRoute.Load(types);
                    //if (config.Plugin.AutoStart != null && config.Plugin.AutoStart.Value)
                    //{
                        //启动服务
                        PluginManager.Action(types, plugin => { plugin.Start(); });
                    //}
                }
                catch(Exception ex)
                {                    
                    Logger.WriteError($"插件【{config.Plugin.ID}】加载失败：{config.Path}");
                    Logger.WriteError(ex.StackTrace);
                }
            });            
            Application.Run(new FormMain());
        }        

        private static void RegistMedical()
        {
            var reg = Registry.ClassesRoot.OpenSubKey("WebProxy");
            //声明一个变量
            if (reg == null)
            {
                reg = Registry.ClassesRoot.CreateSubKey("WebProxy");
                reg.SetValue("URL Protocol", Process.GetCurrentProcess().MainModule.FileName);
                reg.Close();
                Registry.ClassesRoot.CreateSubKey("WebProxy\\DefaultIcon").Close();
                Registry.ClassesRoot.CreateSubKey("WebProxy\\shell").Close();
                Registry.ClassesRoot.CreateSubKey("WebProxy\\shell\\open").Close();
                reg = Registry.ClassesRoot.CreateSubKey("WebProxy\\shell\\open\\command");
                reg.SetValue("", "\""+Process.GetCurrentProcess().MainModule.FileName+"\" \"%1\"");
                reg.Close();
            }
        }

        private static void AutoStart()
        {            
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                key.SetValue("Web-Proxy", Process.GetCurrentProcess().MainModule.FileName);
                key.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteError($"自启动设置失败：{ex.Message}");
            }
        }
    }
}
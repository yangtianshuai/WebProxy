using Microsoft.Win32;
using Proxy.Common;
using System;
using System.Diagnostics;

namespace Web_Proxy
{
    internal class RegistryUtility
    {
        public static void RegistStart()
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
                reg.SetValue("", "\"" + Process.GetCurrentProcess().MainModule.FileName + "\" \"%1\"");
                reg.Close();
            }
        }

        public static bool ExistConfig()
        {
            try
            {
                RegistryKey local = Registry.Users;
                RegistryKey key = local.OpenSubKey(@".DEFAULT\WebProxy\Config", true);
                if (key == null)
                {
                    return false;
                }
                return key.GetValue("Port") != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void RegistryPort(int port)
        {
            try
            {
                RegistryKey local = Registry.Users;
                RegistryKey key = local.OpenSubKey(@".DEFAULT\WebProxy\Config", true);
                if (key == null)
                {
                    key = local.CreateSubKey(@".DEFAULT\WebProxy");
                    key = key.CreateSubKey(@"Config");
                }
                key.SetValue("Port", port);
                key.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteError($"本地注册表配置失败：{ex.Message}");
            }
        }

        public static void AutoStart()
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    key = local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
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

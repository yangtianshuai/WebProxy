using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Proxy.Common
{
    public class SystemHelper
    {
        #region --设置系统时间       
        [DllImport("Kernel32.dll")]
        private static extern void SetLocalTime(SystemTime st);

        [DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        public static extern int GetSystemDefaultLCID();

        [DllImport("kernel32.dll", EntryPoint = "SetLocaleInfoA")]
        public static extern int SetLocaleInfo(int Locale, int LCType, string lpLCData);

        [StructLayout(LayoutKind.Sequential)]
        class SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort Whour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;

        }
        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="dateTime"></param>
        public static void SetLocalTime(DateTime dateTime)
        {
            try
            {
                SystemTime st = new SystemTime();
                st.wYear = (ushort)dateTime.Year;
                st.wMonth = (ushort)dateTime.Month;
                st.wDay = (ushort)dateTime.Day;
                st.Whour = (ushort)dateTime.Hour;
                st.wMinute = (ushort)dateTime.Minute;
                st.wSecond = (ushort)dateTime.Second;
                SetLocalTime(st);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SetLocalTimeType(string dateFormat, string timeFormat)
        {
            try
            {
                int x = GetSystemDefaultLCID();
                if (String.IsNullOrEmpty(dateFormat))
                {
                    dateFormat = "yyyy/MM/dd";
                }
                if (String.IsNullOrEmpty(timeFormat))
                {
                    timeFormat = "HH:mm:ss";
                }
                //日期格式
                SetLocaleInfo(x, 0x20, dateFormat);
                //时间格式
                SetLocaleInfo(x, 0x1003, timeFormat);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        public static string GetCurrentPath()
        {
            var path = Assembly.GetCallingAssembly().Location;
            path = path.Substring(0, path.LastIndexOf("\\"));
            return path;
        }


        /// <summary>
        /// 获取操作系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetOS()
        {
            return new ComputerInfo().OSFullName;
        }
       
        /// <summary>
        /// 获取CPU信息
        /// </summary>
        /// <returns></returns>
        public static string GetCPU()
        {
            var mos = new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get();
            foreach (var obj in mos)
            {
                var max = (int.Parse(obj["MaxClockSpeed"].ToString()) / 1000.0F).ToString("0.#");
                return obj["Name"] + " " + obj["NumberOfCores"].ToString()+"核 " + max + "GHz";               
            }
            return null;
        }

        /// <summary>  
        ///获取本机IP 列表（考虑到多网卡的情况） 
        /// </summary>  
        /// <returns></returns>  
        public static string GetAllIP()
        {
            List<string> ipAddressList = new List<string>();

            IPAddress[] arr_ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            for (int i = 0; i < arr_ip.Length; i++)
            {
                if(arr_ip[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    if (new Regex(@"^((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.){3}(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])$").IsMatch(arr_ip[i].ToString()))
                    {
                        //return arr_ip[i].ToString();
                        ipAddressList.Add(arr_ip[i].ToString());
                    }
                }                
            }

            var ipList = string.Empty;
            if(ipAddressList.Count>0)
            {
                ipList = String.Join(",", ipAddressList.ToArray());
            }
            return ipList;
        }
        /// <summary>  
        ///获取本机IP  
        /// </summary>  
        /// <returns></returns>  
        public static string GetIP()
        {
            IPAddress[] arr_ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            for (int i = 0; i < arr_ip.Length; i++)
            {
                if (arr_ip[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    if (new Regex(@"^((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.){3}(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])$").IsMatch(arr_ip[i].ToString()))
                    {
                        return arr_ip[i].ToString();
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 获取本地IPv4地址
        /// </summary>
        /// <returns></returns>
        public static string[] GetLocalIpv4()
        {
            IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            StringCollection stringCollection = new StringCollection();
            IPAddress[] array = hostAddresses;
            for (int i = 0; i < array.Length; i++)
            {
                IPAddress iPAddress = array[i];
                bool flag = iPAddress.AddressFamily == AddressFamily.InterNetwork;
                if (flag)
                {
                    stringCollection.Add(iPAddress.ToString());
                }
            }
            string[] array2 = new string[stringCollection.Count];
            stringCollection.CopyTo(array2, 0);
            return array2;
        }

        /// <summary>
        /// 获取计算机Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetComputerMac()
        {
            string mac = string.Empty;
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return mac;
        }

        public static string GetDirveType(DriveType type)
        {
            switch (type)
            {
                case DriveType.NoRootDirectory:
                    return "未分区";
                case DriveType.Removable:
                    return "可移动磁盘";
                case DriveType.Fixed:
                    return "硬盘";
                case DriveType.Network:
                    return "网络驱动器";
                case DriveType.CDRom:
                    return "光驱";
                case DriveType.Ram:
                    return "内存磁盘";
            }
            return "未知设备";
        }

        public static string GetSize(long byteSize)
        {
            string unit = "";
            int count = 0;
            while (byteSize >= 1024)
            {
                byteSize = byteSize / 1024;
                count++;
            }
            if (count == 0)
            {
                unit = "B";
            }
            else if (count == 1)
            {
                unit = "KB";
            }
            else if (count == 2)
            {
                unit = "MB";
            }
            else if (count == 3)
            {
                unit = "GB";
            }
            else if (count == 4)
            {
                unit = "TB";
            }
            return byteSize + unit;
        }

        /// <summary>
        /// 判断端口是否占用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
    }
}
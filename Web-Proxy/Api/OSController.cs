using Proxy.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.HttpProxy;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using Web_Proxy.Service;

namespace Web_Proxy.Api
{
    /// <summary>
    /// 操作系统控制器
    /// </summary>
    [Route("api/os")]
    public class OSController : ApiController
    {
        /// <summary>
        /// 获取计算机信息
        /// </summary>
        /// <returns></returns>
        [Route("GetComputer")]
        public ActionResult GetComputerInfo()
        {
            var result = new ResponseResult();

            var host = IPGlobalProperties.GetIPGlobalProperties();
            result.Data = new
            {
                host_name = host.HostName,
                domain_name = host.DomainName.Length > 0 ? host.DomainName : null,
                os = SystemHelper.GetOS(),
                platform = Environment.OSVersion.Platform.ToString(),
                user = Environment.UserName,
                Environment.WorkingSet,
                machine_name = Environment.MachineName,
                cup = SystemHelper.GetCPU()
            };
            return new JsonResult(result);
        }

        /// <summary>
        /// 设置操作系统时间
        /// </summary>
        /// <param name="time">设置时间</param>
        /// <param name="format">格式</param>
        /// <returns></returns>
        [Route("SetTime")]
        public ActionResult SetTime(DateTime time, string format)
        {
            var result = new ResponseResult();
            SystemHelper.SetLocalTime(time);
            if (format != null)
            {
                var array = format.Split(' ');
                SystemHelper.SetLocalTimeType(array[0], array[1]);
            }
            result.Sucess("运行中");
            return new JsonResult(result);
        }

        /// <summary>
        /// 获取系统进程
        /// </summary>
        /// <returns></returns>
        [Route("GetProcess")]
        public ActionResult GetProcess()
        {
            var result = new ResponseResult();
            var processes = Process.GetProcesses();
            var list = new List<object>();
            foreach (var process in processes)
            {
                if (process.MainWindowHandle == null)
                {
                    continue;
                }
                list.Add(new
                {
                    id = process.Id,
                    name = process.ProcessName,
                    title = process.MainWindowTitle,
                    size = process.WorkingSet64 / 1024 / 1024,
                    momery = process.WorkingSet64 / 1024 / 1024,
                    user_name = process.StartInfo.UserName,
                    background_flag = process.MainWindowTitle.Length == 0
                }) ;
            }
            result.Data = list;
            return new JsonResult(result);
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("KillProcess")]
        public ActionResult KillProcess(int id)
        {
            var result = new ResponseResult();
            var processes = Process.GetProcesses();              
            var list = new List<Process>(processes);
            var process = list.Find(t=>t.Id == id);
            process.Kill();
            Thread.Sleep(200);
            if(process.HasExited)
            {
                result.Sucess("进程已经结束");
            }
            else
            {
                result.Message = "进程无法结束";
            }            
            return new JsonResult(result);
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="process_name">进程名</param>
        /// <param name="user_name">用户名</param>
        /// <returns></returns>
        [Route("KillProcess2")]
        public ActionResult KillProcess2(string process_name,string user_name)
        {
            var result = new ResponseResult();
            var processes = Process.GetProcesses();
            var list = new List<Process>(processes);

            process_name = process_name.ToLower();
            user_name = user_name.ToLower();

            bool all_kill_flag = true;
            int count = 0;
            int fail_count = 0;
            foreach (var item in list)
            {
                if (item.ProcessName.ToLower() != process_name)
                {
                    continue;
                }
                var owner_name = GetProcessOwnerName(item.Id);
                if (!string.IsNullOrEmpty(owner_name))
                {
                    owner_name = owner_name.ToLower();
                }
                else
                {
                    continue;
                }

                if (user_name == owner_name)
                {
                    item.Kill();
                    Thread.Sleep(200);
                }
                try
                {
                    if (item.HasExited)
                    {
                        count++;
                    }
                    else
                    {
                        all_kill_flag = false;
                        fail_count++;
                    }
                }
                catch
                { }
            }
            if (all_kill_flag)
            {
                result.Sucess($"成功清除{count}个目标进程");
            }
            else
            {
                result.Message = $"成功清除{count}个目标进程,{fail_count}停止失败";
            }            
            return new JsonResult(result);
        }

        private string GetProcessOwnerName(int processId)
        {
            var processes = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE ProcessId = " + processId);
            foreach (System.Management.ManagementObject process in processes.Get())
            {
                try
                {
                    string[] OwnerInfo = new string[2];
                    process.InvokeMethod("GetOwner", (object[])OwnerInfo);
                    return OwnerInfo[0];
                }
                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取网卡信息
        /// </summary>
        /// <returns></returns>
        [Route("GetNetwok")]
        public ActionResult GetNetwok()
        {
            var result = new ResponseResult();

            var ip_list = new List<object>();
            var adepters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var adepter in adepters)
            {
                var item = new
                {
                    id = adepter.Id,
                    name = adepter.Name,
                    description = adepter.Description,
                    connect_flag = adepter.OperationalStatus == OperationalStatus.Up,
                    adepter.NetworkInterfaceType,
                    ips = new List<object>()
                };
                var ips_address = adepter.GetIPProperties().UnicastAddresses;
                foreach (var ip_address in ips_address)
                {
                    if (ip_address.Address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        continue;//过滤IPV6
                    }
                    string ip = ip_address.Address.ToString();
                    if (ip == "127.0.0.1")
                    {
                        continue;
                    }
                    item.ips.Add(new
                    {
                        ip,
                        ip_mak = ip_address.IPv4Mask?.ToString(),
                        ip_address.SuffixOrigin
                    });
                }
                if (item.ips.Count == 0)
                {
                    continue;
                }
                ip_list.Add(item);
            }
            result.Data = ip_list;
            return new JsonResult(result);
        }

        /// <summary>
        /// 获取硬盘信息
        /// </summary>
        /// <returns></returns>
        [Route("GetDrives")]
        public ActionResult GetDrives()
        {
            var result = new ResponseResult();

            DriveInfo[] dirves = DriveInfo.GetDrives();

            var dirve_list = new List<object>();
            foreach (var dirve in dirves)
            {
                if (!dirve.IsReady)
                {
                    continue;
                }
                dirve_list.Add(
                new
                {
                    name = dirve.Name.Substring(0, dirve.Name.IndexOf(":")),
                    label = dirve.VolumeLabel,
                    drive_type = SystemHelper.GetDirveType(dirve.DriveType),
                    drive_format = dirve.DriveFormat,
                    root_dir = dirve.RootDirectory.FullName,
                    total_size = SystemHelper.GetSize(dirve.TotalSize),
                    available_space = SystemHelper.GetSize(dirve.AvailableFreeSpace)
                });
            }

            result.Data = dirve_list;
            return new JsonResult(result);
        }

        /// <summary>
        /// 关机
        /// </summary>
        /// <returns></returns>
        [Route("ShutDown")]
        public ActionResult ShutDown()
        {
            var result = new ResponseResult();
            OSService.PowerOff();
            result.Sucess("操作成功");
            return new JsonResult(result);
        }

        /// <summary>
        /// 重启
        /// </summary>
        /// <returns></returns>
        [Route("Reboot")]
        public ActionResult Reboot()
        {
            var result = new ResponseResult();
            OSService.Reboot();
            result.Sucess("操作成功");
            return new JsonResult(result);
        }


        /// <summary>
        /// 命令行执行
        /// </summary>
        /// <param name="command">命令行</param>
        /// <returns></returns>
        [Route("command")]
        public ActionResult Command(string command)
        {
            var result = new ResponseResult();
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;            
            process.Start();
            result.Data = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return new JsonResult(result);
        }
        private string result = "";
        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                result += e.Data + Environment.NewLine;
            }
        }
        /// <summary>
        /// 执行bat
        /// </summary>
        /// <param name="bat">bat脚本</param>
        /// <returns></returns>
        [Route("bat")]
        public ActionResult Bat(string bat)
        {
            var result = new ResponseResult();
            var dictionary = Environment.CurrentDirectory;
            var filename = $"{Guid.NewGuid().ToString("N")}.bat";
            using (FileStream fs = File.Create(Path.Combine(dictionary, filename)))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(bat);
                sw.Flush();
                sw.Close();
            }
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WorkingDirectory = dictionary;
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = string.Format("10");
            process.Start();
            process.WaitForExit();
            result.Data = process.StandardOutput.ReadToEnd();
            return new JsonResult(result);
        }

    }
}
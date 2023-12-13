using Proxy.Common;
using Proxy.Common.Setting;
using System;
using System.Diagnostics;
using System.HttpProxy;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Web_Proxy.Api
{
    /// <summary>
    /// 客户端控制器
    /// </summary>
    [Route("api/client")]
    public class ClientController : ApiController
    {
        /// <summary>
        /// 监测是否正常运行
        /// </summary>
        /// <returns></returns>
        [Route("run")]
        public ActionResult Run()
        {
            var result = new ResponseResult();
            result.Sucess("运行中");
            return new JsonResult(result);
        }

        /// <summary>
        /// 退出客户端
        /// </summary>
        /// <returns></returns>
        [Route("exit")]
        public ActionResult Exit()
        {
            var result = new ResponseResult();
            var thread = new Thread(() =>
            {
                Thread.Sleep(1000);
                Application.Exit();
            });
            thread.IsBackground = true;
            thread.Start();
            result.Sucess("客户端已退出");
            return new JsonResult(result);
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        [Route("version")]
        public ActionResult Version()
        {
            var result = new ResponseResult();            
            result.Data = Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion;
            return new JsonResult(result);
        }

        /// <summary>
        /// 更新新版本
        /// </summary>
        /// <param name="url">需要更新的应用</param>
        /// <returns></returns>
        [Route("update")]
        public ActionResult Update(string url)
        {
            var result = new ResponseResult();

            //获取下载器            
            string updaterPath = Environment.CurrentDirectory + @"\Updater.exe";            
            if (!File.Exists(updaterPath))
            {
                int index = url.LastIndexOf('/');
                string updaterUrl = url.Substring(0, index) + "/Updater.exe";
                //下载Updater
                if (!new HttpHelper().Download(updaterUrl, updaterPath))
                {
                    result.Message = "更新程序下载失败！";
                    return new JsonResult(result);
                }
            }
            string updateDir = Environment.CurrentDirectory + @"\update";
            if (Directory.Exists(updateDir))
            {
                Directory.Delete(updateDir, true);
            }
            Directory.CreateDirectory(updateDir);
            //执行文件
            string excPath = Process.GetCurrentProcess().MainModule.FileName;
            int lastIndex = excPath.LastIndexOf('\\') + 1;
            string excName = excPath.Substring(lastIndex, excPath.Length - lastIndex);
            string updatePath = updateDir + @"\" + excName;
            if (!new HttpHelper().Download(url, updatePath))
            {
                result.Message = "更新文件下载失败！";
                return new JsonResult(result);
            }
            //启动更新程序
            if (File.Exists(updaterPath))
            {
                string args = updateDir + " " + excPath;
                Process.Start(updaterPath, args);
            }
            return new JsonResult(result);
        }
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <returns></returns>
        [Route("GetSetting")]
        public ActionResult GetSetting()
        {
            var result = new ResponseResult();
            var configManager  = new SettingManager();
            var config = configManager.Config.Read();
            result.Data = config;
            return new JsonResult(result);
        }
        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <returns></returns>
        [Route("SetSetting")]
        public ActionResult SetSetting([FromBody]SettingConfig setting)
        {
            var result = new ResponseResult();
            if (setting != null)
            {
                var configManager = new SettingManager();
                if (configManager.Config.Write(setting))
                {
                    result.Sucess("修改成功!");
                }
            }
            else
            {
                result.Message = "修改失败!";
            }
            return new JsonResult(result);
        }
    }
}
using Newtonsoft.Json;
using Proxy.Common;
using Proxy.Common.Setting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.HttpProxy;
using System.IO;
using System.Windows.Forms;
using Web_Proxy.Models;
using WebProxy.Plugin;

namespace Web_Proxy.Api
{
    /// <summary>
    /// 插件控制器
    /// </summary>
    [Route("api/plugin")]
    public class PluginController : ApiController
    {
        /// <summary>
        /// 下载插件
        /// </summary>
        /// <param name="pluginId">插件ID</param>
        /// <returns></returns>
        [Route("download")]
        public ActionResult Download(string pluginId, string baseUrl)
        {
            var result = new ResponseResult2();
            if (string.IsNullOrEmpty(pluginId))
            {
                result.Message = "插件id不能为空!";
                return new JsonResult(result);
            }

            //获取插件文件信息
            var url = baseUrl + "/api/plugin/GetFiles";
            var res = JsonConvert.DeserializeObject<ResponseResult2>(new HttpHelper().Get(url + "?pluginId=" + pluginId));
            if (!res.IsSuccess())
            {
                result.Message = "获取插件信息失败!";
                return new JsonResult(result);
            }
            var plugins = JsonConvert.DeserializeObject<List<PluginFileView>>(res.Data.ToString());

            //客户端插件存放路径
            string dir = Environment.CurrentDirectory + "\\" + Guid.NewGuid().ToString("N");

            //从服务器下载插件到客户端是否成功
            var flag = true;

            //存放已下载插件的客户端配置文件
            var list = new List<PluginConfig>();
            foreach (var item in plugins)
            {
                //从服务器读取插件
                var _url = baseUrl + "/api/plugin/DownloadFile";
                var _res = JsonConvert.DeserializeObject<ResponseResult2>(new HttpHelper().Get(_url + "?file_id=" + item.file_id));
                if (!_res.IsSuccess())
                {
                    if (_res == null)
                    {
                        result.Message = "文件下载失败!";
                    }
                    else
                    {
                        result.Message = _res.Message;
                    }

                    return new JsonResult(result);
                }

                //从服务器拿到插件
                var file = JsonConvert.DeserializeObject<FileView>(_res.Data.ToString());

                //将服务器拿到的插件下载到本地磁盘
                if (!DownloadFile(file, dir))
                {
                    flag = false;
                }
                else
                {
                    if (item.main_flag == "*")
                    {
                        var plugin = new PluginConfig();
                        plugin.Name = item.file_name;
                        plugin.Path = $@"{dir}\{item.file_name}";//全路径
                        plugin.CreateTime = DateTime.Now;
                        plugin.From = PluginFrom.Server;
                        plugin.Discription = item.description;
                        plugin.Plugin = new PluginModel
                        {
                            ID = item.plugin_id,
                            Key = item.key,
                            Name = item.plugin_name,
                            Version = item.version,
                            VersionNo = item.version_no
                        };
                        list.Add(plugin);
                    }
                }
            }

            //全部插件下载成功
            if (flag)
            {
                var update_plugin_path = Environment.CurrentDirectory + @"\update_plugin.wp";
                if (File.Exists(update_plugin_path))
                {
                    //已有下载但未更新插件，合并
                    var update_plugin = new Config<List<PluginConfig>>(update_plugin_path).Read();
                    list.AddRange(update_plugin);
                }

                //保存 【插件更新列】表成功
                if (new PluginManager("update_plugin.wp").Config.Write(list))
                {
                    //生成默认配置
                    var setUrl = baseUrl + "/api/pluginSetting/GetDefSetting";
                    var _res = JsonConvert.DeserializeObject<ResponseResult2>(new HttpHelper().Get(setUrl + "?pluginId=" + pluginId));

                    if (!_res.IsSuccess())
                    {
                        if (_res.Message != "未找到默认配置")
                        {
                            result.Message = "获取插件默认配置失败!";
                            return new JsonResult(result);
                        }

                        result.Sucess("下载成功");
                    }
                    else
                    {
                        //写入插件默认配置文件
                        var setPath = dir + @"\setting.cfg";
                        var setting = _res.Data.ToString();
                        var setflag = false;
                        if (!string.IsNullOrEmpty(setting))
                        {
                            setflag = new Config<Dictionary<string, string>>(setPath).Write(JsonConvert.DeserializeObject<Dictionary<string, string>>(setting));
                        }
                        if (!setflag)
                        {
                            result.Message = "默认配置写入失败";
                            return new JsonResult(result);
                        }
                        else
                        {
                            result.Sucess("下载成功");
                        }
                    }
                }
                else
                {
                    result.Message = "更新配置文件写入失败!";
                }
            }
            else
            {
                result.Message = "下载失败";
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// 获取本地配置
        /// </summary>
        /// <param name="pluginId">插件ID</param>       
        /// <returns></returns>
        [Route("GetSetting")]
        public ActionResult GetSetting(string plugin_id)
        {
            var result = new ResponseResult2();
            var setting = PluginManager.Current.GetSetting(plugin_id);
            if (setting != null)
            {
                result.Data = setting.Get();
            }
            else
            {
                result.Message = "暂无插件配置";
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// 修改本地配置
        /// </summary>
        /// <param name="pluginId">插件ID</param>       
        /// <returns></returns>
        [Route("SetSetting")]
        public ActionResult SetSetting([FromBody] ConfigFile config)
        {
            var result = new ResponseResult2();

            if (PluginManager.Current.SetSetting(config.plugin_id
                , JsonConvert.DeserializeObject<Dictionary<string, string>>(config.configs)))
            {
                result.Sucess("保存成功");
            }
            else
            {
                result.Message = "保存失败";
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// 更新所有插件
        /// </summary>
        /// <returns></returns>
        [Route("Update")]
        public ActionResult Update(string clientID)
        {
            var result = new ResponseResult();
            string updaterPath = Environment.CurrentDirectory + @"\Update-Plugin.exe";
            if (File.Exists(updaterPath))
            {
                var setting = new SettingManager().Config.Read();
                var args = "";
                args += " " + Environment.CurrentDirectory + @"\update_plugin.wp";
                args += " " + Environment.CurrentDirectory + @"\plugin.wp";
                args += " " + Environment.CurrentDirectory + @"\Web-Proxy.exe";
                args += " " + setting.BaseApi;
                args += " " + clientID;
                Process.Start(updaterPath, args);//启动指定路径的外部程序，并将参数传递给它。
            }
            else
            {
                result.Message = "未找到更新程序";
            }
            return new JsonResult(result);
        }
        private bool DownloadFile(FileView file, string localfile)
        {
            var result = false;
            try
            {
                if (!Directory.Exists(localfile))
                {
                    Directory.CreateDirectory(localfile);
                }
                string strbase64 = file.base64Str.Trim().Substring(file.base64Str.IndexOf(",") + 1);   //将‘，’以前的多余字符串删除
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strbase64));
                FileStream fs = new FileStream(localfile + "\\" + file.file_name, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] b = stream.ToArray();
                fs.Write(b, 0, b.Length);
                fs.Close();
                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        [Route("Uninstall")]
        public ActionResult Uninstall(string pluginId)
        {
            var result = new ResponseResult2();

            var plugins = new PluginManager().Config.Read();

            if (plugins != null)
            {
                var config = plugins.Find(t => t.Plugin.ID == pluginId);
                if (config == null)
                {
                    result.Message = "客户端未找到插件";
                    return new JsonResult(result);
                }

                //1,移除插件文件夹
                try
                {
                    var delete_file = config.Path.Substring(0, config.Path.LastIndexOf("\\"));
                    Directory.Delete(delete_file, true);
                }
                catch (Exception e)
                {
                    Logger.WriteError($"移除插件文件夹失败:{e.Message}");
                    result.Message = "移除插件文件夹失败";
                    return new JsonResult(result);
                }

                //将插件从插件列表文件中移除
                plugins.Remove(config);
            }

            //2，更新插件列表文件
            if (new PluginManager().Config.Write(plugins))
            {
                //读取配置文件 获取client的token
                var _config = new SettingManager().Config.Read();
                try
                {
                    //3，更新数据表
                    string url = _config.BaseApi + "/api/client/DeleteClientPlugin";
                    var res = JsonConvert.DeserializeObject<ResponseResult2>(new HttpHelper().Get(url + "?plugin_id=" + pluginId + "&client_token=" + _config.Token));
                    if (res.IsSuccess())
                    {
                        result.Sucess("插件卸载成功");

                        //4，更新客户端
                        RestartApplication();
                    }
                    else
                    {
                        result.Message = "更新数据表失败";
                        Logger.WriteError($"更新数据表失败");
                    }
                }
                catch (Exception e)
                {
                    result.Message = "插件卸载失败";
                    Logger.WriteError($"插件卸载失败:{e.Message}");
                }
            }
            else
            {
                result.Message = "更新插件列表失败";
                Logger.WriteError($"更新插件列表失败");
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 更新客户端
        /// </summary>
        private void RestartApplication()
        {
            // 获取当前进程  
            Process currentProcess = Process.GetCurrentProcess();

            // 关闭应用程序进程  
            currentProcess.Close();

            // 等待一段时间以确保进程已经完全关闭  
            System.Threading.Thread.Sleep(2000);

            // 重新启动应用程序  
            Process.Start(Application.ExecutablePath);
        }
    }
}
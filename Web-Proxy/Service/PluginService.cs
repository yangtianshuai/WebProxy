using Proxy.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Proxy.Service
{
    public class PluginService : ApiService
    {
        /// <summary>
        /// 获取客户端插件
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ResponseResult2 GetClientPlugins(string clientId)
        {
            var url = $@"{this.BaseUrl}/api/plugin/GetClientPlugins?id={clientId}";
            return GetResult(new HttpHelper().Get(url));
        }
    }
}
using Proxy.Common;
using System;
using System.Diagnostics;
using System.HttpProxy;

namespace Web_Proxy
{
    public class ApiFilter : IHttpFilter
    {
        private Stopwatch watch { get; set; }

        public void Exception(Exception ex)
        {
            Logger.WriteError(ex.InnerException.StackTrace, ex.InnerException.Message);
        }

        public void Request(HttpClient client)
        {            
            watch = new Stopwatch();
            watch.Start();
        }

        public void Response(HttpClient client)
        {
            watch.Stop();
            if (client.Response != null 
                && client.Response.Status != ResponseStatus.OK)
            {
                string info = $"\r\n地址：{client.Request.URL}\r\n" +
                        $"方式：{client.Request.Method}\r\n" +
                        $"请求体：{client.Request.Body}\r\n" +
                        $"结果：{client.Response.Result?.ToResponse()}\r\n" +
                        $"客户端IP：{client.GetRemoteIp()}\r\n" +
                        $"耗时：{watch.Elapsed.TotalMilliseconds} 毫秒";
                Logger.WriteTrace(info);
            }           
        }
    }
}
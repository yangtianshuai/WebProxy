using Proxy.Common;
using System;
using System.Collections.Generic;
using System.HttpProxy;
using System.Text;
using System.Web;

namespace Web_Proxy.Api
{
    /// <summary>
    /// 开放访问
    /// </summary>
    [Route("api/open")]
    public class OpenController : ApiController
    {
        [Route("api")]
        public ActionResult Api(string app_id, string url)
        {
            string result = "";

            var dict = new SortedDictionary<string, string>();
            if (Request.Body != null && Request.Body.Length > 0)
            {
                if (Request.Body.Length > 8)
                {
                    dict.Add("body", Request.Body.Substring(0, 8));
                }
                else
                {
                    dict.Add("body", Request.Body);
                }
            }
            else
            {
                int index = Request.URL.IndexOf("?");
                if (index > 0)
                {
                    string param = Request.URL.Substring(index + 1, Request.URL.Length - index - 1);
                    string[] array = param.Split('&');

                    foreach (var item in array)
                    {
                        string[] keyValue = item.Split('=');
                        if (keyValue.Length == 2)
                        {
                            dict.Add(keyValue[0], HttpUtility.UrlDecode(keyValue[1]));
                        }
                    }
                }
            }

            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string nonce = new Random(DateTime.Now.Millisecond).Next(0, int.MaxValue).ToString();

            var sign = "";
            sign = $"AppId={app_id},Timestamp={timestamp},Nonce={nonce},Signature={sign}";

            if (Request.Method == HttpVerb.Get)
            {
                result = new HttpHelper().Get(url, request =>
                {
                    request.Headers.Add("OPEN-SIGN", sign);
                });
            } 
            else if (Request.Method == HttpVerb.Post)
            {
                result = new HttpHelper().Post(url, Request.Body,request =>
                {
                    request.Headers.Add("OPEN-SIGN", sign);
                });
            }
            else
            {
                result = "不支持的谓词";
            }
           
            return new DirectResult(result);
        }

        /// <summary>
        /// 快速MD5
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string MD5(string data)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var dataByte = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder result = new StringBuilder();
            foreach (var c in dataByte)
            {
                result.Append((255 - c).ToString("X2"));
            }
            return result.ToString().ToUpper();
        }
    }
}

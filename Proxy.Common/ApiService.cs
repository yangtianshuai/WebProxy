using Newtonsoft.Json;

namespace Proxy.Common
{
    public class ApiService : ConfigService
    {
        /// <summary>
        /// 基础配置
        /// </summary>
        public string BaseUrl { get; set; } 
        //宏力后台api所在服务器
        public string ServerBaseUrl { get; set; }

        public ApiService() {
            this.BaseUrl = this.Path;
        }

        public ResponseResult2 GetResult(string response)
        {
            ResponseResult2 result = null;
            try
            {
                result = JsonConvert.DeserializeObject<ResponseResult2>(response);
            }
            catch { }
            if (result == null)
            {
                result = new ResponseResult2();
            }
            return result;
        }
        
    }
}
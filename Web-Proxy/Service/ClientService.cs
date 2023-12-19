using Newtonsoft.Json;
using Proxy.Common;
using Web_Proxy.Models;

namespace Web_Proxy.Service
{
    public class ClientService : ApiService
    {
        public ClientService() {
            this.BaseUrl = "http://localhost:8990";
        }


        public ClientService(string BaseUrl)
        {
            this.BaseUrl = BaseUrl;
        }

        public string  Config
        {
            get
            {
                return this.BaseUrl;
            }
            set
            {
                this.BaseUrl = value;
            }
        }

        public ResponseResult2 Register(ClientRegisterModel model)
        {
            var response = new HttpHelper().Post($@"{this.BaseUrl}/api/client/register"
                , JsonConvert.SerializeObject(model), (request) =>
                {
                    request.ContentType = "application/json";
                });        
            return GetResult(response);
        }

        /// <summary>
        /// 自动注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseResult2 AutoRegister(ClientRegisterModel model)
        {
            var response = new HttpHelper().Post($@"{this.BaseUrl}/api/client/AutoRegister"
                , JsonConvert.SerializeObject(model), (request) =>
                {
                    request.ContentType = "application/json";
                });
            return GetResult(response);
        }

        public ResponseResult2 ModifyClientIp(ClientRegisterModel model)
        {
            var response = new HttpHelper().Post($@"{this.BaseUrl}/api/client/ModifyClientIPs"
                , JsonConvert.SerializeObject(model), (request) =>
                {
                    request.ContentType = "application/json";
                });
            return GetResult(response);
        }
    }
}
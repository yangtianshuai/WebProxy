using Proxy.Common;
using System.HttpProxy;
using Web_Proxy.WS;
using WebProxy.Plugin;

namespace Web_Proxy.Api
{
    /// <summary>
    /// WebSocket控制器
    /// </summary>
    [Route("api/ws")]
    public class WSController : ApiController
    {
        /// <summary>
        /// 获取WebSocket配置
        /// </summary>       
        /// <returns></returns>
        [Route("get-config")]
        public ActionResult GetConfig()
        {
            var result = new ResponseResult();
            result.Data = new
            {
                port = SocketConfig.Port,
                second = SocketConfig.HeatBeatSecond
            };
            return new JsonResult(result);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        [Route("send")]
        public ActionResult Send(SocketMessage sm)
        {
            var result = new ResponseResult();
            if (SocketManager.Send(sm) > 0)
            {
                result.Sucess("发送成功！");
            }            
            return new JsonResult(result);
        }
    }
}

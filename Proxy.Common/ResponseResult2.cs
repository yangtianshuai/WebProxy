namespace Proxy.Common
{
    public class ResponseResult2
    {
        /// <summary>
        /// Code
        /// </summary>
        public int Code { get; set; }
        private object data;
        /// <summary>
        /// Data
        /// </summary>
        public object Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
                if (value != null)
                {
                    Code = 1;
                }
            }
        }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ResponseResult
        /// </summary>
        public ResponseResult2()
        {
            Code = 0;
            data = null;
            Message = "";
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message">提示消息</param>
        public void Sucess(string message = null)
        {
            Code = 1;
            if (message != null)
            {
                Message = message;
            }
        }
        /// <summary>
        /// IsSuccess
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess()
        {
            return Code == 1;
        }
    }
}

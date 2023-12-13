namespace Proxy.Common
{
    public class ResponseResult
    {       
        /// <summary>
        /// 成功标记
        /// </summary>
        public bool IsSuccess { get; set; }
        
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
                Sucess();
            }
        }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ResponseResult
        /// </summary>
        public ResponseResult()
        {
            IsSuccess = false;
            data = null;
            Message = "";
        }
        public ResponseResult(string message) : this()
        {
            Message = message;
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        public void Sucess(string message = null)
        {
            IsSuccess = true;
            if (message != null)
            {
                Message = message;
            }
        }
    }
}

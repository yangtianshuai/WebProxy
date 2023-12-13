using System;
using System.IO;
using System.Text;

namespace Proxy.Common
{
    /// <summary>
    /// 日志处理   
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public static LogLevel Level { get; set; } = LogLevel.Error;

        private static object writeLock = new object();

        #region --IMcisLogger 成员
        private static string Path
        {
            get
            {               
                return AppDomain.CurrentDomain.BaseDirectory + @"\log";
            }
        }
        private static string TracePath
        {
            get
            {
                string configuration = Path + @"\trace";
                return configuration;
            }
        }
        private static string ErrorPath
        {
            get
            {
                string configuration = Path + @"\error";
                return configuration;
            }
        }        
        #endregion

        #region --写入日志
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="trace">位置</param>      
        /// <param name="exception">错误对象</param>
        public static void WriteError(string trace, string exception)
        {
            lock (writeLock)
            {
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);

                var fileName = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var logPath = System.IO.Path.Combine(ErrorPath, fileName + ".log");
                if (!Directory.Exists(ErrorPath))
                {
                    Directory.CreateDirectory(ErrorPath);
                }
                var logMsg = string.Empty;
                if (!File.Exists(logPath))
                {
                    File.Create(logPath).Close();
                }
                else
                {
                    logMsg = "--------------------------------------------------------------\r\n";
                }

                using (var streamWriter = new StreamWriter(logPath, true, Encoding.UTF8))
                {                   
                    logMsg += string.Format("异常信息:{0}{1}位置：{2}{3}时间:{4}",
                        trace, Environment.NewLine, exception, Environment.NewLine, DateTime.Now);
                    streamWriter.WriteLine(logMsg);
                }
            }
        }        
        /// <summary>
        /// 写入错误日志
        /// </summary>    
        /// <param name="exception"></param>
        public static void WriteError(string exception)
        {
            WriteError("未知", exception);
        }        
        /// <summary>
        /// 写入Trace
        /// </summary>
        /// <param name="content"></param>
        public static void WriteTrace(string content)
        {
            lock (writeLock)
            {
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);

                var fileName = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var logPath = System.IO.Path.Combine(TracePath, fileName + ".log");
                if (!Directory.Exists(TracePath))
                    Directory.CreateDirectory(TracePath);
                if (!File.Exists(logPath))
                    File.Create(logPath).Close();

                using (var streamWriter = new StreamWriter(logPath, true, Encoding.UTF8))
                {
                    var serverMsg = string.Format("{0}  时间:{1}", content, DateTime.Now);
                    streamWriter.WriteLine(serverMsg);
                }
            }
        }
        #endregion

        #region --读取日志
        /// <summary>
        /// 读取本地错误日志
        /// </summary>
        /// <param name="date"></param>
        public static string ReadError(DateTime date)
        {
            var text = string.Empty;
            var fileName = date.ToString("yyyy-MM-dd");
            var logPath = System.IO.Path.Combine(ErrorPath, fileName + ".log");
            return Read(logPath);
        }
        
        /// <summary>
        /// 读取服务日志
        /// </summary>
        /// <param name="date"></param>
        public static string ReadTrace(DateTime date)
        {
            var text = string.Empty;
            var fileName = date.ToString("yyyy-MM-dd");
            var path = System.IO.Path.Combine(TracePath, fileName + ".log");
            return Read(path);
        }       
        /// <summary>
        /// 读取日志
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string Read(string path)
        {
            string text = string.Empty;
            if (!File.Exists(path))
            {
                return text;
            }
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            if (fs.CanRead)
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    string strline = sr.ReadLine();
                    StringBuilder sb = new StringBuilder();
                    while (strline != null)
                    {
                        sb = sb.Append(strline + "\n");
                        strline = sr.ReadLine();
                    }
                    text = sb.ToString();
                }
            }
            //fs.Close();
            return text;
        }
        #endregion

        #region --删除日志
        /// <summary>
        /// 删除某时间之前的错误日志
        /// </summary>
        /// <param name="time"></param>       
        public static void DeleteErrorBeforeDate(DateTime time)
        {
            if (Directory.Exists(ErrorPath))
            {
                DirectoryInfo di = new DirectoryInfo(ErrorPath);
                FileInfo[] files = di.GetFiles();
                FileInfo file;
                for (int i = 0; i < files.Length; i++)
                {
                    file = files[i];
                    if (file.CreationTime.Date < time.Date)
                    {
                        if (!file.IsReadOnly)
                        {
                            file.Delete();
                            //i--;
                        }
                    }
                }
            }
            else
            {
                //WriteError(string.Format("不存在错误日志路径[{0}]", ErrorPath));
            }
        }        
        /// <summary>
        /// 删除某时间之前的跟踪日志
        /// </summary>
        /// <param name="time"></param>
        public static void DeleteTraceBeforeDate(DateTime time)
        {
            if (Directory.Exists(TracePath))
            {
                DirectoryInfo di = new DirectoryInfo(TracePath);
                FileInfo[] files = di.GetFiles();
                FileInfo file;
                for (int i = 0; i < files.Length; i++)
                {
                    file = files[i];
                    if (file.CreationTime.Date < time.Date)
                    {
                        if (!file.IsReadOnly)
                        {
                            file.Delete();
                            //i--;
                        }
                    }
                }
            }            
        }
        #endregion
    }
}
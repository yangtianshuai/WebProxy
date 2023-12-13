using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Proxy.Common
{
    /// <summary>
    /// 配置管理
    /// 创建人：YTS
    /// 创建时间：2019-3-6
    /// </summary>
    public class Config<T>
    {
        private string _path;        
        public Config(string path)
        {
            _path = path;
        }
        public bool Write(T t)
        {           
            try
            {
                if (File.Exists(_path))
                {
                    File.Delete(_path);
                }
                string json = JsonConvert.SerializeObject(t);
                byte[] content = Encoding.UTF8.GetBytes(json);
                using (FileStream fs = new FileStream(_path, FileMode.OpenOrCreate))
                {
                    try
                    {                        
                        BinaryWriter write = new BinaryWriter(fs);
                        fs.Position = 0;
                        for (int i = 0; i < content.Length; i++)
                        {
                            write.Write((byte)(255 - content[i]));
                        }
                        write.Close();
                        return true;
                    }
                    catch { }
                }
                new FileInfo(_path).Attributes = FileAttributes.Hidden;
            }
            catch { }
            return false;
        }
        public T Read()
        {
            if (!File.Exists(_path))
            {
                return default(T);
            }
            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    fs.Position = 0;
                    // 读取 
                    BinaryReader read = new BinaryReader(fs);
                    byte[] buffer = new byte[fs.Length];
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = (byte)(255 - read.ReadByte());
                    }
                    string content = Encoding.UTF8.GetString(buffer);
                    return JsonConvert.DeserializeObject<T>(content);
                }
                catch(Exception ex)
                {

                }
            }
            return default(T);
        }
    }
}
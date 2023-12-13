using System;
using System.IO;
using System.Reflection;

namespace Proxy.Common
{
    public class ProxyManager
    {
        private string basePath = Environment.GetFolderPath(Environment.SpecialFolder.System);

        public ProxyManager()
        {
            basePath = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(basePath);
            string path = Uri.UnescapeDataString(uri.Path);
            basePath = Path.GetDirectoryName(path);
            //basePath = Environment.CurrentDirectory;
        }

        public string BaseDictionary
        {
            get
            {
                string path = basePath + "\\";
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                return path;
            }
        }
    }
}

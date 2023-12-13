using System;
using System.IO;

namespace Proxy.Common
{
    public static class FileHelper
    {
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="src_path">源文件</param>
        /// <param name="distinct_path"></param>
        /// <param name="overwrite_flag"></param>
        /// <returns></returns>
        public static bool CopyDirectory(string src_path, string distinct_path, bool overwrite_flag)
        {
            bool ret = false;
            try
            {
                src_path = src_path.EndsWith(@"\") ? src_path : src_path + @"\";
                distinct_path = distinct_path.EndsWith(@"\") ? distinct_path : distinct_path + @"\";

                if (Directory.Exists(src_path))
                {
                    if (!Directory.Exists(distinct_path))
                    {
                        Directory.CreateDirectory(distinct_path);
                    }

                    foreach (string fls in Directory.GetFiles(src_path))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(distinct_path + flinfo.Name, overwrite_flag);
                    }
                    foreach (string drs in Directory.GetDirectories(src_path))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, distinct_path + drinfo.Name, overwrite_flag) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
}

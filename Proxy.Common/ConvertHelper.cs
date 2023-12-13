using System;
using System.Collections.Generic;
using System.Reflection;

namespace Proxy.Common
{
    public static class ConvertHelper
    {
        public static Dictionary<string, object> ModelToMap(object model)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            Type t = model.GetType(); // 获取对象对应的类， 对应的类型

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance); // 获取当前type公共属性

            foreach (PropertyInfo p in pi)
            {
                MethodInfo m = p.GetGetMethod();

                if (m != null && m.IsPublic)
                {
                    // 进行判NULL处理 
                    if (m.Invoke(model, new object[] { }) != null)
                    {
                        map.Add(p.Name, m.Invoke(model, new object[] { })); // 向字典添加元素
                    }
                }
            }
            return map;
        }
        
        public static string[] ByteToStrings(byte[] buffer)
        {
            var array = new string[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                array[i] = buffer[i] + "";
            }
            return array;
        }

        public static string DictionToNameValue(string url, Dictionary<string, object> dict)
        {
            url += "?";
            foreach (var di in dict)
            {
                url += "";
            }
            return url;
        }

        public static string Hex2Ten(string hex)
        {
            int ten = 0;
            for (int i = 0, j = hex.Length - 1; i < hex.Length; i++)
            {
                ten += HexChar2Value(hex.Substring(i, 1)) * ((int)Math.Pow(16, j));
                j--;
            }
            return ten.ToString();
        }

        public static int HexChar2Value(string hexChar)
        {
            switch (hexChar)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(hexChar);
                case "a":
                case "A":
                    return 10;
                case "b":
                case "B":
                    return 11;
                case "c":
                case "C":
                    return 12;
                case "d":
                case "D":
                    return 13;
                case "e":
                case "E":
                    return 14;
                case "f":
                case "F":
                    return 15;
                default:
                    return 0;
            }
        }
    }
}
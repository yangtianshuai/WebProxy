using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Proxy.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetEnum(Type type, string value)
        {
            return Enum.Parse(type, value);
        }
        /// <summary>
        /// 获取指定枚举类型的Descripton
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="obj">值</param>
        /// <returns>Descripton</returns>
        public static string GetDescription(Type type, int value)
        {
            //如果不是枚举类型,则返回空
            if (type.BaseType.FullName != "System.Enum")
            {
                return string.Empty;
            }
            //如果值为空则返回空             
            FieldInfo[] fields = type.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                if ((int)System.Enum.Parse(type, fields[i].Name) == value)
                {
                    DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].
                        GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (EnumAttributes.Length > 0)
                    {
                        return EnumAttributes[0].Description;
                    }
                }
            }
            return string.Empty;
        }
        
    }
}
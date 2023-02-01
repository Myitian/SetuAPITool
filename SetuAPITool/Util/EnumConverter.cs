using System;

namespace Myitian.SetuAPITool.Util
{
    /// <summary>枚举转换器</summary>
    public static class EnumConverter
    {
        /// <summary>转换为字符串</summary>
        public static string ToString<T>(T value) where T : Enum
        {
            string s = value.ToString().ToLower();
            return s[0] == '_' ? s.Substring(1) : s;
        }
        /// <summary>转换为枚举</summary>
        public static T ToEnum<T>(string value) where T : Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return (T)Enum.Parse(typeof(T), "_" + value, true);
            }
        }
    }
}

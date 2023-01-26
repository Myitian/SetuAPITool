using System;

namespace SetuAPITool.Util
{
    public static class EnumConverter
    {
        public static string ToString<T>(T value) where T : Enum
        {
            string s = value.ToString().ToLower();
            return s[0] == '_' ? s.Substring(1) : s;
        }
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

using System;
using System.Collections.Generic;
using System.Text;

namespace SetuAPITool.Util
{
    public static class EnumConverter
    {
        public static string ToString<T>(T value) where T : Enum
        {
            return value.ToString().ToLower();
        }
        public static T ToEnum<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}

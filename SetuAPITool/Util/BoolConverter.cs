namespace Myitian.SetuAPITool.Util
{
    /// <summary>布尔值转换器</summary>
    public static class BoolConverter
    {
        /// <summary>转换为布尔值</summary>
        public static bool ToBool(string value)
        {
            return !(value == null || value == "false" || value == "0" || value == "null");
        }
        /// <summary>转换为字符串</summary>
        public static string ToString(bool value)
        {
            return value ? "true" : "false";
        }
    }
}

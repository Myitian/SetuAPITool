namespace SetuAPITool.Util
{
    public static class BoolConverter
    {
        public static bool ToBool(string value)
        {
            return !(value == null || value == "false" || value == "0" || value == "null");
        }
        public static string ToString(bool value)
        {
            return value ? "true" : "false";
        }
    }
}

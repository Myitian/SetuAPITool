using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SetuAPITool.Util
{
    public static class ParametersConverter
    {
        public static string URLEncode(params KeyValuePair<string, string>[] parameters)
        {
            return string.Join("&", parameters.Select(x => ToString(x)));
        }

        public static string ToString(KeyValuePair<string, string> parameter, string seperator = "=")
        {
            return WebUtility.UrlEncode(parameter.Key) + seperator + WebUtility.UrlEncode(parameter.Value);
        }
    }
}

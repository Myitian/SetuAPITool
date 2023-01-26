using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SetuAPITool.Util
{
    public static class ParametersConverter
    {

        public static string ToString(string seperator0 = "&", string seperator1 = "=", params KeyValuePair<string, string>[] parameters)
        {
            return string.Join(seperator0, parameters.Select(x => ToString(x, seperator1)));
        }

        public static string ToString(KeyValuePair<string, string> parameter, string seperator = "=")
        {
            return parameter.Key + seperator + parameter.Value;
        }

        public static string UrlEncode(params KeyValuePair<string, string>[] parameters)
        {
            return string.Join("&", parameters.Select(UrlEncode));
        }
        public static string UrlEncode(KeyValuePair<string, string> parameter)
        {
            return WebUtility.UrlEncode(parameter.Key) + '=' + WebUtility.UrlEncode(parameter.Value);
        }
    }
}

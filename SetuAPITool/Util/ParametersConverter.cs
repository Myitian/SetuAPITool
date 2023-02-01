using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Myitian.SetuAPITool.Util
{
    /// <summary>参数转换器</summary>
    public static class ParametersConverter
    {
        /// <summary>转换为字符串</summary>
        /// <param name="seperator0">0级分隔符</param>
        /// <param name="seperator1">1级分隔符</param>
        /// <param name="parameters">键值对参数</param>
        public static string ToString(string seperator0 = "&", string seperator1 = "=", params KeyValuePair<string, string>[] parameters)
        {
            return string.Join(seperator0, parameters.Select(x => ToString(x, seperator1)));
        }
        /// <summary>转换为字符串</summary>
        /// <param name="parameter">键值对参数</param>
        /// <param name="seperator">分隔符</param>
        public static string ToString(KeyValuePair<string, string> parameter, string seperator = "=")
        {
            return parameter.Key + seperator + parameter.Value;
        }

        /// <summary>URL编码并转换为字符串</summary>
        public static string UrlEncode(params KeyValuePair<string, string>[] parameters)
        {
            if (parameters == null)
            {
                return null;
            }
            else
            {
                return string.Join("&", parameters.Select(UrlEncode));
            }
        }
        /// <summary>URL编码并转换为字符串</summary>
        public static string UrlEncode(KeyValuePair<string, string> parameter)
        {
            return WebUtility.UrlEncode(parameter.Key) + '=' + WebUtility.UrlEncode(parameter.Value);
        }
    }
}

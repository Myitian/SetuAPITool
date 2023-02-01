using Newtonsoft.Json;
using System.Collections.Generic;

namespace Myitian.SetuAPITool.Jitsu
{
    /// <summary>Jitsu 响应</summary>
    public class Response
    {
        /// <summary>响应码</summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>附加信息</summary>
        [JsonProperty("alert")]
        public string Alert { get; set; }

        /// <summary>图像</summary>
        [JsonProperty("pics")]
        public List<string> Pics { get; set; }
    }
}

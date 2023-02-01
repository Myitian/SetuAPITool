using Newtonsoft.Json;
using System.Collections.Generic;

namespace Myitian.SetuAPITool.LoliconAPI.V1
{
    /// <summary>Lolicon API V1 响应</summary>
    public class Response
    {
        /// <summary>返回码</summary>
        [JsonProperty("code")]
        public Code Code { get; set; }

        /// <summary>错误信息之类的</summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }

        /// <summary>结果数</summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>色图数组</summary>
        [JsonProperty("data")]
        public List<PixivInfo> Data { get; set; }
    }
}

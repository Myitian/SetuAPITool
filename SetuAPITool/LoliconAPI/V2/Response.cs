using Newtonsoft.Json;
using System.Collections.Generic;

namespace Myitian.SetuAPITool.LoliconAPI.V2
{
    /// <summary>Lolicon API V2 响应</summary>
    public class Response
    {
        /// <summary>错误信息</summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>色图数组</summary>
        [JsonProperty("data")]
        public List<Setu> Data { get; set; }

    }
}

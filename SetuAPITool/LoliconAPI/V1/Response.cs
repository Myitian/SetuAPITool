using Newtonsoft.Json;
using System.Collections.Generic;

namespace SetuAPITool.LoliconAPI.V1
{
    public class Response
    {
        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public List<PixivInfo> Data { get; set; }
    }
}

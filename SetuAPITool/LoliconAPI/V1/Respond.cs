using Newtonsoft.Json;

namespace SetuAPITool.LoliconAPI.V1
{
    public class Respond
    {
        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public PixivInfo[] Data { get; set; }
    }
}

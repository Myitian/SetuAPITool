using Newtonsoft.Json;

namespace SetuAPITool.LoliconAPI.V2
{
    public class Respond
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("data")]
        public Setu[] Data { get; set; }

    }
}

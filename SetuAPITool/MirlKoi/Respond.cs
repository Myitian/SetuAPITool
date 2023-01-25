using Newtonsoft.Json;

namespace SetuAPITool.MirlKoi
{
    public class Respond
    {
        [JsonProperty("pic")]
        public string[] Pic { get; set; }
    }
}

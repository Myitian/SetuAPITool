using Newtonsoft.Json;
using System.Collections.Generic;

namespace SetuAPITool.Jitsu
{
    public class Response
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("pics")]
        public List<string> Pics { get; set; }
    }
}

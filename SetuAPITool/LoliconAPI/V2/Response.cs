using Newtonsoft.Json;
using System.Collections.Generic;

namespace SetuAPITool.LoliconAPI.V2
{
    public class Response
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("data")]
        public List<Setu> Data { get; set; }

    }
}

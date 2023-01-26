using Newtonsoft.Json;
using System.Collections.Generic;

namespace SetuAPITool.MirlKoi
{
    public class Response
    {
        [JsonProperty("pic")]
        public List<string> Pic { get; set; }
    }
}
